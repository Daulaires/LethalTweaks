using System;
using Unity.Netcode;

namespace NetworkingPatch.Network
{
    internal class Networking : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            LevelEvent = null;

            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                Instance?.gameObject.GetComponent<NetworkObject>().Despawn();
            }
            Instance = this;

            base.OnNetworkSpawn();
        }

        [ClientRpc]
        public void EventClientRpc(string eventName)
        {
            LevelEvent?.Invoke(eventName);
        }

        public static event Action<String> LevelEvent;

        public static Networking Instance { get; private set; }
    }
}
