using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    GameObject[,] buildings; //2D array of buildings based on tiles - i.e. buildings[3][4] gets whichever building occupies tile 4,3, or null if nothing is there

    Tilemap map;
    public int tileSize;


    // Start is called before the first frame update
    void Start()
    {
        map  = GetComponent<Tilemap>();
        buildings = new GameObject[map.size.y,map.size.x];

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
    public bool canPlace(int row, int col, int sizecol, int sizerow){
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
// returns spawner so player can add it to list of spawners/mines, OR null if the space is occupied.
//prefab for building should have spawner script with player-chosen params in it or mine script with all resource info
//requires input for the position IN TILES of the spawner to be placed
    public GameObject addBuilding(GameObject prefab, int row, int col){
        Renderer r = prefab.GetComponent<Renderer>();
        (int sizex, int sizey) = tileScale(r);
        if(canPlace( row,  col, sizex, sizey)){
            for(int i=row; i<sizey; i++){
                for(int j=col; j<sizex; j++){
                    buildings[i,j] = prefab;
                }
            }
            GameObject newObj = Instantiate(prefab, new Vector3(col*tileSize, row*tileSize, 0) + Vector3.down, Quaternion.identity);
            return newObj;
        }
        return null;
    }
        
    


//removes a building from the map display
    void destroyBuilding(GameObject obj){
        Renderer r = obj.GetComponent<Renderer>();
        (int row, int col) = tilePosition(r);
        (int sizecol, int sizerow) = tileScale(r);
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
