using UnityEngine;
using System.Collections;
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
                foreach(var w in _target.Curves)
                {
                    w.ID = EditorGUILayout.IntField(w.ID);
                    w.Curve = EditorGUILayout.CurveField(w.Curve);
                }
            }

            foreach(var w in _target.Waves)
            {
                GUILayout.BeginHorizontal();

                GUILayout.TextField(w.ID.ToString(), 4);
                //w.curve = EditorGUILayout.CurveField(w.curve) ;
                EditorGUILayout.ObjectField(w.enemy, typeof(Enemy));
                EditorGUILayout.FloatField( w.spawnInterval);
                EditorGUILayout.FloatField( w.spawnTime);
                GUILayout.EndHorizontal();
            }

        }
        else
        {
            _target = AssetDatabase.LoadAssetAtPath("Assets/wave.asset", typeof(EnemyContainer)) as EnemyContainer; //Selection.activeObject as EnemyContainer;
        }

        /*if(GUILayout.Button("Ny container"))
        {
            var newInstance = EnemyContainer.CreateInstance<EnemyContainer>();
            var path = "Assets/";
            AssetDatabase.CreateAsset(newInstance, path + "wave.asset");
        }*/

    }
}
