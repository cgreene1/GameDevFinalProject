using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

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
    private Vector2 camp;
    // potental kinds of targets
    private Unit enemyScript;
    private Mine enemyMineScript;
    private Spawner enemySpawnerScript;
    [SerializeField] Faction faction;
    private (int, int) upkeep;
    private Player_controls player;
    private bool charging;
    private Map map;
    private Renderer render;
    private int unitType;

    // Start is called before the first frame update
    void Awake()
    {
        health = 100f;
        target = null;
        charging = gameObject.tag == "Enemy";
        speed = 2f;
        range = 1f;
        damage = 5f;
        armour = 1f;
        ap = 1f;
        numAttacks = 1f;
        target = null;
        map = GameObject.Find("Map").GetComponent<Map>();
        render = gameObject.GetComponent<Renderer>();
        unitType = 1;
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
    public void charge(){
        charging = true;
        Debug.Log("Unit is charging");
    }
    

    private void attack(){
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
    public void hit(GameObject target)
    {
        if (enemyScript != null)
        {
            enemyScript.getHit(this);
        }else if(enemyMineScript != null)
        {
            enemyMineScript.getHit(this);
            if(this.tag == "Player")
            {
                findClosest("Enemy");
            } else
            {
                findClosest("Player"); 
            }
        } else if(enemySpawnerScript != null)
        {
            enemySpawnerScript.getHit(this);
            if (this.tag == "Player")
            {
                findClosest("Enemy");
            }
            else
            {
                findClosest("Player");
            }
        } else
        {
            Debug.Log("Targeting failure");
        }
    }

    private void findClosest(string str) {

        List<Unit> enemyScripts = new List<Unit>();
        enemyScripts.AddRange(GameObject.FindObjectsOfType<Unit>().Where(u => u.CompareTag(str))); //change for AI
        List<GameObject> enemies = new List<GameObject>();

        foreach(Unit enemy in enemyScripts){
            enemies.Add(enemy.gameObject);
        }
       
        enemies.AddRange(GameObject.FindGameObjectsWithTag(str)); //change for AI
        //takes in all enemies on the field and calculates distance
        if(enemies.Count > 0){
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
            if(enemyScript == null)
            {
                enemyMineScript = closestEnemy.GetComponent<Mine>();
     
            }
            if(enemyMineScript == null)
            {
                enemySpawnerScript = closestEnemy.GetComponent<Spawner>();
                
            }
        }

    }

    public void dies(){
        Unit me = this;
        if (player != null)
        {
            player.LoseUnit(this);
        }
        Destroy(this.gameObject);
    }

    public void charge(LinkedList<(int, int)> targets)
    {
        charging = true;
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

    public bool showCharge()
    {
        return charging;
    }

    // TODO TODO
    public (int,int) findLocation()
    {
        return map.tilePosition(render);
    }

    public Faction getFaction(){
        return faction;
    }

    public void getPlayer(Player_controls owner)
    {
        player = owner;
    }
    public int showType()
    {
        return unitType;
    }
}
