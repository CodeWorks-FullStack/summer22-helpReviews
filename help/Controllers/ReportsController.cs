using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using help.Models;
using help.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace help.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReportsController : ControllerBase
  {
    private readonly ReportsService _reportsService;

    public ReportsController(ReportsService reportsService)
    {
      _reportsService = reportsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Report>> Create([FromBody] Report reportData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        Report report = _reportsService.Create(reportData);
        report.Creator = user;
        return Ok(report);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        string message = _reportsService.Delete(id, user.Id);
        return Ok(message);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}