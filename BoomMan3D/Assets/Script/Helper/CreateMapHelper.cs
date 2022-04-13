
using UnityEngine;
using static UnityEngine.Object;
using static ConstantsSpace.Constants;
namespace BoomManHelper
{
    class CreateMapHelper 
    {

        private GameObject map;
        private GameObject mapFloorContain;
        private GameObject aroundWallContain;

        private GameObject wallOnTheFloorContain;
        CreateMapHelper(GameObject map)
        {
            this.map = map;
        }
        public void createCannotDestroyWallAndFloor()
        {
            mapFloorContain = new GameObject();
            mapFloorContain.name = "MapFloorContain";
            mapFloorContain.transform.SetParent(map.transform);

            aroundWallContain = new GameObject();
            aroundWallContain.name = "AroundWallContain";
            aroundWallContain.transform.SetParent(map.transform);


            wallOnTheFloorContain = new GameObject();
            wallOnTheFloorContain.name = "WallOnTheFloorContain";
            wallOnTheFloorContain.transform.SetParent(map.transform);
            for (int i = 1; i <= FLOOR_WIDTH_NUM; i++)
            {
                createCannotDestroyColWallAndFloor(i);
            }
        }

        void createCannotDestroyColWallAndFloor(int col)
        {
            for (int i = 1; i <= FLOOR_HEIGHT_NUM; i++)
            {
                bool isFinalOrStart = i == 1 || col == 1 || col ==FLOOR_WIDTH_NUM || i == FLOOR_HEIGHT_NUM;
                if (isFinalOrStart)
                {//创建周围墙体
                    createAroundWall(col, i);
                }
                else if (i % 2 != 0 && col % 2 != 0)
                {//创建地面上墙体
                    createWallOnTheFloor(col, i);
                }
                else
                {
                    double value = UnityEngine.Random.Range(0, 10);
                    if (value > 5)
                    {
                        createCanDestroyWall(col, i);
                    }
                }
                createFloor(col, i);

            }
        }
        void createCanDestroyWall(int col, int row)
        {
            GameObject cube = (GameObject)Instantiate(Resources.Load("Prefabs/MapCanDestroyWallCube_Normal"), new Vector3(col * 1, 2, row * 1),
                    Quaternion.identity);
            cube.transform.SetParent(wallOnTheFloorContain.transform);
        }

        void createWallOnTheFloor(int col, int row)
        {
            GameObject cube = (GameObject)Instantiate(Resources.Load("Prefabs/MapCannotDestroyWallCube_Normal"), new Vector3(col * 1, 2, row * 1),
                    Quaternion.identity);
            cube.transform.SetParent(wallOnTheFloorContain.transform);
        }

        void createAroundWall(int col, int row)
        {
            for (int i = 1; i <= AROUND_WALL_HEIGHT_NUM; i++)
            {
                GameObject cube2 = (GameObject)Instantiate(Resources.Load("Prefabs/MapCannotDestroyWallCube_Normal"), new Vector3(col * 1, i * 1 + 1, row * 1),
                Quaternion.identity);
                cube2.transform.SetParent(aroundWallContain.transform);
            }

        }

        void createFloor(int col, int row)
        {
            GameObject cube = (GameObject)Instantiate(Resources.Load("Prefabs/MapFloorCube_Normal"), new Vector3(col * 1, 1, row * 1),
            Quaternion.identity);
            cube.name = "MapFloorCube_" + col + "_" + row;
            cube.transform.SetParent(mapFloorContain.transform);
        }
    }
}