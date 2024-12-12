using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Attack_Values", menuName = "ScriptableObjects/Attack_ScriptableObject", order = 1)]
public class Attack_Values : ScriptableObject
{
    [FoldoutGroup("Coin_ori"), Tooltip("코인 기본값")]
    public float coin_Original; // 코인 기본값
    [FoldoutGroup("Coin_ori"), Tooltip("최소코인위력 \n(뒷면 혹은 코인이 존재하나 코인토스미적용시 존재할경우(파불코)에도 적용)")]
    public float coin_MinimamValue; // 최소코인위력
}
