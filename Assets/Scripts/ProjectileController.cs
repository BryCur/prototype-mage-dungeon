using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * projectileSpeed * Time.deltaTime;


    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    protected void OnCollisionEnter(Collision other) {
        Debug.Log("projectile collided");
        Destroy(gameObject);
    }
}
