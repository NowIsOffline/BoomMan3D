using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using Player;
using BoomResource;

public class Main : MonoBehaviour
{
    // Use this for initialization
          private GameObject player;
    void Awake()
    {
    }
    void Start()
    {
      player = (GameObject)Instantiate( PrefabsResource.Instance.LoadResource(Constants.PLAYER_PREFAB_PATH), new Vector3(0f, 0f, 0f),
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
