using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	const int FLOOR_WIDTH_NUM = 23;
	const int FLOOR_HEIGHT_NUM = 23;
	const float PLAYER_MOVE_SPEED= 0.1f;
	
	const int AROUND_WALL_HEIGHT_NUM = 2;
	private GameObject map;
	private GameObject mapFloorContain;
	private GameObject aroundWallContain;

	private GameObject wallOnTheFloorContain;


	private GameObject player;
	private Rigidbody playerRigidBody;
	private  Transform towards;

	void Awake(){
		// Application.targetFrameRate=30;
	}
	void Start () {
		createMap();
		createPlayer();

	}
	
	void createMap(){
		map = GameObject.Find("Map");
		// createCannotDestroyWallAndFloor();
	}

	void createPlayer(){
			player=(GameObject) Instantiate(Resources.Load("Prefabs/Player"),new Vector3(0f,0f,0f),
				Quaternion.identity);
			player.transform.SetParent(this.map.transform);
			player.transform.localEulerAngles = new Vector3(player.transform.rotation.x,player.transform.rotation.y+90,player.transform.rotation.z);
			initPlayerPos();
			playerRigidBody = player.GetComponent<Rigidbody>();
			playerRigidBody.freezeRotation=true;//静止碰撞旋转
			towards = player.transform;
	}

	void initPlayerPos(){
			GameObject playerInitPos = GameObject.Find("PlayerInitPos_0");
			player.transform.position=new Vector3(playerInitPos.transform.position.x,
			playerInitPos.transform.position.y,playerInitPos.transform.position.z);
	}


	void createCannotDestroyWallAndFloor(){
		mapFloorContain = new GameObject();
		mapFloorContain.name = "MapFloorContain";
		mapFloorContain.transform.SetParent(map.transform);

		aroundWallContain = new GameObject();
		aroundWallContain.name = "AroundWallContain";
		aroundWallContain.transform.SetParent(map.transform);


		wallOnTheFloorContain = new GameObject();
		wallOnTheFloorContain.name = "WallOnTheFloorContain";
		wallOnTheFloorContain.transform.SetParent(map.transform);
		for(int i =1;i<=FLOOR_WIDTH_NUM;i++){
			createCannotDestroyColWallAndFloor(i);
		}	
	}

	void createCannotDestroyColWallAndFloor(int col){
		for(int i =1;i<=FLOOR_HEIGHT_NUM;i++){
			bool isFinalOrStart= i==1||col==1||col==FLOOR_WIDTH_NUM||i==FLOOR_HEIGHT_NUM;
			if(isFinalOrStart){//创建周围墙体
				createAroundWall(col,i);
			}
			else if(i%2!=0&&col%2!=0){//创建地面上墙体
				createWallOnTheFloor(col,i);
			}else{
				double value=UnityEngine.Random.Range(0,10);
				if(value>5){
					createCanDestroyWall(col,i);
				}
			}
			createFloor(col,i);
			
		}	
	}
	void createCanDestroyWall(int col,int row){
		GameObject cube =(GameObject) Instantiate(Resources.Load("Prefabs/MapCanDestroyWallCube_Normal"),new Vector3(col*1,2,row*1),
				Quaternion.identity);
		cube.transform.SetParent(wallOnTheFloorContain.transform);
	}

	void createWallOnTheFloor(int col,int row){
		GameObject cube =(GameObject) Instantiate(Resources.Load("Prefabs/MapCannotDestroyWallCube_Normal"),new Vector3(col*1,2,row*1),
				Quaternion.identity);
				cube.transform.SetParent(wallOnTheFloorContain.transform);
	}

	void createAroundWall(int col,int row){
		for(int i=1;i<=AROUND_WALL_HEIGHT_NUM;i++){
			GameObject cube2 =(GameObject) Instantiate(Resources.Load("Prefabs/MapCannotDestroyWallCube_Normal"),new Vector3(col*1,i*1+1,row*1),
			Quaternion.identity);
			cube2.transform.SetParent(aroundWallContain.transform);
		}

	}

	void createFloor(int col,int row){
			GameObject cube =(GameObject) Instantiate(Resources.Load("Prefabs/MapFloorCube_Normal"),new Vector3(col*1,1,row*1),
			Quaternion.identity);
			cube.name = "MapFloorCube_"+col+"_"+row;
			cube.transform.SetParent(mapFloorContain.transform);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		MovePlayer();
	}

	void MovePlayer(){
	 	float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
		if(v!=0||h!=0){
			Transform towards = player.transform;
            Vector3 tempTowards =Vector3.forward;
			if(v!=0){
				playerRigidBody.MovePosition(player.transform.position+Vector3.forward*PLAYER_MOVE_SPEED*v);
				tempTowards=v>0?Vector3.forward:-Vector3.forward;
			}
			if(h!=0){
				playerRigidBody.MovePosition(player.transform.position+Vector3.right*PLAYER_MOVE_SPEED*h);
				tempTowards=h>0?Vector3.right:-Vector3.right;
			}
	
			Quaternion q = Quaternion.LookRotation(tempTowards);
            //平滑转向
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, q, 10f * Time.deltaTime);
		}
		
       
        
	}
}
