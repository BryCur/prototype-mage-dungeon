using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private float xOffset = 0.0f;
    private float zOffset = -25.0f;
    private float yOffset = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {}

    private void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
    }
}
