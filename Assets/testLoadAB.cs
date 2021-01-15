using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLoadAB : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/ui/perfab/testbutton.prefab.ab");
        yield return request;
        AssetBundleCreateRequest spriteRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/ui/sprite/pic001.jpg.ab");
        yield return spriteRequest;
        AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync("Assets/BuildResources/UI/Perfab/TestButton.prefab");

        GameObject go = Instantiate(bundleRequest.asset) as GameObject;
        go.transform.SetParent(this.transform);
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
