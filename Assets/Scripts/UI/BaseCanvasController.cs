using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public class BaseCanvasController : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected BaseCanvasController ParentController;

        public void SetParentController(BaseCanvasController parent)
        {
            ParentController = parent;
        }

        public virtual void ShowGameObject(bool show)
        {
            gameObject.SetActive(show);
        }

        public virtual List<ConfigClass.KeyValue> AddButton()
        {
            return null;
        }
        public virtual List<ConfigClass.KeyValue> RemoveButton(string value)
        {
            return null;
        }
        public virtual void ButtonListClicked(string value) { }
        public virtual List<ConfigClass.KeyValue> InitButtons()
        {
            return null;
        }

        protected void BindFileController(FileSystemCanvasController fileController, UnityAction action)
        {
            fileController.OnFileAction(action);
            fileController.OnCancelAction(CancelFileAction);
            fileController.ShowGameObject(true);
            ShowGameObject(false);
        }

        protected virtual void CancelFileAction()
        {
            ShowGameObject(true);
        }
    }
}
