using UnityEngine;

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
    
}

public class test:Character_Main
{
    
} 
