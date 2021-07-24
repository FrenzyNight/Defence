using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image profile;
    Player_char player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player_char>();
        profile.sprite = player.profile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
