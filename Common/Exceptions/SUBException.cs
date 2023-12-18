using System;
using System.Net;
using Common.Enums;

namespace Common.Exceptions
{
    [Serializable]
    public abstract class SUBException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ErrorCode ErrorCode { get; set; }

        protected SUBException(HttpStatusCode statusCode, ErrorCode errorCode, String message) : base(message)
        {
            this.StatusCode = statusCode;
            this.ErrorCode = errorCode;
        }
    }

    [Serializable]
    public class UnauthorizedSUBException : SUBException
    {
        public UnauthorizedSUBException(ErrorCode errorCode, String message)
            : base(HttpStatusCode.Unauthorized, errorCode, message)
        {
            this.ErrorCode = errorCode;
        }
    }

    [Serializable]
    public class BadRequestSUBException : SUBException
    {
        public BadRequestSUBException(ErrorCode errorCode, String message)
            : base(HttpStatusCode.BadRequest, errorCode, message)
        {
            this.ErrorCode = errorCode;
        }
    }

    [Serializable]
    public class NotFoundSUBException : SUBException
    {
        public NotFoundSUBException(ErrorCode errorCode, String message)
            : base(HttpStatusCode.NotFound, errorCode, message)
        {
            this.ErrorCode = errorCode;
        }
    }

    [Serializable]
    public class InternalErrorSUBException : SUBException
    {
        public InternalErrorSUBException(ErrorCode errorCode, String message)
            : base(HttpStatusCode.InternalServerError, errorCode, message)
        {
            this.ErrorCode = errorCode;
        }
    }


}