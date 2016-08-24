# AzFunctions
Generic Function Apps for Azure, that work rather well when called by Azure Logic apps.

- JsonToDocDb.  A node function that acts as an HTTP webhook.  When a JSON document is received it is given directly to DocDb
- JsonToXml. A C# webhook that converts a Json body and returns xml in the respone body.
- JsonToXml+. Same as JsonToXml, but in addition persists the Xml as a file to Blob storage.
- JsonValidate.  A C# webhook that uses the Newtonsoft Json Schema library to validate a Json document against it's schema, returning any validating messages to the user in the respone body.

