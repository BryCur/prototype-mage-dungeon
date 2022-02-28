using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondarySourceController : MonoBehaviour
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
        // rotate object according to horizontal and vertical input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(horizontalInput != 0 || verticalInput != 0){
            float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            // transform.Rotate(Vector3.up, angle);
        }

        transform.position = player.transform.position  + transform.right * offset;

        PlayerController pController = player.GetComponent<PlayerController>();

        if(Input.GetAxis("Fire2") > 0 && pController.getCurrentStance() == PlayerController.Stance.Stationary && Time.time > lastShootTime + shootCooldown){
            Fire();
        }
    }


    private void Fire(){
        // Debug.Log("fire secondary");
        Instantiate(pfProjectile, transform.position, transform.rotation);
        lastShootTime = Time.time;
    }
}
