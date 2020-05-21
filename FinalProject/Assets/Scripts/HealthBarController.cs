using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public PlayerMovement player;
    public Image m_image;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health >= 3)
        {
            Destroy(this.gameObject);
        }
        GetComponent<Image>().sprite = sprites[player.health];
    }
}
