using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVehicle : MonoBehaviour
{

    [SerializeField] float speed = 10;
    private UIManager UIManager;
    private CarController car;
    
    void Start(){
 // Dynamically find UIManager in the scene
        UIManager = FindFirstObjectByType<UIManager>();
        car = FindFirstObjectByType<CarController>();
    }

    void OnCollisionEnter(Collision collision){

    if (collision.gameObject.CompareTag("playerCar")) // Target object ka tag check karo
    {
        UIManager.setIsCollided(true);
    }
    }

    void updateSpeed(){
        speed = car.getCarSpeed() / 4;
    }

    void Update(){
        transform.Translate(0,0, speed * Time.deltaTime);
    }
}
