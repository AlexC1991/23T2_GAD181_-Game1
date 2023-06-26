using UnityEngine;
using UnityEngine.Serialization;

namespace AlexzanderCowell
{
    public class SoundPlusMusicManager : MonoBehaviour
    {
        
        [Header("Sounds")] 
        public AudioSource mainGameSound; // Main game music that is played during the main game play.
        [SerializeField] private AudioSource jumpSounds; // Jump sounds for when the character jumps.
        [SerializeField] private AudioSource timeSounds; // Clock sound for when you collide with the clock in game.
        [SerializeField] private AudioSource rocketTimeSounds; // This sound is used while you are using the rocket boots/shoes as an effect.
        
        [HideInInspector] public bool _playMainMusic;
        private bool _playJumpSound;
        private bool _playTimeImpactSound;
        private bool _playRocketUsageSound;

        [SerializeField] private RocketBootController rC;
        

        private void OnEnable()
        {
            ClockObject.AddMoreTime += ClockSounds;
            CharacterMovement.PlayerIsCurrentlyJumpingEvent += JumpSoundsStart;
        }
        private void Update()
        {
            RocketSounds();
            
            if (_playMainMusic) 
            {
                mainGameSound.Play();
                _playMainMusic = false;
            }

            if (_playJumpSound)
            {
                jumpSounds.Play();
                _playJumpSound = false;
            }

            if (_playRocketUsageSound)
            {
                rocketTimeSounds.Play();
                _playRocketUsageSound = false;
            }

            if (_playTimeImpactSound)
            {
                timeSounds.Play();
                _playTimeImpactSound = false;
            }
            
        }

        private void JumpSoundsMustPlay()
        {
            _playJumpSound = true;
        }
        private void RocketsInUse()
        {
            _playRocketUsageSound = true;
        }
        private void ClockGotHit()
        {
            _playTimeImpactSound = true;
        }
        private void ClockSounds(bool moreTimeAdded)
        {
            if (moreTimeAdded) ClockGotHit();
        }
        private void RocketSounds()
        {
            if (rC.rocketBootState) RocketsInUse();
        }
        private void JumpSoundsStart(bool playerIsJumping)
        {
            if (playerIsJumping) JumpSoundsMustPlay();
        }
        private void OnDisable()
        {
            ClockObject.AddMoreTime -= ClockSounds;
            CharacterMovement.PlayerIsCurrentlyJumpingEvent -= JumpSoundsStart;
        }
    }
}
