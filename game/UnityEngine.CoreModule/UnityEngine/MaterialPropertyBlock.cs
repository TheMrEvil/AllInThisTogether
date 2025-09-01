using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000146 RID: 326
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Math/SphericalHarmonicsL2.h")]
	[NativeHeader("Runtime/Shaders/ShaderPropertySheet.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000F6D7 File Offset: 0x0000D8D7
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(int nameID, float value)
		{
			this.SetFloat(nameID, value);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000F705 File Offset: 0x0000D905
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(int nameID, Vector4 value)
		{
			this.SetVector(nameID, value);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000F711 File Offset: 0x0000D911
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000F722 File Offset: 0x0000D922
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(int nameID, Color value)
		{
			this.SetColor(nameID, value);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000F72E File Offset: 0x0000D92E
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000F73F File Offset: 0x0000D93F
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrix(nameID, value);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000F74B File Offset: 0x0000D94B
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000F75C File Offset: 0x0000D95C
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(int nameID, Texture value)
		{
			this.SetTexture(nameID, value);
		}

		// Token: 0x06000B03 RID: 2819
		[NativeName("GetIntFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntImpl(int name);

		// Token: 0x06000B04 RID: 2820
		[ThreadSafe]
		[NativeName("GetFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatImpl(int name);

		// Token: 0x06000B05 RID: 2821 RVA: 0x0000F768 File Offset: 0x0000D968
		[NativeName("GetVectorFromScript")]
		[ThreadSafe]
		private Vector4 GetVectorImpl(int name)
		{
			Vector4 result;
			this.GetVectorImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000F780 File Offset: 0x0000D980
		[ThreadSafe]
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			Color result;
			this.GetColorImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000F798 File Offset: 0x0000D998
		[NativeName("GetMatrixFromScript")]
		[ThreadSafe]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 result;
			this.GetMatrixImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000B08 RID: 2824
		[NativeName("GetTextureFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture GetTextureImpl(int name);

		// Token: 0x06000B09 RID: 2825
		[NativeName("HasPropertyFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasPropertyImpl(int name);

		// Token: 0x06000B0A RID: 2826
		[NativeName("HasFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasFloatImpl(int name);

		// Token: 0x06000B0B RID: 2827
		[NativeName("HasIntegerFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasIntImpl(int name);

		// Token: 0x06000B0C RID: 2828
		[NativeName("HasTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasTextureImpl(int name);

		// Token: 0x06000B0D RID: 2829
		[NativeName("HasMatrixFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasMatrixImpl(int name);

		// Token: 0x06000B0E RID: 2830
		[NativeName("HasVectorFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasVectorImpl(int name);

		// Token: 0x06000B0F RID: 2831
		[NativeName("HasBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasBufferImpl(int name);

		// Token: 0x06000B10 RID: 2832
		[NativeName("HasConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasConstantBufferImpl(int name);

		// Token: 0x06000B11 RID: 2833
		[NativeName("SetIntFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntImpl(int name, int value);

		// Token: 0x06000B12 RID: 2834
		[NativeName("SetFloatFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatImpl(int name, float value);

		// Token: 0x06000B13 RID: 2835 RVA: 0x0000F7AF File Offset: 0x0000D9AF
		[ThreadSafe]
		[NativeName("SetVectorFromScript")]
		private void SetVectorImpl(int name, Vector4 value)
		{
			this.SetVectorImpl_Injected(name, ref value);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000F7BA File Offset: 0x0000D9BA
		[ThreadSafe]
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0000F7C5 File Offset: 0x0000D9C5
		[NativeName("SetMatrixFromScript")]
		[ThreadSafe]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000B16 RID: 2838
		[ThreadSafe]
		[NativeName("SetTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureImpl(int name, [NotNull("ArgumentNullException")] Texture value);

		// Token: 0x06000B17 RID: 2839
		[ThreadSafe]
		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureImpl(int name, [NotNull("ArgumentNullException")] RenderTexture value, RenderTextureSubElement element);

		// Token: 0x06000B18 RID: 2840
		[NativeName("SetBufferFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		// Token: 0x06000B19 RID: 2841
		[ThreadSafe]
		[NativeName("SetBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x06000B1A RID: 2842
		[ThreadSafe]
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x06000B1B RID: 2843
		[ThreadSafe]
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x06000B1C RID: 2844
		[NativeName("SetFloatArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x06000B1D RID: 2845
		[NativeName("SetVectorArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x06000B1E RID: 2846
		[NativeName("SetMatrixArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x06000B1F RID: 2847
		[NativeName("GetFloatArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[] GetFloatArrayImpl(int name);

		// Token: 0x06000B20 RID: 2848
		[NativeName("GetVectorArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		// Token: 0x06000B21 RID: 2849
		[NativeName("GetMatrixArrayFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		// Token: 0x06000B22 RID: 2850
		[NativeName("GetFloatArrayCountFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFloatArrayCountImpl(int name);

		// Token: 0x06000B23 RID: 2851
		[NativeName("GetVectorArrayCountFromScript")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVectorArrayCountImpl(int name);

		// Token: 0x06000B24 RID: 2852
		[ThreadSafe]
		[NativeName("GetMatrixArrayCountFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMatrixArrayCountImpl(int name);

		// Token: 0x06000B25 RID: 2853
		[ThreadSafe]
		[NativeName("ExtractFloatArrayFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x06000B26 RID: 2854
		[ThreadSafe]
		[NativeName("ExtractVectorArrayFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x06000B27 RID: 2855
		[ThreadSafe]
		[NativeName("ExtractMatrixArrayFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x06000B28 RID: 2856
		[ThreadSafe]
		[FreeFunction("ConvertAndCopySHCoefficientArraysToPropertySheetFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CopySHCoefficientArraysFrom(MaterialPropertyBlock properties, SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count);

		// Token: 0x06000B29 RID: 2857
		[ThreadSafe]
		[FreeFunction("CopyProbeOcclusionArrayToPropertySheetFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CopyProbeOcclusionArrayFrom(MaterialPropertyBlock properties, Vector4[] occlusionProbes, int sourceStart, int destStart, int count);

		// Token: 0x06000B2A RID: 2858
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateImpl();

		// Token: 0x06000B2B RID: 2859
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Destroy", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImpl(IntPtr mpb);

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B2C RID: 2860
		public extern bool isEmpty { [NativeName("IsEmpty")] [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000B2D RID: 2861
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Clear(bool keepMemory);

		// Token: 0x06000B2E RID: 2862 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0000F830 File Offset: 0x0000DA30
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000F884 File Offset: 0x0000DA84
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, (float[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0000F930 File Offset: 0x0000DB30
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, (Vector4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0000F988 File Offset: 0x0000DB88
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, (Matrix4x4[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		public MaterialPropertyBlock()
		{
			this.m_Ptr = MaterialPropertyBlock.CreateImpl();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		~MaterialPropertyBlock()
		{
			this.Dispose();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0000FA24 File Offset: 0x0000DC24
		private void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MaterialPropertyBlock.DestroyImpl(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0000FA66 File Offset: 0x0000DC66
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000FA85 File Offset: 0x0000DC85
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0000FA96 File Offset: 0x0000DC96
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0000FAA2 File Offset: 0x0000DCA2
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0000FAB3 File Offset: 0x0000DCB3
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000FABF File Offset: 0x0000DCBF
		public void SetVector(string name, Vector4 value)
		{
			this.SetVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetVectorImpl(nameID, value);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0000FAED File Offset: 0x0000DCED
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0000FAF9 File Offset: 0x0000DCF9
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0000FB0A File Offset: 0x0000DD0A
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0000FB16 File Offset: 0x0000DD16
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0000FB27 File Offset: 0x0000DD27
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0000FB33 File Offset: 0x0000DD33
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0000FB61 File Offset: 0x0000DD61
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0000FB7F File Offset: 0x0000DD7F
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0000FB8C File Offset: 0x0000DD8C
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0000FBAF File Offset: 0x0000DDAF
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0000FBD2 File Offset: 0x0000DDD2
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0000FBEE File Offset: 0x0000DDEE
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0000FC05 File Offset: 0x0000DE05
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0000FC19 File Offset: 0x0000DE19
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0000FC5B File Offset: 0x0000DE5B
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0000FC6F File Offset: 0x0000DE6F
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0000FC7E File Offset: 0x0000DE7E
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0000FC9A File Offset: 0x0000DE9A
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0000FCB1 File Offset: 0x0000DEB1
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0000FCC5 File Offset: 0x0000DEC5
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public bool HasProperty(string name)
		{
			return this.HasPropertyImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public bool HasProperty(int nameID)
		{
			return this.HasPropertyImpl(nameID);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0000FE00 File Offset: 0x0000E000
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0000FE20 File Offset: 0x0000E020
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0000FE5C File Offset: 0x0000E05C
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0000FE78 File Offset: 0x0000E078
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0000FE98 File Offset: 0x0000E098
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0000FF10 File Offset: 0x0000E110
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0000FF2C File Offset: 0x0000E12C
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0000FF68 File Offset: 0x0000E168
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0000FF88 File Offset: 0x0000E188
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public Vector4 GetVector(string name)
		{
			return this.GetVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00010000 File Offset: 0x0000E200
		public Vector4 GetVector(int nameID)
		{
			return this.GetVectorImpl(nameID);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001001C File Offset: 0x0000E21C
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001003C File Offset: 0x0000E23C
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00010058 File Offset: 0x0000E258
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00010078 File Offset: 0x0000E278
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00010094 File Offset: 0x0000E294
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000100B4 File Offset: 0x0000E2B4
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000100D0 File Offset: 0x0000E2D0
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00010118 File Offset: 0x0000E318
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00010138 File Offset: 0x0000E338
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00010160 File Offset: 0x0000E360
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00010180 File Offset: 0x0000E380
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000101A5 File Offset: 0x0000E3A5
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000101B6 File Offset: 0x0000E3B6
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000101C2 File Offset: 0x0000E3C2
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000101D3 File Offset: 0x0000E3D3
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000101DF File Offset: 0x0000E3DF
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000101F0 File Offset: 0x0000E3F0
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000101FC File Offset: 0x0000E3FC
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), 0, 0, lightProbes.Count);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00010234 File Offset: 0x0000E434
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(lightProbes, 0, 0, lightProbes.Length);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00010262 File Offset: 0x0000E462
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes, int sourceStart, int destStart, int count)
		{
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromListT<SphericalHarmonicsL2>(lightProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00010278 File Offset: 0x0000E478
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = lightProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopySHCoefficientArraysFrom(this, lightProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00010308 File Offset: 0x0000E508
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), 0, 0, occlusionProbes.Count);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00010340 File Offset: 0x0000E540
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(occlusionProbes, 0, 0, occlusionProbes.Length);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0001036E File Offset: 0x0000E56E
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes, int sourceStart, int destStart, int count)
		{
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromListT<Vector4>(occlusionProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00010384 File Offset: 0x0000E584
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes, int sourceStart, int destStart, int count)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = occlusionProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopyProbeOcclusionArrayFrom(this, occlusionProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000B92 RID: 2962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000B93 RID: 2963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		// Token: 0x06000B94 RID: 2964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000B95 RID: 2965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorImpl_Injected(int name, ref Vector4 value);

		// Token: 0x06000B96 RID: 2966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		// Token: 0x06000B97 RID: 2967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x04000416 RID: 1046
		internal IntPtr m_Ptr;
	}
}
