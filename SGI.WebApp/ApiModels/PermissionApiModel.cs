namespace SGI.WebApp.ApiModels
{
    public class PermissionApiModel : BaseApiModel
    {
        public string Name { get; set; }

        public RolePermissionApiModel[] RolePermissions { get; set; }
    }
}
