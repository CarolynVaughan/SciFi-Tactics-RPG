using System;
using System.Collections.Generic;
using UnityEngine;

namespace SciFiTacticsRPG
{
    /// <summary>
    /// Scriptable object that organizes battles and other content into a sequential campaign
    /// for players to experience
    /// </summary>
    [CreateAssetMenu(fileName = "MainCampaign", menuName = "CampaignContentList", order = 1)]
    public class CampaignContentList : ScriptableObject
    {
        /// <summary>
        /// Ordered list of battles and other content for players to play through for this campaign
        /// </summary>
        [SerializeField] private List<CampaignContentEntry> _contentEntries;
        public IReadOnlyList<CampaignContentEntry> ContentEntries => _contentEntries;
    }

    /// <summary>
    /// Basic information about a specific segment of the campaign
    /// </summary>
    [Serializable]
    public struct CampaignContentEntry
    {
        /// <summary>
        /// Type of content this segment of the campaign contains
        /// </summary>
        public CampaignContentType ContentType;
        /// <summary>
        /// ID used to retrieve the necessary information to present this segment of the campaign
        /// from a more content type specific database
        /// </summary>
        public string ContentId;
    }

    /// <summary>
    /// The types of content that can appear in a campaign
    /// </summary>
    public enum CampaignContentType
    {
        Battle
    }
}