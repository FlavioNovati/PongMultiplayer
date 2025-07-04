using System;
using UnityEngine;
using Mirror;

public class PlayerInput : NetworkBehaviour
{
    public event Action<Vector3> OnInputDirection;

    #region SERVER
    [Command]
    private void CmdOnInputDirection(Vector3 direction)
    {
        OnInputDirection?.Invoke(direction);
    }
    #endregion
    
    #region CLIENT
    [Client]
    private void Update()
    {
        //Update Only if has authority on this object
        if (!isOwned) return;

        ReadInput();
    }

    [Client]
    private void ReadInput()
    {
        Vector3 newDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            newDirection = Vector3.up;

        if (Input.GetKey(KeyCode.DownArrow))
            newDirection = Vector3.down;

        CmdOnInputDirection(newDirection);
    }
    #endregion
}
