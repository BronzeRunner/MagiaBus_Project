using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using Sirenix.Serialization;


public class Don_LamanchaLand : Character_Main
{
    public string Identity_Name;
    [Range(1, 3), MinValue(1), MaxValue(3)]
    public int Identity_Grade = 3;
    public int Identity_Level = 50;
    //public JsonDataReader ???
}

public class Skill_Don_LamanchLand_EnoughIsEnough : Attack_Skill
{

    int Heal_Onhit_Cur;
    int Heal_Onhit_Max = 30;
    
    public void OnHitWithSkill(int i)
    {
        if (i <= 0)
            return;
        int Damage = i;

        Damage = Damage / 10 * 3;
        if(Heal_Onhit_Cur + Damage > Heal_Onhit_Max)
        {
            Damage = Heal_Onhit_Max - Heal_Onhit_Cur;
        }
        Owner.Hp_GetDamage(-i);
    }
    public void OnUse(ref int i)
    {
        int Value = i / 5;
        if (Value >0) // 혈잔 소모시 경혈 Value 만큼 얻음
        {
            
        }
        else // 현잔소모 실패시 출혈 5,0 얻음
        {

        }

        Character_Main Enemy;//BattleManager 에서 합진행상대 받아옴
        //if
    }
}

