using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**======================================
PlayerController
-----------------------------------------
Define the possible controls for the player character. 
Possible controls: 
    - WASD / left JS: Move the character. In Stationary stance: control the secondary shooting source (direction for controller, and shoot with KB&M)
    - LT (Left Trigger): in Stationary stance, shoot with the secondary source
    - shift: change stance, // TODO define the rotation: two key to go both ways, or one key for bot rotation?
    - space / LT : except in stationary stance, dash with a small invincibility time
    - mouse / right JS : primary shooting source (direction), has no effect in Agile stance.
    - left click / RT : shoot with the primary source
    - // TODO define key: in stationary stance use q powerful attack (ultimate)

    // TODO WHEN BASIC GAMEPLAY IMPLEMENTED: change shooting attribute for both sources
=========================================*/
public class PlayerController : MonoBehaviour
{
    // Possible player stance, list every stances possible for the player
    public enum Stance {
        Agile, // allows more mobility (higher movespeed,  multiple dash possible), but can't shoot anything
        Standard, // default stance, moderate anything (normal movement speed, dash under cooldown), can shoot from one source
        Stationary // powerful stance, can't move but has a secondary shooting source and can use "ultimate" ability if available
        // TODO define "ultimate" conditions... fill up a bar with damages? simple cooldown? need to gather specific collectibles?
        // TODO define "ultimate"... depending on shooting attributes? available loot
    };

    #region Constant declarations
    private const float STANDARD_MOVE_SPEED = 20.0f; // moving speed in standard stance
    private const float AGILE_MOVE_SPEED = 30.0f; // moving speed in agile stance
    private const float STATIONARY_MOVE_SPEED = 1.0f; // moving speed in satationary stance

    private const float STANDARD_DASH_FORCE = 200.0f; // Dash force in standard stance
    private const float STANDARD_DASH_DURATION = 0.1f; // Dash force in standard stance
    private const float STANDARD_DASH_INVICIBILITY_TIME = 30.0f; // Dash invincibility time in standard stance

    private const float AGILE_DASH_FORCE = 200.0f; // Dash force in agile stance
    private const float AGILE_DASH_DURATION = 0.1f; // Dash force in agile stance
    private const float AGILE_DASH_INVICIBILITY_TIME = 30.0f; // Dash invincibility time in agile stance
        
    #endregion

    private Stance currentStance = Stance.Standard; 
    private float currentMoveSpeed = STANDARD_MOVE_SPEED;
    private float currentDashForce = STANDARD_DASH_FORCE;
    private float currentDashDuration = STANDARD_DASH_DURATION;
    private float currentDashInvicibilityTime = STANDARD_DASH_INVICIBILITY_TIME;

    private Rigidbody rbPlayer;
    public GameObject compass;
    public GameObject primaryShootingSource;
    public GameObject secondaryShootingSource;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        changeStance(Stance.Standard);
    }

    // Update is called once per frame
    void Update()
    {
        // movement 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position += compass.transform.forward * -1 * Time.deltaTime * currentMoveSpeed * horizontalInput
                + compass.transform.right * Time.deltaTime * currentMoveSpeed * verticalInput;

        
        if(Input.GetKeyDown(KeyCode.Space)){
            // how to do a dash?
            StartCoroutine(PerformDash());
        }

        // manage the player stance
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            int nextStance = (int)(currentStance + 1);
            nextStance %= 3;
            changeStance((Stance)nextStance);
        }

        // rotation following the mouse
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down); // ASK Quaternion? kekece?
    }

    /**
        change the stance of the player to a new stance
    */
    void changeStance(Stance newStance){
        switch(newStance){
            case Stance.Agile:
                currentDashForce = AGILE_DASH_FORCE;
                currentDashInvicibilityTime = AGILE_DASH_INVICIBILITY_TIME;
                currentMoveSpeed = AGILE_MOVE_SPEED;
                currentStance = Stance.Agile;

                primaryShootingSource.gameObject.SetActive(false);
                secondaryShootingSource.gameObject.SetActive(false);
                break;

            case Stance.Standard:
                currentDashForce = STANDARD_DASH_FORCE;
                currentDashInvicibilityTime = STANDARD_DASH_INVICIBILITY_TIME;
                currentMoveSpeed = STANDARD_MOVE_SPEED;
                currentStance = Stance.Standard;
                primaryShootingSource.gameObject.SetActive(true);
                secondaryShootingSource.gameObject.SetActive(false);
                break;
            case Stance.Stationary:
                currentDashForce = 0;
                currentDashInvicibilityTime = 0;
                currentMoveSpeed = STATIONARY_MOVE_SPEED;
                currentStance = Stance.Stationary;
                primaryShootingSource.gameObject.SetActive(true);
                secondaryShootingSource.gameObject.SetActive(true);
                break;
        }
    }

    public Stance getCurrentStance(){
        return currentStance;
    }

    // POLISH player dash - invincibility frames
    private IEnumerator PerformDash(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 dashDirection;

        // dash in the moving direction, if no input, in the direction faced by the model (towards the mouse cursor)
        // TODO invincibility time
        if(horizontalInput != 0 || verticalInput != 0){
            dashDirection = new Vector3(horizontalInput, 0, verticalInput);
        } else {
            dashDirection = transform.right;
        }

        Debug.Log("dash!");
        rbPlayer.AddForce(dashDirection * currentDashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(currentDashDuration);

        Debug.Log("end dash...");
        rbPlayer.velocity = Vector3.zero;
    }
}
