using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintController : ButtonController
{
    public Text ActionName;
    public GameObject containerObject;

    protected GameObject _spawnedObject;

    public void GetBlueprint()
    {
        //ActionName.text = "Load";
    }

	// Use this for initialization
	void Start () {
        _spawnedObject = Instantiate(containerObject) as GameObject;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void ButtonClick(MenuButton button)
    {
        _spawnedObject.SendMessage("EnableCanvas", button.ButtonText.text, SendMessageOptions.RequireReceiver);

    }
}
