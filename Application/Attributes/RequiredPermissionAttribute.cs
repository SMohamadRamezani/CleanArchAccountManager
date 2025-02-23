namespace ApiEndPoint.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiredPermissionAttribute(string permission) : Attribute
    {
        public string Permission { get; } = permission;
    }
}
