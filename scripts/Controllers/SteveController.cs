using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SteveController : MonoBehaviour {

    public GameObject cam;
    public GameObject cam2;
    public GameObject player1;
    public GameObject player2;
    public GameObject canvas;
    public GameObject ally;
    public GameObject allyCanvas;
    public GameObject allyCam;
    public Enemy2Controller enemy;
    public BobController player;
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
    int maxStrength = 3;
    int baseStrength = 1;
    int exp = 0;
    int nextLevel = 10;
    bool turn = false;


    public event System.Action OnAttack;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Level", 0) != 0)
        {
            level = PlayerPrefs.GetInt("Level");
            hp = 100 + 10 * (level - 1);
            maxHP = 100 + 10 * (level - 1);
            ap = 100 + 10 * (level - 1);
            maxAP = 100 + 10 * (level - 1);
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
        button.onClick.AddListener(TaskOnClick);
        ability1.onClick.AddListener(A1OnClick);
        ability2.onClick.AddListener(A2OnClick);
        ultimate.onClick.AddListener(UltimateOnClick);
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
            changes.text = "Steve did " + (strength * 10).ToString() + " damage";
            endCombat();
        }
    }

    void A1OnClick()
    {
        if (turn && ap >= 20)
        {
            a1Attack();
            changes.text = "Steve did " + (2 * strength * 10).ToString() + " damage";

            endCombat();
        }
    }

    void A2OnClick()
    {
        if (turn && ap >= 30)
        {
            a2Attack();
            changes.text = "Steve did " + (30 + strength * 10).ToString() + "damage";
            endCombat();
        }
    }

    void UltimateOnClick()
    {
        if (turn && ap >= 50)
        {
            ultimateAttack();
            changes.text = "Steve did " + (150 + strength * 10).ToString() + " damage";
            endCombat();
        }
    }

    public void ultimateAttack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        enemy.lowerHP(150 + 10 * strength);
        ap = ap - 50;
        turn = false;
    }

    public void a2Attack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        enemy.lowerHP(30 + 10 * strength);
        ap = ap - 30;
    }

    public void a1Attack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        enemy.lowerHP(2 * (10 * strength));
        ap = ap - 20;
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
            if (player.getHP() == 0)
            {
                canvas.SetActive(false);
                cam2.SetActive(false);
                player.allyCam.SetActive(true);
                player.allyCanvas.SetActive(true);
                enemy.startTurn();
            }
            else
            {
                canvas.SetActive(false);
                allyCanvas.SetActive(true);
                cam2.SetActive(false);
                allyCam.SetActive(true);
                player.startTurn();
            }
        }
    }

    public void Switch()
    {
        player1.SetActive(true);
        cam.SetActive(true);
        cam2.SetActive(false);
        canvas.SetActive(false);
        player1.SetActive(true);
    }

    public void levelUP()
    {
        level = level + 1;
        maxHP = maxHP + 10;
        maxAP = maxAP + 10;
        hp = maxHP;
        ap = maxAP;
        strength += 1;
        baseStrength += 1;
        maxStrength += 1;
        exp = 0;
        nextLevel = nextLevel * 2;
        levelU.text = "Level = " + level.ToString() + "\n" +
                      "HP = " + maxHP.ToString() + "\n" +
                      "AP = " + maxAP.ToString() + "\n" +
                      "Dexterity = " + strength.ToString() + "\n";
        winText.text = "You Win!";
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
        if (player.getLevel() < level)
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
        enemy.lowerHP(strength * 10);
        turn = false;

    }

    public void lowerHP(int x)
    {
        hp = hp - x;
    }

    public void startTurn()
    {
        turn = true;
    }

    public void resetHP()
    {
        hp = maxHP;
    }

    public int getLevel()
    {
        return level;
    }

    public int getHP()
    {
        return hp;
    }

    public void raiseStrength()
    {
        if (strength == maxStrength)
        {
            changes.text = "Already at Max Strength";
        }
        else
        {
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

    public int getStrength()
    {
        return strength;
    }

    public void resetStrength()
    {
        strength = baseStrength;
        if (player.getStrength() != baseStrength)
        {
            player.resetStrength();
        } 
    }

    public void raiseHP(int amount)
    {
        hp = hp + amount;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
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
        allyCanvas.SetActive(true);
        cam2.SetActive(false);
        allyCam.SetActive(true);
        player.levelUP();
    }

    IEnumerator EndGame()
    {
        winText.text =  "You Beat The Game!";
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
