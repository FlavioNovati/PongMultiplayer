using UnityEngine;

using Mirror;
using UnityEngine.UI;

public class LobbyMenu : NetworkBehaviour
{
    [SerializeField] private Button _startGameButton;

    private void OnEnable()
    {
        _startGameButton.interactable = false;
    }
}
