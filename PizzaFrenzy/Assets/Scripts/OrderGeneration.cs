using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// class that controls the function of the pizza shop
public class OrderGeneration : MonoBehaviour
{

    public List<GameObject> orders;  // orders ready for pickup
    public List<GameObject> standbyHouses;  // houses available for orders
    public float deliveryTime = 20f;  // time alloted for delivery
    public float orderTime = 15f;  // the frequency by which orders are generated
    public GameObject[] allHouses;  // all houses in the scene
    public GameObject[] pizzaBoxes;  // all pizzaboxes at pickup point
    public float[] deliveryLimits;  // the time left for each delivery
    public bool[] ordersOut;  // array that indicated which orders are active
    public ManageUI manageUI;  // UI control class
    float currTime;  // tracks the time between orders 
    float startdelay = 1f;  // the delay between the start of the game and the first order
    public float updateInterval = 60f;  // frequency by which the order time decreases
    float intervalProg = 0;  // tracks interval progress
    float minOrder = 5f;  // the minimum order time

    // Start is called before the first frame update
    void Start()
    {
        // set delivery time
        deliveryTime = PlayerPrefs.GetFloat("deliveryTime");
        // assemble list of all houses
        allHouses = GameObject.FindGameObjectsWithTag("House");
        // assemble pizza boxes
        pizzaBoxes = GameObject.FindGameObjectsWithTag("PizzaBox");
        // set array of delivery limits
        deliveryLimits = new float[allHouses.Length];
        for (int i = 0; i < deliveryLimits.Length; i++) {
            deliveryLimits[i] = deliveryTime;
        }
        // set array of orders out
        ordersOut = new bool[allHouses.Length];
        for (int i = 0; i < ordersOut.Length; i++) {
            ordersOut[i] = false;
        }
        // assemble the list of standby houses
        standbyHouses = new List<GameObject>(allHouses);
        currTime = startdelay;
    }

    // Update is called once per frame
    void Update()
    {
        // update order time and current time
        intervalProg += Time.deltaTime;
        if (intervalProg > updateInterval)
        {
            orderTime = System.Math.Max(minOrder, orderTime - 1);
            for (int i = 0; i < ordersOut.Length; i++)
            {
                // update inactive orders
                if (ordersOut[i] == false)
                {
                    deliveryLimits[i] = deliveryTime;
                }
            }
            intervalProg = 0f;
        }
        // check for outstanding orders
        for (int i = 0; i < ordersOut.Length; i++) {
            // if a particular house has an order
            if (ordersOut[i] == true) {
                deliveryLimits[i] -= Time.deltaTime;
                // if the time has run out for that order
                if (deliveryLimits[i] < 0) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

        // generate new order if the order time has passed
        currTime -= Time.deltaTime;
        if (currTime < 0) {
            assignHouse();
            currTime = orderTime;
        }
    }

    // select random house for delivery
    void assignHouse()
    {
        if (standbyHouses.ToArray().Length != 0)
        {
            // select random index in array of standby houses
            int i = Random.Range(0, standbyHouses.ToArray().Length);
            orders.Add(standbyHouses[i]);
            // turn on pizza icon
            manageUI.pizzaOn();
            // mark order as out
            int ind = System.Array.IndexOf(allHouses, standbyHouses[i]);
            ordersOut[ind] = true;
            standbyHouses.RemoveAt(i);
            // turn on pizza box renderer at pickup
            for (int j = 0; j < orders.ToArray().Length; j++)
            {
                for (int k = 0; k < pizzaBoxes.Length; k++)
                {
                    if (pizzaBoxes[k].name == j.ToString()) {
                        pizzaBoxes[k].GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }
    }

}
