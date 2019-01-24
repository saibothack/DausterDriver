﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace DausterDriver.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string IsLoggedInTokenKey = "isloggedid_key";
        private static readonly bool IsLoggedInTokenDefault = false;

        private const string IsLoggedProccesInTokenKey = "isloggedProccesid_key";
        private static readonly bool IsLoggedProccesInTokenDefault = false;

        private const string idUserCurrentLogin = "idUserCurrentLogin";
        private static readonly int IdUserCurrentLoginToken = 0;

        private const string NameUserCurrentLogin = "isNameCurrentUser_key";
        private static readonly string NameUserCurrentLoginToken = string.Empty;

        private const string imageProfiler = "imageProfiler_key";
        private static readonly string imageProfilerToken = string.Empty;

        private const string AccessTokenCurrentUser = "isAccessToken_key";
        private static readonly string AccessTokenCurrentUserToken = string.Empty;
        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        public static bool IsLoggedIn
        {
            get { return AppSettings.GetValueOrDefault(IsLoggedInTokenKey, IsLoggedInTokenDefault); }
            set { AppSettings.AddOrUpdateValue(IsLoggedInTokenKey, value); }
        }

        public static bool IsLoggedProccesIn
        {
            get { return AppSettings.GetValueOrDefault(IsLoggedProccesInTokenKey, IsLoggedProccesInTokenDefault); }
            set { AppSettings.AddOrUpdateValue(IsLoggedProccesInTokenKey, value); }
        }

        public static int IdUserLogin
        {
            get { return AppSettings.GetValueOrDefault(idUserCurrentLogin, IdUserCurrentLoginToken); }
            set { AppSettings.AddOrUpdateValue(idUserCurrentLogin, value); }
        }

        public static string NameUserLogin
        {
            get { return AppSettings.GetValueOrDefault(NameUserCurrentLogin, NameUserCurrentLoginToken); }
            set { AppSettings.AddOrUpdateValue(NameUserCurrentLogin, value); }
        }

        public static string ImageProfiler
        {
            get { return AppSettings.GetValueOrDefault(imageProfiler, imageProfilerToken); }
            set { AppSettings.AddOrUpdateValue(imageProfiler, value); }
        }

        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault(AccessTokenCurrentUser, AccessTokenCurrentUserToken); }
            set { AppSettings.AddOrUpdateValue(AccessTokenCurrentUser, value); }
        }
    }
}
