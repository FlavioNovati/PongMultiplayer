using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreen : MonoBehaviour
{

    [SerializeField] private RectTransform _titleScreenTab;
    [SerializeField] private RectTransform _hostScreenTab;

#if UNITY_EDITOR
    public void QuitGame() => EditorApplication.isPlaying = false;
#else
    public void QuitGame() => Application.Quit();
#endif

    public void ReturnToMainTab()
    {
        _titleScreenTab.gameObject.SetActive(true);
        _hostScreenTab.gameObject.SetActive(false);
    }

    public void HostGame()
    {
        _titleScreenTab.gameObject.SetActive(false);
        _hostScreenTab.gameObject.SetActive(true);
    }

    public void JoinGame()
    {

    }
}
