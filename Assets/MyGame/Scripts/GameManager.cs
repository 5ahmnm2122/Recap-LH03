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

    private void Update()
    {
        //Counts the stars that are being shot
        countText.text = counter.ToString();
        //Counts the spawn time
        spawnTimer += Time.deltaTime;

        // Counts the score time
        if (timerActive)
        {
            scoreTimer += Time.deltaTime;      
        }
        //Rounds the seconds um to two decimals after the comma
        scoreTimer = Mathf.Round(scoreTimer * 100f) / 100f;
        //Displays the time score
        timeScore.text = "Score: " +  scoreTimer.ToString() + " s";

        //After every two seconds a new target is being shown
        if (spawnTimer > 2)
        {
            ShowTarget();
        }

        //When the counter equals 10 the game is stopped
        if (counter >= 10)
        {
            // The win text appears
            winText.SetActive(true);
            //The score timer is stopped
            timerActive = false;

            // The confetti appears for 6.4 seconds
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
        RectTransform cansvasTransform = canvas.GetComponent<RectTransform>();

        //The canvas width, height, x and y are getting saved into variables
        canvasWidth = cansvasTransform.rect.width;
        canvasHeight = cansvasTransform.rect.height;
        canvasX = cansvasTransform.rect.x;
        canvasY = cansvasTransform.rect.y;

        //the original prefab target is deactivated
        originalTarget.SetActive(false);
        //First star target is being shown
        ShowTarget();
    }

    //Method that makes the target appear
    public void ShowTarget()
    {
        //If the target counter is under 10 and the game is not won a new target is shown
        if (counter < 10)
        {
            //If the old target isn't destroyed by the mouse click it gets destroyed
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

            //The new is not set and activated to be seen, the timer is reset to 0 for the enxt cycle
            target = newTarget;
            target.SetActive(true);
            spawnTimer = 0;
        }
    }
}





