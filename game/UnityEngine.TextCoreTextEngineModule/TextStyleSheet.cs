using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000042 RID: 66
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[Serializable]
	public class TextStyleSheet : ScriptableObject
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0001C81C File Offset: 0x0001AA1C
		internal List<TextStyle> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001C834 File Offset: 0x0001AA34
		public TextStyle GetStyle(int hashCode)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				this.LoadStyleDictionaryInternal();
			}
			TextStyle textStyle;
			bool flag2 = this.m_StyleLookupDictionary.TryGetValue(hashCode, out textStyle);
			TextStyle result;
			if (flag2)
			{
				result = textStyle;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001C874 File Offset: 0x0001AA74
		public TextStyle GetStyle(string name)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
			TextStyle textStyle;
			bool flag2 = this.m_StyleLookupDictionary.TryGetValue(hashCodeCaseInSensitive, out textStyle);
			TextStyle result;
			if (flag2)
			{
				result = textStyle;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001C8BB File Offset: 0x0001AABB
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001C8C8 File Offset: 0x0001AAC8
		private void LoadStyleDictionaryInternal()
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				this.m_StyleLookupDictionary = new Dictionary<int, TextStyle>();
			}
			else
			{
				this.m_StyleLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				bool flag2 = !this.m_StyleLookupDictionary.ContainsKey(this.m_StyleList[i].hashCode);
				if (flag2)
				{
					this.m_StyleLookupDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001C976 File Offset: 0x0001AB76
		public TextStyleSheet()
		{
		}

		// Token: 0x04000389 RID: 905
		[SerializeField]
		private List<TextStyle> m_StyleList = new List<TextStyle>(1);

		// Token: 0x0400038A RID: 906
		private Dictionary<int, TextStyle> m_StyleLookupDictionary;
	}
}
