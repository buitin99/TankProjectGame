using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulltet : MonoBehaviour
{
    private float bulletSpeed = 80f;

    private float deactivationDistance = 70f;

    void Update()
    {
        //transform.Translate is Moves the transform in the direction and distance of translation.
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        //Deactivate the bullet when it's far away
        if (Vector3.SqrMagnitude(transform.position) > deactivationDistance * deactivationDistance)
        {
            gameObject.SetActive(false);
        }
    }
}

