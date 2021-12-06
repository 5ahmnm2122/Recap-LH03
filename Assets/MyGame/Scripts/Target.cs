using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

   
    private void OnMouseDown()
    {
        //Gain access to GameManager class
        GameObject gM = GameObject.Find("GameManager");
        GameManager gameManagerScript = gM.GetComponent<GameManager>();

        //Add +1 to the counter variable for every target that is clicked upon
        gameManagerScript.counter += 1;

       //After the target is clicked, destroy the target
        Destroy(this.gameObject);

        //Call upon the show target method to show new target after this one is destroyed
        gameManagerScript.ShowTarget();
    }

}
