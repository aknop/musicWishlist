using System.Collections.Generic;
using System.Web.Mvc;

namespace MusicLibrary.Helpers
{
    public interface IJsonResponseFactory
    {
        object CreateErrorResponse(string error);
        object CreateErrorResponse(List<string> errors);
        object CreateErrorResponse(ModelStateDictionary modelState);
        object CreateErrorResponse(object referenceObject, ModelStateDictionary modelState);
        object CreateSuccessResponse();
        object CreateSuccessResponse(object referenceObject);
        object CreateSuccessResponse(string successMessage);
        object CreateSuccessResponse(object referenceObject, string successMessage);
        object CreatePartialSuccessResponse(string partialSuccessMessage);
        object CreateCloseResponse();
        string SerializeObject(object data);
    }
}
