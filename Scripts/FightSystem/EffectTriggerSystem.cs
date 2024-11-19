using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EffectTriggerSystem : MonoBehaviour
{
    public Dictionary<string, UnityAction> EffectTrigger_None;
    public Dictionary<string, UnityAction<int>> EffectTrigger_int;

    public void Add_EffectTrigger(string tag,UnityAction Effect)
    {
        if(!EffectTrigger_None.ContainsKey(tag))
        {
            EffectTrigger_None.Add(tag, null);    
        }
        EffectTrigger_None[tag]+= Effect;

    }
    public void Add_EffectTrigger(string tag, UnityAction<int> Effect)
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
    public void Remove_EffectTrigger(string tag, UnityAction<int> Effect)
    {
        if (EffectTrigger_int.ContainsKey(tag))
        {
            EffectTrigger_int[tag] -= Effect;
        }
    }
    //public void Find_EffectTrigger(string tag)


}
