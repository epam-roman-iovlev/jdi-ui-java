using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Epam.JDI.Commons
{
    public static class ReflectionUtils
    {
        public static List<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic).ToList();
        }

        public static List<FieldInfo> StaticFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).ToList();
        }

        public static List<FieldInfo> GetFields(this object obj, params Type[] types)
        {
            return (List<FieldInfo>) GetFields(obj.GetType()).Where(fieldType => types.Contains(fieldType.GetType()));
        }
        public static FieldInfo GetFirstField(this object obj, params Type[] types)
        {
            return GetFields(obj.GetType()).First(field => types.Contains(field.GetType()));
        }

        public static T GetFirstValue<T>(this object obj, params Type[] types)
        {
            return (T) GetFields(obj.GetType()).First(field => types.Contains(field.GetType()))?.GetValue(obj);
        }
        public static string GetClassName(this object obj)
        {
            return obj?.GetType().Name ?? "NULL Class";
        }

        public static bool IsInterfaceOf(this Type type, Type interfaceType)
        {
            return type == interfaceType;
        }
        public static bool IsInterfaceOf(this object obj, Type interfaceType)
        {
            return obj.GetType() == interfaceType;
        }
    }
}
