using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuAesthetic
{
    Noir,
    Synthwave,
    Scifi,
    end
}

public class MenuAestheticManager : MonoBehaviour
{
    private MenuAesthetic currentAesthetic;
    internal MenuAesthetic CurrentAesthetic { get => currentAesthetic; set => currentAesthetic = value; }

    private MenuAesthetic previousAesthetic;
    internal MenuAesthetic PreviousAesthetic { get => previousAesthetic; set => previousAesthetic = value; }

    [SerializeField] private MenuInputManger menuInputManger;

    public event Action<MenuAesthetic, MenuAesthetic> menuAestheticChanged;

    private void OnEnable()
    {
        menuInputManger.moveCameraLeft += CalculateAestheticLeft;
        menuInputManger.moveCameraRight += CalculateAestheticRight;
    }

    private void OnDisable()
    {
        menuInputManger.moveCameraLeft -= CalculateAestheticLeft;
        menuInputManger.moveCameraRight -= CalculateAestheticRight;
    }

    void Awake()
    {
        ChangeAesthetic(MenuAesthetic.Noir);
    }

    private void CalculateAestheticLeft()
    {
        if (currentAesthetic == MenuAesthetic.Synthwave)
            ChangeAesthetic(MenuAesthetic.Noir);
        else if (currentAesthetic == MenuAesthetic.Scifi)
            ChangeAesthetic(MenuAesthetic.Synthwave);
    }

    private void CalculateAestheticRight()
    {
        if (currentAesthetic == MenuAesthetic.Noir)
            ChangeAesthetic(MenuAesthetic.Synthwave);
        else if (currentAesthetic == MenuAesthetic.Synthwave)
            ChangeAesthetic(MenuAesthetic.Scifi);
    }

    public void ChangeAesthetic(MenuAesthetic newMenuAesthetic)
    {
        previousAesthetic = currentAesthetic;
        currentAesthetic = newMenuAesthetic;
        menuAestheticChanged?.Invoke(previousAesthetic, currentAesthetic);
    }
}
