using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADSBanner : MonoBehaviour
{
    [SerializeField] BannerPosition bannerPosition;

    [SerializeField] private string androidAdUnitId = "Banner_Android";
    [SerializeField] private string iOSAdUnitId = "Banner_iOS";
    
    private string adUnitId;
    public static ADSBanner instance;

    private void Awake()
    {
        instance = this;
        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                    ? iOSAdUnitId
                    : androidAdUnitId;
    }

    private void Start()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        StartCoroutine(LoadBannerAfterTime(1f));
    }

    private IEnumerator LoadBannerAfterTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        LoadBanner();
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(adUnitId, options);
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void OnBannerClicked() { }
    private void OnBannerShown() { }
    private void OnBannerHidden() { }
}