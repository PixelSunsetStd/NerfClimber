using System.Collections.Generic;
using HomaGames.HomaBelly;
using UnityEditor;
using UnityEngine;

public class HomaBellyLogWindow : HomaBellyBaseWindow
{
    private const float WINDOW_START_Y = 80f;

    private Vector2 logTraceScrollPosition;
    private List<KeyValuePair<HomaBellyEditorLog.Level, string>> logTrace = new List<KeyValuePair<HomaBellyEditorLog.Level, string>>();

    protected override void Draw(Rect windowPosition)
    {
        Vector2 originalIconSize = EditorGUIUtility.GetIconSize();
        EditorGUIUtility.SetIconSize(new Vector2(16, 16));

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        float logAreaXStartPosition = (windowPosition.width - HomaGamesStyle.DarkGreyBoxStyle.fixedWidth) / 2f;
        GUILayout.BeginArea(new Rect(logAreaXStartPosition, WINDOW_START_Y,
                                    HomaGamesStyle.DarkGreyBoxStyle.fixedWidth,
                                    HomaGamesStyle.DarkGreyBoxStyle.fixedHeight),
            HomaGamesStyle.DarkGreyBoxStyle);

        logTraceScrollPosition = GUILayout.BeginScrollView(logTraceScrollPosition,
            false, false);
        logTrace.Clear();
        logTrace.AddRange(HomaBellyEditorLog.LogTrace);
        if (logTrace != null)
        {
            foreach (KeyValuePair<HomaBellyEditorLog.Level, string> logEntry in logTrace)
            {
                Texture2D icon = null;
                if (logEntry.Key == HomaBellyEditorLog.Level.WARNING)
                {
                    icon = HomaGamesStyle.WarningIcon;
                }

                if (logEntry.Key == HomaBellyEditorLog.Level.ERROR)
                {
                    icon = HomaGamesStyle.ErrorIcon;
                }

                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(icon), GUILayout.Height(20), GUILayout.Width(20));
                GUILayout.Label($"{logEntry.Value}", HomaGamesStyle.LogLabelStyle);
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUIUtility.SetIconSize(originalIconSize);
    }

    protected override void OnVisibleFocus()
    {
        // NO-OP
    }
}
