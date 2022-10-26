using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The class stores information about the user's records
/// </summary>
[CreateAssetMenu(fileName = "Scores", menuName = "ScoresDB", order = 0)]
public class Scores : ScriptableObject
{
    /// <summary>
    /// List of user records
    /// </summary>
    public List<int> scores = new List<int>();
}