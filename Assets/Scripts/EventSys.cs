using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventSys : MonoBehaviour
{
    public Camera MainCamera;
    public Character character;

    [SerializeField]
    private float CameraSpeed = 5.0f;

    public Panel Panel, PanelWithSaw, PanelWith2Saw, 
    PanelWithHole, PanelWithTurel, PanelWithHoleAndWall;
    private Panel[] Panels = new Panel[5];
    private System.Random rand = new System.Random();
    private int Shift = 0;
    private Vector3 Position = new Vector3(2 * 21F, -4.26F -0.03125F);


    private void StartGame()
    {
        for (int i = 0; i < Panels.Length; ++i)
        {
            Destroy(Panels[i].gameObject);
        }
        SetPanels();
    }

    private void Start()
    {
        SetPanels();
    }

    private Panel CreatePanel()
    {
        Panel panel = new Panel();
        double randNum = rand.NextDouble();
        if (randNum <= 0.10)
            panel = Instantiate(PanelWithHole, Position, PanelWithHole.transform.rotation);
        if (randNum > 0.10 && randNum <= 0.25)
            panel = Instantiate(PanelWithHoleAndWall, Position, PanelWithHoleAndWall.transform.rotation);
        if (randNum > 0.25 && randNum <= 0.50)
            panel = Instantiate(PanelWithTurel, Position, PanelWithTurel.transform.rotation);
        if (randNum > 0.50 && randNum <= 0.75)
            panel = Instantiate(PanelWithSaw, Position, PanelWithSaw.transform.rotation);
        if (randNum > 0.75)
            panel = Instantiate(PanelWith2Saw, Position, PanelWith2Saw.transform.rotation);
        return panel;
    }
    private void SetPanels()
    {
        for (int i = 0; i < Panels.Length; ++i)
        {
            Panels[i] = CreatePanel();
            Position.x += 21F;
        }
    }
    private void CheckPanels()
    {
        double randNum = rand.NextDouble();
        var distance = MainCamera.transform.position - Panels[Shift].transform.position;
        distance.y = 0;
        if (distance.sqrMagnitude > 50 * 50)
        {
            Destroy(Panels[Shift].gameObject);
            Panels[Shift] = CreatePanel();
            Position.x += 21F;
            ++Shift;
            if (Shift == Panels.Length)
                Shift = 0;
        }
    } 
    private void MoveCamera()
    {
        Vector3 direction = transform.right;
        MainCamera.transform.position += Vector3.MoveTowards(transform.position, 
        transform.position + direction, CameraSpeed * Time.deltaTime);
    }
    void Update ()
    {
        if (character != null)
        {
            MoveCamera();
            var distance = MainCamera.transform.position - character.transform.position;
            distance.y = 0;
            if (distance.sqrMagnitude > (20 * 20))
            {
                Destroy(character.gameObject);
            }
            CheckPanels();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                StartGame();
            }

        }
	}
}
