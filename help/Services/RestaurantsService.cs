using System;
using System.Collections.Generic;
using help.Models;
using help.Repositories;

namespace help.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _restaurantRepo;
    private readonly ReportsRepository _reportsRepo;

    public RestaurantsService(RestaurantsRepository restaurantRepo, ReportsRepository reportsRepo)
    {
      _restaurantRepo = restaurantRepo;
      _reportsRepo = reportsRepo;
    }

    internal List<Restaurant> GetAll(string userId)
    {
      List<Restaurant> restaurants = _restaurantRepo.GetAll();
      //   NOTE this line filters out shutdown restaurants that you are not the owner of.
      restaurants = restaurants.FindAll(r => r.Shutdown == false || r.CreatorId == userId);
      return restaurants;
    }

    internal Restaurant GetOne(int id, string userId)
    {
      Restaurant restaurant = _restaurantRepo.GetOne(id);
      if (restaurant == null)
      {
        throw new Exception($"No Restaurant at id: {id}");
      }
      if (restaurant.Shutdown == true && restaurant.CreatorId != userId)
      {
        throw new Exception($"{restaurant.Name} is currently closed for business");
      }
      //   NOTE increase exposure each time restaurant is gotten
      restaurant.Exposure++;
      _restaurantRepo.Update(restaurant);
      return restaurant;
    }

    internal List<Report> GetReports(int id, string userId)
    {
      Restaurant restaurant = GetOne(id, userId);
      return _reportsRepo.GetReportsByRestaurantId(id);
    }
    internal Restaurant Create(Restaurant restaurantData)
    {
      return _restaurantRepo.Create(restaurantData);
    }

    internal Restaurant Update(Restaurant update, Account user)
    {
      Restaurant original = GetOne(update.Id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception($"cannot update {original.Name} you are not the creator");
      }
      original.Name = update.Name ?? original.Name;
      original.ImgUrl = update.ImgUrl ?? original.ImgUrl;
      original.Description = update.Description ?? original.Description;
      original.Shutdown = update.Shutdown ?? original.Shutdown;

      return _restaurantRepo.Update(original);
    }


    internal string Delete(int id, Account user)
    {
      Restaurant original = GetOne(id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception($"cannot delete {original.Name}, not yours");
      }
      _restaurantRepo.Delete(id);
      return $"{original.Name} was deleted.";
    }
  }
}