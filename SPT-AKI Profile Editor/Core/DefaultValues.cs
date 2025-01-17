﻿using SPT_AKI_Profile_Editor.Core.Enums;
using SPT_AKI_Profile_Editor.Core.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;

namespace SPT_AKI_Profile_Editor.Core
{
    public static class DefaultValues
    {
        public const string ColorScheme = "Light.Emerald";
        public const string PocketsContainerTpl = "557596e64bdc2dc2118b4571";
        public const string PocketsSlotId = "Pockets";
        public const string FirstPrimaryWeaponSlotId = "FirstPrimaryWeapon";
        public const string HeadwearSlotId = "Headwear";
        public const string TacticalVestSlotId = "TacticalVest";
        public const string BackpackSlotId = "Backpack";
        public const string EarpieceSlotId = "Earpiece";
        public const string SecuredContainerSlotId = "SecuredContainer";
        public const string FaceCoverSlotId = "FaceCover";
        public const string EyewearSlotId = "Eyewear";
        public const string ArmorVestSlotId = "ArmorVest";
        public const string SecondPrimaryWeaponSlotId = "SecondPrimaryWeapon";
        public const string HolsterSlotId = "Holster";
        public const string ScabbardSlotId = "Scabbard";
        public const string ArmBandSlotId = "ArmBand";
        public const bool CheckUpdates = true;
        public const string MoneysDollarsTpl = "5696686a4bdc2da3298b456a";
        public const string MoneysRublesTpl = "5449016a4bdc2d6f028b456f";
        public const string MoneysEurosTpl = "569668774bdc2da2298b4568";
        public const float CommonSkillMaxValue = 5100;
        public const IssuesAction DefaultIssuesAction = IssuesAction.AlwaysShow;

        public static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SPT-AKI Profile Editor");

        public static List<string> BannedItems => new()
        {
            "543be5dd4bdc2deb348b4569",
            "55d720f24bdc2d88028b456d",
            "557596e64bdc2dc2118b4571",
            "566965d44bdc2d814c8b4571",
            "566abbb64bdc2d144c8b457d",
            "5448f39d4bdc2d0a728b4568",
            "5943d9c186f7745a13413ac9",
            "5996f6cb86f774678763a6ca",
            "5996f6d686f77467977ba6cc",
            "5996f6fc86f7745e585b4de3",
            "5cdeb229d7f00c000e7ce174",
            "5d52cc5ba4b9367408500062"
        };

        public static List<string> BannedMasterings => new()
        {
            "MR43"
        };

        public static Dictionary<string, string> DefaultDirsList => new()
        {
            [SPTServerDir.globals] = "Aki_Data\\Server\\database\\locales\\global",
            [SPTServerDir.traders] = "Aki_Data\\Server\\database\\traders",
            [SPTServerDir.bots] = "Aki_Data\\Server\\database\\bots\\types",
            [SPTServerDir.profiles] = "user\\profiles"
        };

        public static Dictionary<string, string> DefaultFilesList => new()
        {
            [SPTServerFile.globals] = "Aki_Data\\Server\\database\\globals.json",
            [SPTServerFile.items] = "Aki_Data\\Server\\database\\templates\\items.json",
            [SPTServerFile.quests] = "Aki_Data\\Server\\database\\templates\\quests.json",
            [SPTServerFile.areas] = "Aki_Data\\Server\\database\\hideout\\areas.json",
            [SPTServerFile.handbook] = "Aki_Data\\Server\\database\\templates\\handbook.json",
            [SPTServerFile.languages] = "Aki_Data\\Server\\database\\locales\\languages.json",
            [SPTServerFile.serverexe] = "Aki.Server.exe"
        };

