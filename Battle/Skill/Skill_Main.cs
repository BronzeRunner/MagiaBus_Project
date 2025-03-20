using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor.Validation;
using Unity.VisualScripting;

public enum Coin_Type{Normal,UnBreakable}
public enum Coin_CalculateType{Plus,Minus,Multiply,Substract}
public class Skill_Main :SerializedMonoBehaviour
{
    #region Skill
    public Skill_Main(Skill_Stat skill_Stat)
    {
        Setting_Skill(skill_Stat);
    }
    public Skill_Stat skill_Stat;
    [ContextMenu("Setting_Skill")]
    public void Setting_Skill()
    {
        Setting_Skill(skill_Stat);
    }
    public void Setting_Skill(Skill_Stat skill_Stat)
    {
        this.skill_Stat = skill_Stat;
        skill_Name = skill_Stat.skill_Name;
        skill_Coin_Value = skill_Stat.skill_Coin_Value;
        skill_Coin_CalculateType = skill_Stat.skill_Coin_CalculateType;
        skill_Value_Normal = skill_Stat.skill_Value_Normal;
        skill_Coins = new List<Coin_Main>(skill_Stat.skill_Coins) ;
        
    }

    //표시되는 스킬이름
    public string skill_Name;
    //코인갯수
    //public int skill_Coin_Count;
    //코인값 (절대값)
    public int skill_Coin_Value;
    //코인 계산방식 (플러스 코인, 음수코인,곱하기 나누기 등등)
    public Coin_CalculateType skill_Coin_CalculateType;
    //스킬 기본위력
    public int skill_Value_Normal;
    
    //코인 계산은 총괄하는 다른코드에서 + 코인리셋 필요
    public List<Coin_Main> skill_Coins ;
    //public Dictionary<int,Coin_Type> skill_Coin_Type;
    #endregion
    //합진행시 코인 계산여부확인
    #region  Coin_Check
    public Coin_Main Coin_Check()
    {
        Coin_Main result = null;
        foreach(Coin_Main coin in skill_Coins)
        {
            if(Coin_Check(coin))
            {
                result = coin;
                break;
            }
        }
        return result;
    }
    public bool Coin_Check(int coin_CurCount)
    {
        bool result = true;
        if(skill_Coins.Count < coin_CurCount)
        {
            result =false;
        }
        else 
        {
            result = Coin_Check(skill_Coins[coin_CurCount]);
        }
        return result;
    }
    public bool Coin_Check(Coin_Main coin_Main)
    {
        if(coin_Main.coin_IsBroken)
        {
            return  false;
        }

        return true;
    }

    #endregion
    //계산식
    // 코인 앞뒤면에따라 "코인위력" 을 받아옴(Coin_Calculate 스킬 계산유형 반영전)
    public int Coin_GetValue(int coin_CurCount, int coin_Value,bool coin_bool)
    {
        
        int result= 0;
        if(skill_Coins.Count > coin_CurCount)
        {
            Coin_CoinValueCalculate(skill_Coins[coin_CurCount],coin_Value,coin_bool);
            
        }


        return result;
    }
    //구버전
    public int Coin_GetValue(int coin_Value,bool coin_bool)
    {
        int result= coin_bool? coin_Value:0;

        return result;
    }
    public int Coin_CoinValueCalculate(Coin_Main coin,int coin_Value,bool coin_bool)
    {
        int result = 0;
        switch(coin.coin_Type)
        {
            //일반
            case Coin_Type.Normal :
            {
                if(!coin.coin_IsBroken && !coin.coin_Result)
                {
                    result= coin_bool? coin_Value:0;
                }
                
                break;
            }
            //파불코
            case Coin_Type.UnBreakable :
            {
                if(!coin.coin_Result)
                {
                    result= coin_bool? (coin.coin_IsBroken ? 1: coin_Value):0 ;
                }

                break;
            }
        }

        return result;
    }
    /// <summary>
    /// 코인값을 입력하여 해당값에따른 값변화를 결과값에 더함
    /// </summary>
    /// <param name="skill_Value_Cur"></param>
    /// <param name="coin_Value_result"></param>
    /// <param name="skill_Coin_CalculateType"></param>
    /// <returns></returns>
    public int Coin_SkillValueCalculate(ref int skill_Value_Cur,int coin_Value_result,Coin_CalculateType skill_Coin_CalculateType)
    {
        int result = 0;
        switch(skill_Coin_CalculateType)
        {
            case Coin_CalculateType.Plus:
            {
                result = Coin_Calculate_Plus(ref skill_Value_Cur,coin_Value_result);
                break;
            }

            default:
            {
                break;
            }
        }

        return result;
    }

    public int Coin_Calculate_Plus(ref int skill_Value_Cur,int coin_Value)
    {
        skill_Value_Cur += coin_Value;
        return coin_Value;
    }

    public bool Skill_CoinBreak()
    {
        foreach(Coin_Main coin in skill_Coins )
        {
            if(coin.coin_IsBroken)
            {
                coin.Coin_Break();
                return true;
            }
        }

        return false;
    }
    //코인 앞뒤 계산은 캐릭터에

    // public Coin_Type Skill_GetCoinType(int skill_Coin_Type_key)
    // {
    //     Coin_Type result;
    //     skill_Coin_Type.TryGetValue(skill_Coin_Type_key,out result);
    //     return result;
    // }

}


public class Coin_Main
{
    //원래(현재X) 몇번쨰 코인인지
    public int coin_Num;
    // 코인 앞뒤(true,false)
    public bool coin_Result;
    // 코인 파괴여부
    public bool coin_IsBroken;
    // 
    public Coin_Type coin_Type;
    public void Coin_Reset(Coin_Main coin_Main)
    {
        coin_Num = coin_Main.coin_Num;
        coin_Result = false;
        coin_IsBroken = false;
        coin_Type = coin_Main.coin_Type;
    }

    
    public void Coin_Break()
    {
        coin_IsBroken = true;
    }
}
