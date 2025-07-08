using UnityEngine;

public class UITitleTab : MonoBehaviour
{
    [SerializeField] private RectTransform _mainTab;
    [SerializeField] private RectTransform _joinTab;
    [SerializeField] private RectTransform _hostTab;
    [SerializeField] private RectTransform _lobbyTab;

    private void OnEnable()
    {
        _mainTab.gameObject.SetActive(true);
        _joinTab.gameObject.SetActive(false);
        _lobbyTab.gameObject.SetActive(false);
        _hostTab.gameObject.SetActive(false);
    }
}
