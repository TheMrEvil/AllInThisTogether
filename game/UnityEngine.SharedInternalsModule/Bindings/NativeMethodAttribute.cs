using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class NativeMethodAttribute : Attribute, IBindingsNameProviderAttribute, IBindingsAttribute, IBindingsIsThreadSafeProviderAttribute, IBindingsIsFreeFunctionProviderAttribute, IBindingsThrowsProviderAttribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000022B8 File Offset: 0x000004B8
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000022C0 File Offset: 0x000004C0
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000022C9 File Offset: 0x000004C9
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000022D1 File Offset: 0x000004D1
		public bool IsThreadSafe
		{
			[CompilerGenerated]
			get
			{
				return this.<IsThreadSafe>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsThreadSafe>k__BackingField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000022DA File Offset: 0x000004DA
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000022E2 File Offset: 0x000004E2
		public bool IsFreeFunction
		{
			[CompilerGenerated]
			get
			{
				return this.<IsFreeFunction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsFreeFunction>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000022EB File Offset: 0x000004EB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000022F3 File Offset: 0x000004F3
		public bool ThrowsException
		{
			[CompilerGenerated]
			get
			{
				return this.<ThrowsException>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ThrowsException>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002304 File Offset: 0x00000504
		public bool HasExplicitThis
		{
			[CompilerGenerated]
			get
			{
				return this.<HasExplicitThis>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasExplicitThis>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000230D File Offset: 0x0000050D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002315 File Offset: 0x00000515
		public bool WritableSelf
		{
			[CompilerGenerated]
			get
			{
				return this.<WritableSelf>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WritableSelf>k__BackingField = value;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002078 File Offset: 0x00000278
		public NativeMethodAttribute()
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002320 File Offset: 0x00000520
		public NativeMethodAttribute(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name == "";
			if (flag2)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			this.Name = name;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000236F File Offset: 0x0000056F
		public NativeMethodAttribute(string name, bool isFreeFunction) : this(name)
		{
			this.IsFreeFunction = isFreeFunction;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002382 File Offset: 0x00000582
		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe) : this(name, isFreeFunction)
		{
			this.IsThreadSafe = isThreadSafe;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002396 File Offset: 0x00000596
		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe, bool throws) : this(name, isFreeFunction, isThreadSafe)
		{
			this.ThrowsException = throws;
		}

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Name>k__BackingField;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <IsThreadSafe>k__BackingField;

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <IsFreeFunction>k__BackingField;

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <ThrowsException>k__BackingField;

		// Token: 0x04000010 RID: 16
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <HasExplicitThis>k__BackingField;

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <WritableSelf>k__BackingField;
	}
}
