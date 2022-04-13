using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using Player;
public class Main : MonoBehaviour
{
    // Use this for initialization
    private GameObject map;
    private PlayerController playerController;
    void Awake()
    {
    }
    void Start()
    {
        map = GameObject.Find("Map");
        this.playerController = new PlayerController(this.map, GameObject.Find("PlayerInitPos_0"));
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        this.playerController.MovePlayer();
    }
}
