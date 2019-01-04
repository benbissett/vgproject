using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    public LayerMask movementMask;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject jeffCam;
    public GameObject steveCam;
    public GameObject canvas;
    public GameObject jeffCanvas;
    public GameObject steveCanvas;
    public GameObject bobCanvas;
    public GameObject bobCamera;
    Camera cam;
    PlayerMotor motor;
    public GameObject player1;
    public GameObject player2;
    public Player2Controller combat;
    public Button exit;
    

    // Use this for initialization
    void Start () {
        cam1.SetActive(true);
        cam2.SetActive(false);
        canvas.SetActive(false);
        jeffCanvas.SetActive(false);
        jeffCam.SetActive(false);
        steveCanvas.SetActive(false);
        steveCam.SetActive(false);
        bobCanvas.SetActive(false);
        bobCamera.SetActive(false);
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        exit.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
            }
        }
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            if (other.gameObject.GetComponent<EnemyController>().chasing == true)
            {
                Switch2();
            }
            else
            {
                Switch();
            }
        }

        if (other.gameObject.CompareTag("StageEnd"))
        {
            PlayerPrefs.SetInt("EnemyLevel", 2);
            PlayerPrefs.SetInt("Stage", 2);
            SceneManager.LoadScene("Level2");
        }

        if (other.gameObject.CompareTag("StageReturn"))
        {
            PlayerPrefs.SetInt("EnemyLevel", 1);
            PlayerPrefs.SetInt("Stage", 1);
            SceneManager.LoadScene("Main");
        }
    }

    public void Switch()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        player1.SetActive(false);
        combat.startTurn();
        canvas.SetActive(true);
    }

    public void Switch2()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        combat.enemy.startTurn();
        canvas.SetActive(true);
    }
}
