using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float armour; 
    private float ap;
    private float numAttacks;
    private float health;
    private float speed;
    private float range;     //adjusts circlecast radius
    private float damage;
    private GameObject target;
    private Unit enemyScript;
    private (int, int) upkeep;
    private Player_controls player;
    private Map map;
    private Renderer render;
    // Start is called before the first frame update
    void Awake()
    {
        health = 100f;
        speed = 2f;
        range = 1f;
        damage = 5f;
        armour = 1f;
        ap = 1f;
        numAttacks = 1f;
        target = null;
        map = GameObject.Find("Map").GetComponent<Map>();
        render = gameObject.GetComponent<Renderer>();
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
    // you take take dmg
    private void getHit(Unit foe){
        float effectiveArmour = armour - foe.showAP();
        if(effectiveArmour < 0)
        { 
            effectiveArmour = 0f; 
        }
        float effectiveDmg = foe.showDmg() - effectiveArmour;
        if(effectiveDmg < 0.5f)
        {
            effectiveDmg = 0.5f;
        }
        health -= (effectiveDmg * foe.showNumAttacks());
        if(health <= 0)
        {
            dies();
        }
    }
    // you hit someone else
    public void hit(GameObject target){
        enemyScript.getHit(this);
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
        player.LoseUnit(this);
        Destroy(gameObject);
    }

    public void charge(LinkedList<(int, int)> targets)
    {
        (int, int) myLoc = findLocation();
        (int,int) closest = targets.First.Value;
        float closestDistance = Mathf.Sqrt((myLoc.Item1 - closest.Item1)^2 + (myLoc.Item2 - closest.Item2)^2);
        foreach ((int, int) target in targets)
        {
            float newDistance = Mathf.Sqrt((myLoc.Item1 - target.Item1) ^ 2 + (myLoc.Item2 - target.Item2) ^ 2);
            if(newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closest = target;
            }
        }
        Vector2 tmp = new Vector2(closest.Item1,closest.Item2);
        // move towards the target now please! then activate defensive funcitonality
        transform.position = Vector2.MoveTowards(transform.position, tmp, speed * Time.deltaTime);
        findClosest();
    }


    public (int,int) showUpkeep()
    {
        return (0,0);
    }

    public float showArmour()
    {
        return armour;
    }
    public float showAP()
    {
        return ap;
    }
    public float showNumAttacks()
    {
        return numAttacks;
    }
    public float showDmg()
    {
        return damage;
    }
    public float showHealth()
    {
        return health;
    }


    // TODO TODO
    public (int,int) findLocation()
    {
        return map.tilePosition(render);
    }










}
