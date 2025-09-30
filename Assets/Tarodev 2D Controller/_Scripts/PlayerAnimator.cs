using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Controlador de animaciones y efectos visuales para el personaje del jugador.
    /// Maneja animaciones, part�culas y sonidos en respuesta a los eventos del controlador.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _anim; // Referencia al componente Animator
        [SerializeField] private SpriteRenderer _sprite; // Referencia al SpriteRenderer para voltear el sprite
        [SerializeField] private GameObject personajeObject;
        [SerializeField] private Animator newAnim;
        [SerializeField] private Animator newAnim2;

        [Header("Settings")]
        [SerializeField, Range(1f, 3f)]
        private float _maxIdleSpeed = 2; // Velocidad m�xima de animaci�n idle cuando se mueve

        [SerializeField] private float _maxTilt = 5; // Inclinaci�n m�xima del personaje al correr
        [SerializeField] private float _tiltSpeed = 20; // Velocidad de interpolaci�n de la inclinaci�n

        [Header("Particles")]
        [SerializeField] private ParticleSystem _jumpParticles; // Part�culas al saltar
        [SerializeField] private ParticleSystem _launchParticles; // Part�culas al lanzarse
        [SerializeField] private ParticleSystem _moveParticles; // Part�culas durante el movimiento
        [SerializeField] private ParticleSystem _landParticles; // Part�culas al aterrizar

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] _footsteps; // Array de sonidos de pasos

        private AudioSource _source; // Componente AudioSource para reproducir sonidos
        private IPlayerController _player; // Referencia al controlador del jugador
        private bool _grounded; // Estado actual de si est� en el suelo
        private ParticleSystem.MinMaxGradient _currentGradient; // Gradiente de color actual para part�culas

        private void Awake()
        {
            // Obtener referencias a los componentes necesarios
            _source = GetComponent<AudioSource>();
            _player = GetComponentInParent<IPlayerController>(); // Buscar en el padre
        }

        private void OnEnable()
        {
            // Suscribirse a los eventos del controlador del jugador
            _player.Jumped += OnJumped;
            _player.GroundedChanged += OnGroundedChanged;

            _moveParticles.Play(); // Iniciar part�culas de movimiento
        }

        private void OnDisable()
        {
            // Desuscribirse de los eventos para evitar memory leaks
            _player.Jumped -= OnJumped;
            _player.GroundedChanged -= OnGroundedChanged;

            _moveParticles.Stop(); // Detener part�culas de movimiento
        }

        private void Update()
        {
            if (_player == null) return; // Verificaci�n de seguridad

            // Actualizar efectos visuales cada frame
            DetectGroundColor(); // Detectar color del suelo para part�culas
            HandleSpriteFlip(); // Manejar volteo del sprite
            HandleIdleSpeed(); // Ajustar velocidad de animaci�n idle
            HandleCharacterTilt(); // Manejar inclinaci�n del personaje
            NuevosEventosAnimaci�n();
        }

        /// <summary>
        /// Voltea el sprite horizontalmente basado en la direcci�n del input
        /// </summary>
        private void HandleSpriteFlip()
        {
            if (_player.FrameInput.x != 0)
            {
                if (_player.FrameInput.x > 0)
                {
                    personajeObject.transform.localScale = new Vector2(1, 1);
                }else if (_player.FrameInput.x < 0)
                {
                    personajeObject.transform.localScale = new Vector2(-1, 1);
                }
            }
                //_sprite.flipX = _player.FrameInput.x < 0; // Voltear si se mueve a la izquierda
        }

        /// <summary>
        /// Ajusta la velocidad de la animaci�n idle basado en la fuerza del input
        /// Tambi�n escala las part�culas de movimiento
        /// </summary>
        private void NuevosEventosAnimaci�n()
        {
            newAnim.SetFloat("dirX", Mathf.Abs(_player.FrameInput.x));
            newAnim.SetBool("agachado", _player.agachado);
            newAnim2.SetFloat("dirX", Mathf.Abs(_player.FrameInput.x));
            newAnim2.SetBool("agachado", _player.agachado);
        }
        private void HandleIdleSpeed()
        {


            var inputStrength = Mathf.Abs(_player.FrameInput.x); // Fuerza del input (0-1)

            // Interpolar entre velocidad normal y m�xima basado en el input
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));

            // Escalar part�culas de movimiento seg�n la fuerza del input
            _moveParticles.transform.localScale = Vector3.MoveTowards(
                _moveParticles.transform.localScale,
                Vector3.one * inputStrength,
                2 * Time.deltaTime
            );
        }

        /// <summary>
        /// Maneja la inclinaci�n del personaje cuando corre
        /// Solo se aplica cuando est� en el suelo
        /// </summary>
        private void HandleCharacterTilt()
        {
            // Calcular rotaci�n objetivo: inclinarse al correr o identidad en aire/sin movimiento
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;

            // Rotar suavemente hacia la rotaci�n objetivo
            _anim.transform.up = Vector3.RotateTowards(
                _anim.transform.up,
                runningTilt * Vector2.up,
                _tiltSpeed * Time.deltaTime,
                0f
            );
        }

        /// <summary>
        /// Maneja el evento de salto del jugador
        /// </summary>
        private void OnJumped()
        {
            // Disparar animaci�n de salto y resetear trigger de grounded
            _anim.SetTrigger(JumpKey);
            _anim.ResetTrigger(GroundedKey);

            // Reproducir part�culas de salto si estaba en el suelo (evitar coyote time)
            if (_grounded)
            {
                SetColor(_jumpParticles);
                SetColor(_launchParticles);
                _jumpParticles.Play();
            }
        }

        /// <summary>
        /// Maneja el cambio de estado grounded/aire
        /// </summary>
        /// <param name="grounded">Si el jugador est� en el suelo</param>
        /// <param name="impact">Fuerza del impacto al aterrizar</param>
        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            if (grounded)
            {
                // Efectos al aterrizar
                DetectGroundColor(); // Actualizar color basado en el suelo
                SetColor(_landParticles); // Aplicar color a part�culas de aterrizaje

                _anim.SetTrigger(GroundedKey); // Disparar animaci�n de aterrizaje

                // Reproducir sonido de paso aleatorio
                _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);

                _moveParticles.Play(); // Reanudar part�culas de movimiento

                // Escalar part�culas de aterrizaje basado en la fuerza del impacto
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                _landParticles.Play();
            }
            else
            {
                // Efectos al dejar el suelo
                _moveParticles.Stop(); // Detener part�culas de movimiento
            }
        }

        /// <summary>
        /// Detecta el color del suelo mediante un raycast hacia abajo
        /// y actualiza el gradiente de color para las part�culas
        /// </summary>
        private void DetectGroundColor()
        {
            // Lanzar raycast hacia abajo para detectar el suelo
            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);

            // Verificar si golpe� un collider v�lido con SpriteRenderer
            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r))
                return;

            // Crear gradiente de color basado en el color del sprite del suelo
            var color = r.color;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(_moveParticles); // Aplicar a part�culas de movimiento
        }

        /// <summary>
        /// Aplica el color actual a un sistema de part�culas
        /// </summary>
        /// <param name="ps">Sistema de part�culas a colorear</param>
        private void SetColor(ParticleSystem ps)
        {
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        // Hashes est�ticos para los par�metros del Animator (mejor performance)
        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
    }
}