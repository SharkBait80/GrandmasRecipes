using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.IdentityModel.Tokens;
using Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RecipesAPI.Authorizer
{
    public class Function
    {

        public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest apigAuthRequest, ILambdaContext context)
        {

            var TokenValidationParameters = new TokenValidationParameters
            {
                // TODO: Contruct the token validation parameters
               
            };

            SecurityToken validatedToken;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            bool authorized = false;

            if (!string.IsNullOrWhiteSpace(apigAuthRequest.AuthorizationToken))
            {
                try
                {
                    var user = handler.ValidateToken(apigAuthRequest.AuthorizationToken, TokenValidationParameters, out validatedToken);
                    var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (claim != null)
                        authorized = claim.Value == SecurityConstants.ClaimName; // Ensure that the claim value matches the assertion
                }
                catch (Exception ex)
                {
                    LambdaLogger.Log($"Error occurred validating token: {ex.Message}");
                }
            }
            APIGatewayCustomAuthorizerPolicy policy = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>()
            };

            policy.Statement.Add(new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement
            {
                // TODO: Construct the IAM Policy statatement
              
            });

           
            APIGatewayCustomAuthorizerContextOutput contextOutput = new APIGatewayCustomAuthorizerContextOutput();
            contextOutput["User"] = authorized ? SecurityConstants.ClaimName : "User";
            contextOutput["Path"] = apigAuthRequest.MethodArn;

            return new APIGatewayCustomAuthorizerResponse
            {
                PrincipalID = authorized ? SecurityConstants.ClaimName : "User",
                Context = contextOutput,
                PolicyDocument = policy
            };

        }
    }
}
