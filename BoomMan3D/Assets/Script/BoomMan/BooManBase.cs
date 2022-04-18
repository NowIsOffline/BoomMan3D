using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using static ConstantsSpace.PlayerConstants;
using BoomResource;

namespace BoomMan
{
    class NeedCreateBomb
    {
        public Vector3 createPos { get; }

        public NeedCreateBomb(Vector3 createPos)
        {
            this.createPos = createPos;
        }
    }
    class BoomManBase : MonoBehaviour
    {
        protected UnityEngine.Object prefabs_Bomb;
        protected GameObject _playerLayer;
        protected GameObject _boomLayer;
        private float maxExistNum = 1;
        protected GameObject _initPos;
        protected Rigidbody playerRigidBody;
        protected int type = 1;
        void Start()
        {
            prefabs_Bomb = PrefabsResource.Instance.LoadResource(Constants.BOMB_PREFAB_PATH);
            initData();
            AddToStage();
        }

        protected float nowBoomNum()
        {
            int count = 0;
            for (int i = 0; i < this._boomLayer.transform.childCount; i++)
            {
                GameObject bomb = this._boomLayer.transform.GetChild(i).gameObject;
                if (bomb.name.IndexOf("Bomb_"+type) != -1)
                {
                    count++;
                }
            }
            return count;
        }

        protected virtual void initData()
        {
        }

        protected virtual void AddToStage()
        {
            transform.SetParent(this._playerLayer.transform);
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 90,
             transform.rotation.z);
            initPlayerPos();
            playerRigidBody = GetComponent<Rigidbody>();
            playerRigidBody.freezeRotation = true;//静止碰撞旋转
        }

        void initPlayerPos()
        {
            this.gameObject.transform.position = new Vector3(this._initPos.transform.position.x,
                this._initPos.transform.position.y, this._initPos.transform.position.z);
        }
        void FixedUpdate()
        {
            this.MoveBoomMan();
            this.checkCreateBoom();
        }
        protected virtual void MoveBoomMan()
        {
        }
        protected void startBoom()
        {
            Vector3 startPos = this.transform.position;
            startPos.x = (float)Math.Round(startPos.x);
            startPos.y = (float)Math.Round(startPos.y);
            startPos.z = (float)Math.Round(startPos.z);
            GameObject Bomb = (GameObject)Instantiate(prefabs_Bomb, startPos,
                     Quaternion.identity);
            Bomb.transform.SetParent(this._boomLayer.transform);
            Bomb.name = "Bomb_" + type;
        }
        protected virtual void checkCreateBoom()
        {

        }

        protected bool IsMaxBoomNum()
        {
            return nowBoomNum() >= maxExistNum;
        }


    }
}