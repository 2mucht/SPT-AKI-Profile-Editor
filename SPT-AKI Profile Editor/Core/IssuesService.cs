﻿using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.Issues;
using SPT_AKI_Profile_Editor.Core.ProfileClasses;
using System.Collections.ObjectModel;
using System.Linq;
using static SPT_AKI_Profile_Editor.Core.ServerClasses.QuestData.QuestConditions.QuestCondition;

namespace SPT_AKI_Profile_Editor.Core
{
    public class IssuesService : BindableEntity
    {
        private ObservableCollection<ProfileIssue> profileIssues = new();

        public ObservableCollection<ProfileIssue> ProfileIssues { get => profileIssues; set => profileIssues = value; }

        public bool HasIssues => ProfileIssues != null && ProfileIssues.Count > 0;

        public void GetIssues()
        {
            profileIssues.Clear();
            GetDuplicateItemsIDIssues(AppData.Profile?.Characters?.Pmc?.Inventory);
            GetDuplicateItemsIDIssues(AppData.Profile?.Characters?.Scav?.Inventory);
            GetTraderIssues(AppData.Profile?.Characters?.Pmc);
            GetQuestIssues(AppData.Profile?.Characters?.Pmc);
            UpdateIssues();
        }

        private void GetDuplicateItemsIDIssues(CharacterInventory inventory)
        {
            if (inventory?.InventoryHaveDuplicatedItems ?? false)
                profileIssues.Add(new DuplicateItemsIDIssue(inventory));
        }

        public void UpdateIssues()
        {
            OnPropertyChanged("ProfileIssues");
            OnPropertyChanged("HasIssues");
        }

        public void FixAllIssues()
        {
            while (HasIssues)
            {
                profileIssues[0].FixAction();
                GetIssues();
            }
        }

        private void GetTraderIssues(Character character)
        {
            var teadersWithIssues = character?.TraderStandingsExt?.Where(x => x.TraderBase?.LoyaltyLevels[x.LoyaltyLevel - 1].MinLevel > character?.Info?.Level);
            foreach (var trader in teadersWithIssues)
                profileIssues.Add(new PMCLevelIssue(trader));
        }

        private void GetQuestIssues(Character character)
        {
            foreach (var quest in character?.Quests?.Where(x => x.QuestData != null && x.Status >= QuestStatus.AvailableForStart))
            {
                foreach (var condition in quest.QuestData.Conditions?.AvailableForStart)
                {
                    switch (condition.Type)
                    {
                        case QuestConditionType.Level:
                            if (!(condition.Props?.CheckRequiredValue(character.Info?.Level ?? 1) ?? true))
                                profileIssues.Add(new PMCLevelIssue(quest, condition.Props.GetNearestValue()));
                            break;
                        case QuestConditionType.Quest:
                            var requiredStatus = condition?.Props?.RequiredStatuses.FirstOrDefault();
                            var targetQuest = character.Quests.Where(x => x.Qid == condition.Props?.Target).FirstOrDefault();
                            if (requiredStatus != null && targetQuest!= null && targetQuest.Status < requiredStatus)
                                profileIssues.Add(new QuestStatusIssue(quest, targetQuest, (QuestStatus)requiredStatus));
                            break;
                        case QuestConditionType.TraderLoyalty:
                            var targetTrader = character.TraderStandingsExt.Where(x => x.Id == condition.Props?.Target).FirstOrDefault();
                            if (targetTrader != null && !(condition.Props?.CheckRequiredValue(targetTrader.LoyaltyLevel) ?? true))
                            {
                                var requiredLoyaltyLevel = condition.Props.GetNearestValue();
                                if (requiredLoyaltyLevel < 1)
                                    continue;
                                profileIssues.Add(new TraderLoyaltyIssue(quest, targetTrader, requiredLoyaltyLevel));
                            }
                            break;
                    }
                }
            }
        }
    }
}