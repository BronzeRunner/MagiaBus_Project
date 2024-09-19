using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Animation
{
    void IA_clash_Same();
    void IA_clash_Lose();
    void IA_clash_Win();
    void IA_clash_WinEnd();
}

public class Animator_sub : MonoBehaviour
{
    Animator anim;
    public Animator MyAnimator
    {

        get
        {
            Animator result = anim;
            if (anim == null)
            {
                
                if (TryGetComponent<Animator>(out result))
                {
                    anim = result;
                    
                }
                else
                {
                    result = transform.GetComponentInChildren<Animator>();
                    if(result == null)
                    {
                        result = transform.GetComponentInParent<Animator>();
                    }
                }

                
            }
            return anim;
        }
    }
    
}
