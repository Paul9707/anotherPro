using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum Type
{
    Sever = 0,
    Client
}

public class SeverChecker : MonoBehaviour
{
    public Type type;
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        if (type.Equals(Type.Sever))
        {
            Start_Server();
        }
        else
        {
            Start_Client();
        }
        
    }

    public void Start_Server()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("WebGL Server ¾ÈµÊ...");
        }
        else 
        {
            networkManager.StartServer();
            Debug.Log($"{networkManager.networkAddress} start Server");
            NetworkServer.OnConnectedEvent += (NetworkConnectionToClient) =>
            {
                Debug.Log($"new Client Connect: {NetworkConnectionToClient.address}");
            };
            NetworkServer.OnDisconnectedEvent += (NetworkConnectionToClient) =>
            {
                Debug.Log($"Client Disconnect: {NetworkConnectionToClient.address}");
            };
        }
    }

    public void Start_Client()
    {
        networkManager.StartClient();
        Debug.Log($"{networkManager.networkAddress} Start Client");
    }

    private void OnApplicationQuit()
    {
        if (NetworkClient.isConnected)
        {
            networkManager.StopClient();
        }

        if (NetworkServer.active)
        {
            networkManager.StopServer();
        }
    }
}
