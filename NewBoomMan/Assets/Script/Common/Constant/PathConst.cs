
public static class PathConst
{
    public static string MAP_RESOURCE_PATH = "MapResource/";

    public static string[] RESOURE_MAP_FLOOR = {
        MAP_RESOURCE_PATH+"FloorBlock_0"
    };

    public static string[] RESOURCE_MAP_WALL = {
        MAP_RESOURCE_PATH+"MapWall_0",
        MAP_RESOURCE_PATH+"MapWall_1",
        MAP_RESOURCE_PATH+"MapWall_2"
    };

    public static string PLAYER_RESOURCE_PATH = "PlayerResource/";
    public static string PLAYER_PREFABS_PATH = PLAYER_RESOURCE_PATH + "Player";

    public static string BOMB_RESOURCE_PATH = "Bomb/";
    public static string BOMB_CONTAIN_SOURCE_PATH = BOMB_RESOURCE_PATH+"BombContain";
    public static string[] RESOURCE_BOMB_WALL = {
        BOMB_RESOURCE_PATH+"BOMB_0"
    };
    public static string FIRE_CONTAIN_SOURCE_PATH = BOMB_RESOURCE_PATH+"FireContain";
    public static string[] RESOURCE_FIRE_WALL = {
        BOMB_RESOURCE_PATH+"FIRE_0"
    };
}
