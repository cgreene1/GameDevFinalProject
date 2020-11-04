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
        else if(target == null && GameObject.Find(target) != null)
            findClosest();
        else
            attack();
        
    }

    //here is the shortest path to find the closest enemy when called
    private void attack(){
        //move towards target if it's out of range
        if(Vector3.Distance(position, target) > range)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else
            hit(target);
    }

    private void getHit(int dmg){
        health -= dmg;
    }

    public void hit(GameObject target){
        target.getHit(attackPower);
    }

    private void findClosest(){

        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("enemies")); //change for AI

        //takes in all enemies on the field and calculates distance
        GameObject closestEnemy;
        float dist = Mathf.Infinity;

        foreach(GameObject enemy in enemies){
            float enemyDist = Vector3.Distance(position, enemy.transform.position);
            if(enemyDist < dist){

                closestEnemy = enemy;
                dist = enemyDist;
            }
        }

        target = closestEnemy;
    }

    public void changeUpkeep(){
        upkeep.First.Value *= -1;
        upkeep.Last.Value *= -1;
    }

    public void dies(){
        changeUpkeep;
        Player.loseUnit(gameObject); //player would call += unit.upkeep.First.Value & unit.upkeep.Last.Value
        Destroy(gameObject);
    }
}
