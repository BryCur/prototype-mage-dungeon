using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimarysourceController : MonoBehaviour
{

    public GameObject player;
    public GameObject pfProjectile;
    private float offset = 2.0f;
    private float lastShootTime = 0.0f;
    private float shootCooldown = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position  + transform.right * offset;

        PlayerController pController = player.GetComponent<PlayerController>();

        if(Input.GetAxis("Fire1") > 0 && pController.getCurrentStance() != PlayerController.Stance.Agile && Time.time > lastShootTime + shootCooldown){
            Fire();
        }
    }


    private void Fire(){
        Debug.Log("fire primary");
        Instantiate(pfProjectile, transform.position, transform.rotation);
        lastShootTime = Time.time;
    }

}
