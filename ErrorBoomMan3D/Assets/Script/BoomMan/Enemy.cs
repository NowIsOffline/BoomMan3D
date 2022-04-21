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
        private GameObject _fireContain;
        private GameObject player;
        private bool _isNeedCreateBomb = false;
        private GameObject _canDestroyWallContain;
        private int[,] _mapArea = new int[23, 23];
        private Vector3 _nextPos = new Vector3(-1f, -1f, -1f);
        private bool _isMoving = false;
        protected override void initData()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this._bombLayer = GameObject.Find("BombLayer");
            this._initPos = GameObject.Find("PlayerInitPos_1");
            this._fireContain = GameObject.Find("FireContain");
            this._canDestroyWallContain = GameObject.Find("CanDestroyWallContain");
            seeker = GetComponent<Seeker>();
            controller = GetComponent<CharacterController>();
            this.type = 2;
        }

        protected override void AfterAddToStage()
        {
            this.updateMapArea();
        }

        protected override void MoveBoomMan()
        {
            //或许应该写成先优先找到最近的可破坏方块或者玩家，碰撞就判断此处放炸弹是否有活命路径，有就放炸弹，无论放置结果，都走活命路径
            //遍历所有没有爆炸的炸弹，保存所有炸弹范围路径
            //遍历所有火焰，保存火焰路径
            //实现广度搜索最近的安全点只要不在上面地方
            // this.startPath();
 
        }
        private ArrayList canDestroyWall = new ArrayList();
        private ArrayList firePos = new ArrayList();
        private ArrayList bombPos = new ArrayList();

        private void updateDestroyWall()
        {
            canDestroyWall.Clear();
            for (int i = 0; i < this._canDestroyWallContain.transform.childCount; i++)
            {
                canDestroyWall.Add(this._canDestroyWallContain.transform.GetChild(i).transform.position);
            }
        }

        private void updateFire()
        {
            firePos.Clear();
            for (int i = 0; i < this._fireContain.transform.childCount; i++)
            {
                firePos.Add(this._fireContain.transform.GetChild(i).transform.position);
            }
        }

        private void updateBomb()
        {
            bombPos.Clear();
            for (int i = 0; i < this._bombLayer.transform.childCount; i++)
            {
                GameObject bomb = this._bombLayer.transform.GetChild(i).gameObject;
                Bomb bombScript = (Bomb)bomb.GetComponent(typeof(Bomb));
                bombPos.AddRange(bombScript.getFirePos());
            }
        }
        private void updateMapArea()
        {
            this.updateDestroyWall();
            this.updateFire();
            this.updateBomb();
            for (int i = 2; i <= 22; i++)
            {
                for (int j = 2; j <= 22; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)//不可破坏墙
                    {
                        this._mapArea[i, j] = -1;
                    }
                    else if (isCanDestroyWall(i, j))//可破坏墙
                    {
                        this._mapArea[i, j] = 1;
                    }
                    else if (findFirePos(i, j) || (findBombPos(i, j)))
                    {
                        this._mapArea[i, j] = 2;
                    }
                    else if (this._nextPos.x == i && this._nextPos.z == j)
                    {
                        this._mapArea[i, j] = 2;
                    }
                    else
                    {
                        this._mapArea[i, j] = 0;
                    }
                }
            }
            findCanDestroyBlockOrPlayerPos();
        }
        private bool isCanDestroyWall(int x, int z)
        {
            for (int i = 0; i < this.canDestroyWall.Count; i++)
            {
                if (((Vector3)canDestroyWall[i]).Equals(new Vector3(x, 0, z)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool findFirePos(int x, int z)
        {
            for (int i = 0; i < this.firePos.Count; i++)
            {
                if (((Vector3)this.firePos[i]).Equals(new Vector3(x, 0, z)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool findBombPos(int x, int z)
        {
            for (int i = 0; i < this.bombPos.Count; i++)
            {
                if (((Vector3)this.bombPos[i]).Equals(new Vector3(x, 0, z)))
                {
                    return true;
                }
            }
            return false;
        }
        private void findCanDestroyBlockOrPlayerPos()
        {
            //递归
            getNextPos((int)this.transform.position.x, (int)this.transform.position.z);
        }

        private bool getNextPos(int x, int z)
        {
            Debug.Log(x + ",," + z);
            if (x < 2 || z < 2 || x > 22 || z > 22 || this._mapArea[x, z] == -1 || this._nextPos.z > 0 || (this._nextPos.x == x && this._nextPos.z == z))
            {
                return false;
            }
            if (this._mapArea[x, z] == 1)
            {
                return true;
            }
            if (this._mapArea[x, z] == 0)
            {
                this._mapArea[x, z] = -1;
                if (this.getNextPos(x - 1, z) || this.getNextPos(x + 1, z) || this.getNextPos(x, z - 1) || this.getNextPos(x, z + 1))
                {
                    this._nextPos = new Vector3(x, 0, z);
                    return false;
                }
            }
            return false;
        }

        
        protected override bool checkCreateBoom()
        {
            //放置炸弹前先广度搜索安全点，没有就不放置（需要将炸弹爆炸范围算入）
            if (this.IsMaxBoomNum())
            {
                return false;
            }
            if (_isNeedCreateBomb)
            {
                this._isNeedCreateBomb = false;
                return true;
            }
            return false;
            // this.startBoom();
        }


    }
}