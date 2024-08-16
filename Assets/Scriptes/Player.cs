using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    public bool isLunched =false;

    private void Awake()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }
    private void Update()
    {
        if (isLunched && rigidbody.IsSleeping())
        {

            GameManager.instance.PlayerFinished();
            Destroy(gameObject);
        }


    }
    public void Lunch(Vector2 vector)
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.AddForce(vector *5 , ForceMode2D.Impulse);
    }
}
