using help.Repositories;

namespace help.Services
{
  public class ReportsService
  {
    private readonly ReportsRepository _reportsRepo;

    public ReportsService(ReportsRepository reportsRepo)
    {
      _reportsRepo = reportsRepo;
    }
  }
}