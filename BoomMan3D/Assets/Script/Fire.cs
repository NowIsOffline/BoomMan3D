using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine("deleteFire");
    }

    IEnumerator deleteFire(){
         yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }

  private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag=="MapCanDestroyWallCube_Normal"){
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
