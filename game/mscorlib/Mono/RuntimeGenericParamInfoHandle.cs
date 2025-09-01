using System;
using System.Reflection;

namespace Mono
{
	// Token: 0x0200004A RID: 74
	internal struct RuntimeGenericParamInfoHandle
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00004782 File Offset: 0x00002982
		internal unsafe RuntimeGenericParamInfoHandle(RuntimeStructs.GenericParamInfo* value)
		{
			this.value = value;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000478B File Offset: 0x0000298B
		internal unsafe RuntimeGenericParamInfoHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GenericParamInfo*)((void*)ptr);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004799 File Offset: 0x00002999
		internal Type[] Constraints
		{
			get
			{
				return this.GetConstraints();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000047A1 File Offset: 0x000029A1
		internal unsafe GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.value->flags;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000047B0 File Offset: 0x000029B0
		private unsafe Type[] GetConstraints()
		{
			int constraintsCount = this.GetConstraintsCount();
			Type[] array = new Type[constraintsCount];
			for (int i = 0; i < constraintsCount; i++)
			{
				RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(*(IntPtr*)(this.value->constraints + (IntPtr)i * (IntPtr)sizeof(RuntimeStructs.MonoClass*) / (IntPtr)sizeof(RuntimeStructs.MonoClass*)));
				array[i] = Type.GetTypeFromHandle(runtimeClassHandle.GetTypeHandle());
			}
			return array;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004804 File Offset: 0x00002A04
		private unsafe int GetConstraintsCount()
		{
			int num = 0;
			RuntimeStructs.MonoClass** ptr = this.value->constraints;
			while (ptr != null && *(IntPtr*)ptr != (IntPtr)((UIntPtr)0))
			{
				ptr += sizeof(RuntimeStructs.MonoClass*) / sizeof(RuntimeStructs.MonoClass*);
				num++;
			}
			return num;
		}

		// Token: 0x04000DE1 RID: 3553
		private unsafe RuntimeStructs.GenericParamInfo* value;
	}
}
