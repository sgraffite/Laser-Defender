using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private int score = 0;
    private Text textComponent;

    public void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void Add(int points)
    {
        score += points;
        textComponent.text = GetScore();
    }

    public string GetScore()
    {
        return score.ToString("000000000");
    }

    public void Reset()
    {
        score = 0;
    }
}
