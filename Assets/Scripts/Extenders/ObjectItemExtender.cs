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
			if (value == null || value.Count() == 0)
				return new ObjectItemList ();
            var result = new ObjectItemList();
            result.AddRange(value);
            return result;
        }

        public static T Cast<T>(this ObjectItem value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static List<ButtonItem> ConvertList(this IEnumerable<ConfigClass.KeyValue> list)
        {
            return list.Select(keyValue => new ButtonItem {Item = keyValue}).ToList();
        }
    }
}
