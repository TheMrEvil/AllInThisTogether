using System;
using System.Runtime.CompilerServices;

namespace Parse
{
	// Token: 0x0200000C RID: 12
	public struct ParseGeoDistance
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002FDF File Offset: 0x000011DF
		public ParseGeoDistance(double radians)
		{
			this = default(ParseGeoDistance);
			this.Radians = radians;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002FEF File Offset: 0x000011EF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002FF7 File Offset: 0x000011F7
		public double Radians
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Radians>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Radians>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003000 File Offset: 0x00001200
		public double Miles
		{
			get
			{
				return this.Radians * 3958.8;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003012 File Offset: 0x00001212
		public double Kilometers
		{
			get
			{
				return this.Radians * 6371.0;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003024 File Offset: 0x00001224
		public static ParseGeoDistance FromMiles(double miles)
		{
			return new ParseGeoDistance(miles / 3958.8);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003036 File Offset: 0x00001236
		public static ParseGeoDistance FromKilometers(double kilometers)
		{
			return new ParseGeoDistance(kilometers / 6371.0);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003048 File Offset: 0x00001248
		public static ParseGeoDistance FromRadians(double radians)
		{
			return new ParseGeoDistance(radians);
		}

		// Token: 0x0400000D RID: 13
		private const double EarthMeanRadiusKilometers = 6371.0;

		// Token: 0x0400000E RID: 14
		private const double EarthMeanRadiusMiles = 3958.8;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private double <Radians>k__BackingField;
	}
}
