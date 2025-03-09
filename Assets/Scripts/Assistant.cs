using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Assistant : MonoBehaviour
{
    private string[] narration = new string[11];
    private string[] responses = new string[11];
    public TextMeshProUGUI AssistantTalks;
    public TextMeshProUGUI PlayerTalks;
    public Button btn;
    public int scenario;
    public Canvas canvas;
    public void Start()
    {
        
        narration[0] = "Hello!";
        responses[0] = "Hello?";

        narration[1] = "Welcome in Eve World! I am The Starling, your SoulMate. I will guide you through basics of your beeing";
        responses[1] = "Sounds good";

        narration[2] = "First off all, you are Leuk, hybrid of tree and lizard, or something similar.";
        responses[2] = "Leuk?";

     //   narration[3] = "Yes. Unfortunately, you live only one day. Your mission is to root on a good spot, and evolve, so your descendands, have better life";
     //   responses[3] = "one day? Sounds depressing...";

      //  narration[3] = "Nothing could be further from the truth. You will become something more magnificent. You will become a Tree, and live... well, long.";
     //   responses[3] = "Okay.";

        //narration[5] = "Well, so as I said before, you live only one day. You are partially a plant, and partially an animal, so you can do photosynthesis, but also can move and eat.";
        //responses[5] = "Okay...";

        narration[3] = "Your goal is to plant as many trees as possible to beat other Leuks";
        responses[3] = "How to plant?";

        narration[4] = "You need to use root action. You can hover any action from panel below, to see their effect and costs.";
        responses[4] = "Costs?";

        narration[5] = "Yes, everything costs energy and water, sometimes even an eye.";
        responses[5] = "Eye?";

        narration[6] = "Yes, but don't worry, you may sometimes get a new one. Water you can refill on map, and energy would refill itself.";
        responses[6] = "Okay....";

        narration[7] = "Okay there are several rules: you can't root next to other tree, you can't step into the water, and you can't root on rock.";
        responses[7] = "Next";

        narration[8] = "To use additional action, drag one of your skills to left White Icon - to move, or to photosythetise  - to right Green Icon";
        responses[8] = "Drag...";

        narration[9] = "You may use WSAD keys or to scroll the map, or QE to change angle. If you loose yourself, press SPACEBAR to center on your character ";
        responses[9] = "Next";

        narration[10] = "Good Luck.";
        responses[10] = "Thanks";

    }
    public void NextScenario()
    {
        scenario++;

        // Ensure scenario does not exceed the bounds of the array
        if (scenario >= narration.Length)
        {
            canvas.enabled = false;
        }
        else 
        {
            AssistantTalks.text = narration[scenario];
            btn.enabled = true;
            PlayerTalks.text = responses[scenario];

        } 
    }
}
