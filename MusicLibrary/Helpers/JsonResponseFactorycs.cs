using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MusicLibrary.Helpers
{
    public class JsonResponseFactory : IJsonResponseFactory
    {
        public object CreateErrorResponse(string error)
        {
            return new { Success = false, ErrorMessages = new List<string> { error } };
        }

        public object CreateErrorResponse(List<string> errors)
        {
            return new { Success = false, ErrorMessages = errors };
        }

        public object CreateErrorResponse(ModelStateDictionary modelState)
        {
            return new { Success = false, ErrorMessages = modelState.Errors() };
        }

        public object CreateErrorResponse(object referenceObject, ModelStateDictionary modelState)
        {
            return new { Success = false, Object = referenceObject, ErrorMessages = modelState.Errors() };
        }

        public object CreateSuccessResponse()
        {
            return new { Success = true };
        }

        public object CreateSuccessResponse(object referenceObject)
        {
            return new { Success = true, Object = referenceObject };
        }

        public object CreateSuccessResponse(string successMessage)
        {
            return new { Success = true, SuccessMessage = successMessage };
        }

        public object CreateSuccessResponse(object referenceObject, string successMessage)
        {
            return new { Success = true, Object = referenceObject, SuccessMessage = successMessage };
        }

        public object CreatePartialSuccessResponse(string partialSuccessMessage)
        {
            return new { PartialSuccess = true, PartialSuccessMessage = partialSuccessMessage };
        }

        public object CreateCloseResponse()
        {
            return new { Close = true };
        }

        public string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data,
                 Formatting.None,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 });
        }
    }
}