﻿using UnityEngine;
using UnityEngine.UI;

namespace ReMaz.LevelEditing.UI
{
    public class Beats : MonoBehaviour
    {
        [SerializeField] private RectTransform _beatPrefab;

        private void Start()
        {
            for (int i = 0; i < LevelEditorUtils.Beats; i++)
            {
                RectTransform beatInstance = Instantiate(_beatPrefab, transform);
                
                // Text
                Text text = beatInstance.GetComponentInChildren<Text>();
                text.text = $"{i + 1}";
                RectTransform textRT = text.GetComponent<RectTransform>();
                textRT.sizeDelta = new Vector2(textRT.sizeDelta.x, LevelEditorUtils.BeatLength * 0.45f);

                // Size
                beatInstance.sizeDelta = new Vector2(beatInstance.sizeDelta.x, LevelEditorUtils.BeatLength);
                beatInstance.anchoredPosition = new Vector2(0f, i * LevelEditorUtils.BeatLength);
            }
        }
    }
}