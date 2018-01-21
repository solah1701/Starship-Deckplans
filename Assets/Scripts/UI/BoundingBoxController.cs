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

    public int LeftBounds;
    public int RightBounds;
    public int TopBounds;
    public int BottomBounds;

    private float ScaleX;
    private float ScaleY;

    private float _startX;
    private float _startY;

    private BoundingBox _prefab;
	private bool _boxStarted;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector3 _referencePosition;
    private float _zScale;

    private int _left, _right, _top, _bottom;

    // Use this for initialization
    void Start ()
    {
        ScaleX = CalculateScaleX();
        ScaleY = CalculateScaleY();
        _left = LeftBounds;
        _right = Screen.width - RightBounds;
        _top = Screen.height - TopBounds;
        _bottom = BottomBounds;

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
        var pos = touch.position;
        if (pos.x < _left || pos.x > _right || pos.y > _top || pos.y < _bottom) return;
        if (!_boxStarted && touch.phase == TouchPhase.Began)
        {
            ObjectModelManager.ResetVertices();
            _startPosition = touch.position;
            CreatePrefab();
            _zScale = _prefab.transform.localScale.y;
            _startX = CalculatePosition(_startPosition.x, Screen.width, Screen.width/ScaleX);
            _startY = CalculatePosition(_startPosition.y, Screen.height, Screen.height/ScaleY);
            _referencePosition = _prefab.transform.position;
            _prefab.transform.Translate(_startX, 0, _startY);
            _boxStarted = true;
            //Debug.Log(string.Format("Bounding Box: Start position = {0} actual = {1} width {2} height {3}",
            //    _startPosition, _prefab.transform.position, Screen.width, Screen.height));
        }
		if (_boxStarted && touch.phase == TouchPhase.Moved) {
			var diff = touch.position;
			var px = CalculatePosition (diff.x, Screen.width, Screen.width / ScaleX);
			var py = CalculatePosition (diff.y, Screen.height, Screen.height / ScaleY);
			var diffScaleX = CalculatePosition (diff.x, Screen.width, Screen.width / ScaleX);
			var diffScaleY = CalculatePosition (diff.y, Screen.height, Screen.height / ScaleY);
			var diffX = _startX - diffScaleX;
			var diffY = _startY - diffScaleY;

			_prefab.transform.position = _referencePosition;
			_prefab.transform.Translate (px + diffX / 2, 0, py + diffY / 2);

			if (diffX < 0)
				diffX = diffX * -1;
			if (diffY < 0)
				diffY = diffY * -1;

			_prefab.transform.localScale = new Vector3 (diffX, _zScale, diffY);

			//Debug.Log (string.Format ("Bounding Box: Moving position = {0} actual = {1} diff x {2} diff y {3}",
			//	diff, _prefab.transform.position, diffX, diffY));
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
