using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static ResourceManager _resource;
    public static ResourceManager Resource
    {
        get
        {
            return _resource;
        }
    }

    private static LuaManager _lua;
    public static LuaManager Lua
    {
        get
        {
            return _lua;
        }
    }

    private static UIManager _ui;
    public static UIManager UI
    {
        get
        {
            return _ui;
        }
    }

    private static EntityManager _entity;
    public static EntityManager Entity
    {
        get
        {
            return _entity;
        }
    }

    private static ScenesManager _scene;
    public static ScenesManager Scene
    {
        get
        {
            return _scene;
        }
    }
    private void Awake()
    {
        _resource = gameObject.AddComponent<ResourceManager>();
        _lua = gameObject.AddComponent<LuaManager>();
        _ui = gameObject.AddComponent<UIManager>();
        _entity = gameObject.AddComponent<EntityManager>();
        _scene = gameObject.AddComponent<ScenesManager>();
    }
}
