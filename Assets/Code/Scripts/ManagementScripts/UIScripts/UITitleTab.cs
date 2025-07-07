using UnityEngine;

public class UITitleTab : MonoBehaviour
{
    [SerializeField] private RectTransform _joinTab;
    [SerializeField] private RectTransform _mainTab;

    private void OnEnable()
    {
        _mainTab.gameObject.SetActive(true);
        _joinTab.gameObject.SetActive(false);
    }
}
