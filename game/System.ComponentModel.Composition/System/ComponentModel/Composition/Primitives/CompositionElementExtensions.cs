using System;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x02000091 RID: 145
	internal static class CompositionElementExtensions
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000AEA1 File Offset: 0x000090A1
		public static ICompositionElement ToSerializableElement(this ICompositionElement element)
		{
			return SerializableCompositionElement.FromICompositionElement(element);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000AEAC File Offset: 0x000090AC
		public static ICompositionElement ToElement(this Export export)
		{
			ICompositionElement compositionElement = export as ICompositionElement;
			if (compositionElement != null)
			{
				return compositionElement;
			}
			return export.Definition.ToElement();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static ICompositionElement ToElement(this ExportDefinition definition)
		{
			return CompositionElementExtensions.ToElementCore(definition);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static ICompositionElement ToElement(this ImportDefinition definition)
		{
			return CompositionElementExtensions.ToElementCore(definition);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static ICompositionElement ToElement(this ComposablePart part)
		{
			return CompositionElementExtensions.ToElementCore(part);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static ICompositionElement ToElement(this ComposablePartDefinition definition)
		{
			return CompositionElementExtensions.ToElementCore(definition);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000AED8 File Offset: 0x000090D8
		public static string GetDisplayName(this ComposablePartDefinition definition)
		{
			return CompositionElementExtensions.GetDisplayNameCore(definition);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000AED8 File Offset: 0x000090D8
		public static string GetDisplayName(this ComposablePartCatalog catalog)
		{
			return CompositionElementExtensions.GetDisplayNameCore(catalog);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000AEE0 File Offset: 0x000090E0
		private static string GetDisplayNameCore(object value)
		{
			ICompositionElement compositionElement = value as ICompositionElement;
			if (compositionElement != null)
			{
				return compositionElement.DisplayName;
			}
			return value.ToString();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000AF04 File Offset: 0x00009104
		private static ICompositionElement ToElementCore(object value)
		{
			ICompositionElement compositionElement = value as ICompositionElement;
			if (compositionElement != null)
			{
				return compositionElement;
			}
			return new CompositionElement(value);
		}
	}
}
