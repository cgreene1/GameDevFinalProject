using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : MonoBehaviour
{
    private float health;
    private float speed;
    private float range;     //adjusts circlecast radius
    private float attackPower;
    private GameObject target;
    private Unit enemyScript;
    public (int, int) upkeep;
    [SerializeField] int faction = 1;


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

        List<Unit> enemyScripts = new List<Unit>();
        enemyScripts.AddRange(GameObject.FindObjectsOfType<Unit>().Where(u => u.getFaction() != faction)); //change for AI
        List<GameObject> enemies = new List<GameObject>();
        foreach(Unit enemy in enemyScripts){
            enemies.Add(enemy.gameObject);
        }
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
        Unit me = this;
      //  Player_controls.LoseUnit(me); //player would call += 
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

    public int getFaction(){
        return faction;
    }

}
