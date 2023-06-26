using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace AlexzanderCowell
{
    public class RandomFlashMessages : MonoBehaviour
    {
        [SerializeField] private GameObject textsCanvas;
        [SerializeField] private Text randomMessages;
        private int randomInteger;
        private int normalInt;
        private float currentTimer;
        private float minTimer = 0;
        private float maxTimer = 1;
        private bool timerTurnAround;

        private void Update()
        {
            TimerGoingUp();
            TimerGoingDown();
            TimerGeneral();
            
            randomInteger = Random.Range(0, 5);
            currentTimer = Mathf.Clamp(currentTimer, minTimer, maxTimer);

            textsCanvas.GetComponent<CanvasGroup>().alpha = currentTimer;

            if (normalInt == 1)
            {
                randomMessages.text = ("Run!..");
            }
            else if (normalInt == 2)
            {
                randomMessages.text = ("Escape the Maze..");
            }
            else if (normalInt == 3)
            {
                randomMessages.text = ("Reach the end..");
            }
            else if (normalInt == 4)
            {
                randomMessages.text = ("Quick their coming..");
            }
            else
            {
                randomMessages.text = ("Don't get lost..");
            }
        }

        private void TimerGoingUp()
        {
            if (currentTimer == minTimer)
            {
                normalInt = randomInteger;
                timerTurnAround = true;
            }
        }

        private void TimerGoingDown()
        {
            if (currentTimer == maxTimer)
            {
                timerTurnAround = false;
            }
        }

        private void TimerGeneral()
        {
            if (timerTurnAround)
            {
                currentTimer += 0.2f * Time.deltaTime;
            }
            else
            {
                
                currentTimer -= 0.4f * Time.deltaTime;
            }
        }
    }
}
