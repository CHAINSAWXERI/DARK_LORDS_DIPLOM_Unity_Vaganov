using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;
using kcp2k;

public class SetIPandPort : MonoBehaviour
{
    [SerializeField] public NetworkManager NetworkManager;
    [SerializeField] public KcpTransport KcpTransport;
    [SerializeField] public TMP_InputField ipInputField;
    [SerializeField] public TMP_InputField portInputField;

    public void SetIPAndPort()
    {
        if (ipInputField != null)
        {
            NetworkManager.networkAddress = ipInputField.text;
            if (ushort.TryParse(portInputField.text, out ushort port))
            {
                KcpTransport.Port = port;
            }
            else
            {
                Debug.LogError("Некорректный порт! Введите число от 0 до 65535");
            }
        }
        else
        {
            Debug.LogError("TMP_InputField не назначен!");
        }
    }
}
