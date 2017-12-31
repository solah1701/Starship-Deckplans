using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.Extenders;

[RequireComponent(typeof(ZoomAndPan))]
public class BlueprintPlane : MonoBehaviour
{
    private float _prevMagnitude = 0;
    private ZoomAndPan _zoomAndPan;
    private float _scaleX;
    private Blueprint _currentBlueprint;

    public IEnumerator SetBlueprintItem(Blueprint item)
    {
        _zoomAndPan = GetComponent<ZoomAndPan>();
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
        Debug.Log(string.Format("SetBlueprintItem Scale: {0} Position: {1}", _currentBlueprint.Scale, _currentBlueprint.Position));
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

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
            _currentBlueprint.Position = _zoomAndPan.Pan().Map();
        if (Input.touchCount == 2)
        {
            var theScale = _zoomAndPan.Zoom();
            if (theScale != Vector3.zero) _currentBlueprint.Scale = theScale.Map();
        }
        Debug.Log(string.Format("Zoom x: {0} z: {1}", _currentBlueprint.Scale.x, _currentBlueprint.Scale.z));
    }
}
