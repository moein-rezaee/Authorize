using Authorize.Models;

namespace Authorize.Common
{
    public class CustomErrors
    {
        public static Result RecordNotFaound(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "رکورد درخواستی یافت نشد",
                En = "Record Not Found!"
            },
            StatusCode = StatusCodes.Status404NotFound,
            Data = data,
            Status = false
        };

        public static Result GetRecordsFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی رکوردها با مشکل مواجه شد",
                En = "Get Records Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result GetRecordFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی رکورد با مشکل مواجه شد",
                En = "Get Record Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result AddRecordFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات ذخیره رکورد با مشکل مواجه شد",
                En = "Add Record Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result EditRecordFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات ویرایش رکورد با مشکل مواجه شد",
                En = "Edit Record Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };

        public static Result DeleteRecordFailed() => new()
        {
            Message = new()
            {
                Fa = "عملیات حذف رکورد با مشکل مواجه شد",
                En = "Delete Record Failed!"
            },
            StatusCode = StatusCodes.Status500InternalServerError,
            Status = false
        };


        public static Result InvalidData(object data) => new()
        {
            Message = new()
            {
                Fa = "داده ورودی نامعتبر می باشد",
                En = "Invalid Input Data"
            },
            StatusCode = StatusCodes.Status404NotFound,
            Data = data,
            Status = false
        };

        public static Result Unauthorized(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "درخواست غیرمجاز. شما متاسفانه به این منبع دسترسی ندارید!",
                En = "Unauthorized"
            },
            Data = data,
            Status = false,
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}
