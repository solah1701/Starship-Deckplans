using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxController : MonoBehaviour {

    public BoundingBox BoundingBoxPrefab;
    public ObjectModelManager ObjectModelManager;

    private BoundingBox _prefab;
	private bool _boxStarted;
    private Vector2 _startPosition;
    private Vector2 _endPosition;

    // Use this for initialization
    void Start () {
		
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
		if (!_boxStarted && touch.phase == TouchPhase.Began) {
			_startPosition = touch.position;
			CreatePrefab ();
		    _prefab.transform.position = _startPosition;
			_boxStarted = true;
			Debug.Log(string.Format("Bounding Box: Start position {0}", _startPosition));
		}
        if (_boxStarted && touch.phase == TouchPhase.Ended)
        {
            _endPosition = touch.position;
			_boxStarted = false;
			Debug.Log(string.Format("Bounding Box: End position {0}", _endPosition));
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
