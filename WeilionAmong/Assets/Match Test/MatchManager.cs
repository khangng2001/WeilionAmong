using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StateGame
{ 
    Lobby,
    Matching,
    Ingame
}

public class MatchManager : MonoBehaviour
{
    [Header("Layout")]
    public GameObject LobbyLayout; 
    public GameObject MatchingLayout; 
    public GameObject InGameLayout;

    [Header("Lobby")]
    public Button MatchingBtn;

    [Header("Matching")]
    public Button CancelBtn;
    public TextMeshProUGUI MatchTimeText;

    [Header("InGame")]
    public Button ShutdownBtn;

    public StateGame StateGame;
    public float MatchTimeCount;

    private void Awake()
    {
        //
        MatchingBtn.onClick.AddListener(() =>
        {
            Matching();
        });

        //
        CancelBtn.onClick.AddListener(() =>
        {
            CancelMatching();
        });

        //
        ShutdownBtn.onClick.AddListener(() =>
        {
            Shutdown();
        });
    }

    public void SwitchState(StateGame state)
    {
        LobbyLayout.SetActive(false);
        MatchingLayout.SetActive(false);
        InGameLayout.SetActive(false);

        switch(state)
        {
            case StateGame.Lobby:
                LobbyLayout.SetActive(true);

                break;

            case StateGame.Matching:
                MatchingLayout.SetActive(true);

                break;
            case StateGame.Ingame:
                InGameLayout.SetActive(true);

                break;
        }
    }

    void Matching()
    {
        SwitchState(StateGame.Matching);
        MatchTimeCount = 0;
        Singleton<NetworkRunnerManager>.Instance.FindMatch((result) =>
        {
            if (result)
            {
                SwitchState(StateGame.Ingame);
            }
            else
            {
                SwitchState(StateGame.Lobby);
                Debug.LogWarning("Match Again");
            }
        }, () =>
        {
            SwitchState(StateGame.Lobby);
            Debug.LogWarning("Disconnect Server");
        });
    }

    void CancelMatching()
    {
        SwitchState(StateGame.Lobby);
        Singleton<NetworkRunnerManager>.Instance.StopMatch();
    }

    void Shutdown()
    {
        Singleton<NetworkRunnerManager>.Instance.Shutdown((result) =>
        {
            if (result)
            {
                SwitchState(StateGame.Lobby);
            }
        });
    }

    void CountTimeMatching()
    {
        if (Singleton<NetworkRunnerManager>.Instance.IsMatching)
        {
            MatchTimeCount += Time.deltaTime;
            MatchTimeText.text = MatchTimeCount.ToString();
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        CountTimeMatching();
    }
}
