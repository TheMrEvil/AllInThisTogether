using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B6 RID: 694
	internal class StyleVariableResolver
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0006275C File Offset: 0x0006095C
		private StyleSheet currentSheet
		{
			get
			{
				return this.m_CurrentContext.sheet;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x00062769 File Offset: 0x00060969
		private StyleValueHandle[] currentHandles
		{
			get
			{
				return this.m_CurrentContext.handles;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00062776 File Offset: 0x00060976
		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0006277E File Offset: 0x0006097E
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x00062786 File Offset: 0x00060986
		public StyleVariableContext variableContext
		{
			[CompilerGenerated]
			get
			{
				return this.<variableContext>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<variableContext>k__BackingField = value;
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0006278F File Offset: 0x0006098F
		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_ContextStack.Clear();
			this.m_Property = property;
			this.PushContext(sheet, handles);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000627BC File Offset: 0x000609BC
		private void PushContext(StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_CurrentContext = new StyleVariableResolver.ResolveContext
			{
				sheet = sheet,
				handles = handles
			};
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000627FB File Offset: 0x000609FB
		private void PopContext()
		{
			this.m_ContextStack.Pop();
			this.m_CurrentContext = this.m_ContextStack.Peek();
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0006281C File Offset: 0x00060A1C
		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.currentSheet,
				handle = handle
			});
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00062854 File Offset: 0x00060A54
		public bool ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			int argc;
			string varName;
			StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out argc, out varName);
			StyleVariableResolver.Result result = this.ResolveVarFunction(ref index, argc, varName);
			return result == StyleVariableResolver.Result.Valid;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00062898 File Offset: 0x00060A98
		private StyleVariableResolver.Result ResolveVarFunction(ref int index, int argc, string varName)
		{
			StyleVariableResolver.Result result = this.ResolveVariable(varName);
			bool flag = result == StyleVariableResolver.Result.NotFound && argc > 1;
			if (flag)
			{
				StyleValueHandle[] currentHandles = this.currentHandles;
				int num = index + 1;
				index = num;
				StyleValueHandle styleValueHandle = currentHandles[num];
				Debug.Assert(styleValueHandle.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", styleValueHandle.valueType));
				bool flag2 = styleValueHandle.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
				if (flag2)
				{
					index++;
					result = this.ResolveFallback(ref index);
				}
			}
			return result;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00062934 File Offset: 0x00060B34
		public bool ValidateResolvedValues()
		{
			bool isCustomProperty = this.m_Property.isCustomProperty;
			bool result;
			if (isCustomProperty)
			{
				result = true;
			}
			else
			{
				string syntax;
				bool flag = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out syntax);
				if (flag)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					result = false;
				}
				else
				{
					Expression exp = StyleVariableResolver.s_SyntaxParser.Parse(syntax);
					result = this.m_Matcher.Match(exp, this.m_ResolvedValues).success;
				}
			}
			return result;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000629C0 File Offset: 0x00060BC0
		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable styleVariable;
			bool flag = !this.variableContext.TryFindVariable(variableName, out styleVariable);
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(styleVariable.name);
				if (flag2)
				{
					result = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(styleVariable.name);
					StyleVariableResolver.Result result2 = StyleVariableResolver.Result.Valid;
					int num = 0;
					while (num < styleVariable.handles.Length && result2 == StyleVariableResolver.Result.Valid)
					{
						bool flag3 = this.m_ResolvedValues.Count + 1 > 100;
						if (flag3)
						{
							return StyleVariableResolver.Result.Invalid;
						}
						StyleValueHandle handle = styleVariable.handles[num];
						bool flag4 = handle.IsVarFunction();
						if (flag4)
						{
							this.PushContext(styleVariable.sheet, styleVariable.handles);
							int argc;
							string varName;
							StyleVariableResolver.ParseVarFunction(styleVariable.sheet, styleVariable.handles, ref num, out argc, out varName);
							result2 = this.ResolveVarFunction(ref num, argc, varName);
							this.PopContext();
						}
						else
						{
							this.m_ResolvedValues.Add(new StylePropertyValue
							{
								sheet = styleVariable.sheet,
								handle = handle
							});
						}
						num++;
					}
					this.m_ResolvedVarStack.Pop();
					result = result2;
				}
			}
			return result;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00062B08 File Offset: 0x00060D08
		private StyleVariableResolver.Result ResolveFallback(ref int index)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.currentHandles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle handle = this.currentHandles[index];
				bool flag = handle.IsVarFunction();
				if (flag)
				{
					int num;
					string variableName;
					StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out num, out variableName);
					result = this.ResolveVariable(variableName);
					bool flag2 = result == StyleVariableResolver.Result.NotFound;
					if (flag2)
					{
						bool flag3 = num > 1;
						if (flag3)
						{
							StyleValueHandle[] currentHandles = this.currentHandles;
							int num2 = index + 1;
							index = num2;
							handle = currentHandles[num2];
							Debug.Assert(handle.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var function", handle.valueType));
							bool flag4 = handle.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
							if (flag4)
							{
								index++;
								result = this.ResolveFallback(ref index);
							}
						}
					}
				}
				else
				{
					this.m_ResolvedValues.Add(new StylePropertyValue
					{
						sheet = this.currentSheet,
						handle = handle
					});
				}
				index++;
			}
			return result;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00062C40 File Offset: 0x00060E40
		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00062C7E File Offset: 0x00060E7E
		public StyleVariableResolver()
		{
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00062CB3 File Offset: 0x00060EB3
		// Note: this type is marked as 'beforefieldinit'.
		static StyleVariableResolver()
		{
		}

		// Token: 0x04000A28 RID: 2600
		internal const int kMaxResolves = 100;

		// Token: 0x04000A29 RID: 2601
		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		// Token: 0x04000A2A RID: 2602
		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		// Token: 0x04000A2B RID: 2603
		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		// Token: 0x04000A2C RID: 2604
		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		// Token: 0x04000A2D RID: 2605
		private StyleProperty m_Property;

		// Token: 0x04000A2E RID: 2606
		private Stack<StyleVariableResolver.ResolveContext> m_ContextStack = new Stack<StyleVariableResolver.ResolveContext>();

		// Token: 0x04000A2F RID: 2607
		private StyleVariableResolver.ResolveContext m_CurrentContext;

		// Token: 0x04000A30 RID: 2608
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private StyleVariableContext <variableContext>k__BackingField;

		// Token: 0x020002B7 RID: 695
		private enum Result
		{
			// Token: 0x04000A32 RID: 2610
			Valid,
			// Token: 0x04000A33 RID: 2611
			Invalid,
			// Token: 0x04000A34 RID: 2612
			NotFound
		}

		// Token: 0x020002B8 RID: 696
		private struct ResolveContext
		{
			// Token: 0x04000A35 RID: 2613
			public StyleSheet sheet;

			// Token: 0x04000A36 RID: 2614
			public StyleValueHandle[] handles;
		}
	}
}
