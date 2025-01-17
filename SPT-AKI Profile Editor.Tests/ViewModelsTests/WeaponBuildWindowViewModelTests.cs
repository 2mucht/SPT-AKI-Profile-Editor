﻿using Newtonsoft.Json;
using NUnit.Framework;
using SPT_AKI_Profile_Editor.Core;
using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.ProfileClasses;
using SPT_AKI_Profile_Editor.Helpers;
using SPT_AKI_Profile_Editor.Tests.Hepers;
using System.IO;
using System.Linq;

namespace SPT_AKI_Profile_Editor.Tests.ViewModelsTests
{
    internal class WeaponBuildWindowViewModelTests
    {
        private static readonly TestsDialogManager dialogManager = new();
        private static readonly TestsWindowsDialogs windowsDialogs = new();
        private static readonly TestsWorker worker = new();

        [Test]
        public void InitializeCorrectlyForPmc()
        {
            WeaponBuildWindowViewModel pmcWeaponBuild = TestViewModel(StashEditMode.PMC, worker);
            Assert.Multiple(() =>
            {
                Assert.That(pmcWeaponBuild, Is.Not.Null, "WeaponBuildWindowViewModel is null");
                Assert.That(pmcWeaponBuild.Worker, Is.Not.Null, "Worker is null");
                Assert.That(pmcWeaponBuild.WindowTitle, Is.EqualTo(TestHelpers.GetTestName("WeaponBuildWindowViewModel", StashEditMode.PMC)), "Wrong WindowTitle");
                Assert.That(pmcWeaponBuild.WeaponBuild, Is.Not.Null, "WeaponBuild is null");
                Assert.That(pmcWeaponBuild.WeaponBuild.Items.Length, Is.EqualTo(4), "WeaponBuild.Items.Length is not 4");
            });
        }

        [Test]
        public void InitializeCorrectlyForScav()
        {
            WeaponBuildWindowViewModel pmcWeaponBuild = TestViewModel(StashEditMode.Scav, worker);
            Assert.Multiple(() =>
            {
                Assert.That(pmcWeaponBuild, Is.Not.Null, "WeaponBuildWindowViewModel is null");
                Assert.That(pmcWeaponBuild.Worker, Is.Not.Null, "Worker is null");
                Assert.That(pmcWeaponBuild.WindowTitle, Is.EqualTo(TestHelpers.GetTestName("WeaponBuildWindowViewModel", StashEditMode.Scav)), "Wrong WindowTitle");
                Assert.That(pmcWeaponBuild.WeaponBuild, Is.Not.Null, "WeaponBuild is null");
                Assert.That(pmcWeaponBuild.WeaponBuild.Items.Length, Is.EqualTo(6), "WeaponBuild.Items.Length is not 6");
            });
        }

        [Test]
        public void CanAddWeaponToWeaponBuildsFromPmc()
        {
            AppData.Profile.WeaponBuilds = new();
            WeaponBuildWindowViewModel pmcWeaponBuild = TestViewModel(StashEditMode.PMC, worker);
            pmcWeaponBuild.AddToWeaponBuilds.Execute(null);
            Assert.That(() => AppData.Profile.WeaponBuilds.ContainsKey(TestHelpers.GetTestName("WeaponBuildWindowViewModel", StashEditMode.PMC)), Is.True);
        }

        [Test]
        public void CanAddWeaponToWeaponBuildsFromScav()
        {
            AppData.Profile.WeaponBuilds = new();
            WeaponBuildWindowViewModel pmcWeaponBuild = TestViewModel(StashEditMode.Scav, worker);
            pmcWeaponBuild.AddToWeaponBuilds.Execute(null);
            Assert.That(() => AppData.Profile.WeaponBuilds.ContainsKey(TestHelpers.GetTestName("WeaponBuildWindowViewModel", StashEditMode.Scav)), Is.True);
        }

        [Test]
        public void CanRemoveFromPmc()
        {
            TestHelpers.LoadDatabaseAndProfile();
            var weapon = AppData.Profile.Characters.Pmc.Inventory.Items.First(x => x.IsWeapon);
            WeaponBuildWindowViewModel pmcWeaponBuild = new(weapon, StashEditMode.PMC, null, null, dialogManager, worker);
            Assert.That(pmcWeaponBuild, Is.Not.Null, "WeaponBuildWindowViewModel is null");
            pmcWeaponBuild.RemoveItem.Execute(null);
            Assert.That(() => AppData.Profile.Characters.Pmc.Inventory.Items.Any(x => x.Id == weapon.Id), Is.False);
        }

        [Test]
        public void CanRemoveFromScav()
        {
            TestHelpers.LoadDatabaseAndProfile();
            var weapon = AppData.Profile.Characters.Scav.Inventory.Items.First(x => x.IsWeapon);
            WeaponBuildWindowViewModel scavWeaponBuild = new(weapon, StashEditMode.Scav, null, null, dialogManager, worker);
            Assert.That(scavWeaponBuild, Is.Not.Null, "WeaponBuildWindowViewModel is null");
            scavWeaponBuild.RemoveItem.Execute(null);
            Assert.That(() => AppData.Profile.Characters.Scav.Inventory.Items.Any(x => x.Id == weapon.Id), Is.False);
        }

        [Test]
        public void CanExportPmcBuild()
        {
            TestHelpers.LoadDatabaseAndProfile();
            var weapon = AppData.Profile.Characters.Pmc.Inventory.Items.First(x => x.IsWeapon);
            WeaponBuildWindowViewModel pmcWeaponBuild = new(weapon, StashEditMode.PMC, null, windowsDialogs, dialogManager, worker);
            Assert.That(pmcWeaponBuild, Is.Not.Null, "WeaponBuildWindowViewModel is null");
            pmcWeaponBuild.ExportBuild.Execute(null);
            Assert.That(File.Exists(windowsDialogs.weaponBuildExportPath), "WeaponBuild not exported");
            WeaponBuild build = JsonConvert.DeserializeObject<WeaponBuild>(File.ReadAllText(windowsDialogs.weaponBuildExportPath));
            Assert.That(build, Is.Not.Null, "Unable to read exported weapon build");
            Assert.That(build.RootTpl, Is.EqualTo(weapon.Tpl), "Exported wrong weapon");
        }

        [Test]
        public void CanInitializeWorker()
        {
            WeaponBuildWindowViewModel pmcWeaponBuild = TestViewModel(StashEditMode.PMC, null);
            Assert.That(pmcWeaponBuild.Worker, Is.Not.Null);
            Assert.That(pmcWeaponBuild.Worker is Worker, Is.True);
        }

        private static WeaponBuildWindowViewModel TestViewModel(StashEditMode editMode, IWorker worker)
        {
            TestHelpers.SetupTestCharacters("WeaponBuildWindowViewModel", editMode);
            InventoryItem item = new()
            {
                Id = TestHelpers.GetTestName("WeaponBuildWindowViewModel", editMode),
                Tpl = TestHelpers.GetTestName("WeaponBuildWindowViewModel", editMode)
            };
            return new(item, editMode, null, null, dialogManager, worker);
        }
    }
}