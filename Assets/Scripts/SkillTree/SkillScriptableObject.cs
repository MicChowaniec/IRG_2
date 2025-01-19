using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SkillScriptableObject", menuName = "Skill")]
public class SkillScriptableObject : OnHoverSC
{
    public ActionTypeEnum actionType;
    public bool RootedSkill;

    
    public GameObject ButtonPrefab;

    public int starling;
    public int biomass;
    public int water;
    public int energy;
    public int protein;
    public int resistance;
    public int eyes;

    public int range;
    public bool self;


    public string purplePassiveDescription;
    public string bluePassiveDescription;
    public string greenPassiveDescription;
    public string yellowPassiveDescription;
    public string orangePassiveDescription;
    public string redPassiveDescription;

    public bool purplePassive;
    public bool bluePassive;
    public bool greenPassive;
    public bool yellowPassive;
    public bool orangePassive;
    public bool redPassive; 


}
