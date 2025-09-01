using System;
using System.Collections.Generic;
using QFSW.QC.Utilities;
using TMPro;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000033 RID: 51
	public class QuantumTheme : ScriptableObject
	{
		// Token: 0x06000136 RID: 310 RVA: 0x0000708C File Offset: 0x0000528C
		private T FindTypeFormatter<T>(List<T> formatters, Type type) where T : TypeFormatter
		{
			foreach (T t in formatters)
			{
				if (type == t.Type || type.IsGenericTypeOf(t.Type))
				{
					return t;
				}
			}
			foreach (T t2 in formatters)
			{
				if (t2.Type.IsAssignableFrom(type))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007158 File Offset: 0x00005358
		public string ColorizeReturn(string data, Type type)
		{
			TypeColorFormatter typeColorFormatter = this.FindTypeFormatter<TypeColorFormatter>(this.TypeFormatters, type);
			if (typeColorFormatter == null)
			{
				return data.ColorText(this.DefaultReturnValueColor);
			}
			return data.ColorText(typeColorFormatter.Color);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007190 File Offset: 0x00005390
		public void GetCollectionFormatting(Type type, out string leftScoper, out string seperator, out string rightScoper)
		{
			CollectionFormatter collectionFormatter = this.FindTypeFormatter<CollectionFormatter>(this.CollectionFormatters, type);
			if (collectionFormatter == null)
			{
				leftScoper = "[";
				seperator = ",";
				rightScoper = "]";
				return;
			}
			leftScoper = collectionFormatter.LeftScoper.Replace("\\n", "\n");
			seperator = collectionFormatter.SeperatorString.Replace("\\n", "\n");
			rightScoper = collectionFormatter.RightScoper.Replace("\\n", "\n");
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000720C File Offset: 0x0000540C
		public QuantumTheme()
		{
		}

		// Token: 0x040000ED RID: 237
		[SerializeField]
		public TMP_FontAsset Font;

		// Token: 0x040000EE RID: 238
		[SerializeField]
		public Material PanelMaterial;

		// Token: 0x040000EF RID: 239
		[SerializeField]
		public Color PanelColor = Color.white;

		// Token: 0x040000F0 RID: 240
		[SerializeField]
		public Color CommandLogColor = new Color(0f, 1f, 1f);

		// Token: 0x040000F1 RID: 241
		[SerializeField]
		public Color SelectedSuggestionColor = new Color(1f, 1f, 0.55f);

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		public Color SuggestionColor = Color.gray;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		public Color ErrorColor = Color.red;

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		public Color WarningColor = new Color(1f, 0.5f, 0f);

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		public Color SuccessColor = Color.green;

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		public string TimestampFormat = "[{0}:{1}:{2}]";

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		public string CommandLogFormat = "> {0}";

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		public Color DefaultReturnValueColor = Color.white;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		public List<TypeColorFormatter> TypeFormatters = new List<TypeColorFormatter>(0);

		// Token: 0x040000FA RID: 250
		[SerializeField]
		public List<CollectionFormatter> CollectionFormatters = new List<CollectionFormatter>(0);
	}
}
