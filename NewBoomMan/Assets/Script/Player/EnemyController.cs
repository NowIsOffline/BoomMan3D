using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerBase
{
    ArrayList pathList = new ArrayList();
    private bool isStartMove = true;
    // Start is called before the first frame update
    void Start()
    {
        pathList.Add(new Vector3(5, 1, 1));
        pathList.Add(new Vector3(5, 1, 5));
        pathList.Add(new Vector3(1, 1, 5));
        pathList.Add(new Vector3(1, 1, 1));
    }

 void Update(){
    if (!isInit)
    {
        return;
    }
    this._createBoomCd += (Time.deltaTime);
    if (pathList.Count > 0)
        {
         Vector3 targetPath = (Vector3)pathList[0];
         Vector3 offset = targetPath - transform.position;
         float difficulValue = Mathf.Abs(offset.x) + Mathf.Abs(offset.y) + Mathf.Abs(offset.z);
         bool isRun = true;
         if (difficulValue < 0.02)
         {
             isRun = false;
             pathList.RemoveAt(0);
         }
         else
         {
             Vector3 newDir = offset.normalized;
             transform.forward = Vector3.Lerp(transform.forward, newDir, 1f);
             characterController.Move(transform.forward * speed * Time.deltaTime);
         }
         ChangeRunState(isRun);
        }
    }
}
