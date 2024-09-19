using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum EffectType { N, Buff, Debuff };
public enum TriggerType 
{   Fail,Clash_Win, Clash_Lose, Clash_Same,ClashEnd_Win, ClashEnd_Lose, ClashCoin_CheckStart, ClashCoin_CheckEnd , Start,End,
    ClashCoin1_true, ClashCoin2_true, ClashCoin3_true, ClashCoin4_true, ClashCoin5_true, ClashCoin6_true, ClashCoin7_true, ClashCoin8_true, ClashCoin9_true, ClashCoin10_true,
    ClashCoin1_false, ClashCoin2_false, ClashCoin3_false, ClashCoin4_false, ClashCoin5_false, ClashCoin6_false, ClashCoin7_false, ClashCoin8_false, ClashCoin9_false, ClashCoin10_false,
    AttackCoin1_true, AttackCoin2_true, AttackCoin3_true, AttackCoin4_true, AttackCoin5_true, AttackCoin6_true, AttackCoin7_true, AttackCoin8_true, AttackCoin9_true, AttackCoin10_true,
    AttackCoin1_false, AttackCoin2_false, AttackCoin3_false, AttackCoin4_false, AttackCoin5_false, AttackCoin6_false, AttackCoin7_false, AttackCoin8_false, AttackCoin9_false, AttackCoin10_false,
    Sink_Trigger
};
public enum EffectScriptType { None,Character_MainSelf,FightManager };

public abstract class BattleEffect : MonoBehaviour
{
    public EffectScriptType SettingType;
    public bool Effect_IsActive = true;
    EffectType E_Type;
    public TriggerType Invoke_Type;
    protected string Effect_Name;
    float Effect_Value;
    float Effect_Count;

    public virtual void Get(float value, float Count)
    {
        ValueChange(value);
        CountChange(Count);
    }
    /*
    public virtual void TurnEnd()
    {
        Effect_Count -= 1;
    }
    public virtual void TrunStart()
    {

    }
    */
    
    public virtual float ValueCheck()
    {
        return Effect_Value;
    }

    public virtual float CountCheck()
    {
        return Effect_Count;
    }

    public virtual void ValueChange(float Value)
    {
        Effect_Value += Value;
    }

    public virtual void CountChange(float count)
    {
        Effect_Count += count;
        if (Effect_Count <= 0)
        {
            EffectRemove();
        }
    }

    public virtual void EffectRemove()
    {
        //효과제거
        Effect_IsActive = false;
    }

    public virtual void Effect_Active()
    {

        CountChange(-1);
    }

    public virtual Action SetTrigger()
    {
        return Effect_Active;
    }

    public virtual void Setting(Component obj)
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

public class Sink :BattleEffect 
{
    
    public Character_Main target;
    public override void Effect_Active()
    {
        if(!Effect_IsActive)
        {
            return;
        }
        target.MentalValueChange(-ValueCheck());
        if (target.CharacterTriggers.ContainsKey(TriggerType.Sink_Trigger))
        {
            target.CharacterTriggers[TriggerType.Sink_Trigger]?.Invoke();
        }
        else
        {
            Debug.Log($"{TriggerType.Sink_Trigger} is null");
        }
        CountChange(-1);
    }

    public override void Setting(Component obj)
    {
        if (obj.TryGetComponent<Character_Main>(out target))
        {
            Effect_IsActive = true;
            if(!target.CharacterTriggers.ContainsKey(TriggerType.Sink_Trigger))
            {
                target.CharacterTriggers.Add(TriggerType.Sink_Trigger, null);
            }
        }
        else
        {
            Effect_IsActive = false;
        }

    }

    
}

public class Paralyze : BattleEffect
{
    public test target;
    public override void Effect_Active()
    {
        target.CoinCheck_ChangeValue = 0;
        base.Effect_Active();
    }
}

