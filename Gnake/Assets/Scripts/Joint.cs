using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    White,
    Black,
    Red,
    Blue,
    Green,
    Yellow
}
public class Joint : MonoBehaviour
{
    public ColorType colorType;
    SpriteRenderer spriteRenderer;
    public Color white;
    public Color black;
    public Color red;
    public Color blue;
    public Color green;
    public Color yellow;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(ColorType col)
    {
        switch (col) 
        {
            case ColorType.White:
                colorType = ColorType.White;
                spriteRenderer.color = white;
                break;

            case ColorType.Black:
                colorType = ColorType.Black;
                spriteRenderer.color = black;
                break;
            case ColorType.Red:
                colorType = ColorType.Red;
                spriteRenderer.color = red;
                break;
            case ColorType.Blue:
                colorType = ColorType.Blue;
                spriteRenderer.color = blue;
                break;
            case ColorType.Green:
                colorType = ColorType.Green;
                spriteRenderer.color = green;
                break;
            case ColorType.Yellow:
                colorType = ColorType.Yellow;
                spriteRenderer.color = yellow;
                break;
        }
    }
}
