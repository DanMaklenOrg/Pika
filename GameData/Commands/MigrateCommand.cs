using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Cocona;
using Pika.DataLayer;
using Pika.DataLayer.Dao;

namespace Pika.GameData.Commands;

public static class MigrateCommand
{
    public static void AddMigrateCommand(this CoconaApp app)
    {
        app.AddCommand("migrate", Migrate);
    }

    private static async Task Migrate(IAmazonDynamoDB db, IGameDao dao)
    {
        await MigrateGame(dao);
        await MigrateGamesRaw(db);
        await MigrateUserStatsRaw(db);
    }

    private static async Task MigrateGame(IGameDao dao)
    {
        foreach (var game in await dao.GetAll())
        {
            // Migrate

            await dao.Create(game);
        }
    }

    private static async Task MigrateGamesRaw(IAmazonDynamoDB db)
    {
        var scanResponse = await db.ScanAsync(new ScanRequest
        {
            TableName = DynamoDbConstants.TableName,
            FilterExpression = "begins_with(pk, :prefix)",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {":prefix", new AttributeValue { S = "Game#" }}
            }
        });

        foreach (var item in scanResponse.Items)
        {
            // Migrate

            await db.PutItemAsync(new PutItemRequest
            {
                TableName = DynamoDbConstants.TableName,
                Item = item,
            });
        }
    }

    private static async Task MigrateUserStatsRaw(IAmazonDynamoDB db)
    {
        var scanResponse = await db.ScanAsync(new ScanRequest
        {
            TableName = DynamoDbConstants.TableName,
            FilterExpression = "begins_with(pk, :prefix)",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {":prefix", new AttributeValue { S = "UserStat#" }}
            }
        });

        foreach (var item in scanResponse.Items)
        {
            // Migrate

            await db.PutItemAsync(new PutItemRequest
            {
                TableName = DynamoDbConstants.TableName,
                Item = item,
            });
        }
    }
}
