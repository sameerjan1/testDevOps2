using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement; // Assuming PlayerMovement is another MonoBehaviour script

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    public void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an obstacle or end object
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("End"))
        {
            // If collided with an obstacle or end object, disable player movement
            movement.enabled = false;

            // Find the GameManager and ensure it exists
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                
                gameManager.EndGame();
            }
            else
            {
                Debug.LogError("GameManager not found!");
            }
        }
    }
}

[TestFixture]
public class PlayerCollisionTest
{
    [UnityTest]
    public IEnumerator OnCollisionEnter_EndGameCalledWhenCollidingWithObstacle()
    {
        // Arrange
        GameObject playerGameObject = new GameObject("Player");
        PlayerCollision playerCollision = playerGameObject.AddComponent<PlayerCollision>();
        PlayerMovement playerMovement = playerGameObject.AddComponent<PlayerMovement>();
        playerCollision.movement = playerMovement;

        GameObject obstacle = new GameObject("Obstacle");
        BoxCollider obstacleCollider = obstacle.AddComponent<BoxCollider>(); // Adding a collider component to the obstacle GameObject
        obstacleCollider.isTrigger = false; // Ensure that the collider is not a trigger

        // Ensure both objects have rigidbodies attached
        Rigidbody playerRigidbody = playerGameObject.AddComponent<Rigidbody>();
        Rigidbody obstacleRigidbody = obstacle.AddComponent<Rigidbody>();

        // Move the objects slightly to ensure they don't intersect initially
        playerGameObject.transform.position = new Vector3(0, 0, 0);
        obstacle.transform.position = new Vector3(0, 2, 0);

        // Act - Simulate physics update to detect collision
        yield return new WaitForFixedUpdate(); // Wait for physics update
        yield return null; // Wait for another frame update

        // Assert
        Assert.IsFalse(playerMovement.enabled); // Player movement should be disabled after collision
        Assert.IsNotNull(Object.FindObjectOfType<GameManager>()); // GameManager instance should be present
    }
     
     [UnityTest]
    public IEnumerator OnCollisionEnter_EndGameCalledWhenCollidingWithEnd()
    {
        // Arrange
        GameObject playerGameObject = new GameObject("Player");
        PlayerCollision playerCollision = playerGameObject.AddComponent<PlayerCollision>();
        PlayerMovement playerMovement = playerGameObject.AddComponent<PlayerMovement>();
        playerCollision.movement = playerMovement;

        GameObject endObject = new GameObject("End");
        BoxCollider endCollider = endObject.AddComponent<BoxCollider>(); // Adding a collider component to the end GameObject
        endCollider.isTrigger = false; // Ensure that the collider is not a trigger

        // Ensure both objects have rigidbodies attached
        Rigidbody playerRigidbody = playerGameObject.AddComponent<Rigidbody>();
        Rigidbody endRigidbody = endObject.AddComponent<Rigidbody>();

        // Move the objects slightly to ensure they don't intersect initially
        playerGameObject.transform.position = new Vector3(0, 0, 0);
        endObject.transform.position = new Vector3(0, 2, 0);

        // Act - Simulate physics update to detect collision
        yield return new WaitForFixedUpdate(); // Wait for physics update
        yield return null; // Wait for another frame update

        // Assert
        Assert.IsFalse(playerMovement.enabled); // Player movement should be disabled after collision
        Assert.IsNotNull(Object.FindObjectOfType<GameManager>()); // GameManager instance should be present
    }

    [UnityTest]
    public IEnumerator OnCollisionEnter_EndGameNotCalledWhenNotCollidingWithObstacleOrEnd()
    {
        yield return null; // Yielding to ensure that any potential collision events are processed in the next frame

        // Arrange
        GameObject playerGameObject = new GameObject("Player");
        PlayerCollision playerCollision = playerGameObject.AddComponent<PlayerCollision>();
        PlayerMovement playerMovement = playerGameObject.AddComponent<PlayerMovement>();
        playerCollision.movement = playerMovement;

        GameObject otherObject = new GameObject("Other");
        BoxCollider otherCollider = otherObject.AddComponent<BoxCollider>(); // Adding a collider component to the other GameObject
        otherCollider.isTrigger = false; // Ensure that the collider is not a trigger

        // Ensure both objects have rigidbodies attached
        Rigidbody playerRigidbody = playerGameObject.AddComponent<Rigidbody>();
        Rigidbody otherRigidbody = otherObject.AddComponent<Rigidbody>();

        // Move the objects slightly to ensure they don't intersect initially
        playerGameObject.transform.position = new Vector3(0, 0, 0);
        otherObject.transform.position = new Vector3(0, 2, 0);

        // Act - Simulate physics update to detect collision
        yield return new WaitForFixedUpdate(); // Wait for physics update
        yield return null; // Wait for another frame update

        // Assert
        Assert.IsTrue(playerMovement.enabled); // Player movement should remain enabled
        Assert.IsNull(Object.FindObjectOfType<GameManager>()); // GameManager instance should not be present
    }
}
