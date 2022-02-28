using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private GameObject target;
    protected float movespeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posTarget = target.transform.position;

        Vector3 targetDirection = (posTarget - transform.position).normalized;

        transform.position += targetDirection * Time.deltaTime * movespeed;
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("enemy collision?");
        if(other.gameObject.CompareTag("projectile")){
            Debug.Log("enemy taking damage");
            Destroy(gameObject);
        }
    }
}
