using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP = 100;
    public int CurrentHP;
    public RectTransform healthBar;

    void Start()
    {
        CurrentHP = HP;
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;
        //healthBar.sizeDelta = new Vector2(CurrentHP, healthBar.sizeDelta.y);
        Debug.Log(CurrentHP);


    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
