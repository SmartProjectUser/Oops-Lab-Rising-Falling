using System.Collections;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    //States:
    //0 - normal
    //1 - rain
    //2 - wind

    private int state = 1;

    //Timers:
    private IEnumerator branchSpawnTimer;
    private IEnumerator insectSpawnTimer;
    private IEnumerator dropSpawnTimer;
    private IEnumerator rockSpawnTimer;

    public BarrierSpawner spawner;
    public LeafController leafController;
    public GameObject background;
    public GameObject rain;
    public GameObject wind;

    private int rainSoundID;
    private int windSoundID;

    private void Start()
    {
        rainSoundID = ANAMusic.load("Android Native Audio/rain.mp3");
        windSoundID = ANAMusic.load("Android Native Audio/wind.mp3");
    }

    public void StartGame()
    {
        StartCoroutine(ChangeWeatherTimer(15));
    }

    public void StopGame()
    {
        state = 0;
        ChangeWeather();
        StopAllCoroutines();
    }

    private void ChangeWeather()
    {
        switch (state)
        {
            case 0: //normal

                StopCoroutines();
                rain.SetActive(false);
                wind.SetActive(false);
                
                if (background.GetComponent<Background>().state == 1)
                    background.GetComponent<Animation>().Play("ShowNormal");

                branchSpawnTimer = BranchSpawnTimer(5);
                StartCoroutine(branchSpawnTimer);
                insectSpawnTimer = InsectSpawnTimer(3);
                StartCoroutine(insectSpawnTimer);
                rockSpawnTimer = RockSpawnTimer(10);
                StartCoroutine(rockSpawnTimer);

                break;
            case 1: //rain

                StopCoroutines();
                rain.SetActive(true);
                wind.SetActive(false);

                if (background.GetComponent<Background>().state == 0)
                    background.GetComponent<Animation>().Play("ShowRain");

                branchSpawnTimer = BranchSpawnTimer(5);
                StartCoroutine(branchSpawnTimer);
                dropSpawnTimer = DropSpawnTimer(2);
                StartCoroutine(dropSpawnTimer);
                rockSpawnTimer = RockSpawnTimer(10);
                StartCoroutine(rockSpawnTimer);

                ANAMusic.play(rainSoundID);

                break;
            case 2: //wind
                StopCoroutines();

                SetupWind();

                if (background.GetComponent<Background>().state == 0)
                    background.GetComponent<Animation>().Play("ShowRain");

                rain.SetActive(false);
                wind.SetActive(true);

                branchSpawnTimer = BranchSpawnTimer(5);
                StartCoroutine(branchSpawnTimer);
                rockSpawnTimer = RockSpawnTimer(10);
                StartCoroutine(rockSpawnTimer);

                ANAMusic.play(windSoundID);

                break;
        }
    }

    private void RandomizeWeather()
    {
        int oldState = state;
        while (oldState == state)
        {
            state = Random.Range(0, 3);
        }
    }

    //Timers
    public IEnumerator ChangeWeatherTimer(int interval)
    {

        while (true)
        {
            RandomizeWeather();
            ChangeWeather();
            yield return new WaitForSeconds(interval);
        }
    }


    public IEnumerator BranchSpawnTimer(int interval)
    {

        while (true)
        {
            spawner.SpawnBranch();
            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator InsectSpawnTimer(int interval)
    {

        while (true)
        {
            spawner.SpawnInsect();
            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator DropSpawnTimer(int interval)
    {

        while (true)
        {
            spawner.SpawnDrop();
            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator RockSpawnTimer(int interval)
    {

        while (true)
        {
            Debug.Log("Spawn rock");
            spawner.SpawnRock();
            yield return new WaitForSeconds(interval);
        }
    }

    private void StopCoroutines()
    {
        if (branchSpawnTimer != null)
            StopCoroutine(branchSpawnTimer);
        if (insectSpawnTimer != null)
            StopCoroutine(insectSpawnTimer);
        if (dropSpawnTimer != null)
            StopCoroutine(dropSpawnTimer);
        if (rockSpawnTimer != null)
            StopCoroutine(rockSpawnTimer);
    }

    private void SetupWind()
    {
        //Randomize wind speed and set it to leaf
        int side = Random.Range(0, 2);
        int sign = side == 0 ? 1 : -1;
        float speed = Random.Range(0.1f, 0.9f) * -sign; // positive - to left side, negative - to right
        leafController.windForce = speed;

        //move wind particles to right side
        wind.transform.position = new Vector3(2.48f * sign, wind.transform.position.y, -9f);

        //rotate wind particles to right side
        int mirrorAngle = sign == 1 ? 90 : -90;
        wind.transform.rotation = Quaternion.Euler(0, 0, mirrorAngle);
    }
}
