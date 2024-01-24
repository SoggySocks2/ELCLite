using System.Reflection;

namespace ELCLite.SharedKernel.SharedObjects
{
    public abstract class PermissionKeyBase<TEnum> :
        PermissionKeyBase<TEnum, int>
        where TEnum : PermissionKeyBase<TEnum, int>
    {
        protected PermissionKeyBase(string name, int value) :
            base(name, value)
        {
        }
    }

    public abstract class PermissionKeyBase<TEnum, TValue>
        where TEnum : PermissionKeyBase<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        static readonly Lazy<TEnum[]> _enumOptions =
            new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

        static readonly Lazy<Dictionary<string, TEnum>> _fromName =
            new Lazy<Dictionary<string, TEnum>>(() => _enumOptions.Value.ToDictionary(item => item.Name));

        static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue =
            new Lazy<Dictionary<TValue, TEnum>>(() =>
            {
                // multiple enums with same value are allowed but store only one per value
                var dictionary = new Dictionary<TValue, TEnum>();
                foreach (var item in _enumOptions.Value)
                {
                    if (!dictionary.ContainsKey(item._value))
                        dictionary.Add(item._value, item);
                }
                return dictionary;
            });

        private static TEnum[] GetAllOptions()
        {
            Type baseType = typeof(TEnum);
            return Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => baseType.IsAssignableFrom(t))
                .SelectMany(t => t.GetFieldsOfType<TEnum>())
                .OrderBy(t => t.Name)
                .ToArray();
        }

        public static IReadOnlyCollection<TEnum> List =>
            _fromName.Value.Values
                .ToList()
                .AsReadOnly();

        private readonly string _name;
        private readonly TValue _value;

        public string Name => _name;

        public TValue Value => _value;

        protected PermissionKeyBase(string name, TValue value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Argument cannot be null or empty.", nameof(name));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _name = name;
            _value = value;
        }

        public static TEnum FromValue(TValue value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!_fromValue.Value.TryGetValue(value, out var result))
            {
                throw new ApplicationException($"No permission with Id {value} found.");
            }
            return result;
        }
    }

    internal static class TypeExtensions
    {
        public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(p => type.IsAssignableFrom(p.FieldType))
                .Select(pi => (TFieldType)pi.GetValue(null))
                .ToList();
        }
    }
}
