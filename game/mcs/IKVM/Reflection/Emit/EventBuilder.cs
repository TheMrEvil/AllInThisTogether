using System;
using System.Collections.Generic;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000E2 RID: 226
	public sealed class EventBuilder : EventInfo
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x000234D8 File Offset: 0x000216D8
		internal EventBuilder(TypeBuilder typeBuilder, string name, EventAttributes attributes, Type eventtype)
		{
			this.typeBuilder = typeBuilder;
			this.name = name;
			this.attributes = attributes;
			this.eventtype = typeBuilder.ModuleBuilder.GetTypeTokenForMemberRef(eventtype);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00023514 File Offset: 0x00021714
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			this.addOnMethod = mdBuilder;
			EventBuilder.Accessor item;
			item.Semantics = 8;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00023544 File Offset: 0x00021744
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			this.removeOnMethod = mdBuilder;
			EventBuilder.Accessor item;
			item.Semantics = 16;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00023578 File Offset: 0x00021778
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			this.fireMethod = mdBuilder;
			EventBuilder.Accessor item;
			item.Semantics = 32;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000235AC File Offset: 0x000217AC
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			EventBuilder.Accessor item;
			item.Semantics = 4;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000235D5 File Offset: 0x000217D5
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000235E4 File Offset: 0x000217E4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder.KnownCA == KnownCA.SpecialNameAttribute)
			{
				this.attributes |= EventAttributes.SpecialName;
				return;
			}
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = this.typeBuilder.ModuleBuilder.AllocPseudoToken();
			}
			this.typeBuilder.ModuleBuilder.SetCustomAttribute(this.lazyPseudoToken, customBuilder);
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00023643 File Offset: 0x00021843
		public override EventAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002364B File Offset: 0x0002184B
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (!nonPublic && (!(this.addOnMethod != null) || !this.addOnMethod.IsPublic))
			{
				return null;
			}
			return this.addOnMethod;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00023673 File Offset: 0x00021873
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (!nonPublic && (!(this.removeOnMethod != null) || !this.removeOnMethod.IsPublic))
			{
				return null;
			}
			return this.removeOnMethod;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002369B File Offset: 0x0002189B
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (!nonPublic && (!(this.fireMethod != null) || !this.fireMethod.IsPublic))
			{
				return null;
			}
			return this.fireMethod;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000236C4 File Offset: 0x000218C4
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (EventBuilder.Accessor accessor in this.accessors)
			{
				if (accessor.Semantics == 4 && (nonPublic || accessor.Method.IsPublic))
				{
					list.Add(accessor.Method);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00023744 File Offset: 0x00021944
		public override MethodInfo[] __GetMethods()
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (EventBuilder.Accessor accessor in this.accessors)
			{
				list.Add(accessor.Method);
			}
			return list.ToArray();
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000237A8 File Offset: 0x000219A8
		public override Type DeclaringType
		{
			get
			{
				return this.typeBuilder;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000237B0 File Offset: 0x000219B0
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000237B8 File Offset: 0x000219B8
		public override Module Module
		{
			get
			{
				return this.typeBuilder.ModuleBuilder;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000237C5 File Offset: 0x000219C5
		public EventToken GetEventToken()
		{
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = this.typeBuilder.ModuleBuilder.AllocPseudoToken();
			}
			return new EventToken(this.lazyPseudoToken);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000237F0 File Offset: 0x000219F0
		public override Type EventHandlerType
		{
			get
			{
				return this.typeBuilder.ModuleBuilder.ResolveType(this.eventtype);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00023808 File Offset: 0x00021A08
		internal void Bake()
		{
			EventTable.Record newRecord = default(EventTable.Record);
			newRecord.EventFlags = (short)this.attributes;
			newRecord.Name = this.typeBuilder.ModuleBuilder.Strings.Add(this.name);
			newRecord.EventType = this.eventtype;
			int num = 335544320 | this.typeBuilder.ModuleBuilder.Event.AddRecord(newRecord);
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = num;
			}
			else
			{
				this.typeBuilder.ModuleBuilder.RegisterTokenFixup(this.lazyPseudoToken, num);
			}
			foreach (EventBuilder.Accessor accessor in this.accessors)
			{
				this.AddMethodSemantics(accessor.Semantics, accessor.Method.MetadataToken, num);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000238F8 File Offset: 0x00021AF8
		private void AddMethodSemantics(short semantics, int methodToken, int propertyToken)
		{
			MethodSemanticsTable.Record newRecord = default(MethodSemanticsTable.Record);
			newRecord.Semantics = semantics;
			newRecord.Method = methodToken;
			newRecord.Association = propertyToken;
			this.typeBuilder.ModuleBuilder.MethodSemantics.AddRecord(newRecord);
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002393C File Offset: 0x00021B3C
		internal override bool IsPublic
		{
			get
			{
				using (List<EventBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Method.IsPublic)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002399C File Offset: 0x00021B9C
		internal override bool IsNonPrivate
		{
			get
			{
				using (List<EventBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if ((enumerator.Current.Method.Attributes & MethodAttributes.MemberAccessMask) > MethodAttributes.Private)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00023A00 File Offset: 0x00021C00
		internal override bool IsStatic
		{
			get
			{
				using (List<EventBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Method.IsStatic)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00023A60 File Offset: 0x00021C60
		internal override bool IsBaked
		{
			get
			{
				return this.typeBuilder.IsBaked;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00023A6D File Offset: 0x00021C6D
		internal override int GetCurrentToken()
		{
			if (this.typeBuilder.ModuleBuilder.IsSaved && ModuleBuilder.IsPseudoToken(this.lazyPseudoToken))
			{
				return this.typeBuilder.ModuleBuilder.ResolvePseudoToken(this.lazyPseudoToken);
			}
			return this.lazyPseudoToken;
		}

		// Token: 0x04000483 RID: 1155
		private readonly TypeBuilder typeBuilder;

		// Token: 0x04000484 RID: 1156
		private readonly string name;

		// Token: 0x04000485 RID: 1157
		private EventAttributes attributes;

		// Token: 0x04000486 RID: 1158
		private readonly int eventtype;

		// Token: 0x04000487 RID: 1159
		private MethodBuilder addOnMethod;

		// Token: 0x04000488 RID: 1160
		private MethodBuilder removeOnMethod;

		// Token: 0x04000489 RID: 1161
		private MethodBuilder fireMethod;

		// Token: 0x0400048A RID: 1162
		private readonly List<EventBuilder.Accessor> accessors = new List<EventBuilder.Accessor>();

		// Token: 0x0400048B RID: 1163
		private int lazyPseudoToken;

		// Token: 0x02000367 RID: 871
		private struct Accessor
		{
			// Token: 0x04000F0E RID: 3854
			internal short Semantics;

			// Token: 0x04000F0F RID: 3855
			internal MethodBuilder Method;
		}
	}
}
