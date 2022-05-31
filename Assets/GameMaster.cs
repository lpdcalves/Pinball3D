using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject pinballSpawner;
    public GameObject pinballPrefab;
    public GameObject floatingPointsPrefab;

    private GameObject current_pinball;

    private int score;
    public Text scoreText;
    public int minFloatingPointsSize = 8;
    public int maxFloatingPointsSize = 16;

    private int numBalls = 5;
    public Text numBallsText;

    float timerBlackhole = 0f;
    bool canUseBlackhole = true;

    void Start()
    {
        current_pinball = Instantiate(pinballPrefab, pinballSpawner.transform.position, Quaternion.identity);
        scoreText.text = "Score: 0";
        numBallsText.text = "Balls: " + numBalls;
    }

    void Update()
    {
        if (!canUseBlackhole)
        {
            timerBlackhole += Time.deltaTime;
            if (timerBlackhole > 1)
            {
                canUseBlackhole = true;
                timerBlackhole = 0;
            }
        }

        if (numBalls > 0)
        {
            if (current_pinball.transform.position.y < -1 || current_pinball.transform.position.y > 100)
            {
                GameObject temp = current_pinball;
                Destroy(temp);

                current_pinball = Instantiate(pinballPrefab, pinballSpawner.transform.position, Quaternion.identity);
                numBalls -= 1;
                numBallsText.text = "Balls: " + numBalls;
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        GameObject points = Instantiate(floatingPointsPrefab, current_pinball.transform.position + new Vector3(1f, 1f, -1f), Quaternion.AngleAxis(60, Vector3.right));
        // calculating size of the point text popup
        int sizeText = minFloatingPointsSize + Mathf.RoundToInt((scoreToAdd / 500f) * (maxFloatingPointsSize - minFloatingPointsSize));
        points.GetComponentInChildren<TextMeshPro>().text = "+"+scoreToAdd.ToString();
        points.GetComponentInChildren<TextMeshPro>().fontSize = sizeText;
    }

    public void TeleportBlackhole(Vector3 nextPosition)
    {
        if (canUseBlackhole)
        {
            canUseBlackhole = false;
            current_pinball.transform.position = nextPosition;
        }
    }
}
