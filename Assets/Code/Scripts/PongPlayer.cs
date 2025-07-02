using Mirror;
using UnityEngine;

public class PongPlayer : NetworkBehaviour
{
    [SerializeField] public string PlayerName = "Player";
    [SerializeField] public int PlayerConnectionID = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("I'm connected");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("Client Disconnected");
    }
}
