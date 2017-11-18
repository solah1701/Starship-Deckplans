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
		renderer.material.mainTexture = www.texture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
