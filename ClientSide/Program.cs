var client = new HttpClient();
var result = await client.GetStringAsync()

var sql = await result.Content.ReadAsStringAsync();
Console.WriteLine();