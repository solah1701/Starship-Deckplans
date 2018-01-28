using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;


public class FavouriteButtonListManager : ButtonListManager
{
    public FileSystemCanvasController FileSystemManager;

    public override ObjectItemList InitButtons()
    {
        return FileSystemManager.InitButtons();
    }

    public override ObjectItemList AddButton()
    {
        return FileSystemManager.AddButton();
    }

    public override ObjectItemList RemoveButton(string value)
    {
        return FileSystemManager.RemoveButton(value);
    }

	public override void ButtonListClicked (string value)
	{
		FileSystemManager.ButtonListClicked (value);
	}
}
