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
    public GameObject buildSpawnerPrefab(){
        Renderer r = spawnerPrefab.GetComponent<Renderer>();
        (int sizex, int sizey) = map.tileScale(r);
        if(map.canPlace(row, col, sizex, sizey)){
            spawnScript.setVals(isOffense, unitPrefab, faction, player);
            player.gainSpawner(spawnScript);
            return map.addBuilding(spawnerPrefab, row, col);
        }
        return null;
    }


    //creates a mine prefab and adds it to the map, returns null if cannot be placed
    //should be called AFTER setting things with buttons
    public GameObject buildMinePrefab(){
        Renderer r = minePrefab.GetComponent<Renderer>();
        (int sizex, int sizey) = map.tileScale(r);
        if(map.canPlace(row, col, sizex, sizey)){
            Mine mineScript = minePrefab.GetComponent<Mine>();
            mineScript.setFaction(faction);
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
