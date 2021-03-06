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
    private Player_controls player;
    Renderer render;
    Map map;
    private bool active;
    [SerializeField] GameObject defaultUnit;
    private float hp;

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
        cost = (10, 10);
        hp = 2000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void setVals(bool off, GameObject pre, Faction fact, Player_controls owner){
        
        // if(this.tag != "Enemy") unitPrefab = pre;
        isOffense = off;
        faction = fact;
        this.tag = faction.showName();
        player = owner;
        cost = (10, 10);
        InvokeRepeating("spawn", spawnDelay, spawnDelay);
    }

    public (int row, int col) getLocation(){
        return map.tilePosition(render);
    }

    public (int x, int y) getSize(){
        return map.tileScale(render);
    }

    // need to check if the player is bankrupt
    void spawn(){
        GameObject newUnit = Instantiate(unitPrefab, new Vector3(trans.position.x, trans.position.y+1, 0), Quaternion.identity);
        newUnit.tag = this.tag;
        active = true;
        newUnit.GetComponent<Unit>().getPlayer(player);
            if (isOffense)
            {
                player.gainAttacker(newUnit.GetComponent<Unit>());
            }
            else if (!isOffense)
            {
                player.gainDefender(newUnit.GetComponent<Unit>());
            }
            else { Debug.Log("What am I a pacifist?"); }
        
    }

    public (int, int) showCost()
    {
        return cost;
    }
    public void stopSpawning()
    {
        CancelInvoke();
        active = false;
    }

    public void reStart()
    {
        if (!active)
        {
            spawn();
        } 
    }
    public GameObject showUnitPrefab()
    {
        return unitPrefab;
    }
    public void getHit(Unit enemy)
    {
        hp -= (enemy.showDmg() * enemy.showNumAttacks());
        if (hp <= 0)
        {
            CancelInvoke();
            Debug.Log(this.gameObject);
            player.loseSpawner(this);
            map.destroyBuilding(this.gameObject);
        }
    }
}
