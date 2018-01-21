using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (TouchHelper))]
public class EditMeshContoller : MonoBehaviour
{
    public ObjectModelManager ObjectModelManager;
    private TouchHelper _touchHelper;

    // Use this for initialization
    void Start()
    {
        _touchHelper = GetComponent<TouchHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectModelManager.EditVertexMode)
            MoveVertices();
    }

    void MoveVertices()
    {
        if (!_touchHelper.TouchInBounds()) return;
    }
}
