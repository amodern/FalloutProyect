using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

    public AnimationClip idle, run, shoot;
    public Transform target, rayCastPoint;
    public GameObject[] waypoints;
    public GameObject lootBagPref;

    Animation anim;
    NavMeshAgent agent;

    int curWaypoint = 0, searchTargetTime = 12;
    float nextFireTime, stopSearchingTarget;
    
    //stats
    int health = 500;
    //for anim
    bool isDead = false, moving = false, shooting = false;
    bool targetSeen = false, canSeeTarget = false;
    public BaseWeapon weapon;

	// Use this for initialization
	void Start () {
        weapon = new BaseWeapon();
        weapon.name = "gun";
        weapon.prefabIndex = 1;
        weapon.magSize = 20;
        weapon.fireRate = 0.2f;
        weapon.damage = 100;
        weapon.iconIndex = 1;
        weapon.quality = BaseItem.ItemQuality.Common;
        weapon.type = BaseItem.ItemType.Weapon;
        weapon.value = 200;
        weapon.weigth = 9;
        weapon.wpType = BaseWeapon.WeaponType.Rifle;

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;
        anim.Play(idle.name);
	}
	
	// Update is called once per frame
	void Update () {
	    if(health <= 0)
        {
            if (!isDead)
            {
                health = 0;
                Dead();
                isDead = true;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(rayCastPoint.transform.position, target.position-transform.position, out hit, 300f))
            {
                if (hit.transform.name == target.name)
                {
                    canSeeTarget = true;
                    targetSeen = true;
                    //Attack
                }
                else
                {
                    if(canSeeTarget)
                        stopSearchingTarget = Time.time + searchTargetTime;
                    canSeeTarget = false;
                    //Patrol or something
                }
            }
            //Debug.Log("RAycastHitTag: " + hit.transform.name);

            if (canSeeTarget)
            {
                float dist = Vector3.Distance(target.position, transform.position);
                if (25 < dist && dist < 100)
                {
                    agent.Resume();
                    agent.SetDestination(target.position);
                    moving = true;
                }
                else if (dist < 25)
                {
                    agent.Stop();
                    shooting = true;
                }

                if (shooting)
                {
                    anim.CrossFade(shoot.name);
                    shooting = false;
                    RotateTowardsTarget();
                }
                else if (moving)
                {
                    anim.CrossFade(run.name);
                    moving = false;
                }
                else
                {
                    anim.CrossFade(idle.name);
                }
            }
            else if(targetSeen)
            { 
                while(stopSearchingTarget <= Time.time)
                {
                    agent.Resume();
                    agent.SetDestination(target.position);
                    anim.CrossFade(run.name);
                }
            }
            else
            {
                if(!agent.hasPath)
                {
                    agent.Resume();
                    agent.SetDestination(waypoints[curWaypoint].transform.position);
                    anim.CrossFade(run.name);
                    if (curWaypoint < waypoints.Length - 1)
                        curWaypoint++;
                    else
                        curWaypoint = 0;
                }
            }
        }
	}

    void RotateTowardsTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target.position - transform.position).normalized), Time.deltaTime * 20);
    }

    void Dead()
    {
        transform.Rotate(new Vector3(40, 0, 90));
        DestroyObject(GetComponent<Collider>());
        DestroyObject(GetComponent<NavMeshAgent>());
        anim.Stop();
        GameObject t = Instantiate(lootBagPref, transform.position + new Vector3(1,0.3f,1), Quaternion.identity) as GameObject;
        t.transform.parent = transform;
        Debug.Log("SLODIER DEAD");
    }

    public void ApplyDamage(int dmg)
    {
        health -= dmg;
    }
}
