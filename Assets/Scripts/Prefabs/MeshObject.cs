using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using UnityEngine;

public class MeshObject : GameObjectBase
{
    private ModelMesh _currentMesh;

    public void SetItem(ModelMesh item, bool enableZoomAndPan)
    {
        SetItem(enableZoomAndPan);
        _currentMesh = item;
        if (item.Scale.IsZero())
        {
            _currentMesh.Scale = transform.localScale.Map();
            _currentMesh.Position = transform.position.Map();
        }
        transform.localScale = _currentMesh.Scale.Map();
        transform.position = _currentMesh.Position.Map();
    }

    protected override void UpdateZoom(Vect3 scale)
    {
        _currentMesh.Scale = scale;
        Debug.Log(string.Format("Zoom x: {0} z: {1}", _currentMesh.Scale.x, _currentMesh.Scale.z));
    }

    protected override void UpdatePan(Vect3 position)
    {
        _currentMesh.Position = position;
    }
}
