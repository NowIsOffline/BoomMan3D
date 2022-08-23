using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject mapFloorPrefabs;
    private GameObject mapWallPrefabs;
    private GameObject mapAroundWallPrefabs;
    private GameObject playerPrefabs;
    private GameObject mapFloorContain;
    private GameObject mapWallContain;
    private GameObject player;
    void Awake()
    {
        mapFloorPrefabs = Resources.Load(PathConst.RESOURE_MAP_FLOOR[0]) as GameObject;
        mapWallPrefabs = Resources.Load(PathConst.RESOURCE_MAP_WALL[0]) as GameObject;
        mapAroundWallPrefabs = Resources.Load(PathConst.RESOURCE_MAP_WALL[1]) as GameObject;
        playerPrefabs = Resources.Load(PathConst.PLAYER_PREFABS_PATH) as GameObject;
    }
    void Start()
    {
        mapFloorContain = GameObject.Find("MapFloorContainer");
        mapWallContain = GameObject.Find("MapBlockContainer");
        InitMapData();
        LoadMap();
    }

    void LoadMap()
    {

        for (int i = 0; i < DataMgr.GetInstance().GetMapDataLength(); i++)
        {
            int row = (int)Mathf.Floor(i / MapConst.MAP_WIDTH_NUM);
            int col = i % MapConst.MAP_WIDTH_NUM;
            var mapFloorBlock = GameObject.Instantiate(mapFloorPrefabs);
            mapFloorBlock.transform.localPosition = new Vector3(row, 0, col);
            mapFloorBlock.name = "MapFLoorBlock_" + i;
            mapFloorBlock.transform.parent = mapFloorContain.transform;
            int mapState = DataMgr.GetInstance().GetMapDataByIndex(i);
            if (mapState == MapConst.MAP_STATE_WALL)
            {
                var mapWall = GameObject.Instantiate(mapWallPrefabs);
                mapWall.transform.localPosition = new Vector3(row, 1, col);
                mapWall.name = "MapWall_" + row + "_" + col;
                mapWall.transform.parent = mapWallContain.transform;
            }
            else if (mapState == MapConst.MAP_STATE_WALL_AROUND)
            {
                var mapWall = GameObject.Instantiate(mapAroundWallPrefabs);
                mapWall.transform.localPosition = new Vector3(row, 1, col);
                mapWall.name = "MapAroundWall_" + row + "_" + col;
                mapWall.transform.parent = mapWallContain.transform;
            }
            if (row == col && row == 1)
            {
                player = GameObject.Instantiate(playerPrefabs);
                PlayerController controller = player.GetComponent<PlayerController>();
                controller.SetData(PlayerConst.PIKACHU_INDEX, new Vector3(1, 1, 1));
            }
        }

    }

    void InitMapData()
    {
        for (int i = 0; i < MapConst.MAP_HEIGHT_NUM; i++)
        {
            InitMapDataRow(i);
        }
    }

    void InitMapDataRow(int row)
    {
        for (int i = 0; i < MapConst.MAP_WIDTH_NUM; i++)
        {
            int mapState = 0;
            if (row == 0 || row == MapConst.MAP_HEIGHT_NUM || i == 0 || i == MapConst.MAP_WIDTH_NUM)
            {
                mapState = MapConst.MAP_STATE_WALL_AROUND;
            }
            else if (row % 2 == 0 && i % 2 == 0)
            {
                mapState = MapConst.MAP_STATE_WALL;
            }
            DataMgr.GetInstance().SetMapDataByIndex(i + row * MapConst.MAP_WIDTH_NUM, mapState);
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
