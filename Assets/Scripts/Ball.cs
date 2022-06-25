using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D m_rigidBody;
    private SpringJoint2D m_springJoint;
    private bool isHolding = false;
    public float releaseTime = 0.15f;
    private Camera m_camera;
    public GameObject hook;
    public float maxDragDistance = 2f;

    private void Start()
    {
        m_camera = Camera.main;
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_springJoint = GetComponent<SpringJoint2D>();
        m_rigidBody.isKinematic = true;
    }

    private void Update()
    {
        if(!isHolding) return;
        Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if(Vector3.Distance(mousePos, hook.transform.position) > maxDragDistance)
        {
            m_rigidBody.position = hook.transform.position + (mousePos - hook.transform.position).normalized * maxDragDistance;
        } else
            m_rigidBody.position = mousePos;
    }

    private void OnMouseDown()
    {
        isHolding = true;
        m_rigidBody.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isHolding = false;
        m_rigidBody.isKinematic = false;

        StartCoroutine(Release());
    }
    

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        m_springJoint.enabled = false;
        this.enabled = false;
    }
}
