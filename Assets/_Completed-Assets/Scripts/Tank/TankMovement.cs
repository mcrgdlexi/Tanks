﻿using UnityEngine;

namespace Complete {
    public class TankMovement : MonoBehaviour {
        public int m_PlayerNumber = 1;        // This is set by this tank's manager.
        public float m_Speed = 12f;           // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;      // How fast the tank turns in degrees per second.
        public AudioSource m_MovementAudio;   // Engine sounds.
        public AudioClip m_EngineIdling;      // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;     // Audio to play when the tank is moving.
		public float m_PitchRange = 0.2f;     // The amount by which the pitch of the engine noises can vary.

        private string m_MovementAxisName;    // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;        // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;        // Reference used to move the tank.
        private float m_MovementInputValue;   // The current value of the movement input.
        private float m_TurnInputValue;       // The current value of the turn input.
        private float m_OriginalPitch;        // The pitch of the audio source at the start of the scene.

        private void Awake () {
            m_Rigidbody = GetComponent<Rigidbody> ();
        }

        private void OnEnable () {
            // When the tank is on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;
            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;
        }

        private void OnDisable () {
            // When the tank is off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;
        }

        private void Start () {
            m_MovementAxisName = "Vertical" + m_PlayerNumber;
            m_TurnAxisName = "Horizontal" + m_PlayerNumber;

            m_OriginalPitch = m_MovementAudio.pitch;
        }

        private void Update () {
            m_MovementInputValue = Input.GetAxis (m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis (m_TurnAxisName);
            EngineAudio ();
        }

		private void EngineAudio () {
			// If the tank is stationary
            if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) {
                //if the audio source is currently playing the driving clip
                if (m_MovementAudio.clip == m_EngineDriving) {
                    // change the clip to idling and play it.
                    m_MovementAudio.clip = m_EngineIdling;
                    m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play ();
                }
            }
            else {
                if (m_MovementAudio.clip == m_EngineIdling) {
                    // change the clip to driving and play.
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
        }

        private void FixedUpdate () {
            Move ();
            Turn ();
        }

        private void Move () {
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }


        private void Turn () {
            float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
            m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
        }
    }
}