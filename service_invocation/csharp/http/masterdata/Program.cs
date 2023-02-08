using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var baseURL = (Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost") + ":" + (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500");

var client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
// Adding app id as part of the header
client.DefaultRequestHeaders.Add("dapr-app-id", "dispatch");

for (int i = 1; i <= 20; i++) {
    var id = "G0000" + i.ToString();
    var order = new Document(id);
    var orderJson = JsonSerializer.Serialize<Document>(order);
    var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

    // Invoking a service
    var response = await client.PostAsync($"{baseURL}/orders", content);
    Console.WriteLine("Master data succesful for: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Document([property: JsonPropertyName("documentId")] string documentId);
