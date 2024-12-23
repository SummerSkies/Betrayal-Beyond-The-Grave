using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string possessedTag = "Possessed";
    [SerializeField] private float movementDelayMultiplier;

    private Vector3 targetPositionOffset;
    private float rotationLock = 0;

    private void Start()
    {
        //The camera moves around the player the same way I want the ghost to, so I'll use the camera's position to update the ghost's
        //Find the position of the ghost and the camera and calculate the difference so the target position is always right behind the camera
        if (player.CompareTag(possessedTag))
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 currentPosition = transform.position;
            targetPositionOffset = cameraPosition - currentPosition;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    void Update()
    {
        //Using slerps instead of rigidbody force prevents slingshotting from too much force
        //Make the ghost always look at the player
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        Quaternion lockedRotation = Quaternion.Euler(rotationLock, targetRotation.eulerAngles.y, rotationLock); //Lock rotation on X and Z axes to 0
        transform.rotation = Quaternion.Slerp(transform.rotation, lockedRotation, Time.deltaTime);

        //Make the ghost follow behind the player's camera
        Vector3 targetPosition = Camera.main.transform.position + targetPositionOffset;
        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * movementDelayMultiplier);
    }
}
