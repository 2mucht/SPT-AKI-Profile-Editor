﻿using Newtonsoft.Json;
using NUnit.Framework;
using SPT_AKI_Profile_Editor.Core;
using System;
using System.IO;
using System.Linq;

namespace SPT_AKI_Profile_Editor.Tests
{
    class ProfileTests
    {
        const string profileFile = @"C:\SPT\user\profiles\0a727cba469df90e68214278.json";

        [OneTimeSetUp]
        public void Setup()
        {
            AppData.AppSettings.ServerPath = @"C:\SPT";
            AppData.LoadDatabase();
            AppData.Profile.Load(profileFile);
        }

        [Test]
        public void CharactersNotNull() => Assert.IsNotNull(AppData.Profile.Characters);

        [Test]
        public void SuitsNotEmpty() => Assert.IsFalse(AppData.Profile.Suits.Length == 0);

        [Test]
        public void PmcNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc);

        [Test]
        public void ScavNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Scav);

        [Test]
        public void IdNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Aid, "Id is empty");

        [Test]
        public void InfoNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Info);

        [Test]
        public void NicknameNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Info.Nickname, "Nickname is empty");

        [Test]
        public void SideNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Info.Side, "Side is empty");

        [Test]
        public void VoiceNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Info.Voice, "Voice is empty");

        [Test]
        public void PmcExperienceNotZero() => Assert.AreNotEqual(0, AppData.Profile.Characters.Pmc.Info.Experience, "Experience is zero");

        [Test]
        public void PmcLevelNotZero() => Assert.AreNotEqual(0, AppData.Profile.Characters.Pmc.Info.Level, "Level is zero");

        [Test]
        public void ScavExperienceNotZero() => Assert.AreNotEqual(0, AppData.Profile.Characters.Scav.Info.Experience, "Experience is zero");

        [Test]
        public void ScavLevelNotZero() => Assert.AreNotEqual(0, AppData.Profile.Characters.Scav.Info.Level, "Level is zero");

        [Test]
        public void GameVersionNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Info.GameVersion, "GameVersion is empty");

        [Test]
        public void CustomizationNotNully() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Customization);

        [Test]
        public void HeadNotEmpty() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Customization.Head, "Head is empty");

        [Test]
        public void TraderStandingsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.TraderStandings.Count == 0);

        [Test]
        public void TraderStandingsNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.TraderStandings, "TraderStandings is null");

        [Test]
        public void QuestsNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Quests, "Quests is null");

        [Test]
        public void QuestsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Quests.Length == 0, "Quests is empty");

        [Test]
        public void EncyclopediaNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Encyclopedia, "Encyclopedia is null");

        [Test]
        public void EncyclopediaNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Encyclopedia.Count == 0, "Encyclopedia is empty");

        [Test]
        public void HideoutNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Hideout, "Hideout is null");

        [Test]
        public void HideoutAreasNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Hideout.Areas.Length == 0, "HideoutAreas is empty");

        [Test]
        public void PmcSkillsNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Skills, "Pmc skills is null");

        [Test]
        public void PmcCommonSkillsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Skills.Common.Length == 0, "Pmc CommonSkills is empty");

        [Test]
        public void PmcMasteringSkillsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Skills.Mastering.Length == 0, "Pmc MasteringSkills is empty");

        [Test]
        public void ScavSkillsNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Scav.Skills, "Scav skills is null");

        [Test]
        public void ScavCommonSkillsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Scav.Skills.Common.Length == 0, "Scav CommonSkills is empty");

        [Test]
        public void ScavMasteringSkillsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Scav.Skills.Mastering.Length == 0, "Scav MasteringSkills is empty");

        [Test]
        public void InventoryNotNull() => Assert.IsNotNull(AppData.Profile.Characters.Pmc.Inventory);

        [Test]
        public void InventoryStashNotEmpty() => Assert.IsNotEmpty(AppData.Profile.Characters.Pmc.Inventory.Stash);

        [Test]
        public void InventoryItemsNotEmpty() => Assert.IsFalse(AppData.Profile.Characters.Pmc.Inventory.Items.Length == 0);

        [Test]
        public void PmcPocketsNotNull() => Assert.IsNotEmpty(AppData.Profile.Characters.Pmc.Inventory.Pockets);

        [Test]
        public void ProfileSavesCorrectly()
        {
            AppData.AppSettings.AutoAddMissingQuests = false;
            AppData.AppSettings.AutoAddMissingMasterings = false;
            AppData.AppSettings.AutoAddMissingScavSkills = false;
            AppData.Profile.Load(profileFile);
            var expected = JsonConvert.DeserializeObject(File.ReadAllText(profileFile));
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.json");
            AppData.Profile.Save(profileFile, testFile);
            var result = JsonConvert.DeserializeObject(File.ReadAllText(testFile));
            Assert.AreEqual(expected.ToString(), result.ToString());
        }

        [Test]
        public void TradersSavesCorrectly()
        {
            AppData.Profile.Load(profileFile);
            AppData.ServerDatabase.SetAllTradersMax();
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testTraders.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Pmc.TraderStandings
                .Where(x => x.Key != "ragfair")
                .All(x => x.Value.LoyaltyLevel == AppData.ServerDatabase.TraderInfos[x.Key].MaxLevel));

        }

        [Test]
        public void QuestsStatusesSavesCorrectly()
        {
            AppData.AppSettings.AutoAddMissingQuests = true;
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Pmc.SetAllQuests("Fail");
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testQuests.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Pmc.Quests.All(x => x.Status == "Fail")
                && AppData.Profile.Characters.Pmc.Quests.Length == AppData.ServerDatabase.QuestsData.Count);
        }

        [Test]
        public void HideoutAreaLevelsSavesCorrectly()
        {
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Pmc.SetAllHideoutAreasMax();
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testHideouts.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Pmc.Hideout.Areas
                .All(x => x.Level == x.MaxLevel));
        }

        [Test]
        public void PmcCommonSkillsSavesCorrectly()
        {
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Pmc.SetAllCommonSkills(AppData.AppSettings.CommonSkillMaxValue);
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testPmcCommonSkills.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Pmc.Skills.Common
                .All(x => x.Id.StartsWith("Bot") || x.Progress == AppData.AppSettings.CommonSkillMaxValue));
        }

        [Test]
        public void ScavCommonSkillsSavesCorrectly()
        {
            AppData.AppSettings.AutoAddMissingScavSkills = true;
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Scav.SetAllCommonSkills(AppData.AppSettings.CommonSkillMaxValue);
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testScavCommonSkills.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Scav.Skills.Common
                .All(x => x.Id.StartsWith("Bot") || x.Progress == x.MaxValue)
                && AppData.Profile.Characters.Scav.Skills.Common.Length == AppData.Profile.Characters.Pmc.Skills.Common.Length);
        }

        [Test]
        public void PmcMasteringSkillsSavesCorrectly()
        {
            AppData.AppSettings.AutoAddMissingMasterings = true;
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Pmc.SetAllMasteringsSkills(AppData.ServerDatabase.ServerGlobals.Config.Mastering.Max(x => x.Level2 + x.Level3));
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testPmcMasteringSkills.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Pmc.Skills.Mastering
                .All(x => x.Progress == x.MaxValue)
                && AppData.Profile.Characters.Pmc.Skills.Mastering.Length == AppData.ServerDatabase.ServerGlobals.Config.Mastering.Length);
        }

        [Test]
        public void ScavMasteringSkillsSavesCorrectly()
        {
            AppData.AppSettings.AutoAddMissingMasterings = true;
            AppData.Profile.Load(profileFile);
            AppData.Profile.Characters.Scav.SetAllMasteringsSkills(AppData.ServerDatabase.ServerGlobals.Config.Mastering.Max(x => x.Level2 + x.Level3));
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testScavMasteringSkills.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Characters.Scav.Skills.Mastering
                .All(x => x.Progress == x.MaxValue)
                && AppData.Profile.Characters.Scav.Skills.Mastering.Length == AppData.ServerDatabase.ServerGlobals.Config.Mastering.Length);
        }

        [Test]
        public void ExaminedItemsSavesCorrectly()
        {
            AppData.Profile.Load(profileFile);
            var expected = AppData.Profile.Characters.Pmc.ExaminedItems.Count;
            AppData.Profile.Characters.Pmc.ExamineAll();
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testExaminedItems.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.AreNotEqual(expected, AppData.Profile.Characters.Pmc.ExaminedItems.Count);
        }

        [Test]
        public void SuitsSavesCorrectly()
        {
            AppData.Profile.Load(profileFile);
            AppData.ServerDatabase.AcquireAllClothing();
            string testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testSuits.json");
            AppData.Profile.Save(profileFile, testFile);
            AppData.Profile.Load(testFile);
            Assert.IsTrue(AppData.Profile.Suits.Length == AppData.ServerDatabase.TraderSuits.Count);
        }
    }
}