using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000433 RID: 1075
	[NativeHeader("Runtime/Director/Core/FrameRate.h")]
	[UsedByNativeCode("FrameRate")]
	internal struct FrameRate : IEquatable<FrameRate>
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x0003F3D7 File Offset: 0x0003D5D7
		public bool dropFrame
		{
			get
			{
				return this.m_Rate < 0;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x0003F3E2 File Offset: 0x0003D5E2
		public double rate
		{
			get
			{
				return this.dropFrame ? ((double)(-(double)this.m_Rate) * 0.999000999000999) : ((double)this.m_Rate);
			}
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x0003F407 File Offset: 0x0003D607
		public FrameRate(uint frameRate = 0U, bool drop = false)
		{
			this.m_Rate = (int)((drop ? uint.MaxValue : 1U) * frameRate);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x0003F41C File Offset: 0x0003D61C
		public bool IsValid()
		{
			return this.m_Rate != 0;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x0003F438 File Offset: 0x0003D638
		public bool Equals(FrameRate other)
		{
			return this.m_Rate == other.m_Rate;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0003F458 File Offset: 0x0003D658
		public override bool Equals(object obj)
		{
			return obj is FrameRate && this.Equals((FrameRate)obj);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x0003F481 File Offset: 0x0003D681
		public static bool operator ==(FrameRate a, FrameRate b)
		{
			return a.Equals(b);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0003F48B File Offset: 0x0003D68B
		public static bool operator !=(FrameRate a, FrameRate b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x0003F498 File Offset: 0x0003D698
		public static bool operator <(FrameRate a, FrameRate b)
		{
			return a.rate < b.rate;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0003F4AA File Offset: 0x0003D6AA
		public static bool operator <=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0003F4BF File Offset: 0x0003D6BF
		public static bool operator >(FrameRate a, FrameRate b)
		{
			return a.rate > b.rate;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0003F4AA File Offset: 0x0003D6AA
		public static bool operator >=(FrameRate a, FrameRate b)
		{
			return a.rate <= b.rate;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0003F4D4 File Offset: 0x0003D6D4
		public override int GetHashCode()
		{
			return this.m_Rate;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0003F4EC File Offset: 0x0003D6EC
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0003F508 File Offset: 0x0003D708
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0003F524 File Offset: 0x0003D724
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = (this.dropFrame ? "F2" : "F0");
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("{0} Fps", new object[]
			{
				this.rate.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0003F590 File Offset: 0x0003D790
		internal static int FrameRateToInt(FrameRate framerate)
		{
			return framerate.m_Rate;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0003F5A8 File Offset: 0x0003D7A8
		internal static FrameRate DoubleToFrameRate(double framerate)
		{
			uint num = (uint)Math.Ceiling(framerate);
			bool flag = num <= 0U;
			FrameRate result;
			if (flag)
			{
				result = new FrameRate(1U, false);
			}
			else
			{
				FrameRate frameRate = new FrameRate(num, true);
				bool flag2 = Math.Abs(framerate - frameRate.rate) < Math.Abs(framerate - num);
				if (flag2)
				{
					result = frameRate;
				}
				else
				{
					result = new FrameRate(num, false);
				}
			}
			return result;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x0003F60C File Offset: 0x0003D80C
		// Note: this type is marked as 'beforefieldinit'.
		static FrameRate()
		{
		}

		// Token: 0x04000E02 RID: 3586
		[Ignore]
		public static readonly FrameRate k_24Fps = new FrameRate(24U, false);

		// Token: 0x04000E03 RID: 3587
		[Ignore]
		public static readonly FrameRate k_23_976Fps = new FrameRate(24U, true);

		// Token: 0x04000E04 RID: 3588
		[Ignore]
		public static readonly FrameRate k_25Fps = new FrameRate(25U, false);

		// Token: 0x04000E05 RID: 3589
		[Ignore]
		public static readonly FrameRate k_30Fps = new FrameRate(30U, false);

		// Token: 0x04000E06 RID: 3590
		[Ignore]
		public static readonly FrameRate k_29_97Fps = new FrameRate(30U, true);

		// Token: 0x04000E07 RID: 3591
		[Ignore]
		public static readonly FrameRate k_50Fps = new FrameRate(50U, false);

		// Token: 0x04000E08 RID: 3592
		[Ignore]
		public static readonly FrameRate k_60Fps = new FrameRate(60U, false);

		// Token: 0x04000E09 RID: 3593
		[Ignore]
		public static readonly FrameRate k_59_94Fps = new FrameRate(60U, true);

		// Token: 0x04000E0A RID: 3594
		[SerializeField]
		private int m_Rate;
	}
}
