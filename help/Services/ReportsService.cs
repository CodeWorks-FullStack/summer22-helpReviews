using System;
using help.Models;
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

    internal Report GetOne(int id)
    {
      Report report = _reportsRepo.GetOne(id);
      if (report == null)
      {
        throw new Exception($"No report at id: {id}");
      }
      return report;
    }

    internal Report Create(Report reportData)
    {
      return _reportsRepo.Create(reportData);
    }

    internal string Delete(int id, string userId)
    {
      Report original = GetOne(id);
      if (original.CreatorId != userId)
      {
        throw new Exception($"Can't delete {original.Title}, not owner");
      }
      _reportsRepo.Delete(id);
      return $"Deleted the {original.Title} report.";
    }
  }
}