using ELCLite.SharedKernel.SharedObjects;

namespace ELCLite.Identity.Api.Data.Entities
{
    public class Permission
    {
        private Permission() { }

        public PermissionKey Key { get; private set; }
        public bool Value { get; private set; }

        public Permission(PermissionKey key, bool value = false)
        {
            Key = key;
            Value = value;
        }
    }
}
