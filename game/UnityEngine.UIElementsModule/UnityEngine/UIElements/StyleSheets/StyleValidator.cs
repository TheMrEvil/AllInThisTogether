using System;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200037B RID: 891
	internal class StyleValidator
	{
		// Token: 0x06001CAA RID: 7338 RVA: 0x00087DB3 File Offset: 0x00085FB3
		public StyleValidator()
		{
			this.m_SyntaxParser = new StyleSyntaxParser();
			this.m_StyleMatcher = new StyleMatcher();
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00087DD4 File Offset: 0x00085FD4
		public StyleValidationResult ValidateProperty(string name, string value)
		{
			StyleValidationResult styleValidationResult = new StyleValidationResult
			{
				status = StyleValidationStatus.Ok
			};
			bool flag = name.StartsWith("--");
			StyleValidationResult result;
			if (flag)
			{
				result = styleValidationResult;
			}
			else
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(name, out text);
				if (flag2)
				{
					string text2 = StylePropertyCache.FindClosestPropertyName(name);
					styleValidationResult.status = StyleValidationStatus.Error;
					styleValidationResult.message = "Unknown property '" + name + "'";
					bool flag3 = !string.IsNullOrEmpty(text2);
					if (flag3)
					{
						styleValidationResult.message = styleValidationResult.message + " (did you mean '" + text2 + "'?)";
					}
					result = styleValidationResult;
				}
				else
				{
					Expression expression = this.m_SyntaxParser.Parse(text);
					bool flag4 = expression == null;
					if (flag4)
					{
						styleValidationResult.status = StyleValidationStatus.Error;
						styleValidationResult.message = string.Concat(new string[]
						{
							"Invalid '",
							name,
							"' property syntax '",
							text,
							"'"
						});
						result = styleValidationResult;
					}
					else
					{
						MatchResult matchResult = this.m_StyleMatcher.Match(expression, value);
						bool flag5 = !matchResult.success;
						if (flag5)
						{
							styleValidationResult.errorValue = matchResult.errorValue;
							switch (matchResult.errorCode)
							{
							case MatchResultErrorCode.Syntax:
							{
								styleValidationResult.status = StyleValidationStatus.Error;
								string str;
								bool flag6 = this.IsUnitMissing(text, value, out str);
								if (flag6)
								{
									styleValidationResult.hint = "Property expects a unit. Did you forget to add " + str + "?";
								}
								else
								{
									bool flag7 = this.IsUnsupportedColor(text);
									if (flag7)
									{
										styleValidationResult.hint = "Unsupported color '" + value + "'.";
									}
								}
								styleValidationResult.message = string.Concat(new string[]
								{
									"Expected (",
									text,
									") but found '",
									matchResult.errorValue,
									"'"
								});
								break;
							}
							case MatchResultErrorCode.EmptyValue:
								styleValidationResult.status = StyleValidationStatus.Error;
								styleValidationResult.message = "Expected (" + text + ") but found empty value";
								break;
							case MatchResultErrorCode.ExpectedEndOfValue:
								styleValidationResult.status = StyleValidationStatus.Warning;
								styleValidationResult.message = "Expected end of value but found '" + matchResult.errorValue + "'";
								break;
							default:
								Debug.LogAssertion(string.Format("Unexpected error code '{0}'", matchResult.errorCode));
								break;
							}
						}
						result = styleValidationResult;
					}
				}
			}
			return result;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00088028 File Offset: 0x00086228
		private bool IsUnitMissing(string propertySyntax, string propertyValue, out string unitHint)
		{
			unitHint = null;
			float num;
			bool flag = !float.TryParse(propertyValue, out num);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = propertySyntax.Contains("<length>") || propertySyntax.Contains("<length-percentage>");
				if (flag2)
				{
					unitHint = "px or %";
				}
				else
				{
					bool flag3 = propertySyntax.Contains("<time>");
					if (flag3)
					{
						unitHint = "s or ms";
					}
				}
				result = !string.IsNullOrEmpty(unitHint);
			}
			return result;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0008809C File Offset: 0x0008629C
		private bool IsUnsupportedColor(string propertySyntax)
		{
			return propertySyntax.StartsWith("<color>");
		}

		// Token: 0x04000E55 RID: 3669
		private StyleSyntaxParser m_SyntaxParser;

		// Token: 0x04000E56 RID: 3670
		private StyleMatcher m_StyleMatcher;
	}
}
