using System;
using UnityEngine;

namespace UltimateCC
{
    /// <summary>
    /// Clase principal del jugador. 
    /// Se encarga de inicializar y controlar la m�quina de estados del personaje,
    /// manejar sus componentes principales (Animator, Rigidbody2D, Input, Collider)
    /// y actualizar la l�gica de movimiento seg�n el estado actual.
    /// </summary>
    public class PlayerMain : MonoBehaviour
    {
        private PlayerStateMachine _stateMachine; // M�quina de estados, controla en qu� estado se encuentra el jugador.

        [NonEditable, Space(5)]
        public AnimName CurrentState; // Se muestra en el inspector para depuraci�n, indica el estado actual.

        // Declaraci�n de todos los estados posibles del jugador:
        public MainState IdleState, WalkState, JumpState, LandState, DashState,
                         CrouchIdleState, CrouchWalkState,
                         WallGrabState, WallClimbState, WallJumpState, WallSlideState;

        // Enum con los nombres de los estados. 
        // Estos valores se usan como par�metros para el Animator.
        public enum AnimName
        {
            Idle, Walk, Jump, ExtraJump1, ExtraJump2, Land, Dash,
            CrouchIdle, CrouchWalk, WallGrab, WallClimb, WallJump, WallSlide
        }

        // --- Referencias a componentes principales ---
        [NonSerialized] public Animator Animator;           // Controla las animaciones seg�n el estado actual.
        [NonSerialized] public Rigidbody2D Rigidbody2D;     // Controla el movimiento f�sico con f�sica 2D.
        [NonSerialized] public PlayerInputManager InputManager; // Gestiona toda la entrada del jugador (teclas, mando, etc.).
        [NonSerialized] public CapsuleCollider2D CapsuleCollider2D; // Define colisiones y se usa en chequeos de suelo y pendientes.

        // Objeto que contiene todos los datos configurables del jugador (velocidades, saltos, etc.).
        public PlayerData PlayerData;

        private void Awake()
        {
            // --- Inicializaci�n de referencias a componentes ---
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            InputManager = GetComponent<PlayerInputManager>();
            CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

            // --- Inicializaci�n de la m�quina de estados ---
            _stateMachine = new PlayerStateMachine();

            // Se crean las instancias de cada estado con sus datos.
            IdleState = new PlayerIdleState(this, _stateMachine, AnimName.Idle, PlayerData);
            WalkState = new PlayerWalkState(this, _stateMachine, AnimName.Walk, PlayerData);
            JumpState = new PlayerJumpState(this, _stateMachine, AnimName.Jump, PlayerData);
            LandState = new PlayerLandState(this, _stateMachine, AnimName.Land, PlayerData);
            //DashState     = new PlayerDashState(this, _stateMachine, AnimName.Dash, PlayerData); // Desactivado en este ejemplo
            CrouchIdleState = new PlayerCrouchIdleState(this, _stateMachine, AnimName.CrouchIdle, PlayerData);
            CrouchWalkState = new PlayerCrouchWalkState(this, _stateMachine, AnimName.CrouchWalk, PlayerData);
            WallGrabState = new PlayerWallGrabState(this, _stateMachine, AnimName.WallGrab, PlayerData);
            WallClimbState = new PlayerWallClimbState(this, _stateMachine, AnimName.WallClimb, PlayerData);
            WallJumpState = new PlayerWallJumpState(this, _stateMachine, AnimName.WallJump, PlayerData);
            WallSlideState = new PlayerWallSlideState(this, _stateMachine, AnimName.WallSlide, PlayerData);
        }

        private void Start()
        {
            // Se inicializa el estado inicial en Idle (quieto).
            _stateMachine.Initialize(IdleState);

            // --- Configuraci�n de curvas de movimiento ---
            // Cada acci�n (salto, ca�da, aterrizaje, etc.) tiene una curva de altura en PlayerData.
            // Para calcular la velocidad de movimiento, se necesita la derivada de esa curva.
            // Aqu� se calcula y asigna la curva de velocidad a cada acci�n.

            PlayerData.Jump.Jumps[0].JumpVelocityCurve = PlayerData.Jump.Jumps[0].JumpHeightCurve.Derivative();

            foreach (var jump in PlayerData.Jump.Jumps)
            {
                jump.JumpVelocityCurve = jump.JumpHeightCurve.Derivative();
            }

            PlayerData.Land.LandVelocityCurve = PlayerData.Land.LandHeightCurve.Derivative();
            //PlayerData.Dash.DashYVelocityCurve = PlayerData.Dash.DashHeightCurve.Derivative(); // Desactivado
            PlayerData.Walls.WallJump.JumpVelocityCurve = PlayerData.Walls.WallJump.JumpHeightCurve.Derivative();
        }

        private void Update()
        {
            // Llama al m�todo Update() del estado actual.
            // Aqu� se maneja la l�gica que depende del frame rate (input, animaciones, etc.).
            _stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            // Llama al m�todo FixedUpdate() del estado actual.
            // Aqu� se maneja la l�gica dependiente de la f�sica (movimiento, colisiones, etc.).
            _stateMachine.CurrentState.FixedUpdate();
        }
    }
}
