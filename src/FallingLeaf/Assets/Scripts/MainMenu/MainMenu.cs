using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject leaf;
    public GameObject stick;
    public GameObject gameOver;
    public Text gameOverBest;

    public GameObject wind;
    public WeatherController weatherController;
    public LeafController leafController;
    public ScoreController scoreController;

    public GameObject[] buttons;

    private Animation anim;
    private Vector2 stickStartPos;

    private static int buttonSoundID;

    // Start is called before the first frame update
    void Start()
    {
        buttonSoundID = AndroidNativeAudio.load("Android Native Audio/button.ogg");

        stickStartPos = new Vector2(stick.transform.localPosition.x, stick.transform.localPosition.y);

        anim = leaf.GetComponent<Animation>();
        anim.Play("LeafMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPreGameAnimation()
    {
        wind.SetActive(true);
        leaf.GetComponent<Animation>().Play("LeafStart");
        stick.GetComponent<Animation>().Play("StickMenu");
    }

    public void StartGame()
    {
        leafController.isPlaying = true;
        leaf.GetComponent<Animation>().Play("LeafSwing");
        weatherController.StartGame();
        scoreController.GameStart();
    }

    // Buttons Click Sector
    public void onPlay()
    {
        AndroidNativeAudio.play(buttonSoundID);

        StartPreGameAnimation();

        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
    }

    public void onResume()
    {
        AndroidNativeAudio.play(buttonSoundID);

        gameOver.SetActive(false);
        Time.timeScale = 1;

        leaf.SetActive(true);
        anim.Play("LeafMenu");

        stick.transform.localPosition = new Vector3(stickStartPos.x, stickStartPos.y, 35.37f);

        buttons[0].SetActive(true);
    }

    public void showGameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        leaf.SetActive(false);
        
        float score = PlayerPrefs.GetFloat(PrefsNames.PREF_BEST, 0f);
        gameOverBest.text = Mathf.Round(score * 100f) / 100f + " cm";
    }

    public void onShop()
    {
        //TODO
    }

    public void onSettings()
    {
        //TODO
    }
}
