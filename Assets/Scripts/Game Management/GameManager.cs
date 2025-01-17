using Menu;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace Manager
{
    public enum Aesthetic
    {
        Egyptian,
        Noir,
        Synthwave,
        Scifi,
        end
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] public PlayerStats playerStats;
        [SerializeField] private UIManager uiManager;

        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private SceneLoader sceneLoader;
        
        [SerializeField] private GameObject synthToSciFi;
        [SerializeField] private GameObject noirToSynth;
        [SerializeField] private GameObject noirSong;
    

        public event Action nextLevel;
        public event Action SetInmortalState;
        public event Action CallPortal;
        public event Action ExitPortal;

        private bool inTutorial;
        public bool InTutorial { get => inTutorial; set => inTutorial = value; }

        private Aesthetic currentAesthetic;
        internal Aesthetic CurrentAesthetic { get => currentAesthetic; set => currentAesthetic = value; }

        private void Start()
        {
            playerStats.collectedObjectsEgypt = 0;
            playerStats.collectedObjectsNoir = 0;
            playerStats.collectedObjectsSynthwave = 0;
            playerStats.collectedObjectsSpace = 0;
            InTutorial = true;
            //playerStats.isEndlessAvailable = false;
            //playerStats.isStoryModeFinished = false;

            stateMachine = new StateMachine();
            stateMachine.AddState<PauseState>(new PauseState(stateMachine, this));
            stateMachine.AddState<PortalState>(new PortalState(stateMachine, this, uiManager, noirSong, noirToSynth, synthToSciFi));
            stateMachine.AddState<EndState>(new EndState(stateMachine, sceneLoader, 3, this));

            stateMachine.AddState<TutorialState>(new TutorialState(stateMachine, this, uiManager, noirSong, noirToSynth));
            stateMachine.AddState<EgyptianState>(new EgyptianState(stateMachine, this, noirSong));
            stateMachine.AddState<NoirState>(new NoirState(stateMachine, this, noirSong));
            stateMachine.AddState<SynthwaveState>(new SynthwaveState(stateMachine, this, synthToSciFi));
            stateMachine.AddState<SciFiState>(new SciFiState(stateMachine, this));

            stateMachine.ChangeState<PortalState>();
        }
        private void Update()
        {
            stateMachine.Update();
        }

        public void ReloadScene()
        {
            Time.timeScale = 1;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        public void CallNextLevel()
        {
            nextLevel.Invoke();
        }
        public void CallInmortalState()
        {
            SetInmortalState?.Invoke();
        }

        public void CallPortalState()
        {
            CallPortal?.Invoke();
        }

        public void ExitPortalState()
        {
            ExitPortal?.Invoke();
        }
    }
}