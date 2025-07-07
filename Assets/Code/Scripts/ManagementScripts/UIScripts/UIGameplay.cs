using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _lScoreText;
    [SerializeField] private TMP_Text _rScoreText;

    private void OnEnable()
    {
        MatchController matchController = FindFirstObjectByType<MatchController>();
        matchController.OnScoreChanged += UpdateScore;
    }

    private void UpdateScore(int lScore, int rScore)
    {
        _lScoreText.text = lScore.ToString();
        _rScoreText.text = rScore.ToString();
    }
}
