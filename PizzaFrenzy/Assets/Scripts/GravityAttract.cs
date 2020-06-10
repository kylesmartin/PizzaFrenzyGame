using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls force of gravity
public class GravityAttract : MonoBehaviour
{

    public float gravity = -10;  // gravity force

    public void Attract(Transform body)
    {
        // set gravity direction
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        // add force of gravity between body and planet
        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }

}
