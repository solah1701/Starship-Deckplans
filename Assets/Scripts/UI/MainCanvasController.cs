using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{

    public Text MenuButton;
    public BaseCanvasController[] CanvasControllers;
    public int ShowIndex;

	// Use this for initialization
	void Start () {
	    foreach (BaseCanvasController canvasController in CanvasControllers)
	    {
	        canvasController.ShowGameObject(false);
	    }
        CanvasControllers[ShowIndex].ShowGameObject(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MenuSelected(string value)
    {
        MenuButton.text = value;
    }
}
