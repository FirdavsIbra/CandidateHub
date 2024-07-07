using CandidateHub.Application.Dtos;
using CandidateHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController: ControllerBase
{
    private readonly ICandidateService _candidateService;
    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet]
    public async Task<ActionResult<CandidateDto[]>> GetAllAsync()
    {
        return Ok(await _candidateService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CandidateDto>> GetByIdAsync(int id)
    {
        return Ok(await _candidateService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult>AddOrUpdateAsync(CandidateDto candidate)
    {
        await _candidateService.AddOrUpdateAsync(candidate);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _candidateService.DeleteAsync(id);
        return Ok();
    }
}