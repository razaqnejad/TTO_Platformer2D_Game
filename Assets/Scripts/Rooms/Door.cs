using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    // [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private int level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x){
                // cam.MoveToNewRoom(nextRoom);
                // nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
                SceneManager.LoadScene(level+3);
            }else{
                cam.MoveToNewRoom(previousRoom);
                // nextRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                SceneManager.LoadScene(level+1);
            }
        }
    }
}
