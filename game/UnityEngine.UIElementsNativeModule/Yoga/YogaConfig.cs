using System;

namespace UnityEngine.Yoga
{
	// Token: 0x02000008 RID: 8
	internal class YogaConfig
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002084 File Offset: 0x00000284
		private YogaConfig(IntPtr ygConfig)
		{
			this._ygConfig = ygConfig;
			bool flag = this._ygConfig == IntPtr.Zero;
			if (flag)
			{
				throw new InvalidOperationException("Failed to allocate native memory");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020C0 File Offset: 0x000002C0
		public YogaConfig() : this(Native.YGConfigNew())
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override void Finalize()
		{
			try
			{
				bool flag = this.Handle != YogaConfig.Default.Handle;
				if (flag)
				{
					Native.YGConfigFree(this.Handle);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002120 File Offset: 0x00000320
		internal IntPtr Handle
		{
			get
			{
				return this._ygConfig;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002150 File Offset: 0x00000350
		public Logger Logger
		{
			get
			{
				return this._logger;
			}
			set
			{
				this._logger = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000215A File Offset: 0x0000035A
		public void SetExperimentalFeatureEnabled(YogaExperimentalFeature feature, bool enabled)
		{
			Native.YGConfigSetExperimentalFeatureEnabled(this._ygConfig, feature, enabled);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000216C File Offset: 0x0000036C
		public bool IsExperimentalFeatureEnabled(YogaExperimentalFeature feature)
		{
			return Native.YGConfigIsExperimentalFeatureEnabled(this._ygConfig, feature);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000021A9 File Offset: 0x000003A9
		public bool UseWebDefaults
		{
			get
			{
				return Native.YGConfigGetUseWebDefaults(this._ygConfig);
			}
			set
			{
				Native.YGConfigSetUseWebDefaults(this._ygConfig, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000021D9 File Offset: 0x000003D9
		public float PointScaleFactor
		{
			get
			{
				return Native.YGConfigGetPointScaleFactor(this._ygConfig);
			}
			set
			{
				Native.YGConfigSetPointScaleFactor(this._ygConfig, value);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021EC File Offset: 0x000003EC
		public static int GetInstanceCount()
		{
			return Native.YGConfigGetInstanceCount();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002203 File Offset: 0x00000403
		public static void SetDefaultLogger(Logger logger)
		{
			YogaConfig.Default.Logger = logger;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002212 File Offset: 0x00000412
		// Note: this type is marked as 'beforefieldinit'.
		static YogaConfig()
		{
		}

		// Token: 0x0400000A RID: 10
		internal static readonly YogaConfig Default = new YogaConfig(Native.YGConfigGetDefault());

		// Token: 0x0400000B RID: 11
		private IntPtr _ygConfig;

		// Token: 0x0400000C RID: 12
		private Logger _logger;
	}
}
