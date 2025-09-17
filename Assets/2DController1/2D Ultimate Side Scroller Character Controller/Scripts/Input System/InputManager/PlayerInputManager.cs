using UnityEngine;
using UnityEngine.InputSystem;
using static UltimateCC.PlayerMain; // Nos permite acceder directamente al enum AnimName

namespace UltimateCC
{
    public class PlayerInputManager : MonoBehaviour
    {
        // 📌 Referencias principales
        private PlayerMain player;         // Referencia al script PlayerMain (controlador del jugador)
        private PlayerData playerData;     // Referencia a los datos del jugador (estadísticas, timers, etc.)
        public InputActions playerControls; // Mapa de controles generado por el nuevo Input System de Unity

        // 📌 Variables privadas donde se guardan los inputs detectados
        // El atributo [NonEditable] sirve para mostrarlas en el inspector como "solo lectura".
        [SerializeField, NonEditable] private float input_Walk;     // Movimiento horizontal (float porque puede ser -1 a 1)
        [SerializeField, NonEditable] private bool input_Jump;      // Salto (presionado o no)
        [SerializeField, NonEditable] private bool input_Dash;      // Dash (presionado o no)
        [SerializeField, NonEditable] private bool input_Crouch;    // Agacharse
        [SerializeField, NonEditable] private bool input_WallGrab;  // Agarrarse a la pared
        [SerializeField, NonEditable] private float input_WallClimb; // Escalar pared (float para indicar dirección/velocidad)

        // 📌 Propiedades públicas para acceder a los inputs desde otros scripts
        public float Input_Walk => input_Walk;
        public bool Input_Jump => input_Jump;
        public bool Input_Dash => input_Dash;
        public bool Input_Crouch => input_Crouch;
        public bool Input_WallGrab => input_WallGrab;
        public float Input_WallClimb => input_WallClimb;

        // 📌 Constructor (se usa si instancias este script por código, no en Unity)
        public PlayerInputManager(PlayerMain player, PlayerData playerData)
        {
            this.player = player;
            this.playerData = playerData;
        }

        private void Awake()
        {
            // Inicializamos el mapa de controles del Input System
            playerControls = new InputActions();

            // Obtenemos referencias de PlayerMain y PlayerData
            player = GetComponent<PlayerMain>();
            playerData = player.PlayerData;
        }

        private void OnEnable()
        {
            // 📌 Activamos el mapa de controles del jugador
            playerControls.Player.Enable();

            // 📌 Registramos eventos: cada acción ejecuta una función cuando se presiona o suelta la tecla/botón
            playerControls.Player.Jump.started += OnJumpStarted;
            playerControls.Player.Jump.canceled += OnJumpCanceled;

            playerControls.Player.Walk.performed += OnWalk;
            playerControls.Player.Walk.canceled += OnWalk;

            playerControls.Player.Dash.performed += OnDash;
            playerControls.Player.Dash.canceled += OnDash;

            playerControls.Player.Crouch.performed += OnCrouch;
            playerControls.Player.Crouch.canceled += OnCrouch;

            playerControls.Player.WallGrab.performed += OnWallGrab;
            playerControls.Player.WallGrab.canceled += OnWallGrab;

            playerControls.Player.WallClimb.performed += OnWallClimb;
            playerControls.Player.WallClimb.canceled += OnWallClimb;
        }

        private void FixedUpdate()
        {
            // 📌 Se actualizan los timers (coyote time, jump buffer, cooldowns) en cada ciclo de física
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            // 🟢 Implementación de "Coyote Time" y "Jump Buffer"
            // (tiempo extra de tolerancia para saltar o escalar, aunque el jugador ya no cumpla la condición exacta)

            // Coyote time del salto (si está en el suelo y no en un salto)
            if (playerData.Physics.IsGrounded && player.CurrentState != AnimName.Jump
                && (!playerData.Physics.IsOnNotWalkableSlope || playerData.Physics.IsMultipleContactWithNonWalkableSlope))
            {
                playerData.Jump.CoyoteTimeTimer = playerData.Jump.CoyoteTimeMaxTime;
            }
            else if (player.CurrentState == AnimName.Jump)
            {
                playerData.Jump.CoyoteTimeTimer = 0f;
            }

            // Coyote time del wall jump (si está junto a una pared)
            if (playerData.Physics.IsNextToWall && player.CurrentState != AnimName.WallJump)
            {
                playerData.Walls.WallJump.CoyoteTimeTimer = playerData.Walls.WallJump.CoyoteTimeMaxTime;
            }
            else if (player.CurrentState == AnimName.WallJump)
            {
                playerData.Walls.WallJump.CoyoteTimeTimer = 0f;
            }

            // 🔄 Restamos el tiempo de los timers cada frame
            playerData.Jump.JumpBufferTimer = playerData.Jump.JumpBufferTimer > 0f ? playerData.Jump.JumpBufferTimer - Time.deltaTime : 0f;
            playerData.Jump.CoyoteTimeTimer = playerData.Jump.CoyoteTimeTimer > 0f ? playerData.Jump.CoyoteTimeTimer - Time.deltaTime : 0f;
            playerData.Dash.DashCooldownTimer = playerData.Dash.DashCooldownTimer > 0f ? playerData.Dash.DashCooldownTimer - Time.deltaTime : 0f;
            playerData.Walls.WallJump.JumpBufferTimer = playerData.Walls.WallJump.JumpBufferTimer > 0f ? playerData.Walls.WallJump.JumpBufferTimer - Time.deltaTime : 0f;
            playerData.Walls.WallJump.CoyoteTimeTimer = playerData.Walls.WallJump.CoyoteTimeTimer > 0f ? playerData.Walls.WallJump.CoyoteTimeTimer - Time.deltaTime : 0f;
        }

        // 📌 Funciones que reciben las acciones del Input System
        #region Input Event Functions

        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            input_Jump = context.ReadValueAsButton(); // Guardamos si se presionó el salto

            // Reiniciamos buffers de salto
            playerData.Jump.JumpBufferTimer = playerData.Jump.JumpBufferMaxTime;
            playerData.Walls.WallJump.JumpBufferTimer = playerData.Walls.WallJump.JumpBufferMaxTime;

            // Si el jugador no está en estados de pared, se considera un "nuevo salto"
            playerData.Jump.NewJump = player.CurrentState != AnimName.WallGrab
                                    && player.CurrentState != AnimName.WallSlide
                                    && player.CurrentState != AnimName.WallClimb
                                    && playerData.Walls.WallJump.CoyoteTimeTimer == 0;
        }

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            input_Jump = context.ReadValueAsButton();

            // Si está en medio de un salto y se suelta la tecla, se corta el salto (menor altura)
            if (player.CurrentState == AnimName.Jump)
            {
                playerData.Physics.CutJump = true;
            }
        }

        private void OnWalk(InputAction.CallbackContext context)
        {
            input_Walk = context.ReadValue<float>(); // Guardamos dirección horizontal (-1, 0, 1)
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            input_Dash = context.ReadValueAsButton(); // Guardamos si se presionó Dash
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            input_Crouch = context.ReadValueAsButton(); // Guardamos si se presionó Crouch
        }

        private void OnWallGrab(InputAction.CallbackContext context)
        {
            input_WallGrab = context.ReadValueAsButton(); // Guardamos si se presionó WallGrab
        }

        private void OnWallClimb(InputAction.CallbackContext context)
        {
            input_WallClimb = context.ReadValue<float>(); // Guardamos dirección de escalada
        }
        #endregion
    }
}

