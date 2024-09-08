using UnityEditor;
using UnityEngine;
using System.Threading;

public class EditorTest : EditorWindow
{
    public float secs = 5f;
    [MenuItem("Examples/Progress Bar Usage")]
    static void Init()
    {
        var window = GetWindow(typeof(EditorUtility));
        window.Show();
    }

    void OnGUI()
    {
        secs = EditorGUILayout.Slider("Time to wait:", secs, 1.0f, 20.0f);
        if (GUILayout.Button("Display bar"))
        {
            var step = 0.1f;
            for (float t = 0; t < secs; t += step)
            {
                EditorUtility.DisplayProgressBar("Simple Progress Bar", "Doing some work...", t / secs);
                // Normally, some computation happens here.
                // This example uses Sleep.
                Thread.Sleep((int)(step * 1000.0f));
            }
            EditorUtility.ClearProgressBar();
        }
    }
}
