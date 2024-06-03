// See https://aka.ms/new-console-template for more information
using ExceptionAddData;
using System.Collections;

var externalRequest = new Request { Id = 1235 };

try
{
    throw new NullReferenceException();
}
catch (Exception ex)
{
    ex.Data.Add("Request body", externalRequest.Json());
    //Logger.Error(ex);

    Console.WriteLine($"ex.Data: {ex.Data.Values}");
    throw;
}


