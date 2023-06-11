using System.Text;
using UnityEngine;

namespace AlexzanderCowell
{
    public class SoundPlusMusicManager : MonoBehaviour
    {

        [Header("Sounds")] 
        [SerializeField] private AudioSource _mainGameSound; // Main game music that is played during the main game play.
        [SerializeField] private AudioSource _jumpSounds; // Jump sounds for when the character jumps.
        [SerializeField] private AudioSource _timeSounds; // Clock sound for when you collide with the clock in game.
        [SerializeField] private AudioSource _rocketTimeSounds; // This sound is used while you are using the rocket boots/shoes as an effect.
        
        private bool playMainMusic;
        private bool playJumpSound;
        private bool playTimeImpactSound;
        private bool playRocketUsageSound;

        private void OnEnable()
        {
            SpawnLocations._MainMazeRoomEvent += MazeMusic;
            ClockObject.addMoreTime += ClockSounds;
            CharacterMovement._ActivateRocketBootStateEvent += RocketSounds;
            CharacterMovement._PlayerIsCurrentlyJumpingEvent += JumpSoundsStart;
        }

        private void OnDisable()
        {
            SpawnLocations._MainMazeRoomEvent -= MazeMusic;
            ClockObject.addMoreTime -= ClockSounds;
            CharacterMovement._ActivateRocketBootStateEvent -= RocketSounds;
            CharacterMovement._PlayerIsCurrentlyJumpingEvent -= JumpSoundsStart;
        }

        private void Update()
        {
            if (playMainMusic)
            {
                _mainGameSound.Play();
            }

            if (playJumpSound)
            {
                _jumpSounds.Play();
                playJumpSound = false;
            }

            if (playRocketUsageSound)
            {
                _rocketTimeSounds.Play();
                playRocketUsageSound = false;
            }

            if (playTimeImpactSound)
            {
                _timeSounds.Play();
                playTimeImpactSound = false;
            }
               
            
        }

        private void MazeMusic(bool insideOfMainMazeRoom)
        {
            if (insideOfMainMazeRoom) playMainMusic = true;
        }

        private void JumpSoundsMustPlay()
        {
            playJumpSound = true;
        }
        
        private void RocketsInUse()
        {
            playRocketUsageSound = true;
        }

        private void ClockGotHit()
        {
            playTimeImpactSound = true;
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
    }
}
