using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Manager : MonoBehaviour
{

    Map map;
    public List<Player_controls> playerlist;

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
            np.man = this;
            if (i == 0) np.setHuman(true);
            playerlist[i] = np;
        }
    }
    // basic two player funciton to give the other player.
    public Player_controls givePlayer(Player_controls safe)
    {
        if (playerlist[0] != safe)
        {
            return playerlist[0];
        }
        else return playerlist[1];
    }


}
