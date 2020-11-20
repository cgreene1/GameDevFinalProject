using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Controls : MonoBehaviour
{
    Map map;
    bool isOffense;
    bool isRare;
    GameObject unitPrefab;
    [SerializeField] GameObject spawnerPrefab;
    [SerializeField] GameObject minePrefab;
    Faction faction;
    Spawner spawnScript;
    int row, col;
    Player_controls player;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        isOffense = true;
        row = 0;
        col = 0;

        spawnScript = spawnerPrefab.GetComponent("Spawner") as Spawner;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //creates a spawner prefab and adds it to the map, returns null if cannot be placed
    //should be called AFTER setting things with buttons
    public GameObject buildSpawnerPrefab(Player_controls builder){
        if (map.canPlace(row, col, 2, 2))
        {
            GameObject newSpawner = map.addBuilding(spawnerPrefab, row, col);      
            Debug.Log("building controls: " + faction.showName());
            spawnScript = newSpawner.GetComponent<Spawner>();
            spawnScript.setVals(isOffense, unitPrefab, faction, builder);
            builder.gainSpawner(spawnScript);
            return newSpawner;
        }
        return null;
    }


    //creates a mine prefab and adds it to the map, returns null if cannot be placed
    //should be called AFTER setting things with buttons
    public GameObject buildMinePrefab(){
        GameObject newMine = map.addBuilding(minePrefab, row, col);
        if (map.canPlace(row, col, 2, 2)){
            Mine mineScript = newMine.GetComponent<Mine>();
            mineScript.setFaction(faction);
            Debug.Log("BuildingControls player:" + player);
            mineScript.setPlayer(player);
          //  mineScript.setRare(isRare);
            player.gainMine(mineScript);
            return map.addBuilding(minePrefab, row, col);
        }
        return null;
    }

    public void setUnitPrefab(GameObject prefab){
        unitPrefab = prefab;
    }

    public void setSpawnLocation(int r, int c){
        row = r; col = c;
    }

    public void setOffense(bool isOff){
        isOffense = isOff;
    }

    public void setRarity(bool rare){
        isRare = rare;
    }

    public void setFaction(Faction fact){
        faction = fact;
    }

    public GameObject getSpawner()
    {
        return spawnerPrefab;
    }

    public void setPlayer(Player_controls owner)
    {
        player = owner;
    }

}
