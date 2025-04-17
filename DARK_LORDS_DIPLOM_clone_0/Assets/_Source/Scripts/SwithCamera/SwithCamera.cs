using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwithCamera : MonoBehaviour
{
    [SerializeField] public Camera MainCamera;
    [SerializeField] public Camera EnemyCamera;
    [SerializeField] public Camera PlayerCamera;
    [SerializeField] public GameObject PlayerCanvas;
    [SerializeField] public GameObject EnemyCanvas;

    void Awake()
    {
    }

    public void SwitchToEnemy()
    {
        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(true);
        PlayerCamera.gameObject.SetActive(false);
        PlayerCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void SwitchToPlayer()
    {
        MainCamera.gameObject.SetActive(false);
        EnemyCamera.gameObject.SetActive(false);
        PlayerCamera.gameObject.SetActive(true);
        EnemyCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }
}
