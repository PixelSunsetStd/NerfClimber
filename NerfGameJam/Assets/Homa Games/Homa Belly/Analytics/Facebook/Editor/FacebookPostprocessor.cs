using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace HomaGames.HomaBelly
{
    public class FacebookPostprocessor
    {
        [InitializeOnLoadMethod]
        static void Configure()
        {
            HomaBellyEditorLog.Debug($"Configuring {HomaBellyFacebookConstants.ID}");
            PluginManifest pluginManifest = PluginManifest.LoadFromLocalFile();

            if (pluginManifest != null)
            {
                PackageComponent packageComponent = pluginManifest.Packages
                    .GetPackageComponent(HomaBellyFacebookConstants.ID, HomaBellyFacebookConstants.TYPE);
                if (packageComponent != null)
                {
                    Dictionary<string, string> configurationData = packageComponent.Data;

                    if (configurationData != null)
                    {
                        try
                        {
                            if (!File.Exists(Application.dataPath + "/FacebookSDK/SDK/Resources/FacebookSettings.asset"))
                            {
                                // Create Facebook settings
                                EditorApplication.ExecuteMenuItem(HomaBellyFacebookConstants.CREATE_FACEBOOK_SETTINGS_MENU);
                            }
                        }
                        catch (System.Exception)
                        {
                            // Ignore
                        }

                        // Configure app ID
                        Facebook.Unity.Settings.FacebookSettings.AppIds[0] = configurationData["s_app_id"];
                        Facebook.Unity.Settings.FacebookSettings.AppLabels[0] = Application.productName;

                        // Determine if FB should send base events or no
                        if (configurationData.ContainsKey("b_auto_log_app_events_enabled"))
                        {
                            bool autoLogAppEventsEnabled = true;
                            bool.TryParse(configurationData["b_auto_log_app_events_enabled"], out autoLogAppEventsEnabled);
                            Facebook.Unity.Settings.FacebookSettings.AutoLogAppEventsEnabled = autoLogAppEventsEnabled;
                        }
                    }
                }
            }
        }
    }
}
