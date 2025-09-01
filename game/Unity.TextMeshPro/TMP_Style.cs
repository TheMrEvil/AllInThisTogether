using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public class TMP_Style
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00026874 File Offset: 0x00024A74
		public static TMP_Style NormalStyle
		{
			get
			{
				if (TMP_Style.k_NormalStyle == null)
				{
					TMP_Style.k_NormalStyle = new TMP_Style("Normal", string.Empty, string.Empty);
				}
				return TMP_Style.k_NormalStyle;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0002689B File Offset: 0x00024A9B
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000268A3 File Offset: 0x00024AA3
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value != this.m_Name)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000268BA File Offset: 0x00024ABA
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000268C2 File Offset: 0x00024AC2
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				if (value != this.m_HashCode)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000268D4 File Offset: 0x00024AD4
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000268DC File Offset: 0x00024ADC
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000268E4 File Offset: 0x00024AE4
		public int[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000268EC File Offset: 0x00024AEC
		public int[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000268F4 File Offset: 0x00024AF4
		internal TMP_Style(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00026924 File Offset: 0x00024B24
		public void RefreshStyle()
		{
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(this.m_Name);
			int length = this.m_OpeningDefinition.Length;
			this.m_OpeningTagArray = new int[length];
			this.m_OpeningTagUnicodeArray = new uint[length];
			for (int i = 0; i < length; i++)
			{
				this.m_OpeningTagArray[i] = (int)this.m_OpeningDefinition[i];
				this.m_OpeningTagUnicodeArray[i] = (uint)this.m_OpeningDefinition[i];
			}
			int length2 = this.m_ClosingDefinition.Length;
			this.m_ClosingTagArray = new int[length2];
			this.m_ClosingTagUnicodeArray = new uint[length2];
			for (int j = 0; j < length2; j++)
			{
				this.m_ClosingTagArray[j] = (int)this.m_ClosingDefinition[j];
				this.m_ClosingTagUnicodeArray[j] = (uint)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x040003AA RID: 938
		internal static TMP_Style k_NormalStyle;

		// Token: 0x040003AB RID: 939
		[SerializeField]
		private string m_Name;

		// Token: 0x040003AC RID: 940
		[SerializeField]
		private int m_HashCode;

		// Token: 0x040003AD RID: 941
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x040003AE RID: 942
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x040003AF RID: 943
		[SerializeField]
		private int[] m_OpeningTagArray;

		// Token: 0x040003B0 RID: 944
		[SerializeField]
		private int[] m_ClosingTagArray;

		// Token: 0x040003B1 RID: 945
		[SerializeField]
		internal uint[] m_OpeningTagUnicodeArray;

		// Token: 0x040003B2 RID: 946
		[SerializeField]
		internal uint[] m_ClosingTagUnicodeArray;
	}
}
