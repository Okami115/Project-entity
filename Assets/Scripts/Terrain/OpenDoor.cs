using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private player.PlayerController pm;
    [SerializeField] private GameObject door;
    public static event Action<String> canOpen;
    public static event Action openDoor;

    private void Awake()
    {
        pm = FindAnyObjectByType<player.PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pm.interaction += OpenningDoor;
            canOpen?.Invoke("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canOpen?.Invoke("");
            pm.interaction -= OpenningDoor;
        }
    }

    private void OpenningDoor()
    {
        canOpen?.Invoke("");
        openDoor?.Invoke();
        Destroy(door);
    }
}
