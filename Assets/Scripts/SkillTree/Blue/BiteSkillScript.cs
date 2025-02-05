using UnityEngine;

public class BiteSkillScript : AbstractSkill
{
    public void ClickOnButton()
    {

    }
    public void Update()
    {
        if (activePlayer.human&&thisListen)
        {
            
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {

                StopListening();
                
            }
            if (Input.GetMouseButtonDown(0))
            {


                Do(clickedTileObject, clickedtileColor);


            }
        }
    }


}
