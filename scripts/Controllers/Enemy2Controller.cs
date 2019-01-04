using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Controller : MonoBehaviour {

    public Player2Controller player;
    public JeffController jeff;
    public SteveController steve;
    public BobController bob;
    public Text enemyChanges;


    int level = 1;
    int hp = 100;
    int maxHP = 100;
    int strength = 1;
    int baseStrength = 1;
    int exp = 10;
    bool turn = false;
    int random;
    

    public event System.Action OnAttack;
    public event System.Action OnDragonAttack;
    public event System.Action OnDragonHeal;
    public event System.Action OnMultiAttack;



    private void Start()
    {
        if (PlayerPrefs.GetInt("EnemyLevel", 0) != 0)
        {
            level = PlayerPrefs.GetInt("EnemyLevel");
            hp = 100 * level;
            maxHP = 100 * level;
            strength = level+1;
            baseStrength = level+1;
            exp = 10 * level;
        }
        if (this.tag == "Dragon")
        {
            level = level+1;
            hp = 1000;
            maxHP = 1000;
            strength = level + 1;
            baseStrength = level + 1;
            exp = 100;
        }
        enemyChanges.text = "";
    }
    
    void Update () {
        if (turn && this.tag == "Enemy")
        {
            random = Random.Range(0, 10);
            if (random <= 5)
            {
                basicAttack();
            }
            if (random >= 6 && random <= 7)
            {
                raiseAttack();
            }
            if (random >= 8 && random <= 9)
            {
                lowerEnemyAttack();
            }
            turn = false;
            player.startTurn();
        }
        if (turn && this.tag == "Dragon")
        {
            random = Random.Range(0, 10);
            if (random <= 5)
            {
                basicAttackDragon();
            }
            if (random >= 6 && random <= 7)
            {
                singleTargetAttackDragon();
            }
            if (random >= 8 && random <= 9)
            {
                heal();
            }
            turn = false;
            player.startTurn();
        }
	}

    public void basicAttack()
    {
        if (OnAttack != null)
        {
            OnAttack();
        }
        random = Random.Range(0, 4);
        if (random == 0 || player.guard == true) {
            player.lowerHP(strength * 10);
            player.guard = false;
            enemyChanges.text = "MC took " + (strength * 10).ToString() + " damage\n";
        }
        else if (random == 1)
        {
            jeff.lowerHP(strength * 10);
            enemyChanges.text = "Jeff took " + (strength * 10).ToString() + " damage\n";
        }
        else if (random == 2)
        {
            steve.lowerHP(strength * 10);
            enemyChanges.text = "Steve took " + (strength * 10).ToString() + " damage\n";
        }
        else
        {
            bob.lowerHP(strength * 10);
            enemyChanges.text = "Bob took " + (strength * 10).ToString() + " damage\n";
        }
        turn = false;
    }

    public void raiseAttack()
    {
        strength += 1;
        player.lowerHP(10 + strength * 10);
        jeff.lowerHP(10 + strength * 10);
        steve.lowerHP(10 + strength * 10);
        bob.lowerHP(10 + strength * 10);
        enemyChanges.text = "Enemy Attack UP + all allies took " + (10 + 10*strength) + " damage";
        turn = false;
    }

    public void lowerAttack()
    {
        strength -= 1;
    }

    public void basicAttackDragon()
    {
        if (OnMultiAttack != null)
        {
            OnMultiAttack();
        }

        player.lowerHP(strength * 10);
        jeff.lowerHP(strength * 10);
        steve.lowerHP(strength * 10);
        bob.lowerHP(strength * 10);
        enemyChanges.text = "All allies took " + (10 * strength) + " damage";
    }

    public void singleTargetAttackDragon()
    {
        if (OnDragonAttack != null)
        {
            OnDragonAttack();
        }

        random = Random.Range(0, 4);
        if (random == 0 || player.guard == true)
        {
            player.lowerHP(strength * 10);
            player.guard = false;
            enemyChanges.text = "MC took " + (strength * 10 * 2).ToString() + " damage\n";
        }
        else if (random == 1)
        {
            jeff.lowerHP(strength * 10);
            enemyChanges.text = "Jeff took " + (strength * 10 * 2).ToString() + " damage\n";
        }
        else if (random == 2)
        {
            steve.lowerHP(strength * 10);
            enemyChanges.text = "Steve took " + (strength * 10 * 2).ToString() + " damage\n";
        }
        else
        {
            bob.lowerHP(strength * 10);
            enemyChanges.text = "Bob took " + (strength * 10 * 2).ToString() + " damage\n";
        }
        turn = false;
    }

    public void heal()
    {
        if (OnDragonHeal != null)
        {
            OnDragonHeal();
        }
        hp += 100;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        strength += 1;
        enemyChanges.text = "Dragon healed 100 health and raised attack";
    }

    public void lowerEnemyAttack()
    {
        player.lowerHP(10 + strength * 10);
        jeff.lowerHP(10 + strength * 10);
        steve.lowerHP(10 + strength * 10);
        bob.lowerHP(10 + strength * 10);
        player.lowerStrength();
        jeff.lowerStrength();
        steve.lowerStrength();
        bob.lowerStrength();
        enemyChanges.text = "All allies Attack Down + all allies took " + (10 + 10 * strength) + " damage";
        turn = false;
    }

    public void resetAttack()
    {
        strength = baseStrength;
    }

    public void lowerHP(int x)
    {
        
        hp = hp - x;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public void startTurn()
    {
        turn = true;
    }

    public int getHP()
    {
        return hp;
    }

    public int giveEXP()
    {
        return exp;
    }

    public void resetHP()
    {
        hp = 100;
    }
}
