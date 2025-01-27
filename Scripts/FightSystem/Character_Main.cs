using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;


public interface IChatacter_Main { }
public class Character_Main : EffectTriggerSystem, IChatacter_Main
{
    [SerializeField]
    Character_Stats Main_Data_Stat;
    [Button("Character_Setting"),Tooltip("!주의 Main_Data_Stat 의 값으로 덮어쒸움니다.")]
    public virtual void Character_Setting()
    {
        
        Identity_Level = 50;
        
        // 동기화 상태 추후 json으로 수정 + 인격관련 코드에 추가
        SpeedMin = Main_Data_Stat.SpeedMin;
        SpeedMax = Main_Data_Stat.SpeedMax;
        Hp_Min = Main_Data_Stat.Hp_Min;
        Hp_Max = Main_Data_Stat.Hp_Max + Identity_Level * Main_Data_Stat.Hp_PerLevel; //레벨당 Hp최대값 증가 추가
        Hp_Cur = Hp_Max /100 *Main_Data_Stat.Hp_StartPer;
        Resistance_AttackType =  Main_Data_Stat.Resistance_AttackType; //공격속성 설정
        Resistance_SinType =  Main_Data_Stat.Resistance_SinType; //죄악속성 설정
    }
    [FoldoutGroup("MainData")]
    #region Level
    public int Identity_Level;
    public int Identity_Rarity;
    //public int 
    #endregion 
    
    #region Mental
    [SerializeField]
    [FoldoutGroup("MainData/Mental"),]
    public int Mental_Cur = 0; // min -45 max 45
    [HorizontalGroup()]
    [FoldoutGroup("MainData/Mental")]
    public int Mental_Max = 45; // 정신력 최대값
    [FoldoutGroup("MainData/Mental")]
    public int Mental_Min = -45; // 정신력 최소값
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MaxPer; //정신력 최대값 앞면 확률
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MinPer; //정신력 최소값 앞면 확률
    public virtual void MentalValueSet(int value)
    {
        Mental_Cur = value;
    }

    public virtual void MentalValueChange(int value)
    {
        Mental_Cur += value;
    }

