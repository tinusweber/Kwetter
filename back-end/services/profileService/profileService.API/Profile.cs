namespace ProfileService.Api
{
    public class Profile
    {
        public string DisplayName { get; set; }
        public Guid OwnerId { get; set; }
        public string ProfilePictureBase64 { get; set; }
        public string Biography { get; set; }
        public List<Guid> FollowingUsers { get; set; }
        public bool Blocked { get; set; }
    }
}
