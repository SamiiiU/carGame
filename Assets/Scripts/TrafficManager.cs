using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] Transform[] lanes ;
    [SerializeField] GameObject[] trafficeVehicles;
    [SerializeField] Transform carTransform;  // Reference to the car's transform 
    [SerializeField] private float destroyDistance = 20f; 
    void Start()
    {
        InvokeRepeating("SpawnTraffic", 0f, Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update()
    {
        checkDodgedCar();
    }

    void checkDodgedCar(){
        // Iterate through all traffic vehicles and check if they have passed the car
        foreach (GameObject vehicle in GameObject.FindGameObjectsWithTag("Traffic"))
        {
            // Check if vehicle passed the car's position
            if (vehicle.transform.position.z < carTransform.position.z - destroyDistance)
            {
                Destroy(vehicle);
            }
        }
    }

    void SpawnTraffic(){
// Random indexes for lanes and vehicles
        int laneIndex = Random.Range(0, lanes.Length);
        int vehicleIndex = Random.Range(0, trafficeVehicles.Length);

        // Spawn vehicle at the selected lane position
        GameObject newVehicle = Instantiate(trafficeVehicles[vehicleIndex], lanes[laneIndex].position, Quaternion.identity);

         // Assign the traffic tag to help in identifying traffic vehicles later
        newVehicle.tag = "Traffic";  // Make sure your vehicle prefab has the tag "Traffic" set in Unity
    }
}
