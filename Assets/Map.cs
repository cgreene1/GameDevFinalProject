using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    GameObject[,] buildings; //2D array of buildings based on tiles - i.e. buildings[3][4] gets whichever building occupies tile 4,3, or null if nothing is there

    public int tileSize; // size of tile in pixels
    public int mapSizeX, mapSizeY; // size of map in tiles


    // Start is called before the first frame update
    void Start()
    {

        tileSize = 32;
        buildings = new GameObject[mapSizeY,mapSizeX];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (int row, int col) tilePosition(Renderer r){
        Vector3 pos = r.bounds.min;
        return (Mathf.RoundToInt(pos.y / tileSize), Mathf.RoundToInt(pos.x / tileSize));
    }

    public (int x, int y) tileScale(Renderer r){
        Vector3 size = r.bounds.size;
        return (Mathf.RoundToInt(size.x / tileSize), Mathf.RoundToInt(size.y / tileSize));
    }

//check if building can be placed
    bool canPlace(Spawner build){
        (int row, int col) = build.getLocation();
        (int sizerow, int sizecol) = build.getSize();
        for(int i=row; i<sizerow; i++){
            for(int j=col; j<sizecol; j++){
                if(buildings[i,j] != null) return false;
            }
        }
        return true;
    }

    bool canPlace(Mine build){
        (int row, int col) = build.getLocation();
        (int sizerow, int sizecol) = build.getSize();
        for(int i=row; i<sizerow; i++){
            for(int j=col; j<sizecol; j++){
                if(buildings[i,j] != null) return false;
            }
        }
        return true;
    }


 //check how many units are in a square (can be changed to just t/f if wanted), syntax works depends on return types of getLocation and getSize
 //we might not use this but i already wrote it when i realized that so oh well
 //i know this would be faster if things were stored in a 2D array so thats a thing we can also do but it would require stuff in player class
    // int unitsInTile(int x, int y){
    //     int population = 0;
    //     foreach(Unit unit in units){
    //         if(unit.getLocation.x >= x && unit.getLocation.row+unit.getSize.x <= x){
    //             if (unit.getLocation.y >= y && unit.getLocation.row+unit.getSize.y <= x) population++;
    //         }
    //     }
    //     return population;
    // }


//add a building to the map display, checking for whether tile is occupied
// returns building so player can add it to list of spawners/mines, OR null if the space is occupied.
//prefab for spawner should point to the UNIT PREFAB, which should contain the UNIT SCRIPT
    Spawner addSpawner(GameObject prefab, bool isOffense, int spawnDelay, int maxHealth){
        if(canPlace(spawn)){
            int row = spawn.getLocation().row;
            int col = spawn.getLocation().col;
            int sizerow = spawn.getSize().y;
            int sizecol = spawn.getSize().x;
            int mapRow = row * tileSize;
            int mapCol = row * tileSize;
            GameObject newChild = Instantiate(prefab, new Vector3(mapCol, mapRow, 0), new Quaternion (0,0,0,1));
            Spawner script = newChild.AddComponent<Spawner>() as Spawner;
            script.setVals(isOffense, spawnDelay, maxHealth);
            for(int i=row; i<sizerow; i++){
                for(int j=col; j<sizecol; j++){
                    buildings[i,j] = newChild;
                }
            }
            return spawn;
        }
        return null;
    }

//prefab should be different depending on resource type
    Mine addMine(Mine mine, GameObject prefab){
        if(canPlace(mine)){
            int row = mine.getLocation().row;
            int col = mine.getLocation().col;
            int sizerow = mine.getSize().y;
            int sizecol = mine.getSize().x;
            int mapRow = row * tileSize;
            int mapCol = row * tileSize;
            GameObject newChild = Instantiate(prefab, new Vector3(mapCol, mapRow, 0), new Quaternion (0,0,0,1));
            Mine script = newChild.AddComponent<Mine>() as Mine;
            for(int i=row; i<sizerow; i++){
                for(int j=col; j<sizecol; j++){
                    buildings[i,j] = newChild;
                }
            }
            return mine;
        }
        return null;
    }

//removes a building from the map display
    void destroyBuilding(GameObject obj){
        Mine build = obj.GetComponent<Mine>();
        if(build == null) {
            Spawner build = obj.GetComponent<Spawner>();
        }
        int row = build.getLocation().row;
        int col = build.getLocation().col;
        int sizerow = build.getSize().y;
        int sizecol = build.getSize().x;
        for(int i=row; i<sizerow; i++){
            for(int j=col; j<sizecol; j++){
                buildings[i,j] = null;
            }
        }
        Destroy(obj);
    }

//changes the scene to victory or defeat depending on if the winner was human
//does NOT check to see if the game is won - i imagine that goes in Player so they can easily trigger victory when last building dies
//isHuman can probably be set to false by default and then be set to True by a Controller class? dunno
    void wonGame(bool isHuman){
        if(isHuman) SceneManager.LoadScene("victory");
        else SceneManager.LoadScene("Defeat");
    }


}
