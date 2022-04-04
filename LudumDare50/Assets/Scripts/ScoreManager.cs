using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance = null;

    public Text scoreText = null;
    public Text particleScoreText = null;
    public int score = 0;

    public int combo = 0;
    public int comboIndex = 0;

    private bool onCombo = false;
    private Coroutine comboCoroutine = null;
    [SerializeField] private List<ComboDifficulty> comboDifficulties = new List<ComboDifficulty>();
    [SerializeField] private Slider comboSlider = null;
    [SerializeField] private Text comboMultiplierText = null;

    private float timeLeft = 0;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        if (onCombo)
            ComboTimer();

        comboSlider.gameObject.SetActive(onCombo);
        comboMultiplierText.gameObject.SetActive(onCombo);
    }

    public void AddScore(int value)
    {
        combo++;
        if (comboIndex < comboDifficulties.Count - 1 && comboDifficulties[comboIndex + 1].comboTarget <= combo)
            comboIndex++;

        if (comboIndex > 0)
        {
            timeLeft = comboDifficulties[comboIndex].timer;
            onCombo = true;
        }

        score += value * GetComboMultiplier();

    }

    public void ComboTimer()
    {
        comboSlider.maxValue = comboDifficulties[comboIndex].timer;
        comboSlider.value = timeLeft;

        comboMultiplierText.text = $"x{comboDifficulties[comboIndex].multiplier}";

        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            onCombo = false;
            combo = 0;
            comboIndex = 0;
        }
    }

    public int GetComboMultiplier()
    {
        return comboDifficulties[comboIndex].multiplier;
    }
}

[Serializable]
public class ComboDifficulty
{
    public float timer = 0;
    public int comboTarget = 0;
    public int multiplier = 0;
}
