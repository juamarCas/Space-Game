using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public float offset_y,offset_z;


    // Update is called once per frame
    void Update()
    {
        
        Vector3 targetPos = new Vector3(cameraTarget.position.x, cameraTarget.position.y + offset_y, cameraTarget.position.z+offset_z);
        transform.position = targetPos;
    }
}
