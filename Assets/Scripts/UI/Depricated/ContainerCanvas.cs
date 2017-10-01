using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCanvas : MonoBehaviour
{

    public string CanvasName;
    private ContainerController _controller;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetControllerReference(ContainerController controller)
    {
        _controller = controller;
    }
}
