using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;


[Serializable]
public struct EffectCondition
{
    [SerializeField]
    string[] Setting_strings;
    [SerializeField]
    int[] Setting_ints;
    Dictionary<string, int> Setting_Dictionary;
    public Dictionary<string,int> Setting
    {
        get 
        {
            if(Setting_Dictionary == null)
            {
                if(Setting_strings == null)
                {
                    return null;
                }
                Setting_Dictionary = new Dictionary<string, int>();
                for(int i = 0; i <Setting_strings.Length;i++)
                {
                    Setting_Dictionary.TryAdd(Setting_strings[i], Setting_ints[i]);
                }
            }
            return Setting_Dictionary;
        }
    }

    public bool ConnditionCheck()
    {
        if(Setting == null)
        {
            return true;
        }
        if(Setting_Dictionary.TryGetValue("Type", out int type)) // 0:����� ���� ȿ��Ȯ�� // 1: �ӵ��� ������ ��� AttackCoin �� Ȯ�� // 2:
        {
            switch (type)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                    
            }

        }


        return false;
    }
}


public interface IChatacter_Main { }
public class Character_Main : SerializedMonoBehaviour, IChatacter_Main
{
    #region Mental
    [SerializeField]
    [FoldoutGroup("Mental_Ori")]
    public int Mental_Cur = 0; // min -45 max 45
    [FoldoutGroup("Mental_Ori")]
    public int Mental_Max = 45; // ���ŷ� �ִ밪
    [FoldoutGroup("Mental_Ori")]
    public int Mental_Min = -45; // ���ŷ� �ּҰ�
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MaxPer; //���ŷ� �ִ밪 �ո� Ȯ��
    //[FoldoutGroup("Mental_Ori")]
    //public float Mental_MinPer; //���ŷ� �ּҰ� �ո� Ȯ��

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
    public float SpeedMin; // �ӵ� �ּҰ�
    public float Speed_GetMin()
    {
        float Value = SpeedMin * (Speed_Changes.ContainsKey("Multi_Max") ? Speed_Changes["Multi_Max"] : 1) + (Speed_Changes.ContainsKey("Plus_Min") ? Speed_Changes["Plus_Max"] : 0);
        return Value;
    }
    [FoldoutGroup("Speed")]
    public float SpeedMax; //�ӵ� �ִ밪
    public float Speed_GetMax()
    {
        float Value = SpeedMax * (Speed_Changes.ContainsKey("Multi_Min") ? Speed_Changes["Multi_Min"] : 1) + (Speed_Changes.ContainsKey("Plus_Max") ? Speed_Changes["Plus_Max"] : 0);
        return Value;
    }
    [FoldoutGroup("Speed")]
    public Dictionary<string, float> Speed_Changes = new Dictionary<string, float>(); // Plus_[] ([]�� + ��)  , Multi_[] ([]�� * ��)
    [FoldoutGroup("Speed")]
    public float Speed_Cur; // �ӵ� ��������
    public float Speed_Set()// 100 = max
    {
        int min = (int)Speed_GetMin();
        int max = (int)Speed_GetMax();
        if (min < max)
            Speed_Cur = UnityEngine.Random.Range(min, max + 1) + (Speed_Changes.ContainsKey("Plus_Cur") ? Speed_Changes["Plus_Cur"] : 0);
        else
            Speed_Cur = min;

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
        Hp_Cur += changeValue;
    }
    /// <summary>
    /// �˾ǼӼ��� ���ݼӼ��� ������ �޽��ϴ�.
    /// </summary>
    /// <param name="Value">��������</param>
    /// <param name="sintype">�˾ǼӼ�</param>
    /// <param name="attacktype">���ݼӼ�</param>
    public void Hp_GetDamage(int Value,SinType sintype,AttackType attacktype)
    {
        Value = (int)(Value * Resistance_Get(sintype) * Resistance_Get(attacktype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// ���ݼӼ��� ���⸸ �޽��ϴ�.
    /// </summary>
    /// <param name="Value">��������</param>
    /// <param name="attacktype">���ݼӼ�</param>
    public void Hp_GetDamage(int Value, AttackType attacktype)
    {
        Value = (int)(Value * Resistance_Get(attacktype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// �˾ǼӼ��� ���⸸ �޽��ϴ�.
    /// </summary>
    /// <param name="Value">��������</param>
    /// <param name="sintype">�˾ǼӼ�</param>
    public void Hp_GetDamage(int Value, SinType sintype)
    {
        Value = (int)(Value * Resistance_Get(sintype));
        Hp_CurChange(Value);
    }
    /// <summary>
    /// �˾ǼӼ��� ���ݼӼ��� ������ �����ʰ� �������ظ� �����ϴ�.
    /// </summary>
    /// <param name="Value">��������</param>
    public void Hp_GetDamage(int Value)
    {
        Hp_CurChange(Value);
    }

    /*
    //[FoldoutGroup("Hp")]
    //public Dictionary<string, UnityEvent<int>> Hp_IntEvents;
    //public void Hp_EventSetting()
    //{

    //}
    
    
    //public Dictionary<TriggerType, Action> CharacterTriggers; // ���� �ߵ��� �̺�Ʈ <ȿ���ٷ�� �ڵ� Ŀ�ǵ� �ý��� �������� ���ۼ�>
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
    Dictionary<SinType, float> Resistance_SinType = new Dictionary<SinType, float>(); // �˾ǼӼ� ������ 1�� �ƴѰ�츸 ����Ȥ���߰�
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
    #endregion
    #region Skill
    public List<Attack_Skill> Skill_CurUse = new List<Attack_Skill>();
    public List<Attack_Skill[]> Skill_Cur = new List<Attack_Skill[]>(1) { new Attack_Skill[3] };
    
    public List<Attack_Skill> Attack_Skill_All = new List<Attack_Skill>();
    public List<Dictionary<Attack_Skill, int>> Attack_Skill_CurAll = new List<Dictionary<Attack_Skill, int>>(1);
    private void Skill_CurExpand()
    {
        int count =  Attack_Skill_CurAll.Count;
        Attack_Skill_CurAll.Add(new Dictionary<Attack_Skill, int>());
        foreach(Attack_Skill skill in Attack_Skill_All)
        {
            Attack_Skill_CurAll[count].Add(skill,skill.Attack_SkillCount);
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
            if (AllCount <= 0 && skill.Attack_SkillCount != 0)
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


