using System;

namespace IKVM.Reflection
{
	// Token: 0x02000033 RID: 51
	internal sealed class GenericEventInfo : EventInfo
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00007FF7 File Offset: 0x000061F7
		internal GenericEventInfo(Type typeInstance, EventInfo eventInfo)
		{
			this.typeInstance = typeInstance;
			this.eventInfo = eventInfo;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008010 File Offset: 0x00006210
		public override bool Equals(object obj)
		{
			GenericEventInfo genericEventInfo = obj as GenericEventInfo;
			return genericEventInfo != null && genericEventInfo.typeInstance == this.typeInstance && genericEventInfo.eventInfo == this.eventInfo;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008053 File Offset: 0x00006253
		public override int GetHashCode()
		{
			return this.typeInstance.GetHashCode() * 777 + this.eventInfo.GetHashCode();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008072 File Offset: 0x00006272
		public override EventAttributes Attributes
		{
			get
			{
				return this.eventInfo.Attributes;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000807F File Offset: 0x0000627F
		private MethodInfo Wrap(MethodInfo method)
		{
			if (method == null)
			{
				return null;
			}
			return new GenericMethodInstance(this.typeInstance, method, null);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008099 File Offset: 0x00006299
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			return this.Wrap(this.eventInfo.GetAddMethod(nonPublic));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000080AD File Offset: 0x000062AD
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			return this.Wrap(this.eventInfo.GetRaiseMethod(nonPublic));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000080C1 File Offset: 0x000062C1
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			return this.Wrap(this.eventInfo.GetRemoveMethod(nonPublic));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000080D8 File Offset: 0x000062D8
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MethodInfo[] otherMethods = this.eventInfo.GetOtherMethods(nonPublic);
			for (int i = 0; i < otherMethods.Length; i++)
			{
				otherMethods[i] = this.Wrap(otherMethods[i]);
			}
			return otherMethods;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008110 File Offset: 0x00006310
		public override MethodInfo[] __GetMethods()
		{
			MethodInfo[] array = this.eventInfo.__GetMethods();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Wrap(array[i]);
			}
			return array;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00008144 File Offset: 0x00006344
		public override Type EventHandlerType
		{
			get
			{
				return this.eventInfo.EventHandlerType.BindTypeParameters(this.typeInstance);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000815C File Offset: 0x0000635C
		public override string Name
		{
			get
			{
				return this.eventInfo.Name;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008169 File Offset: 0x00006369
		public override Type DeclaringType
		{
			get
			{
				return this.typeInstance;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008171 File Offset: 0x00006371
		public override Module Module
		{
			get
			{
				return this.eventInfo.Module;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000817E File Offset: 0x0000637E
		public override int MetadataToken
		{
			get
			{
				return this.eventInfo.MetadataToken;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000818B File Offset: 0x0000638B
		internal override EventInfo BindTypeParameters(Type type)
		{
			return new GenericEventInfo(this.typeInstance.BindTypeParameters(type), this.eventInfo);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000081A4 File Offset: 0x000063A4
		internal override bool IsPublic
		{
			get
			{
				return this.eventInfo.IsPublic;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000081B1 File Offset: 0x000063B1
		internal override bool IsNonPrivate
		{
			get
			{
				return this.eventInfo.IsNonPrivate;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000081BE File Offset: 0x000063BE
		internal override bool IsStatic
		{
			get
			{
				return this.eventInfo.IsStatic;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000081CB File Offset: 0x000063CB
		internal override bool IsBaked
		{
			get
			{
				return this.eventInfo.IsBaked;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000081D8 File Offset: 0x000063D8
		internal override int GetCurrentToken()
		{
			return this.eventInfo.GetCurrentToken();
		}

		// Token: 0x04000144 RID: 324
		private readonly Type typeInstance;

		// Token: 0x04000145 RID: 325
		private readonly EventInfo eventInfo;
	}
}
