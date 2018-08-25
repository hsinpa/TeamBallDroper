using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[STP]BallScritable", menuName = "STP/Ball/Universal", order = 1)]
public class Ball_STP : ScriptableObject {
    public Color32[] baseColors;
    public GameObject generalPrefab;
    public Material generalMaterial;
}
