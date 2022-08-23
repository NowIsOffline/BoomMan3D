using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerModel;
    private GameObject playerModelPrefabs;
    private int modelIndex = PlayerConst.PIKACHU_INDEX;
    void Awake(){
        playerModelPrefabs = Resources.Load(PathConst.PLAYER_MODEL_PATH[modelIndex]) as GameObject;
    }
    void Start()
    {
        playerModel = GameObject.Instantiate(playerModelPrefabs);
        playerModel.transform.parent = GameObject.Find("playerModel").transform;
        float scale =(float) 1/25;
        playerModel.transform.localScale = new Vector3(scale,scale,scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
