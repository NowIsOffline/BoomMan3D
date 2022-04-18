using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using ResourceLoader;

public class Main : MonoBehaviour
{
    // Use this for initialization
    private GameObject player;
    private GameObject enemy;
    void Awake()
    {
    }
    void Start()
    {
        player = (GameObject)Instantiate(PrefabsResource.Instance.LoadResource(Constants.PLAYER_PREFAB_PATH), new Vector3(0f, 0f, 0f),
          Quaternion.identity);
        enemy = (GameObject)Instantiate(PrefabsResource.Instance.LoadResource(Constants.ENEMY_PREFAB_PATH), new Vector3(0f, 0f, 0f),
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
