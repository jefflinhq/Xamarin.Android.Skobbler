﻿using Android.App;
using Android.OS;
using Skobbler.SDKDemo.Application;
namespace Skobbler.SDKDemo.Activities
{
	/// <summary>
	/// Activity where offline reverse geocoding is performed
	/// </summary>
    [Activity]
	public class ReverseGeocodingActivity : Activity
	{

		private DemoApplication application;

		protected internal override void onCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_reverse_geocoding;
			application = (DemoApplication) Application;
		}

		public virtual void onClick(View v)
		{
			switch (v.Id)
			{
				case Resource.Id.reverse_geocode_button:
					SKCoordinate position = Position;
					if (position != null)
					{
						// run reverse geocoding and obtain a search result
						SKSearchResult result = SKReverseGeocoderManager.Instance.reverseGeocodePosition(position);
						// display the search result name
						string text = result != null ? result.Name : "NULL";
						if (result != null && result.ParentsList != null)
						{
							string separator = ", ";
							foreach (SKSearchResultParent parent in result.ParentsList)
							{
								text += separator + parent.ParentName;
							}
						}

						((TextView) FindViewById(Resource.Id.reverse_geocoding_result)).Text = text;
					}
					else
					{
						Toast.MakeText(this, "Invalid latitude or longitude was provided", ToastLength.Short).Show();
					}
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Gets the position from the data in the latitude and longitude fields </summary>
		/// <returns> the position, null if invalid latitude/longitude was provided </returns>
		private SKCoordinate Position
		{
			get
			{
				try
				{
					string latString = ((TextView) FindViewById(Resource.Id.latitude_field)).Text.ToString();
					string longString = ((TextView) FindViewById(Resource.Id.longitude_field)).Text.ToString();
					double latitude = double.Parse(latString);
					double longitude = double.Parse(longString);
					if (latitude > 90 || latitude < -90)
					{
						return null;
					}
					if (longitude > 180 || longitude < -180)
					{
						return null;
					}
					return new SKCoordinate(longitude, latitude);
				}
				catch (System.FormatException)
				{
					return null;
				}
			}
		}
	}
}