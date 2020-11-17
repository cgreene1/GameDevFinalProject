using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Common;
using UnityEngine;

public class Player_controls : MonoBehaviour
{
    private LinkedList<Spawner> spawnerList;
    private LinkedList<Mine> mineList;
    // resource at index 0 is common index 1 is less common (called Rare)
    private List<int> stockpile;
    private List<int> income;
    private List<int> upkeep;
    private LinkedList<Unit> army;
    private LinkedList<Unit> garrison;
    private Faction faction;
    private bool bankrupt;
    private bool human;

    GameObject spawner1;


    // Start is called before the first frame update
    void Start()
    {
        spawner1 = faction.getSpawnPrefab();
        setBank();
        army = new LinkedList<Unit>();
        garrison = new LinkedList<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        finances();
    }

    // set default values for income upkeep and stockpile
    private void setBank()
    {
        // set starting resources: common first then rare
        stockpile[0] = 200;
        stockpile[1] = 100;
        // set base income: common first then rare
        income[0] = 10;
        income[1] = 5;
        // set base upkeep: common first then rare
        upkeep[0] = 0;
        upkeep[1] = 0;
        // set bankrupt status
        bankrupt = false;
    }

    //Handle income and upkeep for the player will declare bankrupt if less than 0 of a single resource
    private void finances()
    {
        int common_change = (income[0] - upkeep[0]);
        int rare_change = (income[1] - upkeep[1]);
        stockpile[0] += common_change;
        stockpile[1] += rare_change;
        if (stockpile[0] < 0 || stockpile[1] < 0)
        {
            bankrupt = true;
        }
        else bankrupt = false;

    }

    // modify common income
    public void changeIncomeCommon(int gained)
    {
        income[0] += gained;
    }
    // modify rare income
    public void changeIncomeRare(int gained)
    {
        income[1] += gained;
    }

    // modify common upkeep
    public void changeUpkeepCommon(int loss)
    {
        upkeep[0] += loss;
    }
    // modify rare upkeep
    public void changeUpkeepRare(int loss)
    {
        upkeep[1] += loss;
    }

    // tell others if Bankrupt
    public bool isBankrupt()
    {
        return bankrupt;
    }

    // tell all offensive units to attack target player!

    public void charge(Player_controls target)
    {
        LinkedList<(int, int)> targets = target.locateAssets();
        foreach (Unit x in army)
        { 
           x.charge(targets);
        }
    }

    // a way to check if the player has lost
    public bool lost()
    {
        if (spawnerList.Count == 0) return true; else return false;
    }

    // when a spawner is destoryed remove it from the list might use lost
    public void loseSpawner(Spawner building)
    {
        if (spawnerList.Contains(building))
        {
            spawnerList.Remove(building);
        }
        else
        {
            Debug.Log("A spawner deletion error has happend");
        }
    }

    // when a mine is destroyed remove it from the list, Income should be modififed by the mine
    public void loseMine(Mine mine)
    {
        if (mineList.Contains(mine))
        {
            (int, int) losses = mine.showIncome();
            losses = (losses.Item1 * -1, losses.Item2 * -1);
            int check = changeResourcesIncome(losses);
            if (check == 0) Debug.Log("a mine had no income!");
            mineList.Remove(mine);
        }
        else
        {
            Debug.Log("A mine deletion error has happend");
        }
    }
    // when a attacker unit is destroyed remove it from this list
    public void LoseUnit(Unit soldier)
    {
        
        if (army.Contains(soldier))
        {
            (int, int) upkeep = soldier.showUpkeep();
            upkeep = (upkeep.Item1 * -1, upkeep.Item2 * -1);
            int check = changeResourcesIncome(upkeep);
            if (check == 0) Debug.Log("A Unit has 0 upkeep");
            army.Remove(soldier);
        }
        else if (garrison.Contains(soldier))
        {
            (int, int) upkeep = soldier.showUpkeep();
            upkeep = (upkeep.Item1 * -1, upkeep.Item2 * -1);
            int check = changeResourcesIncome(upkeep);
            if (check == 0) Debug.Log("A Unit has 0 upkeep");
            garrison.Remove(soldier);
        }
        else
        {
            Debug.Log("A unit deletion error has happend");
        }
    }

    // when a spawner is created add it to the list
    public void gainSpawner(Spawner building)
    {
        (int, int) cost = building.showCost();
        if (Math.Abs(cost.Item1) > 0)
        {
            this.stockpile[0] += (cost.Item1);
    
        }
        else if (Math.Abs(cost.Item2) > 0)
        {
            this.stockpile[1] += (cost.Item2);
         
        }
        else Debug.Log("spawner has 0 cost");
            spawnerList.AddFirst(building);
    }

    // when a mine is added, add it to the list
    public void gainMine(Mine building)
    {
        (int, int) cost = building.showCost();
        if (Math.Abs(cost.Item1) > 0)
        {
            this.stockpile[0] += (cost.Item1);

        }
        else if (Math.Abs(cost.Item2) > 0)
        {
            this.stockpile[1] += (cost.Item2);

        }
        else Debug.Log("mine has 0 cost");

        (int, int) ni = building.showIncome();
        int check = changeResourcesIncome(ni);
        if(check == 0 ) Debug.Log("Mine gained with no income!");
        mineList.AddFirst(building);
    }

    // when an attack is spawned add it to the army list
    public void gainAttacker(Unit soldier)
    {
        (int, int) upkeep = soldier.showUpkeep();
        int check = changeResourcesUpkeep(upkeep);
        if (check == 0) Debug.Log("a Unit has no upkeep!");
        army.AddFirst(soldier);
    }
    // when an defender is spawned add it to the garrison list
    public void gainDefender(Unit soldier)
    {
        (int, int) upkeep = soldier.showUpkeep();
        int check = changeResourcesUpkeep(upkeep);
        if (check == 0) Debug.Log("a Unit has no upkeep!");
        garrison.AddFirst(soldier);
    }
    // finds the location of all buildings and units and returns a linked list of the cordinates
     public LinkedList<(int,int)> locateAssets()
     {
         LinkedList<(int,int)> targets = new LinkedList<(int, int)>();

         foreach (Unit soldier in army)
         {
             targets.AddFirst(soldier.findLocation());
         }
         foreach (Unit soldier in garrison)
         {
             targets.AddFirst(soldier.findLocation());
         }
         /*
         foreach (Spawner building in spawnerList)
         {
             targets.AddFirst(building.findLocation());
         }
         foreach (Mine building in mineList)
         {
             targets.AddFirst(building.findLocation());
         }
         */
         return targets;
     }

    // takes the resources from something and chages the income
    int changeResourcesIncome((int,int) nr)
    {
        if (Math.Abs(nr.Item1) > 0)
        {
            this.changeIncomeCommon(nr.Item1);
            return 1;
        }
        else if (Math.Abs(nr.Item2) > 0)
        {
            this.changeIncomeRare(nr.Item2);
            return 1;
        }
        else return 0;
    }

    // takes the resources from something and changes the upkeep
    int changeResourcesUpkeep((int, int) nr)
    {
        if (Math.Abs(nr.Item1) > 0)
        {
            this.changeUpkeepCommon(nr.Item1);
            return 1;
        }
        else if (Math.Abs(nr.Item2) > 0)
        {
            this.changeUpkeepRare(nr.Item2);
            return 1;
        }
        else return 0;
    }

}

