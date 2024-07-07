using AutoMapper;
using CandidateHub.Application.Dtos;
using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Interfaces;

namespace CandidateHub.Application.Services;

public class CandidateService : ICandidateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddOrUpdateAsync(CandidateDto candidateDto)
    {
        var candidate = await _unitOfWork.Candidates
               .GetByEmailAsync(candidateDto.Email);

        if (candidate != null)
        {
            _mapper.Map(candidateDto, candidate);
            _unitOfWork.Candidates.Update(candidate);
        }
        else
        {
            candidate = _mapper.Map<Candidate>(candidateDto);
            await _unitOfWork.Candidates.AddAsync(candidate);
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);
        if (candidate == null)
        {
            throw new KeyNotFoundException("Candidate not found");
        }

        _unitOfWork.Candidates.Remove(candidate);
        await _unitOfWork.CommitAsync();
    }

    public async Task<CandidateDto[]> GetAllAsync()
    {
        var candidates = await _unitOfWork.Candidates.GetAllAsync();

        return _mapper.Map<CandidateDto[]>(candidates);
    }

    public async Task<CandidateDto> GetByIdAsync(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);
        if (candidate == null)
        {
            throw new KeyNotFoundException("Candidate not found");
        }

        return _mapper.Map<CandidateDto>(candidate);
    }
}