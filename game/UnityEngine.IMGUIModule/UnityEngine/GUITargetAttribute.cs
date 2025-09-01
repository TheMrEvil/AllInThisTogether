using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Method)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000C0D3 File Offset: 0x0000A2D3
		public GUITargetAttribute()
		{
			this.displayMask = -1;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public GUITargetAttribute(int displayIndex)
		{
			this.displayMask = 1 << displayIndex;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C0FA File Offset: 0x0000A2FA
		public GUITargetAttribute(int displayIndex, int displayIndex1)
		{
			this.displayMask = (1 << displayIndex | 1 << displayIndex1);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C118 File Offset: 0x0000A318
		public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
		{
			this.displayMask = (1 << displayIndex | 1 << displayIndex1);
			for (int i = 0; i < displayIndexList.Length; i++)
			{
				this.displayMask |= 1 << displayIndexList[i];
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C168 File Offset: 0x0000A368
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool flag = method != null;
			if (flag)
			{
				object[] customAttributes = method.GetCustomAttributes(true);
				bool flag2 = customAttributes != null;
				if (flag2)
				{
					for (int i = 0; i < customAttributes.Length; i++)
					{
						bool flag3 = customAttributes[i].GetType() != typeof(GUITargetAttribute);
						if (!flag3)
						{
							GUITargetAttribute guitargetAttribute = customAttributes[i] as GUITargetAttribute;
							return guitargetAttribute.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x040000E3 RID: 227
		internal int displayMask;
	}
}
