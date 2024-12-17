using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public enum ComparisonResult { Win, Same, Lose }
public enum CoinType { Normal, UnBreakable, Rev_Atk/*,Rev_Eva*/ }
public enum CoinTosType { Clash, Attack }
/// <summary>
/// Wrath : 분노 ,
///Lust : 색욕 ,
/// Sloth : 나태 ,
/// Gluttony : 탐식 ,
/// Gloom : 우울 ,
/// Pride : 오만 ,
/// Envy  :질투,
/// </summary>
public enum SinType {Envy,Gloom,Gluttony,Lust,Pride,Sloth,Wrath } //Wrath : 분노 , Lust : 색욕 , Sloth : 나태 , Gluttony : 탐식 ,Gloom : 우울 , Pride : 오만 , Envy  :질투
/// <summary>
/// Slash : 참격 ,
/// Pierce 관통 ,
/// Blunt 타격
/// </summary>
public enum AttackType {Slash,Pierce,Blunt } // Slash : 참격 , Pierce 관통 , Blunt 타격

public class Attack_Skill :EffectTriggerSystem
{
    public Character_Main Owner;
    SinType Skill_AttributeType;
    public int Attack_SkillCount;// 스킬 갯수
    
    [SerializeField]
    public Attack_Values Attack_OriValues;
    [Tooltip("해당공격 속도(일반적으로는 캐릭터속도 그대로)")]
    public int Attack_Speed; // 해당공격 속도(일반적으로는 캐릭터속도 그대로)
    [Tooltip("공격 가중치")]
    public int Attack_Weight; //공격 가중치
    [Tooltip("합 최소값(기본값)")]
    public int clash_NormalValue; //합 최소값(기본값)
    public int clash_CurValue;
    public int Attack_CurValue;
    [FoldoutGroup("Coin"), Tooltip("코인 기본값")]
    public int Coin_Value; // 코인 기본값
    [FoldoutGroup("Coin"), Tooltip("최소코인위력 \n(뒷면 혹은 코인이 존재하나 코인토스미적용시 존재할경우(파불코)에도 적용)")]
    public int Coin_MinimamValue; // 최소코인위력
    [FoldoutGroup("Coin"),Tooltip("기본 코인갯수")]
    public int Coin_Count; //코인갯수(기본)
    [FoldoutGroup("Coin"), Tooltip("합진행중 코인갯수")]
    public int Coin_CurCount; // 코인갯수(합진행중)
    public List<CoinType> Coin_CoinTypes;
    public int GetCoinValue(bool coin)
    {
        
        if (coin)
        {
            return Coin_MinimamValue + Coin_Value;
        }
        else
        {
            return Coin_MinimamValue;
        }
        
    }
    public void Coin_Lose(int i)
    {
        while(i > 0)
        {
            Coin_Break();
            i--;
            
        }
    }

    public void Coin_Break()
    {
        Coin_CurCount--;
    }
    /*
    public int Atk_Multiplier;
    public float Atk_Level;
    public float Atk_LevelPlus;
    */

    public int CoinToss_Clash(int coin_Count)
    {
        return CoinToss(coin_Count, CoinTosType.Clash);
    }

    public int CoinToss_Attack(int coin_Count)
    {
        return CoinToss(coin_Count, CoinTosType.Attack);
    }
    private int CoinToss(int coin_Count, CoinTosType TosType)
    {
        int result = 0;
        if (Owner.CoinCalculate())
        {
            result += Coin_Value;
            //코인 앞면
        }
        else
        {

            //코인 뒷면
        }
        return result;
    }
    // Start is called before the first frame update

    public virtual void Skill_Attack(ComparisonResult clash_r, Character_Main Target)
    {
        //값변경하는 함수 만들때 ref값으로가져가서 변경
        clash_CurValue = clash_NormalValue;
        for (int i = 0; i < (Coin_Count > Coin_CurCount ? Coin_Count : Coin_CurCount); i++)
        {
            switch (Coin_CoinTypes[i])
            {
                case CoinType.Normal:
                    {
                        if (Coin_CurCount < i)
                        {
                            Attack_CurValue += CoinToss_Attack(i);
                        }

                        break;
                    }
                case CoinType.UnBreakable:
                    {
                        Attack_CurValue += CoinToss_Attack(i);
                        Target.Damage_Get(Attack_CurValue);
                        break;
                    }
                case CoinType.Rev_Atk:
                    {
                        if (clash_r == ComparisonResult.Lose)
                        {
                            Attack_CurValue += CoinToss_Attack(i);
                        }
                        break;
                    }
            }

            Attack_CurValue += CoinToss_Attack(i);
            Target.Damage_Get(Attack_CurValue);
        }


    }



}




