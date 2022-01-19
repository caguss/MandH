using MandH.iOS;
using MandH.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Google.MobileAds;

using CoreGraphics;


[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace MandH.iOS

{
    class AdMobViewRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                BannerView bannerView = null;
                UIViewController viewCtrl = null;

                foreach (UIWindow v in UIApplication.SharedApplication.Windows)
                {
                    if (v.RootViewController != null)
                    {
                        viewCtrl = v.RootViewController;
                    }
                }

                bannerView = new BannerView(size: AdSizeCons.Banner, origin: new CGPoint(-10, 0))
                {
                    //AdUnitID = "ca-app-pub-4681470946279796/3043892478",
                    AdUnitID = "ca-app-pub-3940256099942544/2934735716",
                    RootViewController = viewCtrl
                };

                //bannerView.AdReceived += (sender, args) =>
                //{
                //    if (!viewOnScreen) this.AddSubview(bannerView);
                //    viewOnScreen = true;
                //};

                bannerView.LoadRequest(GetRequest());
                base.SetNativeControl(bannerView);
            }
            
            Request GetRequest()
            {
                var request = Request.GetDefaultRequest();
                // Requests test ads on devices you specify. Your test device ID is printed to the console when
                // an ad request is made. GADBannerView automatically returns test ads when running on a
                // simulator. After you get your device ID, add it here
                request.TestDevices = new[] { "96080e40efec5229aad21b540eae6fe0" };
                return request;
            }
        }

    }
}