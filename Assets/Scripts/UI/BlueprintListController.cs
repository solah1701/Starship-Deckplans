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
    public RectTransform BlueprintPanel;
    public BlueprintManager BlueprintManager;

    private List<Blueprint> BlueprintNames;
    public Blueprint Current { get; set; }

    void Start()
    {
        InitItems();
    }

    private ObjectItemList Convert(IEnumerable<Blueprint> list)
    {
        return (list as IEnumerable<ObjectItem>).ConvertList();
    }

    public void InitItems()
    {
		BlueprintNames = BlueprintManager.GetBlueprintList() as List<Blueprint>;
        PopulateItems();
    }

    public void AddItems()
    {
		BlueprintNames = BlueprintManager.AddBlueprint() as List<Blueprint>;
        if (BlueprintNames == null) return;
		Current = BlueprintNames [BlueprintNames.Count - 1];
        PopulateItems();
    }

    public void RemoveItems()
    {
        if (Current == null) return;
		BlueprintNames = BlueprintManager.RemoveBlueprint(Current) as List<Blueprint>;
        PopulateItems();
        Current = null;
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
        ClearItems();
        if (BlueprintNames == null) return;
        foreach (var blueprintName in BlueprintNames)
        {
            CreatePrefab(blueprintName.Cast<Blueprint>(), index++);
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
        var ypos = -index * 100 - 60;
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
        Current = value;
    }

}
