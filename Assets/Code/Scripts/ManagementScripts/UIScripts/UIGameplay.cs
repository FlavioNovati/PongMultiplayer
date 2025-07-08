using TMPro;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _lScoreText;
    [SerializeField] private TMP_Text _rScoreText;

    [SerializeField] private TMP_Text _playerWonText;

    private void OnEnable()
    {
        MatchController matchController = FindFirstObjectByType<MatchController>();
        matchController.OnScoreChanged += UpdateScore;
        MatchController.OnPlayerWon += SetPlayerWon;

        _playerWonText.gameObject.SetActive(false);
    }

    private void UpdateScore(int lScore, int rScore)
    {
        _lScoreText.text = lScore.ToString();
        _rScoreText.text = rScore.ToString();
    }

    private void SetPlayerWon(string playerWon)
    {
        _playerWonText.gameObject.SetActive(true);
        _playerWonText.text = playerWon.ToString();
    }
}
