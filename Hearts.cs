using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public GameObject heart0;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        if(playerMovement.currentHealth == 5)
        {
            heart0.GetComponent<Image>().sprite = fullHeart;
            heart1.GetComponent<Image>().sprite = fullHeart;
            heart2.GetComponent<Image>().sprite = fullHeart;
            heart3.GetComponent<Image>().sprite = fullHeart;
            heart4.GetComponent<Image>().sprite = fullHeart;
        }
        if (playerMovement.currentHealth == 4)
        {
            heart0.GetComponent<Image>().sprite = fullHeart;
            heart1.GetComponent<Image>().sprite = fullHeart;
            heart2.GetComponent<Image>().sprite = fullHeart;
            heart3.GetComponent<Image>().sprite = fullHeart;
            heart4.GetComponent<Image>().sprite = emptyHeart;
        }
        if (playerMovement.currentHealth == 3)
        {
            heart0.GetComponent<Image>().sprite = fullHeart;
            heart1.GetComponent<Image>().sprite = fullHeart;
            heart2.GetComponent<Image>().sprite = fullHeart;
            heart3.GetComponent<Image>().sprite = emptyHeart;
            heart4.GetComponent<Image>().sprite = emptyHeart;
        }
        if (playerMovement.currentHealth == 2)
        {
            heart0.GetComponent<Image>().sprite = fullHeart;
            heart1.GetComponent<Image>().sprite = fullHeart;
            heart2.GetComponent<Image>().sprite = emptyHeart;
            heart3.GetComponent<Image>().sprite = emptyHeart;
            heart4.GetComponent<Image>().sprite = emptyHeart;
        }
        if (playerMovement.currentHealth == 1)
        {
            heart0.GetComponent<Image>().sprite = fullHeart;
            heart1.GetComponent<Image>().sprite = emptyHeart;
            heart2.GetComponent<Image>().sprite = emptyHeart;
            heart3.GetComponent<Image>().sprite = emptyHeart;
            heart4.GetComponent<Image>().sprite = emptyHeart;
        }
        if (playerMovement.currentHealth == 0)
        {
            heart0.GetComponent<Image>().sprite = emptyHeart;
            heart1.GetComponent<Image>().sprite = emptyHeart;
            heart2.GetComponent<Image>().sprite = emptyHeart;
            heart3.GetComponent<Image>().sprite = emptyHeart;
            heart4.GetComponent<Image>().sprite = emptyHeart;
        }
    }
}
