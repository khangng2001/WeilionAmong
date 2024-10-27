using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceLoader
{
    private static readonly Dictionary<string, Object> _adressableAssets = new();
    private static readonly Dictionary<string, Object> _resourceAssets = new();
    
    
    public static void LoadResourceByFolder<T>(params string[] paths) where T : Object
    {
        foreach (var path in paths)
        {
            var assets = Resources.LoadAll<T>(path);
            foreach (var asset in assets)
            {
                string key = Path.GetFileName(asset.name);
                _resourceAssets.Add(key, asset);
            }
        }
    }

    public static T Get<T>(string key) where T : Object
    {
        if (_adressableAssets.TryGetValue(key, out var adressableAsset))
            return adressableAsset as T;
        
        if (_resourceAssets.TryGetValue(key, out var asset))
            return asset as T;
        
        return null;
    }

    public static void ReleaseAll()
    {
        _resourceAssets.Clear();
    }
}