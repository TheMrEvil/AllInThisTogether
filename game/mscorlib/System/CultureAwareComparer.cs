using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	public sealed class CultureAwareComparer : StringComparer, ISerializable
	{
		// Token: 0x06000FAF RID: 4015 RVA: 0x000416C9 File Offset: 0x0003F8C9
		internal CultureAwareComparer(CultureInfo culture, CompareOptions options) : this(culture.CompareInfo, options)
		{
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000416D8 File Offset: 0x0003F8D8
		internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
		{
			this._compareInfo = compareInfo;
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			this._options = options;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00041708 File Offset: 0x0003F908
		private CultureAwareComparer(SerializationInfo info, StreamingContext context)
		{
			this._compareInfo = (CompareInfo)info.GetValue("_compareInfo", typeof(CompareInfo));
			bool boolean = info.GetBoolean("_ignoreCase");
			object valueNoThrow = info.GetValueNoThrow("_options", typeof(CompareOptions));
			if (valueNoThrow != null)
			{
				this._options = (CompareOptions)valueNoThrow;
			}
			this._options |= (boolean ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00041780 File Offset: 0x0003F980
		public override int Compare(string x, string y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return this._compareInfo.Compare(x, y, this._options);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000417A5 File Offset: 0x0003F9A5
		public override bool Equals(string x, string y)
		{
			return x == y || (x != null && y != null && this._compareInfo.Compare(x, y, this._options) == 0);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000417CB File Offset: 0x0003F9CB
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this._compareInfo.GetHashCodeOfString(obj, this._options);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000417F0 File Offset: 0x0003F9F0
		public override bool Equals(object obj)
		{
			CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
			return cultureAwareComparer != null && this._options == cultureAwareComparer._options && this._compareInfo.Equals(cultureAwareComparer._compareInfo);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00041828 File Offset: 0x0003FA28
		public override int GetHashCode()
		{
			return this._compareInfo.GetHashCode() ^ (int)(this._options & (CompareOptions)2147483647);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00041842 File Offset: 0x0003FA42
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_compareInfo", this._compareInfo);
			info.AddValue("_options", this._options);
			info.AddValue("_ignoreCase", (this._options & CompareOptions.IgnoreCase) > CompareOptions.None);
		}

		// Token: 0x040012F2 RID: 4850
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x040012F3 RID: 4851
		private readonly CompareInfo _compareInfo;

		// Token: 0x040012F4 RID: 4852
		private CompareOptions _options;
	}
}
