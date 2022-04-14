using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private const int FIRE_RANGE = 3;
    private const int MAX_DIRACT_NUM = 4;
    void Start()
    {
        StartCoroutine("StartBoom");
    }

    private void OnTriggerExit(Collider other)
    {
        Collider bombCollider = GetComponent<Collider>();
        bombCollider.isTrigger = false;
    }

    IEnumerator StartBoom()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
        createFire(-1);
        for (int i = 0; i < MAX_DIRACT_NUM; i++)
        {
            createFire(i);
        }
    }

    private void createFire(int directIndex)
    {
        GameObject fireContain = GameObject.Find("FireContain"); ;
        for (int i = 0; i < FIRE_RANGE; i++)
        {
            Vector3 startPos = this.gameObject.transform.position;
            switch (directIndex)
            {
                case 0:
                    startPos.x += i + 1;
                    break;
                case 1:
                    startPos.x += -(i + 1);
                    break;
                case 2:
                    startPos.z += i + 1;
                    break;
                case 3:
                    startPos.z += -(i + 1);
                    break;
            }
            GameObject fire = (GameObject)Instantiate(Resources.Load("Prefabs/Fire"), startPos,
        Quaternion.identity);
            fire.transform.SetParent(fireContain.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
