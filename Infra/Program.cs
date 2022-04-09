using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Environment = Amazon.CDK.Environment;

string clusterArn = "arn:aws:ecs:eu-west-1:464787150360:cluster/CoreStack-MainEcsCluster03D3CD1A-JeWB2ioJZQEy";

var app = new App();

var stack = new Stack(app, "pika", new StackProps
{
    Env = new Environment
    {
        Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
        Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
    },
});

var mainCluster = Cluster.FromClusterAttributes(stack, "mainCluster", new ClusterAttributes
{
    ClusterArn = clusterArn,
    Vpc = Vpc.FromLookup(stack, "mainVpc", new VpcLookupOptions
    {
        VpcId = "vpc-0b6cfd6872c50c7b8",
    }),
    ClusterName = "CoreStack-MainEcsCluster03D3CD1A-JeWB2ioJZQEy",
    SecurityGroups = new ISecurityGroup[]
    {
        SecurityGroup.FromLookupById(stack, "mainSecurityGroup", "sg-019630ca5c46b7cf9"),
    },
});

var ecr = Repository.FromRepositoryName(stack, "dockerImageRepo", "pika");

var _ = new ApplicationLoadBalancedEc2Service(stack, "service", new ApplicationLoadBalancedEc2ServiceProps
{
    Cluster = mainCluster,
    ServiceName = "pika",
    MemoryLimitMiB = 256,
    TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
    {
        Image = ContainerImage.FromEcrRepository(ecr, "latest"),
    },
});

app.Synth();
