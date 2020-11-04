using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float health;
    private float speed;
    private float range;     //adjusts circlecast radius
    private float attackPower;
    private GameObject target;
    private Unit enemyScript;
    public (int, int) upkeep;


    // Start is called before the first frame update
    void Awake()
    {
        health = 100f;
        speed = 2f;
        range = 1f;
        attackPower = 5f;
        target = null;

        upkeep = (1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health <= 0){
            dies();
        }
        else if(target == null)
            findClosest();
        else
            attack();
        
    }

    //here is the shortest path to find the closest enemy when called
    private void attack(){
        //move towards target if it's out of range
        if(Vector3.Distance(transform.position, target.transform.position) > range)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        else
            hit(target);
    }

    private void getHit(float dmg){
        health -= dmg;
    }

    public void hit(GameObject target){
        enemyScript.getHit(attackPower);
    }

    private void findClosest(){

        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("enemy")); //change for AI

        //takes in all enemies on the field and calculates distance
        GameObject closestEnemy = null;
        float dist = Mathf.Infinity;

        foreach(GameObject enemy in enemies){
            float enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
            if(enemyDist < dist){

                closestEnemy = enemy;
                dist = enemyDist;
            }
        }

        target = closestEnemy;
        enemyScript = closestEnemy.GetComponent<Unit>();
    }

    public void dies(){
        Player.loseUnit(gameObject); //player would call += 
        Destroy(gameObject);
    }
}
