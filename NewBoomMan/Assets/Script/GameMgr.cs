using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> players = new List<GameObject>();
    public GameObject playerPrefabs;
    void Start()
    {
        if (playerPrefabs)
        { 
            var player = Instantiate(playerPrefabs);
            players.Add(player);
         
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
