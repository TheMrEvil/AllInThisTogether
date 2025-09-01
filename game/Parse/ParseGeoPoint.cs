using System;
using System.Collections.Generic;
using Parse.Abstractions.Infrastructure;

namespace Parse
{
	// Token: 0x0200000D RID: 13
	public struct ParseGeoPoint : IJsonConvertible
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003050 File Offset: 0x00001250
		public ParseGeoPoint(double latitude, double longitude)
		{
			this = default(ParseGeoPoint);
			this.Latitude = latitude;
			this.Longitude = longitude;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003067 File Offset: 0x00001267
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000306F File Offset: 0x0000126F
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
			set
			{
				if (value > 90.0 || value < -90.0)
				{
					throw new ArgumentOutOfRangeException("value", "Latitude must be within the range [-90, 90]");
				}
				this.latitude = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000030A0 File Offset: 0x000012A0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000030A8 File Offset: 0x000012A8
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
			set
			{
				if (value > 180.0 || value < -180.0)
				{
					throw new ArgumentOutOfRangeException("value", "Longitude must be within the range [-180, 180]");
				}
				this.longitude = value;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030DC File Offset: 0x000012DC
		public ParseGeoDistance DistanceTo(ParseGeoPoint point)
		{
			double num = 0.017453292519943295;
			double num2 = this.Latitude * num;
			double num3 = this.longitude * num;
			double num4 = point.Latitude * num;
			double num5 = point.Longitude * num;
			double num6 = num2 - num4;
			double num7 = num3 - num5;
			double num8 = Math.Sin(num6 / 2.0);
			double num9 = Math.Sin(num7 / 2.0);
			double num10 = num8 * num8 + Math.Cos(num2) * Math.Cos(num4) * num9 * num9;
			num10 = Math.Min(1.0, num10);
			return new ParseGeoDistance(2.0 * Math.Asin(Math.Sqrt(num10)));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000318C File Offset: 0x0000138C
		IDictionary<string, object> IJsonConvertible.ConvertToJSON()
		{
			return new Dictionary<string, object>
			{
				{
					"__type",
					"GeoPoint"
				},
				{
					"latitude",
					this.Latitude
				},
				{
					"longitude",
					this.Longitude
				}
			};
		}

		// Token: 0x04000010 RID: 16
		private double latitude;

		// Token: 0x04000011 RID: 17
		private double longitude;
	}
}
