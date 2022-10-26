using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Database class stores information about the user's records
/// </summary>
public class Database : MonoBehaviour
{
    public static Database instance { get; private set; }
    [SerializeField] private Scores _scoresDatabase;

    #region Singleton

    private void InitializeDatabase()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    #endregion

    private void Awake()
    {
        InitializeDatabase();

        if (_scoresDatabase == null)
            Debug.LogError("The records database object is missing!");
    }

    /// <summary>
    /// Clearing the user's record history
    /// </summary>
    public void ClearScoresDatabase()
    {
        _scoresDatabase.scores.Clear();
    }

    /// <summary>
    /// Adding a new record to the history of records.
    /// </summary>
    /// <param name="score"></param>
    public void AddNewScoreDatabase(int score)
    {
        if (_scoresDatabase.scores.Contains(score))
            return;

        _scoresDatabase.scores.Add(score);
        _scoresDatabase.scores.Sort();

        if (_scoresDatabase.scores.Count >5)
            _scoresDatabase.scores.RemoveAt(0);
    }

    /// <summary>
    /// Getting all the user's records
    /// </summary>
    /// <returns></returns>
    public List<int> GetScoresDatabase()
    {
        _scoresDatabase.scores.Sort();
        return _scoresDatabase.scores;
    }
}