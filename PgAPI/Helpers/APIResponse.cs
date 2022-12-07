using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PgAPI.Domain;

namespace PgAPI.Helpers
{
    public class APIResponse : JsonResult
    {
        public APIResponse(object value, HttpStatusCode statusCode) : base(value)
        {
            StatusCode = (int)statusCode;
        }
        public static APIResponse Create(object value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new APIResponse(value, statusCode);
        }
        public static APIResponse CreateFromError(ValidationMessage error, HttpStatusCode statusCode)
        {
            return Create(error, statusCode);
        }
    }
}