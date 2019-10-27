using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    //Barriers:
    //0 - branch
    //1 - insect
    //2 - drop
    //3 - rock \m/

    public GameObject[] barriers;

    public LeafController leafController;
    public ScoreController scoreController;
    public MainMenu mainMenu;

    public WeatherController weather;

    private int insectSound;

    private void Start()
    {
        insectSound = AndroidNativeAudio.load("Android Native Audio/insect.mp3");
    }

    public void GameOver()
    {
        leafController.GameOver();
        scoreController.GameOver();
        mainMenu.showGameOver();
        weather.StopGame();
    }

    public void SpawnBranch()
    {
        GameObject tmp = barriers[0];
        int multiplier = Randomize() ? 1 : -1;
        int mirrorAngle = multiplier == -1 ? 180 : 0;

        GameObject branch = Instantiate(tmp);

        branch.transform.parent = gameObject.transform;
        branch.transform.rotation = Quaternion.Euler(0, mirrorAngle, 0);
        branch.transform.localPosition = new Vector3(multiplier * 2.45f, -5f, 0);
    }

    public void SpawnInsect()
    {
        GameObject tmp = barriers[1];
        int multiplier = Randomize() ? 1 : -1;
        int mirrorAngle = multiplier == 1 ? 180 : 0;

        GameObject insect = Instantiate(tmp);

        insect.transform.parent = gameObject.transform;
        insect.GetComponent<Insect>().direction = multiplier * -1;
        insect.GetComponent<Insect>().speed = Random.Range(0.007f, 0.03f);

        insect.transform.rotation = Quaternion.Euler(0, mirrorAngle, 0);
        insect.transform.localPosition = new Vector3(multiplier * 2.45f, -8f, 0);

        AndroidNativeAudio.play(insectSound);
    }

    public void SpawnDrop()
    {
        GameObject tmp = barriers[2];

        GameObject drop = Instantiate(tmp);

        drop.transform.parent = gameObject.transform;
        drop.GetComponent<Drop>().speed = Random.Range(0.01f, 0.1f);
        drop.transform.localPosition = new Vector3(Random.Range(-2.45f, 2.45f), 12f, 0);

    }

    public void SpawnRock()
    {
        GameObject tmp = barriers[3];
        int multiplier = Randomize() ? 1 : -1;
        int mirrorAngle = multiplier == -1 ? 180 : 0;

        GameObject rock = Instantiate(tmp);

        rock.transform.parent = gameObject.transform;
        rock.transform.rotation = Quaternion.Euler(0, mirrorAngle, 0);
        rock.transform.localPosition = new Vector3(multiplier * 2.45f, -5f, 0);

    }

    private bool Randomize()
    {
        int rand = Random.Range(0, 2);
        Debug.Log("rand: " + rand);
        return rand == 0;
    }
}
