#if UNITY_EDITOR
using Unity.VisualScripting;
using UnityEditor;
#endif

using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _gameUI;
    [SerializeField] private RectTransform _titleUI;
    
    private void OnEnable()
    {
        _gameUI.gameObject.SetActive(false);
        _titleUI.gameObject.SetActive(true);

        MatchController.OnMatchStart += SetGameUI;
        MatchController.OnMatchStop += SetTitle;
    }

    private void OnDisable()
    {
        MatchController.OnMatchStart -= SetGameUI;
        MatchController.OnMatchStop -= SetTitle;
    }

    private void SetGameUI()
    {
        _gameUI.gameObject.SetActive(true);
        _titleUI.gameObject.SetActive(false);
    }

    private void SetTitle()
    {
        _gameUI.gameObject.SetActive(false);
        _titleUI.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    public void QuitGame() => EditorApplication.isPlaying = false;
#else
    public void QuitGame() => Application.Quit();
#endif
}
