using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Manager : MonoBehaviour
{

    Map map;
    List<Player_controls> playerlist;

    // Start is called before the first frame update
    void Start()
    {
        // create a map and list of players
        map = GameObject.Find("map").GetComponent<Map>();
        CreatePlayerList(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // create a list of new players

    private void CreatePlayerList(int size)
    {
        for(int i = 0; i < size; i++)
        {
            Player_controls np = new Player_controls();
            if (i == 0) np.setHuman(true);
            playerlist[i] = np;
        }
    }
}
