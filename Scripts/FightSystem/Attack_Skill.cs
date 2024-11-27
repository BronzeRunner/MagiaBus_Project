using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;


public class Attack_Skill :EffectTriggerSystem
{
    public coin_type Type;


    /*public void GetValues()
    {

    }*/
    [Tooltip("해당공격 속도(일반적으로는 캐릭터속도 그대로)")]
    public float Attack_Speed; // 해당공격 속도(일반적으로는 캐릭터속도 그대로)
    [Tooltip("공격 가중치")]
    public float Attack_Weight; //공격 가중치
    [Tooltip("합 최소값(기본값)")]
    public float clash_minimum; //합 최소값
    //[FoldoutGroup("ChangeValue"),Tooltip("합 최소값변동치")] // 변경값 모음
    //public float clash_ChangeValue;//합 최소값변동치
    [FoldoutGroup("Coin_ori"), Tooltip("코인 기본값")]
    public float coin_Original; // 코인 기본값
    [FoldoutGroup("Coin_ori"), Tooltip("코인개수")]
    public int coin_Count; //코인개수
    //[FoldoutGroup("ChangeValue"), Tooltip("코인위력 증감")]
    //public float coin_changeValue; // 코인위력 증감
    [FoldoutGroup("Coin_ori"), Tooltip("최소코인위력 \n(뒷면 혹은 코인이 존재하나 코인트스미적용시에도 적용)")]
    public float coin_MinimamValue; // 최소코인위력
    public Dictionary<string, float> coin_ChangeValue = new Dictionary<string, float>(); //
    [SerializeField]
    public EffectTriggerSystem Skill_Effects = new EffectTriggerSystem();
    public Attack_Skill skill_test;
    public Dictionary<string, int> testD;

    [Button("new class Test")]
    public void test()
    {
            
        //Skill_Effects = AddComponentMenu;
    }

    public float GetCoinValue(bool coin)
    {
        if (coin)
        {

            return coin_MinimamValue + coin_Original;
        }
        else
        {
            return coin_MinimamValue;
        }

    }

    public void Coin_Break()
    {

    }
    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    public Character_Main Owner;
    // Start is called before the first frame update
    
}
