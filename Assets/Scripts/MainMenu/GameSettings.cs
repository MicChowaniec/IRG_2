using UnityEngine;


//This file will pass the settings to the game
[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    int SizeOfMap = 6;
    int CyclesPerDay = 2;
    int Difficulty = 1;
    bool RootingOnlyNextToSelf = false;
}
