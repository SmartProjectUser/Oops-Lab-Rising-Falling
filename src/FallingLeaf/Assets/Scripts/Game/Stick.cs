using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public MainMenu menu;

    public void onAnimationFinished()
    {
        menu.StartGame();
        //Destroy(gameObject);
    }
}
