#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _gameUI;
    [SerializeField] private RectTransform _titleUI;
    [SerializeField] private RectTransform _disconnectionUI;
    
    private void OnEnable()
    {
        _gameUI.gameObject.SetActive(false);
        _titleUI.gameObject.SetActive(true);

        MatchController.OnMatchStart += SetGameUI;
        MatchController.OnMatchStop += SetTitleUI;
        MatchManager.OnMatchInterrupted += SetDisconnetUI;
        MatchManager.OnGameEnded += SetTitleUI;
    }

    private void OnDisable()
    {
        MatchController.OnMatchStart -= SetGameUI;
        MatchController.OnMatchStop -= SetTitleUI;
        MatchManager.OnMatchInterrupted -= SetDisconnetUI;
    }

    private void SetGameUI()
    {
        _gameUI.gameObject.SetActive(true);
        _titleUI.gameObject.SetActive(false);
    }

    private void SetTitleUI()
    {
        _gameUI.gameObject.SetActive(false);
        _titleUI.gameObject.SetActive(true);
    }

    private void SetDisconnetUI()
    {
        _disconnectionUI.gameObject.SetActive(true);
        SetTitleUI();
    }

#if UNITY_EDITOR
    public void QuitGame() => EditorApplication.isPlaying = false;
#else
    public void QuitGame() => Application.Quit();
#endif
}
