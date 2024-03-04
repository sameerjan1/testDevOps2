using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure rb is initialized with the Rigidbody component of the GameObject
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if rb is not null before accessing it
        if (rb != null)
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            if (Input.GetKey("d"))
            {
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else if (Input.GetKey("a"))
            {
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (rb.position.y < -2f)
            {
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.EndGame();
                }
                else
                {
                    Debug.LogError("GameManager not found in the scene!");
                }
            }
        }
        else
        {
            Debug.LogError("Rigidbody component is not assigned! Make sure it's assigned in the inspector or attached to the GameObject.");
        }
    }
}
