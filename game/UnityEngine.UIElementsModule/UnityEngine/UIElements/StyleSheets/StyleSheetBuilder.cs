using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000368 RID: 872
	internal class StyleSheetBuilder
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000851E8 File Offset: 0x000833E8
		public StyleProperty currentProperty
		{
			get
			{
				return this.m_CurrentProperty;
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000851F0 File Offset: 0x000833F0
		public StyleRule BeginRule(int ruleLine)
		{
			StyleSheetBuilder.Log("Beginning rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentRule = new StyleRule
			{
				line = ruleLine
			};
			return this.m_CurrentRule;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0008523C File Offset: 0x0008343C
		public StyleSheetBuilder.ComplexSelectorScope BeginComplexSelector(int specificity)
		{
			StyleSheetBuilder.Log("Begin complex selector with specificity " + specificity.ToString());
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.ComplexSelector;
			this.m_CurrentComplexSelector = new StyleComplexSelector();
			this.m_CurrentComplexSelector.specificity = specificity;
			this.m_CurrentComplexSelector.ruleIndex = this.m_Rules.Count;
			return new StyleSheetBuilder.ComplexSelectorScope(this);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000852B0 File Offset: 0x000834B0
		public void AddSimpleSelector(StyleSelectorPart[] parts, StyleSelectorRelationship previousRelationsip)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			StyleSelector styleSelector = new StyleSelector();
			styleSelector.parts = parts;
			styleSelector.previousRelationship = previousRelationsip;
			string str = "Add simple selector ";
			StyleSelector styleSelector2 = styleSelector;
			StyleSheetBuilder.Log(str + ((styleSelector2 != null) ? styleSelector2.ToString() : null));
			this.m_CurrentSelectors.Add(styleSelector);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00085310 File Offset: 0x00083510
		public void EndComplexSelector()
		{
			StyleSheetBuilder.Log("Ending complex selector");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			bool flag = this.m_CurrentSelectors.Count > 0;
			if (flag)
			{
				this.m_CurrentComplexSelector.selectors = this.m_CurrentSelectors.ToArray();
				this.m_ComplexSelectors.Add(this.m_CurrentComplexSelector);
				this.m_CurrentSelectors.Clear();
			}
			this.m_CurrentComplexSelector = null;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00085390 File Offset: 0x00083590
		public StyleProperty BeginProperty(string name, int line = -1)
		{
			StyleSheetBuilder.Log("Begin property named " + name);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Property;
			this.m_CurrentProperty = new StyleProperty
			{
				name = name,
				line = line
			};
			this.m_CurrentProperties.Add(this.m_CurrentProperty);
			return this.m_CurrentProperty;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000853FC File Offset: 0x000835FC
		public void AddImport(StyleSheet.ImportStruct importStruct)
		{
			this.m_Imports.Add(importStruct);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0008540C File Offset: 0x0008360C
		public void AddValue(float value)
		{
			this.RegisterValue<float>(this.m_Floats, StyleValueType.Float, value);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0008541E File Offset: 0x0008361E
		public void AddValue(Dimension value)
		{
			this.RegisterValue<Dimension>(this.m_Dimensions, StyleValueType.Dimension, value);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00085430 File Offset: 0x00083630
		public void AddValue(StyleValueKeyword keyword)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)keyword, StyleValueType.Keyword));
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00085446 File Offset: 0x00083646
		public void AddValue(StyleValueFunction function)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)function, StyleValueType.Function));
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x0008545D File Offset: 0x0008365D
		public void AddCommaSeparator()
		{
			this.m_CurrentValues.Add(new StyleValueHandle(0, StyleValueType.CommaSeparator));
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00085474 File Offset: 0x00083674
		public void AddValue(string value, StyleValueType type)
		{
			bool flag = type == StyleValueType.Variable;
			if (flag)
			{
				this.RegisterVariable(value);
			}
			else
			{
				this.RegisterValue<string>(this.m_Strings, type, value);
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000854A3 File Offset: 0x000836A3
		public void AddValue(Color value)
		{
			this.RegisterValue<Color>(this.m_Colors, StyleValueType.Color, value);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000854B5 File Offset: 0x000836B5
		public void AddValue(Object value)
		{
			this.RegisterValue<Object>(this.m_Assets, StyleValueType.AssetReference, value);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000854C7 File Offset: 0x000836C7
		public void AddValue(ScalableImage value)
		{
			this.RegisterValue<ScalableImage>(this.m_ScalableImages, StyleValueType.ScalableImage, value);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000854DC File Offset: 0x000836DC
		public void EndProperty()
		{
			StyleSheetBuilder.Log("Ending property");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentProperty.values = this.m_CurrentValues.ToArray();
			this.m_CurrentProperty = null;
			this.m_CurrentValues.Clear();
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00085538 File Offset: 0x00083738
		public int EndRule()
		{
			StyleSheetBuilder.Log("Ending rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Init;
			this.m_CurrentRule.properties = this.m_CurrentProperties.ToArray();
			this.m_Rules.Add(this.m_CurrentRule);
			this.m_CurrentRule = null;
			this.m_CurrentProperties.Clear();
			return this.m_Rules.Count - 1;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000855B4 File Offset: 0x000837B4
		public void BuildTo(StyleSheet writeTo)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			writeTo.floats = this.m_Floats.ToArray();
			writeTo.dimensions = this.m_Dimensions.ToArray();
			writeTo.colors = this.m_Colors.ToArray();
			writeTo.strings = this.m_Strings.ToArray();
			writeTo.rules = this.m_Rules.ToArray();
			writeTo.assets = this.m_Assets.ToArray();
			writeTo.scalableImages = this.m_ScalableImages.ToArray();
			writeTo.complexSelectors = this.m_ComplexSelectors.ToArray();
			writeTo.imports = this.m_Imports.ToArray();
			bool flag = writeTo.imports.Length != 0;
			if (flag)
			{
				writeTo.FlattenImportedStyleSheetsRecursive();
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00085684 File Offset: 0x00083884
		private void RegisterVariable(string value)
		{
			StyleSheetBuilder.Log("Add variable : " + value);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			int num = this.m_Strings.IndexOf(value);
			bool flag = num < 0;
			if (flag)
			{
				this.m_Strings.Add(value);
				num = this.m_Strings.Count - 1;
			}
			this.m_CurrentValues.Add(new StyleValueHandle(num, StyleValueType.Variable));
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000856F8 File Offset: 0x000838F8
		private void RegisterValue<T>(List<T> list, StyleValueType type, T value)
		{
			string str = "Add value of type ";
			string str2 = type.ToString();
			string str3 = " : ";
			T t = value;
			StyleSheetBuilder.Log(str + str2 + str3 + ((t != null) ? t.ToString() : null));
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			list.Add(value);
			this.m_CurrentValues.Add(new StyleValueHandle(list.Count - 1, type));
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00002166 File Offset: 0x00000366
		private static void Log(string msg)
		{
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00085774 File Offset: 0x00083974
		public StyleSheetBuilder()
		{
		}

		// Token: 0x04000E09 RID: 3593
		private StyleSheetBuilder.BuilderState m_BuilderState;

		// Token: 0x04000E0A RID: 3594
		private List<float> m_Floats = new List<float>();

		// Token: 0x04000E0B RID: 3595
		private List<Dimension> m_Dimensions = new List<Dimension>();

		// Token: 0x04000E0C RID: 3596
		private List<Color> m_Colors = new List<Color>();

		// Token: 0x04000E0D RID: 3597
		private List<string> m_Strings = new List<string>();

		// Token: 0x04000E0E RID: 3598
		private List<StyleRule> m_Rules = new List<StyleRule>();

		// Token: 0x04000E0F RID: 3599
		private List<Object> m_Assets = new List<Object>();

		// Token: 0x04000E10 RID: 3600
		private List<ScalableImage> m_ScalableImages = new List<ScalableImage>();

		// Token: 0x04000E11 RID: 3601
		private List<StyleComplexSelector> m_ComplexSelectors = new List<StyleComplexSelector>();

		// Token: 0x04000E12 RID: 3602
		private List<StyleProperty> m_CurrentProperties = new List<StyleProperty>();

		// Token: 0x04000E13 RID: 3603
		private List<StyleValueHandle> m_CurrentValues = new List<StyleValueHandle>();

		// Token: 0x04000E14 RID: 3604
		private StyleComplexSelector m_CurrentComplexSelector;

		// Token: 0x04000E15 RID: 3605
		private List<StyleSelector> m_CurrentSelectors = new List<StyleSelector>();

		// Token: 0x04000E16 RID: 3606
		private StyleProperty m_CurrentProperty;

		// Token: 0x04000E17 RID: 3607
		private StyleRule m_CurrentRule;

		// Token: 0x04000E18 RID: 3608
		private List<StyleSheet.ImportStruct> m_Imports = new List<StyleSheet.ImportStruct>();

		// Token: 0x02000369 RID: 873
		public struct ComplexSelectorScope : IDisposable
		{
			// Token: 0x06001C49 RID: 7241 RVA: 0x0008580C File Offset: 0x00083A0C
			public ComplexSelectorScope(StyleSheetBuilder builder)
			{
				this.m_Builder = builder;
			}

			// Token: 0x06001C4A RID: 7242 RVA: 0x00085816 File Offset: 0x00083A16
			public void Dispose()
			{
				this.m_Builder.EndComplexSelector();
			}

			// Token: 0x04000E19 RID: 3609
			private StyleSheetBuilder m_Builder;
		}

		// Token: 0x0200036A RID: 874
		private enum BuilderState
		{
			// Token: 0x04000E1B RID: 3611
			Init,
			// Token: 0x04000E1C RID: 3612
			Rule,
			// Token: 0x04000E1D RID: 3613
			ComplexSelector,
			// Token: 0x04000E1E RID: 3614
			Property
		}
	}
}
