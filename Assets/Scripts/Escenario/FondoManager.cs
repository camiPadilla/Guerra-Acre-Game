using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{
    [SerializeField]
    private float retraso;
    [SerializeField]
    private Transform camaraTransform;
    private Camera camara;
    private Vector3 anteriorPosicionCamara;
    private bool estaEnCamara;

    // Referencias para calcular bounds
    private Renderer objetoRenderer;
    private Collider objetoCollider3D;
    private Collider2D objetoCollider2D;

    void Start()
    {
        if (camaraTransform == null)
        {
            camara = Camera.main;
            camaraTransform = camara != null ? camara.transform : null;
        }
        else
        {
            camara = camaraTransform.GetComponent<Camera>() ?? Camera.main;
            if (camara == null)
            {
                camara = camaraTransform.GetComponentInParent<Camera>() ?? Camera.main;
            }
        }

        if (camara == null)
        {
            Debug.LogWarning("FondoManager: camaraTransform no asignada y no se encontró Camera.main.");
            anteriorPosicionCamara = Vector3.zero;
        }
        else
        {
            anteriorPosicionCamara = camara.transform.position;
        }

        // Obtener componentes para bounds
        objetoRenderer = GetComponent<Renderer>() ?? GetComponentInChildren<Renderer>();
        objetoCollider3D = GetComponent<Collider>();
        objetoCollider2D = GetComponent<Collider2D>();

        estaEnCamara = false;
    }

    void Update()
    {
        if (camara == null) return;

        bool visible = EstaVisiblePorCamara();

        if (visible)
        {
            // Si acaba de entrar en cámara, reiniciamos el registro para evitar saltos
            if (!estaEnCamara)
            {
                anteriorPosicionCamara = camara.transform.position;
            }

            float deltaX = (camara.transform.position.x - anteriorPosicionCamara.x) * retraso;
            transform.Translate(new Vector3(deltaX, 0f, 0f));
            anteriorPosicionCamara = camara.transform.position;
        }

        estaEnCamara = visible;
    }

    private bool EstaVisiblePorCamara()
    {
        // Obtener bounds válidos: prioridad Renderer -> Collider2D -> Collider3D
        Bounds bounds;
        if (TryGetBounds(out bounds))
        {
            // Calcular frustum y comprobar intersección con los bounds
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camara);
            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
        else
        {
            // Fallback: usar el pivote (comportamiento anterior)
            Vector3 vp = camara.WorldToViewportPoint(transform.position);
            return vp.z > 0f && vp.x >= 0f && vp.x <= 1f && vp.y >= 0f && vp.y <= 1f;
        }
    }

    private bool TryGetBounds(out Bounds bounds)
    {
        if (objetoRenderer != null)
        {
            bounds = objetoRenderer.bounds;
            return true;
        }

        if (objetoCollider2D != null)
        {
            bounds = objetoCollider2D.bounds;
            return true;
        }

        if (objetoCollider3D != null)
        {
            bounds = objetoCollider3D.bounds;
            return true;
        }

        bounds = default;
        return false;
    }
}