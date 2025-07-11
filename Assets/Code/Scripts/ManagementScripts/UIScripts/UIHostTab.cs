using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Mirror;

public class UIHostTab : MonoBehaviour
{
    [FormerlySerializedAs("Nework Address Input Field")]
    [SerializeField] private TMP_InputField _networkAddressField;
    [SerializeField] private Button _hostButton;

    private void Awake()
    {
        _networkAddressField.onSubmit.AddListener(delegate { OnNetworkAddressChanged(); } );
    }

    private void OnEnable()
    {
        _hostButton.interactable = false;
        _networkAddressField.text = string.Empty;
    }

    private void OnNetworkAddressChanged()
    {
        string address = _networkAddressField.text;
        address.Replace(' ', '_');

        if (string.IsNullOrEmpty(address))
            _hostButton.interactable = false;
        else
            _hostButton.interactable = true;
    }

    public void HostLobby()
    {
        string networkAddress = _networkAddressField.text;

        NetworkManager.singleton.networkAddress = networkAddress;
        NetworkManager.singleton.StartHost();
    }
}
