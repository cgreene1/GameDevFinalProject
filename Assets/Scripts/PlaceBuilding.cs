using UnityEngine;
using System.Collections;

public class PlaceBuilding : MonoBehaviour
{


    private int selectedObjectInArray;
    private GameObject currentlySelectedObject;
    private Building_Controls buildingControls;
    private Player_controls humanPlayer;
    private Vector2 mousePos;
    [SerializeField]
    private GameObject unitPrefab;
    private bool wantPlaceSpawn;
    private bool wantPlaceMine;

    private bool isAnObjectSelected = false;

    // Use this for initialization
    void Start()
    {
        selectedObjectInArray = 0;
        // gain ability to place a building
        buildingControls = GameObject.Find("BuildingControls").GetComponent<Building_Controls>();
        // find human player
        humanPlayer = GameObject.Find("Manager").GetComponent<Manager>().showHuman();
        wantPlaceSpawn = false;
        wantPlaceMine = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(wantPlaceSpawn && Input.GetMouseButtonDown(0) && !humanPlayer.isBankrupt())
        {
            buildingControls.setPlayer(humanPlayer);
            placeSpawn();
            wantPlaceSpawn = false;
        }
        if (wantPlaceMine && Input.GetMouseButtonDown(0) && !humanPlayer.isBankrupt())
        {
            buildingControls.setPlayer(humanPlayer);
            placeMine();
            wantPlaceMine = false;
        }
    }

    public void placeSpawn()
    {
        Vector2 spawnPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        buildingControls.setOffense(true);
        buildingControls.setUnitPrefab(unitPrefab);
        buildingControls.setFaction(humanPlayer.showFaction());
        buildingControls.setSpawnLocation((int)spawnPos.y, (int)spawnPos.x);
        GameObject build = buildingControls.buildSpawnerPrefab(humanPlayer);

    }
    public void placeMine()
    {
        Vector2 spawnPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        buildingControls.setFaction(humanPlayer.showFaction());
        buildingControls.setPlayer(humanPlayer);
        buildingControls.setSpawnLocation((int)spawnPos.y, (int)spawnPos.x);
        GameObject build = buildingControls.buildMinePrefab(humanPlayer);

    }
    /*
    private (bool, Vector2) whileTrue(bool flag, Vector2 loc)
    {
         if (Input.GetMouseButtonDown(0)) { 
                flag = false; 
                loc = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return (flag, (new Vector2(Mathf.Round(loc.x), Mathf.Round(loc.y))));
         } else {  return whileTrue(flag, loc); }
    }
    */

    public void wantSpawner()
    {
        wantPlaceSpawn = true;
        wantPlaceMine = false;
    }
    public void wantMine()
    {
        wantPlaceSpawn = false;
        wantPlaceMine = true;
    }
}
