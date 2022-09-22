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
    internal Report GetOne(int id)
    {
      string sql = @"
      SELECT 
      rep.*,
      a.* 
      FROM reports rep
      JOIN accounts a ON rep.creatorId = a.id
      WHERE id = @Id;
      ";
      return _db.Query<Report, Profile, Report>(sql, (rep, profile) =>
      {
        rep.Creator = profile;
        return rep;
      }, new { id }).FirstOrDefault();
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


    internal Report Create(Report reportData)
    {
      string sql = @"
      INSERT INTO reports
      (title, body, rating, creatorId, restaurantId)
      VALUES
      (@title, @body, @rating, @creatorId, @restaurantId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, reportData);
      reportData.Id = id;
      return reportData;
    }
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM reports WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}