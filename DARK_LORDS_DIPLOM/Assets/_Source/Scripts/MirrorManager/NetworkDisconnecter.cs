using Mirror;
using UnityEngine;

public class NetworkDisconnecter : MonoBehaviour
{
    // Вызывайте эту функцию для отключения
    public void Disconnect()
    {
        NetworkManager manager = NetworkManager.singleton;

        if (manager == null)
        {
            Debug.LogError("NetworkManager не найден!");
            return;
        }

        // Определяем текущий режим работы
        bool isHost = NetworkServer.active && NetworkClient.isConnected;
        bool isClientOnly = NetworkClient.isConnected && !isHost;
        bool isServerOnly = NetworkServer.active && !NetworkClient.isConnected;

        // Отключаем в зависимости от режима
        if (isHost)
        {
            Debug.Log("Останавливаем хост...");
            manager.StopHost();
        }
        else if (isClientOnly)
        {
            Debug.Log("Отключаем клиента...");
            manager.StopClient();
        }
        else if (isServerOnly)
        {
            Debug.Log("Останавливаем сервер...");
            manager.StopServer();
        }
        else
        {
            Debug.LogWarning("Не подключено к сети!");
        }
    }
}