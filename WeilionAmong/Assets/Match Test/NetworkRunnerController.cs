using System;
using System.Collections.Generic;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using Fusion.Photon.Realtime;

namespace MatchTest
{
    public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef playerPrefab;

        private NetworkRunner _runner;
        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        public SessionInfoScriptableObject sessionInfo;

        public async void StartGame(GameMode gameMode, Action<bool> callback = null)
        {
            _runner = GetComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);

            StartGameResult result = await _runner.StartGame(new StartGameArgs()
            {
                GameMode = gameMode,
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                SessionName = sessionInfo.SessionName,
                PlayerCount = sessionInfo.MaxPlayer,
                MatchmakingMode = MatchmakingMode.FillRoom,
            });

            if (result.Ok)
            {
                callback?.Invoke(true);
            }
            else
            {
                callback?.Invoke(false);
                Debug.LogWarning(result.ShutdownReason);
            }
        }

        public void Shutdown()
        {
            _runner = GetComponent<NetworkRunner>();
            _runner.Shutdown();
        }
       
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {

        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {

        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (!runner.IsServer) return;
            NetworkObject networkObject = runner.Spawn(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            networkObject.AssignInputAuthority(player);
            _spawnedCharacters.Add(player, networkObject);
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                _runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            var data = new NetworkInputData();

            InputHandler inputHandler = GetComponent<InputHandler>();
            if (inputHandler)
            {
                data.MovementInput = inputHandler.PlayerMovement;
                dir = data.MovementInput;
            }

            input.Set(data);
        }

        public Vector2 dir;

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {

        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {

        }

        public void OnConnectedToServer(NetworkRunner runner)
        {

        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {

        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {

        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {

        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {

        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {

        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {

        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {

        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {

        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {

        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {

        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {

        }
    }
}
