using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000469 RID: 1129
	[StaticAccessor("CameraPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Camera//Director/CameraPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/CameraPlayable.bindings.h")]
	[RequiredByNativeCode]
	public struct CameraPlayable : IPlayable, IEquatable<CameraPlayable>
	{
		// Token: 0x060027F5 RID: 10229 RVA: 0x000429B0 File Offset: 0x00040BB0
		public static CameraPlayable Create(PlayableGraph graph, Camera camera)
		{
			PlayableHandle handle = CameraPlayable.CreateHandle(graph, camera);
			return new CameraPlayable(handle);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000429D0 File Offset: 0x00040BD0
		private static PlayableHandle CreateHandle(PlayableGraph graph, Camera camera)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !CameraPlayable.InternalCreateCameraPlayable(ref graph, camera, ref @null);
			PlayableHandle result;
			if (flag)
			{
				result = PlayableHandle.Null;
			}
			else
			{
				result = @null;
			}
			return result;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00042A04 File Offset: 0x00040C04
		internal CameraPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<CameraPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an CameraPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00042A40 File Offset: 0x00040C40
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00042A58 File Offset: 0x00040C58
		public static implicit operator Playable(CameraPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00042A78 File Offset: 0x00040C78
		public static explicit operator CameraPlayable(Playable playable)
		{
			return new CameraPlayable(playable.GetHandle());
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00042A98 File Offset: 0x00040C98
		public bool Equals(CameraPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00042ABC File Offset: 0x00040CBC
		public Camera GetCamera()
		{
			return CameraPlayable.GetCameraInternal(ref this.m_Handle);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00042AD9 File Offset: 0x00040CD9
		public void SetCamera(Camera value)
		{
			CameraPlayable.SetCameraInternal(ref this.m_Handle, value);
		}

		// Token: 0x060027FE RID: 10238
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Camera GetCameraInternal(ref PlayableHandle hdl);

		// Token: 0x060027FF RID: 10239
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCameraInternal(ref PlayableHandle hdl, Camera camera);

		// Token: 0x06002800 RID: 10240
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateCameraPlayable(ref PlayableGraph graph, Camera camera, ref PlayableHandle handle);

		// Token: 0x06002801 RID: 10241
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000EC6 RID: 3782
		private PlayableHandle m_Handle;
	}
}
