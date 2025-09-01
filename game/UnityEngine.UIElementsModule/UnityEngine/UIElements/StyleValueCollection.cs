using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000278 RID: 632
	internal class StyleValueCollection
	{
		// Token: 0x060013C2 RID: 5058 RVA: 0x000560A0 File Offset: 0x000542A0
		public StyleLength GetStyleLength(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleLength result;
			if (flag)
			{
				result = new StyleLength(styleValue.length, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000560E4 File Offset: 0x000542E4
		public StyleFloat GetStyleFloat(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFloat result;
			if (flag)
			{
				result = new StyleFloat(styleValue.number, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00056128 File Offset: 0x00054328
		public StyleInt GetStyleInt(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleInt result;
			if (flag)
			{
				result = new StyleInt((int)styleValue.number, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0005616C File Offset: 0x0005436C
		public StyleColor GetStyleColor(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleColor result;
			if (flag)
			{
				result = new StyleColor(styleValue.color, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000561B0 File Offset: 0x000543B0
		public StyleBackground GetStyleBackground(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				Texture2D texture2D = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null;
				bool flag2 = texture2D != null;
				if (flag2)
				{
					return new StyleBackground(texture2D, styleValue.keyword);
				}
				Sprite sprite = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Sprite) : null;
				bool flag3 = sprite != null;
				if (flag3)
				{
					return new StyleBackground(sprite, styleValue.keyword);
				}
				VectorImage vectorImage = styleValue.resource.IsAllocated ? (styleValue.resource.Target as VectorImage) : null;
				bool flag4 = vectorImage != null;
				if (flag4)
				{
					return new StyleBackground(vectorImage, styleValue.keyword);
				}
			}
			return StyleKeyword.Null;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000562AC File Offset: 0x000544AC
		public StyleFont GetStyleFont(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFont result;
			if (flag)
			{
				Font v = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null;
				result = new StyleFont(v, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005630C File Offset: 0x0005450C
		public StyleFontDefinition GetStyleFontDefinition(StylePropertyId id)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = this.TryGetStyleValue(id, ref styleValue);
			StyleFontDefinition result;
			if (flag)
			{
				object obj = styleValue.resource.IsAllocated ? styleValue.resource.Target : null;
				result = new StyleFontDefinition(obj, styleValue.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00056368 File Offset: 0x00054568
		public bool TryGetStyleValue(StylePropertyId id, ref StyleValue value)
		{
			value.id = StylePropertyId.Unknown;
			foreach (StyleValue styleValue in this.m_Values)
			{
				bool flag = styleValue.id == id;
				if (flag)
				{
					value = styleValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000563E0 File Offset: 0x000545E0
		public void SetStyleValue(StyleValue value)
		{
			for (int i = 0; i < this.m_Values.Count; i++)
			{
				bool flag = this.m_Values[i].id == value.id;
				if (flag)
				{
					bool flag2 = value.keyword == StyleKeyword.Null;
					if (flag2)
					{
						this.m_Values.RemoveAt(i);
					}
					else
					{
						this.m_Values[i] = value;
					}
					return;
				}
			}
			this.m_Values.Add(value);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00056465 File Offset: 0x00054665
		public StyleValueCollection()
		{
		}

		// Token: 0x04000919 RID: 2329
		internal List<StyleValue> m_Values = new List<StyleValue>();
	}
}
