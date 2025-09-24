using System;
using UnityEngine;

namespace UltimateCC
{
    /// <summary>
    /// Clase principal del jugador. 
    /// Se encarga de inicializar y controlar la máquina de estados del personaje,
    /// manejar sus componentes principales (Animator, Rigidbody2D, Input, Collider)
    /// y actualizar la lógica de movimiento según el estado actual.
    /// </summary>
    public class PlayerMain : MonoBehaviour
    {
        private PlayerStateMachine _stateMachine; // Máquina de estados, controla en qué estado se encuentra el jugador.

        [NonEditable, Space(5)]
        public AnimName CurrentState; // Se muestra en el inspector para depuración, indica el estado actual.

        // Declaración de todos los estados posibles del jugador:
        public MainState IdleState, WalkState, JumpState, LandState, DashState,
                         CrouchIdleState, CrouchWalkState,
                         WallGrabState, WallClimbState, WallJumpState, WallSlideState;

        // Enum con los nombres de los estados. 
        // Estos valores se usan como parámetros para el Animator.
        public enum AnimName
        {
            Idle, Walk, Jump, ExtraJump1, ExtraJump2, Land, Dash,
            CrouchIdle, CrouchWalk, WallGrab, WallClimb, WallJump, WallSlide
        }

        // --- Referencias a componentes principales ---
        [NonSerialized] public Animator Animator;           // Controla las animaciones según el estado actual.
        [NonSerialized] public Rigidbody2D Rigidbody2D;     // Controla el movimiento físico con física 2D.
        [NonSerialized] public PlayerInputManager InputManager; // Gestiona toda la entrada del jugador (teclas, mando, etc.).
        [NonSerialized] public CapsuleCollider2D CapsuleCollider2D; // Define colisiones y se usa en chequeos de suelo y pendientes.

        // Objeto que contiene todos los datos configurables del jugador (velocidades, saltos, etc.).
        public PlayerData PlayerData;

        private void Awake()
        {
            // --- Inicialización de referencias a componentes ---
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            InputManager = GetComponent<PlayerInputManager>();
            CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

            // --- Inicialización de la máquina de estados ---
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

            // --- Configuración de curvas de movimiento ---
            // Cada acción (salto, caída, aterrizaje, etc.) tiene una curva de altura en PlayerData.
            // Para calcular la velocidad de movimiento, se necesita la derivada de esa curva.
            // Aquí se calcula y asigna la curva de velocidad a cada acción.

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
            // Llama al método Update() del estado actual.
            // Aquí se maneja la lógica que depende del frame rate (input, animaciones, etc.).
            _stateMachine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            // Llama al método FixedUpdate() del estado actual.
            // Aquí se maneja la lógica dependiente de la física (movimiento, colisiones, etc.).
            _stateMachine.CurrentState.FixedUpdate();
        }
    }
}
