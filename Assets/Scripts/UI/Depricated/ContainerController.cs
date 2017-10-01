using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContainerController : MonoBehaviour
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

    public void EnableCanvas(string name)
    {
        EnableCanvasas(false);
        var canvas = _containerCanvases.Find(c => c.CanvasName == name);
        if (canvas == null) return;
        canvas.gameObject.SetActive(true);
    }

    private void EnableCanvasas(bool enable)
    {
        foreach (var containerCanvase in _containerCanvases.Where(c => c.CanvasName != "Menu"))
        {
            containerCanvase.gameObject.SetActive(enable);
        }
    }

    protected virtual void SetControllerReferenceOnButtons()
    {
        _containerCanvases = new List<ContainerCanvas>();
        foreach (var containerCanvas in Canvases.Select(t => t.GetComponent<ContainerCanvas>()))
        {
            _containerCanvases.Add(containerCanvas);
            containerCanvas.SetControllerReference(this);
        }
        EnableCanvasas(false);
    }

}
