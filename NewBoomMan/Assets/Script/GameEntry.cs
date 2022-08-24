using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject mapFloorContain;
    private GameObject mapWallContain;
    private GameObject player;

    private Loader loader;
    void Awake()
    {

    }
    void Start()
    {
        mapFloorContain = GameObject.Find("MapFloorContainer");
        mapWallContain = GameObject.Find("MapBlockContainer");
        loader = Loader.GetInstance();
        InitMapData();
        LoadMap();
    }

    void LoadMap()
    {

        for (int i = 0; i < DataMgr.GetInstance().GetMapDataLength(); i++)
        {
            int row = (int)Mathf.Floor(i / MapConst.MAP_WIDTH_NUM);
            int col = i % MapConst.MAP_WIDTH_NUM;
            var mapFloorBlock = GameObject.Instantiate(loader.LoadPrefabs(PathConst.RESOURE_MAP_FLOOR[0]));
            mapFloorBlock.transform.localPosition = new Vector3(row, 0, col);
            mapFloorBlock.name = "MapFLoorBlock_" + i;
            mapFloorBlock.transform.parent = mapFloorContain.transform;
            int mapState = DataMgr.GetInstance().GetMapDataByIndex(i);
            if (mapState == MapConst.MAP_STATE_WALL)
            {
                var mapWall = GameObject.Instantiate(loader.LoadPrefabs(PathConst.RESOURCE_MAP_WALL[0]));
                mapWall.transform.localPosition = new Vector3(row, 1, col);
                mapWall.name = "MapWall_" + row + "_" + col;
                mapWall.transform.parent = mapWallContain.transform;
            }
            else if (mapState == MapConst.MAP_STATE_WALL_AROUND)
            {
                var mapWall = GameObject.Instantiate(loader.LoadPrefabs(PathConst.RESOURCE_MAP_WALL[1]));
                mapWall.transform.localPosition = new Vector3(row, 1, col);
                mapWall.name = "MapAroundWall_" + row + "_" + col;
                mapWall.transform.parent = mapWallContain.transform;
            }
            if (row == col && row == 1)
            {
                InitPlayer();
                // InitEmery();
            }
        }

    }

    void InitEmery()
    {
        player = GameObject.Instantiate(loader.LoadPrefabs(PathConst.PLAYER_PREFABS_PATH));
        player.AddComponent<EnemyController>();
        EnemyController controller = player.GetComponent<EnemyController>();
        controller.SetData(ModelConfig.MEWTWO_INDEX, new Vector3(1, 1, 1));
    }

    void InitPlayer()
    {
        player = GameObject.Instantiate(loader.LoadPrefabs(PathConst.PLAYER_PREFABS_PATH));
        player.AddComponent<PlayerController>();
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.SetData(ModelConfig.PIKACHU_INDEX, new Vector3(1, 1, 1));
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
            if (row == 0 || row == MapConst.MAP_HEIGHT_NUM-1 
            || i == 0 || i == MapConst.MAP_WIDTH_NUM-1)
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
