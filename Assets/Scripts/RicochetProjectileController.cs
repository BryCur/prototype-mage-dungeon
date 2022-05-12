using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetProjectileController : ProjectileController
{
    // Start is called before the first frame update

    [SerializeField] protected int max_rebound = 2;
    [SerializeField] protected int remaining_rebound;
    void Start()
    {
        base.projectileSpeed = 150f;
        remaining_rebound = max_rebound;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected new void OnCollisionEnter(Collision other){
        // TODO look for closest enemy
        GameObject closest_enemy = null;

        if(closest_enemy == null || remaining_rebound == 0){
            // no enemy or rebound, so we destroy it
            base.OnCollisionEnter(other);
        } else {
            remaining_rebound--;

            // TODO change target/direction of projectile
            // TODO damage 
        }
    }
}
