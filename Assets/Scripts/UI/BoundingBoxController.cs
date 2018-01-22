using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchHelper))]
public class BoundingBoxController : MonoBehaviour {

    public BoundingBox BoundingBoxPrefab;
    public ObjectModelManager ObjectModelManager;

    private float _scaleX;
    private float _scaleY;

    private float _startX;
    private float _startY;

    private BoundingBox _prefab;
	private bool _boxStarted;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector3 _referencePosition;
    private float _zScale;
    private TouchHelper _touchHelper;

    void Start ()
    {
        _touchHelper = GetComponent<TouchHelper>();
        _touchHelper.CalculateScale();
        _scaleX = _touchHelper.GetScaleX();
        _scaleY = _touchHelper.GetScaleY();
    }
	
	// Update is called once per frame
	void Update () {
        if (ObjectModelManager.SelectVertexMode)
            SetupBoundingBox();
	}

    void SetupBoundingBox()
    {
        if (!_touchHelper.TouchInBounds()) return;
        var touch = _touchHelper.GetTouch();
        if (!_boxStarted && touch.phase == TouchPhase.Began)
        {
            ObjectModelManager.ResetVertices();
            _startPosition = touch.position;
            CreatePrefab();
            _zScale = _prefab.transform.localScale.y;
            _startX = _touchHelper.CalculatePosition(_startPosition.x, Screen.width, Screen.width/_scaleX);
            _startY = _touchHelper.CalculatePosition(_startPosition.y, Screen.height, Screen.height/_scaleY);
            _referencePosition = _prefab.transform.position;
            _prefab.transform.Translate(_startX, 0, _startY);
            _boxStarted = true;
            //Debug.Log(string.Format("Bounding Box: Start position = {0} actual = {1} width {2} height {3}",
            //    _startPosition, _prefab.transform.position, Screen.width, Screen.height));
        }
		if (_boxStarted && touch.phase == TouchPhase.Moved) {
			var diff = touch.position;
			var px = _touchHelper.CalculatePosition (diff.x, Screen.width, Screen.width / _scaleX);
			var py = _touchHelper.CalculatePosition (diff.y, Screen.height, Screen.height / _scaleY);
			var diffX = _startX - px;
			var diffY = _startY - py;

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
