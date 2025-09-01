using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000086 RID: 134
	internal class ReflectionProperty : ReflectionWritableMember
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000AA10 File Offset: 0x00008C10
		public ReflectionProperty(MethodInfo getMethod, MethodInfo setMethod)
		{
			Assumes.IsTrue(getMethod != null || setMethod != null);
			this._getMethod = getMethod;
			this._setMethod = setMethod;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000AA3E File Offset: 0x00008C3E
		public override MemberInfo UnderlyingMember
		{
			get
			{
				return this.UnderlyingGetMethod ?? this.UnderlyingSetMethod;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000AA50 File Offset: 0x00008C50
		public override bool CanRead
		{
			get
			{
				return this.UnderlyingGetMethod != null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000AA5E File Offset: 0x00008C5E
		public override bool CanWrite
		{
			get
			{
				return this.UnderlyingSetMethod != null;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public MethodInfo UnderlyingGetMethod
		{
			get
			{
				return this._getMethod;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000AA74 File Offset: 0x00008C74
		public MethodInfo UnderlyingSetMethod
		{
			get
			{
				return this._setMethod;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public override string Name
		{
			get
			{
				string name = (this.UnderlyingGetMethod ?? this.UnderlyingSetMethod).Name;
				Assumes.IsTrue(name.Length > 4);
				return name.Substring(4);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000AAA7 File Offset: 0x00008CA7
		public override string GetDisplayName()
		{
			return ReflectionServices.GetDisplayName(base.DeclaringType, this.Name);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000AABA File Offset: 0x00008CBA
		public override bool RequiresInstance
		{
			get
			{
				return !(this.UnderlyingGetMethod ?? this.UnderlyingSetMethod).IsStatic;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		public override Type ReturnType
		{
			get
			{
				if (this.UnderlyingGetMethod != null)
				{
					return this.UnderlyingGetMethod.ReturnType;
				}
				ParameterInfo[] parameters = this.UnderlyingSetMethod.GetParameters();
				Assumes.IsTrue(parameters.Length != 0);
				return parameters[parameters.Length - 1].ParameterType;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00005EFC File Offset: 0x000040FC
		public override ReflectionItemType ItemType
		{
			get
			{
				return ReflectionItemType.Property;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000AB10 File Offset: 0x00008D10
		public override object GetValue(object instance)
		{
			Assumes.NotNull<MethodInfo>(this._getMethod);
			return this.UnderlyingGetMethod.SafeInvoke(instance, Array.Empty<object>());
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000AB2E File Offset: 0x00008D2E
		public override void SetValue(object instance, object value)
		{
			Assumes.NotNull<MethodInfo>(this._setMethod);
			this.UnderlyingSetMethod.SafeInvoke(instance, new object[]
			{
				value
			});
		}

		// Token: 0x0400016F RID: 367
		private readonly MethodInfo _getMethod;

		// Token: 0x04000170 RID: 368
		private readonly MethodInfo _setMethod;
	}
}
