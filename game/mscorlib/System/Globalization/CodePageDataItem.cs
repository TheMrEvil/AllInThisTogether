using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009A1 RID: 2465
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x060058E8 RID: 22760 RVA: 0x0012B4D6 File Offset: 0x001296D6
		[SecurityCritical]
		internal CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0012B511 File Offset: 0x00129711
		[SecurityCritical]
		internal static string CreateString(string pStrings, uint index)
		{
			if (pStrings[0] == '|')
			{
				return pStrings.Split(CodePageDataItem.sep, StringSplitOptions.RemoveEmptyEntries)[(int)index];
			}
			return pStrings;
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0012B52E File Offset: 0x0012972E
		public string WebName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060058EB RID: 22763 RVA: 0x0012B55F File Offset: 0x0012975F
		public virtual int UIFamilyCodePage
		{
			get
			{
				return this.m_uiFamilyCodePage;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060058EC RID: 22764 RVA: 0x0012B567 File Offset: 0x00129767
		public string HeaderName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_headerName == null)
				{
					this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
				}
				return this.m_headerName;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060058ED RID: 22765 RVA: 0x0012B598 File Offset: 0x00129798
		public string BodyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_bodyName == null)
				{
					this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
				}
				return this.m_bodyName;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x0012B5C9 File Offset: 0x001297C9
		public uint Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0012B5D1 File Offset: 0x001297D1
		// Note: this type is marked as 'beforefieldinit'.
		static CodePageDataItem()
		{
		}

		// Token: 0x040036F7 RID: 14071
		internal int m_dataIndex;

		// Token: 0x040036F8 RID: 14072
		internal int m_uiFamilyCodePage;

		// Token: 0x040036F9 RID: 14073
		internal string m_webName;

		// Token: 0x040036FA RID: 14074
		internal string m_headerName;

		// Token: 0x040036FB RID: 14075
		internal string m_bodyName;

		// Token: 0x040036FC RID: 14076
		internal uint m_flags;

		// Token: 0x040036FD RID: 14077
		private static readonly char[] sep = new char[]
		{
			'|'
		};
	}
}
