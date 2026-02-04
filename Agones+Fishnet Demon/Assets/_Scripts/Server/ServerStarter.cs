using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;

public class ServerStarter : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;

    private void Start()
    {
        // Check if running as dedicated server
        if (Application.isBatchMode || IsHeadlessMode())
        {
            StartServer();
        }
    }

    private void StartServer()
    {
        if (networkManager != null)
        {
            networkManager.ServerManager.StartConnection();
            Debug.Log("Server started on port " + networkManager.TransportManager.Transport.GetPort());
        }
    }

    private bool IsHeadlessMode()
    {
        // Check for headless mode indicators
        return SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
    }
}