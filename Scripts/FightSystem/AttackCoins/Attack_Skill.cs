using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
/// <summary>
/// Attack : 공격 ,
/// Evade : 회피 ,
/// Counter 반격 ,
/// Deffend : 방어 ,
/// Counter_p : 강화반격(합가능반격) ,
/// Deffend_p : 강화방어(합가능 방어) 
/// </summary>
public enum SkillType {Attack, Evade, Counter,Deffend,Counter_p,Deffend_p}
public enum ComparisonResult { Win, Same, Lose }
[Tooltip("Normal : 일반코인 \n UnBreakable : 파괴불가코인 \n Rev_Atk : 반격코인(스킬X)")]
public enum CoinType { Normal, UnBreakable, Rev_Atk/*,Rev_Eva*/ }
public enum CoinTosType { Clash, Attack }
/// < summary >
/// Wrath : 분노 ,
///Lust : 색욕 ,
/// Sloth : 나태 ,
/// Gluttony : 탐식 ,
/// Gloom : 우울 ,
/// Pride : 오만 ,
/// Envy  :질투,
/// </summary>
public enum SinType {N,Envy,Gloom,Gluttony,Lust,Pride,Sloth,Wrath } //Wrath : 분노 , Lust : 색욕 , Sloth : 나태 , Gluttony : 탐식 ,Gloom : 우울 , Pride : 오만 , Envy  :질투
/// <summary>
/// Slash : 참격 ,
/// Pierce 관통 ,
/// Blunt 타격
/// </summary>
public enum AttackType {N,Slash,Pierce,Blunt } // Slash : 참격 , Pierce 관통 , Blunt 타격

//스킬 만들떄 해당 코드 상속시킬것
public class Attack_Skill :EffectTriggerSystem
{
    public string skill_Name;
    public Character_Main Owner;
    public Attack_Values attack_OriValues;
    [FoldoutGroup("Skill"),Tooltip("Attack : 공격\n Evade : 회피\n Counter 반격\n Deffend : 방어\n Counter_p : 강화반격(합가능반격)\n Deffend_p : 강화방어(합가능 방어) ")]
    public SkillType skill_Type;
    [FoldoutGroup("Skill"),Tooltip("Wrath : 분노 \n Lust : 색욕 \n Sloth : 나태 \n Gluttony : 탐식 \n Gloom : 우울 \n Pride : 오만 \n Envy  :질투")]
    public SinType skill_SinType;
    [FoldoutGroup("Skill"), Tooltip("Slash : 참격 \n Pierce 관통 \n Blunt 타격")]
    public AttackType skill_AttackType;
    [FoldoutGroup("Skill"), Tooltip("스킬 기본값 (공렙 방렙 미포함 기본게임에는 없음)")]
    public int skill_Value;
    [FoldoutGroup("Skill"), Tooltip("공격스킬일경우 공격레벨\n수비스킬일경우 방어레벨\n(레벨 미포함)")]
    public int skill_Level;
    [FoldoutGroup("Skill")]
    public int skill_Count;// 스킬 갯수
    public int Skill_CurLevel()
    {
        int curSkillLevel = (Owner == null? 0: Owner.Identity_Level) + skill_Level;
        if(skill_AttackType == AttackType.N)
        {
            //방렙
            Call_EffectTrigger("GetDeffendLevel", ref curSkillLevel);
        }
        else
        {
            //공렙
            Call_EffectTrigger("GetAttackLevel", ref curSkillLevel);
        }
        return curSkillLevel;
    } //현재 공렙/방렙

    
    [Tooltip("해당공격 속도(일반적으로는 캐릭터속도 그대로)")]
    public int attack_Speed; // 해당공격 속도(일반적으로는 캐릭터속도 그대로)
    [Tooltip("공격 가중치")]
    public int attack_Weight; //공격 가중치
    
    [FoldoutGroup("Skill/Coin"), Tooltip("코인 값")]
    public int coin_Value; // 코인 기본값
    [FoldoutGroup("Skill/Coin"),Tooltip("기본 코인갯수")]
    public int coin_Count; //코인갯수(기본)
    [FoldoutGroup("Skill/Coin")]
    public List<CoinType> Coin_CoinTypes;

    [FoldoutGroup("Skill/Dynamic"), Tooltip("합 위력값(공렙방렙 계산)")]
    public int skill_CurValue; //합 최소값(기본값)
    [FoldoutGroup("Skill/Dynamic"), Tooltip("합진행중 코인갯수")]
    public int coin_CurCount; // 코인갯수(합진행중)
    [FoldoutGroup("Skill/Dynamic"), Tooltip("합진행중 공격 가중치")]
    public int attack_CurWeight; //공격 가중치(합진행중)


    public Attack_Skill() //생성자
    {
        //this.Owner = Owner;
        Skill_Setting();
    }
    [FoldoutGroup("Skill"),Button("Skill_Setting")]
    public virtual void Skill_Setting()
    {
        skill_SinType = attack_OriValues.Skill_SinType;
        skill_AttackType = attack_OriValues.Skill_AttackType;
        skill_Count = attack_OriValues.Skill_Count;
        attack_Weight = attack_OriValues.Attack_Weight;
        // 캐릭터 스킬 만들시 동기화별 추가
    }

    public void Skill_Reset()
    {
        //scriptable object 완성시 변경예정
        coin_CurCount = coin_Count;
        skill_CurValue = skill_Value + Skill_CurLevel() / 3;
    }
    
    public int GetCoinValue(bool coin)
    {
        if (coin)
        {
            return coin_Value;
        }
        else
        {
            return 0;
        }
        
    }

    public void Clash_Lose()
    {
        Call_EffectTrigger("Clash_Lose");
        Owner.Call_EffectTrigger("Clash_Lose");
        Coin_Lose(1);
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
        coin_CurCount--;
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
        Call_EffectTrigger("Coin_Toss");
        Call_EffectTrigger($"Coin_Toss_{coin_Count}");
        int result = 0;
        if (Owner.CoinCalculate())
        {
            Call_EffectTrigger($"Coin_Front_{coin_Count}",ref result);
            Call_EffectTrigger("Coin_Front",ref result);
            result += coin_Value;
            //코인 앞면

        }
        else
        {
            Call_EffectTrigger($"Coin_Back_{coin_Count}",ref result);
            Call_EffectTrigger("Coin_Back",ref result);
            //코인 뒷면
        }
        return result;
    }
    // Start is called before the first frame update

    public virtual void Skill_Attack(ComparisonResult clash_r, Character_Main Target)
    {
        //값변경하는 함수 만들때 ref값으로가져가서 변경
        skill_CurValue = skill_Value;
        for (int i = 0; i < (coin_Count > coin_CurCount ? coin_Count : coin_CurCount); i++)
        {
            switch (Coin_CoinTypes[i])
            {
                case CoinType.Normal:
                    {
                        if (coin_CurCount < i)
                        {
                            skill_CurValue += CoinToss_Attack(i);
                        }

                        break;
                    }
                case CoinType.UnBreakable:
                    {
                        skill_CurValue += CoinToss_Attack(i);
                        Target.Hp_GetDamage(skill_CurValue,skill_SinType,skill_AttackType,skill_Name);
                        break;
                    }
                case CoinType.Rev_Atk:
                    {
                        if (clash_r == ComparisonResult.Lose)
                        {
                            skill_CurValue += CoinToss_Attack(i);
                        }
                        break;
                    }
            }

            skill_CurValue += CoinToss_Attack(i);
            Target.Hp_GetDamage(skill_CurValue, skill_SinType, skill_AttackType, skill_Name);
        }


    }



}




