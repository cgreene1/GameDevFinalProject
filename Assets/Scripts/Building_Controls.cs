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
    [SerializeField] GameObject enemySpawnerPrefab;
    [SerializeField] GameObject enemyMinePrefab;
    [SerializeField] GameObject enemyUnitPrefab;
    Faction faction;
    Spawner spawnScript;
    int row, col;
    Player_controls player;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        isOffense = true;
        row = 0;
        col = 0;
        sound = GetComponent<AudioSource>();
        spawnScript = spawnerPrefab.GetComponent("Spawner") as Spawner;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //creates a spawner prefab and adds it to the map, returns null if cannot be placed
    //should be called AFTER setting things with buttons
    public GameObject buildSpawnerPrefab(Player_controls builder){
            GameObject newSpawner;
            if(faction.showName() == "Enemy"){
                newSpawner = map.addBuilding(enemySpawnerPrefab, row, col);
            }
            else{
                newSpawner = map.addBuilding(spawnerPrefab, row, col);  
                if(newSpawner != null) sound.Play();
            }
            if(newSpawner == null) Debug.Log("Spawner can't be placed at "+col+", "+row);
            else{
                spawnScript = newSpawner.GetComponent<Spawner>();
                spawnScript.setVals(isOffense, unitPrefab, faction, builder);
                builder.gainSpawner(spawnScript);
            }
            return newSpawner;
    }


    //creates a mine prefab and adds it to the map, returns null if cannot be placed
    //should be called AFTER setting things with buttons
    public GameObject buildMinePrefab(Player_controls builder){

        GameObject newMine;
        if(faction.showName() == "Enemy"){
            newMine = map.addBuilding(enemyMinePrefab, row, col);
        }
        else{
            newMine = map.addBuilding(minePrefab, row, col);
        }
        if(newMine != null){
            Mine mineScript = newMine.GetComponent<Mine>();
            mineScript.setFaction(builder.showFaction());
            mineScript.setPlayer(builder);
            builder.gainMine(mineScript);
            return newMine;
        }
        return newMine;

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

    public GameObject getMine()
    {
        return enemyMinePrefab;
    }
    public void setPlayer(Player_controls owner)
    {
        player = owner;
    }


    public GameObject getPlayerSpawner()
    {
        return spawnerPrefab;
    }
    public GameObject getPlayerMine()
    {
        return minePrefab;
    }
    public GameObject getPlayerUnit1()
    {
        return spawnerPrefab;
    }
    public GameObject getEnemySpawner()
    {
        return enemySpawnerPrefab;
    }
    public GameObject getEnemyMine()
    {
        return enemyMinePrefab;
    }
    public GameObject getEnemyUnit1()
    {
        return enemyUnitPrefab;
    }



    }
