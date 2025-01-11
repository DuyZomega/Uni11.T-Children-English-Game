using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[SerializeField]
[CreateAssetMenu]
public class ScoreboardData : ScriptableObject
{

    [Header("UserData")]
    public List<UserObject> users;

    async void Awake()
    {
    }

    //List<UserID, UserObject>
    /*
     create an object that have username, dictionary<Level, Score>
     */
    //public async Task<List<UserObject>> LoadUsersScoreBoard()
    //{
    //    try
    //    {
    ////Get data from users
    //var DBTask = await DBreference.Child("users").GetValueAsync();
    //List<UserObject> userObjects = new List<UserObject>();
    //if (DBTask.Value == null)
    //{
    //    return null;
    //}

    //userObjects = DBTask.Children.Select(userid =>
    //{
    //    return new UserObject
    //    {
    //        UserId = userid.Key,

    //        Username = userid.Child("username").Value.ToString(),

    //        CategoryScore = userid.Children
    //        .Where(category => category.Key != "username")
    //        .ToDictionary(
    //            category => category.Key,
    //           category => new UserScoreObject
    //           {
    //                LevelScore = category.Children.ToDictionary(
    //                    item => item.Key,
    //                    item => double.Parse(item.Child("score").Value.ToString())),
    //                ScoreOfCategory = 0
    //            })
    //    };
    //}).ToList();
    //return userObjects;
    //}
    //catch (System.Exception ex)
    //{

    //    throw new System.Exception(ex.Message);
    //}
    //}
}
