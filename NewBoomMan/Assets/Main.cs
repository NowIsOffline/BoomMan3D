using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaState lua = new LuaState();
        lua.Start();
        lua.DoString("print('hello world')");
        lua.Dispose();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
