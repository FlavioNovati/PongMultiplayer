using Mirror;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoinLobbyTab : MonoBehaviour
{
    [SerializeField] private TMP_InputField _addressInput;
    [SerializeField] private Button _joinButton;

    private void Awake()
    {
        _addressInput.onValueChanged.AddListener(delegate { OnTextChanged(); });
    }

    private void OnEnable()
    {
        _addressInput.text = string.Empty;
        _joinButton.interactable = false;
    }

    private void OnTextChanged()
    {
        if(_addressInput.text != string.Empty)
            _joinButton.interactable = true;
        else
            _joinButton.interactable = false;
    }

    public void JoinLobby()
    {
        //TODO: JoinLobby
        NetworkManager.singleton.networkAddress = _addressInput.text;
        NetworkManager.singleton.StartClient();
    }
}
