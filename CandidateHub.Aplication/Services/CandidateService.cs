using AutoMapper;
using CandidateHub.Application.Constants;
using CandidateHub.Application.Dtos;
using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateHub.Application.Services;

public class CandidateService : ICandidateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);
    public CandidateService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
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

        _cache.Remove(CacheKeys.AllCandidates);
        if (candidate != null)
        {
            _cache.Remove(CacheKeys.CandidateById(candidate.Id));
        }
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

        _cache.Remove(CacheKeys.AllCandidates);
        _cache.Remove(CacheKeys.CandidateById(id));
    }

    public async Task<CandidateDto[]> GetAllAsync()
    {
        if (!_cache.TryGetValue(CacheKeys.AllCandidates, out CandidateDto[] cachedCandidates))
        {
            var candidates = await _unitOfWork.Candidates.GetAllAsync();
            cachedCandidates = _mapper.Map<CandidateDto[]>(candidates);

            _cache.Set(CacheKeys.AllCandidates, cachedCandidates, _cacheExpiration);
        }

        return cachedCandidates;
    }

    public async Task<CandidateDto> GetByIdAsync(int id)
    {
        var cacheKey = CacheKeys.CandidateById(id);

        if (!_cache.TryGetValue(cacheKey, out CandidateDto cachedCandidate))
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);
            if (candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found");
            }

            cachedCandidate = _mapper.Map<CandidateDto>(candidate);

            _cache.Set(cacheKey, cachedCandidate, _cacheExpiration);
        }

        return cachedCandidate;
    }
}