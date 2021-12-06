using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int counter = 0;
    public Text countText;
    public GameObject originalTarget;
    private GameObject target;
    private float canvasWidth;
    private float canvasHeight;
    private float canvasX;
    private float canvasY;
    private float spawnTimer = 0;
    private float scoreTimer = 0;
    private float margin = 100f;
    public GameObject canvas;
    public GameObject winText;
    public GameObject confettiBurst;
    public Text timeScore;
    private bool timerActive = true;
    private int maxScore = 10;

    private void Update()
    {
        //Displays the score
        countText.text = counter.ToString();
        //Counts the spawn time
        spawnTimer += Time.deltaTime;

        if (timerActive)
        {
            // Counts the score time
            scoreTimer += Time.deltaTime;      
        }
        //Rounds the seconds to two decimals after the comma
        scoreTimer = Mathf.Round(scoreTimer * 100f) / 100f;
        //Displays the time score
        timeScore.text = "Score: " +  scoreTimer.ToString() + " s";

        //After every two seconds a new target is being shown
        if (spawnTimer > 2)
        {
            ShowTarget();
        }

        //When the counter reaches the max Score the game is stopped
        if (counter >= maxScore)
        {
            //Displays the win text
            winText.SetActive(true);
            //The score timer is stopped
            timerActive = false;

            //The confetti appears for a certain amount of time
            if (spawnTimer < 6.5)
            {
                confettiBurst.SetActive(true);
            }
            else
            {
                confettiBurst.SetActive(false);
            }
        }
        //When the left mouse button is clicked a shooting sound plays
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.GetComponent<AudioSource>().Play(0);
        }
    }

    //Start sequence
    void Start()
    {
        //The canvas rect transfrom is saved here
        RectTransform canvasTransform = canvas.GetComponent<RectTransform>();

        //The canvas width, height, x and y are saved into variables
        canvasWidth = canvasTransform.rect.width;
        canvasHeight = canvasTransform.rect.height;
        canvasX = canvasTransform.rect.x;
        canvasY = canvasTransform.rect.y;

        //The original prefab target is deactivated
        originalTarget.SetActive(false);
        //First star target is shown
        ShowTarget();
    }

    //Method that makes the target appear
    public void ShowTarget()
    {
        //If the target counter is under max score, the game is not won, a new target is shown
        if (counter < maxScore)
        {
            //If the old target isn't destroyed by the mouse it gets destroyed
            Destroy(target);
            //Calculation of the position of the new target
            var xPos = Random.Range(margin, canvasWidth - margin);
            var yPos = Random.Range(margin, canvasHeight - margin);
            //Add the offset canvas X and Y to align the area where the stars are being spawned
            xPos = xPos + canvasX;
            yPos = yPos + canvasY;

            //New instance of the target is instanciated with freshly calculated position
            GameObject newTarget = Instantiate(originalTarget, canvas.transform);
            newTarget.GetComponent<Transform>().localPosition = new Vector2(xPos, yPos);

            //The new target has to be activated, the timer is reset to 0 for the next cycle
            target = newTarget;
            target.SetActive(true);
            spawnTimer = 0;
        }
    }
}





