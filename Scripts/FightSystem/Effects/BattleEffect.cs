using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum EffectType { N, Buff, Debuff };
/*public enum TriggerType 
{   Fail,Clash_Win, Clash_Lose, Clash_Same,ClashEnd_Win, ClashEnd_Lose, ClashCoin_CheckStart, ClashCoin_CheckEnd , Start,End,
    ClashCoin1_true, ClashCoin2_true, ClashCoin3_true, ClashCoin4_true, ClashCoin5_true, ClashCoin6_true, ClashCoin7_true, ClashCoin8_true, ClashCoin9_true, ClashCoin10_true,
    ClashCoin1_false, ClashCoin2_false, ClashCoin3_false, ClashCoin4_false, ClashCoin5_false, ClashCoin6_false, ClashCoin7_false, ClashCoin8_false, ClashCoin9_false, ClashCoin10_false,
    AttackCoin1_true, AttackCoin2_true, AttackCoin3_true, AttackCoin4_true, AttackCoin5_true, AttackCoin6_true, AttackCoin7_true, AttackCoin8_true, AttackCoin9_true, AttackCoin10_true,
    AttackCoin1_false, AttackCoin2_false, AttackCoin3_false, AttackCoin4_false, AttackCoin5_false, AttackCoin6_false, AttackCoin7_false, AttackCoin8_false, AttackCoin9_false, AttackCoin10_false,
    Sink_Trigger
};*/ //걍 string 으로 바꿈
public enum EffectScriptType { None,Character_MainSelf,FightManager };

public abstract class BattleEffect : MonoBehaviour
{
    //public delegate void intDelegate(int i);
    public bool Effect_show = false ;
    public EffectScriptType SettingType;
    public bool Effect_IsActive = true;
    EffectType E_Type;
    public string Invoke_Type;
    protected string Effect_Name;
    int Effect_Value;
    int Effect_Count;

    public virtual void AvilableCheck()
    {
        if (Effect_Count <= 0 || Effect_Value <= 0)
        {
            EffectRemove();
        }

    }

    public virtual void Get(int value, int Count)
    {
        ValueChange(value);
        CountChange(Count);
    }

    public virtual int ValueCheck()
    {
        return Effect_Value;
    }

    public virtual int CountCheck()
    {
        return Effect_Count;
    }

    public virtual void ValueChange(int Value)
    {
        Effect_Value = Value;
        AvilableCheck();
    }
    public virtual void ValueAdd(int Value)
    {
        Effect_Value += Value;
        AvilableCheck();
    }

    public virtual void CountChange(int count)
    {
        Effect_Count = count;
        AvilableCheck();
    }
    public virtual void CountAdd(int count)
    {
        Effect_Count += count;
        AvilableCheck();

    }
    public virtual void EffectRemove()
    {
        //효과제거
        Effect_Value = 0;
        Effect_Count = 0;
        Effect_IsActive = false;
    }

    public virtual void Effect_Active()
    {

        CountChange(-1);
    }

    //public virtual Action SetTrigger()
    //{
    //    return Effect_Active;
    //}

    public virtual void Setting()
    {

    }

    public virtual void TurnEnd()
    {
        CountChange(-1);
    }
}

public interface IBattleEffect_SetTrigger
{
    public Action SetTrigger();
    
}

public interface IBattleEffect_FirstSetting
{
    public void Setting(Component obj);
}

public interface IBattleEffectInterface : IBattleEffect_SetTrigger , IBattleEffect_FirstSetting
{
    
}

public class Effect_Sink :BattleEffect 
{

    UnityAction Sink_Active_Delegate;
    UnityAction<int> Sink_Consume_Delegate;
    public Character_Main Main;
    public Effect_Sink(int Value, int Count, Component code)
    {
        if (!code.TryGetComponent<Character_Main>(out Main))
        {
            EffectRemove();
        }
        else
        {
            Setting();
        }

        ValueChange(Value);
        CountChange(Count);

    }
    public Effect_Sink(int Value, Component code)
    {
        int Count = 1;
        if (!code.TryGetComponent<Character_Main>(out Main))
        {
            EffectRemove();
        }
        else
        {
            Setting();
        }

        ValueChange(Value);
        CountChange(Count);
    }
    public Effect_Sink(Component code, int Count)
    {
        int Value = 1;
        if (!code.TryGetComponent<Character_Main>(out Main))
        {
            EffectRemove();
        }
        else
        {
            Setting();
        }

        ValueChange(Value);
        CountChange(Count);
    }
    public void Sink_Active()
    {
        Effect_Active();
    }
    public override void Effect_Active()
    {
        if(!Effect_IsActive)
        {
            return;
        }
        Main.MentalValueChange(-ValueCheck());
       
        CountChange(-1);
    }
    public void Sink_Consume_Active(int i)
    {
        if (!Effect_IsActive)
        {
            return;
        }
        // 효과사용으로 인한 감소
        CountAdd(i);
    }
    

    public override void Setting()
    {
        

    }

    public void SetEvent_Active()
    {
        Sink_Active_Delegate = Sink_Active;
    }
    public void SetEvent_Consume()
    {
        Sink_Consume_Delegate = Sink_Consume_Active;
    }




}

public class Paralyze : BattleEffect
{
    public test target;
    public override void Effect_Active()
    {
        
        base.Effect_Active();
    }
}

