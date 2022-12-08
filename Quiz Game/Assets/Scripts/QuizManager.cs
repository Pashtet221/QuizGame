using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject quizPanel;
    public GameObject GoPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;
    public Text allQuestions;

    private int totalQuastions = 0;
    private int current = 0;
    private int score;

    public Image lvlImage;


    private void Start()
    {
        totalQuastions = QnA.Count;
        GoPanel.SetActive(false);
        GanerateQuestion();
        DisplayAllQuestions();
    }

    public void DisplayAllQuestions()
    {
        current += 1;
        allQuestions.text = current + " ИЗ " + totalQuastions;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void GameOver()
    {
        quizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuastions;
    }


    public void Correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }

    public void Wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        GanerateQuestion();

        DisplayAllQuestions();
    }

    private void SetAnswers()
    {
        for(int i = 0;i < options.Length;i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }


    private void GanerateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0,QnA.Count);

            lvlImage.sprite = QnA[currentQuestion].img;

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of questions");
            GameOver();
        }
    }
  
}
