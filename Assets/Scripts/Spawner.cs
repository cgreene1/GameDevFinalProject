﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool isOffense;
    [SerializeField] int spawnDelay;
    private int maxHealth;
    private int currentHealth;
    private int resourceDrain;
    private (int, int) cost;
    private int timer;
    public GameObject unitPrefab;
    Transform trans;
    Faction faction;
    // Player player;
    Renderer render;
    Map map;

    int population;

    // Start is called before the first frame update
    void Start()
    {
        if(isOffense){
            //do stuff here to set units to be offensive
        }
        map = GameObject.Find("Map").GetComponent<Map>();
        render = gameObject.GetComponent<Renderer>();
        trans = gameObject.GetComponent<Transform>();
        population = 0;
        cost = (1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if(timer > spawnDelay){
            spawn();
            timer = 0;
        }
    }

    public void setVals(bool off, GameObject pre, Faction fact){
        unitPrefab = pre;
        isOffense = off;
        faction = fact;
    }

    public (int row, int col) getLocation(){
        return map.tilePosition(render);
    }

    public (int x, int y) getSize(){
        return map.tileScale(render);
    }


    void spawn(){
        GameObject newUnit = Instantiate(unitPrefab, new Vector3(trans.position.x, trans.position.y+1, 0), Quaternion.identity);
        newUnit.tag = faction.showName();
        Debug.Log(newUnit.tag);
    }

    public (int, int) showCost()
    {
        return cost;
    }

    public GameObject showUnitPrefab()
    {
        return unitPrefab;
    }
}
