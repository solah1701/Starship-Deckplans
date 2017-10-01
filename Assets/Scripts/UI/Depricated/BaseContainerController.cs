using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BaseContainerController : MonoBehaviour
{

    public Canvas[] Canvases;
    private List<ContainerCanvas> _containerCanvases;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        SetControllerReferenceOnButtons();
    }

    protected virtual void SetControllerReferenceOnButtons()
    {
        _containerCanvases = new List<ContainerCanvas>();
        foreach (var containerCanvas in Canvases.Select(t => t.GetComponent<ContainerCanvas>()))
        {
            _containerCanvases.Add(containerCanvas);
            //containerCanvas.SetControllerReference(this);
        }
    }
}
