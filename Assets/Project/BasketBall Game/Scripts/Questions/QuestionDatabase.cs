using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "QuestionDatabase", order = 1)]
public class QuestionDatabase : ScriptableObject
{
    public QuestionData[] questions;// = new List<QuestionData>();
}