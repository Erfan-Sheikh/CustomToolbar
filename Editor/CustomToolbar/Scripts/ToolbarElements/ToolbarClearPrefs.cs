using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

[Serializable]
internal class ToolbarClearPrefs : BaseToolbarElement {
    private static GUIContent clearPlayerPrefsBtn;

    public override string NameInList => "[Button] Clear prefs";
    public override int SortingGroup => 4;


    public override void Init() {
        clearPlayerPrefsBtn = EditorGUIUtility.IconContent("SaveFromPlay");
        clearPlayerPrefsBtn.tooltip = "Clear player prefs";
    }

    protected override void OnDrawInList(Rect position) {

    }

    protected override void OnDrawInToolbar() {
        if (GUILayout.Button(clearPlayerPrefsBtn, ToolbarStyles.commandButtonStyle)) {
            PlayerPrefs.DeleteAll();
            var savePath = $"{Application.persistentDataPath}/StreamingAssets/";
            var localSavePath = "Assets/StreamingAssets/";
            ClearSaveDirectory(savePath);
            ClearSaveDirectory(localSavePath);
            Debug.Log("Clear Player Prefs");
        }
    }
	
    private void ClearSaveDirectory(string path)
    {
        // Define the directory path

        if (Directory.Exists(path))
        {
            // Delete all files in the directory
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            // Delete all subdirectories and their contents
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                Directory.Delete(directory, true); // Recursive deletion
            }
        }
        else
        {
            Debug.LogWarning($"Directory does not exist: {path}");
        }
    }
}