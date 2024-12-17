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
    public Effect_Bleed(int Value,int Count, Component code)
    {
        if(!code.TryGetComponent<Character_Main>(out Main))
        {
            EffectRemove();
        }
        ValueChange(Value);
        CountChange(Count);
        Set_Event();

    }
    
    void Set_Event()
    {
        
    }

    Dictionary<string, UnityEvent> Bleed_Events;
    Dictionary<string, UnityEvent<int>> Bleed_IntEvents;



    public override void Effect_Active()
    {
        if(!Bleed_Events.ContainsKey("BleedActive"))
        {
            //BleedActive = new ;   
        }
        Bleed_Events["BleedActive"]?.Invoke(ValueCheck());
        if(!Bleed_Events.ContainsKey("BleedConsume"))
        {

        }
        Bleed_Events["BleedConsume"]?.Invoke();
    }

    public void Bleed_Active()
    {
        //Main.Hp_IntEvents["HpCurChange"]?.Invoke((int)ValueCheck());
        Main.Hp_GetDamage(Effect_);
    }
    public void Bleed_CountChange(int count)
    {
        CountChange(count);
    }
    public void Bleed_ValueChange(int value)
    {
        ValueChange(value); 
    }
    public void Bleed_Consume()
    {
        CountChange(-1);
    }
    public void Bleed_Consume_Active()
    {
        //효과사용으로인한 감소
        Bleed_Consume();
    }
    public void Bleed_Consume_UnActive()
    {
        //턴종료 로인한 감소
        Bleed_Consume();
    }
}
