using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    private int _fireIndex;
    private bool _isInit = false;

    private float _passTime;
    public bool _isDestroy = false;
    public void SetData(int fireIndex)
    {
        _fireIndex = fireIndex;
        var content = transform.Find("Content");
        GameObject firePrefabs = GameObject.Instantiate(Loader.GetInstance().LoadPrefabs(PathConst.RESOURCE_FIRE_WALL[fireIndex]));
        firePrefabs.transform.SetParent(content.transform);
        _isInit = true;
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (!_isInit || this._isDestroy)
        {
            return;
        }
        this._passTime += (Time.deltaTime);
        if (this._passTime >= BombConst.FIRE_EXIST_SEC)
        {
            this._isDestroy = true;
            Destroy(gameObject);
        }
    }
}
