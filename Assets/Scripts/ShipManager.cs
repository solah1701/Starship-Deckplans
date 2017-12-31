﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Extenders;
using UnityEngine;
using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine.Events;

public class ShipManager : MonoBehaviour
{

    public FileSystemCanvasController LoadShipController;
    public FileSystemCanvasController SaveShipController;
    //public FileSystemCanvasController LoadBlueprintController;
    public ModalDialogCanvasController DialogBox;
    public ScreenManager ScreenManager;

    //public GameObject CylinderPrefab;
    //public GameObject CubePrefab;

    //public UnityEvent OnDeckChanged;

    private Ship _ship;
    //private Deck _currentDeck;
    private bool _isSaving;
	//private string _currentMeshType;

    //public int DeckCount
    //{
    //    get { return _ship.Decks.Count; }
    //}

    void Start()
    {
        //if (OnDeckChanged == null)
        //    OnDeckChanged = new UnityEvent();
        Init();
    }

    public void Init()
    {
        if (_ship == null) PopulateShip(LoadShipController.GetStartingFile());
    }

    public Ship GetShip()
    {
        return _ship;
    }

    public void Load()
    {
        BindFileController(LoadShipController, LoadShipAction);
        Debug.Log("Load screen");
    }

    public void Save()
    {
        var startingFile = SaveShipController.GetStartingFile();
        var fileInfo = new FileInfo(startingFile);
        SaveShipController.FileText.text = fileInfo.Name;

        BindFileController(SaveShipController, SaveShipAction);
        Debug.Log("Save screen");
    }

    public void New()
    {
        DialogBox.Choice("Creating a new ship will lose any unsaved changes. Do you wish to Save?", DoYesAction,
            DoNoAction, DoCancelAction);
    }

    void LoadShipAction()
    {
        var filepath = FileHelper.GetFilePath(LoadShipController.PathText.text, LoadShipController.FileText.text);
        PopulateShip(filepath);
        ScreenManager.OpenPanel(ScreenManager.initiallyOpen);
        Debug.Log("Load Ship");
    }

    //void LoadBlueprintAction()
    //{
    //    var filepath = LoadBlueprintController.PathText.text;
    //    var item = new Blueprint {FileName = LoadBlueprintController.FileText.text, FilePath = filepath};
    //    if (!_currentDeck.Blueprints.Contains(item))
    //        _currentDeck.Blueprints.Add(item);
    //    ScreenManager.OpenPanel(ScreenManager.previouslyOpen);
    //}

    void PopulateShip(string filepath)
    {
        _ship = FileHelper.Load<Ship>(filepath);
    }

    void SaveShipAction()
    {
        var filepath = FileHelper.GetFilePath(SaveShipController.PathText.text, SaveShipController.FileText.text);
        FileHelper.Save(filepath, _ship);
        if (_isSaving) DoNoAction();
		ScreenManager.OpenPanel (ScreenManager.previouslyOpen);
        Debug.Log("Save Ship");
    }

    void DoYesAction()
    {
        BindFileController(SaveShipController, SaveShipAction);
        _isSaving = true;
    }

    void DoNoAction()
    {
        _ship = new Ship();
    }

    void DoCancelAction()
    {
        //Debug.Log("Cancel");
    }

    public void BindFileController(FileSystemCanvasController fileController, UnityAction action)
    {
        fileController.OnFileAction(action);
        fileController.OnCancelAction(CancelFileAction);
        ScreenManager.OpenPanel(fileController.FileSystemAnimator);
    }

    protected virtual void CancelFileAction()
    {
        ScreenManager.OpenPanel(ScreenManager.initiallyOpen);
    }

    public void UpdateShipName(string value)
    {
        if (_ship == null || value == "") return;
        _ship.ShipName = value;
    }

    public string GetShipName()
    {
        return _ship == null ? "" : _ship.ShipName;
    }

    #region DeckManager
    //  public ObjectItemList GetDecks()
    //  {
    //      return _ship == null
    //          ? new ObjectItemList()
    //          : _ship.Decks.Select(
    //              deck => new ButtonItem {Item = new ConfigClass.KeyValue {Key = deck.DeckName, Value = deck.DeckName}})
    //              .Cast<ObjectItem>()
    //              .ConvertList();
    //  }

