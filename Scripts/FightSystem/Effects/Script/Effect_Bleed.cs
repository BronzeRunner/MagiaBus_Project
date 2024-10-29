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
    When tossing an attack Coin, take fixed damage by the effect��s Potency. 
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

    public UnityEvent BleedActive ;
    public UnityEvent<int> BleedCountChange ;
    public UnityEvent BleedConsume;
    public UnityEvent BleedTurnEnd;
    

    public override void Effect_Active()
    {
        if(BleedActive == null)
        {
            //BleedActive = new ;   
        }
        BleedActive?.Invoke();
        if(BleedConsume == null)
        {

        }
        BleedConsume?.Invoke();
    }

    public void Bleed_Active()
    {
        Main.HpCur_Change?.Invoke((int)ValueCheck());
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
}
