using UnityEngine;

namespace HomaGames.HomaBelly
{
    public class HomaBelly : MonoBehaviour, IHomaBellyBridge
    {
        #region Public properties
        [SerializeField, Tooltip("Enable to see Debug logs")]
        private bool debugEnabled = true;
        #endregion

        #region Singleton pattern

        private static HomaBelly _instance;
        public static HomaBelly Instance
        {
            get
            {
                if (_instance == null)
                {
                    HomaGamesLog.Warning("WARNING! Homa Belly not initialized. Please ensure Homa Belly prefab is present in your scene hierarchy. Prefab can be found under Assets/Homa Games/Homa Belly/Core/Prefabs folder");
                    GameObject homaBellyGameObject = new GameObject("Homa Belly");
                    _instance = homaBellyGameObject.AddComponent<HomaBelly>();
                }

                return _instance;
            }
        }

        #endregion

        #region Private properties
#if UNITY_EDITOR && UNITY_2019
        private HomaDummyBridge homaBridge = new HomaDummyBridge();
#else
        private HomaBridge homaBridge = new HomaBridge();
#endif
        #endregion

        private void Awake()
        {
            #region Singleton pattern
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            #endregion

            // Initialization
            SetDebug(debugEnabled);
            HomaGamesLog.Debug("Instantiating Homa Belly");
            homaBridge.Initialize();
        }

        private void OnApplicationPause(bool pause)
        {
            homaBridge.OnApplicationPause(pause);
        }

        #region IHomaBellyBridge

        public void SetDebug(bool enabled)
        {
            HomaGamesLog.debugEnabled = enabled;
            homaBridge.SetDebug(enabled);
        }

        public void ValidateIntegration()
        {
            homaBridge.ValidateIntegration();
        }

        // Rewarded Video Ads
        public void ShowRewardedVideoAd(string placementId = null)
        {
            homaBridge.ShowRewardedVideoAd(placementId);
        }

        public bool IsRewardedVideoAdAvailable(string placementId = null)
        {
            return homaBridge.IsRewardedVideoAdAvailable();
        }

        // Banners
        public void LoadBanner(string placementId)
        {
            LoadBanner(BannerSize.BANNER, BannerPosition.BOTTOM, placementId);
        }

        public void LoadBanner(UnityEngine.Color bannerBackgroundColor, string placementId = null)
        {
            LoadBanner(BannerSize.BANNER, BannerPosition.BOTTOM, placementId, bannerBackgroundColor);
        }

        public void LoadBanner(BannerPosition position, string placementId = null)
        {
            LoadBanner(BannerSize.BANNER, position, placementId);
        }

        public void LoadBanner(BannerSize size, string placementId = null)
        {
            LoadBanner(size, BannerPosition.BOTTOM, placementId);
        }

        public void LoadBanner(BannerSize size = BannerSize.BANNER, BannerPosition position = BannerPosition.BOTTOM, string placementId = null, Color bannerBackgroundColor = default)
        {
            homaBridge.LoadBanner(size, position, placementId, bannerBackgroundColor);
        }

        public void ShowBanner(string placementId = null)
        {
            homaBridge.ShowBanner();
        }

        public void HideBanner(string placementId = null)
        {
            homaBridge.HideBanner();
        }

        public void DestroyBanner(string placementId = null)
        {
            homaBridge.DestroyBanner();
        }

        public void ShowInsterstitial(string placementId = null)
        {
            homaBridge.ShowInsterstitial(placementId);
        }

        public bool IsInterstitialAvailable(string placementId = null)
        {
            return homaBridge.IsInterstitialAvailable();
        }

        [HomaGames.HomaBelly.PreserveAttribute]
        public void SetUserIsAboveRequiredAge(bool consent)
        {
            homaBridge.SetUserIsAboveRequiredAge(consent);
        }

        [HomaGames.HomaBelly.PreserveAttribute]
        public void SetTermsAndConditionsAcceptance(bool consent)
        {
            homaBridge.SetTermsAndConditionsAcceptance(consent);
        }

        [HomaGames.HomaBelly.PreserveAttribute]
        public void SetAnalyticsTrackingConsentGranted(bool consent)
        {
            homaBridge.SetAnalyticsTrackingConsentGranted(consent);
        }

        [HomaGames.HomaBelly.PreserveAttribute]
        public void SetTailoredAdsConsentGranted(bool consent)
        {
            homaBridge.SetTailoredAdsConsentGranted(consent);
        }

        public void TrackInAppPurchaseEvent(string productId, string currencyCode, double unitPrice, string transactionId = null, string payload = null)
        {
            homaBridge.TrackInAppPurchaseEvent(productId, currencyCode, unitPrice, transactionId, payload);
        }

        public void TrackResourceEvent(ResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
        {
            homaBridge.TrackResourceEvent(flowType, currency, amount, itemType, itemId);
        }

        public void TrackProgressionEvent(ProgressionStatus progressionStatus, string progression, int score = 0)
        {
            homaBridge.TrackProgressionEvent(progressionStatus, progression, score);
        }

        public void TrackErrorEvent(ErrorSeverity severity, string message)
        {
            homaBridge.TrackErrorEvent(severity, message);
        }

        public void TrackDesignEvent(string eventName, float eventValue = 0)
        {
            homaBridge.TrackDesignEvent(eventName, eventValue);
        }

        public void TrackAdEvent(AdAction adAction, AdType adType, string adNetwork, string adPlacementId)
        {
            homaBridge.TrackAdEvent(adAction, adType, adNetwork, adPlacementId);
        }

        public void TrackAdRevenue(AdRevenueData adRevenueData)
        {
            homaBridge.TrackAdRevenue(adRevenueData);
        }

        #endregion
    }
}
