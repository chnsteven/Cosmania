using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Object collided");
        var _nextRoomScene = DoorRoomTag.nextRoomScene;
        if (_nextRoomScene != null && other.gameObject.CompareTag("Player"))
        {
            SceneLoader.instance.LoadScene(DoorRoomTag.nextRoomScene);
        }
    }
}
