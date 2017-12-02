using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Assets.Scripts.Models
{
    public class ObjectItem
    {
    }

    public class ObjectItemList : List<ObjectItem>
    {
    }

	[System.Serializable]
	public class ObjectItemListEvent: UnityEvent<ObjectItemList> 
	{
	}
}
