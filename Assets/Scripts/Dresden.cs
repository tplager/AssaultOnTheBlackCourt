﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresden : Vehicle
{
    public int MAX_HEALTH;
    public List<Pickup> activePickups = new List<Pickup>(); 

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            base.Update();

            OrientSprite();
        }

        if (health <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public override void CalcSteeringForces()
    {
        ultimateForce = Vector3.zero;

        Vector3 xyPosition = new Vector3(transform.position.x, 0, transform.position.z);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ultimateForce += Seek(transform.position + Vector3.up);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ultimateForce += Seek(transform.position - Vector3.up);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) ultimateForce += Seek(transform.position + Vector3.right);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) ultimateForce += Seek(transform.position - Vector3.right);

        if (ultimateForce != Vector3.zero)
        {
            ultimateForce = ultimateForce.normalized * maxForce;
        }
    }

    public override Vector3 Separation()
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Displays debug lines for this object
    /// if the showDebugLines field is true
    /// </summary>
    public void OnRenderObject()
    {
        if (showDebugLines == true)
        {
            forwardMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.up);
            GL.End();

            rightMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.right);
            GL.End();

            futureMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + velocity);
            GL.End();
        }
    }

    private void OrientSprite()
    {
        bool flip = gameObject.GetComponentInChildren<SpriteRenderer>().flipX;
        float dot = Vector3.Dot(direction, transform.right);

        if (dot < 0) flip = true;
        else if (dot > 0) flip = false;

        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = flip; 
    }
}
