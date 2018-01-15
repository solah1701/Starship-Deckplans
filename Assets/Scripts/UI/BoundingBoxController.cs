using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxController : MonoBehaviour {

    public BoundingBox BoundingBoxPrefab;
    public ObjectModelManager ObjectModelManager;
    public float ScaleXDen = 47;
    public float ScaleXNum = 635;
    public float ScaleXOff = 19;
    public float ScaleYDen = 50;
    public float ScaleYNum = 271;
    public float ScaleYOff = -6;

    public float ScaleX;
    public float ScaleY;

    private BoundingBox _prefab;
	private bool _boxStarted;
    private Vector2 _startPosition;
    private Vector2 _endPosition;

    // Use this for initialization
    void Start ()
    {
        ScaleX = CalculateScaleX();
        ScaleY = CalculateScaleY();
        Debug.Log(string.Format("ScaleX = {0} ScaleY = {1}", ScaleX, ScaleY));
    }
	
	// Update is called once per frame
	void Update () {
        if (ObjectModelManager.SelectVertexMode)
            SetupBoundingBox();
	}

    void SetupBoundingBox()
    {
        if (Input.touchCount < 1)
            return;
        var touch = Input.GetTouch(0);
        if (!_boxStarted && touch.phase == TouchPhase.Began)
        {
            _startPosition = touch.position;
            CreatePrefab();
            var px = CalculatePosition(_startPosition.x, Screen.width, Screen.width/ScaleX);
            var py = CalculatePosition(_startPosition.y, Screen.height, Screen.height/ScaleY);
            _prefab.transform.Translate(px, 0, py);
            _boxStarted = true;
            Debug.Log(string.Format("Bounding Box: Start position = {0} actual = {1} width {2} height {3}",
                _startPosition, _prefab.transform.position, Screen.width, Screen.height));
        }
        if (_boxStarted && touch.phase == TouchPhase.Ended)
        {
            _endPosition = touch.position;
            _boxStarted = false;
            //Debug.Log(string.Format("Bounding Box: End position = {0} delta = {1}", _endPosition, touch.deltaPosition));
        }
    }

    float CalculateScaleX()
    {
        return Screen.width * ScaleXDen/ScaleXNum + ScaleXOff;
    }

    float CalculateScaleY()
    {
        return Screen.height * ScaleYDen/ScaleYNum + ScaleYOff;
    }

    float CalculatePosition(float cursor, float touch, float page)
    {
        return page - 2*cursor*page/touch;
    }

    void CreatePrefab()
    {
		DestroyPrefab ();
        _prefab = Instantiate(BoundingBoxPrefab);
        _prefab.transform.SetParent(transform, true);
    }

	void DestroyPrefab()
	{
	    var prefabs = GetComponentsInChildren<BoundingBox>();
	    foreach (var prefab in prefabs)
	    {
	        Destroy(prefab.gameObject);
	    }
	}
}
