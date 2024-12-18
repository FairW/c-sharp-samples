var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/webhook", async context =>
{  
    var apiKey = context.Request.Headers["Authorization"];
    if (apiKey != "jarlem-api-key")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return;
    }

    var requestBody = await context.Request.ReadFromJsonAsync<WebhookPayload>();
     
    // Process the received webhook data
    Console.WriteLine($"Header: {requestBody?.Header}, Body: {requestBody?.Body}");
 
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Webhook acknowledged");
});

app.Run();
