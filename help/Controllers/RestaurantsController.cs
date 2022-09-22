using System;
using System.Collections.Generic;
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
  public class RestaurantsController : ControllerBase
  {
    private readonly RestaurantsService _rService;

    public RestaurantsController(RestaurantsService rService)
    {
      _rService = rService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Restaurant>>> GetAll()
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        // NOTE getting the user on an un-Authorized get, means user could be null, use ?
        List<Restaurant> restaurants = _rService.GetAll(user?.Id);
        return Ok(restaurants);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetOne(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        // NOTE getting the user on an un-Authorized get, means user could be null, use ?
        Restaurant restaurant = _rService.GetOne(id, user?.Id);
        return Ok(restaurant);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reports")]
    public async Task<ActionResult<List<Report>>> GetReports(int id)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        List<Report> reports = _rService.GetReports(id, user?.Id);
        return Ok(reports);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurantData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        restaurantData.CreatorId = user.Id;
        Restaurant restaurant = _rService.Create(restaurantData);
        restaurant.Creator = user;
        return Ok(restaurant);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Update(int id, [FromBody] Restaurant restaurantData)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        restaurantData.Id = id;
        Restaurant restaurant = _rService.Update(restaurantData, user);
        return Ok(restaurant);

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
        string message = _rService.Delete(id, user);
        return Ok(message);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}