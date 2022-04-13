using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;
using static ConstantsSpace.PlayerConstants;
namespace Player
{
    class PlayerController
    {
        private GameObject _map;
        private GameObject _initPos;
        private GameObject player;
        private Rigidbody playerRigidBody;
        public PlayerController()
        {
            this._map = GameObject.Find("Map");
            this._initPos = GameObject.Find("PlayerInitPos_0");
            player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0f, 0f, 0f),
        Quaternion.identity);
            player.transform.SetParent(this._map.transform);
            player.transform.localEulerAngles = new Vector3(player.transform.rotation.x, player.transform.rotation.y + 90, player.transform.rotation.z);
            initPlayerPos();
            playerRigidBody = player.GetComponent<Rigidbody>();
            playerRigidBody.freezeRotation = true;//静止碰撞旋转

        }
        void initPlayerPos()
        {
            player.transform.position = new Vector3(this._initPos.transform.position.x,
                this._initPos.transform.position.y, this._initPos.transform.position.z);
        }

        public void MovePlayer()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            if (v != 0 || h != 0)
            {
                Transform towards = player.transform;
                Vector3 tempTowards = Vector3.forward;
                if (v != 0)
                {
                    playerRigidBody.MovePosition(player.transform.position + Vector3.forward * PLAYER_MOVE_SPEED * v);
                    tempTowards = v > 0 ? Vector3.forward : -Vector3.forward;
                }
                if (h != 0)
                {
                    playerRigidBody.MovePosition(player.transform.position + Vector3.right * PLAYER_MOVE_SPEED * h);
                    tempTowards = h > 0 ? Vector3.right : -Vector3.right;
                }
                Quaternion q = Quaternion.LookRotation(tempTowards);
                //平滑转向
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, q, 10f * Time.deltaTime);
            }
        }
    }
}