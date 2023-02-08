var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapPost("/orders", (Document document) =>
{
    Console.WriteLine("To be dispatched : " + document);
    return document.ToString();
});

await app.RunAsync();

public record Document(string documentId);
