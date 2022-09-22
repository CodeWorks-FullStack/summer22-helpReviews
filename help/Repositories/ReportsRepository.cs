using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using help.Models;

namespace help.Repositories
{
  public class ReportsRepository
  {
    private readonly IDbConnection _db;

    public ReportsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Report> GetReportsByRestaurantId(int restaurantId)
    {
      string sql = @"
        SELECT
        rep.*,
        a.*
        FROM reports rep
        JOIN accounts a ON rep.creatorId = a.id
        WHERE rep.restaurantId = @restaurantId
        ";
      return _db.Query<Report, Profile, Report>(sql, (rep, profile) =>
      {
        rep.Creator = profile;
        return rep;
      }, new { restaurantId }).ToList();
    }
  }
}