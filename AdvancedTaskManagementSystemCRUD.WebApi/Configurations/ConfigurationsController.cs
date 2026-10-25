using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Configurations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConfigurationsController(
    IConfigurationService configurationService, 
    IMapper mapper,
    IValidator<GroupDTO> groupValidator,
    IValidator<PriorityDTO> priorityValidator,
    IValidator<StatusDTO> statusValidator,
    IValidator<TypeDTO> typeValidator,
    IValidator<LabelDTO> labelValidator,
    IValidator<UpdateGroupRequest> updateGroupValidator
    ) : ControllerBase
{
    [HttpGet("GetGroups")]
    public IActionResult GetGroups()
    {
        var groups = configurationService.GetGroups();
        return Ok(groups);
    }

    [HttpGet("GetPriorities")]
    public IActionResult GetPriorities()
    {
        var priorities = configurationService.GetPriorities();
        return Ok(priorities);
    }
    [HttpGet("GetTypes")]
    public IActionResult GetTypes()
    {
        var types = configurationService.GetTypes();
        return Ok(types);
    }

    [HttpGet("GetStatusByTypeId/{typeId}")]
    public IActionResult GetStatusByTypeId([FromRoute] int typeId)
    {
        var status = configurationService.GetStatusByTypeId(typeId);
        return Ok(status);
    }

    [HttpGet("GetLabels")]
    public IActionResult GetLabels()
    {
        var labels = configurationService.GetLabels();
        return Ok(labels);
    }

    [HttpGet("GetProjectStatuses")]
    public IActionResult GetProjectStatuses()
    {
        var status = configurationService.GetProjectStatuses();
        return Ok(status);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddGroup")]
    public async Task<IActionResult> AddGroup([FromBody] AddGroupRequest request)
    {
        var group = mapper.Map<GroupDTO>(request);

        var result = await groupValidator.ValidateAsync(group);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(await configurationService.AddGroup(group));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddPriority")]
    public async Task<IActionResult> AddPriority([FromBody] AddPriorityRequest request)
    {
        var priority = mapper.Map<PriorityDTO>(request);

        var result = await priorityValidator.ValidateAsync(priority);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(await configurationService.AddPriority(priority));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddStatus")]
    public async Task<IActionResult> AddStatus([FromBody] AddStatusRequest request)
    {
        var status = mapper.Map<StatusDTO>(request);

        var result = await statusValidator.ValidateAsync(status);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(await configurationService.AddStatus(status));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddProjectStatus")]
    public async Task<IActionResult> AddProjectStatus([FromBody] AddProjectStatusRequest request)
    {
        var status = mapper.Map<ProjectStatusDTO>(request);

        return Ok(await configurationService.AddProjectStatus(status));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddType")]
    public async Task<IActionResult> AddType([FromBody] AddTypeRequest request)
    {
        var type = mapper.Map<TypeDTO>(request);

        var result = await typeValidator.ValidateAsync(type);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        return Ok(await configurationService.AddType(type));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddLabel")]
    public async Task<IActionResult> AddLabel([FromBody] AddLabelRequest request)
    {
        var label = mapper.Map<LabelDTO>(request);

        var result = await labelValidator.ValidateAsync(label);
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var labelId = await configurationService.AddLabel(label);
        return labelId == 0 ? BadRequest("Etykieta o podanej nazwie już istnieje.") : Ok(labelId);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateGroup")]
    public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest request)
    {
        var resultValidation = await updateGroupValidator.ValidateAsync(request);
        if (!resultValidation.IsValid)
        {
            return BadRequest(resultValidation.Errors);
        }

        var result = await configurationService.UpdateGroup(request.OldName, request.NewName);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdatePriority")]
    public async Task<IActionResult> UpdatePriority([FromBody] UpdatePriorityRequest request)
    {
        var priority = mapper.Map<PriorityDTO>(request);

        var resultValidation = await priorityValidator.ValidateAsync(priority);
        if (!resultValidation.IsValid)
        {
            return BadRequest(resultValidation.Errors);
        }

        var result = await configurationService.UpdatePriority(priority);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        var status = mapper.Map<StatusDTO>(request);

        var resultValidation = await statusValidator.ValidateAsync(status);
        if (!resultValidation.IsValid)
        {
            return BadRequest(resultValidation.Errors);
        }

        var result = await configurationService.UpdateStatus(status);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateProjectStatus")]
    public async Task<IActionResult> UpdateProjectStatus([FromBody] UpdateProjectStatusRequest request)
    {
        var status = mapper.Map<ProjectStatusDTO>(request);

        var result = await configurationService.UpdateProjectStatus(status);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateType")]
    public async Task<IActionResult> UpdateType([FromBody] UpdateTypeRequest request)
    {
        var type = mapper.Map<TypeDTO>(request);

        var resultValidation = await typeValidator.ValidateAsync(type);
        if (!resultValidation.IsValid)
        {
            return BadRequest(resultValidation.Errors);
        }

        var result = await configurationService.UpdateType(type);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateLabel")]
    public async Task<IActionResult> UpdateLabel([FromBody] UpdateLabelRequest request)
    {
        var label = mapper.Map<LabelDTO>(request);

        var resultValidation = await labelValidator.ValidateAsync(label);
        if (!resultValidation.IsValid)
        {
            return BadRequest(resultValidation.Errors);
        }

        var result = await configurationService.UpdateLabel(label);
        if (result is null)
            return NotFound();
        return result == 0 ? BadRequest("Etykieta o podanej nazwie już istnieje.") : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteGroup/{name}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] string name)
    {
        var result = await configurationService.DeleteGroup(name);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeletePriority/{id}")]
    public async Task<IActionResult> DeletePriority([FromRoute] int id)
    {
        var result = await configurationService.DeletePriority(id);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteStatus/{id}")]
    public async Task<IActionResult> DeleteStatus([FromRoute] int id)
    {
        var result = await configurationService.DeleteStatus(id);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteProjectStatus/{id}")]
    public async Task<IActionResult> DeleteProjectStatus([FromRoute] int id)
    {
        var result = await configurationService.DeleteProjectStatus(id);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteType/{id}")]
    public async Task<IActionResult> DeleteType([FromRoute] int id)
    {
        var result = await configurationService.DeleteType(id);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteLabel/{id}")]
    public async Task<IActionResult> DeleteLabel([FromRoute] int id)
    {
        var result = await configurationService.DeleteLabel(id);
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }
}
