using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image healthMask;
    public Image manaMask;
    //public GameObject hero;
    //public float originalSizeHealth;
    //public float originalSizeMana;

    //Currently not attached to a gameobject

    void Awake()
    {
        instance = this;
    }

    /*void Start()
    {
        originalSizeHealth = healthMask.rectTransform.rect.width;
        originalSizeMana = manaMask.rectTransform.rect.width;
    }*/

    public void SetValueHealth(float value)
    {
        //healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeHealth * value);

        //healthMask.fillAmount = Player.instance.currentHealth / Player.instance.maxHealth;
        healthMask.fillAmount = value;
    }

    public void SetValueMana(float value)
    {
        //manaMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSizeMana * value);
        //manaMask.fillAmount = Player.instance.currentMana / Player.instance.maxMana;
        manaMask.fillAmount = value;
    }

}
