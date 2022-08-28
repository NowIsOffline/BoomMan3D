using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isInit = false;
    private int _bombIndex;
    private int _fireIndex;
    private int _fireRange;
    private GameObject _bombLayer;
    private float _passTime;
    private bool _isBoom= false;

    public void SetData(int bombIndex, int fireIndex, int fireRange)
    {
        _bombIndex = bombIndex;
        _fireIndex = fireIndex;
        _fireRange = fireRange;
        var content = transform.Find("Content");
        GameObject bombPrefabs = GameObject.Instantiate(Loader.GetInstance().LoadPrefabs(PathConst.RESOURCE_BOMB_WALL[_bombIndex]));
        bombPrefabs.transform.SetParent(content.transform);
        _bombLayer = GameObject.Find("BombLayerContainer");
        _isInit = true;
    }

    void Start()
    {

    }

    public void StartBomb()
    {   this._isBoom = true;
        ArrayList firePosArr = GetFirePos();
        for (int i = 0; i < firePosArr.Count; i++)
        {
            GameObject firePrefabs = GameObject.Instantiate(Loader.GetInstance().LoadPrefabs(PathConst.FIRE_CONTAIN_SOURCE_PATH));
            firePrefabs.transform.SetParent(_bombLayer.transform);
            firePrefabs.GetComponent<Fire>().SetData(_fireIndex);
            firePrefabs.transform.position = (Vector3)firePosArr[i];
        }
        Destroy(gameObject);
    }

    public ArrayList GetFirePos()
    {
        ArrayList firePosArr = new ArrayList();
        Vector3 startPos = transform.position;
        firePosArr.Add(startPos);
        for (int i = 0; i <= _fireRange; i++)
        {
            firePosArr.Add(new Vector3(startPos.x + 1, startPos.y, startPos.z));
            firePosArr.Add(new Vector3(startPos.x - 1, startPos.y, startPos.z));
            firePosArr.Add(new Vector3(startPos.x, startPos.y, startPos.z + 1));
            firePosArr.Add(new Vector3(startPos.x, startPos.y, startPos.z - 1));
        }
        return firePosArr;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isInit || this._isBoom)
        {
            return;
        }
        this._passTime += (Time.deltaTime);
        if(this._passTime>=BombConst.BOMB_START_BOOM_SEC){
            this.StartBomb();
        }
    }
}
