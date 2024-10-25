using Fusion;
using MatchTest;
using System;
using UnityEngine;

public class NetworkRunnerManager : MonoBehaviour, ISingleton
{
    public GameObject NetworkRunnerPrefab;
    public GameObject NetworkRunnerObject;

    public NetworkRunnerController NetworkRunnerController;

    public bool IsFinding;

    public void FindMatch(Action<bool> callback)
    {
        if (NetworkRunnerObject == null)
        {
            NetworkRunnerObject = Instantiate(NetworkRunnerPrefab);
            NetworkRunnerController = NetworkRunnerObject.GetComponent<NetworkRunnerController>();
        }

        NetworkRunnerController.StartGame(GameMode.Host, callback);
    }

    public void Shutdown(Action<bool> callback)
    {
        if (!NetworkRunnerController) return;
        NetworkRunnerController.Shutdown();
        callback?.Invoke(true);
    }

    //private void Update()
    //{
    //    if (IsFinding)
    //    {
    //        if (NetworkRunnerObject == null)
    //        {
    //            NetworkRunnerObject = Instantiate(NetworkRunnerPrefab);
    //            NetworkRunner = NetworkRunnerObject.GetComponent<NetworkRunner>();
    //        }
    //    }
    //    else if (NetworkRunnerObject != null)
    //    {
    //        Destroy(NetworkRunnerObject);
    //        if (NetworkRunner)  NetworkRunner = null;
    //    }
    //}
}