    //  public ObjectItemList RemoveDeck()
    //  {
    //var index = _ship.Decks.FindIndex (deck => deck.DeckName == _currentDeck.DeckName);
    //      _ship.Decks.Remove(_currentDeck);
    //if (_ship.Decks.Count <= index)
    //	_currentDeck = _ship.Decks.Last();
    //else
    //	_currentDeck = _ship.Decks [index];
    //OnDeckChanged.Invoke();
    //      return GetDecks();
    //  }

    //  public void AddDeck(int i)
    //  {
    //      while (true)
    //      {
    //          var deckName = string.Format("Deck {0}", i++);
    //          if (_ship.Decks.Exists(d => d.DeckName == deckName))
    //              continue;
    //          _ship.Decks.Add(new Deck {DeckName = deckName});
    //	GetDeck (deckName);
    //	OnDeckChanged.Invoke();
    //          return;
    //      }
    //  }

    //  public Deck GetDeck(string value)
    //  {
    //      _currentDeck = _ship.Decks.Find(deck => deck.DeckName == value);
    //      return _currentDeck;
    //  }

    //  public void SelectDeck(string value, Animator panel)
    //  {
    //      GetDeck(value);
    //      ScreenManager.OpenPanel(panel);
    //      OnDeckChanged.Invoke();
    //  }

    //  public string GetDeckName()
    //  {
    //      if (_currentDeck == null)
    //          return "";
    //      return _currentDeck.DeckName;
    //  }

    //  public void UpdateDeckName(string value)
    //  {
    //      if (_currentDeck == null || value == "")
    //          return;
    //      _currentDeck.DeckName = value;
    //OnDeckChanged.Invoke();
    //  }
    #endregion

    #region BlueprintManager
    //  public IEnumerable<Blueprint> GetBlueprintList()
    //  {
    //if (_currentDeck == null)
    //	return new List<Blueprint> ();
    //      return _currentDeck.Blueprints;
    //  }

    //  public IEnumerable<Blueprint> AddBlueprint()
    //  {
    //      BindFileController(LoadBlueprintController, LoadBlueprintAction);
    //      return GetBlueprintList();
    //  }

    //  public IEnumerable<Blueprint> RemoveBlueprint(Blueprint item)
    //  {
    //      if (_currentDeck.Blueprints.Contains(item))
    //          _currentDeck.Blueprints.Remove(item);
    //      return GetBlueprintList();
    //  }
    #endregion

    #region ObjectModelManager
 //   public void SetMeshType(string value)
	//{
	//	_currentMeshType = value;
	//}

 //   public GameObject AddModel()
 //   {
 //       if (_currentMeshType == null) return null;
 //       var meshName = _currentDeck.CreateMeshPathName();
 //       var path = "Assets/Meshes/" + meshName + ".asset";

 //       GameObject mesh;
 //       if (_currentMeshType == "Cylinder") mesh = Instantiate(CylinderPrefab);
 //       else
 //       //if (_currentMeshType == "Cuboid")
 //           mesh = Instantiate(CubePrefab);
 //       //gameObject.AddComponent<MeshFilter>();
 //       //gameObject.AddComponent<MeshRenderer>();
 //       //GetComponent<MeshRenderer>().material.color = Color.white;
 //       mesh.name = meshName;

 //       //TODO: Asset creation should be managed when the json file is being saved, otherwise we will be left
 //       //with a whole bunch of zombie assets which are not bound to anything
 //       var asset = mesh.GetComponent<MeshFilter>().mesh;
 //       AssetDatabase.CreateAsset(asset, path);
 //       AssetDatabase.SaveAssets();
 //       return mesh;
 //   }

 //   public List<GameObject> ShowVerticesAsSpheres(Vector3[] vertices)
 //   {
 //       var spheres = new List<GameObject>();
 //       var threshold = 0.1f;
 //       foreach (var vertex in vertices)
 //       {
 //           if (spheres.Any(sph => (sph.transform.position - vertex).magnitude < threshold)) continue;
 //           var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
 //           sphere.transform.position = vertex;
 //           sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
 //           sphere.tag = "vertexHelper";
 //           var rend = sphere.GetComponent<Renderer>();
 //           rend.material.color = Color.red;

 //           spheres.Add(sphere);
 //       }
 //       return spheres;
 //   }

 //   public void RemoveModel()
 //   {
        
 //   }

 //   public void SelectMesh(string value)
 //   {
        
 //   }
    #endregion

}
