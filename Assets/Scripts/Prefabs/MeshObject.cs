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
        //TODO: Ideally the scale of the mesh will need to be saved with the mesh prefab, rather than from the metadata
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
        //UpdateVertexScale(scale);
    }

    protected override void UpdatePan(Vect3 position)
    {
        _currentMesh.Position = position;
    }

    private void UpdateVertexScale(Vect3 scale)
    {
        var vertexSpheres = GetComponentsInChildren<MeshObject>();
        var vertexScale = 0.1f / scale.x;
        foreach (var vertexSphere in vertexSpheres)
        {
            vertexSphere.transform.localScale = new Vector3(vertexScale, vertexScale, vertexScale);
        }

    }
}
