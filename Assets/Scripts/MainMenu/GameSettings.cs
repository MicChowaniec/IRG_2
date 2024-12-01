using UnityEngine;


//This file will pass the settings to the game
[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public int SizeOfMap = 6;
    public int CyclesPerDay = 2;
    public int Difficulty = 1;
    public bool RootingOnlyNextToSelf = false;
}
