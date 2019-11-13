using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scoreboard", menuName = "Game/Settings/ScoreBoard")]
[System.Serializable]
public class ScoreBoard : ScriptableObject
{
    public int highScore;
    public int lives;
    public int score;
}
