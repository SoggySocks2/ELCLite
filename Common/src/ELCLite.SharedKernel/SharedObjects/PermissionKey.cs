namespace ELCLite.SharedKernel.SharedObjects
{
    public class PermissionKey : PermissionKeyBase<PermissionKey>
    {
        // Customer 1-100
        public static readonly PermissionKey CustomerRead = new(1, nameof(CustomerRead), "Customer Read", "Customer");
        public static readonly PermissionKey CustomerWrite = new(2, nameof(CustomerWrite), "Customer Write", "Customer");
        public static readonly PermissionKey CustomerUpdate = new(3, nameof(CustomerUpdate), "Customer Update", "Customer");
        public static readonly PermissionKey CustomerDelete = new(4, nameof(CustomerDelete), "Customer Delete", "Customer");

        // Vehicle 101-199
        public static readonly PermissionKey VehicleRead = new(101, nameof(VehicleRead), "Vehicle Read", "Vehicle");
        public static readonly PermissionKey VehicleWrite = new(102, nameof(VehicleWrite), "Vehicle Write", "Vehicle");
        public static readonly PermissionKey VehicleUpdate = new(103, nameof(VehicleUpdate), "Vehicle Update", "Vehicle");
        public static readonly PermissionKey VehicleDelete = new(104, nameof(VehicleDelete), "Vehicle Delete", "Vehicle");

        private PermissionKey(int value, string name, string description, string group)
            : base(name, value)
        {
            Description = description;
            Group = group;
        }

        public string Description { get; private set; }
        public string Group { get; private set; }
    }
}
