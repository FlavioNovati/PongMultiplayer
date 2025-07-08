using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Mirror;

public class UIJoinTab : MonoBehaviour
{
    [FormerlySerializedAs("Nework Address Input Field")]
    [SerializeField] private TMP_InputField _networkAddressField;
    [SerializeField] private Button _joinButton;

    private void Awake()
    {
        _networkAddressField.onSubmit.AddListener(delegate { OnNetworkAddressChanged(); });
    }

    private void OnEnable()
    {
        _joinButton.interactable = false;
        _networkAddressField.text = string.Empty;
    }

    private void OnNetworkAddressChanged()
    {
        string address = _networkAddressField.text;
        address.Replace(' ', '_');

        if (string.IsNullOrEmpty(address))
            _joinButton.interactable = false;
        else
            _joinButton.interactable = true;
    }

    public void JoinLobby()
    {
        string networkAddress = _networkAddressField.text;
        NetworkManager.singleton.networkAddress = networkAddress;
        NetworkManager.singleton.StartClient();
    }
}
