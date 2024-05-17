using Authorize.Models;

namespace Authorize.Common
{
    public class CustomResults
    {
        public static Result GetRecordOk(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی رکورد با موفقیت انجام شد",
                En = "Get Record Successfully Done!"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };

        public static Result GetRecordsOk(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات واکشی رکوردها با موفقیت انجام شد",
                En = "Get Records Successfully Done!"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };

        public static Result AddRecordOk(object data) => new()
        {
            Message = new()
            {
                Fa = "عملیات افزودن رکورد با موفقیت انجام شد",
                En = "Record Added!"
            },
            StatusCode = StatusCodes.Status201Created,
            Data = data
        };
        
        public static Result EditRecordOk(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "عملیات ویرایش رکورد با موفقیت انجام شد",
                En = "Record Edited!"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };


        public static Result DeleteRecordOk(object? data = null) => new()
        {
            Message = new()
            {
                Fa = "عملیات حذف رکورد با موفقیت انجام شد",
                En = "Record Deleted!"
            },
            StatusCode = StatusCodes.Status200OK,
            Data = data
        };
    }
}