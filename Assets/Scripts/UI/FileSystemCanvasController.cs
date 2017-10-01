using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Assets.Scripts;
using Assets.Scripts.Models;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FileSystemCanvasController : BaseCanvasController
{
    public bool Save;
    public InputField PathText;
    public InputField FileText;
    public RectTransform FilePanel;
    public RectTransform FavouritePanel;
    public GameObject prefabButton;
    public ConfigClass.FileType FileTypeName;
    public string searchPattern;
    public Button ActionButton;
    public Text ActionButtonText;
    public Button CancelButton;

    private const string configFilename = "config.json";

    private ConfigClass config;


    public FileSystemCanvasController()
    {
        InitDictionaries();
    }

    void InitDictionaries()
    {
        if (!LoadConfig()) return;
    }

    // Use this for initialization
    void Start()
    {
        SetPath(config.GetCurrentPath(FileTypeName));
        ActionButtonText.text = Save ? "Save" : "Open";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool LoadConfig()
    {
        var filepath = FileHelper.GetCurrentFilePath(configFilename);
        config = FileHelper.Load<ConfigClass>(filepath);
        return config != null;
    }

    public string GetStartingFile()
    {
        return config.StartingShipFile;
    }

    public void OnFileAction(UnityAction action)
    {
        ActionButton.onClick.RemoveAllListeners();
        ActionButton.onClick.AddListener(action);
        ActionButton.onClick.AddListener(FileAction);
        Debug.Log("Action clicked");
    }

    public void OnCancelAction(UnityAction action)
    {
        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(action);
    }

    #region "Button List Methods:
    public override List<ConfigClass.KeyValue> InitButtons()
    {
        return !LoadConfig() ? null : config.GetCurrentList(FileTypeName);
    }

    public override List<ConfigClass.KeyValue> AddButton()
    {
        var directoryInfo = new DirectoryInfo(config.GetCurrentPath(FileTypeName));
        config.AddFavouriteItem(FileTypeName, directoryInfo.Name, directoryInfo.FullName);
        SaveConfig();
        return config.GetCurrentList(FileTypeName);
    }

    public override List<ConfigClass.KeyValue> RemoveButton(string value)
    {
        config.RemoveFavouriteItem(FileTypeName, value);
        //config.RemoveFavouriteBlueprint(value);
        SaveConfig();
        return config.GetCurrentList(FileTypeName);
    }
    public override void ButtonListClicked(string value)
    {
        SetPath(value);
    }
    #endregion

    public void SetPath()
    {
        try
        {
            PopulateFilePanel(PathText.text, FilePanel);
        }
        catch (DirectoryNotFoundException) { }
        catch (UnauthorizedAccessException)
        {
            // ignored
        }
    }

    private void SetPath(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        PathText.text = value;
    }

    private void PopulateFavourites(List<ConfigClass.KeyValue> values)
    {
        var index = 0;
        ClearButtons(FavouritePanel);
        foreach (var value in values)
        {
            CreateButton(FavouritePanel, value.Key, 0, 0, index++, FavouriteButtonClicked);
        }
    }

    public void SetFile()
    {
        //FileText.text = value;
    }

    private void SetFile(string value)
    {
        FileText.text = value;
    }

    public void OpenPath()
    {

    }

    public void OpenFile()
    {

    }

    public void AddFavourite()
    {
    }

    private void SaveConfig()
    {
        var filepath = FileHelper.GetCurrentFilePath(configFilename);
        FileHelper.Save(filepath, config);
    }

    private void FileAction()
    {
        config.StartingShipFile = FileHelper.GetFilePath(PathText.text, FileText.text);
        SaveConfig();
    }

    private void PopulateFilePanel(string path, RectTransform panel)
    {
        const int XOFFSET = 360;
        var directoryInfo = new DirectoryInfo(path);
        var directoryInfos = directoryInfo.GetDirectories();
        var root = directoryInfo.Parent == null;
        config.SetCurrentPath(FileTypeName, path);
        SaveConfig();

        int[] x = { 0 };
        int[] y = { 0 };
        var index = 0;
        ClearButtons(panel);
        if (!root)
        {
            CreateButton(panel, "..", x[0], y[0], index++, DirectoryButtonClicked);
        }
        foreach (var directory in directoryInfos.Where(directory => CreateButton(panel, directory.Name, x[0], y[0], index++, DirectoryButtonClicked)))
        {
            x[0] += XOFFSET;
            y[0] = index;
        }
        var fileInfos = directoryInfo.GetFiles(searchPattern);
        foreach (var fileInfo in fileInfos.Where(fileInfo => CreateButton(panel, fileInfo.Name, x[0], y[0], index++, FileButtonClicked)))
        {
            x[0] += XOFFSET;
            y[0] = index;
        }
    }

    private void ClearButtons(RectTransform panel)
    {
        var tempButtons = panel.GetComponentsInChildren<Button>();
        foreach (var tempButton in tempButtons)
        {
            Destroy(tempButton.gameObject);
        }
    }

    private bool CreateButton(RectTransform panel, string value, int x, int y, int index, ButtonClicked buttonClicked)
    {
        var xpos = x + 190;
        var ypos = -(index - y) * 30 - 30;
        var goButton = Instantiate(prefabButton);
        goButton.transform.SetParent(panel, false);
        goButton.transform.localScale = new Vector3(1, 1, 1);
        goButton.transform.position += new Vector3(xpos, ypos, 0);

        var tempButton = goButton.GetComponent<Button>();
        var buttonText = goButton.GetComponentInChildren<Text>();
        buttonText.text = string.Format(" {0}", value);

        tempButton.onClick.AddListener(() => buttonClicked(value));

        var height = FilePanel.rect.height - 45;

        return -ypos > height;
    }

    delegate void ButtonClicked(string value);

    private void DirectoryButtonClicked(string value)
    {
        string path;
        if (value == "..")
        {
            var directoryInfo = new DirectoryInfo(PathText.text).Parent;
            path = directoryInfo != null ? directoryInfo.FullName : PathText.text;
        }
        else
        {
            path = string.Format(@"{0}\{1}", PathText.text, value);
            var directoryInfo = new DirectoryInfo(path);
            path = directoryInfo.FullName;
        }
        SetPath(path);
    }

    private void FileButtonClicked(string value)
    {
        SetFile(value);
    }

    private void FavouriteButtonClicked(string value)
    {
        SetPath(config.GetCurrentList(FileTypeName).Find(x => x.Key == value).Value);
    }
}
