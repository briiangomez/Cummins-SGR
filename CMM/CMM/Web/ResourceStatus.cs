namespace CMM.Web
{
    using System;

    public enum ResourceStatus
    {
        BadRequest = 400,
        Empty = 0xcc,
        InternalServerError = 500,
        MovedPermanently = 0x12d,
        NotAcceptable = 0x196,
        NotFound = 0x194,
        NotModified = 0x130,
        OK = 200,
        SeeOther = 0x12f,
        ServiceUnavailable = 0x1f7
    }
}

