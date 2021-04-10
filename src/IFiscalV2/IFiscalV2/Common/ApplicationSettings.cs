namespace IFiscalV2.Common
{
    using IFiscalV2.Models.Auth;
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public struct LoginSate
    {
        public const string None = "";
        public const string TryLogin = "try_login";
        public const string SelectSite = "select_site";
        public const string SelectEleccion = "select_eleccion";
        public const string LoginOk = "login_ok";

    }
    public static class ApplicationSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static void Clear()
        {
            AppSettings.Clear();
        }

        #region Setting Constants

        private const string LastUserNameSettingsKey = "last_username";
        private const string LastUserPasswordKey = "last_userpassword";
        private const string LastAccessTokenKey = "last_access_token";

        private const string SelectedSiteIdSettingsKey = "selected_siteid";
        private const string SelectedSiteNameSettingsKey = "selected_siteName";

        private const string SelectedEleccionIdSettingsKey = "selected_eleccionid";
        private const string SelectedEleccionNameSettingsKey = "selectedEeleccionName";

        private const string LastLoginStepSettingsKey = "lastLogin_step";

        private const string IsGlobalSettingsKey = "is_global";
        private const string InGlobalSettingsKey = "in_global";
        private const string InEleccionSettingsKey = "in_eleccion";

        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static UserLogin LastUserLogin
        {
            get => new UserLogin
            {
                UserName = AppSettings.GetValueOrDefault(LastUserNameSettingsKey, SettingsDefault),
                Password = AppSettings.GetValueOrDefault(LastUserPasswordKey, SettingsDefault),
            };
            set
            {
                AppSettings.AddOrUpdateValue(LastUserNameSettingsKey, value.UserName ?? SettingsDefault);
                AppSettings.AddOrUpdateValue(LastUserPasswordKey, value.Password ?? SettingsDefault);
            }
        }

        public static string LastAccessToken
        {
            get => AppSettings.GetValueOrDefault(LastAccessTokenKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(LastAccessTokenKey, (value ?? SettingsDefault));
        }

        public static string SelectedSiteId
        {
            get => AppSettings.GetValueOrDefault(SelectedSiteIdSettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SelectedSiteIdSettingsKey, (value ?? SettingsDefault));
        }

        public static string SelectedSiteName
        {
            get => AppSettings.GetValueOrDefault(SelectedSiteNameSettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SelectedSiteNameSettingsKey, (value ?? SettingsDefault));
        }

        public static string SelectedEleccionId
        {
            get => AppSettings.GetValueOrDefault(SelectedEleccionIdSettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SelectedEleccionIdSettingsKey, (value ?? SettingsDefault));
        }

        public static string SelectedEleccionName
        {
            get => AppSettings.GetValueOrDefault(SelectedEleccionNameSettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SelectedEleccionNameSettingsKey, (value ?? SettingsDefault));
        }

        public static string LastLoginResult
        {
            get => AppSettings.GetValueOrDefault(LastLoginStepSettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(LastLoginStepSettingsKey, (value ?? SettingsDefault));
        }

        public static bool IsGlobal
        {
            get => AppSettings.GetValueOrDefault(IsGlobalSettingsKey, "F").Equals("T") ? true : false;
            set => AppSettings.AddOrUpdateValue(IsGlobalSettingsKey, (value ? "T" : "F"));
        }
        public static bool InGlobal
        {
            get => AppSettings.GetValueOrDefault(InGlobalSettingsKey, "F").Equals("T") ? true : false;
            set => AppSettings.AddOrUpdateValue(InGlobalSettingsKey, (value ? "T" : "F"));
        }
        public static bool InEleccion
        {
            get => AppSettings.GetValueOrDefault(InEleccionSettingsKey, "F").Equals("T") ? true : false;
            set => AppSettings.AddOrUpdateValue(InEleccionSettingsKey, (value ? "T" : "F"));
        }


    }

}
