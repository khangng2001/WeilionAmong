using UnityEngine;

[CreateAssetMenu(fileName = "SessionInfo", menuName = "ScriptableObject/SessionInfo")]
public class SessionInfoScriptableObject : ScriptableObject
{
    public string SessionName;
    public int PlayerCount;
    public int MaxPlayer;
    public string Region;
    public bool IsOpen;
    public bool IsVisible;
    public string[] SessionProperties;
}
