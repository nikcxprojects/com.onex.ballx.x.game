using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [Header("Settings References")]
    [SerializeField] private Text _audioText;
    [SerializeField] private Text _musicText;

    [Header("Scores")]
    [SerializeField] private Transform _scoreWindowTransform;
    [SerializeField] private GameObject _scorePrefab;
    private List<GameObject> _scoreInitialied = new List<GameObject>();
    [SerializeField] private AudioClip _clickSound;

    public void PlaySound()
    {
        AudioManager.instance.PlayAudio(_clickSound);
    }

    private void Awake()
    {
        AudioLoader();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// The method includes the main menu
    /// </summary>
    public void Home()
    {
        if (_scoreInitialied.Count == 0)
            return;

        foreach (GameObject score in _scoreInitialied)
            Destroy(score);

        _scoreInitialied.Clear();
    }

    public void ViewScores()
    {
        List<int> scores = Database.instance.GetScoresDatabase();

        if (scores.Count == 0)
            return;

        for (int i = scores.Count - 1; i > -1; i--)
        {
            GameObject scoreObj = Instantiate(_scorePrefab, _scoreWindowTransform);
            scoreObj.GetComponent<RectTransform>().localScale = Vector3.one;

            Text scoreText = scoreObj.GetComponent<Text>();
            scoreText.text = $"{scores.Count - i}. {scores[i]} points";

            _scoreInitialied.Add(scoreObj);
        }
    }

    /// <summary>
    /// Clears information about records
    /// </summary>
    public void ClearScores()
    {
        foreach (GameObject score in _scoreInitialied)
            Destroy(score);

        _scoreInitialied.Clear();
    }

    #region Settings
    public void ChangeAudio()
    {
        if (PlayerPrefs.GetString(PrefsKey.audio) == "Enable")
        {
            PlayerPrefs.SetString(PrefsKey.audio, "Disable");
            AudioManager.instance.EnableAudio(false);
            _audioText.text = "SOUND: OFF";
        }
        else
        {
            PlayerPrefs.SetString(PrefsKey.audio, "Enable");
            AudioManager.instance.EnableAudio(true);
            _audioText.text = "SOUND: ON";
        }
    }

    public void ChangeMusic()
    {
        if (PlayerPrefs.GetString(PrefsKey.music) == "Enable")
        {
            PlayerPrefs.SetString(PrefsKey.music, "Disable");
            AudioManager.instance.EnableMusic(false);
            _musicText.text = "MUSIC: OFF";
        }
        else
        {
            PlayerPrefs.SetString(PrefsKey.music, "Enable");
            AudioManager.instance.EnableMusic(true);
            _musicText.text = "MUSIC: ON";
        }
    }

    private void AudioLoader()
    {
        if (!PlayerPrefs.HasKey(PrefsKey.audio)) PlayerPrefs.SetString(PrefsKey.audio, "Enable");
        if (!PlayerPrefs.HasKey(PrefsKey.vibration)) PlayerPrefs.SetString(PrefsKey.vibration, "Enable");
        if (!PlayerPrefs.HasKey(PrefsKey.music)) PlayerPrefs.SetString(PrefsKey.music, "Enable");

        if (PlayerPrefs.GetString(PrefsKey.audio) == "Enable")
        {
            AudioManager.instance.EnableAudio(true);
            _audioText.text = "SOUND: ON";
        }
        else
        {
            AudioManager.instance.EnableAudio(false);
            _audioText.text = "SOUND: OFF";
        }

        if (PlayerPrefs.GetString(PrefsKey.music) == "Enable")
        {
            AudioManager.instance.EnableMusic(true);
            _musicText.text = "MUSIC: ON";
        }
        else
        {
            AudioManager.instance.EnableMusic(false);
            _musicText.text = "MUSIC: OFF";
        } 
    }

    #endregion
}
