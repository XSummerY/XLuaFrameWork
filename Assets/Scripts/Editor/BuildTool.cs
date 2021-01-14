﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildTool : Editor
{
    [MenuItem("Tools/BuildBundleForWindows")]
    static void BundleBuildWindows()
    {
        Build(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Tools/BuildBundleForAndroid")]
    static void BundleBuildAndroid()
    {
        Build(BuildTarget.Android);
    }
    [MenuItem("Tools/BuildBundleForiOS")]
    static void BundleBuildiOS()
    {
        Build(BuildTarget.iOS);
    }
    static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath, "*", SearchOption.AllDirectories);

        for(int i = 0; i < files.Length; ++i)
        {
            if (files[i].EndsWith(".meta"))
                continue;
            string fileName = PathUtil.GetStandardPath(files[i]);
            Debug.Log("files:" + fileName);
            string assetName = PathUtil.GetUnityPath(fileName);
            
            AssetBundleBuild assetBundle = new AssetBundleBuild();
            assetBundle.assetNames = new string[] { assetName };
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath, "").ToLower();
            assetBundle.assetBundleName = bundleName + ".ab";
            assetBundleBuilds.Add(assetBundle);
        }
        if (Directory.Exists(PathUtil.BundleOutPath))
        {
            Directory.Delete(PathUtil.BundleOutPath, true);
        }
        Directory.CreateDirectory(PathUtil.BundleOutPath);
        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);
    }
}
