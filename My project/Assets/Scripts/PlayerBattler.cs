using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    // Start is called before the first frame update

    public string Name;
    public int MaxHp;
    public int CurrentHP;
    public float Speed;
    public int Defense;
    public int AttackPower;

    public void innit(string n, int MHp, int CHP, float Spd, int Def, int AP) 
    {
        Name = n;
        MaxHp = MHp;
        CurrentHP = CHP;
        Speed = Spd;
        Defense = Def;
        AttackPower = AP;
    }

    public bool TakeDamage(int damage){
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            return true;
        }
        else
        {
            return false;    
        }
    }
}
