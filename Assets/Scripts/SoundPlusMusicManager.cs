using UnityEngine;
using UnityEngine.Serialization;

namespace AlexzanderCowell
{
    public class SoundPlusMusicManager : MonoBehaviour
    {

        [FormerlySerializedAs("_mainGameSound")]
        [Header("Sounds")] 
        [SerializeField] private AudioSource mainGameSound; // Main game music that is played during the main game play.
        [SerializeField] private AudioSource jumpSounds; // Jump sounds for when the character jumps.
        [SerializeField] private AudioSource timeSounds; // Clock sound for when you collide with the clock in game.
        [SerializeField] private AudioSource rocketTimeSounds; // This sound is used while you are using the rocket boots/shoes as an effect.
        
        private bool _playMainMusic;
        private bool _playJumpSound;
        private bool _playTimeImpactSound;
        private bool _playRocketUsageSound;

        private void OnEnable()
        {
            SpawnLocations.MainMazeRoomEvent += MazeMusic;
            ClockObject.AddMoreTime += ClockSounds;
            CharacterMovement.ActivateRocketBootStateEvent += RocketSounds;
            CharacterMovement.PlayerIsCurrentlyJumpingEvent += JumpSoundsStart;
        }
        private void Update()
        {
            if (_playMainMusic)
            {
                mainGameSound.Play();
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
        private void MazeMusic(bool insideOfMainMazeRoom)
        {
            if (insideOfMainMazeRoom) _playMainMusic = true;
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
        private void RocketSounds(bool timeToMoveFaster)
        {
            if (timeToMoveFaster) RocketsInUse();
        }
        private void JumpSoundsStart(bool playerIsJumping)
        {
            if (playerIsJumping) JumpSoundsMustPlay();
        }
        private void OnDisable()
        {
            SpawnLocations.MainMazeRoomEvent -= MazeMusic;
            ClockObject.AddMoreTime -= ClockSounds;
            CharacterMovement.ActivateRocketBootStateEvent -= RocketSounds;
            CharacterMovement.PlayerIsCurrentlyJumpingEvent -= JumpSoundsStart;
        }
    }
}
