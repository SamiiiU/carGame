using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    // UI Elements
    // [SerializeField] private Slider fuelSlider;

    // Fuel-related variables
    private float fuelLevel = 100f; // Starting fuel level
    private float maxFuelLevel = 100f; // Maximum fuel capacity
    private float fuelConsumptionRate = 5f; // Rate of fuel consumption per second
    private float fuelIncreaseAmount = 20f; // Fuel added per pickup

    [SerializeField] private CarController carController;

    // Other variables
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private GameObject FuelObject;

    private float rotationSpeed = 50f;
    private int fuelsPickedUp = 0;

    void Start()
    {
        // fuelSlider.maxValue = maxFuelLevel; // Set slider max value
        // fuelSlider.value = fuelLevel; // Initialize slider/
        InvokeRepeating("fuelSpawner", 0f, Random.Range(5, 10));
    }

    void Update()
    {
        ConsumeFuel(); // Decrease fuel over time
        missingFuelDestroyer();
        fuelFloater();
        CheckFuelPickup();
    }

    void ConsumeFuel()
    {
        if (fuelLevel > 0)
        {
            // Decrease fuel level smoothly over time
            if(carController.getCarSpeed() > 50){
                fuelConsumptionRate = carController.getCarSpeed() / 10;
            }else if(carController.getCarSpeed() == 0){
                fuelConsumptionRate = 0.5f;
            }
            else{
                fuelConsumptionRate = carController.getCarSpeed() / 20 ;
            }
            
            fuelLevel -= fuelConsumptionRate * Time.deltaTime;
            // fuelSlider.value = Mathf.Lerp(fuelSlider.value, fuelLevel, Time.deltaTime * 5); // Smooth transition
        }
        else
        {
            // Handle what happens when fuel runs out
            Debug.Log("Out of Fuel! Game Over!");
        }
    }

    void CheckFuelPickup()
    {
        foreach (GameObject fuel in GameObject.FindGameObjectsWithTag("FuelPickup"))
        {
            float distance = Vector3.Distance(carTransform.position, fuel.transform.position);
            if (distance < 1f) // Adjust threshold as needed
            {
                fuelsPickedUp++;
                fuelLevel = Mathf.Min(fuelLevel + fuelIncreaseAmount, maxFuelLevel); // Add fuel but cap at maxFuelLevel
                // fuelSlider.value = Mathf.Lerp(fuelSlider.value, fuelLevel, Time.deltaTime * 5); // Smooth transition
                Destroy(fuel);
            }
        }
    }

    void fuelSpawner()
    {
        int laneIndex = Random.Range(0, 3);
        GameObject newFuel = Instantiate(FuelObject, lanes[laneIndex].position, Quaternion.identity);
        newFuel.tag = "FuelPickup";
    }

    void missingFuelDestroyer()
    {
        foreach (GameObject oilContainer in GameObject.FindGameObjectsWithTag("FuelPickup"))
        {
            if (oilContainer.transform.position.y < -50)
            {
                Destroy(oilContainer);
            }
        }
    }

    void fuelFloater()
    {
        foreach (GameObject oilContainer in GameObject.FindGameObjectsWithTag("FuelPickup"))
        {
            oilContainer.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    public float getFuelLevel(){
        return fuelLevel;
    }
}