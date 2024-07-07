using AutoMapper;
using CandidateHub.Application.Dtos;
using CandidateHub.Domain.Entities;

namespace CandidateHub.Application.Mapping;

public class ApplicationMapperProfile: Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<Candidate, CandidateDto>().ReverseMap();
    }
}