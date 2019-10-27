using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public float score = 0;

    public Text scoreLabel;

    private IEnumerator scoreTimer;

    // Start is called before the first frame update
    void Start()
    {
        scoreTimer = ScoreTimer(0.1f);
        scoreLabel.text = Mathf.Round(score * 100f) / 100f + " cm";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        StartCoroutine(scoreTimer);
    }

    public void GameOver()
    {
        StopCoroutine(scoreTimer);

        score = 0;
        scoreLabel.text = Mathf.Round(score * 100f) / 100f + " cm";

        PlayerPrefs.SetFloat(PrefsNames.PREF_BEST, score);
    }

    public IEnumerator ScoreTimer(float interval)
    {

        while (true)
        {
            score += 0.1f;
            scoreLabel.text = Mathf.Round(score * 100f) / 100f + " cm";

            yield return new WaitForSeconds(interval);
        }
    }
}
