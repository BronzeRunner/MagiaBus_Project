using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
//using System.IO;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;


public interface IChatacter_Main { }
public class Character_Main : EffectTriggerSystem, IChatacter_Main
{
    [SerializeField]
    Character_Stats Main_Data_Stat;
    
    public virtual void Data_Stat()
    {
        
        Level_Cur = 50;
         // 동기화 상태 추후 json으로 수정 + 인격관련 코드에 추가

        Hp_Max = Main_Data_Stat.Hp_Max; //레벨당 Hp최대값 증가 추가
        Hp_Cur = Hp_Max /100 *Main_Data_Stat.Hp_StartPer;
        Resistance_Change(Main_Data_Stat.Resistance_AttackType); //공격속성 설정
        Resistance_Change(Main_Data_Stat.Resistance_SinType); //죄악속성 설정
    }

    #region Level
    public int Level_Cur;
    //public int 
    #endregion 
    #region Mental
    [SerializeField]
    [FoldoutGroup("Mental"),]
    public int Mental_Cur = 0; // min -45 max 45
    [HorizontalGroup()]
    [FoldoutGroup("Mental")]
    public int Mental_Max = 45; // 정신력 최대값
    [FoldoutGroup("Mental")]
    public int Mental_Min = -45; // 정신력 최소값
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MaxPer; //정신력 최대값 앞면 확률
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MinPer; //정신력 최소값 앞면 확률

    public virtual void MentalValueChange(int value)
    {
        Mental_Cur += value;
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

    [Button("CoinPercentage Test"), FoldoutGroup("Mental_Ori")]
    public void GetCoinPercentage_test()
    {
        Debug.Log($"Cur Percentage = {Mental_Cur + 50},CurCoin state = {CoinCalculate()}");
    }
    #endregion
    #region speed
    [FoldoutGroup("Speed")]
    public int SpeedMin; // 속도 최소값
    public int Speed_GetMin()
    {
        int Value = SpeedMin;
        Call_EffectTrigger("Speed_GetMin", ref Value);
        return Value;
    }
    [FoldoutGroup("Speed")]
    public int SpeedMax; //속도 최대값
    public int Speed_GetMax()
    {
        int Value = SpeedMax;
        Call_EffectTrigger("Speed_GetMax", ref Value);
        return Value;
    }
    //[FoldoutGroup("Speed")]
    //public Dictionary<string, int> Speed_Changes = new Dictionary<string, int>(); // Plus_[] ([]값 + 값)  , Multi_[] ([]값 * 값)
    [FoldoutGroup("Speed")]
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
    #endregion
    #region Hp
    [FoldoutGroup("Hp")]
    public float Hp_Max;
    [FoldoutGroup("Hp")]
    public float Hp_Min;
    [FoldoutGroup("Hp")]
    public float Hp_Cur;
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
    public void Hp_GetDamage(int Value,SinType sintype,AttackType attacktype)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Value * Resistance_Get(sintype) * Resistance_Get(attacktype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// 공격속성의 영향만 받습니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    /// <param name="attacktype">공격속성</param>
    public void Hp_GetDamage(int Value, AttackType attacktype)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Value * Resistance_Get(attacktype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// 죄악속성의 영향만 받습니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    /// <param name="sintype">죄악속성</param>
    public void Hp_GetDamage(int Value, SinType sintype)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Value = (int)(Value * Resistance_Get(sintype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// 죄악속성과 공격속성의 영향을 받지않고 고정피해를 입힙니다.
    /// </summary>
    /// <param name="Value">공격위력</param>
    public void Hp_GetDamage(int Value)
    {
        Call_EffectTrigger("Hp_GetDamage", ref Value);
        Hp_CurChange(Value);
    }

    /*
    //[FoldoutGroup("Hp")]
    //public Dictionary<string, UnityEvent<int>> Hp_IntEvents;
    //public void Hp_EventSetting()
    //{

    //}
    
    
    //public Dictionary<TriggerType, Action> CharacterTriggers; // 현재 발동될 이벤트 <효과다루는 코드 커맨드 시스템 느낌으로 재작성>
    */
    #endregion
    #region state
    public State CurState = State.Alive;
    public List<int> CurState_StruggleLine = new List<int>();
    public enum State
    {
        Alive = 1,Stagger = 2,Stagger_P = 3,Stagger_PP = 4,Dead = 0
    }
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
    public void State_Change(State state)
    {
        CurState = state;
    }
    #endregion
    #region Resistance
    Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // 죄악속성 저항이 1이 아닌경우만 변경혹은추가
    Dictionary<AttackType, float> Resistance_AttackType = new Dictionary<AttackType, float>();
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
    #region Skill
    public List<Attack_Skill> Skill_CurUse = new List<Attack_Skill>();
    public List<Attack_Skill[]> Skill_Cur = new List<Attack_Skill[]>(1) { new Attack_Skill[3] };
    
    public List<Attack_Skill> Attack_Skill_All = new List<Attack_Skill>();
    public List<Dictionary<Attack_Skill, int>> Attack_Skill_CurAll = new List<Dictionary<Attack_Skill, int>>(1);
    public void Skill_CurExpand()
    {
        int count =  Attack_Skill_CurAll.Count;
        Attack_Skill_CurAll.Add(new Dictionary<Attack_Skill, int>());
        foreach(Attack_Skill skill in Attack_Skill_All)
        {
            Attack_Skill_CurAll[count].Add(skill,skill.Skill_SkillCount);
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
            if (AllCount <= 0 && skill.Skill_SkillCount != 0)
            {
                result = skill;
                skill_CurAllSlot[result] -= 1;
                return result;
            }
        }

        return null;
    }
    #endregion
    #region SideEffects
    List<BattleEffect> Effect_CurBattleEffect;
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


