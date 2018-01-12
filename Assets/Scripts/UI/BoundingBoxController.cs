using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxController : MonoBehaviour {

    public BoundingBox BoundingBoxPrefab;
    public ObjectModelManager ObjectModelManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ObjectModelManager.SelectVertexMode)
            Debug.LogWarning("Vertex Mode");
	}
}
