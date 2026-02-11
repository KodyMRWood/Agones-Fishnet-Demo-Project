using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCubeCreator : NetworkBehaviour
{
    public NetworkObject CubePrefab;

    public override void OnStartClient()
    {
        if (IsOwner)
            GetComponent<PlayerInput>().enabled = true;
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
            SpawnCube();
    }

    // We are using a ServerRpc here because the Server needs to do all network object spawning.
    [ServerRpc]
    private void SpawnCube()
    {
        NetworkObject obj = Instantiate(CubePrefab, transform.position, Quaternion.identity);
        obj.GetComponent<SyncMaterialColor>().Color.Value = Random.ColorHSV();
        Spawn(obj); // NetworkBehaviour shortcut for ServerManager.Spawn(obj);
    }
}