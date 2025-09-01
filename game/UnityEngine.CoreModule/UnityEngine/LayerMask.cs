using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020C RID: 524
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	[NativeHeader("Runtime/BaseClasses/BitField.h")]
	[NativeClass("BitField", "struct BitField;")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct LayerMask
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x00025218 File Offset: 0x00023418
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00025230 File Offset: 0x00023430
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask result;
			result.m_Mask = intVal;
			return result;
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x0002524C File Offset: 0x0002344C
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x00025264 File Offset: 0x00023464
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x0600171D RID: 5917
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("LayerToString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string LayerToName(int layer);

		// Token: 0x0600171E RID: 5918
		[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
		[NativeMethod("StringToLayer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x0600171F RID: 5919 RVA: 0x00025270 File Offset: 0x00023470
		public static int GetMask(params string[] layerNames)
		{
			bool flag = layerNames == null;
			if (flag)
			{
				throw new ArgumentNullException("layerNames");
			}
			int num = 0;
			foreach (string layerName in layerNames)
			{
				int num2 = LayerMask.NameToLayer(layerName);
				bool flag2 = num2 != -1;
				if (flag2)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x040007F9 RID: 2041
		[NativeName("m_Bits")]
		private int m_Mask;
	}
}
