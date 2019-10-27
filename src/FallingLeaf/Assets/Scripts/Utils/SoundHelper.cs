using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    private static int buttonSoundID;
    private static int musicID;
    private static int insectSoundID;

    private void Start()
    {
        AndroidNativeAudio.makePool(16);
        musicID = ANAMusic.load("Android Native Audio/melody.ogg");
        ANAMusic.play(musicID);
    }

}
