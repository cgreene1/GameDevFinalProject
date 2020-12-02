using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction
{
    private LinkedList<Unit> roster;
    private LinkedList<Building_Functionality> buildings;

    private string whichFaction;
    private GameObject[] unitPrefabs;
    private GameObject spawnerPrefab;
    private GameObject minePrefab;

    // call at start to create proper faction for player
    public Faction SetFaction(bool player)
    {
        // set faction here, eiter a button push for player or random for the AI
        unitPrefabs = null;
        spawnerPrefab = null;
        if (player)
        {
            // ask player to choose which faction they what to play as
            whichFaction = "Player"; // this is where there choice is recorded
        }
        else
        {
            // have something set up earlier or random
            whichFaction = "Enemy";
        }
        PopulateFaction();
        return this;

    }

    public void PopulateFaction()
    {
        if (this.whichFaction == "Player")
        {
            spawnerPrefab = (GameObject.Find("BuildingControls").GetComponent<Building_Controls>().getPlayerSpawner());
            minePrefab = GameObject.Find("BuildingControls").GetComponent<Building_Controls>().getPlayerMine();

        } else if (this.whichFaction == "Enemy")
        {
            spawnerPrefab = (GameObject.Find("BuildingControls").GetComponent<Building_Controls>().getEnemySpawner());
            minePrefab = GameObject.Find("BuildingControls").GetComponent<Building_Controls>().getEnemyMine();

        }
    }



    public GameObject getSpawnPrefab(){
        return spawnerPrefab;
    }

    public GameObject[] getUnitPrefabs(){
        return unitPrefabs;
    }

    public string showName(){
        return whichFaction;
    }
}
