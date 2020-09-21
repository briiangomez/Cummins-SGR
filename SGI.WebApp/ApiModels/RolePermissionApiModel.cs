namespace SGI.WebApp.ApiModels
{
    public class RolePermissionApiModel : BaseApiModel
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public RoleApiModel Role { get; set; }

        public PermissionApiModel Permission { get; set; }
    }
}
