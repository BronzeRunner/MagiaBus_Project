using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public class Effect_Bleed : BattleEffect
{
    
    Character_Main Main;
    /*
     Bleed
    When tossing an attack Coin, take fixed damage by the effect’s Potency. 
    Then, reduce its Count by 1.
     */
    UnityAction Effect_Active_Delegate; //효과발동
    UnityAction<int> Effect_Consume_Delegate;  //횟수감소
    public Effect_Bleed(int Value,int Count, Component code)
    {
        if(!code.TryGetComponent<Character_Main>(out Main))
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
    public Effect_Bleed(int Value, Component code)
    {
        int Count = 1;
        if (!code.TryGetComponent<Character_Main>(out Main))
        {
            EffectRemove();
        }
        else
        {
            Setting();
            ValueChange(Value);
            CountChange(Count);
        }  
    }
    public Effect_Bleed(Component code, int Count)
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

    public override void Setting()
    {
        Main.Add_EffectTrigger("Coin_Toss",Bleed_Active);
        Main.Add_EffectTrigger("Turn_End", Bleed_Consume_UnActive);

        Effect_Active_Delegate = Effect_Active;
        Effect_Consume_Delegate = Bleed_Consume;
    }
    
    public void SetEvent_Active()
    {
        Effect_Active_Delegate = Effect_Active;
    }
    public void SetEvent_Consume()
    {
        Effect_Consume_Delegate = Bleed_Consume;
    }
    /*
    Dictionary<string, UnityEvent> Bleed_Events;
    Dictionary<string, UnityEvent<int>> Bleed_IntEvents;
    */


    public void Bleed_Active()
    {
        Main.Call_EffectTrigger("Bleed_Active");
    }
    public override void Effect_Active()
    {
        if(!Effect_IsActive)
        {
            return;
        }
        Bleed_Damage(ValueCheck());
        Effect_Consume_Delegate(1);
    }

    public void Bleed_Damage(int i)
    {
        Main.Call_EffectTrigger("Hp_BleedDmg",ref i);
        Main.Hp_GetDamage(i);
    }
    public void Bleed_Get(int Value,int Count)
    {
        Main.Call_EffectTrigger("Bleed_Get");
        Bleed_CountGet(Count);
        Bleed_ValueGet(Value);
    }
    public void Bleed_CountGet(int Count)
    {
        Main.Call_EffectTrigger("Bleed_CountGet", ref Count);
        CountAdd(Count);
    }
    public void Bleed_ValueGet(int Value)
    {
        Main.Call_EffectTrigger("Bleed_ValueGet", ref Value);
        ValueAdd(Value);
    }
    public void Bleed_ValueChange(int value)
    {
        ValueChange(value); 
    }
    public void Bleed_Consume(int i)
    {
        // 횟수감소(효과사용,턴종료,외부간섭)
        CountAdd(i);
    }
    public void Bleed_Consume_Active()
    {
        if (!Effect_IsActive)
        {
            return;
        }
        //효과사용으로인한 감소

        CountAdd(-1);
    }
    public void Bleed_Consume_UnActive()
    {
        if (!Effect_IsActive)
        {
            return;
        }
        //턴종료 로인한 감소
        Effect_Consume_Delegate(1);
    }
}
