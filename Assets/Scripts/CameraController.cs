using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    [SerializeField] float xOffset = 0.0f;
    [SerializeField] float zOffset = -25.0f;
    [SerializeField] float yOffset = 60.0f;
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
