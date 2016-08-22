#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, out string outputBlob)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    string jsondoc = await req.Content.ReadAsStringAsync();
    
    if (jsondoc == null ) {
        return req.CreateResponse(HttpStatusCode.BadRequest, new {
            error = "Please post JsonDoc"
        });
    }
    else {
        try {
			//output the document to storage
            outputBlob = jsondoc;
            
            //create xml root
            XmlDocument xDoc = JsonConvert.DeserializeXmlNode(jsondoc, "module");
            log.Info($"{xDoc.OuterXml}");
            
            return req.CreateResponse(HttpStatusCode.OK, xDoc.OuterXml);
             
        }
        catch(Exception ex){
            log.Info($"{ex.ToString()}");
            return req.CreateResponse(HttpStatusCode.BadRequest, new {
				error = $"Error parsing jsondocument {ex.Message}"
            });
        }
    }
}