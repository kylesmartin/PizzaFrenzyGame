using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the existence of the pizza projectile
public class ProjectilePizza : MonoBehaviour
{
    // destroys pizza projectile 5 seconds after collision with house 
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 5f);
    }
}
