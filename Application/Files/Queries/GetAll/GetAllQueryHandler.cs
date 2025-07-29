using System.Text.Json;
using Application.Dtos.Files;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Files.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<GetFileDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private const string CacheKey = "all_files"; 

    public GetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<List<GetFileDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var cachedData = await _cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<GetFileDto>>(cachedData) ?? new List<GetFileDto>();
        }
        
        var files = await _unitOfWork.FilesRepo.GetAll(cancellationToken);
        var filesDto = _mapper.Map<List<GetFileDto>>(files);
        
        var cacheOptions = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };

        var serialized = JsonSerializer.Serialize(filesDto);
        await _cache.SetStringAsync(CacheKey, serialized, cacheOptions, token: cancellationToken);
        
        return filesDto;
    }
}