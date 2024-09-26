using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;



                                                                                           



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
        if(Setting_Dictionary.TryGetValue("Type", out int type)) // 0:디버프 버프 효과확인 // 1: 속도값 합위력 등등 AttackCoin 값 확인 // 2:
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
public class Character_Main : MonoBehaviour
{
    [SerializeField]

    
    [FoldoutGroup("Mental_Ori")]
    public float Mental = 0; // min -45 max 45
    [FoldoutGroup("Mental_Ori")]
    public float Mental_Max = 45; // 정신력 최대값
    [FoldoutGroup("Mental_Ori")]
    public float Mental_Min = -45; // 정신력 최소값
    [FoldoutGroup("Mental_Ori")]
    public float Mental_MaxPer; //정신력 최대값 앞면 확률
    [FoldoutGroup("Mental_Ori")]
    public float Mental_MinPer; //정신력 최소값 앞면 확률

    [FoldoutGroup("Speed")]
    public float SpeedMin; // 속도 최소값
    [FoldoutGroup("Speed")]
    public float SpeedMax; //속도 최대값
    [FoldoutGroup("Speed")]
    public float SpeedPlus; // 속도 변동값 (더하기)
    public float SpeedMultiplier; // 속도 변동값 (곱하기)
    [FoldoutGroup("Speed")]
    public float Speed_Cur; // 속도 현재결과값

    //public Dictionary<TriggerType, Action> CharacterTriggers; // 현재 발동될 이벤트 <효과다루는 코드 커맨드 시스템 느낌으로 재작성>
    enum State
    {
        Alive,Stagger,Stagger_P,Stagger_PP,Dead
    }
    Character_Main[] AttackTargets;
    //
    Dictionary<EffectType, List<BattleEffect>> CurEffects; // 현재 걸려있는 효과

    //inGame
    public AttackCoins[] CurAttack;
    public void SetCurCoin(int count,AttackCoins coins)
    {
        CurAttack[count] = coins;
    }

    public List<BattleEffect> GetEffects()
    {
        List<BattleEffect> result = new List<BattleEffect>();
        foreach (EffectType type in CurEffects.Keys)
        {
            result.AddRange(CurEffects[type]);
            
        }
        return result;
        
    }
    
    public virtual void MentalValueChange(float value)
    {
        Mental += value;
        
    }

    public virtual void MentalMinMaxCheck()
    {
        if(Mental_Max < Mental_Min)
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
        if(Mental > Mental_Max)
        {
            Mental = Mental_Max;
        }
        else if (Mental < Mental_Min)
        {

        }
    }

    public float GetCoinPercentage()
    {
        float result = 50;
        float mentalPer =  ((Mental < 0 ? -Mental : ((Mental_Min < 0 ? -Mental_Min : Mental_Min) + Mental)) / (Mental_Max - Mental_Min)); // 현재 정신력 비율
        Debug.Log("mentalper = " +mentalPer);
        result = (Mental_MaxPer - Mental_MinPer) * mentalPer + Mental_MinPer;
        return result;
    }
    [Button("CoinPercentage Test"),FoldoutGroup("Mental_Ori")]
    public void GetCoinPercentage_test()
    {
        Debug.Log($"Cur Percentage = {GetCoinPercentage()}");
    }

    


}


