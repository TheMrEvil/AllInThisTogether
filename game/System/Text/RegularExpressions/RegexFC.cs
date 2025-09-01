using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000203 RID: 515
	internal sealed class RegexFC
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x0003E4F3 File Offset: 0x0003C6F3
		public RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003E510 File Offset: 0x0003C710
		public RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '￿')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this.CaseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003E57F File Offset: 0x0003C77F
		public RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this.CaseInsensitive = caseInsensitive;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
		public bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this.CaseInsensitive |= fc.CaseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003E61F File Offset: 0x0003C81F
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0003E627 File Offset: 0x0003C827
		public bool CaseInsensitive
		{
			[CompilerGenerated]
			get
			{
				return this.<CaseInsensitive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CaseInsensitive>k__BackingField = value;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E630 File Offset: 0x0003C830
		public string GetFirstChars(CultureInfo culture)
		{
			if (this.CaseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x040008FF RID: 2303
		private RegexCharClass _cc;

		// Token: 0x04000900 RID: 2304
		public bool _nullable;

		// Token: 0x04000901 RID: 2305
		[CompilerGenerated]
		private bool <CaseInsensitive>k__BackingField;
	}
}
