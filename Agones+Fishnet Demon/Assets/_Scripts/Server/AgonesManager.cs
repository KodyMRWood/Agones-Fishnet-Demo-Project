
using FishNet.Managing;
using UnityEngine;
using System.Collections;
using Agones;


public class AgonesManager : MonoBehaviour
{
    private AgonesSdk agonesSdk;

    private void Start()
    {
        if (Application.isBatchMode || IsHeadlessMode())
        {
            InitializeAgones();
        }
    }

    private void InitializeAgones()
    {
        agonesSdk = gameObject.AddComponent<AgonesSdk>();

        agonesSdk.Connect();

        var connected = agonesSdk.Connect();

        if (connected.IsCompleted)
        {
            Debug.Log("Successfully connected to Agones SDK");
            StartCoroutine(HealthCheckRoutine());

            // Mark the game server as ready
            bool ready = agonesSdk.Ready().IsCompleted;
            Debug.Log(ready ? "Server marked as Ready" : "Failed to mark server as Ready");
        }
        else
        {
            Debug.LogError("Failed to connect to Agones SDK");
        }
    }

    private IEnumerator HealthCheckRoutine()
    {
        while (true)
        {
            //agonesSdk.Health();
            yield return new WaitForSeconds(2f);
        }
    }

    private bool IsHeadlessMode()
    {
        return SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null;
    }

    private void OnApplicationQuit()
    {
        if (agonesSdk != null)
        {
            agonesSdk.Shutdown();
        }
    }
}