    public virtual void MentalOverCheck()
    {
        if (Mental_Cur < Mental_Min)
        {
            Mental_Cur = Mental_Min;
        }
        else if (Mental_Cur > Mental_Max)
        {
            Mental_Cur = Mental_Max;
        }
    }
    public virtual void MentalMinMaxCheck()
    {
        if (Mental_Max < Mental_Min)
        {
            float i = Mental_Min;
            Mental_Min = Mental_Max;
            Mental_Max = Mental_Min;
        }
        {
            /*if(Mental > (Mental_Max > Mental_Min ? Mental_Max : Mental_Min))
            {
                Mental = Mental_Max > Mental_Min ? Mental_Max : Mental_Min;
            }
            else if(Mental < (Mental_Max > Mental_Min ? Mental_Min : Mental_Max))
            {
                Mental = Mental_Max > Mental_Min ? Mental_Min : Mental_Max;
            }*/
        }
        if (Mental_Cur > Mental_Max)
        {
            Mental_Cur = Mental_Max;
        }
        else if (Mental_Cur < Mental_Min)
        {
            Mental_Cur = Mental_Min;
        }
    }
    public virtual bool CoinCalculate()
    {
        int percentage = 50 + Mental_Cur;
        System.Random rnd = new System.Random();
        if (rnd.Next(1, 101) <= percentage)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    [Button("CoinPercentage Test"), FoldoutGroup("MainData/Mental")]
    public void GetCoinPercentage_test()
    {
        Debug.Log($"Cur Percentage = {Mental_Cur + 50},CurCoin state = {CoinCalculate()}");
    }
    #endregion
    #region speed
    [FoldoutGroup("MainData/Speed")]
    public int SpeedMin; // 속도 최소값
    public int Speed_GetMin()
    {
        int Value = SpeedMin;
        Call_EffectTrigger("Speed_GetMin", ref Value);
        return Value;
    }
    [FoldoutGroup("MainData/Speed")]
    public int SpeedMax; //속도 최대값
    public int Speed_GetMax()
    {
        int Value = SpeedMax;
        Call_EffectTrigger("Speed_GetMax", ref Value);
        return Value;
    }
    //[FoldoutGroup("Speed")]
    //public Dictionary<string, int> Speed_Changes = new Dictionary<string, int>(); // Plus_[] ([]값 + 값)  , Multi_[] ([]값 * 값)
    [FoldoutGroup("MainData/Speed")]
    public int Speed_Cur; // 속도 현재결과값
    public int Speed_Set()// 100 = max
    {
        int min = Speed_GetMin();
        int max = Speed_GetMax();
        if (min < max)
            Speed_Cur = UnityEngine.Random.Range(min, max + 1);
        else
            Speed_Cur = min;
        Call_EffectTrigger("Speed_GetCur",ref Speed_Cur);
        return Speed_Cur;
    }
    public void Speed_Set(int i)
    {
        Speed_Cur = i;
    }
    #endregion
    #region Hp
    public void Hp_Setting()
    {
        Hp_Min = Main_Data_Stat.Hp_Min;
        Hp_Max = Main_Data_Stat.Hp_Max + Main_Data_Stat.Hp_PerLevel * Identity_Level;
        Hp_Cur = Hp_Max;
    }
    [FoldoutGroup("MainData/Hp")]
    public float Hp_Max;
    [FoldoutGroup("MainData/Hp")]
    public float Hp_Min;
    [FoldoutGroup("MainData/Hp")]
    public float Hp_Cur;
    public void Hp_CurChange(int changeValue,string reason)
    {
        Call_EffectTrigger("Hp_Change", ref changeValue);
        Call_EffectTrigger($"{reason}_Hp_Change", ref changeValue);
        Hp_Cur += changeValue;
    }
    public void Hp_CurChange(int changeValue)
    {
        Call_EffectTrigger("Hp_Change", ref changeValue);
        Hp_Cur += changeValue;
    }
    /// <summary>
    /// 죄악속성과 공격속성의 영향을 받습니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    /// <param name="sintype">죄악속성</param>
    /// <param name="attacktype">공격속성</param>
    public void Hp_GetDamage(int Value,SinType sintype,AttackType attacktype,string Reason)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Resistance_Calculate(Value, sintype,attacktype));
        
