using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using MandH.Droid;
using MandH.Views;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace MandH.Droid
{
	public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
	{
		public AdMobViewRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null && Control == null)
			{
				SetNativeControl(CreateAdView());
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(AdView.AdUnitId))
				Control.AdUnitId = "ca-app-pub-4582877447179169/8068729576";
		}

		private AdView CreateAdView()
		{
			var adView = new AdView(Context)
			{
				AdSize = AdSize.Banner,
				AdUnitId =  "ca-app-pub-4582877447179169/8068729576"
			};

			adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

			adView.LoadAd(new AdRequest
							.Builder()
							.Build());

			return adView;
		}

	}
}