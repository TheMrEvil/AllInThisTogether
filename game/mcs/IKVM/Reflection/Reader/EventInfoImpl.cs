using System;

namespace IKVM.Reflection.Reader
{
	// Token: 0x02000093 RID: 147
	internal sealed class EventInfoImpl : EventInfo
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x000192BB File Offset: 0x000174BB
		internal EventInfoImpl(ModuleReader module, Type declaringType, int index)
		{
			this.module = module;
			this.declaringType = declaringType;
			this.index = index;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000192D8 File Offset: 0x000174D8
		public override bool Equals(object obj)
		{
			EventInfoImpl eventInfoImpl = obj as EventInfoImpl;
			return eventInfoImpl != null && eventInfoImpl.declaringType == this.declaringType && eventInfoImpl.index == this.index;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00019318 File Offset: 0x00017518
		public override int GetHashCode()
		{
			return this.declaringType.GetHashCode() * 123 + this.index;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001932F File Offset: 0x0001752F
		public override EventAttributes Attributes
		{
			get
			{
				return (EventAttributes)this.module.Event.records[this.index].EventFlags;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00019351 File Offset: 0x00017551
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethod(this.module, this.MetadataToken, nonPublic, 8);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00019371 File Offset: 0x00017571
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethod(this.module, this.MetadataToken, nonPublic, 32);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00019392 File Offset: 0x00017592
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethod(this.module, this.MetadataToken, nonPublic, 16);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000193B3 File Offset: 0x000175B3
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			return this.module.MethodSemantics.GetMethods(this.module, this.MetadataToken, nonPublic, 4);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000193D3 File Offset: 0x000175D3
		public override MethodInfo[] __GetMethods()
		{
			return this.module.MethodSemantics.GetMethods(this.module, this.MetadataToken, true, -1);
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000193F3 File Offset: 0x000175F3
		public override Type EventHandlerType
		{
			get
			{
				return this.module.ResolveType(this.module.Event.records[this.index].EventType, this.declaringType);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00019426 File Offset: 0x00017626
		public override string Name
		{
			get
			{
				return this.module.GetString(this.module.Event.records[this.index].Name);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00019453 File Offset: 0x00017653
		public override Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0001945B File Offset: 0x0001765B
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00019463 File Offset: 0x00017663
		public override int MetadataToken
		{
			get
			{
				return (20 << 24) + this.index + 1;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00019473 File Offset: 0x00017673
		internal override bool IsPublic
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isPublic;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00019489 File Offset: 0x00017689
		internal override bool IsNonPrivate
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isNonPrivate;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001949F File Offset: 0x0001769F
		internal override bool IsStatic
		{
			get
			{
				if (!this.flagsCached)
				{
					this.ComputeFlags();
				}
				return this.isStatic;
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000194B5 File Offset: 0x000176B5
		private void ComputeFlags()
		{
			this.module.MethodSemantics.ComputeFlags(this.module, this.MetadataToken, out this.isPublic, out this.isNonPrivate, out this.isStatic);
			this.flagsCached = true;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00010856 File Offset: 0x0000EA56
		internal override int GetCurrentToken()
		{
			return this.MetadataToken;
		}

		// Token: 0x04000307 RID: 775
		private readonly ModuleReader module;

		// Token: 0x04000308 RID: 776
		private readonly Type declaringType;

		// Token: 0x04000309 RID: 777
		private readonly int index;

		// Token: 0x0400030A RID: 778
		private bool isPublic;

		// Token: 0x0400030B RID: 779
		private bool isNonPrivate;

		// Token: 0x0400030C RID: 780
		private bool isStatic;

		// Token: 0x0400030D RID: 781
		private bool flagsCached;
	}
}
