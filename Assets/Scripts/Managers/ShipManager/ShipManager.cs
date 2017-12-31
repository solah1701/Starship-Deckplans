using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class ShipManager : MonoBehaviour
{

    public FileSystemCanvasController LoadShipController;
    public FileSystemCanvasController SaveShipController;
    public ModalDialogCanvasController DialogBox;
    public ScreenManager ScreenManager;
	public UnityEvent OnShipLoaded;

    private Ship _ship;
    private bool _isSaving;

    void Start()
    {
		if (OnShipLoaded == null)
			OnShipLoaded = new UnityEvent();
        Init();
    }

    public void Init()
    {
		if (_ship == null) 		this.StartCoroutine (DownloadShip (LoadShipController.GetStartingFile()));
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

    void PopulateShip(string filepath)
	{
		this.StartCoroutine (DownloadShip (filepath));
		//_ship = FileHelper.Load<Ship> (filepath);
		//OnShipLoaded.Invoke ();
	}

	public IEnumerator DownloadShip(string filepath){
		var result = new WWW (string.Format("file://{0}", filepath));
		yield return result;
		_ship = FileHelper.Convert<Ship> (result.text);
		OnShipLoaded.Invoke ();
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

}
