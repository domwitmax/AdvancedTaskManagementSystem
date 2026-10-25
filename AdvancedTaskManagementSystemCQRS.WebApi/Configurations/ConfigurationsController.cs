using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddGroup;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddLabel;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddPriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddProjectStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteProjectStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetProjectStatuses;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateProjectStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.AddType;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteGroup;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteLabel;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeletePriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteType;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetGroups;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetPriorities;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetStatusByTypeId;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetTypes;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetLabels;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateGroup;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateLabel;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdatePriority;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateType;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Configurations;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConfigurationsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet("GetGroups")]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await mediator.Send(new GetGroupsQuery());
        return Ok(groups);
    }

    [HttpGet("GetPriorities")]
    public async Task<IActionResult> GetPriorities()
    {
        var priorities = await mediator.Send(new GetPrioritiesQuery());
        return Ok(priorities);
    }
    [HttpGet("GetTypes")]
    public async Task<IActionResult> GetTypes()
    {
        var types = await mediator.Send(new GetTypesQuery());
        return Ok(types);
    }

    [HttpGet("GetLabels")]
    public async Task<IActionResult> GetLabels()
    {
        var labels = await mediator.Send(new GetLabelsQuery());
        return Ok(labels);
    }

    [HttpGet("GetStatusByTypeId/{typeId}")]
    public async Task<IActionResult> GetStatusByTypeId([FromRoute] int typeId)
    {
        var status = await mediator.Send(new GetStatusByTypeIdQuery(typeId));
        return Ok(status);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddGroup")]
    public async Task<IActionResult> AddGroup([FromBody] AddGroupRequest request)
    {
        var command = mapper.Map<AddGroupCommand>(request);
        return Ok(await mediator.Send(command));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddPriority")]
    public async Task<IActionResult> AddPriority([FromBody] AddPriorityRequest request)
    {
        var command = mapper.Map<AddPriorityCommand>(request);
        return Ok(await mediator.Send(command));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddStatus")]
    public async Task<IActionResult> AddStatus([FromBody] AddStatusRequest request)
    {
        var command = mapper.Map<AddStatusCommand>(request);
        return Ok(await mediator.Send(command));
    }

    [HttpGet("GetProjectStatuses")]
    public async Task<IActionResult> GetProjectStatuses()
    {
        var statuses = await mediator.Send(new GetProjectStatusesQuery());
        return Ok(statuses);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddProjectStatus")]
    public async Task<IActionResult> AddProjectStatus([FromBody] AddProjectStatusRequest request)
    {
        var command = mapper.Map<AddProjectStatusCommand>(request);
        return Ok(await mediator.Send(command));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddType")]
    public async Task<IActionResult> AddType([FromBody] AddTypeRequest request)
    {
        var command = mapper.Map<AddTypeCommand>(request);
        return Ok(await mediator.Send(command));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPost("AddLabel")]
    public async Task<IActionResult> AddLabel([FromBody] AddLabelRequest request)
    {
        var command = mapper.Map<AddLabelCommand>(request);
        var result = await mediator.Send(command);
        return result == 0 ? BadRequest("Etykieta o podanej nazwie już istnieje.") : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateGroup")]
    public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdateGroupCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdatePriority")]
    public async Task<IActionResult> UpdatePriority([FromBody] UpdatePriorityRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdatePriorityCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdateStatusCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateProjectStatus")]
    public async Task<IActionResult> UpdateProjectStatus([FromBody] UpdateProjectStatusRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdateProjectStatusCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateType")]
    public async Task<IActionResult> UpdateType([FromBody] UpdateTypeRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdateTypeCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("UpdateLabel")]
    public async Task<IActionResult> UpdateLabel([FromBody] UpdateLabelRequest request)
    {
        var result = await mediator.Send(mapper.Map<UpdateLabelCommand>(request));
        if (result is null)
            return NotFound();
        return result == 0 ? BadRequest("Etykieta o podanej nazwie już istnieje.") : Ok(result);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteGroup/{name}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] string name)
    {
        var result = await mediator.Send(new DeleteGroupCommand(name));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeletePriority/{id}")]
    public async Task<IActionResult> DeletePriority([FromRoute] int id)
    {
        var result = await mediator.Send(new DeletePriorityCommand(id));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteStatus/{id}")]
    public async Task<IActionResult> DeleteStatus([FromRoute] int id)
    {
        var result = await mediator.Send(new DeleteStatusCommand(id));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteProjectStatus/{id}")]
    public async Task<IActionResult> DeleteProjectStatus([FromRoute] int id)
    {
        var result = await mediator.Send(new DeleteProjectStatusCommand(id));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteType/{id}")]
    public async Task<IActionResult> DeleteType([FromRoute] int id)
    {
        var result = await mediator.Send(new DeleteTypeCommand(id));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpDelete("DeleteLabel/{id}")]
    public async Task<IActionResult> DeleteLabel([FromRoute] int id)
    {
        var result = await mediator.Send(new DeleteLabelCommand(id));
        return result ? Ok() : BadRequest("Nie można usunąć elementu konfiguracji, ponieważ nie istnieje albo jest używany.");
    }
}
