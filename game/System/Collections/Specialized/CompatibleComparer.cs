using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x0600274F RID: 10063 RVA: 0x0008884B File Offset: 0x00086A4B
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00088864 File Offset: 0x00086A64
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000888D4 File Offset: 0x00086AD4
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x000888FF File Offset: 0x00086AFF
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00088907 File Offset: 0x00086B07
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0008890F File Offset: 0x00086B0F
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x00088932 File Offset: 0x00086B32
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x04001539 RID: 5433
		private IComparer _comparer;

		// Token: 0x0400153A RID: 5434
		private static volatile IComparer defaultComparer;

		// Token: 0x0400153B RID: 5435
		private IHashCodeProvider _hcp;

		// Token: 0x0400153C RID: 5436
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
