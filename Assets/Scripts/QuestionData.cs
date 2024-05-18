using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New QuestionData", menuName = "QuestionData")]
public class QuestionData : ScriptableObject
{
    [System.Serializable]
    public struct question
    {
        public string questtionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public question[] questions;
    
}
