using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    public Button Button;
    public Text ButtonText;
    public string ButtonName;

    private ButtonController _controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClick()
    {
        _controller.ButtonClick(this);
    }

    public void SetControllerReference(ButtonController controller)
    {
        _controller = controller;
    }
}
