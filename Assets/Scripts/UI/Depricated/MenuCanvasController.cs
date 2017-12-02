using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasController : BaseCanvasController
{
    public MainCanvasController MainCanvasController;

    public void SelectBlueprint()
    {
        Select("Blueprint");
    }

    public void SelectMesh()
    {
        Select("Mesh");
    }

    private void Select(string value)
    {
        MainCanvasController.MenuSelected(value);
        ShowGameObject(false);
    }
}
