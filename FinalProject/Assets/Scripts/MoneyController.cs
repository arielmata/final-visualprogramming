using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    public enum MoneyType
    {
        Gold,
        Gem,
        Bag
    }

    public MoneyType moneyType;
    public int goldScore;
    public int gemScore;
    public int bagScore;
    public AudioClip moneyClip;

    public TextMeshProUGUI scoreText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (moneyType == MoneyType.Gold)
            {
                scoreText.GetComponent<ScoreController>().score += goldScore;
                scoreText.GetComponent<ScoreController>().UpdateScore();
            }
            else if (moneyType == MoneyType.Gem)
            {
                scoreText.GetComponent<ScoreController>().score += gemScore;
                scoreText.GetComponent<ScoreController>().UpdateScore();
            } else if (moneyType == MoneyType.Bag)
            {
                scoreText.GetComponent<ScoreController>().score += bagScore;
                scoreText.GetComponent<ScoreController>().UpdateScore();
            }
            AudioSource.PlayClipAtPoint(moneyClip, transform.position);
            Destroy(this.gameObject);
        }
    }
}
