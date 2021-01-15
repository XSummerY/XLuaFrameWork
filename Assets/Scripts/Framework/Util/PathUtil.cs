using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    // RootDirectory
    public static readonly string AssetsPath = Application.dataPath;

    // The bundle directory
    public static readonly string BuildResourcesPath = AssetsPath + "/BuildResources/"; //take care of the last '/'

    // The output directory
    public static readonly string BundleOutPath = Application.streamingAssetsPath;

    public static string GetUnityPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }
        return path.Substring(path.IndexOf("Assets"));
    }

    /// <summary>
    /// Replace the "\\" of the path to "/"
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetStandardPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }
        return path.Trim().Replace("\\", "/");
    }
}
