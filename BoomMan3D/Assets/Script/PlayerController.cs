using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantsSpace.PlayerConstants;
namespace Player
{
    class NeedCreateBomb
    {
        public Vector3 createPos { get; }
     
        public NeedCreateBomb(Vector3 createPos)
        {
            this.createPos = createPos;
        }
    }
    class PlayerController : MonoBehaviour
    {
        private const float CREATE_BOMB_SEC = 0.5f;
        private GameObject _playerLayer;
        private GameObject _boomLayer;

        private GameObject _initPos;

        private List<NeedCreateBomb> _bombArray;
        private Rigidbody playerRigidBody;
        void Start()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this._boomLayer = GameObject.Find("BombLayer");
            this._initPos = GameObject.Find("PlayerInitPos_0");
            this._bombArray = new List<NeedCreateBomb>();

            transform.SetParent(this._playerLayer.transform);
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 90,
             transform.rotation.z);
            initPlayerPos();
            playerRigidBody = GetComponent<Rigidbody>();
            playerRigidBody.freezeRotation = true;//静止碰撞旋转
            InvokeRepeating("createBomb", .5f, .5f);
        }
        void createBomb()
        {
            if (this._bombArray!=null&&this._bombArray.Count > 0)
            {
                var startPos = this._bombArray[0].createPos;
                this._bombArray.RemoveAt(0);
    GameObject Bomb = (GameObject)Instantiate(Resources.Load("Prefabs/Bomb"), startPos,
               Quaternion.identity);
               Bomb.transform.SetParent(this._boomLayer.transform);
            }
        }

        void initPlayerPos()
        {
            this.gameObject.transform.position = new Vector3(this._initPos.transform.position.x,
                this._initPos.transform.position.y, this._initPos.transform.position.z);
        }

        void MovePlayer()
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



        void FixedUpdate()
        {
            this.MovePlayer();
            if (Input.GetKey(KeyCode.Space))
            {
                int bombArrLength = this._bombArray.Count;
                if (bombArrLength >= 2)
                {
                    this._bombArray.RemoveAt(0);
                }
                Vector3 startPos = this.transform.position;
                startPos.x = (float)Math.Round(startPos.x);
                startPos.y = (float)Math.Round(startPos.y);
                startPos.z = (float)Math.Round(startPos.z);
                this._bombArray.Add(new NeedCreateBomb(startPos));
            }
        }
    }
}