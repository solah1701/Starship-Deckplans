using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private List<Blueprint> BlueprintNames;

    void Start()
    {
        InitItems();
    }

    public void InitItems()
    {
        BlueprintNames = new List<Blueprint>();
    }

    public void AddBlueprint()
    {
        CanvasController.AddButton();
    }

    public void AddItem(Blueprint item)
    {
        BlueprintNames.Add(item);
        PopulateItems();
        Debug.Log(string.Format("Add Item {0}", item));
    }

    public List<Blueprint> GetBlueprints()
    {
        return BlueprintNames;
    }

    public void SetBlueprints(List<Blueprint> value)
    {
        BlueprintNames = value;
        PopulateItems();
    }

    private void PopulateItems()
    {
        var index = 0;
        Clear();
        if (BlueprintNames == null) return;
        foreach (var blueprintName in BlueprintNames)
        {
            CreatePrefab(blueprintName, index++);
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
        var ypos = -index * 100 - 100;
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
        var filename = Path.Combine(item.FilePath, item.FileName);
        WWW www = new WWW(filename);

        //TODO: need to get the async pattern working
        //yield return www;

        var tmpRenderer = plane.GetComponent<Renderer>();
        tmpRenderer.material.mainTexture = www.texture;
    }

    void TheButtonClicked(Blueprint value)
    {
        var countBefore = BlueprintNames.Count;
        Debug.Log(string.Format("before = {0}", countBefore));
        if (BlueprintNames.Contains(value)) BlueprintNames.Remove(value);
        var countAfter = BlueprintNames.Count;
        PopulateItems();
        var countFinal = BlueprintNames.Count;
        Debug.Log(string.Format("before = {1}, after = {2}, final = {3}, Remove {0}", value.FileName, countBefore, countAfter, countFinal));
    }

}
