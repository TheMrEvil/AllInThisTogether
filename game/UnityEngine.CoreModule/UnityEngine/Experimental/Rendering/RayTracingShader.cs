using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000485 RID: 1157
	[NativeHeader("Runtime/Shaders/RayTracingShader.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	public sealed class RayTracingShader : Object
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060028A9 RID: 10409
		public extern float maxRecursionDepth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060028AA RID: 10410
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloat", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float val);

		// Token: 0x060028AB RID: 10411
		[FreeFunction(Name = "RayTracingShaderScripting::SetInt", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(int nameID, int val);

		// Token: 0x060028AC RID: 10412 RVA: 0x000434A9 File Offset: 0x000416A9
		[FreeFunction(Name = "RayTracingShaderScripting::SetVector", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			this.SetVector_Injected(nameID, ref val);
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x000434B4 File Offset: 0x000416B4
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrix", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			this.SetMatrix_Injected(nameID, ref val);
		}

		// Token: 0x060028AE RID: 10414
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArray(int nameID, float[] values);

		// Token: 0x060028AF RID: 10415
		[FreeFunction(Name = "RayTracingShaderScripting::SetIntArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntArray(int nameID, int[] values);

		// Token: 0x060028B0 RID: 10416
		[FreeFunction(Name = "RayTracingShaderScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		// Token: 0x060028B1 RID: 10417
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		// Token: 0x060028B2 RID: 10418
		[NativeMethod(Name = "RayTracingShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture texture);

		// Token: 0x060028B3 RID: 10419
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer);

		// Token: 0x060028B4 RID: 10420
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer);

		// Token: 0x060028B5 RID: 10421
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantComputeBuffer(int nameID, [NotNull("ArgumentNullException")] ComputeBuffer buffer, int offset, int size);

		// Token: 0x060028B6 RID: 10422
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBuffer(int nameID, [NotNull("ArgumentNullException")] GraphicsBuffer buffer, int offset, int size);

		// Token: 0x060028B7 RID: 10423
		[NativeMethod(Name = "RayTracingShaderScripting::SetAccelerationStructure", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAccelerationStructure(int nameID, [NotNull("ArgumentNullException")] RayTracingAccelerationStructure accelerationStructure);

		// Token: 0x060028B8 RID: 10424
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetShaderPass(string passName);

		// Token: 0x060028B9 RID: 10425
		[NativeMethod(Name = "RayTracingShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTextureFromGlobal(int nameID, int globalTextureNameID);

		// Token: 0x060028BA RID: 10426
		[NativeName("DispatchRays")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(string rayGenFunctionName, int width, int height, int depth, Camera camera = null);

		// Token: 0x060028BB RID: 10427 RVA: 0x000434BF File Offset: 0x000416BF
		public void SetBuffer(int nameID, GraphicsBuffer buffer)
		{
			this.SetGraphicsBuffer(nameID, buffer);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0000E886 File Offset: 0x0000CA86
		private RayTracingShader()
		{
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000434CB File Offset: 0x000416CB
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000434DC File Offset: 0x000416DC
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000434ED File Offset: 0x000416ED
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000434FE File Offset: 0x000416FE
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0004350F File Offset: 0x0004170F
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00043520 File Offset: 0x00041720
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00043531 File Offset: 0x00041731
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00043542 File Offset: 0x00041742
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0004354E File Offset: 0x0004174E
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0004355F File Offset: 0x0004175F
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0004356B File Offset: 0x0004176B
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00043582 File Offset: 0x00041782
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00043594 File Offset: 0x00041794
		public void SetTexture(string name, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(name), texture);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000435A5 File Offset: 0x000417A5
		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x000435B6 File Offset: 0x000417B6
		public void SetBuffer(string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000435C7 File Offset: 0x000417C7
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000435D6 File Offset: 0x000417D6
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000435EA File Offset: 0x000417EA
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000435F9 File Offset: 0x000417F9
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0004360D File Offset: 0x0004180D
		public void SetAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetAccelerationStructure(Shader.PropertyToID(name), accelerationStructure);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0004361E File Offset: 0x0004181E
		public void SetTextureFromGlobal(string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x060028D2 RID: 10450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		// Token: 0x060028D3 RID: 10451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
