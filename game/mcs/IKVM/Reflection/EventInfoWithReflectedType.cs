using System;

namespace IKVM.Reflection
{
	// Token: 0x02000026 RID: 38
	internal sealed class EventInfoWithReflectedType : EventInfo
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000055EA File Offset: 0x000037EA
		internal EventInfoWithReflectedType(Type reflectedType, EventInfo eventInfo)
		{
			this.reflectedType = reflectedType;
			this.eventInfo = eventInfo;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005600 File Offset: 0x00003800
		public override EventAttributes Attributes
		{
			get
			{
				return this.eventInfo.Attributes;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000560D File Offset: 0x0000380D
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.eventInfo.GetAddMethod(nonPublic), this.reflectedType);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005626 File Offset: 0x00003826
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.eventInfo.GetRaiseMethod(nonPublic), this.reflectedType);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000563F File Offset: 0x0000383F
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.eventInfo.GetRemoveMethod(nonPublic), this.reflectedType);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005658 File Offset: 0x00003858
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.eventInfo.GetOtherMethods(nonPublic), this.reflectedType);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005671 File Offset: 0x00003871
		public override MethodInfo[] __GetMethods()
		{
			return MemberInfo.SetReflectedType<MethodInfo>(this.eventInfo.__GetMethods(), this.reflectedType);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005689 File Offset: 0x00003889
		public override Type EventHandlerType
		{
			get
			{
				return this.eventInfo.EventHandlerType;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005696 File Offset: 0x00003896
		internal override bool IsPublic
		{
			get
			{
				return this.eventInfo.IsPublic;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000056A3 File Offset: 0x000038A3
		internal override bool IsNonPrivate
		{
			get
			{
				return this.eventInfo.IsNonPrivate;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000056B0 File Offset: 0x000038B0
		internal override bool IsStatic
		{
			get
			{
				return this.eventInfo.IsStatic;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000056BD File Offset: 0x000038BD
		internal override EventInfo BindTypeParameters(Type type)
		{
			return this.eventInfo.BindTypeParameters(type);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000056CB File Offset: 0x000038CB
		public override string ToString()
		{
			return this.eventInfo.ToString();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000056D8 File Offset: 0x000038D8
		public override bool __IsMissing
		{
			get
			{
				return this.eventInfo.__IsMissing;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000056E5 File Offset: 0x000038E5
		public override Type DeclaringType
		{
			get
			{
				return this.eventInfo.DeclaringType;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000056F2 File Offset: 0x000038F2
		public override Type ReflectedType
		{
			get
			{
				return this.reflectedType;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000056FC File Offset: 0x000038FC
		public override bool Equals(object obj)
		{
			EventInfoWithReflectedType eventInfoWithReflectedType = obj as EventInfoWithReflectedType;
			return eventInfoWithReflectedType != null && eventInfoWithReflectedType.reflectedType == this.reflectedType && eventInfoWithReflectedType.eventInfo == this.eventInfo;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000573F File Offset: 0x0000393F
		public override int GetHashCode()
		{
			return this.reflectedType.GetHashCode() ^ this.eventInfo.GetHashCode();
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005758 File Offset: 0x00003958
		public override int MetadataToken
		{
			get
			{
				return this.eventInfo.MetadataToken;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005765 File Offset: 0x00003965
		public override Module Module
		{
			get
			{
				return this.eventInfo.Module;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005772 File Offset: 0x00003972
		public override string Name
		{
			get
			{
				return this.eventInfo.Name;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000577F File Offset: 0x0000397F
		internal override bool IsBaked
		{
			get
			{
				return this.eventInfo.IsBaked;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000578C File Offset: 0x0000398C
		internal override int GetCurrentToken()
		{
			return this.eventInfo.GetCurrentToken();
		}

		// Token: 0x04000113 RID: 275
		private readonly Type reflectedType;

		// Token: 0x04000114 RID: 276
		private readonly EventInfo eventInfo;
	}
}
