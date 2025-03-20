using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class Skill_CoinCaculate : MonoBehaviour
{
    #region Creater
     public Skill_CoinCaculate(Skill_Main skill_Main)
    {
        Setting_Skill(skill_Main);

        Reset_CurValues();
    }

    #endregion

    #region Skill_
    public void Setting_Skill(Skill_Main skill_Main)
    {
        this.skill_Main = skill_Main;
        skill_Stat = skill_Main.skill_Stat;
        skill_Coin_Count = skill_Main.skill_Coins.Count;
        skill_Coin_Value = skill_Main.skill_Coin_Value;
        skill_Coin_CalculateType = skill_Main.skill_Coin_CalculateType;
        skill_Value_Normal = skill_Main.skill_Value_Normal;
    }
    
    
    //스킬 시전자
    public Character_Main character_Main;
    //거던 이나 효과등등으로 변경된 스킬셋은 아래 skill Main에
    //Skill_CoinCaculate 생성시 발동 + 여기서 레벨계산할듯(바로다음 합 시작)

    //이제 완전원본값은 스킬스탯에
    public Skill_Stat skill_Stat;
    public Skill_Main skill_Main;
    public int skill_Coin_Count;
    //코인값 (절대값)
    public int skill_Coin_Value;
    //코인 배수(음수는 -1 양수는 1)
    Coin_CalculateType skill_Coin_CalculateType;
    //스킬 기본위력
    public int skill_Value_Normal;


    #endregion
    //public Dictionary<int,Coin_Type> skill_Coin_Type;

    // 합진행중 변경된값은 아래 cur 값들에
    #region Cur_합진행중 값들
    public void Setting_Cur()
    {
        Reset_CurValues();
    }
    //합진행중 코인갯수 해당겟수만큼 코인계산함함
    public int skill_Coin_Count_Cur;
    public int skill_Coin_Value_Cur;
    public Coin_CalculateType skill_Coin_CalculateType_Cur;
    //public int skill_Value_Normal_Cur;
    public int skill_Value_Cur;
    
    #endregion
    
    
    //코인 계산은 총괄하는 다른코드에서
    

   
    //CurValue 리셋
    public void Reset_CurValues()
    {
        
        skill_Coin_Count_Cur = skill_Coin_Count;
        skill_Coin_Value_Cur = skill_Coin_Value;
        skill_Value_Cur = 0;
        //skill_Coin_Type_Cur = skill_Coin_Type;
    }

    public (bool coin_bool,int coin_Value_result, int Coin_Value_Calculated) Coin_Value_Calculate(int coin_CurCount)
    {
        
        bool coin_bool = true;//코인 앞뒤 계산 (캐릭터 에서 계산)
        //코인값 얻기 
        int coin_Value_result = skill_Main.Coin_GetValue(coin_CurCount,skill_Coin_Value_Cur,coin_bool);
        //해당값 바탕으로 type에따라 값 적용시키기
        int Coin_Value_Calculated = skill_Main.Coin_SkillValueCalculate(ref skill_Value_Cur,coin_Value_result,skill_Coin_CalculateType);
        return (coin_bool,coin_Value_result,Coin_Value_Calculated);
    }


    // public bool Coin_TrueFalse_Calculate()
    // {
    //     if(Random.Range(1,101) <= coin_Percentage)
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    [ContextMenu("Skill_Calculate")]
    public void Test()
    {
        //Setting_Skill(new Skill_Main(skill_Stat));
        Setting_Cur();
        Skill_Calculate();
    }
    public void Skill_Calculate()
    {
        skill_Value_Cur = skill_Value_Normal;
        //한번씩 번갈아가면서 계산하도록
        for(int i = 0; i < skill_Coin_Count;i++)
        {
            var logData = Coin_Value_Calculate(i);
            Debug.Log($"코인 : {logData.coin_bool}|코인 결과값 : {logData.coin_Value_result}|스킬 위력변동값 : {logData.Coin_Value_Calculated}");
        }
        //Debug.Log($"{skill_Stat.skill_Name} : 위력 {skill_Value_Cur}");

    }

    public void Skill_Lose()
    {
        Coin_Break();
        //합패배
    }
    public void Coin_Break()
    {
        Coin_Main coin = skill_Main.Coin_Check();
        if (coin != null)
        {
            coin.Coin_Break();
        }
    }

    // [ContextMenu("test")]
    // public void Skill_Fix()
    // {
    //     //skill_Stat.test = Random.Range(-10.0f,11.0f);
    //     //StreamReader readingTest;
        
    //     Debug.Log(skill_Stat.Serialize());
    //     Debug.Log(AssetDatabase.GetAssetPath(skill_Stat));
    //     Debug.Log(skill_Stat.Serialize().Deserialize());
        
    // }
}
