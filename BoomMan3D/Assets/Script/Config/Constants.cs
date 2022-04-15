namespace ConstantsSpace
{
    static class WallConstants
    {
        public const string MAP_FLOOR_CONTAIN = "MapFloorContain";
        public const string AROUND_WALL_CONTAIN = "AroundWallContain";
        public const string CAN_NOT_DESTROY_WALL_CONTAIN = "CanNotDestroyWallContain";
        public const string CAN_DESTROY_WALL_CONTAIN = "CanDestroyWallContain";
        public const int FLOOR_WIDTH_NUM = 23;
        public const int FLOOR_HEIGHT_NUM = 23;
        public const int AROUND_WALL_HEIGHT_NUM = 2;
    }
    static class Constants
    {
        public const string PLAYER_PREFAB_PATH = "Prefabs/Player";
        public const string ENEMY_PREFAB_PATH = "Prefabs/Enemy";
        public const string BOMB_PREFAB_PATH = "Prefabs/Bomb";
        public const string CAN_DESTROY_WALL_PREFAB_PATH = "Prefabs/MapCanDestroyWallCube_Normal";
        public const string CAN_NOT_DESTROY_WALL_PREFAB_PATH = "Prefabs/MapCannotDestroyWallCube_Normal";
        public const string FLOOR_CUBE_PREFAB_PATH = "Prefabs/MapFloorCube_Normal";
        public const string FIRE_PREFAB_PATH = "Prefabs/Fire";
    }
    static class PlayerConstants
    {
        public const float PLAYER_MOVE_SPEED = 0.1f;
    }


}
