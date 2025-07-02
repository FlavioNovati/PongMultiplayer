using Mirror;
using UnityEngine;

public class PongRacket : NetworkBehaviour
{
    [SyncVar(hook = "HandleNameChange")]
    public string RacketName = "Player";

    #region Client
    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    private void HandleNameChange(string oldName, string newName)
    {
        Debug.Log($"Name Changed from {oldName} to {newName}");
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            MoveRacket(Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveRacket(Vector3.down);
        }
    }

    public void MoveRacket(Vector3 direction)
    {
        CmdMove(direction);
    }

    #endregion

    #region Server
    [Command]
    private void CmdMove(Vector3 direction)
    {
        if (!authority && !isOwned)
            return;

        Debug.Log(this.name + " " + direction);
    }
    #endregion
}
