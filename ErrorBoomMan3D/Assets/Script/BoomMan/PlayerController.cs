using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using static ConstantsSpace.PlayerConstants;
using ResourceLoader;
namespace BoomMan
{
    class PlayerController : BoomManBase
    {
        protected override void initData()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this.name = "Player";
            this._bombLayer = GameObject.Find("BombLayer");
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

        protected override bool checkCreateBoom()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!this.IsMaxBoomNum())
                {
                    return true;
                }

            }
            return false;
        }
    }
}