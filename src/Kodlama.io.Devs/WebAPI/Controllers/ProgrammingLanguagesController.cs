using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgrammingLanguagesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProgrammingLanguagesController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
    {
        CreatedProgrammingLanguageDto result = await _mediator.Send(createProgrammingLanguageCommand);

        return Ok(result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProgrammingLanguageCommand deleteProgrammingLanguageCommand)
    {
        DeletedProgrammingLanguageDto result = await _mediator.Send(deleteProgrammingLanguageCommand);

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand)
    {
        UpdatedProgrammingLanguageDto result = await _mediator.Send(updateProgrammingLanguageCommand);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProgrammingLanguageQuery getListProgrammingLanguageQuery = new() { PageRequest = pageRequest };
        ProgrammingLanguageListModel result = await _mediator.Send(getListProgrammingLanguageQuery);

        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetList([FromRoute] GetByIdProgrammingLanguageQuery getByIdProgrammingLanguageQuery)
    {
        ProgrammingLanguageGetByIdDto result = await _mediator.Send(getByIdProgrammingLanguageQuery);

        return Ok(result);
    }
}
