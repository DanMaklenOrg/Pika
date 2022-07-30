# Pika

### Development Environment Setup
1. Make sure you have a postgres sql service up and running locally
   - Run database migrations `dotnet ef database update -p DataLayer -- <host> <port> <username> <password>`
2. (optional) Set development config
   - Create new file `Service/dev.config.json`
   - Populate/Modify any config fields you might want to change
     - `Service/dev.config.template.json` contains commonly used dev.config fields
3. Set your AWS credentials in `Service/appsettings.Development.json`
   - Create new file `Service/appsettings.Development.json`
   - Set AWS credentials/profile in the new file.
   - `Service/appsettings.Development.template.json` can serve as a guide to how the file should look like.
> **Remember:** Don't commit development config or aws credentials
