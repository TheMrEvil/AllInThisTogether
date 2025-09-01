using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000239 RID: 569
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class ComputeShader : Object
	{
		// Token: 0x0600182F RID: 6191
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[RequiredByNativeCode]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindKernel(string name);

		// Token: 0x06001830 RID: 6192
		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasKernel(string name);

		// Token: 0x06001831 RID: 6193
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x06001832 RID: 6194
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x06001833 RID: 6195 RVA: 0x000276F7 File Offset: 0x000258F7
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00027702 File Offset: 0x00025902
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x06001835 RID: 6197
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x06001836 RID: 6198
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x06001837 RID: 6199
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x06001838 RID: 6200
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x06001839 RID: 6201
		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] Texture texture, int mipLevel);

		// Token: 0x0600183A RID: 6202
		[NativeMethod(Name = "ComputeShaderScripting::SetRenderTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTexture(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] RenderTexture texture, int mipLevel, RenderTextureSubElement element);

		// Token: 0x0600183B RID: 6203
		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID);

		// Token: 0x0600183C RID: 6204
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		// Token: 0x0600183D RID: 6205
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		// Token: 0x0600183E RID: 6206 RVA: 0x0002770D File Offset: 0x0002590D
		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0002771A File Offset: 0x0002591A
		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x06001840 RID: 6208
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		// Token: 0x06001841 RID: 6209
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		// Token: 0x06001842 RID: 6210
		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z);

		// Token: 0x06001843 RID: 6211
		[NativeName("DispatchComputeShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x06001844 RID: 6212
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DispatchIndirect(int kernelIndex, [NotNull("ArgumentNullException")] ComputeBuffer argsBuffer, uint argsOffset);

		// Token: 0x06001845 RID: 6213
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_DispatchIndirectGraphicsBuffer(int kernelIndex, [NotNull("ArgumentNullException")] GraphicsBuffer argsBuffer, uint argsOffset);

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00027728 File Offset: 0x00025928
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				LocalKeywordSpace result;
				this.get_keywordSpace_Injected(out result);
				return result;
			}
		}

		// Token: 0x06001847 RID: 6215
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06001848 RID: 6216
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06001849 RID: 6217
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x0600184A RID: 6218 RVA: 0x0002773E File Offset: 0x0002593E
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			this.EnableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00027748 File Offset: 0x00025948
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			this.DisableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00027752 File Offset: 0x00025952
		[FreeFunction("ComputeShaderScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword_Injected(ref keyword, value);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0002775D File Offset: 0x0002595D
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled_Injected(ref keyword);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00027767 File Offset: 0x00025967
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00027777 File Offset: 0x00025977
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00027787 File Offset: 0x00025987
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00027798 File Offset: 0x00025998
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x06001852 RID: 6226
		[FreeFunction("ComputeShaderScripting::IsSupported", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSupported(int kernelIndex);

		// Token: 0x06001853 RID: 6227
		[FreeFunction("ComputeShaderScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] GetShaderKeywords();

		// Token: 0x06001854 RID: 6228
		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x000277B8 File Offset: 0x000259B8
		// (set) Token: 0x06001856 RID: 6230 RVA: 0x000277D0 File Offset: 0x000259D0
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06001857 RID: 6231
		[FreeFunction("ComputeShaderScripting::GetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern LocalKeyword[] GetEnabledKeywords();

		// Token: 0x06001858 RID: 6232
		[FreeFunction("ComputeShaderScripting::SetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnabledKeywords(LocalKeyword[] keywords);

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000277DC File Offset: 0x000259DC
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x000277F4 File Offset: 0x000259F4
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0000E886 File Offset: 0x0000CA86
		private ComputeShader()
		{
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000277FF File Offset: 0x000259FF
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00027810 File Offset: 0x00025A10
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00027821 File Offset: 0x00025A21
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00027832 File Offset: 0x00025A32
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00027843 File Offset: 0x00025A43
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00027854 File Offset: 0x00025A54
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00027865 File Offset: 0x00025A65
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00027876 File Offset: 0x00025A76
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00027882 File Offset: 0x00025A82
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00027893 File Offset: 0x00025A93
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0002789F File Offset: 0x00025A9F
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000278B6 File Offset: 0x00025AB6
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000278C8 File Offset: 0x00025AC8
		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			this.SetTexture(kernelIndex, nameID, texture, 0);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000278D6 File Offset: 0x00025AD6
		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000278E9 File Offset: 0x00025AE9
		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000278FD File Offset: 0x00025AFD
		public void SetTexture(int kernelIndex, int nameID, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, nameID, texture, mipLevel, element);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0002790E File Offset: 0x00025B0E
		public void SetTexture(int kernelIndex, string name, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel, element);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00027924 File Offset: 0x00025B24
		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0002793B File Offset: 0x00025B3B
		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0002794D File Offset: 0x00025B4D
		public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0002795F File Offset: 0x00025B5F
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0002796E File Offset: 0x00025B6E
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00027982 File Offset: 0x00025B82
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00027991 File Offset: 0x00025B91
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000279A8 File Offset: 0x00025BA8
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000279F2 File Offset: 0x00025BF2
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00027A00 File Offset: 0x00025C00
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			this.Internal_DispatchIndirectGraphicsBuffer(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00027A4A File Offset: 0x00025C4A
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06001878 RID: 6264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x06001879 RID: 6265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);

		// Token: 0x0600187A RID: 6266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_keywordSpace_Injected(out LocalKeywordSpace ret);

		// Token: 0x0600187B RID: 6267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EnableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x0600187C RID: 6268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x0600187D RID: 6269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalKeyword_Injected(ref LocalKeyword keyword, bool value);

		// Token: 0x0600187E RID: 6270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsLocalKeywordEnabled_Injected(ref LocalKeyword keyword);
	}
}
