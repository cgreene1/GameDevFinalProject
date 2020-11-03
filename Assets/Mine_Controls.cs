using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_Controls : Building_Functionality
{
    // Start is called before the first frame update

    // Type of resource
   private (int,int) income;

    void Start()
    {
        // set resource type
        // add to player income of resource type
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        // reduce player income of resource
    }

    public override void testing()
    {

    }

    public (int,int)showIncome()
    {
        return income;
    }
}
