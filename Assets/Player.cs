// using System.Collections;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using UnityEditor.U2D.Common;
// using UnityEngine;

// public class Player : MonoBehaviour
// {
//     private LinkedList<Spawner_Controls> spawnerList;
//     private LinkedList<Mine_Controls> mineList;
//     // resource at index 0 is common index 1 is less common
//     private LinkedList<int> stockpile;
//     private LinkedList<int> income;
//     private LinkedList<int> upkeep;
//     private LinkedList<Unit> army;
//     private LinkedList<Unit> garrison;
//     private Faction faction;
//     private bool bankrupt;
//     private bool human;
//     // Start is called before the first frame update
//     void Start()
//     {
//         setBank();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         finances();
//     }

//     // set default values for income upkeep and stockpile
//     private void setBank()
//     {
//         // set starting resources: common first then rare
//         stockpile.AddFirst(0);
//         stockpile.AddLast(0);
//         // set base income: common first then rare
//         income.AddFirst(0);
//         income.AddLast(0);
//         // set base upkeep: common first then rare
//         upkeep.AddFirst(0);
//         upkeep.AddLast(0);
//         // set bankrupt status
//         bankrupt = false;
//     }

//     //Handle income and upkeep for the player will declare bankrupt if less than 0 of a single resource
//     private void finances()
//     {
//         int common_change = (income.First.Value - upkeep.First.Value);
//         int rare_change = (income.Last.Value - upkeep.Last.Value);
//         stockpile.First.Value += common_change;
//         stockpile.Last.Value += rare_change;
//         if (stockpile.First.Value < 0 || stockpile.Last.Value < 0)
//         {
//             bankrupt = true;
//         }
//         else bankrupt = false;

//     }

//     // modify common income
//     public void changeIncomeCommon(int gained)
//     {
//         income.First.Value += gained;
//     }
//     // modify rare income
//     public void changeIncomeRare(int gained)
//     {
//         income.Last.Value += gained;
//     }

//     // modify common upkeep
//     public void changeUpkeepCommon(int loss)
//     {
//         upkeep.Last.Value += loss;
//     }
//     // modify rare upkeep
//     public void changeUpkeepRare(int loss)
//     {
//         upkeep.Last.Value += loss;
//     }

//     // tell others if Bankrupt
//     public bool isBankrupt()
//     {
//         return bankrupt;
//     }

//     // tell all offensive units to attack target player!
//     public void charge(Player target)
//     {
//         foreach (Unit x in army)
//         {
//             x.charge(target);
//         }
//     }
//     // a way to check if the player has lost
//     public bool lost()
//     {
//         if (spawnerList.Count == 0) return true; else return false;
//     }

//     // when a spawner is destoryed remove it from the list might use lost
//     public void loseSpawner(Spawner_Controls building)
//     {
//         if (spawnerList.Contains(building))
//         {
//             spawnerList.Remove(building);
//         }
//         else
//         {
//             Debug.Log("A spawner deletion error has happend");
//         }
//     }

//     // when a mine is destroyed remove it from the list, Income should be modififed by the mine
//     public void loseMine(Mine_Controls mine)
//     {
//         if (mineList.Contains(mine))
//         {
//             mineList.Remove(mine);
//         }
//         else
//         {
//             Debug.Log("A mine deletion error has happend");
//         }
//     }
//     // when a attacker unit is destroyed remove it from this list
//     public void loseUnit(Unit soldier)
//     {
//         if (army.Contains(soldier))
//         {
//             army.Remove(soldier);
//         }
//         else if (garrison.Contains(soldier))
//         {
//             garrison.Remove(soldier);
//         }
//         else
//         {
//             Debug.Log("A unit deletion error has happend");
//         }
//     }

//     // when a spawner is created add it to the list
//     public void gainSpawner(Spawner_Controls building)
//     {
//         spawnerList.AddFirst(building);
//     }

//     // when a mine is added, add it to the list
//     public void gainMine(Mine_Controls building)
//     {
//         mineList.AddFirst(building);
//     }

//     // when an attack is spawned add it to the army list
//     public void gainAttacker(Unit soldier)
//     {
//         army.AddFirst(soldier);
//     }
//     // when an defender is spawned add it to the garrison list
//     public void gainDefender(Unit soldier)
//     {
//         garrison.AddFirst(soldier);
//     }
//     // finds the location of all buildings and units and returns a linked list of the cordinates
//     public LinkedList<(int,int)> locateAssets()
//     {
//         LinkedList<(int,int)> targets;

//         foreach (unit soldier in army)
//         {
//             targets.AddFirst(soldier.findLocation());
//         }
//         foreach (Unit soldier in garrison)
//         {
//             targets.AddFirst(soldier.findLocation());
//         }
//         foreach (Spawner_Controls building in spawnerList)
//         {
//             targets.AddFirst(building.findLocation());
//         }
//         foreach (Mine_Controls building in mineList)
//         {
//             targets.AddFirst(building.findLocation());
//         }
//         return targets;
//     }

// }

