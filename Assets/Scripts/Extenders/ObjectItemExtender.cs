using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Models;

namespace Assets.Scripts.Extenders
{
    public static class ObjectItemExtender
    {
        public static ObjectItemList ConvertList(this IEnumerable<ObjectItem> value)
        {
            var result = new ObjectItemList();
            result.AddRange(value);
            return result;
        }

        public static T Cast<T>(this ObjectItem value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
