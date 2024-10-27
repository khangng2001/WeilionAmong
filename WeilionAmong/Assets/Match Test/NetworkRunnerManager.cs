using MatchTest;
using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkRunnerManager : MonoBehaviour, ISingleton
{
    [SerializeField] private GameObject networkRunnerPrefab;
    [SerializeField, ReadOnly] private GameObject _networkRunnerObject;

    [SerializeField, ReadOnly] private NetworkRunnerController _networkRunnerController;

    [SerializeField] private bool _isMatching;
    public bool IsMatching => _isMatching;

    #region Fast Test
    [Button]
    public void StartHost()
    {
        if (_networkRunnerObject == null)
        {
            _networkRunnerObject = Instantiate(networkRunnerPrefab);
            _networkRunnerController = _networkRunnerObject.GetComponent<NetworkRunnerController>();
        }

        _networkRunnerController.StartGame(Fusion.GameMode.Host);
    }
    [Button]
    public void Shutdown()
    {
        if (_networkRunnerController != null)
        {
            _networkRunnerController.Shutdown();
        }
    }
    #endregion

    public async void FindMatch(Action<bool> noticationCallback = null, Action disconnectServerCallback = null)
    {
        _isMatching = true;

        await StartClient(noticationCallback, disconnectServerCallback);
    }

    async Task StartHost(Action<bool> noticationCallback = null, Action disconnectServerCallback = null)
    {
        if (!_isMatching)
        {
            noticationCallback(false);
            return;
        }

        _networkRunnerObject = Instantiate(networkRunnerPrefab);
        _networkRunnerController = _networkRunnerObject.GetComponent<NetworkRunnerController>();

        await _networkRunnerController.StartGame(Fusion.GameMode.Host, (result) =>
        {
            if (result)
            {
                if (_isMatching)
                {
                    _isMatching = false;
                    noticationCallback(true);
                }
                else
                {
                    noticationCallback(false);
                }
            }
            else
            {
                _isMatching = false;
                noticationCallback(false);
            }
        }, disconnectServerCallback);
    }

    async Task StartClient(Action<bool> callback = null, Action disconnectServerCallback = null)
    {
        if (_networkRunnerObject == null)
        {
            _networkRunnerObject = Instantiate(networkRunnerPrefab);
            _networkRunnerController = _networkRunnerObject.GetComponent<NetworkRunnerController>();
        }

        await _networkRunnerController.StartGame(Fusion.GameMode.Client, async (result) =>
        {
            if (result)
            {
                if (_isMatching)
                {
                    _isMatching = false;
                    callback(true);
                }
                else
                {
                    callback(false);
                }
            }
            else
            {
                await StartHost(callback, disconnectServerCallback);
            }
        }, disconnectServerCallback);
    }

    public void StopMatch()
    {
        _isMatching = false;
        Shutdown();
    }

    public void Shutdown(Action<bool> callback = null)
    {
        if (_networkRunnerController)
        {
            _networkRunnerController.Shutdown();
            _networkRunnerController = null;
            callback?.Invoke(true);
        }

        if (_networkRunnerObject)
        {
            Destroy(_networkRunnerObject);
            _networkRunnerObject = null;
        }
    }
}
