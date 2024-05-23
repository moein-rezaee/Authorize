using Authorize.Common;
using Authorize.DTOs;
using Authorize.Entities;
using Authorize.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Authorize.Interfaces;

namespace MyApp.Namespace
{
    [Route("[controller]")]
    [ApiController]
    public class ResourceController(
        IUnitOfWorkRepository db,
        ILogger<ResourceController> logger,
        IValidator<AddResourceDto> addValidator,
        IValidator<EditResourceDto> editValidator) : ControllerBase
    {
        private IUnitOfWorkRepository _db { get; init; } = db;
        private ILogger<ResourceController> _logger { get; init; } = logger;
        private IValidator<AddResourceDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditResourceDto> _editValidator { get; init; } = editValidator;

        [HttpGet]
        public IActionResult Get()
        {
            Result result;
            try
            {
                var found = _db.Resources.ToList();
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
                var found = _db.Resources.Find(id);
                if (found is null)
                {
                    result = CustomErrors.RecordNotFaound();
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
        public IActionResult Add(AddResourceDto dto)
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

                Resource item = dto.Adapt<Resource>();
                _db.Resources.Add(item);
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
        public IActionResult Edit(EditResourceDto dto)
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

                bool isOk = _db.Resources.Edit(dto);
                _db.Save();

                if (isOk)
                {
                    result = CustomResults.EditRecordOk();
                }
                else
                {
                    result = CustomErrors.RecordNotFaound();
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


        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Result result;
            try
            {
                bool isOk = _db.Resources.Delete(id);
                _db.Save();
                if (isOk)
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
                _logger.LogDebug(ex.Message, ex);
                result = CustomErrors.DeleteRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

    }
}
