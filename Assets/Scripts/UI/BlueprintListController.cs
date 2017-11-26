using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintListController : MonoBehaviour
{

    public GameObject PrefabItem;
    public GameObject PrefabBluprint;
    public RectTransform BlueprintPanel;
    public BaseCanvasController CanvasController;

    private ObjectItemList BlueprintNames;
    public Blueprint Current { get; set; }

    void Start()
    {
        InitItems();
    }

    public void InitItems()
    {
        BlueprintNames = CanvasController.InitButtons();
        PopulateItems();
    }

    public void AddBlueprint()
    {
        CanvasController.AddButton();
    }

    public List<Blueprint> AddItem(Blueprint item)
    {
        BlueprintNames.Add(item);
        PopulateItems();
        Current = item;
        Debug.Log(string.Format("Add Item {0}", item));
        return BlueprintNames.Cast<Blueprint>().ToList();
    }

    public void RemoveBlueprint()
    {
        Remove(Current);
    }

    void Remove(Blueprint value)
    {
        var countBefore = BlueprintNames.Count;
        Debug.Log(string.Format("before = {0}", countBefore));
        if (BlueprintNames.Contains(value)) BlueprintNames.Remove(value);
        var countAfter = BlueprintNames.Count;
        PopulateItems();
        var countFinal = BlueprintNames.Count;
        Debug.Log(string.Format("before = {1}, after = {2}, final = {3}, Remove {0}", value.FileName, countBefore, countAfter, countFinal));
    }

    private void PopulateItems()
    {
        var index = 0;
        Clear();
        if (BlueprintNames == null) return;
        foreach (var blueprintName in BlueprintNames)
        {
            CreatePrefab(blueprintName.Cast<Blueprint>(), index++);
        }
    }

    void Clear()
    {
        ClearItems();
        ClearPrefabItems();
    }

    void ClearItems()
    {
        var items = BlueprintPanel.GetComponentsInChildren<BlueprintItemController>();
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

    void ClearPrefabItems()
    {
        var items = BlueprintPanel.GetComponentsInChildren<BlueprintPlane>();
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

    void CreatePrefab(Blueprint item, int index)
    {
        var ypos = -index * 100 - 60;
        var itemPanel = Instantiate(PrefabItem);
        itemPanel.transform.SetParent(BlueprintPanel, false);
        itemPanel.transform.position += new Vector3(0, ypos, 0);
        var tempItemPanel = itemPanel.GetComponent<BlueprintItemController>();
        tempItemPanel.FileNameText.text = item.FileName;
        var tempButton = tempItemPanel.GetComponentInChildren<Button>();
        tempButton.onClick.AddListener(() => TheButtonClicked(item));
        CreateBlueprintPrefab(item);
    }

    /// <summary>
    /// Create the Blueprint Item on a plane from the jpg image file
    /// </summary>
    /// <param name="item"></param>
    void CreateBlueprintPrefab(Blueprint item)
    {
		var plane = Instantiate(PrefabBluprint);
        plane.transform.SetParent(BlueprintPanel, true);
        var scriptReference = plane.GetComponent<BlueprintPlane>();
        if (scriptReference == null) return;
        StartCoroutine(scriptReference.SetBlueprintItem(item));
    }

    void TheButtonClicked(Blueprint value)
    {
        Current = value;
    }

}