        public static List<AppLocalization> DefaultLocalizations => new()
        {
            new AppLocalization
            {
                Key = "en",
                Name = "English",
                Translations = new Dictionary<string, string>()
                {
                    ["button_edit"] = "Edit",
                    ["button_save"] = "Save",
                    ["button_yes"] = "Yes",
                    ["button_no"] = "No",
                    ["button_close"] = "Close",
                    ["button_select"] = "Select",
                    ["button_settings"] = "SETTINGS",
                    ["button_save_profile"] = "SAVE PROFILE",
                    ["button_reload_profile"] = "RESET CHANGES",
                    ["button_fast_mode"] = "FAST MODE",
                    ["button_faq"] = "FAQ",
                    ["progress_dialog_title"] = "Please wait ...",
                    ["progress_dialog_caption"] = "Loading data",
                    ["reload_profile_dialog_title"] = "Resetting changes",
                    ["reload_profile_dialog_caption"] = "Are you sure? All changes will be lost",
                    ["server_select"] = "Select the SPT-AKI Server directory.",
                    ["invalid_server_location_caption"] = "Error",
                    ["invalid_server_location_text"] = "SPT-AKI server not found",
                    ["invalid_server_location_description"] = "SPT-AKI server not found. Select a different folder or change the relative paths to not founded server files/folders.",
                    ["no_accounts"] = "Failed to get accounts. No accounts?!",
                    ["tab_info_title"] = "Information",
                    ["tab_info_pmc"] = "PMC",
                    ["tab_info_scav"] = "Scav",
                    ["tab_info_id"] = "ID",
                    ["tab_info_nickname"] = "Nickname",
                    ["tab_info_side"] = "Side",
                    ["tab_info_voice"] = "Voice",
                    ["tab_info_level"] = "Level",
                    ["tab_info_experience"] = "Experience",
                    ["tab_info_game_version"] = "Game Version",
                    ["tab_info_pockets"] = "Pockets",
                    ["tab_info_head"] = "Head",
                    ["tab_info_health"] = "Health",
                    ["tab_merchants_title"] = "Merchants",
                    ["tab_merchants_name"] = "Name",
                    ["tab_merchants_level"] = "Level",
                    ["tab_merchants_enabled"] = "Enabled",
                    ["tab_merchants_sales_sum"] = "Sales sum",
                    ["tab_merchants_standing"] = "Standing",
                    ["tab_quests_title"] = "Quests",
                    ["tab_quests_trader"] = "Trader",
                    ["tab_quests_name"] = "Name",
                    ["tab_quests_status"] = "Status",
                    ["tab_quests_standart_group"] ="Active tasks",
                    ["tab_quests_daily_group"] = "Operational tasks (Daily)",
                    ["tab_quests_weekly_group"] = "Operational tasks (Weekly)",
                    ["tab_quests_edit_all_button"] = "Execute",
                    ["tab_quests_mark_all"] = "Mark all quests:",
                    ["tab_no_data"] = "No data to display.\nTry to enable adding missing items in settings",
                    ["tab_settings_title"] = "Settings",
                    ["tab_settings_lang"] = "Language",
                    ["tab_settings_server"] = "SPT-AKI Server directory",
                    ["tab_settings_account"] = "Account",
                    ["tab_settings_color_scheme"] = "Color scheme",
                    ["tab_settings_auto_add_quests"] = "Adding missing quests to the profile",
                    ["tab_settings_auto_add_masterings"] = "Adding missing weapon masterings to profile",
                    ["tab_settings_auto_add_scav_skills"] = "Adding missing Scav skills to the profile",
                    ["tab_settings_main"] = "Main",
                    ["tab_settings_additional"] = "Additional",
                    ["tab_settings_troubleshooting"] = "Troubleshooting",
                    ["tab_settings_appdata"] = "Open app data folder",
                    ["tab_settings_reset_settings"] = "Reset settings",
                    ["tab_settings_reset_localizations"] = "Reset localizations",
                    ["tab_settings_issues_action"] = "Default action for profile issues",
                    ["tab_settings_check_updates"] = "Check updates",
                    ["tab_hideout_title"] = "Hideout",
                    ["tab_hideout_area"] = "Area",
                    ["tab_hideout_level"] = "Level",
                    ["tab_hideout_maximum_button"] = "Set everything to maximum",
                    ["tab_skills_title"] = "Skills",
                    ["tab_skills_skill"] = "Skill",
                    ["tab_skills_exp"] = "Experience",
                    ["tab_skills_set_all"] = "Set experience for all skills:",
                    ["tab_mastering_title"] = "Weapon mastering",
                    ["tab_mastering_weapon"] = "Weapon",
                    ["tab_mastering_exp"] = "Experience",
                    ["tab_mastering_set_all"] = "Set experience for all weapons:",
                    ["tab_examined_items_title"] = "Examined items",
                    ["tab_examined_items_item"] = "Item",
                    ["tab_examined_items_examine_all_button"] = "Examine all",
                    ["tab_stash_title"] = "Stash",
                    ["tab_stash_items"] = "Items",
                    ["tab_stash_dialog_money"] = "Enter the amount of money you want to add",
                    ["tab_stash_no_slots"] = "Not enough free slots",
                    ["tab_stash_add"] = "Add",
                    ["tab_stash_open"] = "Open",
                    ["tab_stash_inspect"] = "Inspect",
                    ["tab_stash_remove"] = "Remove all",
                    ["tab_stash_fir"] = "Item found in raid",
                    ["tab_stash_items_adding"] = "Adding items",
                    ["tab_stash_pmc_equipment"] = "PMC Equipment",
                    ["tab_stash_scav_equipment"] = "Scav Equipment",
                    ["tab_stash_is_modded_item"] = "This item was added by mod",
                    ["tab_stash_search"] = "Search",
                    ["tab_stash_empty_slot"] = "Empty slot",
                    ["tab_stash_no_items"] = "There are no items",
                    ["tab_stash_global_items_presets"] = "Global items presets",
                    ["tab_backups_title"] = "Backups",
                    ["tab_backups_date"] = "Date",
                    ["tab_backups_actions"] = "Actions",
                    ["tab_backups_restore"] = "Restore",
                    ["tab_backups_remove"] = "Remove",
                    ["tab_backups_empty"] = "There are no backups",
                    ["tab_about_title"] = "About",
                    ["tab_about_text"] = "Program for editing player profile on the SPT-AKI server",
                    ["tab_about_developer"] = "Developer:",
                    ["tab_about_latest_version"] = "Latest version:",
                    ["tab_about_support"] = "Support the developer:",
                    ["tab_about_steam_trade_url"] = "Steam Trade Offer Link",
                    ["tab_about_spt_aki_project_url"] = "SPT-AKI Project Site",
                    ["save_profile_dialog_title"] = "Saving a profile",
                    ["save_profile_dialog_caption"] = "Profile saved successfully",
                    ["save_profile_dialog_ok"] = "OK",
                    ["remove_backup_dialog_title"] = "Deleting a backup",
                    ["remove_backup_dialog_caption"] = "Are you sure want to delete this backup?",
                    ["restore_backup_dialog_title"] = "Restoring a backup",
                    ["restore_backup_dialog_caption"] = "Are you sure want to restore this backup?",
                    ["remove_stash_item_title"] = "Removing an item",
                    ["remove_stash_item_caption"] = "Are you sure you want to delete this item?",
                    ["remove_stash_items_caption"] = "Are you sure you want to delete all items?",
                    ["remove_equipment_items_caption"] = "Are you sure you want to delete all equipment?",
                    ["tab_stash_mod_items"] = "The stash contains items added by mods.\nFunctions for adding money and items are disabled.\nTo use them, remove these items, or move them to the container.",
                    ["profile_empty"] = "There is no data to display. The profile is empty. Log into the game under this profile and try again.",
                    ["app_quit"] = "Quit application?",
                    ["button_quit"] = "Quit",
                    ["tab_clothing_title"] = "Clothing",
                    ["tab_clothing_acquired"] = "Acquired",
                    ["tab_clothing_acquire_all"] = "Acquire all",
                    ["server_runned"] = "The server you selected is currently running. Shut down the server and restart the program.",
                    ["update_avialable"] = "Update available",
                    ["tab_presets_title"] = "Presets",
                    ["tab_presets_user"] = "User",
                    ["tab_presets_global"] = "Global",
                    ["tab_presets_export"] = "Export",
                    ["tab_presets_import"] = "Import",
                    ["tab_presets_add_to_stash"] = "Add to stash",
                    ["tab_presets_empty"] = "Profile does not have any weapon builds",
                    ["tab_presets_wrong_file"] = "This file does not contain the weapon build",
                    ["remove_preset_dialog_title"] = "Deleting a preset",
                    ["remove_preset_dialog_caption"] = "Are you sure want to delete this preset?",
                    ["tab_presets_export_all"] = "Export all",
                    ["remove_presets_dialog_caption"] = "Are you sure want to delete all presets?",
                    ["tab_presets_ergonomics"] = "Ergonomics",
                    ["tab_preset_recoil_up"] = "Recoil Up",
                    ["tab_preset_recoil_back"] = "Recoil Back",
                    ["tab_preset_parts"] = "Weapon parts & mods",
                    ["tab_presets_modded_items"] = "Weapon preset contains items added by mods",
                    ["update_dialog_version"] = "Version",
                    ["update_dialog_publish_date"] = "Publish date",
                    ["update_dialog_size"] = "Size",
                    ["update_dialog_downloads"] = "Downloads count",
                    ["update_dialog_download_button"] = "Download",
                    ["update_dialog_open_button"] = "Open GitHub",
                    ["download_dialog_title"] = "File download",
                    ["button_cancel"] = "Cancel",
                    ["download_dialog_downloaded"] = "Downloaded:",
                    ["download_dialog_success"] = "File downloaded successfully",
                    ["issue_action_alwaysshow"] = "Always show",
                    ["issue_action_alwaysfix"] = "Always fix",
                    ["issue_action_alwaysignore"] = "Always ignore",
                    ["profile_issues_title"] = "Profile issues",
                    ["profile_issues_fix_all"] = "Fix all and save",
                    ["profile_issues_ignore"] = "Ignore all and save",
                    ["profile_issues_remember_action"] = "Remember as default action",
                    ["profile_issues_fix_command"] = "Fix",
                    ["profile_issues_pmc_level_issue_trader"] = "Trader {0} requires PMC level {1}",
                    ["profile_issues_pmc_level_issue_quest"] = "For quest {0} ({1}) requires PMC level {2}",
                    ["profile_issues_duplicate_items_id_issue"] = "{0} stash contains items with the same ID",
                    ["profile_issues_quest_status_issue_quest"] = "For quest {0} ({1}) requires quest {2} ({3}) with status {4}",
                    ["profile_issues_trader_loyalty_issue_quest"] = "For quest {0} ({1}) requires merchant {2} with level {3}",
                    ["weapon_build_window_export"] = "Export build",
                    ["weapon_build_window_add"] = "Add to builds",
                    ["container_window_content"] = "Contents",
                    ["localization_editor_title"] = "Localization editor",
                    ["localization_editor_key"] = "Key",
                    ["localization_editor_value"] = "Translation",
                    ["windows_dialogs_json_file"] = "JSON file",
                    ["server_path_editor_dialog_description"] = "Description",
                    ["server_path_editor_dialog_path"] = "Relative path",
                    ["dir_globals_description"] = "Folder containing all localizations",
                    ["dir_traders_description"] = "Folder containing merchant settings",
                    ["dir_bots_description"] = "Folder containing bot type settings",
                    ["dir_profiles_description"] = "Folder containing profiles",
                    ["file_globals_description"] = "File with main settings of server",
                    ["file_items_description"] = "File with main settings of the items",
                    ["file_quests_description"] = "File with main settings of quests",
                    ["file_areas_description"] = "File with main settings of hideout areas",
                    ["file_handbook_description"] = "File with a list of categories and items in handbook",
                    ["file_languages_description"] = "File with list of localizations",
                    ["file_serverexe_description"] = "Server executable"
                }
            },
            new AppLocalization
            {
                Key = "ru",
                Name = "Русский",
                Translations = new Dictionary<string, string>()
                {
                    ["button_edit"] = "Редактировать",
                    ["button_save"] = "Сохранить",
                    ["button_yes"] = "Да",
                    ["button_no"] = "Нет",
                    ["button_close"] = "Закрыть",
                    ["button_select"] = "Выбрать",
                    ["button_settings"] = "НАСТРОЙКИ",
                    ["button_save_profile"] = "СОХРАНИТЬ ПРОФИЛЬ",
                    ["button_reload_profile"] = "СБРОСИТЬ ИЗМЕНЕНИЯ",
                    ["button_fast_mode"] = "БЫСТРЫЙ РЕЖИМ",
                    ["button_faq"] = "ЧАВО",
                    ["progress_dialog_title"] = "Пожалуйста подождите ...",
                    ["progress_dialog_caption"] = "Идет загрузка данных",
                    ["reload_profile_dialog_title"] = "Сброс изменений",
                    ["reload_profile_dialog_caption"] = "Вы уверены? Все изменения будут потеряны",
                    ["server_select"] = "Выберите папку с сервером SPT-AKI.",
                    ["invalid_server_location_caption"] = "Ошибка",
                    ["invalid_server_location_text"] = "Сервер SPT-AKI не найден",
                    ["invalid_server_location_description"] = "Сервер SPT-AKI не найден. Выберите другую папку или измените относительные пути к не найденным файлам/папкам сервера.",
                    ["no_accounts"] = "Не удалось получить аккаунты. Нет аккаунтов?!",
                    ["tab_info_title"] = "Информация",
                    ["tab_info_pmc"] = "ЧВК",
                    ["tab_info_scav"] = "Дикий",
                    ["tab_info_id"] = "ID",
                    ["tab_info_nickname"] = "Никнейм",
                    ["tab_info_side"] = "Сторона",
                    ["tab_info_voice"] = "Голос",
                    ["tab_info_level"] = "Уровень",
                    ["tab_info_experience"] = "Опыт",
                    ["tab_info_game_version"] = "Версия игры",
                    ["tab_info_pockets"] = "Карманы",
                    ["tab_info_head"] = "Голова",
                    ["tab_info_health"] = "Здоровье",
                    ["tab_merchants_title"] = "Торговцы",
                    ["tab_merchants_name"] = "Имя",
                    ["tab_merchants_level"] = "Уровень",
                    ["tab_merchants_enabled"] = "Включен",
                    ["tab_merchants_sales_sum"] = "Сумма оборота",
                    ["tab_merchants_standing"] = "Отношение",
                    ["tab_quests_title"] = "Квесты",
                    ["tab_quests_trader"] = "Торговец",
                    ["tab_quests_name"] = "Название",
                    ["tab_quests_status"] = "Статус",
                    ["tab_quests_standart_group"] = "Текущие задачи",
                    ["tab_quests_daily_group"] = "Оперативные задачи (Ежедневные)",
                    ["tab_quests_weekly_group"] = "Оперативные задачи (Еженедельные)",
                    ["tab_quests_edit_all_button"] = "Выполнить",
                    ["tab_quests_mark_all"] = "Отметить все квесты:",
                    ["tab_no_data"] = "Нет данных для отображения.\nПопробуйте включить добавление отсутствующих элементов в настройках",
                    ["tab_settings_title"] = "Настройки",
                    ["tab_settings_lang"] = "Язык",
                    ["tab_settings_server"] = "Каталог SPT-AKI Server",
                    ["tab_settings_account"] = "Аккаунт",
                    ["tab_settings_color_scheme"] = "Цветовая схема",
                    ["tab_settings_auto_add_quests"] = "Добавление отсутствующих квестов в профиль",
                    ["tab_settings_auto_add_masterings"] = "Добавление отсутствующих навыков владения оружием в профиль",
                    ["tab_settings_auto_add_scav_skills"] = "Добавление отсутствующих умений Дикого в профиль",
                    ["tab_settings_main"] = "Основное",
                    ["tab_settings_additional"] = "Дополнительное",
                    ["tab_settings_troubleshooting"] = "Устранение проблем",
                    ["tab_settings_appdata"] = "Открыть папку данных приложения",
                    ["tab_settings_reset_settings"] = "Сброс настроек",
                    ["tab_settings_reset_localizations"] = "Сброс локализаций",
                    ["tab_settings_issues_action"] = "Действие по умолчанию для проблем с профилем",
                    ["tab_settings_check_updates"] = "Проверка обновлений",
                    ["tab_hideout_title"] = "Убежище",
                    ["tab_hideout_area"] = "Зона",
                    ["tab_hideout_level"] = "Уровень",
                    ["tab_hideout_maximum_button"] = "Установить все на максимум",
                    ["tab_skills_title"] = "Умения",
                    ["tab_skills_skill"] = "Умение",
                    ["tab_skills_exp"] = "Опыт",
                    ["tab_skills_set_all"] = "Установить опыт для всех умений:",
                    ["tab_mastering_title"] = "Владение оружием",
                    ["tab_mastering_weapon"] = "Оружие",
                    ["tab_mastering_exp"] = "Опыт",
                    ["tab_mastering_set_all"] = "Установить опыт для всего оружия:",
                    ["tab_examined_items_title"] = "Изученные предметы",
                    ["tab_examined_items_item"] = "Предмет",
                    ["tab_examined_items_examine_all_button"] = "Изучить все",
                    ["tab_stash_title"] = "Схрон",
                    ["tab_stash_items"] = "Предметы",
                    ["tab_stash_dialog_money"] = "Введите сумму денег, которую хотите добавить",
                    ["tab_stash_no_slots"] = "Недостаточно свободных слотов",
                    ["tab_stash_add"] = "Добавить",
                    ["tab_stash_open"] = "Открыть",
                    ["tab_stash_inspect"] = "Осмотреть",
                    ["tab_stash_remove"] = "Удалить все",
                    ["tab_stash_fir"] = "Предмет найденный в рейде",
                    ["tab_stash_items_adding"] = "Добавление предметов",
                    ["tab_stash_pmc_equipment"] = "Снаряжение ЧВК",
                    ["tab_stash_scav_equipment"] = "Снаряжение Дикого",
                    ["tab_stash_is_modded_item"] = "Этот предмет добавлен модом",
                    ["tab_stash_search"] = "Поиск",
                    ["tab_stash_empty_slot"] = "Пустой слот",
                    ["tab_stash_no_items"] = "Предметы отсутствуют",
                    ["tab_stash_global_items_presets"] = "Глабальные пресеты предметов",
                    ["tab_backups_title"] = "Бэкапы",
                    ["tab_backups_date"] = "Дата",
                    ["tab_backups_actions"] = "Действия",
                    ["tab_backups_restore"] = "Восстановить",
                    ["tab_backups_remove"] = "Удалить",
                    ["tab_backups_empty"] = "Бэкапы отсутствуют",
                    ["tab_about_title"] = "О программе",
                    ["tab_about_text"] = "Программа для редактирования профиля игрока на сервере SPT-AKI",
                    ["tab_about_developer"] = "Разработчик:",
                    ["tab_about_latest_version"] = "Последняя версия:",
                    ["tab_about_support"] = "Поддержать разработчика:",
                    ["tab_about_steam_trade_url"] = "Ссылка на обмен Steam",
                    ["tab_about_spt_aki_project_url"] = "Сайт проекта SPT-AKI",
                    ["save_profile_dialog_title"] = "Сохранение профиля",
                    ["save_profile_dialog_caption"] = "Профиль успешно сохранен",
                    ["save_profile_dialog_ok"] = "OK",
                    ["remove_backup_dialog_title"] = "Удаление бэкапа",
                    ["remove_backup_dialog_caption"] = "Вы действительно хотите удалить этот бэкап?",
                    ["restore_backup_dialog_title"] = "Восстановление бэкапа",
                    ["restore_backup_dialog_caption"] = "Вы действительно хотите восстановить этот бэкап?",
                    ["remove_stash_item_title"] = "Удаление предмета",
                    ["remove_stash_item_caption"] = "Вы действительно хотите удалить этот предмет?",
                    ["remove_stash_items_caption"] = "Вы действительно хотите удалить все предметы?",
                    ["remove_equipment_items_caption"] = "Вы действительно хотите удалить все снаряжение?",
                    ["tab_stash_mod_items"] = "Схрон содержит предметы добавленные модами.\nФункции добавления денег и предметов отключены.\nЧто бы воспользоваться ими удалите эти предметы, или переместите их в контейнер.",
                    ["profile_empty"] = "Нет данных для отображения. Профиль пустой. Зайдите в игру под этим профилем и попробуйте снова.",
                    ["app_quit"] = "Выйти из приложения?",
                    ["button_quit"] = "Выход",
                    ["tab_clothing_title"] = "Одежда",
                    ["tab_clothing_acquired"] = "Приобретено",
                    ["tab_clothing_acquire_all"] = "Получить все",
                    ["server_runned"] = "Выбранный вами сервер запущен в данный момент. Выключите сервер и перезапустите программу.",
                    ["update_avialable"] = "Доступно обновление",
                    ["tab_presets_title"] = "Сборки",
                    ["tab_presets_user"] = "Пользовательские",
                    ["tab_presets_global"] = "Глобальные",
                    ["tab_presets_export"] = "Экспорт",
                    ["tab_presets_import"] = "Импорт",
                    ["tab_presets_add_to_stash"] = "Добавить в схрон",
                    ["tab_presets_empty"] = "В профиле отсутствуют сборки оружия",
                    ["tab_presets_wrong_file"] = "Этот файл не содержит сборку оружия",
                    ["remove_preset_dialog_title"] = "Удаление сборки",
                    ["remove_preset_dialog_caption"] = "Вы действительно хотите удалить эту сборку?",
                    ["tab_presets_export_all"] = "Экспортировать все",
                    ["remove_presets_dialog_caption"] = "Вы действительно хотите удалить все сборки?",
                    ["tab_presets_ergonomics"] = "Эргономика",
                    ["tab_preset_recoil_up"] = "Вертикальная отдача",
                    ["tab_preset_recoil_back"] = "Горизонтальная отдача",
                    ["tab_preset_parts"] = "Оружейные части и моды",
                    ["tab_presets_modded_items"] = "Сборка содержит предметы добавленные модами",
                    ["update_dialog_version"] = "Версия",
                    ["update_dialog_publish_date"] = "Дата публикации",
                    ["update_dialog_size"] = "Размер",
                    ["update_dialog_downloads"] = "Количество скачиваний",
                    ["update_dialog_download_button"] = "Скачать",
                    ["update_dialog_open_button"] = "Открыть GitHub",
                    ["download_dialog_title"] = "Скачивание файла",
                    ["button_cancel"] = "Отмена",
                    ["download_dialog_downloaded"] = "Скачано:",
                    ["download_dialog_success"] = "Файл успешно скачан",
                    ["issue_action_alwaysshow"] = "Всегда показывать",
                    ["issue_action_alwaysfix"] = "Всегда исправлять",
                    ["issue_action_alwaysignore"] = "Всегда игнорировать",
                    ["profile_issues_title"] = "Проблемы с профилем",
                    ["profile_issues_fix_all"] = "Исправить все и сохранить",
                    ["profile_issues_ignore"] = "Игнорировать все и сохранить",
                    ["profile_issues_remember_action"] = "Запомнить как действие по умолчанию",
                    ["profile_issues_fix_command"] = "Исправить",
                    ["profile_issues_pmc_level_issue_trader"] = "Для трейдера {0} требуется ЧВК уровня {1}",
                    ["profile_issues_pmc_level_issue_quest"] = "Для квеста {0} ({1}) требуется ЧВК уровня {2}",
                    ["profile_issues_duplicate_items_id_issue"] = "Схрон {0} содержит предметы с одинаковыми ID",
                    ["profile_issues_quest_status_issue_quest"] = "Для квеста {0} ({1}) требуется квест {2} ({3}) со статусом {4}",
                    ["profile_issues_trader_loyalty_issue_quest"] = "Для квеста {0} ({1}) требуется торговец {2} со уровнем {3}",
                    ["weapon_build_window_export"] = "Экспорт сборки",
                    ["weapon_build_window_add"] = "Добавить в сборки",
                    ["container_window_content"] = "Содержимое",
                    ["localization_editor_title"] = "Редактор локализации",
                    ["localization_editor_key"] = "Ключ",
                    ["localization_editor_value"] = "Перевод",
                    ["windows_dialogs_json_file"] = "Файл JSON",
                    ["server_path_editor_dialog_description"] = "Описание",
                    ["server_path_editor_dialog_path"] = "Относительный путь",
                    ["dir_globals_description"] = "Папка содержащая все локализации",
                    ["dir_traders_description"] = "Папка содержащая настройки торговцев",
                    ["dir_bots_description"] = "Папка содержащая настройки типов ботов",
                    ["dir_profiles_description"] = "Папка содержащая профили",
                    ["file_globals_description"] = "Файл с основными настройками сервера",
                    ["file_items_description"] = "Файл с основными настройками предметов",
                    ["file_quests_description"] = "Файл с основными настройками квестов",
                    ["file_areas_description"] = "Файл с основными настройками зон убежища",
                    ["file_handbook_description"] = "Файл со списком категорий и предметов в справочнике",
                    ["file_languages_description"] = "Файл со списком локализаций",
                    ["file_serverexe_description"] = "Исполняемый файл сервера"
                }
            }
        };
    }
}