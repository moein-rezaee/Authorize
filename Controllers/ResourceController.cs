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
    public class ResourceController(AuthorizeContextDb db, IValidator<AddResourceDto> addValidator, IValidator<EditResourceDto> editValidator) : ControllerBase
    {
        private AuthorizeContextDb _db { get; init; } = db;
        private IValidator<AddResourceDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditResourceDto> _editValidator { get; init; } = editValidator;

        [HttpGet]
        public IActionResult Get()
        {
            Result result;
            try
            {
                var founded = _db.Resources.ToList();
                result = CustomResults.GetRecordsOk(founded);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
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
                var founded = _db.Resources.FirstOrDefault(i => i.Id == id);
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
        public IActionResult Add(AddResourceDto dto)
        {
            Result result;
            try
            {
                var check = _addValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Resource item = dto.Adapt<Resource>();
                _db.Resources.Add(item);
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
        public IActionResult Edit(EditResourceDto dto)
        {
            Result result;
            try
            {
                var check = _editValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                }

                Resource item = dto.Adapt<Resource>();
                var founded = _db.Resources.FirstOrDefault(i => i.Id == item.Id);

                if (founded is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    _db.Resources.Update(item);
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


        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Result result;
            try
            {
                var founded = _db.Resources.FirstOrDefaultAsync(i => i.Id == id);
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
