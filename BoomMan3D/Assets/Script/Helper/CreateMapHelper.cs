
using UnityEngine;
using ConstantsSpace;
using static ConstantsSpace.WallConstants;
namespace BoomManHelper
{
    class CreateMapHelper : MonoBehaviour
    {

        private Object prefabs_canNotDestroy;
        private Object prefabs_canDestroy;
        private Object prefabs_floor;
        private GameObject mapFloorContain;
        private GameObject aroundWallContain;

        private GameObject canNotDestroyWallContain;

        private GameObject canDestroyWallContain;

        void Start()
        {
            prefabs_canNotDestroy = Resources.Load(Constants.CAN_NOT_DESTROY_WALL_PREFAB_PATH);
            prefabs_canDestroy = Resources.Load(Constants.CAN_DESTROY_WALL_PREFAB_PATH);
            prefabs_floor = Resources.Load(Constants.FLOOR_CUBE_PREFAB_PATH);
            // createCannotDestroyWallAndFloor();
        }

        public void createCannotDestroyWallAndFloor()
        {
            mapFloorContain = new GameObject();
            mapFloorContain.name = MAP_FLOOR_CONTAIN;
            mapFloorContain.transform.SetParent(this.transform);

            aroundWallContain = new GameObject();
            aroundWallContain.name = AROUND_WALL_CONTAIN;
            aroundWallContain.transform.SetParent(this.transform);

            canNotDestroyWallContain = new GameObject();
            canNotDestroyWallContain.name = CAN_NOT_DESTROY_WALL_CONTAIN;
            canNotDestroyWallContain.transform.SetParent(this.transform);

            canDestroyWallContain = new GameObject();
            canDestroyWallContain.name = CAN_DESTROY_WALL_CONTAIN;
            canDestroyWallContain.transform.SetParent(this.transform);
            for (int i = 1; i <= FLOOR_WIDTH_NUM; i++)
            {
                createCannotDestroyColWallAndFloor(i);
            }
        }

        void createCannotDestroyColWallAndFloor(int col)
        {
            for (int i = 1; i <= FLOOR_HEIGHT_NUM; i++)
            {
                bool isFinalOrStart = i == 1 || col == 1 || col == FLOOR_WIDTH_NUM || i == FLOOR_HEIGHT_NUM;
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
            GameObject cube = (GameObject)Instantiate(prefabs_canDestroy, new Vector3(col * 1, 2, row * 1),
                    Quaternion.identity);
            cube.transform.SetParent(canDestroyWallContain.transform);
        }

        void createWallOnTheFloor(int col, int row)
        {
            GameObject cube = (GameObject)Instantiate(prefabs_canNotDestroy, new Vector3(col * 1, 2, row * 1),
                    Quaternion.identity);
            cube.transform.SetParent(canNotDestroyWallContain.transform);
        }

        void createAroundWall(int col, int row)
        {
            for (int i = 1; i <= AROUND_WALL_HEIGHT_NUM; i++)
            {
                GameObject cube2 = (GameObject)Instantiate(prefabs_canNotDestroy, new Vector3(col * 1, i * 1 + 1, row * 1),
                Quaternion.identity);
                cube2.transform.SetParent(aroundWallContain.transform);
            }

        }

        void createFloor(int col, int row)
        {
            GameObject cube = (GameObject)Instantiate(prefabs_floor, new Vector3(col * 1, 1, row * 1),
            Quaternion.identity);
            cube.name = "MapFloorCube_" + col + "_" + row;
            cube.transform.SetParent(mapFloorContain.transform);
        }
    }
}