using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 保存UI
    Dictionary<string, GameObject> m_UI = new Dictionary<string, GameObject>();
    // UI分组
    Dictionary<string, Transform> m_UIGroups = new Dictionary<string, Transform>();

    private Transform m_UIParent;

    private void Awake()
    {
        m_UIParent = this.transform.parent.Find("UI");
    }

    public void SetUIGroup(List<string> group)
    {
        for(int i = 0; i < group.Count; ++i)
        {
            GameObject go = new GameObject("Group-" + group[i]);
            go.transform.SetParent(m_UIParent, false);
            m_UIGroups.Add(group[i], go.transform);
        }
    }

    Transform GetUIGroup(string groupName)
    {
        if (!m_UIGroups.ContainsKey(groupName))
        {
            Debug.LogError("Group \"" + groupName + "\" is not exisit");
        }
        return m_UIGroups[groupName].transform;
    }

    public void OpenUI(string uiName,string groupName,string luaName)
    {
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
             Transform parent = GetUIGroup(groupName);
             go.transform.SetParent(parent,false);
             uiLogic.Init(luaName);     
             uiLogic.OnOpen();          // Start
         });
    }
}
