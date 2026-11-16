using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SciFiTacticsRPG
{
    /// <summary>
    /// Singleton class that controls the game's low level functions
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CampaignContentList _campaignContent;

        [Tooltip("Parent object for the overlay displayed when loading a new scene")]
        [SerializeField] private GameObject _loadingOverlayObject;

        private static GameController _instance = null;

        public static GameController Instance => _instance;

        private const string TitleSceneName = "Title";

        private string _currentSceneName;
        private int _campaignEntryIndex;
        private string _currentContentId;
        public string CurrentContentId => _currentContentId;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private async void Start()
        {
            try
            {
                await LoadSceneAsync(TitleSceneName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Loads the specified scene, unloading the previously loaded scene if necessary
        /// </summary>
        /// <returns></returns>
        private async Awaitable LoadSceneAsync(string sceneName)
        {
            if (sceneName == _currentSceneName) return;

            if (_loadingOverlayObject) _loadingOverlayObject.SetActive(true);

            if (!string.IsNullOrWhiteSpace(_currentSceneName))
            {
                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(_currentSceneName);

                while (!unloadOperation.isDone)
                {
                    await Awaitable.NextFrameAsync();
                }
            }

            _currentSceneName = sceneName;
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!loadOperation.isDone)
            {
                await Awaitable.NextFrameAsync();
            }

            if (_loadingOverlayObject) _loadingOverlayObject.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public async void StartNewGame()
        {
            try
            {
                if (_campaignContent == null || _campaignContent.ContentEntries == null || _campaignContent.ContentEntries.Count < 1)
                {
                    Debug.LogError($"Campaign information is null or empty!");
                    return;
                }

                _campaignEntryIndex = 0;
                await LoadCurrentCampaignEntryAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Awaitable LoadCurrentCampaignEntryAsync()
        {
            CampaignContentEntry currentEntry = _campaignContent.ContentEntries[_campaignEntryIndex];

            _currentContentId = currentEntry.ContentId;

            string sceneToLoad = TitleSceneName;
            switch (currentEntry.ContentType)
            {
                case CampaignContentType.Battle:
                    sceneToLoad = "Battle";
                    break;
            }

            await LoadSceneAsync(sceneToLoad);
        }

        /// <summary>
        /// Returns to the Title screen
        /// </summary>
        public async void ReturnToTitle()
        {
            try
            {
                _campaignEntryIndex = 0;
                _currentContentId = string.Empty;
                await LoadSceneAsync(TitleSceneName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}