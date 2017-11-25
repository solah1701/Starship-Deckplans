using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintPlane : MonoBehaviour
{

    //public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
	//public string _url = "http://www.cygnus-x1.net/links/lcars/blueprints/uss-enterprise-fasa-15mm-deck-plans-sheet-4.jpg";

	// Use this for initialization
	public IEnumerator SetTexture (string url)
	{
		url = string.Format ("file://{0}", url);
	    WWW www = new WWW(url);
	    //var tex = new Texture2D(4, 4, TextureFormat.DXT5, false);
	    yield return www;
	    //www.LoadImageIntoTexture(tex);
	    Renderer renderer = GetComponent<Renderer>();
		float width = www.texture.width;
		float height = www.texture.height;
		float scaleX = (width / height) - 1F;
		transform.localScale += new Vector3 (scaleX, 0, 0);
		renderer.material.mainTexture = www.texture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var myTouch = Input.GetTouch (0);
		if (Input.touchCount == 1)
			Pan (myTouch.deltaPosition);
		if (Input.touchCount == 2)
			Zoom ();
	}

	private float prevMagnitude = 0;

	void Zoom()
	{
		var touch0 = Input.GetTouch (0);
		var touch1 = Input.GetTouch (1);
		var touch0delta = touch0.deltaPosition;
		var touch1delta = touch1.deltaPosition;
		var touch0pos = touch0.position;
		var touch1pos = touch1.position;
		var origVector = touch0pos - touch0delta - touch1pos - touch1delta;
		var diff = touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began ? 0 : origVector.magnitude - prevMagnitude;
		Debug.Log(string.Format("Zoom Diff: {0} Magnitude: {1} origMag: {2} phase {3}", diff, origVector.magnitude, prevMagnitude, touch0.phase));
		prevMagnitude = origVector.magnitude;
		if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) {
			prevMagnitude = 0;
			return;
		}
		Zoom (diff);
	}

	void Zoom(float factor)
	{
		if (factor < 0.1)
			return;
		const float scale = 120;
		var zoomFactor = factor / scale;
		var ratio = transform.localScale.x / transform.localScale.z;
		if (transform.localScale.x + zoomFactor < 0 || transform.localScale.z + zoomFactor < 0)
			return;
		transform.localScale += new Vector3 (zoomFactor * ratio, 0, zoomFactor);
	}

	void Pan(Vector2 position)
	{ 
		const float scale = 60;
		transform.Translate(-position.x / scale, 0, -position.y / scale);
	}
}
