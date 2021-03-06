using System;
using System.Collections;
using UnityEngine;

public class DragRigidbody2 : MonoBehaviour
    {
    public float k_Spring = 50.0f;
    public float k_Damper = 5.0f;
    public float k_Drag = 10.0f;
    public float k_AngularDrag = 5.0f;
    public float k_Distance = 0.2f;
    public bool k_AttachToCenterOfMass = false;
	public float k_MaxDistance, k_MinDistance;

    private SpringJoint m_SpringJoint;

    public GameObject obj;
    public GameObject CubeManager;

    void Start()
    {
        obj = null;
        CubeManager = GameObject.FindGameObjectWithTag("CubeManager");
    }

    private void Update()
    {
        // Make sure the user pressed the mouse down
        if (!Input.GetMouseButtonDown(0))
        {
            
            return;
        }

        var mainCamera = FindCamera();

        // We need to actually hit an object
        RaycastHit hit = new RaycastHit();
        if (
            !Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
				mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, k_MaxDistance,
                                Physics.DefaultRaycastLayers))
        {
            return;
        }
        // We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic || hit.distance < k_MinDistance)
        {
            return;
        }

        if (!m_SpringJoint)
        {
            var go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            m_SpringJoint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;
        }

        obj = hit.rigidbody.gameObject;

        m_SpringJoint.transform.position = hit.point;
        m_SpringJoint.anchor = Vector3.zero;

        m_SpringJoint.spring = k_Spring;
        m_SpringJoint.damper = k_Damper;
        m_SpringJoint.maxDistance = k_Distance;
        m_SpringJoint.connectedBody = hit.rigidbody;

        StartCoroutine("DragObject", hit.distance);

        CubeManager.GetComponent<CubeManager>().checkdata(obj).isDraged = true;
        CubeManager.GetComponent<CubeManager>().timeUpd(obj);
        CubeManager.GetComponent<CubeManager>().CubeUpdateDel(obj);
    }


    private IEnumerator DragObject(float distance)
    {
        var oldDrag = m_SpringJoint.connectedBody.drag;
        var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
        var mainCamera = FindCamera();
        while (Input.GetMouseButton(0))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            m_SpringJoint.transform.position = ray.GetPoint(distance);
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            CubeManager.GetComponent<CubeManager>().checkdata(obj).isDraged = false;
            CubeManager.GetComponent<CubeManager>().CubeUpdateSpwn(obj);
            obj = null;
            
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
    }


    private Camera FindCamera()
    {
        if (GetComponent<Camera>())
        {
            return GetComponent<Camera>();
        }

        return Camera.main;
    }
}

