using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintListController : MonoBehaviour
{

    public GameObject PrefabItem;
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

    public void PopulateItems()
    {
        var index = 0;
        ClearItems();
        if (BlueprintNames == null) return;
        foreach (var blueprintName in BlueprintNames)
        {
            CreatePrefab(blueprintName, index++);
        }
    }

    void ClearItems()
    {
        var items = BlueprintPanel.GetComponentsInChildren<BlueprintItemController>();
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }

    void CreatePrefab(Blueprint item, int index)
    {
        var ypos = -index * 98;
        var itemPanel = Instantiate(PrefabItem);
        itemPanel.transform.SetParent(BlueprintPanel, false);
        itemPanel.transform.position += new Vector3(0, ypos, 0);
        var tempItemPanel = itemPanel.GetComponent<BlueprintItemController>();
        tempItemPanel.FileNameText.text = item.FileName;
        var tempButton = tempItemPanel.GetComponentInChildren<Button>();
        tempButton.onClick.AddListener(() => TheButtonClicked(item));
    }

    void TheButtonClicked(Blueprint value)
    {
        if (BlueprintNames.Contains(value)) BlueprintNames.Remove(value);
        PopulateItems();
        Debug.Log(string.Format("Remove {0}", value.FileName));
    }

}
