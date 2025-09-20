using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Egg", menuName = "Egg", order = 2)]
public class EggData : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public RuntimeAnimatorController eggAnimation;
}
