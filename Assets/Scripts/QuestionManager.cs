using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    public Text questionText;
    public Text scoreText;
    public Text finalScore;
    public Button[] answerButton;
    public QuestionData questionData; // ánh xạ đến scriptable obj
    public GameObject correct;
    public GameObject wrong;
    public GameObject gameFinish;

    private int currentQuestion = 0;
    private static int score;

    void Start()
    {
        SetQuestion(currentQuestion);
        correct.gameObject.SetActive(false);
        wrong.gameObject.SetActive(false);
        gameFinish.gameObject.SetActive(false);
    }
    
    void SetQuestion(int questionIndex)
    {
        questionText.text = questionData.questions[questionIndex].questtionText;

        foreach(Button a in answerButton)
        {
           a.onClick.RemoveAllListeners();
        }

        for(int i = 0; i < answerButton.Length; i++)
        {
            answerButton[i].GetComponentInChildren<Text>().text = questionData.questions[questionIndex].answers[i];
            int ansewrIndex = i;
            answerButton[i].onClick.AddListener(() =>
            {
                CheckAnswer(ansewrIndex);
            });
        }
    }

    void CheckAnswer(int ansewrIndex)
    {
        if (ansewrIndex == questionData.questions[currentQuestion].correctAnswerIndex)
        {
            score++;
            scoreText.text = "" + score;
            // hiện panel nếu trả lời đúng
            correct.gameObject.SetActive(true);

            //Ẩn tất cả câu trả lời
            foreach (Button a in answerButton)
            {
                a.interactable = false;
            }

            //Câu tiếp theo
            StartCoroutine(Next());
        }
        else
        {
            //trả lời sai thì hiện panel false
            wrong.gameObject.SetActive(true);

            //Ẩn tất cả câu trả lời
            foreach(Button a in answerButton)
            {
                a.interactable = false;
            }

            //Câu tiếp theo
            StartCoroutine(Next());

        } 

        IEnumerator Next()
        {
            yield return new WaitForSeconds(2);
            currentQuestion++;


            if (currentQuestion < questionData.questions.Length)
            {
                Reset();
            }
            else
            { // Hiển thị điểm nếu hết câu hỏi
                gameFinish.SetActive(true );

                float scorePercentage = (float)score / questionData.questions.Length * 100;

                finalScore.text = scorePercentage.ToString("F0") + "%";

                if (scorePercentage < 50)
                {
                    finalScore.text += "\nGAME OVER BRO";
                }
                else if (scorePercentage < 60)
                {
                    finalScore.text += "\nKEEP TRYING BRO";
                }
                else if (scorePercentage < 70)
                {
                    finalScore.text += "\nGOOD JOB BRO";
                }
                else if (scorePercentage < 80)
                {
                    finalScore.text += "\nWELL DONE BRO";
                }
                else 
                {
                    finalScore.text += "\nYOU ARE GOD BRO";
                }
            }
        }
    }
    public void Reset()
    {
        correct.gameObject.SetActive(false);
        wrong.gameObject.SetActive(false);

        foreach(Button a in answerButton)
        {
            a.interactable = true;
        }

        SetQuestion(currentQuestion);
    }

}
