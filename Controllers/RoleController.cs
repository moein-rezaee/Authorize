using Authorize.Context;
using Authorize.Common;
using Authorize.DTOs;
using Authorize.Entities;
using Authorize.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authorize.Interfaces;

namespace MyApp.Namespace
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController(
        IUnitOfWorkRepository db,
        ILogger<RoleController> logger,
        IValidator<AddRoleDto> addValidator,
        IValidator<EditRoleDto> editValidator) : ControllerBase
    {
        private IUnitOfWorkRepository _db { get; init; } = db;
        private ILogger<RoleController> _logger { get; init; } = logger;
        private IValidator<AddRoleDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditRoleDto> _editValidator { get; init; } = editValidator;

        [HttpGet]
        public IActionResult Get()
        {
            Result result;
            try
            {
                var found = _db.Roles.ToList();
                result = CustomResults.GetRecordsOk(found);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message, ex);
                result = CustomErrors.GetRecordsFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Result result;
            try
            {
                var found = _db.Roles.Find(id);
                if (found is null)
                {
                    result = CustomErrors.RecordNotFound();
                }
                else
                {
                    result = CustomResults.GetRecordOk(found);
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message, ex);
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
                    return StatusCode(result.StatusCode, result);
                }

                Role item = dto.Adapt<Role>();
                _db.Roles.Add(item);
                _db.Save();

                result = CustomResults.AddRecordOk(item.Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message, ex);
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
                    return StatusCode(result.StatusCode, result);
                }

                bool isOk = _db.Roles.Edit(dto);
                _db.Save();
                if (isOk)
                {
                    result = CustomResults.EditRecordOk();
                }
                else
                {
                    result = CustomErrors.RecordNotFound();
                }

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message, ex);
                result = CustomErrors.EditRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Result result;
            try
            {
                bool isOk = _db.Roles.Delete(id);
                if (isOk)
                {
                    result = CustomResults.DeleteRecordOk();
                }
                else
                {
                    result = CustomErrors.RecordNotFound();
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message, ex);
                result = CustomErrors.DeleteRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
