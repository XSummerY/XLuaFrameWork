using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UObject = UnityEngine.Object;

public class ResourceManager : MonoBehaviour
{
    internal class BundleInfo
    {
        public string AssetsName;
        public string BundleName;
        public List<string> Dependences;
    }
    // BundleInfo 的集合
    private Dictionary<string, BundleInfo> m_BundleInfos = new Dictionary<string, BundleInfo>();


    
    /// <summary>
    /// 解析版本文件
    /// </summary>
    public void ParseVersionFile()
    {
        // 获得版本文件的路径
        string url = Path.Combine(PathUtil.BundleResourcePath, AppConst.FileListName);
        string[] data = File.ReadAllLines(url);
        // 解析filelist中的文件信息
        for(int i = 0; i < data.Length; ++i)
        {
            BundleInfo bundleInfo = new BundleInfo();
            string[] info = data[i].Split('|');
            bundleInfo.AssetsName = info[0];
            bundleInfo.BundleName = info[1];
            bundleInfo.Dependences = new List<string>(info.Length - 2);
            for(int j = 2; j < info.Length; ++j)
            {
                bundleInfo.Dependences.Add(info[j]);
            }
            m_BundleInfos.Add(bundleInfo.AssetsName, bundleInfo);
        }
    }

    IEnumerator LoadBundleAsync(string assetName,Action<UObject> action=null)
    {
        
        if (m_BundleInfos.ContainsKey(assetName))
        {
            string bundleName = m_BundleInfos[assetName].BundleName;
            string bundlePath = Path.Combine(PathUtil.BundleResourcePath, bundleName);
            List<string> dependences = m_BundleInfos[assetName].Dependences;
            if (dependences != null && dependences.Count > 0)
            {
                for (int i = 0; i < dependences.Count; ++i)
                {
                    yield return LoadBundleAsync(dependences[i]);
                }
            }

            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return request;

            AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync(assetName);
            yield return bundleRequest;

            /*if (action != null && bundleRequest != null)
            {
                action.Invoke(bundleRequest.asset);
            }*/
            action?.Invoke(bundleRequest?.asset);
        }
        else
        {
            Debug.LogError("缺少文件: "+assetName);
        }
    }
    /// <summary>
    /// 编辑器环境下加载资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="action"></param>
    void EditorLoadAsset(string assetName, Action<UObject> action = null)
    {
#if UNITY_EDITOR
        UObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath(assetName, typeof(UObject));
        if (obj == null)
        {
            Debug.LogError("assets name is not exist: " + assetName);
        }
        action?.Invoke(obj);
#endif
    }

    private void LoadAsset(string assetName,Action<UObject> action)
    {
        if (AppConst.GameMode == GameMode.EditorMode)
        {
            Debug.Log("EditorMode");
            EditorLoadAsset(assetName, action);
        }
        else
        {
            Debug.Log("PackageMode");
            StartCoroutine(LoadBundleAsync(assetName, action));
        }
        
    }

    public void LoadUI(string assetName,Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetUIPath(assetName), action);
    }
    public void LoadSound(string assetName, Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetSoundPath(assetName), action);
    }
    public void LoadMusic(string assetName, Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetMusicPath(assetName), action);
    }
    public void LoadScene(string assetName, Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetScenePath(assetName), action);
    }
    public void LoadSprite(string assetName, Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetSpritePath(assetName), action);
    }
    public void LoadEffect(string assetName, Action<UObject> action = null)
    {
        this.LoadAsset(PathUtil.GetEffectPath(assetName), action);
    }
}
