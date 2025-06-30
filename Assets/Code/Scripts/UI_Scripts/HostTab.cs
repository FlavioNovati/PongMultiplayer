using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HostTab : MonoBehaviour
{
    [SerializeField] private TMP_InputField _addressInput;
    [SerializeField] private Button _hostButton;

    private void Awake()
    {
        _addressInput.onValueChanged.AddListener(delegate { OnTextChanged(); });
    }

    private void OnEnable()
    {
        _addressInput.text = string.Empty;
        _hostButton.interactable = false;
    }

    private void OnTextChanged()
    {
        if (_addressInput.text != string.Empty)
            _hostButton.interactable = true;
        else
            _hostButton.interactable = false;
    }

    public void HostGame()
    {
        //TODO: HostGame
        Debug.Log("Starting Host");
        NetworkManager.singleton.networkAddress = _addressInput.text;
        NetworkManager.singleton.StartHost();
    }
}
