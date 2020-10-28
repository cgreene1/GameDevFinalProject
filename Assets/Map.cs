using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    Building[,] buildings; //2D array of buildings based on tiles - i.e. buildings[3][4] gets whichever building occupies tile 4,3, or null if nothing is there

    private int tileSize; // size of tile in pixels
    private int mapSize; // size of map in tiles



    // Start is called before the first frame update
    void Start()
    {
        tileSize = 32;
        mapSize = 32;
        buildings = new Building[mapSize,mapSize];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

//check if building can be placed
    bool canPlace(Building build){
        int row = build.getLocation.y;
        int col = build.getLocation.x;
        int sizerow = build.getSize.y;
        int sizecol = build.getSize.x;
        for(int i=row; i<sizerow; i++){
            for(int j=col; j<sizecol; j++){
                if(buildings[i][j] != null) return false;
            }
        }
        return true;
    }

 //check how many units are in a square (can be changed to just t/f if wanted), syntax works depends on return types of getLocation and getSize
 //we might not use this but i already wrote it when i realized that so oh well
 //i know this would be faster if things were stored in a 2D array so thats a thing we can also do but it would require stuff in player class
    int unitsInTile(int x, int y){
        int population = 0;
        foreach(Unit unit in units){
            if(unit.getLocation.x >= x && unit.getLocation.row+unit.getSize.x <= x){
                if (building.getLocation.y >= y && building.getLocation.row+building.getSize.y <= x) population++;
            }
        }
        return population;
    }

//add a building to the map display, checking for whether tile is occupied
// returns building so player can add it to list of spawners/mines, OR null if the space is occupied.
// TODO actually create the GameObject of the building! right now this just adds it to the map's list of buildings
    Building addBuilding(Building build){
        if(canPlace(build)){
            int row = build.getLocation.y;
            int col = build.getLocation.x;
            int sizerow = build.getSize.y;
            int sizecol = build.getSize.x;
            for(int i=row; i<sizerow; i++){
                for(int j=col; j<sizecol; j++){
                    buildings[i][j] = build;
                }
            }
            
            return build;
        }
        return null;
    }

//removes a building from the map display
//TODO actually destroy the physical GameObject
    void destroyBuilding(Building build){
        int row = build.getLocation.y;
        int col = build.getLocation.x;
        int sizerow = build.getSize.y;
        int sizecol = build.getSize.x;
        for(int i=row; i<sizerow; i++){
            for(int j=col; j<sizecol; j++){
                buildings[i][j] = null;
            }
        }
    }

//changes the scene to victory or defeat depending on if the winner was human
//does NOT check to see if the game is won - i imagine that goes in Player so they can easily trigger victory when last building dies
//isHuman can probably be set to false by default and then be set to True by a Controller class? dunno
    void wonGame(bool isHuman){
        if(isHuman) SceneManager.LoadScene("victory");
        else SceneManager.LoadScene("Defeat");
    }

}
