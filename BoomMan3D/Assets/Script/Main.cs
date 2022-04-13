using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using Player;
public class Main : MonoBehaviour
{
    // Use this for initialization
    private PlayerController playerController;
    void Awake()
    {
    }
    void Start()
    {
        this.playerController = new PlayerController();
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
