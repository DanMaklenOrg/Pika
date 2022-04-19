﻿using Amazon.CDK;
using Amazon.CDK.AWS.CertificateManager;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECR;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.IAM;
using Environment = Amazon.CDK.Environment;

string clusterArn = "arn:aws:ecs:eu-west-1:464787150360:cluster/CoreStack-MainEcsCluster03D3CD1A-JeWB2ioJZQEy";
string certificateArn = "arn:aws:acm:eu-west-1:464787150360:certificate/76e11e53-a292-4ff3-9857-d374d19ca507";

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
    SecurityGroups = new[]
    {
        SecurityGroup.FromLookupById(stack, "mainSecurityGroup", "sg-019630ca5c46b7cf9"),
    },
});

var ecr = Repository.FromRepositoryName(stack, "dockerImageRepo", "pika");

var _ = new ApplicationLoadBalancedEc2Service(stack, "service", new ApplicationLoadBalancedEc2ServiceProps
{
    Cluster = mainCluster,
    ServiceName = "pika",
    SslPolicy = SslPolicy.RECOMMENDED,
    ListenerPort = 55501,
    Certificate = Certificate.FromCertificateArn(stack, "certificate", certificateArn),
    MemoryLimitMiB = 256,
    TaskImageOptions = new ApplicationLoadBalancedTaskImageOptions
    {
        Image = ContainerImage.FromEcrRepository(ecr, "latest"),
        TaskRole = Role.FromRoleArn(stack, "role", "arn:aws:iam::464787150360:role/MainClusterServiceRole"),
        Environment = new Dictionary<string, string> {{"AWS_REGION", stack.Region}},

    },
    CircuitBreaker = new DeploymentCircuitBreaker
    {
        Rollback = true,
    },
});

app.Synth();
