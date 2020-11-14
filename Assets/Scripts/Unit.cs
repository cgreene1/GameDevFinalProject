using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float health;
    private float speed;
    private float range;
    private float attackPower;
    private bool charging;
    private GameObject target;
    private Vector2 camp;
    private Unit enemyScript;
    public (int, int) upkeep;


    // Start is called before the first frame update
    void Awake()
    {
        health = 100f;
        speed = 3f;
        range = 0.8f;
        attackPower = 2f;
        target = null;
        charging = false;

        upkeep = (1, 1);

        camp = new Vector2(Random.Range(-3f, -5f), Random.Range(-1f, 1f));
    }


    // Update is called once per frame
    void FixedUpdate(){


        if(health <= 0){
            dies();
        }


        if(gameObject.tag == "Player"){
            //if a player unit is defending
            if(!charging){
                transform.position = Vector2.MoveTowards(transform.position, camp, speed * Time.deltaTime); //might need this in a coroutine
            }

            //if player unit is charging
            else{
                if(target == null){
                    findClosest("Enemy");
                }
                //move towards target if its out of range
                else if(Vector2.Distance(this.transform.position, target.transform.position) > range){
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                    //Debug.Log("dist = " + Vector2.Distance(this.transform.position, target.transform.position));

                }
                //attack if it's in range
                else if(Vector2.Distance(this.transform.position, target.transform.position) <= range){
                    transform.position = this.transform.position;
                    attack();
                }
            }
        }

        //if an enemy unit
        else{
            if(target == null){
                    findClosest("Player");
            }
            //move towards target if its out of range
            else if(Vector2.Distance(this.transform.position, target.transform.position) > range){
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                //Debug.Log("dist = " + Vector2.Distance(this.transform.position, target.transform.position));
            }
            //attack if it's in range
            else if(Vector2.Distance(this.transform.position, target.transform.position) <= range){
                transform.position = this.transform.position;
                attack();
            }

        }
        
    }


    //here is the shortest path to find the closest enemy when called
    private void charge(){
        if(gameObject.tag != "Enemy")
            charging = true;
    }

    private void attack(){
            hit(target);
    }

    private void getHit(float dmg){
        health -= dmg;
    }

    public void hit(GameObject target){
        enemyScript.getHit(attackPower);
    }

    private void findClosest(string str){

        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag(str)); //change for AI

        //takes in all enemies on the field and calculates distance
        GameObject closestEnemy = null;
        float dist = Mathf.Infinity;

        foreach(GameObject enemy in enemies){
            float enemyDist = Vector2.Distance(transform.position, enemy.transform.position);
            if(enemyDist < dist){

                closestEnemy = enemy;
                dist = enemyDist;
            }
        }

        target = closestEnemy;
        enemyScript = closestEnemy.GetComponent<Unit>();
    }

    public void dies(){
        Unit me = this;
        Destroy(gameObject);
    }

    public (int,int) showUpkeep()
    {
        return (0,0);
    }
    // TODO TODO
    public (int,int) findLocation()
    {
        return (0, 0);
    }
}
