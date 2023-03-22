namespace profileService.API.Models;

public class ProfileUpdate
{
    public string DisplayName { get; set; }
    public string ProfilePictureBase64 { get; set; }
    public string Biography { get; set; }
}