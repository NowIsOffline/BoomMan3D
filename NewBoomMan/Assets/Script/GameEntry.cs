using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    // Start is called before the first frame update
    private static int FLOOR_STATE_FLOOR = 1;
    public int MAP_WIDTH_NUM = 21;
    public int MAP_HEIGHT_NUM = 21;
    private int[] mapData;
    private GameObject mapFloorPrefabs;
    void Awake()
    {
        mapFloorPrefabs = Resources.Load(PathConst.RESOURE_MAP_FLOOR[1]) as GameObject;
    }
    void Start()
    {
        mapData = new int[MAP_WIDTH_NUM * MAP_HEIGHT_NUM];
        InitMapData();
        LoadMap();
    }

    void LoadMap()
    {

        for (int i = 0; i < mapData.Length; i++)
        {
            int row = (int)Mathf.Floor(i / MAP_WIDTH_NUM);
            int col = i % MAP_WIDTH_NUM;
            var mapFloorBlock = GameObject.Instantiate(mapFloorPrefabs);
            mapFloorBlock.transform.localPosition = new Vector3(row, 0, col);
        }

    }

    void InitMapData()
    {
        for (int i = 0; i < MAP_HEIGHT_NUM; i++)
        {
            InitMapDataRow(i);
        }
    }

    void InitMapDataRow(int row)
    {
        for (int i = 0; i < MAP_WIDTH_NUM; i++)
        {
            int mapState = 0;
            if (row % 2 != 0 || i % 2 != 0)
            {
                mapState = FLOOR_STATE_FLOOR;
            }
            mapData[i + row * MAP_WIDTH_NUM] = mapState;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Destroy()
    {

    }
}
