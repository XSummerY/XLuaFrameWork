using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
public class HotUpdate : MonoBehaviour
{
    internal class DownFileInfo
    {
        public string url;
        public string fileName;
        public DownloadHandler fileData;
    }
    /// <summary>
    /// 下载单个文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IEnumerator DownloadFile(DownFileInfo fileInfo, Action<DownFileInfo> Complete)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(fileInfo.url);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.LogError("下载文件出错: " + fileInfo.url);
            yield break;
            // 重试逻辑
            // DoSomething...
        }
        fileInfo.fileData = webRequest.downloadHandler;
        Complete?.Invoke(fileInfo);
        webRequest.Dispose();
    }

    IEnumerator DownloadFiles(List<DownFileInfo> fileInfos, Action<DownFileInfo> Complete, Action AllDownloadComplete)
    {
        foreach(DownFileInfo info in fileInfos)
        {
            yield return DownloadFile(info, Complete);
        }
        AllDownloadComplete?.Invoke();
    }

    private List<DownFileInfo> GetFileInfos(string fileData,string path)
    {
        string content = fileData.Trim().Replace("\r", "");
        string[] files = content.Split('\n');
        List<DownFileInfo> downloadFileInfos = new List<DownFileInfo>(files.Length);
        for(int i = 0; i < files.Length; ++i)
        {
            string[] info = files[i].Split('|');
            DownFileInfo fileInfo = new DownFileInfo();
            fileInfo.fileName = info[1];
            fileInfo.url = Path.Combine(path, info[1]);
            downloadFileInfos.Add(fileInfo);
        }
        return downloadFileInfos;
    }
}
