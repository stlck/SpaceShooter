using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class EnemyWindow : EditorWindow {

    private static EnemyWindow _instance;

    [MenuItem("custom/create")]
	public static void OpenWindow()
    {
        _instance = EditorWindow.GetWindow<EnemyWindow>();
    }

    private EnemyContainer _target;
    private bool expandWaves = false;

    void OnGUI()
    {

        if(_target != null)
        {
            expandWaves = EditorGUILayout.Foldout(expandWaves, "Waves");
            if(expandWaves)
            {
				if(GUILayout.Button("+"))
					_target.Curves.Add(new WaveCurve());
                foreach(var w in _target.Curves)
                {
                	GUILayout.BeginHorizontal();
                    w.ID = EditorGUILayout.IntField(w.ID, GUILayout.Width(200));
					w.Curve = EditorGUILayout.CurveField(w.Curve, GUILayout.Width(200));
                	GUILayout.EndHorizontal();
                }
            }
			GUILayout.BeginHorizontal();
			
			GUILayout.Label("ID", GUILayout.Width(20));
			//w.curve = EditorGUILayout.CurveField(w.curve) ;
			GUILayout.Label("enemy");
			GUILayout.Label("count");
			GUILayout.Label("interv");
			GUILayout.Label("spawnt");
			GUILayout.Label("curve");
			GUILayout.Label("rev");
			GUILayout.EndHorizontal();
            foreach(var w in _target.Waves)
            {
                GUILayout.BeginHorizontal();

				w.ID = EditorGUILayout.IntField(w.ID, GUILayout.Width(20));
                //w.curve = EditorGUILayout.CurveField(w.curve) ;
				w.enemy = (Enemy)EditorGUILayout.ObjectField(w.enemy, typeof(Enemy));
				w.count = EditorGUILayout.IntField(w.count);
                w.spawnInterval = EditorGUILayout.FloatField( w.spawnInterval);
                w.spawnTime = EditorGUILayout.FloatField( w.spawnTime);
				w.CurveID = EditorGUILayout.IntPopup(w.CurveID, _target.Curves.Select (m => m.ID.ToString()).ToArray(),_target.Curves.Select (m => m.ID).ToArray());
				w.reversed = EditorGUILayout.Toggle(w.reversed);                       

                GUILayout.EndHorizontal();
            }

        }
        else
        {
            _target = AssetDatabase.LoadAssetAtPath("Assets/Resources/wave.asset", typeof(EnemyContainer)) as EnemyContainer; //Selection.activeObject as EnemyContainer;
        }

        /*if(GUILayout.Button("Ny container"))
        {
            var newInstance = EnemyContainer.CreateInstance<EnemyContainer>();
            var path = "Assets/";
            AssetDatabase.CreateAsset(newInstance, path + "wave.asset");
        }*/

    }
}
