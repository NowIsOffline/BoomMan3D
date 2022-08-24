using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected GameObject playerModel;
    private GameObject playerModelPrefabs;
    protected bool isInit = false;
    protected Animator animator = null;
    protected bool nowRunState = false;
    protected ModelConfigData modelConfig = null;
    protected CharacterController characterController;
    protected float speed = PlayerConst.MOVE_SPEED;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetData(int index, Vector3 initPos)
    {
        modelConfig = ModelConfig.ModelConfigs[index];
        characterController = GetComponent<CharacterController>();
        playerModelPrefabs = Resources.Load(modelConfig.ModelPath) as GameObject;
        playerModel = GameObject.Instantiate(playerModelPrefabs);
        playerModel.transform.parent = GameObject.Find("playerModel").transform;
        float scale = modelConfig.ModelScale;
        playerModel.transform.localScale = new Vector3(scale, scale, scale);
        InitAnimator();
        transform.localPosition = initPos;
        isInit = true;
    }

    void InitAnimator()
    {
        var model = playerModel.transform.Find("Model");
        animator = model.GetComponent<Animator>();
    }

    protected void ChangeRunState(bool isRun)
    {
        if (isRun != nowRunState)
        {
            nowRunState = isRun;
            animator.SetBool("IsRun", isRun);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }


}
