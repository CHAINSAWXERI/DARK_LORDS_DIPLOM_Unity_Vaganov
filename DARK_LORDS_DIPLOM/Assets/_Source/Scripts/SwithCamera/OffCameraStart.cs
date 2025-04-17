using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffCameraStart : MonoBehaviour
{
    [SerializeField] public GameObject CameraOn;

    void Start()
    {
        gameObject.SetActive(false);
        CameraOn.SetActive(true);
    }
}
