using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelListController : MonoBehaviour {

    public GameObject PrefabItem;
    public RectTransform BlueprintPanel;
    public ObjectModelManager ObjectModelManager;

    private ModelMesh Mesh;
    public ModelMesh Current { get; set; }
    
    public void AddItem()
    {
		ObjectModelManager.AddModel ();
        //var model = ObjectModelManager.AddModel();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
