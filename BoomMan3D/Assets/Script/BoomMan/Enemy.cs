using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstantsSpace;
using BoomResource;
namespace BoomMan
{
    class Enemy : BoomManBase
    {

        protected override void initData()
        {
            this._playerLayer = GameObject.Find("PlayerLayer");
            this._boomLayer = GameObject.Find("BombLayer");
            this._initPos = GameObject.Find("PlayerInitPos_1");
        }
        protected override void MoveBoomMan()
        {
            
        }
    }
}