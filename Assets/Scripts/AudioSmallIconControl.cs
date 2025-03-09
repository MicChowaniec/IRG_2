
using UnityEngine;
using UnityEngine.UI;

public class AudioSmallIconControl : MonoBehaviour
{
    public Sprite SoundOn;
    public Sprite SoundOff;

    public Image image;
    public GameObject audioS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnEnable()
    {
        image = GetComponent<Image>();
        if (audioS.activeSelf)
        {

            image.sprite = SoundOn;
        }
        else
        {
            image.sprite = SoundOff;
        }
    }
    public void OnClick()
    {
        if(audioS.activeSelf)
        {
            audioS.SetActive(false);
            image.sprite = SoundOff;
            
        }
        else
        {
            audioS.SetActive(true);
            image.sprite = SoundOn;
        }
    }

  
}
