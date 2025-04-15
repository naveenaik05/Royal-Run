using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TMP_Text scoreboardText;
    [SerializeField] AudioSource coinCollectingSFX;
    int score = 0;

    public void IncreaseScore(int scoreAmount)
    {
        if(gameManager.GameOver) return;
        coinCollectingSFX.Play();
        score += scoreAmount;
        scoreboardText.text = score.ToString();
    }
}
