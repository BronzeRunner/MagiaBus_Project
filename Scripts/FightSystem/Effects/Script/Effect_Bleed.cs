using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effect_Bleed : BattleEffect
{
    Character_Main Main;
    /*
     Bleed
    When tossing an attack Coin, take fixed damage by the effect¡¯s Potency. 
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

    }
    
    void Set_Event()
    {
        
    }

    public UnityEvent BleedActive ;
    public UnityEvent BleedCountDecrease ;

    public override void Effect_Active()
    {
        if(BleedActive == null)
        {
            //BleedActive +=  Bleed_Active; 
            //BleedActive
        }
        BleedActive?.Invoke();
        if(BleedCountDecrease == null)
        {

        }
        BleedCountDecrease?.Invoke();
    }

    public void Bleed_Active()
    {
        Main.HpCur_Change?.Invoke((int)ValueCheck());
    }
    public void Bleed_CountDecrease()
    {
        CountChange(-1);
    }

}
