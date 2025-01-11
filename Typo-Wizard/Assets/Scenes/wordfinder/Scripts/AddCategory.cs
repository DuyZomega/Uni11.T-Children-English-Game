using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameLevelData;
using static UnityEngine.GraphicsBuffer;

public class AddCategory : MonoBehaviour
{
    public Text categoryText;
    public Image progressBarFilling;



    // Start is called before the first frame update
    private void Start()
    {
        ConfigurePrefab();

    }
    private void Update()
    {
    }
    public GameLevelData ConfigurePrefab()
    {
        var dataFromDataBase = AccountManager._gameData;
        var testCategory = SelectPuzzleButton.cloneLevelData.data;
        var listBoardData = new List<BoardData>();
        //Stack<string> testArray = new Stack<string>();
        //int numberOfRows = 0;
        //foreach (var pair in dataFromFireBase["cate1"])
        //{
        //    foreach (var text in pair.Words)
        //    {
        //        var searchWord = new BoardData.SearchingWord();
        //        searchWord.Found = false;
        //        searchWord.Word = text.Word;
        //        listSearchWord.Add(searchWord);
        //        testArray.Push(text.Word);
        //    }
        //    numberOfRows = pair.Row;
        //}
        //char[,] matrix = GenerateWordMatrix(testArray, numberOfRows);

        foreach (var pair in dataFromDataBase["cate1"])
        {
            Stack<string> testArray = new Stack<string>();
            var listSearchWord = new List<BoardData.SearchingWord>();

            foreach (var text in pair.Words)
            {
                var searchWord = new BoardData.SearchingWord();
                searchWord.Found = false;
                searchWord.Word = text.Word.ToUpper();
                listSearchWord.Add(searchWord);
                testArray.Push(text.Word.ToUpper());

            }
            char[,] matrix = GenerateWordMatrix(testArray, pair.Row);
            int boardRowInt = 0;
            var listBoardRow = new BoardData.BoardRow[pair.Row];

            for (int i = 0; i < pair.Row; i++)
            {
                char[] myArray = new char[pair.Row];

                for (int j = 0; j < pair.Row; j++)
                {
                    myArray[j] = matrix[i, j];

                }
                string[] converToStringArray = new string[pair.Row];
                for (int z = 0; z < pair.Row; z++)
                {
                    converToStringArray[z] = myArray[z].ToString();
                }
                BoardData.BoardRow boardRow = new BoardData.BoardRow()
                {
                    Row = converToStringArray,
                    Size = 10
                };
                listBoardRow[boardRowInt] = boardRow;
                boardRowInt++;
            }


            BoardData databaseBoardData = new BoardData()
            {
                name = "cate1",
                Columns = pair.Column,
                Rows = pair.Row,
                timeInSeconds = 180,
                SearchWords = listSearchWord,
                Board = listBoardRow
            };


           

            listBoardData.Add(databaseBoardData);
        }


        //for (int i = 0; i < numberOfRows; i++)
        //{
        //    int boardRowInt = 0;
        //    var listBoardRow = new BoardData.BoardRow[numberOfRows];
        //    char[] myArray = new char[numberOfRows];

        //    for (int j = 0; j < numberOfRows; j++)
        //    {
        //        myArray[i] = matrix[i, j];

        //    }
        //    string[] converToStringArray = new string[numberOfRows];
        //    for (int z = 0; z < numberOfRows; z++)
        //    {
        //        converToStringArray[z] = myArray[z].ToString();
        //    }
        //    BoardData.BoardRow boardRow = new BoardData.BoardRow()
        //    {
        //        Row = converToStringArray,
        //        Size = 10
        //    };
        //    listBoardRow[boardRowInt] = boardRow;
        //    boardRowInt++;
        //}
        //BoardData firebaseBoardData = new BoardData()
        //{
        //    name = "cate1",
        //    Columns = pair.Column,
        //    Rows = pair.Row,
        //    timeInSeconds = 180,
        //    SearchWords = listSearchWord,
        //    Board = listBoardRow
        //};
        //listBoardData.Add(firebaseBoardData);

        CategoryRecord categoryRecord = new CategoryRecord()
        {
            categoryName = "cate1",
            boardData = listBoardData
        };

        SelectPuzzleButton.cloneLevelData.data.Add(categoryRecord);
        return SelectPuzzleButton.cloneLevelData;


    }
    char[,] GenerateWordMatrix(Stack<string> words, int matrixSize)
    {

        char[,] matrix = new char[matrixSize, matrixSize];

        // Initialize the matrix with '.'
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                matrix[i, j] = '.';
            }
        }

        // Place the words in the matrix
        while (words.Count > 0)
        {
            PlaceWord(matrix, words.Pop(), matrixSize);
        }
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                if (matrix[i, j] == '.')
                {
                    int index = UnityEngine.Random.Range(0, letters.Length);

                    matrix[i, j] = letters[index];
                }
               ;
            }
        }
        return matrix;
    }

    void PlaceWord(char[,] matrix, string word, int matrixSize)
    {
        int length = word.Length;
        int row, col;

        while (true)
        {
            row = UnityEngine.Random.Range(0, matrixSize);
            col = UnityEngine.Random.Range(0, matrixSize - length + 1);

            bool canPlace = true;
            for (int i = 0; i < length; i++)
            {
                if (matrix[row, col + i] != '.')
                {
                    canPlace = false;
                    break;
                }
            }

            if (canPlace)
            {
                for (int i = 0; i < length; i++)
                {
                    matrix[row, col + i] = word[i];
                }
                return;

            }
        }
    }
}
