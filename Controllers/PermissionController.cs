using Authorize.Common;
using Authorize.DTOs;
using Authorize.Entities;
using Authorize.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Authorize.Services;
using Authorize.Interfaces;
using NuGet.Protocol;

namespace Authorize.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionController(
        IUnitOfWorkRepository db,
        ILogger<PermissionController> logger,
        IValidator<CheckPermissionDto> validator,
        IValidator<AddPermissionDto> addValidator,
        IValidator<EditPermissionDto> editValidator) : ControllerBase
    {
        private IUnitOfWorkRepository _db { get; init; } = db;
        private ILogger<PermissionController> _logger { get; init; } = logger;
        private IValidator<CheckPermissionDto> _validator { get; init; } = validator;
        private IValidator<AddPermissionDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditPermissionDto> _editValidator { get; init; } = editValidator;
        private PermissionService _service { get; init; } = new(db);

        [HttpGet]
        public IActionResult Get()
        {
            Result result;
            try
            {
                var found = _db.Permissions.ToList();
                result = CustomResults.GetRecordsOk(found);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
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
                var found = _db.Permissions.Find(id);
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
                _logger.LogError(ex.ToJson());
                result = CustomErrors.GetRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPost("Check")]
        public IActionResult Check(CheckPermissionDto dto)
        {
            Result result;
            try
            {
                var check = _validator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                    return StatusCode(result.StatusCode, result);
                }


                bool hasPermission = _service.Check(dto);
                if (hasPermission)
                {
                    result = CustomResults.UserIsValid();
                }
                else
                {
                    result = CustomErrors.Unauthorized();
                }

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.AddRecordFailed();
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
                    return StatusCode(result.StatusCode, result);
                }

                Permission item = dto.Adapt<Permission>();
                _db.Permissions.Add(item);
                _db.Save();

                result = CustomResults.AddRecordOk(item.Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
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
                    return StatusCode(result.StatusCode, result);
                }

                bool isOk = _db.Permissions.Edit(dto);
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
                _logger.LogError(ex.ToJson());
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
                var isOk = _db.Permissions.Delete(id);
                _db.Save();

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
                _logger.LogError(ex.ToJson());
                result = CustomErrors.DeleteRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}

