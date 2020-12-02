﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Transform trans, show;
    Faction faction;
    Renderer render;
    Map map;
    Player_controls player;
    bool isRare;
    (int, int) income;
    (int, int) cost;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        render = gameObject.GetComponent<Renderer>();
        trans = gameObject.GetComponent<Transform>();
        isRare = false;
        // set income
        income = (10, 10);
        cost = (10, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public (int row, int col) getLocation(){
        return map.tilePosition(render);
    }

    public (int x, int y) getSize(){
        return map.tileScale(render);
    }
    public (int,int) showIncome()
    {
        return income;
    }
    public (int,int) showCost()
    {
        return cost;
    }
    public void setFaction(Faction fact)
    {
        faction = fact;
    }
    public void setPlayer(Player_controls owner)
    {
        player = owner;
    }
}
