using System;
using System.Runtime.CompilerServices;

namespace Unity.Burst
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method)]
	public class BurstCompileAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public FloatMode FloatMode
		{
			[CompilerGenerated]
			get
			{
				return this.<FloatMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FloatMode>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002079 File Offset: 0x00000279
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		public FloatPrecision FloatPrecision
		{
			[CompilerGenerated]
			get
			{
				return this.<FloatPrecision>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FloatPrecision>k__BackingField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208A File Offset: 0x0000028A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020A6 File Offset: 0x000002A6
		public bool CompileSynchronously
		{
			get
			{
				return this._compileSynchronously != null && this._compileSynchronously.Value;
			}
			set
			{
				this._compileSynchronously = new bool?(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B4 File Offset: 0x000002B4
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool Debug
		{
			get
			{
				return this._debug != null && this._debug.Value;
			}
			set
			{
				this._debug = new bool?(value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020DE File Offset: 0x000002DE
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020FA File Offset: 0x000002FA
		public bool DisableSafetyChecks
		{
			get
			{
				return this._disableSafetyChecks != null && this._disableSafetyChecks.Value;
			}
			set
			{
				this._disableSafetyChecks = new bool?(value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002108 File Offset: 0x00000308
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002124 File Offset: 0x00000324
		public bool DisableDirectCall
		{
			get
			{
				return this._disableDirectCall != null && this._disableDirectCall.Value;
			}
			set
			{
				this._disableDirectCall = new bool?(value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002132 File Offset: 0x00000332
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000213A File Offset: 0x0000033A
		public OptimizeFor OptimizeFor
		{
			[CompilerGenerated]
			get
			{
				return this.<OptimizeFor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OptimizeFor>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002143 File Offset: 0x00000343
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000214B File Offset: 0x0000034B
		internal string[] Options
		{
			[CompilerGenerated]
			get
			{
				return this.<Options>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Options>k__BackingField = value;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002154 File Offset: 0x00000354
		public BurstCompileAttribute()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000215C File Offset: 0x0000035C
		public BurstCompileAttribute(FloatPrecision floatPrecision, FloatMode floatMode)
		{
			this.FloatMode = floatMode;
			this.FloatPrecision = floatPrecision;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002172 File Offset: 0x00000372
		internal BurstCompileAttribute(string[] options)
		{
			this.Options = options;
		}

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private FloatMode <FloatMode>k__BackingField;

		// Token: 0x04000012 RID: 18
		[CompilerGenerated]
		private FloatPrecision <FloatPrecision>k__BackingField;

		// Token: 0x04000013 RID: 19
		internal bool? _compileSynchronously;

		// Token: 0x04000014 RID: 20
		internal bool? _debug;

		// Token: 0x04000015 RID: 21
		internal bool? _disableSafetyChecks;

		// Token: 0x04000016 RID: 22
		internal bool? _disableDirectCall;

		// Token: 0x04000017 RID: 23
		[CompilerGenerated]
		private OptimizeFor <OptimizeFor>k__BackingField;

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private string[] <Options>k__BackingField;
	}
}
