using Newtonsoft.Json;
using OrarDude;
using System.Diagnostics;

string url = "https://www.cs.ubbcluj.ro/files/orar/2023-2/tabelar/IE3.html";

static string GetHtml(string url)
{
    using var httpClient = new HttpClient();

    var response = httpClient.GetAsync(url).Result;
    Trace.Assert(response.IsSuccessStatusCode, "Failed to download HTML. Status code: " + response.StatusCode);

    var str = response.Content.ReadAsStringAsync().Result;
    return str;
}

// Get the html
//var html = GetHtml(url);
//File.WriteAllText("x.html", html);
var html = File.ReadAllText("x.html");

var page = Parser.ParsePage(html);
//var pageJson = JsonConvert.SerializeObject(page);
//File.WriteAllText("x.txt", pageJson);

var oPage = Interpreter.ProcessData(page);

var oHtml = Builder.BuildHtml(oPage);
File.WriteAllText("y.html", oHtml);
