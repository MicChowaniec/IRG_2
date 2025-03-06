using UnityEngine;

public class ParametersScript : MonoBehaviour
{
    public GameSettings gs;

    public void UpdateMapSize(int mapSize)
    {
        gs.SizeOfMap = mapSize;

    }
    public void UpdateCyclesPerDay(int cyclesPerDay)
    {
        gs.CyclesPerDay = cyclesPerDay;
    }
    public void UpdateDifficultyLevel(int difficultyLevel)
    {
        gs.Difficulty = difficultyLevel;
    }

}
