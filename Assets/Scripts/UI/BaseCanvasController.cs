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

        public virtual ObjectItemList AddButton()
        {
            return null;
        }
        public virtual ObjectItemList RemoveButton(string value)
        {
            return null;
        }
        public virtual void ButtonListClicked(string value) { }
        public virtual ObjectItemList InitButtons()
        {
            return null;
        }

        public virtual List<T> GetItems<T>()
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
