using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTextureResolver.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Resolver : IDisposable
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002064 File Offset: 0x00000264
		public Resolver()
		{
			bool flag = !System.enabled;
			if (flag)
			{
				throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
			}
			this.m_Ptr = Resolver.InitNative();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020AC File Offset: 0x000002AC
		~Resolver()
		{
			this.Dispose(false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020E0 File Offset: 0x000002E0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020F4 File Offset: 0x000002F4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Flush_Internal();
				Resolver.ReleaseNative(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitNative();

		// Token: 0x06000017 RID: 23
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseNative(IntPtr ptr);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Flush_Internal();

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init_Internal(int width, int height);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000213E File Offset: 0x0000033E
		public int CurrentWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentWidth>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentWidth>k__BackingField = value;
			}
		} = 0;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000214F File Offset: 0x0000034F
		public int CurrentHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentHeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CurrentHeight>k__BackingField = value;
			}
		} = 0;

		// Token: 0x0600001E RID: 30 RVA: 0x00002158 File Offset: 0x00000358
		public void UpdateSize(int width, int height)
		{
			bool flag = this.CurrentWidth != width || this.CurrentHeight != height;
			if (flag)
			{
				bool flag2 = width <= 0 || height <= 0;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Zero sized dimensions are invalid (width: {0}, height: {1}.", width, height));
				}
				this.CurrentWidth = width;
				this.CurrentHeight = height;
				this.Flush_Internal();
				this.Init_Internal(this.CurrentWidth, this.CurrentHeight);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt)
		{
			this.Process(cmd, rt, 0, this.CurrentWidth, 0, this.CurrentHeight, 0, 0);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002208 File Offset: 0x00000408
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt, int x, int width, int y, int height, int mip, int slice)
		{
			bool flag = cmd == null;
			if (flag)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ProcessVTFeedback(rt, this.m_Ptr, slice, x, width, y, height, mip);
		}

		// Token: 0x04000009 RID: 9
		internal IntPtr m_Ptr;

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <CurrentWidth>k__BackingField;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <CurrentHeight>k__BackingField;
	}
}
