using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class ButtonItem : ObjectItem
    {
        public ConfigClass.KeyValue Item { get; set; }
        public string Key { get { return Item == null ? "" : Item.Key; } }
        public string Value { get { return Item == null ? "" : Item.Value; } }
    }
}
