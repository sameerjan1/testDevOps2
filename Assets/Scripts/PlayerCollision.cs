// namespace PlayerCollision;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collisionInfo)
    {
        
    Debug.Log(collisionInfo.collider.tag);
   
        if (collisionInfo.collider.name == "Obstacle" || collisionInfo.collider.name == "END")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
