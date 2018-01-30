using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonStatus : MonoBehaviour {

    public Image thisImage;

    public bool status;
    public int levelIndex;

    public Sprite activeSprite;
    public Sprite deactiveSprite;

	
	void Start ()
    {
        thisImage = this.GetComponent<Image>();

        SetButtonStatus(status);
	}
	

    public void SetButtonStatus(bool isActive)
    {
        if (isActive)
        {
            thisImage.sprite = activeSprite;
        }
        else
            thisImage.sprite = deactiveSprite;
    }

    public void ChangeToScene ()
    {
        if(status == true)
            SceneManager.LoadScene(levelIndex);
        else
            return;
    }

}
