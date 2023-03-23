using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulettepiece : MonoBehaviour
{
   [SerializeField]  private Image imageIcon;

    [SerializeField] private Text textDescription;

    public void Setup(RoulettePieceData roulette)
    {
        imageIcon.sprite = roulette.icon;
        textDescription.text = roulette.description;
    }
}
