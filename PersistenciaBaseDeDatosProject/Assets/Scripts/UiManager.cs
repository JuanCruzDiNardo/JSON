using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public Material Mat0;
    public Material Mat1;
    public Material Mat2;
    public Material Mat3;
    public TextMeshProUGUI health;
    public TextMeshProUGUI Level;
    public Player Player;
    public TMP_Dropdown TMP_Dropdown;
    public Slider slider;
    public Slider slider2;

    public void Start()
    {
        slider.minValue = 1;
        slider.maxValue = 100;

        slider2.minValue = 100;
        slider2.maxValue = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Player.level;
        slider.onValueChanged.AddListener(UpdateIntValue);

        slider2.value = Player.health;
        slider2.onValueChanged.AddListener(UpdateIntValue2);

        switch (TMP_Dropdown.value)
        {
            case 0:
                Player.GetComponent<Renderer>().material = Mat1;
                break;
            case 1:
                Player.GetComponent<Renderer>().material = Mat2;
                break;
            case 2:
                Player.GetComponent<Renderer>().material = Mat3;
                break;
            default:
                Player.GetComponent<Renderer>().material = Mat0;
                break;
        }

        Ui(Player);
    }

    public void UpdateIntValue(float value)
    {
        Player.level = Mathf.RoundToInt(value);
    }

    public void UpdateIntValue2(float value)
    {
        Player.health = Mathf.RoundToInt(value);
    }

    public void Ui(Player player)
    {
        if (player != null)
        {
            Level.text = "Nivel: " + player.level.ToString();
            health.text = "Vida: " + player.health.ToString();
        }
        else
        {
            Level.text = "xxxxxxx";
            health.text = "xxxxxxx";
        }
    }
}
