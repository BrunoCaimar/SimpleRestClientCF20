SimpleRestClientCF20
====================

Very Simple C# Rest Client for .NET Compact Framework 2.0 (VS 2005)

* HTTP requests made by System.Net.WebRequest
* JSON serialization/deserialization made by JsonNETCF ( a port of Newtonsoft.JsonNet for .NET CF 2.0 )


Simple Example Usage:
---------------------

```
string url = "https://api.github.com/";
string resource = "repos";
string resourceId = "brunocaimar/simplerestclientcf20";

// GET - HTTP GET to https://api.github.com/repos/brunocaimar/simplerestclientcf20
RestClient restClient = new RestClient(url, resource);
GitRepository gitRepo = restClient.Get<GitRepository>(resourceId);

// POST - HTTP Post to https://api.github.com/repos/
restClient.Post(gitRepo);
```

