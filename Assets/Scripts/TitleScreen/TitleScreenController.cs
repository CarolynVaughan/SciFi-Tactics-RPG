using System;
using UnityEngine;
using UnityEngine.UI;

namespace SciFiTacticsRPG.TitleScreen
{
    /// <summary>
    /// Controls the Title screen
    /// </summary>
    public class TitleScreenController : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadSaveButton;
        [SerializeField] private Button _settingsButton;

        private bool _isProcessingInput;

        private void Start()
        {
            if (_newGameButton)
            {
                _newGameButton.onClick.AddListener(HandleNewGameClicked);
                _newGameButton.interactable = true;
            }
            if (_loadSaveButton)
            {
                _loadSaveButton.interactable = false;
            }
            if (_settingsButton)
            {
                _settingsButton.interactable = false;
            }
        }

        /// <summary>
        /// Handles the New Game button being pressed
        /// </summary>
        private void HandleNewGameClicked()
        {
            if (_isProcessingInput) return;

            _isProcessingInput = true;

            if (GameController.Instance != null)
            {
                GameController.Instance.StartNewGame();
            }
            else
            {
                _isProcessingInput = false;
            }
        }
    }
}