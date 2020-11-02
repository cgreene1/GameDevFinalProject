using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //here is the shortest path to find the closest enemy when called
    void attack(){
        thisX = this.location.x;
        thisY = this.location.y;

    }
}
