using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.JDI.Commons;

namespace Epam.JDI.Web.Settings
{
    public static class MapInterfaceToElement
    {
        private static Dictionary<Type, Type> map = new Dictionary<Type, Type>();

        public static void Init(object[][] pairs)
        {
            map = new Dictionary<Type, Type>();
            pairs.Where(pair => pair.Length == 2).ForEach(pair =>
                map.Add((Type)pair[0], (Type)pair[1]));
        }
        public static void Update(object[][] pairs)
        {
            pairs.Where(pair => pair.Length == 2).ForEach(pair =>
            {
                var key =   (Type) pair[0];
                var value = (Type) pair[1];
                if (map.ContainsKey(key))
                    map.Remove(key);
                map.Add(key, value);
            });
        }

        public static Type ClassFromInterface(Type clazz)
        {
            return map[clazz];
        }
    }
}
