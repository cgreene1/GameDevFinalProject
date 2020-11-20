using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Manager : MonoBehaviour
{

    Map map;
    List<GameObject> playerlist;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        // create a map and list of players
        map = GameObject.Find("Map").GetComponent<Map>();
        CreatePlayerList(1);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //gui layer to show resources
    /*public void OnGUI() {
        try
        {
            Player_controls guiList = playerlist[0];
            GUI.Label(new Rect(10,10,100,20), "Income: " + (guiList.showCommonIncome()).ToString());
        }
        catch
        {
            Debug.Log("FAILED");
            throw;
        }
    }*/
    // create a list of new players

    private void CreatePlayerList(int size)
    {
        playerlist = new List<GameObject>(size);
       for(int i = 0; i <= size; i++)
        {
            GameObject np = Instantiate(prefab);
            playerlist.Add(np);
            if(i == 0)
            {
                np.GetComponent<Player_controls>().setHuman(true);
            } else
            {
                np.GetComponent<Player_controls>().setHuman(false);
            }
            Debug.Log("A player was added");
        }
        
    }

    public Player_controls givePlayer(Player_controls safe)
    {
        if (playerlist[0] != safe)
        {
            return playerlist[0].GetComponent<Player_controls>();
        }
        else return playerlist[1].GetComponent<Player_controls>();
    }

    public Player_controls showHuman() {
        return playerlist[0].GetComponent<Player_controls>();
    }
}
