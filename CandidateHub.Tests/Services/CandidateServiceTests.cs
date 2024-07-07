using AutoMapper;
using CandidateHub.Application.Constants;
using CandidateHub.Application.Dtos;
using CandidateHub.Application.Services;
using CandidateHub.Domain.Entities;
using CandidateHub.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace CandidateHub.Tests.Services;

public class CandidateServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly CandidateService _candidateService;

    public CandidateServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Candidate, CandidateDto>().ReverseMap();
        });
        _mapper = mapperConfig.CreateMapper();

        _cache = new MemoryCache(new MemoryCacheOptions());

        _candidateService = new CandidateService(_unitOfWorkMock.Object, _mapper, _cache);
    }

    [Fact]
    public async Task GetAllCandidatesAsync_ShouldReturnAllCandidates()
    {
        var candidates = new Candidate[]
        {
            new Candidate { Id = 1, Email = "test1@example.com" },
            new Candidate { Id = 2, Email = "test2@example.com" }
        };
        _unitOfWorkMock.Setup(u => u.Candidates.GetAllAsync()).ReturnsAsync(candidates);

        var result = await _candidateService.GetAllAsync();

        Assert.Equal(2, result.Length);
        Assert.Equal("test1@example.com", result.First().Email);

        var cachedResult = _cache.Get<CandidateDto[]>(CacheKeys.AllCandidates);
        Assert.NotNull(cachedResult);
        Assert.Equal(2, cachedResult.Length);
    }

    [Fact]
    public async Task GetCandidateByIdAsync_ShouldReturnCandidate()
    {
        var candidate = new Candidate { Id = 1, Email = "test1@example.com" };
        var candidateDto = _mapper.Map<CandidateDto>(candidate);

        _cache.Set(CacheKeys.CandidateById(candidate.Id), candidateDto);

        var result = await _candidateService.GetByIdAsync(candidate.Id);

        Assert.NotNull(result);
        Assert.Equal("test1@example.com", result.Email);
    }

    [Fact]
    public async Task AddOrUpdateAsync_ShouldUpdateExistingCandidateAndInvalidateCache()
    {
        var candidate = new Candidate { Id = 1, Email = "test1@example.com" };
        var candidateDto = new CandidateDto { Email = "test1@example.com", FirstName = "UpdatedName" };

        _unitOfWorkMock.Setup(u => u.Candidates.GetByEmailAsync(candidateDto.Email)).ReturnsAsync(candidate);

        _cache.Set(CacheKeys.AllCandidates, new List<CandidateDto>());
        _cache.Set(CacheKeys.CandidateById(candidate.Id), _mapper.Map<CandidateDto>(candidate));

        await _candidateService.AddOrUpdateAsync(candidateDto);

        _unitOfWorkMock.Verify(u => u.Candidates.Update(It.IsAny<Candidate>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);

        Assert.Null(_cache.Get<CandidateDto[]>(CacheKeys.AllCandidates));
        Assert.Null(_cache.Get<CandidateDto>(CacheKeys.CandidateById(candidate.Id)));
    }

    [Fact]
    public async Task AddOrUpdateAsync_ShouldAddNewCandidateAndInvalidateCache()
    {
        var candidateDto = new CandidateDto { Email = "test2@example.com" };

        _unitOfWorkMock.Setup(u => u.Candidates.GetByEmailAsync(candidateDto.Email)).ReturnsAsync((Candidate)null);

        _cache.Set(CacheKeys.AllCandidates, new List<CandidateDto>());

        await _candidateService.AddOrUpdateAsync(candidateDto);

        _unitOfWorkMock.Verify(u => u.Candidates.AddAsync(It.IsAny<Candidate>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);

        Assert.Null(_cache.Get<CandidateDto[]>(CacheKeys.AllCandidates));
    }

    [Fact]
    public async Task DeleteCandidateAsync_ShouldRemoveCandidateAndInvalidateCache()
    {
        var candidate = new Candidate { Id = 1, Email = "test1@example.com" };
        _unitOfWorkMock.Setup(u => u.Candidates.GetByIdAsync(candidate.Id))
            .ReturnsAsync(candidate);

        _cache.Set(CacheKeys.AllCandidates, new List<CandidateDto>());
        _cache.Set(CacheKeys.CandidateById(candidate.Id), _mapper.Map<CandidateDto>(candidate));

        await _candidateService.DeleteAsync(candidate.Id);

        _unitOfWorkMock.Verify(u => u.Candidates.Remove(It.IsAny<Candidate>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);

        var cachedListResult = _cache.Get<CandidateDto[]>(CacheKeys.AllCandidates);
        var cachedCandidateResult = _cache.Get<CandidateDto>(CacheKeys.CandidateById(candidate.Id));
        Assert.Null(cachedListResult);
        Assert.Null(cachedCandidateResult);
    }
}