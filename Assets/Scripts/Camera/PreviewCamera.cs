using UnityEngine;

public class PreviewCamera : MonoBehaviour
{
    [SerializeField] private Transform m_Target = null;
    
    private void Update()
    {
        transform.LookAt(m_Target);
        transform.Translate(Vector3.right * Time.deltaTime);
    }
}
