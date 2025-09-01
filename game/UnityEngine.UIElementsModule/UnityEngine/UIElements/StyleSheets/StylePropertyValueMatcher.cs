using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000377 RID: 887
	internal class StylePropertyValueMatcher : BaseStyleMatcher
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0008757C File Offset: 0x0008577C
		private StylePropertyValue current
		{
			get
			{
				return base.hasCurrent ? this.m_Values[base.currentIndex] : default(StylePropertyValue);
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x000875AD File Offset: 0x000857AD
		public override int valueCount
		{
			get
			{
				return this.m_Values.Count;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00004E8A File Offset: 0x0000308A
		public override bool isCurrentVariable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x000875BC File Offset: 0x000857BC
		public override bool isCurrentComma
		{
			get
			{
				return base.hasCurrent && this.m_Values[base.currentIndex].handle.valueType == StyleValueType.CommaSeparator;
			}
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000875F8 File Offset: 0x000857F8
		public MatchResult Match(Expression exp, List<StylePropertyValue> values)
		{
			MatchResult matchResult = new MatchResult
			{
				errorCode = MatchResultErrorCode.None
			};
			bool flag = values == null || values.Count == 0;
			MatchResult result;
			if (flag)
			{
				matchResult.errorCode = MatchResultErrorCode.EmptyValue;
				result = matchResult;
			}
			else
			{
				base.Initialize();
				this.m_Values = values;
				StyleValueHandle handle = this.m_Values[0].handle;
				bool flag2 = handle.valueType == StyleValueType.Keyword && handle.valueIndex == 1;
				bool flag3;
				if (flag2)
				{
					base.MoveNext();
					flag3 = true;
				}
				else
				{
					flag3 = base.Match(exp);
				}
				bool flag4 = !flag3;
				if (flag4)
				{
					StyleSheet sheet = this.current.sheet;
					matchResult.errorCode = MatchResultErrorCode.Syntax;
					matchResult.errorValue = sheet.ReadAsString(this.current.handle);
				}
				else
				{
					bool hasCurrent = base.hasCurrent;
					if (hasCurrent)
					{
						StyleSheet sheet2 = this.current.sheet;
						matchResult.errorCode = MatchResultErrorCode.ExpectedEndOfValue;
						matchResult.errorValue = sheet2.ReadAsString(this.current.handle);
					}
				}
				result = matchResult;
			}
			return result;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00087714 File Offset: 0x00085914
		protected override bool MatchKeyword(string keyword)
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Keyword;
			bool result;
			if (flag)
			{
				StyleValueKeyword valueIndex = (StyleValueKeyword)current.handle.valueIndex;
				result = (valueIndex.ToUssString() == keyword.ToLower());
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					string a = current.sheet.ReadEnum(current.handle);
					result = (a == keyword.ToLower());
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x0008779C File Offset: 0x0008599C
		protected override bool MatchNumber()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000877C4 File Offset: 0x000859C4
		protected override bool MatchInteger()
		{
			return this.current.handle.valueType == StyleValueType.Float;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000877EC File Offset: 0x000859EC
		protected override bool MatchLength()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool result;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				result = (dimension.unit == Dimension.Unit.Pixel);
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					float b = current.sheet.ReadFloat(current.handle);
					result = Mathf.Approximately(0f, b);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00087870 File Offset: 0x00085A70
		protected override bool MatchPercentage()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool result;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				result = (dimension.unit == Dimension.Unit.Percent);
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Float;
				if (flag2)
				{
					float b = current.sheet.ReadFloat(current.handle);
					result = Mathf.Approximately(0f, b);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000878F4 File Offset: 0x00085AF4
		protected override bool MatchColor()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Color;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = current.handle.valueType == StyleValueType.Enum;
				if (flag2)
				{
					Color clear = Color.clear;
					string text = current.sheet.ReadAsString(current.handle);
					bool flag3 = StyleSheetColor.TryGetColor(text.ToLower(), out clear);
					if (flag3)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0008796C File Offset: 0x00085B6C
		protected override bool MatchResource()
		{
			return this.current.handle.valueType == StyleValueType.ResourcePath;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00087994 File Offset: 0x00085B94
		protected override bool MatchUrl()
		{
			StyleValueType valueType = this.current.handle.valueType;
			return valueType == StyleValueType.AssetReference || valueType == StyleValueType.ScalableImage;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000879CC File Offset: 0x00085BCC
		protected override bool MatchTime()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			bool result;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				result = (dimension.unit == Dimension.Unit.Second || dimension.unit == Dimension.Unit.Millisecond);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00087A24 File Offset: 0x00085C24
		protected override bool MatchCustomIdent()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Enum;
			bool result;
			if (flag)
			{
				string text = current.sheet.ReadAsString(current.handle);
				Match match = BaseStyleMatcher.s_CustomIdentRegex.Match(text);
				result = (match.Success && match.Length == text.Length);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00087A90 File Offset: 0x00085C90
		protected override bool MatchAngle()
		{
			StylePropertyValue current = this.current;
			bool flag = current.handle.valueType == StyleValueType.Dimension;
			if (flag)
			{
				Dimension dimension = current.sheet.ReadDimension(current.handle);
				Dimension.Unit unit = dimension.unit;
				Dimension.Unit unit2 = unit;
				if (unit2 - Dimension.Unit.Degree <= 3)
				{
					return true;
				}
			}
			bool flag2 = current.handle.valueType == StyleValueType.Float;
			bool result;
			if (flag2)
			{
				float b = current.sheet.ReadFloat(current.handle);
				result = Mathf.Approximately(0f, b);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00087B24 File Offset: 0x00085D24
		public StylePropertyValueMatcher()
		{
		}

		// Token: 0x04000E48 RID: 3656
		private List<StylePropertyValue> m_Values;
	}
}
