namespace SGI.WebApp.ApiModels
{
    public class RoleApiModel : BaseApiModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public RolePermissionApiModel[] RolePermissions { get; set; }
        public UserRoleApiModel[] UserRoles { get; set; }
    }
}
