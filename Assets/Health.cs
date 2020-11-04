using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP = 100;
    public int CurrentHP;
    public RectTransform healthBar;
    public bool hit = false;
    public Enemy enemy;
    public Player Player;
    void Start()
    {
        CurrentHP = HP;
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        healthBar.sizeDelta = new Vector2(CurrentHP, healthBar.sizeDelta.y);

    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentHP == 0)
        {
            enemy.defeated = true;
        }
    }
}
