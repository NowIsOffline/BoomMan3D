using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject viewPrefabs;
    public GameObject bombPrefab;
    private int viewIndex = 0;
    private ModelConfigData configData;
    private bool canDropBombs = true;
    private Rigidbody rigidBody;
    private Animator animator;
    private float moveSpeed = 5f;
    private bool canMove = true;
    private bool dead = false;
    void Start()
    {
        configData = ModelConfig.ModelConfigs[viewIndex];
        viewPrefabs = Loader.GetInstance().LoadPrefabs(configData.ModelPath);
        if (viewPrefabs)
        {
            GameObject view = Instantiate(viewPrefabs);
            Transform playerModel = transform.Find("PlayerModel");
            view.transform.SetParent(playerModel);
            view.transform.position = new Vector3(0, 0, 0);
            view.transform.localScale = new Vector3(configData.ModelScale, configData.ModelScale, configData.ModelScale);
            animator = view.transform.Find("Model").GetComponent<Animator>();
        }
        transform.position = new Vector3(1, 0, 1);
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", false);
        if (!canMove)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        { //Up movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.A))
        { //Left movement
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            transform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.S))
        { //Down movement
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("Walking", true);
        }

        if (Input.GetKey(KeyCode.D))
        { //Right movement
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("Walking", true);
        }

        if (canDropBombs && Input.GetKeyDown(KeyCode.Space))
        { //Drop bomb
            DropBomb();
        }
    }

    void DropBomb()
    {
        if (bombPrefab)
        { //Check if bomb prefab is assigned first
            // Create new bomb and snap it to a tile
            Instantiate(bombPrefab,
                new Vector3(Mathf.RoundToInt(transform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(transform.position.z)),
                bombPrefab.transform.rotation);
        }
    }
}
