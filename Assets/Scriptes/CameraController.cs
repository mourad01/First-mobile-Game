using UnityEngine;

public class CameraController : MonoBehaviour
{
    // This script controls the camera behavior in a game.

    public Player player; // Reference to the player object that the camera will follow.
    public float offset = 2.0f; // Horizontal offset between the camera and the player.

    public static CameraController instance; // Singleton instance of the CameraController to allow easy access from other scripts.

    void Awake()
    {
        instance = this; // Assign this instance of CameraController to the static variable, ensuring only one instance exists.
    }


    void FixedUpdate()
    {
        if (player == null)
        {
            return; // If there is no player assigned, exit the function to avoid errors.
        }

        // Check if the player has been launched and if the player's x position is greater than or equal to the camera's x position minus the offset.
        if (player.isLunched == true && player.transform.position.x >= transform.position.x - offset)
        {
            // Smoothly move the camera towards the player's position with the specified offset.
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + offset, 1, -10), Time.deltaTime * 10);
        }
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer; // Set the player reference to the newly provided player.
        Vector3 newPos = player.transform.position; // Get the player's current position.
        newPos.z = -10; // Set the camera's z position to -10 to keep it at the same depth.
        transform.position = newPos; // Immediately set the camera's position to the player's position.
    }

}
