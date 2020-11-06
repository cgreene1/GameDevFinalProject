using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject finalObject;

    private Vector2 mousePos;

    private bool isAnObjectSelected = false;

    private GameObject currentlySelectedObject;

    [SerializeField]
    private LayerMask allTilesLayer;
	
	// Update is called once per frame
	void Update ()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));

        if (Input.GetMouseButtonDown(0))
        {
             Instantiate(finalObject, transform.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy (GameObject.FindWithTag("Template"));
            this.enabled = false;
        }
	}

}
