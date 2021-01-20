using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 保存UI
    Dictionary<string, GameObject> m_UI = new Dictionary<string, GameObject>();
    GameObject Canvas;
    public void OpenUI(string uiName,string luaName)
    {
        Canvas = GameObject.Find("Canvas");
        // Awake的模拟
        // 要打开一个UI时先判断UI池中是否存在这个UI，如果有直接打开，如果没有先创建
        GameObject go = null;
        if(m_UI.TryGetValue(uiName,out go))
        {
            UILogic uiLogic = go.GetComponent<UILogic>();
            uiLogic.OnOpen();           // Start
            return;
        }
        Manager.Resource.LoadUI(uiName, (UnityEngine.Object obj) =>
         {
             go = Instantiate(obj) as GameObject;
             m_UI.Add(uiName,go);
             UILogic uiLogic = go.AddComponent<UILogic>();
             go.transform.SetParent(Canvas.transform);
             go.transform.localPosition = Vector3.zero;
             uiLogic.Init(luaName);     
             uiLogic.OnOpen();          // Start
         });
    }
}
