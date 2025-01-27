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
    //public int Identity_Level = 50;
    //public JsonDataReader ???
}

public class Skill_Don_LamanchLand_EnoughIsEnough : Attack_Skill
{

    /*
     이 스킬에서 적중 시, 입힌 체력 피해량의 30%만큼 체력 회복 (최대 10 회복) (EffectTrigger를 통해 입은 데미지 확인)
    [사용시] 혈찬을 최대 50 소모하여, 혈찬 5 당, 경혈 1 얻음 
            혈찬을 소모하지 못했으면, 출혈 5 얻음
    [사용시] 대상의 출혈 6 당, 코인 위력 +1 (최대 2)

    (코인1)[적중시] 출혈 2 부여
    (코인2)[적중시] 출혈 2 부여
     */
    [SerializeField]
    int Heal_Onhit_Cur;
    [SerializeField]
    int Heal_Onhit_Max = 30;
    
    /// <summary>
    /// 입힌체력 피해량
    /// </summary>
    /// <param name="i">스킬로 입힌 피해량</param>
    public void OnEnemyHit(ref int i)
    {
        if(i < 0)
        {

        }
    }
    public void OnUse(ref int i)
    {
        int Value = i / 5;
        if (Value >0) // 혈잔 소모시 경혈 Value 만큼 얻음
        {
            i -= Value * 5;
        }
        else // 현잔소모 실패시 출혈 5,0 얻음
        {

        }

        Character_Main Enemy;//BattleManager 에서 합진행상대 받아옴
        //if
    }
}

