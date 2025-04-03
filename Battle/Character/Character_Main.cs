using System.Collections.Generic;
using UnityEngine;

public enum Skill_Type {N,Attack, Evade, Counter,Deffend,Counter_p,Deffend_p}
public enum Attack_Type{N,Slash,Pierce,Blunt};
/// <summary>
/// Envy : 질투 , Gluttony : 탐식 , Lust : 색욕 , Pride : 오만 , Sloth : 나태 , Wrath : 분노, 우울?
/// </summary>
public enum Sin_Type{N= 0,Envy= 1,Gluttony=1,Lust=1,Pride=1,Sloth=1,Wrath=1 };
public enum Struggle_State{Normal = 0,Struggle= 1,StruggleP = 2,StrugglePP = 3};
public abstract class Character_Main
{
    //정신력 확률 계산식 == 정신력 + 50 (5퍼 3번은 언제봐도 레전드)
    #region Sanity
    int character_Sanity;
    public int Character_Sanity
    {
        get
        {return character_Sanity;}
        
        set
        {
            value = Mathf.Clamp(value,character_Sanity_Min,character_Sanity_Max);
            character_Sanity = value;
        }
    }
    int character_Sanity_Max = 45;
    public int Character_Sanity_Max
    {
        get
        {return character_Sanity_Max;}
        set 
        {
            character_Sanity_Max = value < character_Sanity_Min ? character_Sanity_Min:value; 
        }
    }
    
    int character_Sanity_Min = -45;
    public int Character_Sanity_Min
    {
        get
        {return character_Sanity_Min;}
        set 
        {
            character_Sanity_Min = value > character_Sanity_Max ? character_Sanity_Max:value; 
        }
    }
    
    public bool Character_CoinResult()
    {
        int value = Random.Range(1,101);
        if(Character_Sanity +50<= value)
        {
            return true;
        }
        
        return false;
    }
    #endregion

    #region Hp
    //최대체력
    int character_Hp_MaxHp; 
    //현재체력
    int character_Hp_CurHp;
    //최소체력 (보통은 0)
    int character_Hp_MinHp = 0;



    #endregion

    #region  Struggle
    //현재 흐트러짐상태
    Struggle_State character_Struggle_CurState;
    //흐트러짐상태 변경 가능여부 (흐트러질경우 보통 다음턴 흐트러짐이 유지되는동안 false)
    public bool character_Struggle_IsAvailable;
    //흐트러짐선 0번부터 값이 0이되면 흐트러짐상태부여여
    List<int> character_Struggle_StruggleLine;
    #endregion

    #region Resistance
    //공격속성 저항
    Dictionary<Attack_Type,float> character_Resistance_AttacType;
    //죄악속성 저항
    Dictionary<Sin_Type,float> character_Resistance_SinType;
    #endregion

    #region Speed
    //속도 최소값
    int character_Speed_MinSpeed;
    //속도 최대값
    int character_Speed_MaxSpeed;
    //속도 현재값
    int character_Speed_CurSpeed;
    
    //속도 값 얻기기
    public int Character_Speed_GetSpeed()
    {
        int result = Random.Range(character_Speed_MinSpeed,character_Speed_MaxSpeed);
        return result;
    }
    //속도값 설정
    public void Character_Speed_SetCurSpeed()
    {
        Character_Speed_SetCurSpeed(Character_Speed_GetSpeed());
    }
    public void Character_Speed_SetCurSpeed(int speedValue)
    {
        character_Speed_CurSpeed = speedValue;
    }
    #endregion

    
    
}

public class test:Character_Main
{
    
} 
