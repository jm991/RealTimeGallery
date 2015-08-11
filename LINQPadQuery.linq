<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
</Query>

using (var client = new HttpClient())
using (var content = new MultipartFormDataContent())
{
	var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(fileName));
	fileContent.Headers.ContentDisposition = new ContentDisposition()
	{
		Filename = Guid.NewGuid().ToString() + ".png"
	};
	content.Add(fileContent);
	
	var requestUri = "http://192.168.1.103/api/photo";
	var result = await client.PutAsync(requestUri, content);
	
	Console.WriteLine(result.ToString());
}