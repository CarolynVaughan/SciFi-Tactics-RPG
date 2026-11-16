using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SciFiTacticsRPG.Battle
{
    /// <summary>
    /// Scriptable object that contains information needed to play specific battles
    /// that can be retrieved using an ID string
    /// </summary>
    [CreateAssetMenu(fileName = "BattleDatabase", menuName = "BattleDatabase", order = 2)]
    public class BattleDatabase : ScriptableObject
    {
        /// <summary>
        /// Dictionary containing battle information, keyed off of ID
        /// </summary>
        [SerializeField] private Dictionary<string, BattleEntry> _battlesById;

        public IReadOnlyDictionary<string, BattleEntry> BattlesById => _battlesById;
    }

    /// <summary>
    /// Detailed information about a specific battle that is necessary to play it
    /// </summary>
    [Serializable]
    public struct BattleEntry
    {
        /// <summary>
        /// Tile-based map the battle takes place on
        /// All maps are laid out with (0, 0) in the bottom left
        /// </summary>
        public Tilemap Map;
        /// <summary>
        /// Locations on the map that the player's army can be deploy in at the start of the battle
        /// How many deployment positions are available determines how many members of the player's army
        /// can be fielded in a particular battle
        /// </summary>
        public Vector2Int[] DeploymentPositions;
    }
}