using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;

public class BlueprintListController : MonoBehaviour {

    public GameObject PrefabItem;
    public RectTransform BlueprintPanel;
    public BaseCanvasController CanvasController;

    private List<ConfigClass.KeyValue> BlueprintNames;

    public void InitItems()
    {
        
    }

    public void AddItem(Blueprint item)
    {
        CreatePrefab(item);
        Debug.Log("Add Item");
    }

    public void RemoveItem()
    {
        
    }

    void CreatePrefab(Blueprint item)
    {
        var itemPanel = Instantiate(PrefabItem);
        var tempItemPanel = itemPanel.GetComponent<BlueprintItemController>();
        tempItemPanel.FileNameText.text = item.FileName;
    }

    void Populate()
    {
        
    }
}
