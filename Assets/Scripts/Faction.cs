using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction
{
    private LinkedList<Unit> roster;
    private LinkedList<Building_Functionality> buildings;

    private string whichFaction;
    private GameObject[] unitPrefabs;
    [SerializeField] GameObject[] spawnerPrefabs;
    private LinkedList<GameObject> minePrefabs;

    // call at start to create proper faction for player
    public Faction SetFaction(bool player)
    {
        // set faction here, eiter a button push for player or random for the AI
        Faction nf = new Faction();

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
        return nf;

    }

    public void PopulateFaction()
    {
        if (this.whichFaction == "SCIFI")
        {
            SetSCIFI();
        }else if(this.whichFaction == "Fantasy")
        {
            SetFantasy();
        }else if(this.whichFaction == "Undead")
        {
            SetUndead();
        }
        else
        {
            Debug.Log("Error in Faction naming");
        }
    }


    void SetSCIFI()
    {
        
    }

    void SetFantasy()
    {

    }

    void SetUndead()
    {

    }


    public GameObject getSpawnPrefab(){
        return spawnerPrefabs[0];
    }

    public GameObject[] getUnitPrefabs(){
        return unitPrefabs;
    }

    public string showName(){
        return whichFaction;
    }
}
