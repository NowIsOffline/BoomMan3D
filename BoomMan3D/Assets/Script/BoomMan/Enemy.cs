using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using ResourceLoader;
using Pathfinding;

namespace BoomMan
{
    class Enemy : BoomManBase
    {

        private Seeker seeker;
        private CharacterController controller;
        private GameObject player;
        private Vector3 nextSafePos = new Vector3(-1, -1, -1);
        protected override void initData()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this._boomLayer = GameObject.Find("BombLayer");
            this._initPos = GameObject.Find("PlayerInitPos_1");
            seeker = GetComponent<Seeker>();
            controller = GetComponent<CharacterController>();
            this.type = 2;

            //Start a new path to the targetPosition, return the result to the OnPathComplete function

        }
        protected override void MoveBoomMan()
        {
            //遍历所有没有爆炸的炸弹，保存所有炸弹范围路径
            //遍历所有火焰，保存火焰路径
            //实现广度搜索最近的安全点只要不在上面地方
            // this.startPath();
        }

        private void startPath()
        {
            if (player == null)
            {
                this.player = GameObject.Find("player");
            }
            seeker.StartPath(transform.position, GetNextPos(), OnPathComplete);
            this.nextSafePos.y = -1f;
        }

        private Vector3 GetNextPos()
        {
            return player.gameObject.transform.position;
        }

        private void OnPathComplete(Path p)
        {
            checkCreateBoom();
        }

        protected override void checkCreateBoom()
        {
            //放置炸弹前先广度搜索安全点，没有就不放置（需要将炸弹爆炸范围算入）
            if (this.IsMaxBoomNum())
            {
                return;
            }
            this.startBoom();
        }


    }
}