using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyContainer : ScriptableObject {

    public List<WaveCurve> Curves = new List<WaveCurve>();

    public List<Wave> Waves = new List<Wave>();

}


[System.Serializable]
public class WaveCurve
{
    public int ID;
    public AnimationCurve Curve;
    
}

[System.Serializable]
public class Wave
{
    public int ID;
    public Enemy enemy;
    public int CurveID;
    public int count;
    public float spawnInterval;
    public float spawnTime;
}