using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;
    public GameObject player1;
    public GameObject player2;
    public GameObject ally;
    public GameObject canvas;
    public GameObject jeffCanvas;
    public GameObject jeffCam;
    public Enemy2Controller enemy;
    public JeffController jeff;
    public Button button;
    public Button ability1;
    public Button ability2;
    public Button ultimate;
    public Text myHP;
    public Text enemyHP;
    public Text winText;
    public Text levelU;
    public Text changes;

    int level = 1;
    int hp = 100;
    int maxHP = 100;
    int ap = 100;
    int maxAP = 100;
    int strength = 1;
    int baseStrength = 1;
    int maxStrength = 3;
    int exp = 0;
    int nextLevel = 10;
    bool turn = false;
    public bool guard = false;

    public event System.Action OnAttack;

    private void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        ability1.onClick.AddListener(A1OnClick);
        ability2.onClick.AddListener(A2OnClick);
        ultimate.onClick.AddListener(UltimateOnClick);
        if (PlayerPrefs.GetInt("Level", 0) != 0)
        {
            level = PlayerPrefs.GetInt("Level");
            hp = 100 + 10 * (level-1);
            maxHP = 100 + 10 * (level-1);
            ap = 100 + 10 * (level-1);
            maxAP = 100 + 10 * (level-1);
            strength = level;
            baseStrength = level;
            maxStrength = strength + 2;
        }

        if (PlayerPrefs.GetInt("Exp", 0) != 0)
        {
            exp = PlayerPrefs.GetInt("Exp");
        }

        if (PlayerPrefs.GetInt("NextLevel", 0) != 0)
        {
            nextLevel = PlayerPrefs.GetInt("NextLevel");
        }
        if (level < 3)
        {
            ability2.gameObject.SetActive(false);
            
        }
        if (level < 5)
        {
            ultimate.gameObject.SetActive(false);
        }
        winText.text = "";
        levelU.text = "";
    }

    void Update()
    {
        exp = PlayerPrefs.GetInt("Exp");
        myHP.text = "Level = " + level.ToString() + "\n" +
                    "Hp = " + hp.ToString() + "\n" +
                    "Ap = " + ap.ToString() + "\n" +
                    "Exp = " + exp.ToString() + " of " + nextLevel.ToString();
        enemyHP.text = "HP = " + enemy.getHP().ToString();
    }

    void TaskOnClick()
    {
        if (turn)
        {
            basicAttack();

            endCombat();
        }
    }

    void A1OnClick()
    {
        if (turn && ap >= 10)
        {
            a1Attack();
            ap = ap - 10;
            canvas.SetActive(false);
            jeffCanvas.SetActive(true);
            cam2.SetActive(false);
            jeffCam.SetActive(true);
            jeff.startTurn();
            changes.text = "Activated gaurd and lowered enemy attack";
        }
    }

    void A2OnClick()
    {
        if (turn && ap >= 20)
        {
            a2Attack();

            endCombat();
        }
    }

    void UltimateOnClick()
    {
        if (turn && ap >= 50)
        {
            ultimateAttack();
            a1Attack();

            endCombat();
        }
    }

    public void endCombat()
    {
        if (enemy.getHP() <= 0)
        {
            resetStrength();
            enemy.resetAttack();
            changes.text = "";
            myHP.text = "Hp = " + hp.ToString();
            enemyHP.text = "HP = " + enemy.getHP().ToString();
            exp = exp + enemy.giveEXP();
            PlayerPrefs.SetInt("Exp", exp);
            if (exp == nextLevel)
            {
                levelUP();
            }
            else
            {
                winText.text = "You Win!";
                if (enemy.tag == "Dragon")
                {
                    StartCoroutine("EndGame");
                }

                else
                {
                    StartCoroutine("Wait");
                }
            }

        }

        else
        {
            if (jeff.getHP() == 0)
            {
                if (jeff.player.getHP() == 0)
                {
                    if (jeff.player.player.getHP() == 0)
                    {
                        enemy.startTurn();
                    }
                    else
                    {
                        canvas.SetActive(false);
                        cam2.SetActive(false);
                        jeff.player.allyCam.SetActive(true);
                        jeff.player.allyCanvas.SetActive(true);
                        jeff.player.player.startTurn();
                    }
                }
                else
                {
                    canvas.SetActive(false);
                    cam2.SetActive(false);
                    jeff.allyCam.SetActive(true);
                    jeff.allyCanvas.SetActive(true);
                    jeff.player.startTurn();
                }
            }
            else
            {
                canvas.SetActive(false);
                cam2.SetActive(false);
                jeffCam.SetActive(true);
                jeffCanvas.SetActive(true);
                jeff.startTurn();
            }
        }
    }

    public void a1Attack()
    {
        guard = true;
        enemy.lowerAttack();
    }

    public void a2Attack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        changes.text = "MC did " + (2 * (strength * 10)).ToString() + " damage";
        enemy.lowerHP(2 * (strength * 10));
        ap = ap - 20;
    }

    public void ultimateAttack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        enemy.lowerHP(100 + (strength * 10));
        changes.text = "MC did " + (100 + (strength * 10)).ToString() + " damage and activated guard";
        ap = ap - 50;
    }

    public void Switch()
    {
        cam.SetActive(true);
        cam2.SetActive(false);
        canvas.SetActive(false);
        player1.SetActive(true);
    }

    public void levelUP()
    {
        level = level + 1;
        maxHP = maxHP + 10;
        hp = maxHP;
        maxAP += 10;
        ap = maxAP;
        strength += 1;
        baseStrength += 1;
        maxStrength += 1;
        exp = 0;
        nextLevel = nextLevel * 2;
        levelU.text = "Level = " + level.ToString() + "\n" +
                      "HP = " + maxHP.ToString() + "\n" +
                      "AP = " + maxAP.ToString() + "\n" +
                      "Strength = " + strength.ToString() + "\n";
        winText.text = "You Win!";
        PlayerPrefs.SetInt("Exp", exp);
        PlayerPrefs.SetInt("NextLevel", nextLevel);
        PlayerPrefs.SetInt("Level", level);
        if (level == 3)
        {
            ability2.gameObject.SetActive(true);
            levelU.text += "Gained new ability";
        }
        if (level == 5)
        {
            ultimate.gameObject.SetActive(true);
            levelU.text += "Gained ultimate ability";
        }
        if (jeff.getLevel() < level)
        {
            StartCoroutine("LevelWait");
        }
        else
        {
            StartCoroutine("Wait");
        }
    }

    public void basicAttack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        ap = ap + 5 + level;
        if (ap > maxAP)
        {
            ap = maxAP;
        }

        changes.text = "MC did " + (strength * 10).ToString() + " damage";
        enemy.lowerHP(strength * 10);
        turn = false;

    }

    public void lowerHP(int x)
    {
        hp = hp - x;
        if (hp <= 0)
        {
            die();
        }
    }

    public void startTurn()
    {
        turn = true;
    }

    public void resetHP()
    {
        hp = maxHP;
    }

    public int getHP()
    {
        return hp;
    }

    public int getLevel()
    {
        return level;
    }

    public void raiseHP(int amount)
    {
        hp = hp + amount;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    public void raiseStrength()
    {
        if (strength == maxStrength)
        {
            changes.text = "Already at Max Strength";
        }
        else {
            strength = strength + 1;
        }
        
    }

    public void lowerStrength()
    {
        if (strength != 1)
        {
            strength = strength - 1;
        }  
    }

    public void resetStrength()
    {
        strength = baseStrength;
        if (jeff.getStrength() != baseStrength)
        {
            jeff.resetStrength();
        }
    }

    public int getStrength()
    {
        return strength;
    }

    public void die()
    {
        StartCoroutine("Die");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        levelU.text = "";
        winText.text = "";
        enemy.resetHP();
        resetHP();
        Switch();
    }

    IEnumerator LevelWait()
    {
        yield return new WaitForSeconds(2.0f);
        levelU.text = "";
        winText.text = "";
        resetHP();
        canvas.SetActive(false);
        jeffCanvas.SetActive(true);
        cam2.SetActive(false);
        jeffCam.SetActive(true);
        jeff.levelUP();
    }

    IEnumerator Die()
    {
        winText.text = "YOU LOSE";
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator EndGame()
    {
        winText.text = "Congragulations You Beat The Game";
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
    }

}

