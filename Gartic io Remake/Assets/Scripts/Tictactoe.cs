using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tictactoe : MonoBehaviour
{
    public Sprite square, triangle, nullSprite;
    public Button firstButton, secondButton, thirdButton, fourthButton;

    Sprite spriteType;
    Button buttonType;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteType();
    }

    public void Button1()
    {
        buttonType = firstButton;
        buttonType.GetComponent<Image>().sprite = spriteType;
        spriteType = nullSprite;
    }

    public void Button2() 
    {
        buttonType = secondButton;
        buttonType.GetComponent<Image>().sprite = spriteType;
        spriteType = nullSprite;
    }

    public void Button3()
    {
        buttonType = thirdButton;
        buttonType.GetComponent<Image>().sprite = spriteType;
        spriteType = nullSprite;
    }

    public void Button4()
    {
        buttonType = fourthButton;
        buttonType.GetComponent<Image>().sprite = spriteType;
        spriteType = nullSprite;
    }


    public void SpriteType()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            spriteType = square;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            spriteType = triangle;
        }
    }
}
