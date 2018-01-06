using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelListController : MonoBehaviour {

    public GameObject PrefabItem;
    public RectTransform ModelPanel;
    public ObjectModelManager ObjectModelManager;

    private List<ModelMesh> _meshList;
    public ModelMesh Current { get; set; }
    
    // Use this for initialization
    void Start ()
    {
        InitItems();
    }

    public void InitItems()
    {
        _meshList = ObjectModelManager.GetModelMeshList() as List<ModelMesh>;
        PopulateItems();
    }

    public void AddItem()
    {
        _meshList = ObjectModelManager.AddModel() as List<ModelMesh>;
        PopulateItems();
    }

    public void RemoveItem()
    {
        if (Current == null) return;
        _meshList = ObjectModelManager.RemoveModel(Current) as List<ModelMesh>;
        PopulateItems();
    }

    void PopulateItems()
    {
        var index = 0;
        if (_meshList == null) return;
        ClearItems();
        foreach (var modelMesh in _meshList)
        {
            CreatePrefab(modelMesh, index++);
        }
    }
    
    void ClearItems()
    {
        var items = ModelPanel.GetComponentsInChildren<ModelItemController>();
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

    void CreatePrefab(ModelMesh item, int index)
    {
        var ypos = -index * 100 - 60;
        var itemPanel = Instantiate(PrefabItem);
        itemPanel.transform.SetParent(ModelPanel, false);
        itemPanel.transform.position += new Vector3(0, ypos, 0);
        var tempItemPanel = itemPanel.GetComponent<ModelItemController>();
		tempItemPanel.MeshNameText.text = item.MeshId;
        var tempButton = tempItemPanel.GetComponentInChildren<Button>();
        tempButton.onClick.AddListener(() => TheButtonClicked(item));
    }

    void TheButtonClicked(ModelMesh item)
    {
        Current = item;
    }
}
