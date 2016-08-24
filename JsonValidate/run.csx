using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    string reqbody = await req.Content.ReadAsStringAsync();
    //log.Info($"{reqbody}");
    
    if (reqbody == null ) {
        return req.CreateResponse(HttpStatusCode.OK, new {
            error = $"Request body was empty"});
    }
    else {
        try {
            JObject jsonbody = JObject.Parse(reqbody);
            //log.Info($"{jsonbody}");
            
            var jsondoc = jsonbody["jsondoc"];
            JSchema schema = JSchema.Parse(jsonbody["schema"].ToString());

            IList<string> messages; 
            bool valid = jsondoc.IsValid(schema, out messages );
            log.Info($"Validation message count : {messages.Count}");
            
            if (valid == true) {
                log.Info($"Is valid {valid}");
                return req.CreateResponse(HttpStatusCode.OK, new {schemaValid = valid});
            }  
            else {
                log.Info($"Is NOT valid {valid}");
                return req.CreateResponse(HttpStatusCode.OK, new {
                    schemaValid = valid,
                    error = $"Error validating json against schema",
                    validationMessages = messages
                });
            }
             
        }
        catch(Exception ex){
            log.Info($"{ex.ToString()}");
                    return req.CreateResponse(HttpStatusCode.OK, new {
                        error = $"Error parsing {ex.Message}"});
        }
    }
}