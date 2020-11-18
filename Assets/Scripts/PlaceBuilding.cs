using UnityEngine;
using System.Collections;

public class PlaceBuilding : MonoBehaviour {


    private int selectedObjectInArray;
    private GameObject currentlySelectedObject;
    private Building_Controls buildingControls;
    private Player_controls humanPlayer;

    [SerializeField]
    private GameObject unitPrefab; 


    private bool isAnObjectSelected = false;

	// Use this for initialization
	void Start ()
    {
        selectedObjectInArray = 0;
        buildingControls = GameObject.Find("BuildingControls").GetComponent<Building_Controls>();
        humanPlayer = GameObject.Find("Manager").GetComponent<Manager>().showHuman();

    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void placeSpawn() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 spawnPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
           //currentlySelectedObject = (GameObject)Instantiate(selectableObjects[selectedObjectInArray], spawnPos, Quaternion.identity);
            isAnObjectSelected = true;
            buildingControls.setOffense(true);
            buildingControls.setUnitPrefab(unitPrefab);
            buildingControls.setFaction(humanPlayer.showFaction());
            buildingControls.setSpawnLocation((int)spawnPos.y, (int)spawnPos.x);
            GameObject build = buildingControls.buildSpawnerPrefab();

        if (Input.GetMouseButtonDown(1) && isAnObjectSelected == true)
        {
            Destroy(currentlySelectedObject);
            isAnObjectSelected = false;
            selectedObjectInArray = 0;
        }
    }
}
