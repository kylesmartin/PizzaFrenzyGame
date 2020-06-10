using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the pizza projectile movement
public class PizzaDropper : MonoBehaviour
{
    public GameObject pizza;  // pizza object to be fired
    public GameObject firePoint;  // the point of launch
    public int magnitude = 5;  // force magnitude used to propel the pizza
    private AudioSource source;  // the sound of the shot

    // Start is called before the first frame update
    private void Start()
    {
        // set audio
        source = GetComponent<AudioSource>();
    }

    // function that shoots pizza at house
    public void deliverPizza(GameObject targethouse)
    {
        // create pizza object
        GameObject drop = Instantiate(pizza, firePoint.transform.position, Quaternion.identity) as GameObject;
        // set direction of fire
        var dir = (targethouse.transform.position - firePoint.transform.position).normalized;
        // add force to pizza object
        drop.GetComponent<Rigidbody>().AddForce(dir * magnitude, ForceMode.Impulse);
        // play sound
        source.Play();
    }
}
