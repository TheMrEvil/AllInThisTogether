using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	public class OrdinalComparer : StringComparer
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x00041881 File Offset: 0x0003FA81
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00041890 File Offset: 0x0003FA90
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
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000418BA File Offset: 0x0003FABA
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return x.Equals(y);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000418F5 File Offset: 0x0003FAF5
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			if (this._ignoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00041918 File Offset: 0x0003FB18
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00041940 File Offset: 0x0003FB40
		public override int GetHashCode()
		{
			int hashCode = "OrdinalComparer".GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x040012F5 RID: 4853
		private readonly bool _ignoreCase;
	}
}
