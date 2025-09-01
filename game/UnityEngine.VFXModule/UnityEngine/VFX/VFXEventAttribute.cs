using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000E RID: 14
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXEventAttribute : IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
			this.m_VfxAsset = vfxAsset;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
		private VFXEventAttribute() : this(IntPtr.Zero, false, null)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000208C File Offset: 0x0000028C
		internal static VFXEventAttribute CreateEventAttributeWrapper()
		{
			return new VFXEventAttribute(IntPtr.Zero, false, null);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020AC File Offset: 0x000002AC
		internal void SetWrapValue(IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			this.m_Ptr = ptrToEventAttribute;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		public VFXEventAttribute(VFXEventAttribute original)
		{
			bool flag = original == null;
			if (flag)
			{
				throw new ArgumentNullException("VFXEventAttribute expect a non null attribute");
			}
			this.m_Ptr = VFXEventAttribute.Internal_Create();
			this.m_VfxAsset = original.m_VfxAsset;
			this.Internal_InitFromEventAttribute(original);
		}

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000009 RID: 9 RVA: 0x00002120 File Offset: 0x00000320
		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute vfxeventAttribute = new VFXEventAttribute(VFXEventAttribute.Internal_Create(), true, vfxAsset);
			vfxeventAttribute.Internal_InitFromAsset(vfxAsset);
			return vfxeventAttribute;
		}

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromAsset(VisualEffectAsset vfxAsset);

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromEventAttribute(VFXEventAttribute vfxEventAttribute);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		internal VisualEffectAsset vfxAsset
		{
			get
			{
				return this.m_VfxAsset;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002160 File Offset: 0x00000360
		private void Release()
		{
			bool flag = this.m_Owner && this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VFXEventAttribute.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_VfxAsset = null;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
		~VFXEventAttribute()
		{
			this.Release();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021E0 File Offset: 0x000003E0
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000010 RID: 16
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000011 RID: 17
		[NativeName("HasValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasBool(int nameID);

		// Token: 0x06000012 RID: 18
		[NativeName("HasValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasInt(int nameID);

		// Token: 0x06000013 RID: 19
		[NativeName("HasValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasUint(int nameID);

		// Token: 0x06000014 RID: 20
		[NativeName("HasValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasFloat(int nameID);

		// Token: 0x06000015 RID: 21
		[NativeName("HasValueFromScript<Vector2f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector2(int nameID);

		// Token: 0x06000016 RID: 22
		[NativeName("HasValueFromScript<Vector3f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector3(int nameID);

		// Token: 0x06000017 RID: 23
		[NativeName("HasValueFromScript<Vector4f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasVector4(int nameID);

		// Token: 0x06000018 RID: 24
		[NativeName("HasValueFromScript<Matrix4x4f>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasMatrix4x4(int nameID);

		// Token: 0x06000019 RID: 25
		[NativeName("SetValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBool(int nameID, bool b);

		// Token: 0x0600001A RID: 26
		[NativeName("SetValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int i);

		// Token: 0x0600001B RID: 27
		[NativeName("SetValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetUint(int nameID, uint i);

		// Token: 0x0600001C RID: 28
		[NativeName("SetValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float f);

		// Token: 0x0600001D RID: 29 RVA: 0x000021F1 File Offset: 0x000003F1
		[NativeName("SetValueFromScript<Vector2f>")]
		public void SetVector2(int nameID, Vector2 v)
		{
			this.SetVector2_Injected(nameID, ref v);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021FC File Offset: 0x000003FC
		[NativeName("SetValueFromScript<Vector3f>")]
		public void SetVector3(int nameID, Vector3 v)
		{
			this.SetVector3_Injected(nameID, ref v);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002207 File Offset: 0x00000407
		[NativeName("SetValueFromScript<Vector4f>")]
		public void SetVector4(int nameID, Vector4 v)
		{
			this.SetVector4_Injected(nameID, ref v);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002212 File Offset: 0x00000412
		[NativeName("SetValueFromScript<Matrix4x4f>")]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			this.SetMatrix4x4_Injected(nameID, ref v);
		}

		// Token: 0x06000021 RID: 33
		[NativeName("GetValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		// Token: 0x06000022 RID: 34
		[NativeName("GetValueFromScript<int>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		// Token: 0x06000023 RID: 35
		[NativeName("GetValueFromScript<UInt32>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUint(int nameID);

		// Token: 0x06000024 RID: 36
		[NativeName("GetValueFromScript<float>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000025 RID: 37 RVA: 0x00002220 File Offset: 0x00000420
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002238 File Offset: 0x00000438
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002250 File Offset: 0x00000450
		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002268 File Offset: 0x00000468
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002280 File Offset: 0x00000480
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022A0 File Offset: 0x000004A0
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022C0 File Offset: 0x000004C0
		public bool HasUint(string name)
		{
			return this.HasUint(Shader.PropertyToID(name));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000022E0 File Offset: 0x000004E0
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002300 File Offset: 0x00000500
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002320 File Offset: 0x00000520
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002340 File Offset: 0x00000540
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002360 File Offset: 0x00000560
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000237E File Offset: 0x0000057E
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000238F File Offset: 0x0000058F
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000023A0 File Offset: 0x000005A0
		public void SetUint(string name, uint i)
		{
			this.SetUint(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000023B1 File Offset: 0x000005B1
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023C2 File Offset: 0x000005C2
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000023D3 File Offset: 0x000005D3
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000023E4 File Offset: 0x000005E4
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000023F5 File Offset: 0x000005F5
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002408 File Offset: 0x00000608
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002428 File Offset: 0x00000628
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002448 File Offset: 0x00000648
		public uint GetUint(string name)
		{
			return this.GetUint(Shader.PropertyToID(name));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002468 File Offset: 0x00000668
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002488 File Offset: 0x00000688
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024A8 File Offset: 0x000006A8
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000024C8 File Offset: 0x000006C8
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000024E8 File Offset: 0x000006E8
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyValuesFrom([NotNull("ArgumentNullException")] VFXEventAttribute eventAttibute);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x040000E4 RID: 228
		private IntPtr m_Ptr;

		// Token: 0x040000E5 RID: 229
		private bool m_Owner;

		// Token: 0x040000E6 RID: 230
		private VisualEffectAsset m_VfxAsset;
	}
}
