using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public int sceneID = 1;

    public Image image;
    public Text title;

    private string loadStr = "";
    private int dotCount = 1;

    private bool loadCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        loadStr = "LOADING";

        title.text = loadStr + ".";

        StartCoroutine(LoadingText());
        StartCoroutine(AsyncLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            image.fillAmount = progress;

            yield return null;
        }

        loadCompleted = true;
    }

    private IEnumerator LoadingText()
    {
        while (!loadCompleted)
        {
            string loadingStr = loadStr;

            if (dotCount < 4)
            {
                title.text += ".";
                dotCount++;
            }               
            else
            {
                title.text = loadStr + ".";
                dotCount = 1;
            }               

            yield return null;
        }
    }
}
