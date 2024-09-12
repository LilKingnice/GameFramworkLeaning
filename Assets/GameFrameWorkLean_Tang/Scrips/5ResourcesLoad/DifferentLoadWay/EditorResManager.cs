using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor ResourcesManager
/// The ResourceManager can only be utilized for loading resources during development. 
/// This functionality won't take effect post-release, as files with in the 
/// Editor folder are not included in the final built project.
/// </summary>
public class EditorResManager : BaseSingletonWithoutMono<EditorResManager>
{
    private EditorResManager()
    {
        
    }

    string rootPath = "Assets/Editor/Arts/";

    //Load single resource
    public T LoadEditorRes<T>(string path) where T : Object
    {
#if UNITY_EDITOR
        //Suffixes: Prefab, Texture (Image), Material, Audio, etc.
        // string suffixName = "";
        // if (typeof(T) == typeof(GameObject))
        //     suffixName = ".prefab";
        // else if (typeof(T) == typeof(Material))
        //     suffixName = ".mat";
        // else if (typeof(T) == typeof(Texture))
        //     suffixName = ".png";
        // else if (typeof(T) == typeof(AudioClip))
        //     suffixName = ".mp3";
        // T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path+suffixName);

        T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path);
        return res;
#else
        return null;
#endif
    }

    //Load resources associated with the texture atlas.
    public Sprite LoadSprite(string path, string spriteName)
    {
#if UNITY_EDITOR
        //Load all sub-resources from the texture atlas.
        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);

        foreach (var item in sprites)
        {
            if (item.name == spriteName)
            {
                return item as Sprite;
            }
        }
        return null;
#else
        return null;
#endif
    }


    /// <summary>
    /// Load all sub-images from the texture atlas file and return them to the external caller.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Dictionary<string, Sprite> LoadSprites(string path)
    {
#if UNITY_EDITOR
        Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
        foreach (var item in sprites)
        {
            spriteDic.Add(item.name, item as Sprite);
        }
        return spriteDic;

#else
        return null;
#endif
    }
}
