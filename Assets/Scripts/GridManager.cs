using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Sprite sprite;
    public int[,] Grid;
    int Vertical, Horizontal, Columns, Rows;
    // Start is called before the first frame update
    public void GridSpawn()
    {
       // Debug.Log("Gridspawn Called");
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * Screen.width / Screen.height;
        Columns = Horizontal * 4;
        Rows = Vertical * 4;
        Grid = new int[Columns, Rows];
        for (int i = 0; i < Columns; i++)
        {
           // Debug.Log("in first for loop");
            for (int j = 0; j < Rows; j++)
            {
                //Debug.Log("in second for loop");
                Grid[i, j] = Random.Range(0, 10);
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }
    public void SpawnTile(int x, int y, int value)
    {
        //ebug.Log("spawntile called");
        GameObject g = new GameObject("x: " + x + "y: " + y);
        g.tag = "Coords";
        g.transform.position = new Vector3(x - (Horizontal - 6.0f), y - (Vertical + 2.0f));
        var s = g.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        s.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0.3f);
    }

    public void DestroyGrid() {
        GameObject[] coords = GameObject.FindGameObjectsWithTag("Coords");
        foreach (GameObject coord in coords)
        {
            Destroy(coord);
        }
    }
}
