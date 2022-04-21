using System.Collections;
using System.Collections.Generic;
using ConstantsSpace;
using UnityEngine;
using ResourceLoader;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private Object prefabs_Fire;
    private const int FIRE_RANGE = 1;
    private const int MAX_DIRACT_NUM = 4;
    public GameObject[] CannotDestroyWall;
    public GameObject[] CanDestroyWall;

    private ArrayList _firePos;
    private bool isBoomed = false;
    void Start()
    {
        StartCoroutine("StartBoom");
    }

    public ArrayList getFirePos(){
        this.updateFirePosArray();
        return this._firePos;
    }
    public bool isDangePos(Vector3 pos)
    {
        this.updateFirePosArray();
        for (int i = 0; i < this._firePos.Count; i++)
        {
            if (pos.Equals(this._firePos[i]))
            {
                return true;
            }
        }
        return false;
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
        this.gameObject.layer = 6;
        AstarPath.active.Scan();
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
        AstarPath.active.Scan();
        CannotDestroyWall = GameObject.FindGameObjectsWithTag("CannotDestroyWall");
        prefabs_Fire = PrefabsResource.Instance.LoadResource(Constants.FIRE_PREFAB_PATH);
        isBoomed = true;
        Destroy(this.gameObject);
        CanDestroyWall = GameObject.FindGameObjectsWithTag("CanDestroyWall");//能破坏的墙每次都要查询一次
        this.updateFirePosArray();
        GameObject fireContain = GameObject.Find("FireContain");
        for (int i = 0; i < this._firePos.Count; i++)
        {
            GameObject fire = (GameObject)Instantiate(prefabs_Fire, (Vector3)this._firePos[i],
              Quaternion.identity);
            fire.transform.SetParent(fireContain.transform);
        }
    }

    private void updateFirePosArray()
    {
        if (_firePos == null)
        {
            _firePos = new ArrayList();

        }
        _firePos.Clear();
        for (int i = -1; i < MAX_DIRACT_NUM; i++)
        {
            GetFirePos(i);
        }
    }

    private void GetFirePos(int directIndex)
    {
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
            this._firePos.Add(startPos);
            if (checkHitCanDestroyWall(startPos) || directIndex == -1)
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

    // Update is called once per frame
    void Update()
    {

    }
}
