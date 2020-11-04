using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    private LinkedList<Unit> roster;
    private LinkedList<Building_Functionality> buildings;

    private GameObject[] unitPrefabs;
    [SerializeField] GameObject[] spawnerPrefabs;
    private LinkedList<GameObject> minePrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getSpawnPrefab(){
        return spawnerPrefabs[0];
    }
}
