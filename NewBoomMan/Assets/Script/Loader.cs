using System.Collections.Generic;
using UnityEngine;

public class Loader
{
    private Dictionary<string, GameObject> LoaderList = new Dictionary<string, GameObject>();
    private static Loader _instatnce = null;
    public static Loader GetInstance()
    {
        if (_instatnce == null)
        {
            _instatnce = new Loader();
        }
        return _instatnce;
    }

    public GameObject LoadPrefabs(string path)
    {
        if (LoaderList.ContainsKey(path) == false)
        {
            LoaderList[path] = Resources.Load(path) as GameObject;
        }
        return LoaderList[path];
    }
}