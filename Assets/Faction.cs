using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    private LinkedList<Unit> roster;
    private LinkedList<Building_Functionality> buildings;
    private string whichFaction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // call at start to create proper faction for player
    public Faction SetFaction(bool player)
    {
        // set faction here, eiter a button push for player or random for the AI
        Faction nf = new Faction();

        if (player)
        {
            // ask player to choose which faction they what to play as
            whichFaction = "holder"; // this is where there choice is recorded
        }
        else
        {
            // have something set up earlier or random
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

}
