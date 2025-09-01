using System;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class TypeRegistryItemAttribute : Attribute
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00003D9C File Offset: 0x00001F9C
		public TypeRegistryItemAttribute(string name = null, string categoryPath = null, SdfIconType icon = SdfIconType.None, float lightIconColorR = 0f, float lightIconColorG = 0f, float lightIconColorB = 0f, float lightIconColorA = 0f, float darkIconColorR = 0f, float darkIconColorG = 0f, float darkIconColorB = 0f, float darkIconColorA = 0f, int priority = 0)
		{
			this.Name = name;
			this.CategoryPath = categoryPath;
			this.Icon = icon;
			bool flag = lightIconColorR != 0f || lightIconColorG != 0f || lightIconColorB != 0f || lightIconColorA > 0f;
			if (flag)
			{
				float a = (lightIconColorA > 0f) ? lightIconColorA : 1f;
				this.LightIconColor = new Color?(new Color(lightIconColorR, lightIconColorG, lightIconColorB, a));
			}
			else
			{
				this.LightIconColor = null;
			}
			bool flag2 = darkIconColorR != 0f || darkIconColorG != 0f || darkIconColorB != 0f || darkIconColorA > 0f;
			if (flag2)
			{
				float a2 = (darkIconColorA > 0f) ? darkIconColorA : 1f;
				this.DarkIconColor = new Color?(new Color(darkIconColorR, darkIconColorG, darkIconColorB, a2));
			}
			else
			{
				this.DarkIconColor = null;
			}
			this.Priority = priority;
		}

		// Token: 0x0400014F RID: 335
		public string Name;

		// Token: 0x04000150 RID: 336
		public string CategoryPath;

		// Token: 0x04000151 RID: 337
		public SdfIconType Icon;

		// Token: 0x04000152 RID: 338
		public Color? LightIconColor;

		// Token: 0x04000153 RID: 339
		public Color? DarkIconColor;

		// Token: 0x04000154 RID: 340
		public int Priority;
	}
}
