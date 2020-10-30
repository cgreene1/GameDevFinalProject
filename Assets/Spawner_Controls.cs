using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Controls : Building_Functionality
{
    // private variable:
    // Unitspawning
    // type of spawner
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        // set the unit that is spawning
        // set what kind of spawn this is (offensive or defensive)
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        while (active)
        {
            ResourceCheck();
            // timer here to call spawn

        }


    }
    private void Spawn()
    {

    }
    private void ResourceCheck()
    {
        /*
          if(resources <= 0){
             active = false;
          }else{
                active = true;
          }
        */
    }

    public override void testing()
    {

    }


}
