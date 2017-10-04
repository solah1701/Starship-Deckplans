using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class ConfigClass
    {
        public class KeyValue
        {
            public string Key;
            public string Value;
        }

        public enum FileType
        {
            Ship,
            Blueprint
        }

        public readonly List<KeyValue> FavouriteShipList;
        public string CurrentShip;
        public string StartingShipFile { get; set; }
        public readonly List<KeyValue> FavouriteBlueprintList;
        public string CurrentBlueprint;

        private readonly Dictionary<FileType, List<KeyValue>> _currentListDictionary;
        private readonly Dictionary<FileType, Action<string>> _setCurrentPathDictionary;
        private readonly Dictionary<FileType, Func<string>> _getCurrentPathDictionary;
        //private readonly Dictionary<FileType, Action<string>> _setCurrentFileDictionary;
        //private readonly Dictionary<FileType, Func<string>> _getCurrentFileDictionary;
        private readonly Dictionary<FileType, Action<string, string>> _addFavouriteItemDictionary;
        private readonly Dictionary<FileType, Action<string>> _removeFavouriteItemDictionary;

        public ConfigClass()
        {
            FavouriteBlueprintList = new List<KeyValue>();
            FavouriteShipList = new List<KeyValue>();

            _currentListDictionary = new Dictionary<FileType, List<KeyValue>>
            {
                {FileType.Blueprint, FavouriteBlueprintList},
                {FileType.Ship, FavouriteShipList}
            };
            _setCurrentPathDictionary = new Dictionary<FileType, Action<string>>
            {
                {FileType.Blueprint,  SetCurrentBlueprint},
                {FileType.Ship, SetCurrentShip }
            };
            _getCurrentPathDictionary = new Dictionary<FileType, Func<string>>
            {
                {FileType.Blueprint, GetCurrentBlueprint},
                {FileType.Ship, GetCurrentShip}
            };
            //_setCurrentFileDictionary = new Dictionary<FileType, Action<string>>
            //{
            //    {FileType.Blueprint,  SetCurrentBlueprintFile},
            //    {FileType.Ship, SetCurrentShipFile }
            //};
            //_getCurrentFileDictionary = new Dictionary<FileType, Func<string>>
            //{
            //    {FileType.Blueprint, GetCurrentBlueprintFile },
            //    {FileType.Ship, GetCurrentShipFile }
            //};
            _addFavouriteItemDictionary = new Dictionary<FileType, Action<string, string>>
            {
                {FileType.Blueprint, AddFavouriteBlueprint},
                {FileType.Ship, AddFavouriteShip}
            };
            _removeFavouriteItemDictionary = new Dictionary<FileType, Action<string>>
            {
                {FileType.Blueprint, RemoveFavouriteBlueprint},
                {FileType.Ship, RemoveFavouriteShip}
            };
        }

        public string GetCurrentPath(FileType fileType)
        {
            return _getCurrentPathDictionary[fileType].Invoke();
        }

        public void SetCurrentPath(FileType fileType, string value)
        {
            _setCurrentPathDictionary[fileType].Invoke(value);
        }

        //public string GetStartingFile(FileType fileType)
        //{
        //    return _getCurrentFileDictionary[fileType].Invoke();
        //}

        //public void SetCurrentFile(FileType fileType, string value)
        //{
        //    _setCurrentFileDictionary[fileType].Invoke(value);
        //}

        public List<KeyValue> GetCurrentList(FileType fileType)
        {
            return _currentListDictionary[fileType];
        }

        public void AddFavouriteItem(FileType fileType, string name, string fullname)
        {
            _addFavouriteItemDictionary[fileType].Invoke(name, fullname);
        }

        public void RemoveFavouriteItem(FileType fileType, string name)
        {
            _removeFavouriteItemDictionary[fileType].Invoke(name);
        }

        private void SetCurrentShip(string value)
        {
            CurrentShip = value;
        }

        private void SetCurrentBlueprint(string value)
        {
            CurrentBlueprint = value;
        }

        private string GetCurrentShip()
        {
            return CurrentShip;
        }

        private string GetCurrentBlueprint()
        {
            return CurrentBlueprint;
        }

        //private void SetCurrentShipFile(string value)
        //{
        //    StartingShipFile = value;
        //}

        //private void SetCurrentBlueprintFile(string value)
        //{
        //    CurrentBlueprintFile = value;
        //}

        //private string GetCurrentShipFile()
        //{
        //    return StartingShipFile;
        //}

        //private string GetCurrentBlueprintFile()
        //{
        //    return CurrentBlueprintFile;
        //}

        private void AddFavouriteShip(string name, string fullname)
        {
            AddFavourite(FavouriteShipList, name, fullname);
        }

        private void RemoveFavouriteShip(string name)
        {
            RemoveFavourite(FavouriteShipList, name);
        }

        private void AddFavouriteBlueprint(string name, string fullname)
        {
            AddFavourite(FavouriteBlueprintList, name, fullname);
        }

        private void RemoveFavouriteBlueprint(string name)
        {
            RemoveFavourite(FavouriteBlueprintList, name);
        }

        private void AddFavourite(List<KeyValue> list, string name, string fullname)
        {
            if (list.Find(x => x.Key == name) != null) return;
            list.Add(new KeyValue { Key = name, Value = fullname });
        }

        private void RemoveFavourite(List<KeyValue> list, string name)
        {
            var item = list.Find(x => x.Key == name);
            if (item == null) return;
            list.Remove(item);
        }
    }
}
