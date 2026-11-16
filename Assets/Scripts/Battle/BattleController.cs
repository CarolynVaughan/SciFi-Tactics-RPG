using UnityEngine;

namespace SciFiTacticsRPG.Battle
{
    /// <summary>
    /// 
    /// </summary>
    public class BattleController : MonoBehaviour
    {
        [Tooltip("Parent transform for the tilemaps used for a battle")]
        [SerializeField] private Transform _mapParent;

        [Tooltip("Database containing the information needed to play specific battles")] 
        [SerializeField] private BattleDatabase _battleDatabase;

        private void Start()
        {
            GameController gameController = GameController.Instance;
            if (gameController == null)
            {
                Debug.LogError($"Game controller is null in {nameof(BattleController)}.{nameof(Start)}!");
                return;
            }

            if (_battleDatabase == null || _battleDatabase.BattlesById.Count < 1)
            {
                Debug.LogError($"Battle database is null or empty in {nameof(BattleController)}.{nameof(Start)}! Aborting battle!");
                gameController.ReturnToTitle();
                return;
            }

            if (!_battleDatabase.BattlesById.ContainsKey(gameController.CurrentContentId))
            {
                Debug.LogError($"Battle database doesn't have battle with ID of {gameController.CurrentContentId} in {nameof(BattleController)}.{nameof(Start)}! Aborting battle!");
                gameController.ReturnToTitle();
                return;
            }

            BattleEntry battle = _battleDatabase.BattlesById[gameController.CurrentContentId];
            Instantiate(battle.Map, _mapParent);
        }
    }
}