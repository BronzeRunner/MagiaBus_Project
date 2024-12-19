using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
public delegate void Rint(ref int i);

[System.Serializable]
public class EffectTriggerSystem : SerializedMonoBehaviour
{
    //Coin_Toss(n,i) , Turn_End(n) ,Coin_Front(i),Coin_Back(i),Hp_Change(i),Hp_GetDamage(i),Hp_[Reason]Dmg(i),[EffectName]_Get(n),[EffectName]_CountGet(i),[EffectName]_ValueGet(i)
    //OnHit_[Reason](i)
    //T MainCode;
    [SerializeField, FoldoutGroup("EffectTrigger")]
    public Dictionary<string, UnityAction> EffectTrigger_None = new Dictionary<string, UnityAction>();
    [SerializeField, FoldoutGroup("EffectTrigger")]
    public Dictionary<string, Rint> EffectTrigger_int = new Dictionary<string, Rint>();

    public void Add_EffectTrigger(string tag,UnityAction Effect)
    {
        if(!EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None.Add(tag, null);    
        }
        EffectTrigger_None[tag]+= Effect;

    }
    public void Add_EffectTrigger(string tag, Rint Effect)
    {
        if (!EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int.Add(tag, null);
        }
        EffectTrigger_int[tag] += Effect;
    }
    public void Remove_EffectTrigger(string tag,UnityAction Effect)
    {
        if (EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None[tag] -= Effect;
        }
    }
    public void Remove_EffectTrigger(string tag, Rint Effect)
    {
        if (EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int[tag] -= Effect;
        }
    }
    /// <summary>
    /// (tag) 상황시 발동하는 변수를 요구하지않는 이펙트를 발동시킴니다.
    /// </summary>
    /// <param name="tag"></param>
    public void Call_EffectTrigger(string tag)
    {
        if (EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None[tag]?.Invoke();
        }
    }
    /// <summary>
    /// (tag) 상황시 발동하는 int값(value)를 요구는 이펙트를 발동시킴니다.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="Value"></param>
    public void Call_EffectTrigger(string tag , ref int Value)
    {
        if (EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int[tag]?.Invoke(ref Value);
        }
    }
    //public void Find_EffectTrigger(string tag)


}
