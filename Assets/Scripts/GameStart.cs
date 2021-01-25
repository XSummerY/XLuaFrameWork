using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameMode GameMode;
    void Awake()
    {
        Manager.Event.Subscribe(1, OnLuaInit);
        AppConst.GameMode = this.GameMode;
        Manager.Resource.ParseVersionFile();
        /*Manager.Lua.Init(
            () =>
            {

                Manager.Lua.RunLua("test");
            }
            );*/
        Manager.Lua.Init();
        DontDestroyOnLoad(this);
    }

    void OnLuaInit(object args)
    {
        Manager.Lua.RunLua("test");
        Manager.Pool.CreateGameObjectPool("UI", 10);
        Manager.Pool.CreateGameObjectPool("Effect", 120);
        Manager.Pool.CreateAssetPool("AssetBundle", 10);
    }

    public void OnApplicationQuit()
    {
        Manager.Event.UnSubscribe(1, OnLuaInit);
    }
}
