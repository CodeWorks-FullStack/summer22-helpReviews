using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using help.Models;

namespace help.Repositories
{
  public class RestaurantsRepository
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Restaurant> GetAll()
    {
      string sql = @"
    SELECT
        rest.*,
        COUNT(rep.id) AS reportCount,
        a.*
    FROM restaurants rest
    LEFT JOIN reports rep ON rep.restaurantId = rest.id 
    JOIN accounts a ON rest.creatorId = a.id
    GROUP BY(rest.id)
    ORDER BY rest.exposure desc;
      ";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, profile) =>
      {
        rest.Creator = profile;
        return rest;
      }).ToList();
    }


    internal Restaurant GetOne(int id)
    {
      string sql = @"
    SELECT
        rest.*,
        COUNT(rep.id) AS reportCount,
        a.*
    FROM restaurants rest
    LEFT JOIN reports rep ON rep.restaurantId = rest.id 
    JOIN accounts a ON rest.creatorId = a.id
    WHERE rest.id = @id
    GROUP BY(rest.id);
      ";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, profile) =>
      {
        rest.Creator = profile;
        return rest;
      }, new { id }).FirstOrDefault();
    }


    internal Restaurant Create(Restaurant restaurantData)
    {
      string sql = @"
        INSERT INTO restaurants
        (name, imgUrl, description, creatorId)
        VALUES
        (@name, @imgUrl, @description, @creatorId);
        SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, restaurantData);
      restaurantData.Id = id;
      return restaurantData;
    }


    internal Restaurant Update(Restaurant update)
    {
      string sql = @"
        UPDATE restaurants SET
        name = @name,
        imgUrl = @imgUrl,
        description = @description,
        exposure = @exposure,
        shutdown = @shutdown
        WHERE id = @id;
        ";
      _db.Execute(sql, update);
      return update;
    }
    internal void Delete(int id)
    {
      string sql = @"
        DELETE FROM restaurants WHERE id = @id;
        ";

      _db.Execute(sql, new { id });
    }
  }
}