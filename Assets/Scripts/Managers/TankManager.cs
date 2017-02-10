using System;
using UnityEngine;

namespace Complete {
    [Serializable]
	public class TankManager {
        public Color m_PlayerColor;                           // This is the color this tank will be tinted.
        public Transform m_SpawnPoint;                        // The position and direction the tank will have when it spawns.
        [HideInInspector] public int m_PlayerNumber;          // This specifies which player this the manager for.
        [HideInInspector] public string m_ColoredPlayerText;  // A string that represents the player with their number colored to match their tank.
        [HideInInspector] public GameObject m_Instance;       // A reference to the instance of the tank when it is created.
        [HideInInspector] public int m_Wins;                  // The number of wins this player has so far.

        private TankMovement m_Movement;
        private TankShooting m_Shooting;
        private GameObject m_CanvasGameObject;                // Used to disable the world space UI during the Starting and Ending phases of each round.

        public void Setup () {
            m_Movement = m_Instance.GetComponent<TankMovement> ();
            m_Shooting = m_Instance.GetComponent<TankShooting> ();
            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas> ().gameObject;
            m_Movement.m_PlayerNumber = m_PlayerNumber;
            m_Shooting.m_PlayerNumber = m_PlayerNumber;
            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer> ();
            for (int i = 0; i < renderers.Length; i++) {
                renderers[i].material.color = m_PlayerColor;
            }
        }

        public void DisableControl () {
            m_Movement.enabled = false;
            m_Shooting.enabled = false;
            m_CanvasGameObject.SetActive (false);
        }

        public void EnableControl () {
            m_Movement.enabled = true;
            m_Shooting.enabled = true;
            m_CanvasGameObject.SetActive (true);
        }

        // Used at the start of each round to put the tank into it's default state.
        public void Reset () {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive (false);
            m_Instance.SetActive (true);
        }
    }
}