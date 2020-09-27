namespace SGI.WebApp.ApiModels
{
    public class UserRoleApiModel : BaseApiModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserApiModel User { get; set; }
        public RoleApiModel Role { get; set; }

    }
}
