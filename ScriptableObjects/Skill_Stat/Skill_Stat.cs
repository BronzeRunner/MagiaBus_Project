using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

//오직 스킬스텟 int float 등등 만 저장
//스킬하나당 하나 쓰기엔 너무 많아짐

[CreateAssetMenu(fileName = "Skill_Stat", menuName = "Scriptable Objects/Skill_Stat")]
public class Skill_Stat : SerializedScriptableObject
{
    public int skill_Coin_Count;
    //코인값 (절대값)
    public int skill_Coin_Value;
    //코인 계산방식 (플러스 코인, 음수코인,곱하기 나누기 등등등)
    public Coin_CalculateType skill_Coin_CalculateType;
    //스킬 기본위력
    public int skill_Value_Normal;
    
    //코인 계산은 총괄하는 다른코드에서
    public List<Coin_Main> skill_Coins ;
    

    
}
