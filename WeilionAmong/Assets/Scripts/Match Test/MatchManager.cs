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
    public bool IsMatching;
    public float MatchTimeCount;

    private void Awake()
    {
        MatchingBtn.onClick.AddListener(() =>
        {
            IsMatching = true;
            MatchTimeCount = 0;
            SwitchState(StateGame.Matching);
            Singleton<NetworkRunnerManager>.Instance.FindMatch((result) =>
            {
                if (result)
                {
                    IsMatching = false;
                    SwitchState(StateGame.Ingame);
                }
            });
        });

        CancelBtn.onClick.AddListener(() =>
        {
            IsMatching = false;
        });

        ShutdownBtn.onClick.AddListener(() =>
        {
            Singleton<NetworkRunnerManager>.Instance.Shutdown((result) =>
            {
                if (result)
                {
                    IsMatching = false;
                    SwitchState(StateGame.Lobby);
                }
            });
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

    private void Update()
    {
        if (IsMatching)
        {
            MatchTimeCount += Time.deltaTime;
            MatchTimeText.text = MatchTimeCount.ToString();
        }
        else
        {

            return;
        }
    }
}
