using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;

public class BannerAd : MonoBehaviour
{
    private Yodo1U3dBannerAdView bannerAdView;
    // Start is called before the first frame update
    void Start()
    {
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) => { });
        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);
        Yodo1U3dMas.InitializeSdk();

        this.RequestBanner();
    }
    private void RequestBanner()
    {
        // Clean up banner before reusing
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerBottom | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);
        bannerAdView.LoadAd();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
