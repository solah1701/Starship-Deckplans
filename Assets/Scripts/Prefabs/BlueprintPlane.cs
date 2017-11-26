using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts.Extenders;

public class BlueprintPlane : MonoBehaviour
{

    private float _prevMagnitude = 0;
    private float _scaleX;
    private Blueprint _currentBlueprint;

    public IEnumerator SetBlueprintItem(Blueprint item)
    {
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
	void Update () 
	{
		if (Input.touchCount == 1)
			Pan ();
		if (Input.touchCount == 2)
			Zoom ();
	}

	void Zoom()
	{
		var touch0 = Input.GetTouch (0);
		var touch1 = Input.GetTouch (1);
		var touch0Delta = touch0.deltaPosition;
		var touch1Delta = touch1.deltaPosition;
		var touch0Pos = touch0.position;
		var touch1Pos = touch1.position;
		var origVector = touch0Pos - touch0Delta - touch1Pos - touch1Delta;
		var diff = touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began ? 0 : origVector.magnitude - _prevMagnitude;
		_prevMagnitude = origVector.magnitude;
		if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) {
			_prevMagnitude = 0;
			return;
		}
		Zoom (diff);
	}

	void Zoom(float factor)
	{
		const float scale = 120;
		float n = 1.0f;
		if (transform.localScale.x > 3.0f)
			n = 3.0f;
		var zoomFactor = factor / (scale / n );
		var ratio = transform.localScale.x / transform.localScale.z;
		if (transform.localScale.x + zoomFactor < 0 || transform.localScale.z + zoomFactor < 0)
			return;
		transform.localScale += new Vector3 (zoomFactor * ratio, 0, zoomFactor);
		Debug.Log (string.Format ("Zoom factor: {0} x: {1} z: {2}", zoomFactor, transform.localScale.x, transform.localScale.z));
        _currentBlueprint.Scale = transform.localScale.Map();

	}

	void Pan()
	{
        var myTouch = Input.GetTouch(0);
	    Vector2 position = myTouch.deltaPosition;
        const float scale = 60;
		transform.Translate(-position.x / scale, 0, -position.y / scale);
        _currentBlueprint.Position = transform.position.Map();
	}
}
