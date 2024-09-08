using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Assistant : MonoBehaviour
{
    public string[] narration;
    public string[] responses;
    public TextMeshProUGUI AssistantTalks;
    public TextMeshProUGUI PlayerTalks;
    public Button btn;
    public int scenario;
    public void Start()
    {
        narration[0] = "Hello!";
        responses[0] = "Hello?";

        narration[1] = "Welcome in Eve World! I am The Starling, your SoulMate. I will guide you through basics of your beeing";
        responses[1] = "Sounds good";

        narration[2] = "First off all, you are Leuk, hybrid of sampling and lizard, or something similar.";
        responses[2] = "Leuk?";

        narration[3] = "Yes. Unfortunately, you live only one day. Your mission is to root on a good spot, and evolve, so your descendands, have better life";
        responses[3] = "one day? Sounds depressing...";

        narration[4] = "Nothing could be further from the truth. You will become something more magnificent. You will become a Tree, and live... well, long.";
        responses[4] = "Okay.";

        narration[5] = "Well, so as I said before, you live only one day. You are partially a plant, and partially an animal, so you can do photosynthesis, but also can move and eat.";
        responses[5] = "Okay...";

        narration[6] = "But you are very specific beeing, you cannot do two things at the same time, and each action use energy";
        responses[6] = "How can I restore energy?";

        narration[7] = "Well it is simple, you are partially plant, so the Sun will give you some. The higher is sun, the more energy you gain.";
        responses[7] = "So just wait for the energy from the Sun?";

        narration[8] = "You are also partially animal, so you can eat some fruits or other animals.";
        responses[8] = "Okay, lets eat something!";

        narration[9] = "Not so fast, as I said before, you cannot do two things at one moment, at this point you are talking to me, so you cannot move";
        responses[9] = "Continue...";

        narration[10] = "Hovewer, I can bring you some fruits if you want to. Do you see that bush with purple flowers? Click 'Scout' button on the purple field, and click on the bush to send me there";
        responses[10] = "";

        narration[11] = "As you may see, Starling counter is one less now. Actually it is zero. I hope you enjoyed your food";
        responses[11] = "Yes, thank you";

        narration[12] = "Ok lets go to the next turn, and you won't be able to talk to me, but you can listen";
        responses[12] = "";

        narration[13] = "Water is very important to all life, as you may know. You will use it regardless you do something or not, that is why this is very important to drink.";
        responses[13] = "'Listen'";

        narration[14] = "As long as you are not rooted into the ground, you must be nearby water to drink it. Fortunately, there is nice water tile, just next to you, use button 'Water' to drink";
        responses[14] = "";

        narration[15] = "Good! In this turn your body can grow, using photosynthesis. You can also grow by eating other things, when you are in Leuk stadium. Now press the button and see what happed";
        responses[15] = "";

        narration[16] = "Your body is bigger, you have more capacity for energy, and water, you are also stronger! Good! let's go to the next turn";
        responses[16] = "";

        narration[17] = "Now you can finally move. Use move button, and click on the fields to move. Try to go to this grass between water and desert.";
        responses[17] = "";

        narration[18] = "Okay good, lets go to the next round";
        responses[18] = "";

        narration[19] = "Now we are in Power Turn, you can attack something!, use this punch button, to attack nearby animal";
        responses[19] = "";
       
        narration[20] = "Now you are in the root turn. Be carefull. You can only root once per day... Yes it means to you once per life. You don't want to root somewhere where is no water.";
        responses[20] = "'Listen'";
    }
    public void NextScenario()
    {
        scenario++;

        // Ensure scenario does not exceed the bounds of the array
        if (scenario >= narration.Length)
        {
            scenario = narration.Length - 1; // Optionally, set it to the last valid index or handle as needed
            Debug.LogWarning("Scenario index out of range. Adjusting to maximum valid index.");
        }

        AssistantTalks.text = narration[scenario];

        if (responses[scenario] != "")
        {
            btn.enabled = true;
            PlayerTalks.text = responses[scenario];
        }
        else
        {
            btn.enabled = false;
        }
    }
}
