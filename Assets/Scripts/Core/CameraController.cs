using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance_X;
    [SerializeField] private float aheadDistance_Y;
    [SerializeField] private float cameraSpeed;
    private float lookAhead_X;
    private float lookAhead_Y;

    private void Update()
    {
        //Room camera
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead_X, player.position.y + lookAhead_Y, transform.position.z);
        lookAhead_X = Mathf.Lerp(lookAhead_X, (aheadDistance_X * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookAhead_Y = Mathf.Lerp(lookAhead_Y, aheadDistance_Y, Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}