using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestController))]
public class TestPanel : MonoBehaviour
{

    private TestController _testController;

    private void Awake()
    {
        _testController = GetComponent<TestController>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonClicked()
    {
        _testController.ButtonClicked();
    }
}
