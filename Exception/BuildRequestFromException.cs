using Korbitec.Licensing.Application.Exceptions;
using Korbitec.Licensing.Common;
using Microsoft.AspNetCore.Mvc;

namespace Korbitec.Licensing.FunctionApplication.Exception
{
    public static class ExceptionCommon
    {
        public static ObjectResult Build( this System.Exception e)
        {
            return e switch
            {
                MissingActivationKeyException make => 
                    new BadRequestObjectResult(
                        new BaseMessageResponse
                        {
                            HasError = true, 
                            ErrorCode = make.Code,
                            ErrorMessage = make.Message
                        }),

                ActivationKeyNotFoundException aknf => 
                    new NotFoundResult(
                        new BaseMessageResponse
                        {
                            HasError = true, 
                            ErrorCode = aknf.Code, 
                            ErrorMessage = aknf.Message
                        }),

                MissingServerIdException msie =>
                    new BadRequestObjectResult(
                        new BaseMessageResponse
                        {
                            HasError = true,
                            ErrorCode = msie.Code,
                            ErrorMessage = msie.Message
                        }),

                ServerIdNotFoundException sinf =>
                    new NotFoundResult(
                        new BaseMessageResponse
                        {
                            HasError = true,
                            ErrorCode = sinf.Code,
                            ErrorMessage = sinf.Message
                        }),

                FirmNotFoundException sinf =>
                    new NotFoundResult(
                        new BaseMessageResponse
                        {
                            HasError = true,
                            ErrorCode = sinf.Code,
                            ErrorMessage = sinf.Message
                        }),

                _ => new InternalServerErrorObjectResult(
                    new BaseMessageResponse
                {
                    HasError = true, 
                    ErrorMessage = e.Message
                })
            };
        }

        public class BadRequestObjectResult : ObjectResult
        {
            public BadRequestObjectResult(BaseMessageResponse value) 
                : base(value)
            {
                StatusCode = 400;
            }
        }

        public class NotFoundResult : ObjectResult
        {
            public NotFoundResult(BaseMessageResponse value)
                : base(value)
            {
                StatusCode = 404;
            }
        }

        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(BaseMessageResponse value)
                : base(value)
            {
                StatusCode = 500;
            }
        }
    }
}
