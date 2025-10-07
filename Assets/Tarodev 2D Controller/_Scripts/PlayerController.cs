using System;
using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Controlador de jugador 2D desarrollado por Tarodev.
    /// Versión gratuita con funcionalidades básicas de movimiento y salto.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats; // Configuración de estadísticas del jugador
        private Rigidbody2D _rb; // Componente Rigidbody2D para física
        private CapsuleCollider2D _col; // Collider para detección de colisiones
        private FrameInput _frameInput; // Input capturado en el frame actual
        private Vector2 _frameVelocity; // Velocidad calculada para el frame
        private bool _cachedQueryStartInColliders; // Cache para configuración de Physics2D
        [SerializeField] private float reduccion;
        private bool forzarAgachado;

        #region Interface

        // Implementación de la interfaz IPlayerController
        public Vector2 FrameInput => _frameInput.Move;
        public bool agachado => _frameInput.agachado; // PropiedadAgachado
        public event Action<bool, float> GroundedChanged; // Evento cuando cambia el estado de grounded
        public event Action Jumped; // Evento cuando el jugador salta

        #endregion

        private float _time; // Tiempo acumulado del juego

        private void Awake()
        {
            // Obtener referencias a los componentes
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            // Cachear configuración de Physics2D para restaurarla después
            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void Update()
        {
            _time += Time.deltaTime; // Actualizar tiempo acumulado
            GatherInput(); // Capturar input del usuario
        }

        /// <summary>
        /// Recopila y procesa el input del usuario para el frame actual
        /// </summary>
        private void GatherInput()
        {
            //Debug.Log(reduccion);
            // Crear estructura con el input actual
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C), // Salto presionado en este frame
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C), // Salto mantenido
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), // Input de movimiento
                agachado = Input.GetKey(KeyCode.LeftControl)|| forzarAgachado
            };

            // Aplicar deadzone y snapping si está habilitado
            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            // Manejar input de salto para buffer de salto
            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true; // Marcar que hay un salto pendiente de procesar
                _timeJumpWasPressed = _time; // Registrar momento del input de salto
            }
            if (_frameInput.agachado)
            {
                reduccion = 0.5f;
            }
            else
            {
                reduccion = 1;
            }
        }

        private void FixedUpdate()
        {
            // Física y movimiento se procesan en FixedUpdate
            CheckCollisions(); // Verificar colisiones con suelo y techo
            HandleJump(); // Manejar lógica de salto
            HandleDirection(); // Manejar movimiento horizontal
            HandleGravity(); // Aplicar gravedad

            ApplyMovement(); // Aplicar velocidad final al Rigidbody
        }

        #region Collisions

        private float _frameLeftGrounded = float.MinValue; // Tiempo cuando se dejó el suelo
        private bool _grounded; // Estado actual de contacto con el suelo

        /// <summary>
        /// Verifica colisiones con suelo y techo usando raycasts
        /// </summary>
        private void CheckCollisions()
        {
            // Configurar Physics2D para evitar que los raycasts empiecen dentro de colliders
            Physics2D.queriesStartInColliders = false;

            // Realizar raycasts para detectar suelo y techo
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool angosto = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance+0.5f, ~_stats.PlayerLayer);


            // Si se golpea un techo, limitar velocidad vertical hacia arriba
            if (ceilingHit)
            {
                _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            }
            if (angosto)
            {
                if (_frameInput.agachado)
                {
                    forzarAgachado = true;
                }
            }
            else
            {
                forzarAgachado = false;
            }

            // Detectar cuando se aterriza en el suelo
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true; // Reactivar coyote time
                _bufferedJumpUsable = true; // Reactivar buffer de salto
                _endedJumpEarly = false; // Resetear flag de salto temprano
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y)); // Invocar evento
            }
            // Detectar cuando se deja el suelo
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time; // Registrar tiempo cuando se dejó el suelo
                GroundedChanged?.Invoke(false, 0); // Invocar evento
            }

            // Restaurar configuración original de Physics2D
            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        // Variables para manejar la mecánica de salto
        private bool _jumpToConsume; // Si hay un salto pendiente de procesar
        private bool _bufferedJumpUsable; // Si el buffer de salto está disponible
        private bool _endedJumpEarly; // Si se soltó el salto temprano
        private bool _coyoteUsable; // Si el coyote time está disponible
        private float _timeJumpWasPressed; // Tiempo cuando se presionó salto

        // Propiedades para verificar condiciones de salto
        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        /// <summary>
        /// Maneja toda la lógica relacionada con el salto
        /// </summary>
        private void HandleJump()
        {
            // Detectar si se soltó el salto temprano (para caída más rápida)
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            // Salir si no hay salto para procesar y no hay buffer de salto activo
            if (!_jumpToConsume && !HasBufferedJump) return;

            // Ejecutar salto si está en suelo o puede usar coyote time
            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false; // Resetear flag de salto pendiente
        }

        /// <summary>
        /// Ejecuta el salto aplicando fuerza y actualizando estados
        /// </summary>
        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower* reduccion; // Aplicar fuerza de salto
            Jumped?.Invoke(); // Invocar evento de salto
        }

        #endregion


        #region Horizontal

        /// <summary>
        /// Maneja el movimiento horizontal y la desaceleración
        /// </summary>
        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                // Aplicar desaceleración cuando no hay input
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                // Acelerar hacia la velocidad máxima
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed * reduccion *0.75f, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        /// <summary>
        /// Maneja la aplicación de gravedad y fuerzas verticales
        /// </summary>
        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                // Aplicar fuerza de grounding cuando está en suelo
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                // Aplicar gravedad en el aire
                var inAirGravity = _stats.FallAcceleration;
                // Aumentar gravedad si se soltó el salto temprano
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        /// <summary>
        /// Aplica la velocidad calculada al Rigidbody2D
        /// </summary>
        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Advertencia en el editor si no hay stats asignadas
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    /// <summary>
    /// Estructura que contiene el input capturado en un frame
    /// </summary>
    public struct FrameInput
    {
        public bool JumpDown; // Salto presionado en este frame
        public bool JumpHeld; // Salto mantenido
        public Vector2 Move; // Input de movimiento (Horizontal, Vertical)
        public bool agachado;
    }

    /// <summary>
    /// Interfaz para el controlador de jugador
    /// </summary>
    public interface IPlayerController
    {
        // Eventos para notificar cambios de estado
        public event Action<bool, float> GroundedChanged; // (grounded, velocidad de impacto)
        public event Action Jumped; // Salto ejecutado

        // Input de movimiento del frame actual
        public Vector2 FrameInput { get; }
        public bool agachado { get; }
    }
}