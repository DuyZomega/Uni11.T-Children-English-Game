//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System;
//using UnityEditorInternal;
//using System.Text.RegularExpressions;
//using System.Linq;

//[CustomEditor(typeof(BoardData), false)]
//[CanEditMultipleObjects]
//[Serializable]
//public class BoardDataDrawer : Editor
//{
//    private BoardData GameDataInstance => target as BoardData;
//    private ReorderableList _dataList;

//    private void OnEnable()
//    {
//        InitializeReordableList(ref _dataList, "SearchWords", "Searching Words");
//    }
//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        GameDataInstance.timeInSeconds = 
//            EditorGUILayout.FloatField("Max Game Time (in seconds)", GameDataInstance.timeInSeconds);
//        DrawColumnsRowsInputFields();
//        EditorGUILayout.Space();
//        ConvertToUpperButton();

//        if (GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
//        {
//            DrawBoardTable();
//        }

//        GUILayout.BeginHorizontal();

//        ClearBoardButton();
//        FillUpWithRandomLettersButton();

//        GUILayout.EndHorizontal();

//        EditorGUILayout.Space();
//        _dataList.DoLayoutList();

//        serializedObject.ApplyModifiedProperties();
//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(GameDataInstance);
//        }
//    }

//    private void DrawColumnsRowsInputFields()
//    {
//        var columnsTemp = GameDataInstance.Columns;
//        var rowsTemp = GameDataInstance.Rows;

//        GameDataInstance.Columns = EditorGUILayout.IntField("Columns", GameDataInstance.Columns);
//        GameDataInstance.Rows = EditorGUILayout.IntField("Rows", GameDataInstance.Rows);

//        if (GameDataInstance.Columns !=  columnsTemp || GameDataInstance.Rows != rowsTemp 
//            && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
//        {
//            GameDataInstance.CreateNewBoard();
//        }
//    }

//    private void DrawBoardTable()
//    {
//        var tableStyle = new GUIStyle("box");
//        tableStyle.padding = new RectOffset(10, 10, 10, 10);
//        tableStyle.margin.left = 32;

//        var headerColumnStyle = new GUIStyle();
//        headerColumnStyle.fixedWidth = 35;

//        var columnStyle = new GUIStyle();
//        columnStyle.fixedWidth = 50;

//        var rowStyle = new GUIStyle();
//        rowStyle.fixedHeight = 25;
//        rowStyle.fixedWidth = 40;
//        rowStyle.alignment = TextAnchor.MiddleCenter;

//        var textFieldStyle = new GUIStyle();
//        textFieldStyle.normal.background = Texture2D.grayTexture;
//        textFieldStyle.normal.textColor = Color.white;
//        textFieldStyle.fontStyle = FontStyle.Bold;
//        textFieldStyle.alignment = TextAnchor.MiddleCenter;

//        EditorGUILayout.BeginHorizontal(tableStyle);
//        for (int x = 0; x < GameDataInstance.Columns; x++)
//        {
//            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
//            for (int y = 0; y < GameDataInstance.Rows; y++)
//            {
//                if (x >= 0 && y >= 0)
//                {
//                    EditorGUILayout.BeginHorizontal(rowStyle);
//                    var character = EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFieldStyle);
//                    if (GameDataInstance.Board[x].Row[y].Length > 1)
//                    {
//                        character = GameDataInstance.Board[x].Row[y].Substring(0, 1);
//                    }
//                    GameDataInstance.Board[x].Row[y] = character;
//                    EditorGUILayout.EndHorizontal();
//                }
//            }
//            EditorGUILayout.EndVertical();
//        }
//        EditorGUILayout.EndHorizontal();
//    }

//    private void InitializeReordableList(ref ReorderableList list, string propertyName, string listLabel)
//    {
//        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true, true, true);

//        list.drawHeaderCallback = rect =>
//        {
//            EditorGUI.LabelField(rect, listLabel);
//        };

//        var l = list;

//        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
//        {
//            var element = l.serializedProperty.GetArrayElementAtIndex(index);
//            rect.y += 2;

//            EditorGUI.PropertyField(
//                new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("word"), GUIContent.none);
//        };
//    }

//    private void ConvertToUpperButton()
//    {
//        if (GUILayout.Button("To Upper"))
//        {
//            for (int i = 0; i < GameDataInstance.Columns; i++)
//            {
//                for (int j = 0; j < GameDataInstance.Rows; j++)
//                {
//                    var errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-z]").Count;
                    
//                    if (errorCounter > 0)
//                    {
//                        GameDataInstance.Board[i].Row[j] = GameDataInstance.Board[i].Row[j].ToUpper();
//                    }
//                }
//            }
//            foreach (var searchWord in GameDataInstance.SearchWords)
//            {
//                var errorCounter = Regex.Matches(searchWord.word, @"[a-z]").Count;

//                if (errorCounter > 0)
//                {
//                    searchWord.word = searchWord.word.ToUpper();
//                }
//            }
//        }
//    }

//    private void ClearBoardButton()
//    {
//        if (GUILayout.Button("Clear Board"))
//        {
//            for (int i = 0; i < GameDataInstance.Columns; i++)
//            {
//                for (int j = 0; j < GameDataInstance.Rows; j++)
//                {
//                    GameDataInstance.Board[i].Row[j] = " ";
//                }
//            }
//        }
//    }

//    private void FillUpWithRandomLettersButton()
//    {
//        if (GUILayout.Button("Fill up With Random"))
//        {
//            for (int i = 0; i < GameDataInstance.Columns; i++)
//            {
//                for (int j = 0; j < GameDataInstance.Rows; j++)
//                {
//                    int errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-zA-z]").Count;
//                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//                    int index = UnityEngine.Random.Range(0, letters.Length);

//                    if (errorCounter == 0)
//                    {
//                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();
//                    }
//                }
//            }
//        }
//    }
//}
