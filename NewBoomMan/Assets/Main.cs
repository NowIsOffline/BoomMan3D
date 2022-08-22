using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    public string startScript = "Main.lua";
    public LuaState lua;
    LuaFunction luaFunc = null;
    void Start()
    {
        lua = new LuaState();
        lua.Start();
        lua.LuaSetTop(0);
        lua.AddSearchPath(Application.dataPath + "Lua");
        lua.DoFile(startScript);
        LuaTable module = lua.GetTable("BM.Main");
        LuaFunction main = module.GetLuaFunction("Main");
        main.Call();
        main.Dispose();
        main = null;
    }

    void Destroy()
    {
        lua = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
