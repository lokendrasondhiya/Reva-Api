using Microsoft.Extensions.Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultURL");

var client = new SecretClient(new Uri(keyVaultUrl.Value!.ToString()), new DefaultAzureCredential());
KeyVaultSecret secret = client.GetSecret("StorageConnection");
string connectionstring = secret.Value;

builder.Services.AddAzureClients(azureclient =>
{
    azureclient.AddBlobServiceClient(connectionstring);
    azureclient.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
