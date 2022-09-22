namespace help.Models
{
  public class Restaurant : RepoItem<int>
  {
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public string Description { get; set; }
    public int Exposure { get; set; }
    public bool? Shutdown { get; set; }
    public int ReportCount { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}