using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafController : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private float speedModifier = 0.01f;

    public float windForce = 0;
    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        isPlaying = false;
        Debug.Log("GAME OVER");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("delta pos:" + touch.deltaPosition.x);

                MoveLeaf(SmoothPos(touch.deltaPosition.x));
            }

            if (touch.phase == TouchPhase.Ended)
            {

            }
        }

        if (windForce != 0)
        {
            MoveLeaf(windForce);
        }
    }

    private void MoveLeaf(float pos)
    {
        if (transform.localPosition.x + pos * speedModifier < -1.1f || 
            transform.localPosition.x + pos * speedModifier > 1.1f)
        {
            pos = 0;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + pos * speedModifier,
            transform.localPosition.y, 0);
    }

    private float SmoothPos(float pos)
    {
        if (pos > 50)
            pos = 50;
        else if (pos < -50)
            pos = -50;

        return pos;
    }
}
