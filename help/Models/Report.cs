namespace help.Models
{
  public class Report : RepoItem<int>
  {
    public string Title { get; set; }
    public int Rating { get; set; }
    public string Body { get; set; }
    public int RestaurantId { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}