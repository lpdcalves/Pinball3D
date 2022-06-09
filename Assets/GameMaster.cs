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
    public GameObject bloqueadorCanaleta;
    public AudioSource gameoverEffect;

    private GameObject current_pinball;
    private MySceneManager sceneManager;

    private int score;
    public Text scoreText;
    public int minFloatingPointsSize = 8;
    public int maxFloatingPointsSize = 16;
    public bool useSlowTime = false;

    private int numBalls = 5;
    public Text numBallsText;

    float timerBlackhole = 0f;
    bool canUseBlackhole = true;
    bool endGame = false;
    float endGameTimer = 0;

    void Start()
    {
        sceneManager = FindObjectOfType<MySceneManager>();
        current_pinball = Instantiate(pinballPrefab, pinballSpawner.transform.position, Quaternion.identity);
        scoreText.text = "Score: 0";
        numBallsText.text = "Balls: " + numBalls;
    }

    void Update()
    {
        if (!canUseBlackhole)
        {
            timerBlackhole += Time.deltaTime;
            if (timerBlackhole > 0.8)
            {
                if (useSlowTime) { Time.timeScale = 1f; }
                canUseBlackhole = true;
                timerBlackhole = 0;
            }
        }

        if (current_pinball != null)
        {
            if (current_pinball.transform.position.y < 6 || current_pinball.transform.position.y > 100)
            {
                bloqueadorCanaleta.GetComponent<Collider>().isTrigger = true;

                GameObject temp = current_pinball;
                Destroy(temp);

                if (numBalls > 0)
                {
                    current_pinball = Instantiate(pinballPrefab, pinballSpawner.transform.position, Quaternion.identity);
                    numBalls -= 1;
                    numBallsText.text = "Balls: " + numBalls;
                }
                else
                {
                    endGame = true;
                    gameoverEffect.Play();
                }
            }
        }

        if (endGameTimer < 3 && endGame)
            endGameTimer += Time.deltaTime;
        if (endGameTimer > 3)
            sceneManager.EndGame();
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

    public void TeleportBlackhole(Vector3 nextPosition, Vector3 dir)
    {
        if (canUseBlackhole)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                float randomTurn = Random.Range(-0.1f, 0.1f);
                dir += new Vector3(randomTurn, randomTurn, randomTurn);
            }
            canUseBlackhole = false;
            current_pinball.transform.position = nextPosition;
            current_pinball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            current_pinball.GetComponent<Rigidbody>().AddForce(dir * 25f, ForceMode.Impulse);
        }
    }
}
