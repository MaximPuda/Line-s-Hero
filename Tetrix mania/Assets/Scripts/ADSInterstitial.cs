using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

[RequireComponent(typeof(ADSInitializer))]
public class ADSInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId = "Interstitial_Android";
    [SerializeField] private string iOsAdUnitId = "Interstitial_iOS";
    [SerializeField, Range(0, 1)] private float adsProbability = 0.5f;

    private string adUnitId;
    private ADSInitializer initializer;

    public static ADSInterstitial instance;

    public UnityAction onAdsShowComplete;
    public UnityAction onAdsShowFailure;

    private void Awake()
    {
        instance = this;
        initializer = GetComponent<ADSInitializer>();

        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOsAdUnitId
            : androidAdUnitId;
    }

    private void Start()
    {
        LoadAd();
    }

    private void LoadAd()
    {
        Debug.Log("Loading Ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        if (Advertisement.isInitialized)
        {
            float adsRandom = Random.Range(0, 1f);
            if (Advertisement.isInitialized && adsRandom <= adsProbability)
            {
                Debug.Log("Showing Ad: " + adUnitId);
                Advertisement.Show(adUnitId, this);
            }
            else onAdsShowComplete?.Invoke();
        }
        else initializer.InitializeAds();
    }

    public void OnUnityAdsAdLoaded(string adUnitId) { }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        onAdsShowFailure?.Invoke();
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        onAdsShowComplete?.Invoke();
        LoadAd();
    }
}