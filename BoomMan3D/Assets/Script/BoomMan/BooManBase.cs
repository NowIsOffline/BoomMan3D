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
        protected const float CREATE_BOMB_SEC = .1f;
        protected float nowSec = 0f;
        protected GameObject _initPos;
        protected Rigidbody playerRigidBody;
        void Start()
        {
            prefabs_Bomb = PrefabsResource.Instance.LoadResource(Constants.BOMB_PREFAB_PATH);
            initData();
            AddToStage();
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
            InvokeRepeating("startTime", .1f, .5f);
        }
        void startTime()
        {
            nowSec += .5f;
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

        protected virtual void checkCreateBoom()
        {

        }
    }
}