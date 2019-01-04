using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
	// Use this for initialization
	public void Switch()
    {
        player1.SetActive(false);
        player2.SetActive(true);
    }

    public void Switch2()
    {
        player1.SetActive(true);
        player2.SetActive(false);
    }
}
