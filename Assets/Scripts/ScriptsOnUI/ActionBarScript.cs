using UnityEngine;

public class ActionBarScript :MonoBehaviour
{
    public GameObject ActionBar;
    public void OnEnable()
    {
        PlayerManager.HumanPlayerBroadcast += ChangeVisibilty;
    }
    public void OnDisable()
    {
        PlayerManager.HumanPlayerBroadcast -= ChangeVisibilty;
    }
    private void ChangeVisibilty(bool visible)
    {
        ActionBar.SetActive(visible);
    }
}
