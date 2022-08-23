using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerModel;
    private GameObject playerModelPrefabs;
    private int modelIndex = 0;
    private bool isInit = false;
    private Animator animator = null;
    private bool nowRunState = false;
    void Awake()
    {

    }
    void Start()
    {

    }

    public void SetData(int index,Vector3 initPos)
    {
        transform.localPosition = initPos;
        modelIndex = index;
        playerModelPrefabs = Resources.Load(PathConst.PLAYER_MODEL_PATH[modelIndex]) as GameObject;
        playerModel = GameObject.Instantiate(playerModelPrefabs);
        playerModel.transform.parent = GameObject.Find("playerModel").transform;
        float scale = (float)1 / 25;
        playerModel.transform.localScale = new Vector3(scale, scale, scale);
        InitAnimator();
        isInit = true;
    }

    void InitAnimator()
    {
        var model = playerModel.transform.Find("Model");
        animator = model.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInit)
        {
            return;
        }
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        StartMove(v, h);
    }

    void StartMove(float v, float h)
    {
        bool isRun = Mathf.Abs(v) > 0.01 || Mathf.Abs(h) > 0.01;
        if (isRun != nowRunState)
        {
            nowRunState = isRun;
            animator.SetBool("IsRun", isRun);
            Vector3 newDir = new Vector3(h, 0, v).normalized;
            playerModel.transform.forward = Vector3.Lerp(playerModel.transform.forward, newDir, 1f);
        }

    }
}
