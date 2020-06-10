using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls body that is subject to gravity
public class GravityBody : MonoBehaviour
{
    public GravityAttract attractor;  // the gravity source
    private Transform myTransform;  // object transform 

    // Start is called before the first frame update
    void Start()
    {
        // freeze rotation and remove built in gravity
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // run attract function
        attractor.Attract(myTransform);
    }
}
