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
    - // TODO define key: in stationary stance    

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

    private const float STANDARD_MOVE_SPEED = 20.0f; // moving speed in standard stance
    private const float AGILE_MOVE_SPEED = 30.0f; // moving speed in agile stance

    private const float STANDARD_DASH_FORCE = 30.0f; // Dash force in standard stance
    private const float STANDARD_DASH_DURATION = 30.0f; // Dash force in standard stance
    private const float STANDARD_DASH_INVICIBILITY_TIME = 30.0f; // Dash invincibility time in standard stance
    private const float AGILE_DASH_FORCE = 30.0f; // Dash force in agile stance
    private const float AGILE_DASH_DURATION = 30.0f; // Dash force in agile stance
    private const float AGILE_DASH_INVICIBILITY_TIME = 30.0f; // Dash invincibility time in agile stance

    // TODO set properties private 
    public Stance currentStance = Stance.Standard; 
    public float currentMoveSpeed = STANDARD_MOVE_SPEED;
    public float currentDashForce = STANDARD_DASH_FORCE;
    public float currentDashDuration = STANDARD_DASH_FORCE;
    public float currentDashInvicibilityTime = STANDARD_DASH_INVICIBILITY_TIME;

    private Rigidbody rbPlayer;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // FIXME rotation of the model make this code not work as intended...
        transform.Translate(new Vector3(1 * Time.deltaTime * currentMoveSpeed * horizontalInput, 0 , 1 * Time.deltaTime * currentMoveSpeed * verticalInput));

        // player dash 
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("dash!");
            // how to do a dash?
            rbPlayer.AddForce(Vector3.forward * currentDashForce, ForceMode.Impulse);
        }

        // manage the player stance
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            int nextStance = (int)(currentStance + 1);
            nextStance %= 3;
            changeStance((Stance)nextStance);
        }

        // rotation following the mouse
        // FIXME the character is off rotated of roughly 90°
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
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
                break;

            case Stance.Standard:
                currentDashForce = STANDARD_DASH_FORCE;
                currentDashInvicibilityTime = STANDARD_DASH_INVICIBILITY_TIME;
                currentMoveSpeed = STANDARD_MOVE_SPEED;
                currentStance = Stance.Standard;
                break;
            case Stance.Stationary:
                currentDashForce = 0;
                currentDashInvicibilityTime = 0;
                currentMoveSpeed = 0;
                currentStance = Stance.Stationary;
                break;
        }
    }
}
