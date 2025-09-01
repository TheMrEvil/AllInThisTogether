using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200046A RID: 1130
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h")]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		// Token: 0x06002802 RID: 10242 RVA: 0x00042AEC File Offset: 0x00040CEC
		public static MaterialEffectPlayable Create(PlayableGraph graph, Material material, int pass = -1)
		{
			PlayableHandle handle = MaterialEffectPlayable.CreateHandle(graph, material, pass);
			return new MaterialEffectPlayable(handle);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x00042B10 File Offset: 0x00040D10
		private static PlayableHandle CreateHandle(PlayableGraph graph, Material material, int pass)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !MaterialEffectPlayable.InternalCreateMaterialEffectPlayable(ref graph, material, pass, ref @null);
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

		// Token: 0x06002804 RID: 10244 RVA: 0x00042B44 File Offset: 0x00040D44
		internal MaterialEffectPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<MaterialEffectPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an MaterialEffectPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x00042B80 File Offset: 0x00040D80
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x00042B98 File Offset: 0x00040D98
		public static implicit operator Playable(MaterialEffectPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00042BB8 File Offset: 0x00040DB8
		public static explicit operator MaterialEffectPlayable(Playable playable)
		{
			return new MaterialEffectPlayable(playable.GetHandle());
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x00042BD8 File Offset: 0x00040DD8
		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x00042BFC File Offset: 0x00040DFC
		public Material GetMaterial()
		{
			return MaterialEffectPlayable.GetMaterialInternal(ref this.m_Handle);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x00042C19 File Offset: 0x00040E19
		public void SetMaterial(Material value)
		{
			MaterialEffectPlayable.SetMaterialInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00042C2C File Offset: 0x00040E2C
		public int GetPass()
		{
			return MaterialEffectPlayable.GetPassInternal(ref this.m_Handle);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x00042C49 File Offset: 0x00040E49
		public void SetPass(int value)
		{
			MaterialEffectPlayable.SetPassInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600280D RID: 10253
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Material GetMaterialInternal(ref PlayableHandle hdl);

		// Token: 0x0600280E RID: 10254
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialInternal(ref PlayableHandle hdl, Material material);

		// Token: 0x0600280F RID: 10255
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPassInternal(ref PlayableHandle hdl);

		// Token: 0x06002810 RID: 10256
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPassInternal(ref PlayableHandle hdl, int pass);

		// Token: 0x06002811 RID: 10257
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateMaterialEffectPlayable(ref PlayableGraph graph, Material material, int pass, ref PlayableHandle handle);

		// Token: 0x06002812 RID: 10258
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x04000EC7 RID: 3783
		private PlayableHandle m_Handle;
	}
}
