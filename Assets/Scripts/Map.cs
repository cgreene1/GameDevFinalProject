using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    List<GameObject> buildings; //2D array of buildings based on tiles - i.e. buildings[3][4] gets whichever building occupies tile 4,3, or null if nothing is there

    Tilemap map;
    public int tileSize;


    // Start is called before the first frame update
    void Start()
    {
        map  = GetComponent<Tilemap>();
        buildings = new List<GameObject>();
        tileSize = 16;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (int row, int col) tilePosition(Renderer r){
        Vector3 pos = r.bounds.min;
        return (Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.x));
    }

    public (int x, int y) tileScale(Renderer r){
        Vector3 size = r.bounds.size;
        return (Mathf.RoundToInt(size.x), Mathf.RoundToInt(size.y));
    }

//check if building can be placed
    public bool canPlace(int row, int col, int sizecol, int sizerow){
        Vector3 pos = new Vector3((float)col, (float)row, 0f);
        foreach (GameObject build in buildings){
            Debug.Log(build);
            Renderer rend = build.GetComponent<Renderer>();
            if(rend.bounds.Contains(pos)){
                return false;
            }

        }
        Debug.Log(col+" "+row);
        if(col < 0) col--;
        if(col>0) col++;
        if(row<0) row--;
        if(row>0) row++;
        Vector3Int intPos = new Vector3Int(col, row, 0);
        return map.cellBounds.Contains(intPos);
        // return true;
    }

    public (int col, int row) getSize(){
        map.CompressBounds();
        return (map.size.y, map.size.x);
    }


//add a building to the map display, checking for whether tile is occupied
// returns spawner so player can add it to list of spawners/mines, OR null if the space is occupied.
//prefab for building should have spawner script with player-chosen params in it or mine script with all resource info
//requires input for the position IN TILES of the spawner to be placed
    public GameObject addBuilding(GameObject prefab, int row, int col){
        Debug.Log(prefab.GetComponent<Renderer>());
       Renderer r = prefab.GetComponent<Renderer>();
        (int sizex, int sizey) = tileScale(r);
        if(canPlace( row,  col, sizex, sizey)){
            GameObject newObj = Instantiate(prefab, new Vector3(col, row, 0), Quaternion.identity);
            buildings.Add(newObj);
            return newObj;
        }
        return null;
    }
        
    


//removes a building from the map display
    public void destroyBuilding(GameObject obj){
        buildings.Remove(obj);
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
