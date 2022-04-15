using System.Collections;
using System.Collections.Generic;
using ConstantsSpace;
using UnityEngine;
using BoomResource;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private Object prefabs_Fire;
    private const int FIRE_RANGE = 3;
    private const int MAX_DIRACT_NUM = 4;
    public GameObject[] CannotDestroyWall;
    public GameObject[] CanDestroyWall;
    private bool isBoomed = false;
    public int BoomTime = 0;
    void Start()
    {
       
        StartCoroutine("StartBoom");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fire")
        {
            Boom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Collider bombCollider = GetComponent<Collider>();
        bombCollider.isTrigger = false;
    }

    IEnumerator StartBoom()
    {
        yield return new WaitForSeconds(1.5f);
        Boom();
    }

    void Boom()
    {
        if (isBoomed)
        {
            return;
        }
        CannotDestroyWall = GameObject.FindGameObjectsWithTag("CannotDestroyWall");
        prefabs_Fire = PrefabsResource.Instance.LoadResource(Constants.FIRE_PREFAB_PATH);
        isBoomed = true;
        Destroy(this.gameObject);
        CanDestroyWall = GameObject.FindGameObjectsWithTag("CanDestroyWall");//能破坏的墙每次都要查询一次
        BoomTime++;
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
            if (checkHitCannotDestroyWall(startPos))
            {
                return;
            }
            if (prefabs_Fire == null)
            {
                Debug.LogError("Fire = null");
                return;
            }
            GameObject fire = (GameObject)Instantiate(prefabs_Fire, startPos,
       Quaternion.identity);
            fire.transform.SetParent(fireContain.transform);
            if (checkHitCanDestroyWall(startPos) || directIndex < 0)
            {
            return;
        }

    }
}

private bool checkHitCannotDestroyWall(Vector3 startPos)
{
    for (int i = 0; i < CannotDestroyWall.Length; i++)
    {
        if (startPos.Equals(CannotDestroyWall[i].transform.position))
        {
            return true;
        }
    }
    return false;
}

private bool checkHitCanDestroyWall(Vector3 startPos)
{

    for (int i = 0; i < CanDestroyWall.Length; i++)
    {
        if (startPos.Equals(CanDestroyWall[i].transform.position))
        {
            return true;
        }
    }
    return false;
}

private double pointsDistance(Vector3 pos1, Vector3 pos2)
{
    double dSquareSum = 0;
    dSquareSum = Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2);
    dSquareSum += Mathf.Pow(pos1.z - pos2.z, 2);
    return System.Math.Sqrt(dSquareSum);
}
// Update is called once per frame
void Update()
{

}
}
