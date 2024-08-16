using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    
    // This script manages the UI elements in the game, including buttons and end screens.

    public GameObject nextButton;  // Reference to the UI button that proceeds to the next level or action.
    public GameObject endSceen;    // Reference to the end screen UI element displayed at the end of a game.
    public GameObject win;         // Reference to the UI element that indicates a win.
    public GameObject lose;        // Reference to the UI element that indicates a loss.

    public static GameUI instance; // Singleton instance of the GameUi, allowing it to be accessed from other scripts.

    private void Awake()
    {
        instance = this; // Set the static instance to this instance of the script, ensuring only one instance exists.
    }

    public void OnNextButtonClick()
    {
        // This method is called when the 'Next' button is clicked.
        GameManager.instance.SpawnerNewPlayer(); // Tell the GameManager to spawn a new player.
        nextButton.SetActive(false); // Hide the 'Next' button after it has been clicked.
    }

    public void OnRestart()
    {
        // This method is called to restart the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene to restart the game.
    }

    public void LunchEndedScreen(bool isWin)
    {
        // This method is called to display the end screen when the game ends.
        endSceen.SetActive(true); // Show the end screen UI.

        win.SetActive(isWin);    // Show the win UI element if the player won.
        lose.SetActive(!isWin);  // Show the lose UI element if the player lost.
    }

   

}
