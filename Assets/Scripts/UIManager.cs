using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;  // For Scene Management


public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI speedText; //
    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private FuelSystem fuelSystem; //

    [SerializeField] private Slider fuelSlider;
    [SerializeField] private CarController carController;

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject howToPlayUI;

    [SerializeField] private GameObject mainMenuUI;

    [SerializeField] private bool isCollided = false;
    [SerializeField] private bool isFuelOver = false;
    void Start()
    {
        Time.timeScale = 0;
        fuelSlider.maxValue = 100;
        mainMenuUI.SetActive(true);
        gameOverUI.SetActive(false);
        inGameUI.SetActive(false);
        
    }

    public void PlayGame(){
        mainMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        inGameUI.SetActive(true);
        pauseUI.SetActive(false); // Replace with your actual Game Scene name
        Time.timeScale = 1f;  
    }


    public void PauseGame(){
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;  
        // add logic to pause the gamescene , like the motion of car and everything would same when we click resume
    }

    public void GoToHome(){
        SceneManager.LoadScene(0);
        mainMenuUI.SetActive(true); // Replace with your actual Game Scene name
        inGameUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    public void GameOver(){
        inGameUI.SetActive(false);      
        gameOverUI.SetActive(true);
        //there is a restart button 
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); //SceneManager.GetActiveScene().buildIndex
        mainMenuUI.SetActive(false);
        inGameUI.SetActive(true);

    }

    public void setIsCollided(bool isCollided){
        this.isCollided = isCollided;
    }

    public bool IsCollided(){
        return isCollided;
    }

    public bool getIsFuelEnd(){
        return isFuelOver;
    }

    void fuelEnd(){
        if(fuelSlider.value == 0){
            gameOverText.text = "Fuel end :(";
            isFuelOver = true;
            GameOver();
        }
    }

    void collidedGameOver(){
        if(isCollided){
            gameOverText.text = "OOPS! you hitted the car";
            GameOver();
        }
    }

    public void showHowToPlay()
    {
        howToPlayUI.SetActive(true);// Show the Instructions Panel 
        mainMenuUI.SetActive(false);// Hide the main menu
    }

    // Exit Button Functionality
    public void ExitGame()
    {
        Application.Quit();
    }

    // Close How to Play Panel
    public void CloseInstructions()
    {
        howToPlayUI.SetActive(false);
        mainMenuUI.SetActive(true);// Show the main menu
    }
    

    // Update is called once per frame
    void Update()
    {
        speedText.text = carController.getCarSpeed().ToString() + " km/h";
        fuelSlider.value = fuelSystem.getFuelLevel();
        
        fuelEnd();
        collidedGameOver();
    }
}
