public class ModelConfigData{
    private int modelIndex;
    private string modelName;
    private string modelPath;
    private float modelScale;
    public ModelConfigData(int index,string name,string path,float scale){
        ModelIndex = ModelIndex;
        ModelName = name;
        ModelPath = path;
        ModelScale = scale;
    }

    public int ModelIndex { get => modelIndex; set => modelIndex = value; }
    public string ModelName { get => modelName; set => modelName = value; }
    public string ModelPath { get => modelPath; set => modelPath = value; }
    public float ModelScale { get => modelScale; set => modelScale = value; }
}