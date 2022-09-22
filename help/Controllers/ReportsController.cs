using help.Services;
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
  }
}