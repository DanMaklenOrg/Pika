using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Pika.Service;

public static class AuthExtensions
{
    public static void AddAnaAuth(this WebApplicationBuilder builder)
    {
        byte[] authKey = Convert.FromBase64String(builder.Configuration["AuthKeyBase64"]);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(authKey),
            });

        builder.Services.AddAuthorization(options =>
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build()
        );
    }
}
