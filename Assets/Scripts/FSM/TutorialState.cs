using Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialState : State
{
    public TutorialState(StateMachine machine, GameManager gameManager, int distance) : base(machine)
    {
        this.gameManager = gameManager;
        conditions.Add(typeof(EndLevel), new EndLevel(distance, gameManager.playerStats));
    }

    public override void Enter()
    {
        Debug.LogWarning("Enter: TUTORIAL :: State");
        gameManager.CurrentAesthetic = Aesthetic.Noir;
        gameManager.playerStats.distanceTraveled = 0;
    }

    public override void Exit()
    {
        Debug.LogWarning("Exit: TUTORIAL :: State");
        gameManager.InTutorial = false;
    }

    public override void Update()
    {

        if (CheckCondition<EndLevel>())
        {
            machine.ChangeState<PortalState>();
            return;
        }
    }

}