using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorRoomTag : MonoBehaviour
{
    public static string nextRoomScene;
    [SerializeField] private TMP_Text roomTag;
    [SerializeField] private GameObject door;
    //[SerializeField] private Door door;

    private void Start()
    {
        roomTag.text = door.name;
    }
}
