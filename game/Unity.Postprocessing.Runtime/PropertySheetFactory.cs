using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200006A RID: 106
	public sealed class PropertySheetFactory
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00010CA2 File Offset: 0x0000EEA2
		public PropertySheetFactory()
		{
			this.m_Sheets = new Dictionary<Shader, PropertySheet>();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		[Obsolete("Use PropertySheet.Get(Shader) with a direct reference to the Shader instead.")]
		public PropertySheet Get(string shaderName)
		{
			Shader shader = Shader.Find(shaderName);
			if (shader == null)
			{
				throw new ArgumentException(string.Format("Invalid shader ({0})", shaderName));
			}
			return this.Get(shader);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public PropertySheet Get(Shader shader)
		{
			if (shader == null)
			{
				throw new ArgumentException(string.Format("Invalid shader ({0})", shader));
			}
			PropertySheet propertySheet;
			if (this.m_Sheets.TryGetValue(shader, out propertySheet))
			{
				return propertySheet;
			}
			string name = shader.name;
			propertySheet = new PropertySheet(new Material(shader)
			{
				name = string.Format("PostProcess - {0}", name.Substring(name.LastIndexOf('/') + 1)),
				hideFlags = HideFlags.DontSave
			});
			this.m_Sheets.Add(shader, propertySheet);
			return propertySheet;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010D74 File Offset: 0x0000EF74
		public void Release()
		{
			foreach (PropertySheet propertySheet in this.m_Sheets.Values)
			{
				propertySheet.Release();
			}
			this.m_Sheets.Clear();
		}

		// Token: 0x04000221 RID: 545
		private readonly Dictionary<Shader, PropertySheet> m_Sheets;
	}
}
