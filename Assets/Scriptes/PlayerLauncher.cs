using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField]
    private Transform playerStartPosition; // Reference to the starting position of the player object
    public Player player; // Reference to the player object (assuming 'Player' is a custom class)
    private bool holdingPlayer; // Boolean flag to check if the player is currently being held
    private Camera cam; // Reference to the main camera
    public static PlayerLauncher Instance; // Singleton instance for easy access to this script

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the Singleton instance
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get and cache the main camera reference
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // If there's no player assigned, exit the update loop
        if (player == null)
        {
            return;
        }

        // Check if the input is detected and the player hasn't been launched yet
        if (InputDown() && player.isLunched == false)
        {
            Vector3 touchWorldPosition;

            // Determine the position of the touch or mouse in world coordinates
            if (Input.touchCount > 0)
            {

                touchWorldPosition = cam.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                touchWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            // Set the Z-axis to 0 because we are working in 2D space
            touchWorldPosition.z = 0;

            // If the touch/mouse position is close enough to the player, set holdingPlayer to true
            if (Vector3.Distance(touchWorldPosition, player.transform.position) <= 3.0f)
            {
                holdingPlayer = true;
            }
        }

        // If the input is released and the player is being held, launch the player
        if (InputUp() && holdingPlayer == true)
        {
            holdingPlayer = false; // Stop holding the player
                                   // Launch the player in the direction opposite to the start position
            player.Lunch(playerStartPosition.position - player.transform.position);
            player.isLunched = true; // Mark the player as launched
            
        }

        // If the player is being held and hasn't been launched, update the player's position
        if (holdingPlayer && player.isLunched == false)
        {
            Vector3 newPos;

            // Get the new position from the touch or mouse input
            if (Input.touchCount > 0)
            {
                newPos = cam.ScreenToWorldPoint(Input.touches[0].position);
            }
            else
            {
                newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            // Set the Z-axis to 0 to keep the player in the 2D plane
            newPos.z = 0;
            player.transform.position = newPos; // Update the player's position
        }
    }

    // Method to detect if input (touch or mouse) is pressed down
    bool InputDown()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
           
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }

    // Method to detect if input (touch or mouse) is released
    bool InputUp()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            return true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        else { return false; }
    }

    // Method to set a new player object in the game
    public void SetNewPlayer(GameObject playerPrefab)
    {
        player = Instantiate(playerPrefab, playerStartPosition.position, Quaternion.identity).GetComponent<Player>();
        CameraController.instance.SetPlayer(player);

    }

}
