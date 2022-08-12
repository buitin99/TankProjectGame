using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCamera : MonoBehaviour
{
    //Initialization Transform used store and manipulate the position, rotation, scale of the Tank
    public Transform tank;

    private Vector3 tankCameraVector3;

    void Start()
    {
        //Get value to locate the tank in 3D world space
        tankCameraVector3 = tank.transform.position - transform.position;
    }
    //Used for camera move along GameObject(Tank). It's also the smoothest way to move the Camera to An Object
    void LateUpdate()
    {
        //Update the position
        transform.position = tank.transform.position - tank.transform.rotation * tankCameraVector3;
        //Rotate the Camera every frame
        transform.LookAt(tank);
    }
}
