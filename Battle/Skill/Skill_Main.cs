using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using JetBrains.Annotations;

public enum Coin_Type{Normal,UnBreakable}
public enum Coin_CalculateType{Plus,Minus,Multiply,Substract}
public class Skill_Main :SerializedMonoBehaviour
{
    public Skill_Stat skill_Stat;

    //표시되는 스킬이름
    string skill_Name;
    //코인갯수
    //public int skill_Coin_Count;
    //코인값 (절대값)
    public int skill_Coin_Value;
    //코인 계산방식 (플러스 코인, 음수코인,곱하기 나누기 등등등)
    public Coin_CalculateType skill_Coin_CalculateType;
    //스킬 기본위력
    public int skill_Value_Normal;
    
    //코인 계산은 총괄하는 다른코드에서
    public List<Coin_Main> skill_Coins ;
    public Dictionary<int,Coin_Type> skill_Coin_Type;
    

    //계산식
    public int Coin_GetValue(int coin_Value,bool coin_bool)
    {
        int result= coin_bool? coin_Value:0;

        return result;
    }
    /// <summary>
    /// 코인값을 입력하여 해당값에따른 값변화를 결과값에 더함함
    /// </summary>
    /// <param name="skill_Value_Cur"></param>
    /// <param name="coin_Value_result"></param>
    /// <param name="skill_Coin_CalculateType"></param>
    /// <returns></returns>
    public int Coin_Calculate(ref int skill_Value_Cur,int coin_Value_result,Coin_CalculateType skill_Coin_CalculateType)
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
    public void Coin_Breake()
    {
        coin_IsBroken = true;
    }
}
