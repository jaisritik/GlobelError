//using GlobelError.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Newtonsoft.Json;
//using System.Net;

//namespace GlobelError.Filters
//{
//    public class ExceptionFilter : IExceptionFilter
//    {
//        public void OnException(ExceptionContext context)
//        {
//            var error = new ErrorResponse
//            {
//                Status = (int)HttpStatusCode.InternalServerError,
//                Message = context.Exception.Message
//            };

//            context.Result=new JsonResult(error)
//            { StatusCode = 500};   
//        }
//    }
//}
