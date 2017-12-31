using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{

    public Text TheText;

    private int _clickCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked()
    {
        TheText.text = string.Format("Button Clicked {0} times", ++_clickCount);
    }
}