        Hp_CurChange(Value);
    }
    /// <summary>
    /// 공격속성의 영향만 받습니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    /// <param name="attacktype">공격속성</param>
    public void Hp_GetDamage(int Value, AttackType attacktype, string reason)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Resistance_Calculate(Value, attacktype));
        Hp_CurChange(Value , reason);
    }
    /// <summary>
    /// 죄악속성의 영향만 받습니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    /// <param name="sintype">죄악속성</param>
    public void Hp_GetDamage(int Value, SinType sintype, string reason)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Resistance_Calculate(Value,sintype));
        Hp_CurChange(Value, reason);
    }
    /// <summary>
    /// 죄악속성과 공격속성의 영향을 받지않고 고정피해를 입힙니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    public void Hp_GetDamage(int Value, string reason)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Hp_CurChange(Value, reason);
    }

    /*
    //[FoldoutGroup("Hp")]
    //public Dictionary<string, UnityEvent<int>> Hp_IntEvents;
    //public void Hp_EventSetting()
    //{

    //}
    
    
    //public Dictionary<TriggerType, Action> CharacterTriggers; // 현재 발동될 이벤트 <효과다루는 코드 커맨드 시스템 느낌으로 재작성>
    */
    #region barrier
    public float Barrier_Cur;
    public void Barrier_Check(int Value,string reason)
    {
        if (Barrier_Cur == 0)
        {
            Hp_CurChange(-Value, reason);
            return;
        }
        else
        { 
            Barrier_GetDamage(Value, reason);
        }

    }

    public void Barrier_GetDamage(int Value,string reason)
    {
        float Hp_Value = Math.Clamp(Value - Barrier_Cur, 0, Value);
        int Barrier_Damage = Value -(int)Hp_Value;
        Call_EffectTrigger("Barrier_GetDamage", ref Barrier_Damage);
        Barrier_CurChange(-Barrier_Damage,reason);
        if (Hp_Value >0)
        {
            Hp_CurChange(-(int)Hp_Value, reason);
        }
    }

    public void Barrier_CurChange(int value ,string reason)
    {
        Call_EffectTrigger("Barrier_CurChange", ref value);
        Call_EffectTrigger($"{reason}_Barrier_CurChange",ref value);
        Barrier_Cur += value;
    }
    #endregion
    #endregion

    #region state

    public void State_Setting()
    {
        foreach(int percent in Main_Data_Stat.Struggle_Line)
        {
            CurState_StruggleLine.Add((int)(Hp_Max / 100 * percent));
        }
        CurState = State_Enum.Alive;
    }
    public enum State_Enum
    {
        Alive = 1, Stagger = 2, Stagger_P = 3, Stagger_PP = 4, Dead = 0
    }
    [FoldoutGroup("MainData/State")]
    public State_Enum CurState = State_Enum.Alive;
    [FoldoutGroup("MainData/State")]
    public bool State_StruggleAvailable;
    [FoldoutGroup("MainData/State")]
    public List<int> CurState_StruggleLine = new List<int>();
    
    public void State_Check()
    {
        int stack = 0;
        foreach(int percentage in CurState_StruggleLine)
        {
            if (Hp_Cur < Hp_Max / 100 * percentage)
            {
                stack++;
            }
            else break;
        }
        for(int i = 0; i<stack;i++)
        {
            if(CurState_StruggleLine.Count >0)
            {
                CurState_StruggleLine.Remove(i);
                //CurState_StruggleLine.Sort();
            }
        }
    }
    public void State_Change(State_Enum state)
    {
        CurState = state;
    }
    #endregion
    #region Resistance
    [FoldoutGroup("MainData/Resistance")]
    Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // 죄악속성 저항이 1이 아닌경우만 변경혹은추가
    [FoldoutGroup("MainData/Resistance")]
    Dictionary<AttackType, float> Resistance_AttackType = new Dictionary<AttackType, float>();
    /// <summary>
    /// 공격위력으로 입는 피해량을 계산합니다.
    /// </summary>
    /// <param name="value">받는 공격 위력</param>
    /// <param name="sinType">피해량 계산에 영향을 주는 죄악속성</param>
    /// <returns></returns>
    public float Resistance_Calculate(float value,SinType sinType)
    {
        return value * Resistance_Get(sinType);
    }
    /// <summary>
    /// 공격위력으로 입는 피해량을 계산합니다.
    /// </summary>
    /// <param name="value">받는 공격 위력</param>
    /// <param name="sinType">피해량 계산에 영향을 주는 죄악속성</param>
    /// <param name="attackType">피해량 계산에 영향을 주는 공격속성</param>
    /// <returns></returns>
    public float Resistance_Calculate(float value, SinType sinType,AttackType attackType)
    {
        return value * Resistance_Get(sinType) * Resistance_Get(attackType);
    }
    /// <summary>
    /// 공격위력으로 입는 피해량을 계산합니다.
    /// </summary>
    /// <param name="value">받는 공격 위력</param>
    /// <param name="attackType">피해량 계산에 영향을 주는 공격속성</param>
    /// <returns></returns>
    public float Resistance_Calculate(float value, AttackType attackType)
    {
        return value * Resistance_Get(attackType);
    }


    public float Resistance_Get(SinType type)
    {
        float result = 1;
        Resistance_SinType.TryGetValue(type, out result);
        return result; 
    }
    public float Resistance_Get(AttackType type)
    {
        float result = 1;
        Resistance_AttackType.TryGetValue(type, out result);
        return result;
    }

    public void Resistance_Change(SinType type,float value)
    {
        if(Resistance_SinType.ContainsKey(type))
        {
            Resistance_SinType[type] = value;
        }
        else
        {
            Resistance_SinType.Add(type, value);
        } 
    }
    public void Resistance_Change(Dictionary<SinType, float> Resistance_SinType)
    {
        this.Resistance_SinType= Resistance_SinType;
    }

    public void Resistance_Change(AttackType type,float value)
    {
        if (Resistance_AttackType.ContainsKey(type))
        {
            Resistance_AttackType[type] = value;
        }
        else
        {
            Resistance_AttackType.Add(type, value);
        }
    }
    public void Resistance_Change(Dictionary<AttackType, float> Resistance_AttackType)
    {
        this.Resistance_AttackType = Resistance_AttackType;
    }

    public void Resistance_Add(SinType type,float value)
    {
        if (Resistance_SinType.ContainsKey(type))
        {
            Resistance_SinType[type] += value;
        }
        else
        {
            Resistance_SinType.Add(type, 1 + value);
        }
    }
    public void Resistance_Add(AttackType type, float value)
    {
        if (Resistance_AttackType.ContainsKey(type))
        {
            Resistance_AttackType[type] += value;
        }
        else
        {
            Resistance_AttackType.Add(type, 1 + value);
        }
    }
    #endregion
    #region Effects
    [FoldoutGroup("MainData/BattleEffect")]
    List<BattleEffect> Effect_CurBattleEffect;
    //버프디버프 추가 감소
    public void Effect_GetEffect(BattleEffect battleEffect)
    {
        //if(Effect_CurBattleEffect.Contains(battleEffect))
    }

    public void Effect_AddEffect(BattleEffect battleEffect)// 
    {

    }

    public void Effect_NewEffect(BattleEffect battleEffect) // 버프 디버프 없을때 추가
    {

    }

    #endregion
    #region Skill
    [FoldoutGroup("MainData/Skill")]
    public List<Attack_Skill> Attack_Skill_All = new List<Attack_Skill>();
    [FoldoutGroup("MainData/Skill/Ingame")]
    public List<Attack_Skill> Skill_CurUse = new List<Attack_Skill>();
    [FoldoutGroup("MainData/Skill/Ingame"),Tooltip("현재 인게임 슬롯")]
    public List<Attack_Skill[]> Skill_Cur = new List<Attack_Skill[]>() ;
    [FoldoutGroup("MainData/Skill/Ingame")]
    public List<Dictionary<Attack_Skill, int>> Attack_Skill_CurAll = new List<Dictionary<Attack_Skill, int>>(1);
    public void Skill_CurExpand()
    {
        int count =  Attack_Skill_CurAll.Count;
        Attack_Skill_CurAll.Add(new Dictionary<Attack_Skill, int>());
        foreach(Attack_Skill skill in Attack_Skill_All)
        {
            Attack_Skill_CurAll[count].Add(skill,skill.skill_Count);
        }

        
        Skill_Cur.Add(new Attack_Skill[3] {NextSkill(count), NextSkill(count), NextSkill(count) });
    }

    public Attack_Skill NextSkill(int slotCount)
    {

        Attack_Skill result;
        int AllCount = 0;
        Dictionary<Attack_Skill, int> skill_CurAllSlot = Attack_Skill_CurAll[slotCount];

        foreach (int count in skill_CurAllSlot.Values)
        {
            AllCount += count;
        }

        System.Random rnd = new System.Random();
        AllCount = rnd.Next(1, AllCount + 1);
        foreach (Attack_Skill skill in skill_CurAllSlot.Keys)
        {
            AllCount = -skill_CurAllSlot[skill];
            if (AllCount <= 0 && skill.skill_Count != 0)
            {
                result = skill;
                skill_CurAllSlot[result] -= 1;
                return result;
            }
        }

        return null;
    }
    #endregion
    
    /*public List<BattleEffect> GetEffects()
    {
        List<BattleEffect> result = new List<BattleEffect>();
        foreach (EffectType type in CurEffects.Keys)
        {
            result.AddRange(CurEffects[type]);
            
        }
        return result;
        
    }*/

}


