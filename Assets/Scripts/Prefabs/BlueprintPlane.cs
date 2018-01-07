using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.Extenders;

public class BlueprintPlane : GameObjectBase
{
    private float _scaleX;
    private Blueprint _currentBlueprint;

    public IEnumerator SetBlueprintItem(Blueprint item, bool enableZoomAndPan)
    {
        SetItem(enableZoomAndPan);
        _currentBlueprint = item;
        var filename = Path.Combine(item.FilePath, item.FileName);
        yield return StartCoroutine(SetTexture(filename));
        if (item.Scale.IsZero())
        {
            transform.localScale += new Vector3(_scaleX, 0, 0);
            _currentBlueprint.Scale = transform.localScale.Map();
            _currentBlueprint.Position = transform.position.Map();
            yield return null;
        }
        transform.localScale = _currentBlueprint.Scale.Map();
        transform.position = _currentBlueprint.Position.Map();
    }

    // Use this for initialization
    public IEnumerator SetTexture (string url)
	{
		url = string.Format ("file://{0}", url);
	    WWW www = new WWW(url);
	    yield return www;
	    Renderer renderer = GetComponent<Renderer>();
		float width = www.texture.width;
		float height = www.texture.height;
		_scaleX = (width / height) - 1F;
		renderer.material.mainTexture = www.texture;
	}

    protected override void UpdateZoom(Vect3 scale)
    {
        _currentBlueprint.Scale = scale;
    }

    protected override void UpdatePan(Vect3 position)
    {
        _currentBlueprint.Position = position;
    }

}
