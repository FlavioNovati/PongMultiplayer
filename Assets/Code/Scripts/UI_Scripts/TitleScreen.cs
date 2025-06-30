using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private RectTransform _titleScreenTab;
    [SerializeField] private RectTransform _hostScreenTab;
    [SerializeField] private RectTransform _joinScreenTab;
    [SerializeField] private RectTransform _playerListTab;

#if UNITY_EDITOR
    public void QuitGame() => EditorApplication.isPlaying = false;
#else
    public void QuitGame() => Application.Quit();
#endif

    private void OnEnable()
    {
        _titleScreenTab.gameObject.SetActive(true);
        _hostScreenTab.gameObject.SetActive(false);
        _joinScreenTab.gameObject.SetActive(false);
        _playerListTab.gameObject.SetActive(false);
    }
}
