
public class DataMgr
{
    private int[] mapData;
    private static DataMgr _instance;

    // This is the static method that controls the access to the singleton
    // instance. On the first run, it creates a singleton object and places
    // it into the static field. On subsequent runs, it returns the client
    // existing object stored in the static field.
    public static DataMgr GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DataMgr();
            _instance.Init();
        }
        return _instance;
    }

    public void Init()
    {
        mapData = new int[MapConst.MAP_WIDTH_NUM * MapConst.MAP_HEIGHT_NUM];
    }

    public int GetMapDataLength()
    {
        return mapData.Length;
    }

    public int GetMapDataByIndex(int index)
    {
        return mapData[index];
    }

    public void SetMapDataByIndex(int index,int state){
        mapData[index] = state;
    }
}
