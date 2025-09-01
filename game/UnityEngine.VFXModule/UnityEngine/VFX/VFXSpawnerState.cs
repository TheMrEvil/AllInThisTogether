using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000014 RID: 20
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXSpawnerState : IDisposable
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000027E4 File Offset: 0x000009E4
		public VFXSpawnerState() : this(VFXSpawnerState.Internal_Create(), true)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000027F4 File Offset: 0x000009F4
		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
		}

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000082 RID: 130 RVA: 0x0000280C File Offset: 0x00000A0C
		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			VFXSpawnerState vfxspawnerState = new VFXSpawnerState(IntPtr.Zero, false);
			vfxspawnerState.PrepareWrapper();
			return vfxspawnerState;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002834 File Offset: 0x00000A34
		private void PrepareWrapper()
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute != null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Unexpected calling twice prepare wrapper");
			}
			this.m_WrapEventAttribute = VFXEventAttribute.CreateEventAttributeWrapper();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000287C File Offset: 0x00000A7C
		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptrToSpawnerState, IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			bool flag = this.m_WrapEventAttribute == null;
			if (flag)
			{
				throw new Exception("VFXSpawnerState : Missing PrepareWrapper");
			}
			this.m_Ptr = ptrToSpawnerState;
			this.m_WrapEventAttribute.SetWrapValue(ptrToEventAttribute);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000028CC File Offset: 0x00000ACC
		internal IntPtr GetPtr()
		{
			return this.m_Ptr;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000028E4 File Offset: 0x00000AE4
		private void Release()
		{
			bool flag = this.m_Ptr != IntPtr.Zero && this.m_Owner;
			if (flag)
			{
				VFXSpawnerState.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_WrapEventAttribute = null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002934 File Offset: 0x00000B34
		~VFXSpawnerState()
		{
			this.Release();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002964 File Offset: 0x00000B64
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000089 RID: 137
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002993 File Offset: 0x00000B93
		public bool playing
		{
			get
			{
				return this.loopState == VFXSpawnerLoopState.Looping;
			}
			set
			{
				this.loopState = (value ? VFXSpawnerLoopState.Looping : VFXSpawnerLoopState.Finished);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600008C RID: 140
		public extern bool newLoop { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		public extern VFXSpawnerLoopState loopState { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		public extern float spawnCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		public extern float deltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		public extern float totalTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		public extern float delayBeforeLoop { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		public extern float loopDuration { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		public extern float delayAfterLoop { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		public extern int loopIndex { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009D RID: 157
		// (set) Token: 0x0600009E RID: 158
		public extern int loopCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern VFXEventAttribute Internal_GetVFXEventAttribute();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000029A4 File Offset: 0x00000BA4
		public VFXEventAttribute vfxEventAttribute
		{
			get
			{
				bool flag = !this.m_Owner && this.m_WrapEventAttribute != null;
				VFXEventAttribute result;
				if (flag)
				{
					result = this.m_WrapEventAttribute;
				}
				else
				{
					result = this.Internal_GetVFXEventAttribute();
				}
				return result;
			}
		}

		// Token: 0x040000F1 RID: 241
		private IntPtr m_Ptr;

		// Token: 0x040000F2 RID: 242
		private bool m_Owner;

		// Token: 0x040000F3 RID: 243
		private VFXEventAttribute m_WrapEventAttribute;
	}
}
