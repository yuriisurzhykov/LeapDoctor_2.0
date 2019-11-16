using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        // Rotate the cube by converting the angles into a quaternion.
        Quaternion pRotation = player.transform.rotation;

        // Dampen towards the target rotation
        transform.rotation = pRotation;
    }
}
