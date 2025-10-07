using UnityEngine;

namespace TarodevController
{
    [CreateAssetMenu]
    public class ScriptableStats : ScriptableObject
    {
        [Header("CAPAS")]
        [Tooltip("Establece esto a la capa en la que está tu jugador")]
        public LayerMask PlayerLayer;

        [Header("ENTRADA")]
        [Tooltip("Hace que toda entrada se ajuste a un número entero. Previene que los gamepads caminen lentamente. Valor recomendado es true para garantizar paridad entre gamepad/teclado.")]
        public bool SnapInput = true;

        [Tooltip("Entrada mínima requerida antes de montar una escalera o trepar un borde. Evita trepar no deseado usando controles"), Range(0.01f, 0.99f)]
        public float VerticalDeadZoneThreshold = 0.3f;

        [Tooltip("Entrada mínima requerida antes de que se reconozca izquierda o derecha. Evita deriva con controles pegajosos"), Range(0.01f, 0.99f)]
        public float HorizontalDeadZoneThreshold = 0.1f;

        [Header("MOVIMIENTO")]
        [Tooltip("La velocidad máxima de movimiento horizontal")]
        public float MaxSpeed = 14;

        [Tooltip("La capacidad del jugador para ganar velocidad horizontal")]
        public float Acceleration = 120;

        [Tooltip("El ritmo al cual el jugador se detiene")]
        public float GroundDeceleration = 60;

        [Tooltip("Desaceleración en el aire solo después de detener la entrada en medio del aire")]
        public float AirDeceleration = 30;

        [Tooltip("Una fuerza constante hacia abajo aplicada mientras está en el suelo. Ayuda en pendientes"), Range(0f, -10f)]
        public float GroundingForce = -1.5f;

        [Tooltip("La distancia de detección para detección de suelo y techo"), Range(0f, 0.5f)]
        public float GrounderDistance = 0.05f;

        [Header("SALTO")]
        [Tooltip("La velocidad inmediata aplicada al saltar")]
        public float JumpPower = 36;

        [Tooltip("La velocidad máxima de movimiento vertical")]
        public float MaxFallSpeed = 40;

        [Tooltip("La capacidad del jugador para ganar velocidad de caída. También conocida como Gravedad en el Aire")]
        public float FallAcceleration = 110;

        [Tooltip("El multiplicador de gravedad añadido cuando el salto se libera temprano")]
        public float JumpEndEarlyGravityModifier = 3;

        [Tooltip("El tiempo antes de que el salto coyote se vuelva inutilizable. El salto coyote permite ejecutar el salto incluso después de dejar una plataforma")]
        public float CoyoteTime = .15f;

        [Tooltip("La cantidad de tiempo que almacenamos un salto en búfer. Esto permite entrada de salto antes de tocar realmente el suelo")]
        public float JumpBuffer = .2f;
    }
}