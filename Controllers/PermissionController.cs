using Authenticate.Context;
using Authorize.Common;
using Authorize.DTOs;
using Authorize.Entities;
using Authorize.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionController(AuthorizeContextDb db, IValidator<AddPermissionDto> addValidator, IValidator<EditPermissionDto> editValidator) : ControllerBase
    {
        private AuthorizeContextDb _db { get; init; } = db;
        private IValidator<AddPermissionDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditPermissionDto> _editValidator { get; init; } = editValidator;

        public IActionResult Get()
        {
            Result result;
            try
            {
                var founded = _db.Permissions.ToList();
                result = CustomResults.GetRecordsOk(founded);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.GetRecordsFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        public IActionResult Get(Guid id)
        {
            Result result;
            try
            {
                var founded = _db.Permissions.FirstOrDefault(i => i.Id == id);
                if (founded is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    result = CustomResults.GetRecordOk(founded);
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.GetRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPost]
        public IActionResult Add(AddPermissionDto dto)
        {
            Result result;
            try
            {
                var check = _addValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Permission item = dto.Adapt<Permission>();
                _db.Permissions.Add(item);
                _db.SaveChanges();
                
                result = CustomResults.AddRecordOk(item.Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.AddRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPut]
        public IActionResult Edit(EditPermissionDto dto)
        {
            Result result;
            try
            {
                var check = _editValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Permission item = dto.Adapt<Permission>();
                var founded = _db.Permissions.FirstOrDefault(i => i.Id == item.Id);

                if (founded is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    _db.Permissions.Update(item);
                    _db.SaveChanges();
                    result = CustomResults.EditRecordOk();
                }

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.EditRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }


        public IActionResult Delete(Guid id)
        {
            Result result;
            try
            {
                var founded = _db.Permissions.FirstOrDefaultAsync(i => i.Id == id);
                if (founded is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    result = CustomResults.DeleteRecordOk();
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                result = CustomErrors.DeleteRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
