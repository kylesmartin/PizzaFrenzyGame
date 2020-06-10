using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// controls the movement and behavior of the pizzacar
public class CarMovement : MonoBehaviour
{

    public float forceMag = 5;  // magnitude of force that causes acceleration
    public float rotationMag = 5;  // magnitude of force that controls turning
    public float maxSpeed = 60;  // maximum speed of the vehicle
    public List<GameObject> orders;  // orders currently in the car
    public Transform planet;  // planet object
    public OrderGeneration orderGen;  // pizza shop class
    public ManageUI manageUI;  // UI control class
    public PizzaDropper pizzaDropper;  // class for shooting pizzas
    private AudioSource source;  // engine audio
    public GameObject mainBox;  // pizza box that produces audio

    // Start is called before the first frame update
    private void Start()
    {
        // begin engine sound upon startup
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // get the inputs
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        // add force based on inputs
        GetComponent<Rigidbody>().AddForce(transform.forward * forceMag * z, ForceMode.Acceleration);

        // check car speed
        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }

        // adjust engine pitch based on speed relative to max speed
        source.pitch = GetComponent<Rigidbody>().velocity.magnitude / maxSpeed;

        // rotate
        transform.Rotate(0, x * rotationMag * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider trig)
    {
        // check if in posession of pizza for particular house
        for (int i = 0; i < orders.ToArray().Length; i++)
        {
            if (trig.name == orders[i].name)
            {
                // shoot pizza
                pizzaDropper.deliverPizza(orders[i]);
                // add order to possible orders
                orderGen.standbyHouses.Add(orders[i]);
                // remove UI object
                manageUI.removeUI(orders[i]);
                // reset delivery time
                int ind = System.Array.IndexOf(orderGen.allHouses, orders[i]);
                orderGen.deliveryLimits[ind] = orderGen.deliveryTime;
                orderGen.ordersOut[ind] = false;
                // remove order from car
                orders.RemoveAt(i);
            }
        }

        // get pizzas
        if (trig.name == "pizzashop")
        {
            // add orders from shop into car
            for (int i = 0; i < orderGen.orders.ToArray().Length; i++)
            {
                orders.Add(orderGen.orders[i]);
                manageUI.addUI(orderGen.orders[i]);
            }
            // play pizza pickup sound
            if (orderGen.orders.ToArray().Length > 0)
            {
                mainBox.GetComponent<AudioSource>().Play();
            }
            // remove pizza boxes from view
            for (int j = 0; j < orderGen.pizzaBoxes.Length; j++)
            {
                orderGen.pizzaBoxes[j].GetComponent<Renderer>().enabled = false;
            }
            // remove orders from pizzashop
            orderGen.orders.Clear();
            manageUI.pizzaOff();
        }
    }

}

