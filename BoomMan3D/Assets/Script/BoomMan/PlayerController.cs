using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using static ConstantsSpace.PlayerConstants;
using BoomResource;
namespace BoomMan
{
    class PlayerController : BoomManBase
    {
        protected override void initData()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this._boomLayer = GameObject.Find("BombLayer");
            this._initPos = GameObject.Find("PlayerInitPos_0");
        }
        protected override void MoveBoomMan()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            if (v != 0 || h != 0)
            {
                Transform towards = transform;
                Vector3 tempTowards = Vector3.forward;
                if (v != 0)
                {
                    playerRigidBody.MovePosition(transform.position + Vector3.forward * PLAYER_MOVE_SPEED * v);
                    tempTowards = v > 0 ? Vector3.forward : -Vector3.forward;
                }
                if (h != 0)
                {
                    playerRigidBody.MovePosition(transform.position + Vector3.right * PLAYER_MOVE_SPEED * h);
                    tempTowards = h > 0 ? Vector3.right : -Vector3.right;
                }
                Quaternion q = Quaternion.LookRotation(tempTowards);
                //平滑转向
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 10f * Time.deltaTime);
            }
        }

        protected override void checkCreateBoom()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (nowSec < CREATE_BOMB_SEC)
                {
                    return;
                }
                nowSec = 0f;
                Vector3 startPos = this.transform.position;
                startPos.x = (float)Math.Round(startPos.x);
                startPos.y = (float)Math.Round(startPos.y);
                startPos.z = (float)Math.Round(startPos.z);
                GameObject Bomb = (GameObject)Instantiate(prefabs_Bomb, startPos,
                         Quaternion.identity);
                Bomb.transform.SetParent(this._boomLayer.transform);
            }
        }
    }
}