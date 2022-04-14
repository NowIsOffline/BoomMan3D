using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using Player;
public class Main : MonoBehaviour
{
    // Use this for initialization
          private GameObject player;
    void Awake()
    {
    }
    void Start()
    {
      player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0f, 0f, 0f),
        Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
     
    }
}
