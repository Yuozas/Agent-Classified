using System.Collections;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    #region ScoreData

    public int newRunScore;
    private float scoringSpeed;

    #endregion ScoreData

    private void Start()
    {
        GameData.Instance.ScoreHandler = this;
        newRunScore = 0;
        scoringSpeed = 1f;
        StartScoring();
    }

    private IEnumerator Score(System.Action onComplete)
    {
        newRunScore++;
        if (newRunScore.Modulus(10, 0))
            GameData.Instance.MonetaryHandler.AddMoney(1);
        yield return new WaitForSeconds(scoringSpeed);
        onComplete?.Invoke();
    }

    private void StartScoring() => StartCoroutine(Score(StartScoring));

    public void EndRun() => SetData();


    private void SetData()
    {
        if(HighScore)
            PlayerPrefs.SetInt("HighScore", newRunScore);
        PlayerPrefs.SetInt("Score", newRunScore);
    }
    private bool HighScore => newRunScore > PlayerPrefs.GetInt("HighScore");

}