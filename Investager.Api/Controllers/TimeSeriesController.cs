﻿using Investager.Core.Dtos;
using Investager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investager.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TimeSeriesController : ControllerBase
{
    private readonly ITimeSeriesService _timeSeriesService;

    public TimeSeriesController(ITimeSeriesService timeSeriesService)
    {
        _timeSeriesService = timeSeriesService;
    }

    [HttpGet("{key}")]
    public async Task<ActionResult<ICollection<TimePointResponse>>> Get([FromRoute] string key)
    {
        var response = await _timeSeriesService.Get(key);

        return response.Points.ToList();
    }
}
