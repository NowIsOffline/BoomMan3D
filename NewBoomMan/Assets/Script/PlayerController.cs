using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    void Update()
    {
        if (!isInit)
        {
            return;
        }
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        StartMove(v, h);
    }

     void StartMove(float v, float h)
    {
        bool isRun = Mathf.Abs(v) > 0.01 || Mathf.Abs(h) > 0.01;
        if (isRun != nowRunState)
        {
            nowRunState = isRun;
            animator.SetBool("IsRun", isRun);
            Vector3 newDir = new Vector3(h, 0, v).normalized;
            transform.forward = Vector3.Lerp(transform.forward, newDir, 1f);
        }
        if (nowRunState)
        {
            characterController.Move(transform.forward * speed * Time.deltaTime);
        }
    }
}
