using System;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x0200007D RID: 125
	internal class ReflectionMethod : ReflectionMember
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000A070 File Offset: 0x00008270
		public ReflectionMethod(MethodInfo method)
		{
			Assumes.NotNull<MethodInfo>(method);
			this._method = method;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A085 File Offset: 0x00008285
		public MethodInfo UnderlyingMethod
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A08D File Offset: 0x0000828D
		public override MemberInfo UnderlyingMember
		{
			get
			{
				return this.UnderlyingMethod;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00005907 File Offset: 0x00003B07
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A095 File Offset: 0x00008295
		public override bool RequiresInstance
		{
			get
			{
				return !this.UnderlyingMethod.IsStatic;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000A0A5 File Offset: 0x000082A5
		public override Type ReturnType
		{
			get
			{
				return this.UnderlyingMethod.ReturnType;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000A0B2 File Offset: 0x000082B2
		public override ReflectionItemType ItemType
		{
			get
			{
				return ReflectionItemType.Method;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A0B5 File Offset: 0x000082B5
		public override object GetValue(object instance)
		{
			return ReflectionMethod.SafeCreateExportedDelegate(instance, this._method);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A0C3 File Offset: 0x000082C3
		private static ExportedDelegate SafeCreateExportedDelegate(object instance, MethodInfo method)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(method);
			return new ExportedDelegate(instance, method);
		}

		// Token: 0x04000158 RID: 344
		private readonly MethodInfo _method;
	}
}
