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
    private GameObject mapWallPrefabs;
    private GameObject mapFloorContain;
    private GameObject mapWallContain;
    void Awake()
    {
        mapFloorPrefabs = Resources.Load(PathConst.RESOURE_MAP_FLOOR[0]) as GameObject;
        mapWallPrefabs = Resources.Load(PathConst.RESOURCE_MAP_WALL[0]) as GameObject;
    }
    void Start()
    {
        mapData = new int[MAP_WIDTH_NUM * MAP_HEIGHT_NUM];
        mapFloorContain = GameObject.Find("MapFloorContainer");
        mapWallContain = GameObject.Find("MapBlockContainer");
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
            mapFloorBlock.name = "MapFLoorBlock_" + i;
            mapFloorBlock.transform.parent = mapFloorContain.transform;
            int mapState = mapData[i];
            if (mapState == FLOOR_STATE_FLOOR)
            {
                var mapWall = GameObject.Instantiate(mapWallPrefabs);
                mapWall.transform.localPosition = new Vector3(row, 1, col);
                mapWall.name = "MapWall_" + row + "_" + col;
                mapWall.transform.parent = mapWallContain.transform;
            }
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
            if (row % 2 != 0 && i % 2 != 0)
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
