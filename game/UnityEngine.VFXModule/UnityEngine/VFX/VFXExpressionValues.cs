using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000F RID: 15
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class VFXExpressionValues
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002506 File Offset: 0x00000706
		private VFXExpressionValues()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002510 File Offset: 0x00000710
		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			return new VFXExpressionValues
			{
				m_Ptr = ptr
			};
		}

		// Token: 0x0600004C RID: 76
		[NativeThrows]
		[NativeName("GetValueFromScript<bool>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(int nameID);

		// Token: 0x0600004D RID: 77
		[NativeName("GetValueFromScript<int>")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(int nameID);

		// Token: 0x0600004E RID: 78
		[NativeName("GetValueFromScript<UInt32>")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetUInt(int nameID);

		// Token: 0x0600004F RID: 79
		[NativeName("GetValueFromScript<float>")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000050 RID: 80 RVA: 0x00002530 File Offset: 0x00000730
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			Vector2 result;
			this.GetVector2_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002548 File Offset: 0x00000748
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			Vector3 result;
			this.GetVector3_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002560 File Offset: 0x00000760
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			Vector4 result;
			this.GetVector4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002578 File Offset: 0x00000778
		[NativeThrows]
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			Matrix4x4 result;
			this.GetMatrix4x4_Injected(nameID, out result);
			return result;
		}

		// Token: 0x06000054 RID: 84
		[NativeName("GetValueFromScript<Texture*>")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x06000055 RID: 85
		[NativeThrows]
		[NativeName("GetValueFromScript<Mesh*>")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Mesh GetMesh(int nameID);

		// Token: 0x06000056 RID: 86 RVA: 0x00002590 File Offset: 0x00000790
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurveFromScript(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x06000057 RID: 87
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetAnimationCurveFromScript(int nameID, AnimationCurve curve);

		// Token: 0x06000058 RID: 88 RVA: 0x000025B4 File Offset: 0x000007B4
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradientFromScript(nameID, gradient);
			return gradient;
		}

		// Token: 0x06000059 RID: 89
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_GetGradientFromScript(int nameID, Gradient gradient);

		// Token: 0x0600005A RID: 90 RVA: 0x000025D8 File Offset: 0x000007D8
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000025F8 File Offset: 0x000007F8
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002618 File Offset: 0x00000818
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002638 File Offset: 0x00000838
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002658 File Offset: 0x00000858
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002678 File Offset: 0x00000878
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002698 File Offset: 0x00000898
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000026B8 File Offset: 0x000008B8
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000026D8 File Offset: 0x000008D8
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000026F8 File Offset: 0x000008F8
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002718 File Offset: 0x00000918
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002738 File Offset: 0x00000938
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		// Token: 0x040000E7 RID: 231
		internal IntPtr m_Ptr;
	}
}
