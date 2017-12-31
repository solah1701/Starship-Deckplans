using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Models;

public abstract class ButtonListManager : MonoBehaviour
{
	//void OnEnable()
	//{
		//InitButtons ();
	//}

    public abstract ObjectItemList InitButtons();

    public abstract ObjectItemList AddButton();

    public abstract ObjectItemList RemoveButton(string value);

	public abstract void ButtonListClicked(string value);
}
