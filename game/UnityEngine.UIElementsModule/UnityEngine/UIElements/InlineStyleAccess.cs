using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x02000279 RID: 633
	internal class InlineStyleAccess : StyleValueCollection, IStyle
	{
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00056479 File Offset: 0x00054679
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00056481 File Offset: 0x00054681
		private VisualElement ve
		{
			[CompilerGenerated]
			get
			{
				return this.<ve>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ve>k__BackingField = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005648A File Offset: 0x0005468A
		public InlineStyleAccess.InlineRule inlineRule
		{
			get
			{
				return this.m_InlineRule;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00056492 File Offset: 0x00054692
		public InlineStyleAccess(VisualElement ve)
		{
			this.ve = ve;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000564A4 File Offset: 0x000546A4
		protected override void Finalize()
		{
			try
			{
				StyleValue styleValue = default(StyleValue);
				bool flag = base.TryGetStyleValue(StylePropertyId.BackgroundImage, ref styleValue);
				if (flag)
				{
					bool isAllocated = styleValue.resource.IsAllocated;
					if (isAllocated)
					{
						styleValue.resource.Free();
					}
				}
				bool flag2 = base.TryGetStyleValue(StylePropertyId.UnityFont, ref styleValue);
				if (flag2)
				{
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00056538 File Offset: 0x00054738
		public void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			this.m_InlineRule.sheet = sheet;
			this.m_InlineRule.rule = rule;
			this.m_InlineRule.propertyIds = StyleSheetCache.GetPropertyIds(rule);
			this.ApplyInlineStyles(this.ve.computedStyle);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00056578 File Offset: 0x00054778
		public bool IsValueSet(StylePropertyId id)
		{
			foreach (StyleValue styleValue in this.m_Values)
			{
				bool flag = styleValue.id == id;
				if (flag)
				{
					return true;
				}
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged styleValueManaged in this.m_ValuesManaged)
				{
					bool flag3 = styleValueManaged.id == id;
					if (flag3)
					{
						return true;
					}
				}
			}
			bool result;
			if (id != StylePropertyId.TextShadow)
			{
				if (id != StylePropertyId.Cursor)
				{
					switch (id)
					{
					case StylePropertyId.Rotate:
						result = this.m_HasInlineRotate;
						break;
					case StylePropertyId.Scale:
						result = this.m_HasInlineScale;
						break;
					case StylePropertyId.TransformOrigin:
						result = this.m_HasInlineTransformOrigin;
						break;
					case StylePropertyId.Translate:
						result = this.m_HasInlineTranslate;
						break;
					default:
						result = false;
						break;
					}
				}
				else
				{
					result = this.m_HasInlineCursor;
				}
			}
			else
			{
				result = this.m_HasInlineTextShadow;
			}
			return result;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000566C0 File Offset: 0x000548C0
		public void ApplyInlineStyles(ref ComputedStyle computedStyle)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			bool flag = this.m_InlineRule.sheet != null;
			if (flag)
			{
				InlineStyleAccess.s_StylePropertyReader.SetInlineContext(this.m_InlineRule.sheet, this.m_InlineRule.rule.properties, this.m_InlineRule.propertyIds, 1f);
				computedStyle.ApplyProperties(InlineStyleAccess.s_StylePropertyReader, ref parentStyle);
			}
			foreach (StyleValue sv in this.m_Values)
			{
				computedStyle.ApplyStyleValue(sv, ref parentStyle);
			}
			bool flag2 = this.m_ValuesManaged != null;
			if (flag2)
			{
				foreach (StyleValueManaged sv2 in this.m_ValuesManaged)
				{
					computedStyle.ApplyStyleValueManaged(sv2, ref parentStyle);
				}
			}
			bool flag3 = this.ve.style.cursor.keyword != StyleKeyword.Null;
			if (flag3)
			{
				computedStyle.ApplyStyleCursor(this.ve.style.cursor.value);
			}
			bool flag4 = this.ve.style.textShadow.keyword != StyleKeyword.Null;
			if (flag4)
			{
				computedStyle.ApplyStyleTextShadow(this.ve.style.textShadow.value);
			}
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			if (hasInlineTransformOrigin)
			{
				computedStyle.ApplyStyleTransformOrigin(this.ve.style.transformOrigin.value);
			}
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			if (hasInlineTranslate)
			{
				computedStyle.ApplyStyleTranslate(this.ve.style.translate.value);
			}
			bool hasInlineScale = this.m_HasInlineScale;
			if (hasInlineScale)
			{
				computedStyle.ApplyStyleScale(this.ve.style.scale.value);
			}
			bool hasInlineRotate = this.m_HasInlineRotate;
			if (hasInlineRotate)
			{
				computedStyle.ApplyStyleRotate(this.ve.style.rotate.value);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00056948 File Offset: 0x00054B48
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00056978 File Offset: 0x00054B78
		StyleCursor IStyle.cursor
		{
			get
			{
				StyleCursor styleCursor = default(StyleCursor);
				bool flag = this.TryGetInlineCursor(ref styleCursor);
				StyleCursor result;
				if (flag)
				{
					result = styleCursor;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineCursor(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000569A4 File Offset: 0x00054BA4
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x000569D4 File Offset: 0x00054BD4
		StyleTextShadow IStyle.textShadow
		{
			get
			{
				StyleTextShadow styleTextShadow = default(StyleTextShadow);
				bool flag = this.TryGetInlineTextShadow(ref styleTextShadow);
				StyleTextShadow result;
				if (flag)
				{
					result = styleTextShadow;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineTextShadow(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00056A00 File Offset: 0x00054C00
		private StyleList<T> GetStyleList<T>(StylePropertyId id)
		{
			StyleValueManaged styleValueManaged = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref styleValueManaged);
			StyleList<T> result;
			if (flag)
			{
				result = new StyleList<T>(styleValueManaged.value as List<T>, styleValueManaged.keyword);
			}
			else
			{
				result = StyleKeyword.Null;
			}
			return result;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00056A48 File Offset: 0x00054C48
		private void SetStyleValueManaged(StyleValueManaged value)
		{
			bool flag = this.m_ValuesManaged == null;
			if (flag)
			{
				this.m_ValuesManaged = new List<StyleValueManaged>();
			}
			for (int i = 0; i < this.m_ValuesManaged.Count; i++)
			{
				bool flag2 = this.m_ValuesManaged[i].id == value.id;
				if (flag2)
				{
					bool flag3 = value.keyword == StyleKeyword.Null;
					if (flag3)
					{
						this.m_ValuesManaged.RemoveAt(i);
					}
					else
					{
						this.m_ValuesManaged[i] = value;
					}
					return;
				}
			}
			this.m_ValuesManaged.Add(value);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00056AE8 File Offset: 0x00054CE8
		private bool TryGetStyleValueManaged(StylePropertyId id, ref StyleValueManaged value)
		{
			value.id = StylePropertyId.Unknown;
			bool flag = this.m_ValuesManaged == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (StyleValueManaged styleValueManaged in this.m_ValuesManaged)
				{
					bool flag2 = styleValueManaged.id == id;
					if (flag2)
					{
						value = styleValueManaged;
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00056B74 File Offset: 0x00054D74
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00056BA4 File Offset: 0x00054DA4
		StyleTransformOrigin IStyle.transformOrigin
		{
			get
			{
				StyleTransformOrigin styleTransformOrigin = default(StyleTransformOrigin);
				bool flag = this.TryGetInlineTransformOrigin(ref styleTransformOrigin);
				StyleTransformOrigin result;
				if (flag)
				{
					result = styleTransformOrigin;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineTransformOrigin(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00056BD0 File Offset: 0x00054DD0
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00056C00 File Offset: 0x00054E00
		StyleTranslate IStyle.translate
		{
			get
			{
				StyleTranslate styleTranslate = default(StyleTranslate);
				bool flag = this.TryGetInlineTranslate(ref styleTranslate);
				StyleTranslate result;
				if (flag)
				{
					result = styleTranslate;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineTranslate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00056C2C File Offset: 0x00054E2C
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00056C5C File Offset: 0x00054E5C
		StyleRotate IStyle.rotate
		{
			get
			{
				StyleRotate styleRotate = default(StyleRotate);
				bool flag = this.TryGetInlineRotate(ref styleRotate);
				StyleRotate result;
				if (flag)
				{
					result = styleRotate;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineRotate(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00056C88 File Offset: 0x00054E88
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00056CB8 File Offset: 0x00054EB8
		StyleScale IStyle.scale
		{
			get
			{
				StyleScale styleScale = default(StyleScale);
				bool flag = this.TryGetInlineScale(ref styleScale);
				StyleScale result;
				if (flag)
				{
					result = styleScale;
				}
				else
				{
					result = StyleKeyword.Null;
				}
				return result;
			}
			set
			{
				bool flag = this.SetInlineScale(value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00056CE4 File Offset: 0x00054EE4
		private bool SetStyleValue(StylePropertyId id, StyleLength inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.length == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.length = inlineValue.ToLength();
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00056DA0 File Offset: 0x00054FA0
		private bool SetStyleValue(StylePropertyId id, StyleFloat inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00056E58 File Offset: 0x00055058
		private bool SetStyleValue(StylePropertyId id, StyleInt inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00056F14 File Offset: 0x00055114
		private bool SetStyleValue(StylePropertyId id, StyleColor inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.color == inlineValue.value && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.color = inlineValue.value;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00056FD4 File Offset: 0x000551D4
		private bool SetStyleValue<T>(StylePropertyId id, StyleEnum<T> inlineValue) where T : struct, IConvertible
		{
			StyleValue styleValue = default(StyleValue);
			int num = UnsafeUtility.EnumToInt<T>(inlineValue.value);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool flag2 = styleValue.number == (float)num && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.number = (float)num;
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00057094 File Offset: 0x00055294
		private bool SetStyleValue(StylePropertyId id, StyleBackground inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				VectorImage x = styleValue.resource.IsAllocated ? (styleValue.resource.Target as VectorImage) : null;
				Sprite x2 = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Sprite) : null;
				Texture2D x3 = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Texture2D) : null;
				RenderTexture x4 = styleValue.resource.IsAllocated ? (styleValue.resource.Target as RenderTexture) : null;
				bool flag2 = x == inlineValue.value.vectorImage && x3 == inlineValue.value.texture && x2 == inlineValue.value.sprite && x4 == inlineValue.value.renderTexture && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.vectorImage != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.vectorImage);
			}
			else
			{
				bool flag5 = inlineValue.value.sprite != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.sprite);
				}
				else
				{
					bool flag6 = inlineValue.value.texture != null;
					if (flag6)
					{
						styleValue.resource = GCHandle.Alloc(inlineValue.value.texture);
					}
					else
					{
						bool flag7 = inlineValue.value.renderTexture != null;
						if (flag7)
						{
							styleValue.resource = GCHandle.Alloc(inlineValue.value.renderTexture);
						}
						else
						{
							styleValue.resource = default(GCHandle);
						}
					}
				}
			}
			base.SetStyleValue(styleValue);
			bool flag8 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag8)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0005734C File Offset: 0x0005554C
		private bool SetStyleValue(StylePropertyId id, StyleFontDefinition inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				Font x = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null;
				FontAsset x2 = styleValue.resource.IsAllocated ? (styleValue.resource.Target as FontAsset) : null;
				bool flag2 = x == inlineValue.value.font && x2 == inlineValue.value.fontAsset && styleValue.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					styleValue.resource.Free();
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value.font != null;
			if (flag4)
			{
				styleValue.resource = GCHandle.Alloc(inlineValue.value.font);
			}
			else
			{
				bool flag5 = inlineValue.value.fontAsset != null;
				if (flag5)
				{
					styleValue.resource = GCHandle.Alloc(inlineValue.value.fontAsset);
				}
				else
				{
					styleValue.resource = default(GCHandle);
				}
			}
			base.SetStyleValue(styleValue);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag6)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00057510 File Offset: 0x00055710
		private bool SetStyleValue(StylePropertyId id, StyleFont inlineValue)
		{
			StyleValue styleValue = default(StyleValue);
			bool flag = base.TryGetStyleValue(id, ref styleValue);
			if (flag)
			{
				bool isAllocated = styleValue.resource.IsAllocated;
				if (isAllocated)
				{
					Font x = styleValue.resource.IsAllocated ? (styleValue.resource.Target as Font) : null;
					bool flag2 = x == inlineValue.value && styleValue.keyword == inlineValue.keyword;
					if (flag2)
					{
						return false;
					}
					bool isAllocated2 = styleValue.resource.IsAllocated;
					if (isAllocated2)
					{
						styleValue.resource.Free();
					}
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValue.id = id;
			styleValue.keyword = inlineValue.keyword;
			styleValue.resource = ((inlineValue.value != null) ? GCHandle.Alloc(inlineValue.value) : default(GCHandle));
			base.SetStyleValue(styleValue);
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005764C File Offset: 0x0005584C
		private bool SetStyleValue<T>(StylePropertyId id, StyleList<T> inlineValue)
		{
			StyleValueManaged styleValueManaged = default(StyleValueManaged);
			bool flag = this.TryGetStyleValueManaged(id, ref styleValueManaged);
			if (flag)
			{
				List<T> list = styleValueManaged.value as List<T>;
				bool flag2 = list != null && inlineValue.value != null && list.SequenceEqual(inlineValue.value) && styleValueManaged.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleValueManaged.id = id;
			styleValueManaged.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.value != null;
			if (flag4)
			{
				bool flag5 = styleValueManaged.value == null;
				if (flag5)
				{
					styleValueManaged.value = new List<T>(inlineValue.value);
				}
				else
				{
					List<T> list2 = (List<T>)styleValueManaged.value;
					list2.Clear();
					list2.AddRange(inlineValue.value);
				}
			}
			else
			{
				styleValueManaged.value = null;
			}
			this.SetStyleValueManaged(styleValueManaged);
			bool flag6 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag6)
			{
				result = this.RemoveInlineStyle(id);
			}
			else
			{
				this.ApplyStyleValue(styleValueManaged);
				result = true;
			}
			return result;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00057784 File Offset: 0x00055984
		private bool SetInlineCursor(StyleCursor inlineValue)
		{
			StyleCursor styleCursor = default(StyleCursor);
			bool flag = this.TryGetInlineCursor(ref styleCursor);
			if (flag)
			{
				bool flag2 = styleCursor.value == inlineValue.value && styleCursor.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleCursor.value = inlineValue.value;
			styleCursor.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineCursor = false;
				result = this.RemoveInlineStyle(StylePropertyId.Cursor);
			}
			else
			{
				this.m_InlineCursor = styleCursor;
				this.m_HasInlineCursor = true;
				this.ApplyStyleCursor(styleCursor);
				result = true;
			}
			return result;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00057850 File Offset: 0x00055A50
		private void ApplyStyleCursor(StyleCursor cursor)
		{
			this.ve.computedStyle.ApplyStyleCursor(cursor.value);
			BaseVisualElementPanel elementPanel = this.ve.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this.ve;
			if (flag)
			{
				this.ve.elementPanel.cursorManager.SetCursor(cursor.value);
			}
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000578BC File Offset: 0x00055ABC
		private bool SetInlineTextShadow(StyleTextShadow inlineValue)
		{
			StyleTextShadow styleTextShadow = default(StyleTextShadow);
			bool flag = this.TryGetInlineTextShadow(ref styleTextShadow);
			if (flag)
			{
				bool flag2 = styleTextShadow.value == inlineValue.value && styleTextShadow.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			styleTextShadow.value = inlineValue.value;
			styleTextShadow.keyword = inlineValue.keyword;
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineTextShadow = false;
				result = this.RemoveInlineStyle(StylePropertyId.TextShadow);
			}
			else
			{
				this.m_InlineTextShadow = styleTextShadow;
				this.m_HasInlineTextShadow = true;
				this.ApplyStyleTextShadow(styleTextShadow);
				result = true;
			}
			return result;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00057988 File Offset: 0x00055B88
		private void ApplyStyleTextShadow(StyleTextShadow textShadow)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TextShadow, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTextShadow(this.ve, this.ve.computedStyle, textShadow, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TextShadow);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTextShadow(textShadow.value);
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00057A50 File Offset: 0x00055C50
		private bool SetInlineTransformOrigin(StyleTransformOrigin inlineValue)
		{
			StyleTransformOrigin styleTransformOrigin = default(StyleTransformOrigin);
			bool flag = this.TryGetInlineTransformOrigin(ref styleTransformOrigin);
			if (flag)
			{
				bool flag2 = styleTransformOrigin.value == inlineValue.value && styleTransformOrigin.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineTransformOrigin = false;
				result = this.RemoveInlineStyle(StylePropertyId.TransformOrigin);
			}
			else
			{
				this.m_InlineTransformOrigin = inlineValue;
				this.m_HasInlineTransformOrigin = true;
				this.ApplyStyleTransformOrigin(inlineValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00057AFC File Offset: 0x00055CFC
		private void ApplyStyleTransformOrigin(StyleTransformOrigin transformOrigin)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.TransformOrigin, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTransformOrigin(this.ve, this.ve.computedStyle, transformOrigin, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.TransformOrigin);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTransformOrigin(transformOrigin.value);
			}
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00057BC4 File Offset: 0x00055DC4
		private bool SetInlineTranslate(StyleTranslate inlineValue)
		{
			StyleTranslate styleTranslate = default(StyleTranslate);
			bool flag = this.TryGetInlineTranslate(ref styleTranslate);
			if (flag)
			{
				bool flag2 = styleTranslate.value == inlineValue.value && styleTranslate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineTranslate = false;
				result = this.RemoveInlineStyle(StylePropertyId.Translate);
			}
			else
			{
				this.m_InlineTranslateOperation = inlineValue;
				this.m_HasInlineTranslate = true;
				this.ApplyStyleTranslate(inlineValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00057C70 File Offset: 0x00055E70
		private void ApplyStyleTranslate(StyleTranslate translate)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Translate, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineTranslate(this.ve, this.ve.computedStyle, translate, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Translate);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleTranslate(translate.value);
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00057D38 File Offset: 0x00055F38
		private bool SetInlineScale(StyleScale inlineValue)
		{
			StyleScale styleScale = default(StyleScale);
			bool flag = this.TryGetInlineScale(ref styleScale);
			if (flag)
			{
				bool flag2 = styleScale.value == inlineValue.value && styleScale.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineScale = false;
				result = this.RemoveInlineStyle(StylePropertyId.Scale);
			}
			else
			{
				this.m_InlineScale = inlineValue;
				this.m_HasInlineScale = true;
				this.ApplyStyleScale(inlineValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00057DE4 File Offset: 0x00055FE4
		private void ApplyStyleScale(StyleScale scale)
		{
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Scale, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineScale(this.ve, this.ve.computedStyle, scale, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Scale);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleScale(scale.value);
			}
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x00057EAC File Offset: 0x000560AC
		private bool SetInlineRotate(StyleRotate inlineValue)
		{
			StyleRotate styleRotate = default(StyleRotate);
			bool flag = this.TryGetInlineRotate(ref styleRotate);
			if (flag)
			{
				bool flag2 = styleRotate.value == inlineValue.value && styleRotate.keyword == inlineValue.keyword;
				if (flag2)
				{
					return false;
				}
			}
			else
			{
				bool flag3 = inlineValue.keyword == StyleKeyword.Null;
				if (flag3)
				{
					return false;
				}
			}
			bool flag4 = inlineValue.keyword == StyleKeyword.Null;
			bool result;
			if (flag4)
			{
				this.m_HasInlineRotate = false;
				result = this.RemoveInlineStyle(StylePropertyId.Rotate);
			}
			else
			{
				this.m_InlineRotateOperation = inlineValue;
				this.m_HasInlineRotate = true;
				this.ApplyStyleRotate(inlineValue);
				result = true;
			}
			return result;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00057F58 File Offset: 0x00056158
		private void ApplyStyleRotate(StyleRotate rotate)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ref ComputedStyle computedStyle2 = ref parent.computedStyle;
			}
			else
			{
				InitialStyle.Get();
			}
			ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
			bool flag = false;
			ComputedTransitionProperty computedTransitionProperty;
			bool flag2 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(StylePropertyId.Rotate, out computedTransitionProperty);
			if (flag2)
			{
				flag = ComputedStyle.StartAnimationInlineRotate(this.ve, this.ve.computedStyle, rotate, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
			}
			else
			{
				this.ve.styleAnimation.CancelAnimation(StylePropertyId.Rotate);
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this.ve.computedStyle.ApplyStyleRotate(rotate.value);
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00058054 File Offset: 0x00056254
		private void ApplyStyleValue(StyleValue value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			bool flag = false;
			bool flag2 = StylePropertyUtil.IsAnimatable(value.id);
			if (flag2)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty computedTransitionProperty;
				bool flag3 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(value.id, out computedTransitionProperty);
				if (flag3)
				{
					flag = ComputedStyle.StartAnimationInline(this.ve, value.id, this.ve.computedStyle, value, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(value.id);
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				this.ve.computedStyle.ApplyStyleValue(value, ref parentStyle);
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00058168 File Offset: 0x00056368
		private void ApplyStyleValue(StyleValueManaged value)
		{
			VisualElement parent = this.ve.hierarchy.parent;
			ComputedStyle ptr;
			if (parent != null)
			{
				ref ComputedStyle computedStyle = ref parent.computedStyle;
				ptr = parent.computedStyle;
			}
			else
			{
				ptr = InitialStyle.Get();
			}
			ref ComputedStyle parentStyle = ref ptr;
			this.ve.computedStyle.ApplyStyleValueManaged(value, ref parentStyle);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000581B8 File Offset: 0x000563B8
		private bool RemoveInlineStyle(StylePropertyId id)
		{
			long matchingRulesHash = this.ve.computedStyle.matchingRulesHash;
			bool flag = matchingRulesHash == 0L;
			bool result;
			if (flag)
			{
				this.ApplyFromComputedStyle(id, InitialStyle.Get());
				result = true;
			}
			else
			{
				ComputedStyle computedStyle;
				bool flag2 = StyleCache.TryGetValue(matchingRulesHash, out computedStyle);
				if (flag2)
				{
					this.ApplyFromComputedStyle(id, ref computedStyle);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00058214 File Offset: 0x00056414
		private void ApplyFromComputedStyle(StylePropertyId id, ref ComputedStyle newStyle)
		{
			bool flag = false;
			bool flag2 = StylePropertyUtil.IsAnimatable(id);
			if (flag2)
			{
				ComputedTransitionUtils.UpdateComputedTransitions(this.ve.computedStyle);
				ComputedTransitionProperty computedTransitionProperty;
				bool flag3 = this.ve.computedStyle.hasTransition && this.ve.styleInitialized && this.ve.computedStyle.GetTransitionProperty(id, out computedTransitionProperty);
				if (flag3)
				{
					flag = ComputedStyle.StartAnimation(this.ve, id, this.ve.computedStyle, ref newStyle, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
				else
				{
					this.ve.styleAnimation.CancelAnimation(id);
				}
			}
			bool flag4 = !flag;
			if (flag4)
			{
				this.ve.computedStyle.ApplyFromComputedStyle(id, ref newStyle);
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000582E0 File Offset: 0x000564E0
		public bool TryGetInlineCursor(ref StyleCursor value)
		{
			bool hasInlineCursor = this.m_HasInlineCursor;
			bool result;
			if (hasInlineCursor)
			{
				value = this.m_InlineCursor;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00058310 File Offset: 0x00056510
		public bool TryGetInlineTextShadow(ref StyleTextShadow value)
		{
			bool hasInlineTextShadow = this.m_HasInlineTextShadow;
			bool result;
			if (hasInlineTextShadow)
			{
				value = this.m_InlineTextShadow;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00058340 File Offset: 0x00056540
		public bool TryGetInlineTransformOrigin(ref StyleTransformOrigin value)
		{
			bool hasInlineTransformOrigin = this.m_HasInlineTransformOrigin;
			bool result;
			if (hasInlineTransformOrigin)
			{
				value = this.m_InlineTransformOrigin;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00058370 File Offset: 0x00056570
		public bool TryGetInlineTranslate(ref StyleTranslate value)
		{
			bool hasInlineTranslate = this.m_HasInlineTranslate;
			bool result;
			if (hasInlineTranslate)
			{
				value = this.m_InlineTranslateOperation;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000583A0 File Offset: 0x000565A0
		public bool TryGetInlineRotate(ref StyleRotate value)
		{
			bool hasInlineRotate = this.m_HasInlineRotate;
			bool result;
			if (hasInlineRotate)
			{
				value = this.m_InlineRotateOperation;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000583D0 File Offset: 0x000565D0
		public bool TryGetInlineScale(ref StyleScale value)
		{
			bool hasInlineScale = this.m_HasInlineScale;
			bool result;
			if (hasInlineScale)
			{
				value = this.m_InlineScale;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00058400 File Offset: 0x00056600
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00058434 File Offset: 0x00056634
		StyleEnum<Align> IStyle.alignContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignContent);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignContent = (YogaAlign)this.ve.computedStyle.alignContent;
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00058484 File Offset: 0x00056684
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000584B8 File Offset: 0x000566B8
		StyleEnum<Align> IStyle.alignItems
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignItems);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignItems, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignItems = (YogaAlign)this.ve.computedStyle.alignItems;
				}
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x00058508 File Offset: 0x00056708
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x0005853C File Offset: 0x0005673C
		StyleEnum<Align> IStyle.alignSelf
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.AlignSelf);
				return new StyleEnum<Align>((Align)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Align>(StylePropertyId.AlignSelf, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.AlignSelf = (YogaAlign)this.ve.computedStyle.alignSelf;
				}
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0005858C File Offset: 0x0005678C
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000585AC File Offset: 0x000567AC
		StyleColor IStyle.backgroundColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BackgroundColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000585E0 File Offset: 0x000567E0
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x00058600 File Offset: 0x00056800
		StyleBackground IStyle.backgroundImage
		{
			get
			{
				return base.GetStyleBackground(StylePropertyId.BackgroundImage);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BackgroundImage, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00058634 File Offset: 0x00056834
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00058654 File Offset: 0x00056854
		StyleColor IStyle.borderBottomColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderBottomColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00058688 File Offset: 0x00056888
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x000586A8 File Offset: 0x000568A8
		StyleLength IStyle.borderBottomLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000586DC File Offset: 0x000568DC
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x000586FC File Offset: 0x000568FC
		StyleLength IStyle.borderBottomRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderBottomRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00058730 File Offset: 0x00056930
		// (set) Token: 0x06001413 RID: 5139 RVA: 0x00058750 File Offset: 0x00056950
		StyleFloat IStyle.borderBottomWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderBottomWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderBottomWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderBottomWidth = this.ve.computedStyle.borderBottomWidth;
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000587A4 File Offset: 0x000569A4
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x000587C4 File Offset: 0x000569C4
		StyleColor IStyle.borderLeftColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderLeftColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000587F8 File Offset: 0x000569F8
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x00058818 File Offset: 0x00056A18
		StyleFloat IStyle.borderLeftWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderLeftWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderLeftWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderLeftWidth = this.ve.computedStyle.borderLeftWidth;
				}
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0005886C File Offset: 0x00056A6C
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x0005888C File Offset: 0x00056A8C
		StyleColor IStyle.borderRightColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderRightColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x000588C0 File Offset: 0x00056AC0
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x000588E0 File Offset: 0x00056AE0
		StyleFloat IStyle.borderRightWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderRightWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderRightWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderRightWidth = this.ve.computedStyle.borderRightWidth;
				}
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00058934 File Offset: 0x00056B34
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x00058954 File Offset: 0x00056B54
		StyleColor IStyle.borderTopColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.BorderTopColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00058988 File Offset: 0x00056B88
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x000589A8 File Offset: 0x00056BA8
		StyleLength IStyle.borderTopLeftRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopLeftRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopLeftRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000589DC File Offset: 0x00056BDC
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x000589FC File Offset: 0x00056BFC
		StyleLength IStyle.borderTopRightRadius
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.BorderTopRightRadius);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopRightRadius, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.BorderRadius | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00058A30 File Offset: 0x00056C30
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x00058A50 File Offset: 0x00056C50
		StyleFloat IStyle.borderTopWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.BorderTopWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.BorderTopWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.BorderWidth | VersionChangeType.Repaint);
					this.ve.yogaNode.BorderTopWidth = this.ve.computedStyle.borderTopWidth;
				}
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00058AA4 File Offset: 0x00056CA4
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x00058AC4 File Offset: 0x00056CC4
		StyleLength IStyle.bottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Bottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Bottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Bottom = this.ve.computedStyle.bottom.ToYogaValue();
				}
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00058B18 File Offset: 0x00056D18
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x00058B38 File Offset: 0x00056D38
		StyleColor IStyle.color
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.Color);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Color, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00058B6C File Offset: 0x00056D6C
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x00058BA0 File Offset: 0x00056DA0
		StyleEnum<DisplayStyle> IStyle.display
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Display);
				return new StyleEnum<DisplayStyle>((DisplayStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<DisplayStyle>(StylePropertyId.Display, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
					this.ve.yogaNode.Display = (YogaDisplay)this.ve.computedStyle.display;
				}
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00058BF4 File Offset: 0x00056DF4
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x00058C14 File Offset: 0x00056E14
		StyleLength IStyle.flexBasis
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FlexBasis);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexBasis, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexBasis = this.ve.computedStyle.flexBasis.ToYogaValue();
				}
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00058C68 File Offset: 0x00056E68
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00058C9C File Offset: 0x00056E9C
		StyleEnum<FlexDirection> IStyle.flexDirection
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexDirection);
				return new StyleEnum<FlexDirection>((FlexDirection)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FlexDirection>(StylePropertyId.FlexDirection, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexDirection = (YogaFlexDirection)this.ve.computedStyle.flexDirection;
				}
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00058CEC File Offset: 0x00056EEC
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00058D0C File Offset: 0x00056F0C
		StyleFloat IStyle.flexGrow
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexGrow);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexGrow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexGrow = this.ve.computedStyle.flexGrow;
				}
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00058D5C File Offset: 0x00056F5C
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x00058D7C File Offset: 0x00056F7C
		StyleFloat IStyle.flexShrink
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.FlexShrink);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FlexShrink, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.FlexShrink = this.ve.computedStyle.flexShrink;
				}
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00058DCC File Offset: 0x00056FCC
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x00058E00 File Offset: 0x00057000
		StyleEnum<Wrap> IStyle.flexWrap
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.FlexWrap);
				return new StyleEnum<Wrap>((Wrap)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Wrap>(StylePropertyId.FlexWrap, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Wrap = (YogaWrap)this.ve.computedStyle.flexWrap;
				}
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00058E50 File Offset: 0x00057050
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x00058E70 File Offset: 0x00057070
		StyleLength IStyle.fontSize
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.FontSize);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.FontSize, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00058EA0 File Offset: 0x000570A0
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x00058EC0 File Offset: 0x000570C0
		StyleLength IStyle.height
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Height);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Height, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Height = this.ve.computedStyle.height.ToYogaValue();
				}
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00058F14 File Offset: 0x00057114
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x00058F48 File Offset: 0x00057148
		StyleEnum<Justify> IStyle.justifyContent
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.JustifyContent);
				return new StyleEnum<Justify>((Justify)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Justify>(StylePropertyId.JustifyContent, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.JustifyContent = (YogaJustify)this.ve.computedStyle.justifyContent;
				}
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00058F98 File Offset: 0x00057198
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x00058FB8 File Offset: 0x000571B8
		StyleLength IStyle.left
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Left);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Left, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Left = this.ve.computedStyle.left.ToYogaValue();
				}
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0005900C File Offset: 0x0005720C
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0005902C File Offset: 0x0005722C
		StyleLength IStyle.letterSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.LetterSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.LetterSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00059060 File Offset: 0x00057260
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00059080 File Offset: 0x00057280
		StyleLength IStyle.marginBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginBottom = this.ve.computedStyle.marginBottom.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000590D4 File Offset: 0x000572D4
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x000590F4 File Offset: 0x000572F4
		StyleLength IStyle.marginLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginLeft = this.ve.computedStyle.marginLeft.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00059148 File Offset: 0x00057348
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00059168 File Offset: 0x00057368
		StyleLength IStyle.marginRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginRight = this.ve.computedStyle.marginRight.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000591BC File Offset: 0x000573BC
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x000591DC File Offset: 0x000573DC
		StyleLength IStyle.marginTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MarginTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MarginTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MarginTop = this.ve.computedStyle.marginTop.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00059230 File Offset: 0x00057430
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00059250 File Offset: 0x00057450
		StyleLength IStyle.maxHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxHeight = this.ve.computedStyle.maxHeight.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000592A4 File Offset: 0x000574A4
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x000592C4 File Offset: 0x000574C4
		StyleLength IStyle.maxWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MaxWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MaxWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MaxWidth = this.ve.computedStyle.maxWidth.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00059318 File Offset: 0x00057518
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x00059338 File Offset: 0x00057538
		StyleLength IStyle.minHeight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinHeight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinHeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinHeight = this.ve.computedStyle.minHeight.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0005938C File Offset: 0x0005758C
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x000593AC File Offset: 0x000575AC
		StyleLength IStyle.minWidth
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.MinWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.MinWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.MinWidth = this.ve.computedStyle.minWidth.ToYogaValue();
				}
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00059400 File Offset: 0x00057600
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00059420 File Offset: 0x00057620
		StyleFloat IStyle.opacity
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.Opacity);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Opacity, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Opacity);
				}
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00059454 File Offset: 0x00057654
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00059488 File Offset: 0x00057688
		StyleEnum<Overflow> IStyle.overflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Overflow);
				return new StyleEnum<Overflow>((Overflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				StyleEnum<OverflowInternal> inlineValue = new StyleEnum<OverflowInternal>((OverflowInternal)value.value, value.keyword);
				bool flag = this.SetStyleValue<OverflowInternal>(StylePropertyId.Overflow, inlineValue);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Overflow);
					this.ve.yogaNode.Overflow = (YogaOverflow)this.ve.computedStyle.overflow;
				}
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000594EC File Offset: 0x000576EC
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0005950C File Offset: 0x0005770C
		StyleLength IStyle.paddingBottom
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingBottom = this.ve.computedStyle.paddingBottom.ToYogaValue();
				}
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00059560 File Offset: 0x00057760
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00059580 File Offset: 0x00057780
		StyleLength IStyle.paddingLeft
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingLeft = this.ve.computedStyle.paddingLeft.ToYogaValue();
				}
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x000595D4 File Offset: 0x000577D4
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x000595F4 File Offset: 0x000577F4
		StyleLength IStyle.paddingRight
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingRight = this.ve.computedStyle.paddingRight.ToYogaValue();
				}
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00059648 File Offset: 0x00057848
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00059668 File Offset: 0x00057868
		StyleLength IStyle.paddingTop
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.PaddingTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.PaddingTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PaddingTop = this.ve.computedStyle.paddingTop.ToYogaValue();
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x000596BC File Offset: 0x000578BC
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x000596F0 File Offset: 0x000578F0
		StyleEnum<Position> IStyle.position
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Position);
				return new StyleEnum<Position>((Position)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Position>(StylePropertyId.Position, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.PositionType = (YogaPositionType)this.ve.computedStyle.position;
				}
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00059740 File Offset: 0x00057940
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00059760 File Offset: 0x00057960
		StyleLength IStyle.right
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Right);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Right, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Right = this.ve.computedStyle.right.ToYogaValue();
				}
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x000597B4 File Offset: 0x000579B4
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x000597E8 File Offset: 0x000579E8
		StyleEnum<TextOverflow> IStyle.textOverflow
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.TextOverflow);
				return new StyleEnum<TextOverflow>((TextOverflow)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflow>(StylePropertyId.TextOverflow, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0005981C File Offset: 0x00057A1C
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0005983C File Offset: 0x00057A3C
		StyleLength IStyle.top
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Top);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Top, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Top = this.ve.computedStyle.top.ToYogaValue();
				}
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00059890 File Offset: 0x00057A90
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x000598B0 File Offset: 0x00057AB0
		StyleList<TimeValue> IStyle.transitionDelay
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDelay);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDelay, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000598E4 File Offset: 0x00057AE4
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00059904 File Offset: 0x00057B04
		StyleList<TimeValue> IStyle.transitionDuration
		{
			get
			{
				return this.GetStyleList<TimeValue>(StylePropertyId.TransitionDuration);
			}
			set
			{
				bool flag = this.SetStyleValue<TimeValue>(StylePropertyId.TransitionDuration, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00059938 File Offset: 0x00057B38
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x00059958 File Offset: 0x00057B58
		StyleList<StylePropertyName> IStyle.transitionProperty
		{
			get
			{
				return this.GetStyleList<StylePropertyName>(StylePropertyId.TransitionProperty);
			}
			set
			{
				bool flag = this.SetStyleValue<StylePropertyName>(StylePropertyId.TransitionProperty, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.TransitionProperty);
				}
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0005998C File Offset: 0x00057B8C
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x000599AC File Offset: 0x00057BAC
		StyleList<EasingFunction> IStyle.transitionTimingFunction
		{
			get
			{
				return this.GetStyleList<EasingFunction>(StylePropertyId.TransitionTimingFunction);
			}
			set
			{
				bool flag = this.SetStyleValue<EasingFunction>(StylePropertyId.TransitionTimingFunction, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x000599DC File Offset: 0x00057BDC
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x000599FC File Offset: 0x00057BFC
		StyleColor IStyle.unityBackgroundImageTintColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityBackgroundImageTintColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityBackgroundImageTintColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Color);
				}
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00059A30 File Offset: 0x00057C30
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00059A64 File Offset: 0x00057C64
		StyleEnum<ScaleMode> IStyle.unityBackgroundScaleMode
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityBackgroundScaleMode);
				return new StyleEnum<ScaleMode>((ScaleMode)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<ScaleMode>(StylePropertyId.UnityBackgroundScaleMode, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00059A98 File Offset: 0x00057C98
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00059AB8 File Offset: 0x00057CB8
		StyleFont IStyle.unityFont
		{
			get
			{
				return base.GetStyleFont(StylePropertyId.UnityFont);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFont, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00059AEC File Offset: 0x00057CEC
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00059B0C File Offset: 0x00057D0C
		StyleFontDefinition IStyle.unityFontDefinition
		{
			get
			{
				return base.GetStyleFontDefinition(StylePropertyId.UnityFontDefinition);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityFontDefinition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00059B40 File Offset: 0x00057D40
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00059B74 File Offset: 0x00057D74
		StyleEnum<FontStyle> IStyle.unityFontStyleAndWeight
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityFontStyleAndWeight);
				return new StyleEnum<FontStyle>((FontStyle)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<FontStyle>(StylePropertyId.UnityFontStyleAndWeight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00059BA8 File Offset: 0x00057DA8
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00059BDC File Offset: 0x00057DDC
		StyleEnum<OverflowClipBox> IStyle.unityOverflowClipBox
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityOverflowClipBox);
				return new StyleEnum<OverflowClipBox>((OverflowClipBox)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<OverflowClipBox>(StylePropertyId.UnityOverflowClipBox, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00059C10 File Offset: 0x00057E10
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00059C30 File Offset: 0x00057E30
		StyleLength IStyle.unityParagraphSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.UnityParagraphSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityParagraphSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00059C64 File Offset: 0x00057E64
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00059C84 File Offset: 0x00057E84
		StyleInt IStyle.unitySliceBottom
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceBottom);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceBottom, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00059CB8 File Offset: 0x00057EB8
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x00059CD8 File Offset: 0x00057ED8
		StyleInt IStyle.unitySliceLeft
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceLeft);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceLeft, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00059D0C File Offset: 0x00057F0C
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x00059D2C File Offset: 0x00057F2C
		StyleInt IStyle.unitySliceRight
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceRight);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceRight, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00059D60 File Offset: 0x00057F60
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00059D80 File Offset: 0x00057F80
		StyleInt IStyle.unitySliceTop
		{
			get
			{
				return base.GetStyleInt(StylePropertyId.UnitySliceTop);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnitySliceTop, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00059DB4 File Offset: 0x00057FB4
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x00059DE8 File Offset: 0x00057FE8
		StyleEnum<TextAnchor> IStyle.unityTextAlign
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextAlign);
				return new StyleEnum<TextAnchor>((TextAnchor)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextAnchor>(StylePropertyId.UnityTextAlign, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00059E1C File Offset: 0x0005801C
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x00059E3C File Offset: 0x0005803C
		StyleColor IStyle.unityTextOutlineColor
		{
			get
			{
				return base.GetStyleColor(StylePropertyId.UnityTextOutlineColor);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineColor, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00059E70 File Offset: 0x00058070
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x00059E90 File Offset: 0x00058090
		StyleFloat IStyle.unityTextOutlineWidth
		{
			get
			{
				return base.GetStyleFloat(StylePropertyId.UnityTextOutlineWidth);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.UnityTextOutlineWidth, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00059EC4 File Offset: 0x000580C4
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00059EF8 File Offset: 0x000580F8
		StyleEnum<TextOverflowPosition> IStyle.unityTextOverflowPosition
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.UnityTextOverflowPosition);
				return new StyleEnum<TextOverflowPosition>((TextOverflowPosition)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<TextOverflowPosition>(StylePropertyId.UnityTextOverflowPosition, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00059F2C File Offset: 0x0005812C
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x00059F60 File Offset: 0x00058160
		StyleEnum<Visibility> IStyle.visibility
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.Visibility);
				return new StyleEnum<Visibility>((Visibility)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<Visibility>(StylePropertyId.Visibility, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint | VersionChangeType.Picking);
				}
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00059F94 File Offset: 0x00058194
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00059FC8 File Offset: 0x000581C8
		StyleEnum<WhiteSpace> IStyle.whiteSpace
		{
			get
			{
				StyleInt styleInt = base.GetStyleInt(StylePropertyId.WhiteSpace);
				return new StyleEnum<WhiteSpace>((WhiteSpace)styleInt.value, styleInt.keyword);
			}
			set
			{
				bool flag = this.SetStyleValue<WhiteSpace>(StylePropertyId.WhiteSpace, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles);
				}
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00059FF8 File Offset: 0x000581F8
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0005A018 File Offset: 0x00058218
		StyleLength IStyle.width
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.Width);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.Width, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Styles);
					this.ve.yogaNode.Width = this.ve.computedStyle.width.ToYogaValue();
				}
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0005A06C File Offset: 0x0005826C
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x0005A08C File Offset: 0x0005828C
		StyleLength IStyle.wordSpacing
		{
			get
			{
				return base.GetStyleLength(StylePropertyId.WordSpacing);
			}
			set
			{
				bool flag = this.SetStyleValue(StylePropertyId.WordSpacing, value);
				if (flag)
				{
					this.ve.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005A0BD File Offset: 0x000582BD
		// Note: this type is marked as 'beforefieldinit'.
		static InlineStyleAccess()
		{
		}

		// Token: 0x0400091A RID: 2330
		private static StylePropertyReader s_StylePropertyReader = new StylePropertyReader();

		// Token: 0x0400091B RID: 2331
		private List<StyleValueManaged> m_ValuesManaged;

		// Token: 0x0400091C RID: 2332
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private VisualElement <ve>k__BackingField;

		// Token: 0x0400091D RID: 2333
		private bool m_HasInlineCursor;

		// Token: 0x0400091E RID: 2334
		private StyleCursor m_InlineCursor;

		// Token: 0x0400091F RID: 2335
		private bool m_HasInlineTextShadow;

		// Token: 0x04000920 RID: 2336
		private StyleTextShadow m_InlineTextShadow;

		// Token: 0x04000921 RID: 2337
		private bool m_HasInlineTransformOrigin;

		// Token: 0x04000922 RID: 2338
		private StyleTransformOrigin m_InlineTransformOrigin;

		// Token: 0x04000923 RID: 2339
		private bool m_HasInlineTranslate;

		// Token: 0x04000924 RID: 2340
		private StyleTranslate m_InlineTranslateOperation;

		// Token: 0x04000925 RID: 2341
		private bool m_HasInlineRotate;

		// Token: 0x04000926 RID: 2342
		private StyleRotate m_InlineRotateOperation;

		// Token: 0x04000927 RID: 2343
		private bool m_HasInlineScale;

		// Token: 0x04000928 RID: 2344
		private StyleScale m_InlineScale;

		// Token: 0x04000929 RID: 2345
		private InlineStyleAccess.InlineRule m_InlineRule;

		// Token: 0x0200027A RID: 634
		internal struct InlineRule
		{
			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x06001491 RID: 5265 RVA: 0x0005A0C9 File Offset: 0x000582C9
			public StyleProperty[] properties
			{
				get
				{
					return this.rule.properties;
				}
			}

			// Token: 0x0400092A RID: 2346
			public StyleSheet sheet;

			// Token: 0x0400092B RID: 2347
			public StyleRule rule;

			// Token: 0x0400092C RID: 2348
			public StylePropertyId[] propertyIds;
		}
	}
}
