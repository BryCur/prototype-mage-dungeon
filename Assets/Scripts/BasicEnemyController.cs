using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemyController : MonoBehaviour
{
    
    private GameObject target;
    public float currentHealth = 10;
    public float maxHealth = 10;
    public GameObject goHealthBar;
    public Slider healthSlider;
    protected float movespeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        
        currentHealth = 10;
        maxHealth= 10;
        goHealthBar.SetActive(false);
        healthSlider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posTarget = target.transform.position;

        Vector3 targetDirection = (posTarget - transform.position).normalized;

        transform.position += targetDirection * Time.deltaTime * movespeed;
        
        // FIXME rotate enemy so they face the player - rotation is sloppy...
        Quaternion rot = Quaternion.LookRotation(posTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("enemy collision?");
        if(other.gameObject.CompareTag("projectile")){
            Debug.Log("enemy taking damage");

            currentHealth--;
            
            if(currentHealth < maxHealth){
                goHealthBar.SetActive(true);
            }

            if(currentHealth <= 0){
                Destroy(gameObject);
            }

            if(currentHealth > maxHealth){
                currentHealth = maxHealth;
            }

            healthSlider.value = CalculateHealth();

            Destroy(other.gameObject);
        }
    }

    private float CalculateHealth(){
        return currentHealth / maxHealth;
    }
}
