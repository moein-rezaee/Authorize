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
    public class RoleController(AuthorizeContextDb db, IValidator<AddRoleDto> addValidator, IValidator<EditRoleDto> editValidator) : ControllerBase
    {
        private AuthorizeContextDb _db { get; init; } = db;
        private IValidator<AddRoleDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditRoleDto> _editValidator { get; init; } = editValidator;

        public IActionResult Get()
        {
            Result result;
            try
            {
                var founded = _db.Roles.ToList();
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
                var founded = _db.Roles.FirstOrDefault(i => i.Id == id);
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
        public IActionResult Add(AddRoleDto dto)
        {
            Result result;
            try
            {
                var check = _addValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Role item = dto.Adapt<Role>();
                _db.Roles.Add(item);
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
        public IActionResult Edit(EditRoleDto dto)
        {
            Result result;
            try
            {
                var check = _editValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Role item = dto.Adapt<Role>();
                var founded = _db.Roles.FirstOrDefault(i => i.Id == item.Id);

                if (founded is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    _db.Roles.Update(item);
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
                var founded = _db.Roles.FirstOrDefaultAsync(i => i.Id == id);
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
