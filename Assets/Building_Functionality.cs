using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building_Functionality : MonoBehaviour
{
    // private variables
    // Location;
    // Faction;
    // Owner;

    // Start is called before the first frame update
    void Start()
    {
        // get the location of the building
        // set faction fro9m player action
        // set who owns this
    }

    // Update is called once per frame
    void Update()
    {

    }

    public (int,int) findLocation()
    {
        return (0, 0);
    }
    public abstract void testing();


    /* public [Location type] getLoc(){
     * 
     * return loc;
     * }
     */
    /*public [Player] Ownedby(){
     * 
     * return owner;
     *} 
    */


    //define my own type for this?
    void spawnUnit(GameObject unitType){
        //spawn the passed in unit type on call
        GameObject newUnit = Instantiate<GameObject>(unitType);
        newUnit.transform.position = new Vector2(getLoc.position.x, getLoc.position.y);

    }

}
