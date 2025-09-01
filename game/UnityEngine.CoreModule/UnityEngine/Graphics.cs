using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200012D RID: 301
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	[NativeHeader("Runtime/Graphics/CopyTexture.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public class Graphics
	{
		// Token: 0x06000872 RID: 2162
		[FreeFunction("GraphicsScripting::GetMaxDrawMeshInstanceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x06000873 RID: 2163
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ColorGamut GetActiveColorGamut();

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000C82C File Offset: 0x0000AA2C
		public static ColorGamut activeColorGamut
		{
			get
			{
				return Graphics.GetActiveColorGamut();
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000875 RID: 2165
		// (set) Token: 0x06000876 RID: 2166
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		public static extern GraphicsTier activeTier { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000877 RID: 2167
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetPreserveFramebufferAlpha")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetPreserveFramebufferAlpha();

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0000C844 File Offset: 0x0000AA44
		public static bool preserveFramebufferAlpha
		{
			get
			{
				return Graphics.GetPreserveFramebufferAlpha();
			}
		}

		// Token: 0x06000879 RID: 2169
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetMinOpenGLESVersion")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern OpenGLESVersion GetMinOpenGLESVersion();

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public static OpenGLESVersion minOpenGLESVersion
		{
			get
			{
				return Graphics.GetMinOpenGLESVersion();
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000C874 File Offset: 0x0000AA74
		[FreeFunction("GraphicsScripting::GetActiveColorBuffer")]
		private static RenderBuffer GetActiveColorBuffer()
		{
			RenderBuffer result;
			Graphics.GetActiveColorBuffer_Injected(out result);
			return result;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000C88C File Offset: 0x0000AA8C
		[FreeFunction("GraphicsScripting::GetActiveDepthBuffer")]
		private static RenderBuffer GetActiveDepthBuffer()
		{
			RenderBuffer result;
			Graphics.GetActiveDepthBuffer_Injected(out result);
			return result;
		}

		// Token: 0x0600087D RID: 2173
		[FreeFunction("GraphicsScripting::SetNullRT")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetNullRT();

		// Token: 0x0600087E RID: 2174 RVA: 0x0000C8A1 File Offset: 0x0000AAA1
		[NativeMethod(Name = "GraphicsScripting::SetRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetRTSimple(RenderBuffer color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple_Injected(ref color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		[NativeMethod(Name = "GraphicsScripting::SetMRTSimple", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetMRTSimple([NotNull("ArgumentNullException")] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetMRTSimple_Injected(color, ref depth, mip, face, depthSlice);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		[NativeMethod(Name = "GraphicsScripting::SetMRTFull", IsFreeFunction = true, ThrowsException = true)]
		private static void Internal_SetMRTFullSetup([NotNull("ArgumentNullException")] RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, int depthSlice, [NotNull("ArgumentNullException")] RenderBufferLoadAction[] colorLA, [NotNull("ArgumentNullException")] RenderBufferStoreAction[] colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA)
		{
			Graphics.Internal_SetMRTFullSetup_Injected(color, ref depth, mip, face, depthSlice, colorLA, colorSA, depthLA, depthSA);
		}

		// Token: 0x06000881 RID: 2177
		[NativeMethod(Name = "GraphicsScripting::SetRandomWriteTargetRT", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav);

		// Token: 0x06000882 RID: 2178
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav, bool preserveCounterValue);

		// Token: 0x06000883 RID: 2179
		[FreeFunction("GraphicsScripting::SetRandomWriteTargetBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRandomWriteTargetGraphicsBuffer(int index, GraphicsBuffer uav, bool preserveCounterValue);

		// Token: 0x06000884 RID: 2180
		[StaticAccessor("GetGfxDevice()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearRandomWriteTargets();

		// Token: 0x06000885 RID: 2181
		[FreeFunction("CopyTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Full(Texture src, Texture dst);

		// Token: 0x06000886 RID: 2182
		[FreeFunction("CopyTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice_AllMips(Texture src, int srcElement, Texture dst, int dstElement);

		// Token: 0x06000887 RID: 2183
		[FreeFunction("CopyTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Slice(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip);

		// Token: 0x06000888 RID: 2184
		[FreeFunction("CopyTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTexture_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY);

		// Token: 0x06000889 RID: 2185
		[FreeFunction("ConvertTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Full(Texture src, Texture dst);

		// Token: 0x0600088A RID: 2186
		[FreeFunction("ConvertTexture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ConvertTexture_Slice(Texture src, int srcElement, Texture dst, int dstElement);

		// Token: 0x0600088B RID: 2187
		[FreeFunction("GraphicsScripting::CopyBuffer", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyBufferImpl([NotNull("ArgumentNullException")] GraphicsBuffer source, [NotNull("ArgumentNullException")] GraphicsBuffer dest);

		// Token: 0x0600088C RID: 2188 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow1([NotNull("NullExceptionObject")] Mesh mesh, int subsetIndex, Vector3 position, Quaternion rotation)
		{
			Graphics.Internal_DrawMeshNow1_Injected(mesh, subsetIndex, ref position, ref rotation);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0000C8EE File Offset: 0x0000AAEE
		[FreeFunction("GraphicsScripting::DrawMeshNow")]
		private static void Internal_DrawMeshNow2([NotNull("NullExceptionObject")] Mesh mesh, int subsetIndex, Matrix4x4 matrix)
		{
			Graphics.Internal_DrawMeshNow2_Injected(mesh, subsetIndex, ref matrix);
		}

		// Token: 0x0600088E RID: 2190
		[FreeFunction("GraphicsScripting::DrawTexture")]
		[VisibleToOtherModules(new string[]
		{
			"UnityEngine.IMGUIModule"
		})]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawTexture(ref Internal_DrawTextureArguments args);

		// Token: 0x0600088F RID: 2191 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
		[FreeFunction("GraphicsScripting::RenderMesh")]
		private unsafe static void Internal_RenderMesh(RenderParams rparams, [NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, Matrix4x4 objectToWorld, Matrix4x4* prevObjectToWorld)
		{
			Graphics.Internal_RenderMesh_Injected(ref rparams, mesh, submeshIndex, ref objectToWorld, prevObjectToWorld);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000C908 File Offset: 0x0000AB08
		[FreeFunction("GraphicsScripting::RenderMeshInstanced")]
		private static void Internal_RenderMeshInstanced(RenderParams rparams, [NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, IntPtr instanceData, RenderInstancedDataLayout layout, uint instanceCount)
		{
			Graphics.Internal_RenderMeshInstanced_Injected(ref rparams, mesh, submeshIndex, instanceData, ref layout, instanceCount);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000C918 File Offset: 0x0000AB18
		[FreeFunction("GraphicsScripting::RenderMeshIndirect")]
		private static void Internal_RenderMeshIndirect(RenderParams rparams, [NotNull("NullExceptionObject")] Mesh mesh, [NotNull("NullExceptionObject")] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			Graphics.Internal_RenderMeshIndirect_Injected(ref rparams, mesh, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0000C926 File Offset: 0x0000AB26
		[FreeFunction("GraphicsScripting::RenderMeshPrimitives")]
		private static void Internal_RenderMeshPrimitives(RenderParams rparams, [NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, int instanceCount)
		{
			Graphics.Internal_RenderMeshPrimitives_Injected(ref rparams, mesh, submeshIndex, instanceCount);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0000C932 File Offset: 0x0000AB32
		[FreeFunction("GraphicsScripting::RenderPrimitives")]
		private static void Internal_RenderPrimitives(RenderParams rparams, MeshTopology topology, int vertexCount, int instanceCount)
		{
			Graphics.Internal_RenderPrimitives_Injected(ref rparams, topology, vertexCount, instanceCount);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0000C93E File Offset: 0x0000AB3E
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndexed")]
		private static void Internal_RenderPrimitivesIndexed(RenderParams rparams, MeshTopology topology, [NotNull("NullExceptionObject")] GraphicsBuffer indexBuffer, int indexCount, int startIndex, int instanceCount)
		{
			Graphics.Internal_RenderPrimitivesIndexed_Injected(ref rparams, topology, indexBuffer, indexCount, startIndex, instanceCount);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000C94E File Offset: 0x0000AB4E
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndirect")]
		private static void Internal_RenderPrimitivesIndirect(RenderParams rparams, MeshTopology topology, [NotNull("NullExceptionObject")] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			Graphics.Internal_RenderPrimitivesIndirect_Injected(ref rparams, topology, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000C95C File Offset: 0x0000AB5C
		[FreeFunction("GraphicsScripting::RenderPrimitivesIndexedIndirect")]
		private static void Internal_RenderPrimitivesIndexedIndirect(RenderParams rparams, MeshTopology topology, [NotNull("NullExceptionObject")] GraphicsBuffer indexBuffer, [NotNull("NullExceptionObject")] GraphicsBuffer commandBuffer, int commandCount, int startCommand)
		{
			Graphics.Internal_RenderPrimitivesIndexedIndirect_Injected(ref rparams, topology, indexBuffer, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000C96C File Offset: 0x0000AB6C
		[FreeFunction("GraphicsScripting::DrawMesh")]
		private static void Internal_DrawMesh(Mesh mesh, int submeshIndex, Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMesh_Injected(mesh, submeshIndex, ref matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x06000898 RID: 2200
		[FreeFunction("GraphicsScripting::DrawMeshInstanced")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstanced([NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, [NotNull("NullExceptionObject")] Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x06000899 RID: 2201 RVA: 0x0000C994 File Offset: 0x0000AB94
		[FreeFunction("GraphicsScripting::DrawMeshInstancedProcedural")]
		private static void Internal_DrawMeshInstancedProcedural([NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, [NotNull("NullExceptionObject")] Material material, Bounds bounds, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedProcedural_Injected(mesh, submeshIndex, material, ref bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirect([NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, [NotNull("NullExceptionObject")] Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedIndirect_Injected(mesh, submeshIndex, material, ref bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		[FreeFunction("GraphicsScripting::DrawMeshInstancedIndirect")]
		private static void Internal_DrawMeshInstancedIndirectGraphicsBuffer([NotNull("NullExceptionObject")] Mesh mesh, int submeshIndex, [NotNull("NullExceptionObject")] Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
		{
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(mesh, submeshIndex, material, ref bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x0600089C RID: 2204
		[FreeFunction("GraphicsScripting::DrawProceduralNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x0600089D RID: 2205
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount);

		// Token: 0x0600089E RID: 2206
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x0600089F RID: 2207
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060008A0 RID: 2208
		[FreeFunction("GraphicsScripting::DrawProceduralIndirectNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060008A1 RID: 2209
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirectNow")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset);

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000CA14 File Offset: 0x0000AC14
		[FreeFunction("GraphicsScripting::DrawProcedural")]
		private static void Internal_DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProcedural_Injected(material, ref bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000CA38 File Offset: 0x0000AC38
		[FreeFunction("GraphicsScripting::DrawProceduralIndexed")]
		private static void Internal_DrawProceduralIndexed(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexed_Injected(material, ref bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000CA60 File Offset: 0x0000AC60
		[FreeFunction("GraphicsScripting::DrawProceduralIndirect")]
		private static void Internal_DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndirect_Injected(material, ref bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000CA84 File Offset: 0x0000AC84
		[FreeFunction("GraphicsScripting::DrawProceduralIndirect")]
		private static void Internal_DrawProceduralIndirectGraphicsBuffer(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndirectGraphicsBuffer_Injected(material, ref bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirect")]
		private static void Internal_DrawProceduralIndexedIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexedIndirect_Injected(material, ref bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		[FreeFunction("GraphicsScripting::DrawProceduralIndexedIndirect")]
		private static void Internal_DrawProceduralIndexedIndirectGraphicsBuffer(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(material, ref bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008A8 RID: 2216
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial5(Texture source, RenderTexture dest, [NotNull("ArgumentNullException")] Material mat, int pass, bool setRT);

		// Token: 0x060008A9 RID: 2217
		[FreeFunction("GraphicsScripting::BlitMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMaterial6(Texture source, RenderTexture dest, [NotNull("ArgumentNullException")] Material mat, int pass, bool setRT, int destDepthSlice);

		// Token: 0x060008AA RID: 2218
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap4(Texture source, RenderTexture dest, [NotNull("ArgumentNullException")] Material mat, [NotNull("ArgumentNullException")] Vector2[] offsets);

		// Token: 0x060008AB RID: 2219
		[FreeFunction("GraphicsScripting::BlitMultitap")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BlitMultiTap5(Texture source, RenderTexture dest, [NotNull("ArgumentNullException")] Material mat, [NotNull("ArgumentNullException")] Vector2[] offsets, int destDepthSlice);

		// Token: 0x060008AC RID: 2220
		[FreeFunction("GraphicsScripting::Blit")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit2(Texture source, RenderTexture dest);

		// Token: 0x060008AD RID: 2221
		[FreeFunction("GraphicsScripting::Blit")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit3(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x060008AE RID: 2222 RVA: 0x0000CAF5 File Offset: 0x0000ACF5
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit4(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4_Injected(source, dest, ref scale, ref offset);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000CB02 File Offset: 0x0000AD02
		[FreeFunction("GraphicsScripting::Blit")]
		private static void Blit5(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5_Injected(source, dest, ref scale, ref offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060008B0 RID: 2224
		[NativeMethod(Name = "GraphicsScripting::CreateGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateGPUFenceImpl(GraphicsFenceType fenceType, SynchronisationStageFlags stage);

		// Token: 0x060008B1 RID: 2225
		[NativeMethod(Name = "GraphicsScripting::WaitOnGPUFence", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitOnGPUFenceImpl(IntPtr fencePtr, SynchronisationStageFlags stage);

		// Token: 0x060008B2 RID: 2226
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBuffer", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCommandBuffer([NotNull("ArgumentNullException")] CommandBuffer buffer);

		// Token: 0x060008B3 RID: 2227
		[NativeMethod(Name = "GraphicsScripting::ExecuteCommandBufferAsync", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCommandBufferAsync([NotNull("ArgumentNullException")] CommandBuffer buffer, ComputeQueueType queueType);

		// Token: 0x060008B4 RID: 2228 RVA: 0x0000CB14 File Offset: 0x0000AD14
		internal static void CheckLoadActionValid(RenderBufferLoadAction load, string bufferType)
		{
			bool flag = load != RenderBufferLoadAction.Load && load != RenderBufferLoadAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} LoadAction provided.", new object[]
				{
					bufferType
				}));
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000CB50 File Offset: 0x0000AD50
		internal static void CheckStoreActionValid(RenderBufferStoreAction store, string bufferType)
		{
			bool flag = store != RenderBufferStoreAction.Store && store != RenderBufferStoreAction.DontCare;
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Bad {0} StoreAction provided.", new object[]
				{
					bufferType
				}));
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		internal static void SetRenderTargetImpl(RenderTargetSetup setup)
		{
			bool flag = setup.color.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
			}
			bool flag2 = setup.color.Length != setup.colorLoad.Length;
			if (flag2)
			{
				throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
			}
			bool flag3 = setup.color.Length != setup.colorStore.Length;
			if (flag3)
			{
				throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");
			}
			foreach (RenderBufferLoadAction load in setup.colorLoad)
			{
				Graphics.CheckLoadActionValid(load, "Color");
			}
			foreach (RenderBufferStoreAction store in setup.colorStore)
			{
				Graphics.CheckStoreActionValid(store, "Color");
			}
			Graphics.CheckLoadActionValid(setup.depthLoad, "Depth");
			Graphics.CheckStoreActionValid(setup.depthStore, "Depth");
			bool flag4 = setup.cubemapFace < CubemapFace.Unknown || setup.cubemapFace > CubemapFace.NegativeZ;
			if (flag4)
			{
				throw new ArgumentException("Bad CubemapFace provided");
			}
			Graphics.Internal_SetMRTFullSetup(setup.color, setup.depth, setup.mipLevel, setup.cubemapFace, setup.depthSlice, setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetRTSimple(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
		{
			bool flag = rt;
			if (flag)
			{
				Graphics.SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
			}
			else
			{
				Graphics.Internal_SetNullRT();
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000CD24 File Offset: 0x0000AF24
		internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
		{
			Graphics.Internal_SetMRTSimple(colorBuffers, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000CD40 File Offset: 0x0000AF40
		public static void SetRenderTarget(RenderTexture rt, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, [DefaultValue("0")] int mipLevel, [DefaultValue("CubemapFace.Unknown")] CubemapFace face, [DefaultValue("0")] int depthSlice)
		{
			Graphics.SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0000CD6A File Offset: 0x0000AF6A
		public static void SetRenderTarget(RenderTargetSetup setup)
		{
			Graphics.SetRenderTargetImpl(setup);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static RenderBuffer activeColorBuffer
		{
			get
			{
				return Graphics.GetActiveColorBuffer();
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		public static RenderBuffer activeDepthBuffer
		{
			get
			{
				return Graphics.GetActiveDepthBuffer();
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public static void SetRandomWriteTarget(int index, RenderTexture uav)
		{
			bool flag = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetRT(index, uav);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000CE70 File Offset: 0x0000B070
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
		{
			bool flag = uav == null;
			if (flag)
			{
				throw new ArgumentNullException("uav");
			}
			bool flag2 = uav.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("uav");
			}
			bool flag3 = index < 0 || index >= SystemInfo.supportedRandomWriteTargetCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("must be non-negative less than {0}.", SystemInfo.supportedRandomWriteTargetCount));
			}
			Graphics.Internal_SetRandomWriteTargetGraphicsBuffer(index, uav, preserveCounterValue);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000CEEE File Offset: 0x0000B0EE
		public static void CopyTexture(Texture src, Texture dst)
		{
			Graphics.CopyTexture_Full(src, dst);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000CEF9 File Offset: 0x0000B0F9
		public static void CopyTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			Graphics.CopyTexture_Slice_AllMips(src, srcElement, dst, dstElement);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000CF06 File Offset: 0x0000B106
		public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
		{
			Graphics.CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000CF18 File Offset: 0x0000B118
		public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
		{
			Graphics.CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000CF40 File Offset: 0x0000B140
		public static bool ConvertTexture(Texture src, Texture dst)
		{
			return Graphics.ConvertTexture_Full(src, dst);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public static bool ConvertTexture(Texture src, int srcElement, Texture dst, int dstElement)
		{
			return Graphics.ConvertTexture_Slice(src, srcElement, dst, dstElement);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000CF78 File Offset: 0x0000B178
		public static GraphicsFence CreateAsyncGraphicsFence([DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000CF98 File Offset: 0x0000B198
		public static GraphicsFence CreateAsyncGraphicsFence()
		{
			return Graphics.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.PixelProcessing);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		public static GraphicsFence CreateGraphicsFence(GraphicsFenceType fenceType, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStageFlags stage)
		{
			GraphicsFence result = default(GraphicsFence);
			result.m_FenceType = fenceType;
			result.m_Ptr = Graphics.CreateGPUFenceImpl(fenceType, stage);
			result.InitPostAllocation();
			result.Validate();
			return result;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0000CFF5 File Offset: 0x0000B1F5
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence)
		{
			Graphics.WaitOnAsyncGraphicsFence(fence, SynchronisationStage.PixelProcessing);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000D000 File Offset: 0x0000B200
		public static void WaitOnAsyncGraphicsFence(GraphicsFence fence, [DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
		{
			bool flag = fence.m_FenceType > GraphicsFenceType.AsyncQueueSynchronisation;
			if (flag)
			{
				throw new ArgumentException("Graphics.WaitOnGraphicsFence can only be called with fences created with GraphicsFenceType.AsyncQueueSynchronization.");
			}
			fence.Validate();
			bool flag2 = fence.IsFencePending();
			if (flag2)
			{
				Graphics.WaitOnGPUFenceImpl(fence.m_Ptr, GraphicsFence.TranslateSynchronizationStageToFlags(stage));
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0000D04C File Offset: 0x0000B24C
		internal static void ValidateCopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
		{
			bool flag = source == null;
			if (flag)
			{
				throw new ArgumentNullException("source");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
			long num = (long)source.count * (long)source.stride;
			long num2 = (long)dest.count * (long)dest.stride;
			bool flag3 = num != num2;
			if (flag3)
			{
				throw new ArgumentException(string.Format("CopyBuffer source and destination buffers must be the same size, source was {0} bytes, dest was {1} bytes", num, num2));
			}
			bool flag4 = (source.target & GraphicsBuffer.Target.CopySource) == (GraphicsBuffer.Target)0;
			if (flag4)
			{
				throw new ArgumentException("CopyBuffer source must have CopySource target", "source");
			}
			bool flag5 = (dest.target & GraphicsBuffer.Target.CopyDestination) == (GraphicsBuffer.Target)0;
			if (flag5)
			{
				throw new ArgumentException("CopyBuffer destination must have CopyDestination target", "dest");
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0000D10B File Offset: 0x0000B30B
		public static void CopyBuffer(GraphicsBuffer source, GraphicsBuffer dest)
		{
			Graphics.ValidateCopyBuffer(source, dest);
			Graphics.CopyBufferImpl(source, dest);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000D120 File Offset: 0x0000B320
		private static void DrawTextureImpl(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat, int pass)
		{
			Internal_DrawTextureArguments internal_DrawTextureArguments = default(Internal_DrawTextureArguments);
			internal_DrawTextureArguments.screenRect = screenRect;
			internal_DrawTextureArguments.sourceRect = sourceRect;
			internal_DrawTextureArguments.leftBorder = leftBorder;
			internal_DrawTextureArguments.rightBorder = rightBorder;
			internal_DrawTextureArguments.topBorder = topBorder;
			internal_DrawTextureArguments.bottomBorder = bottomBorder;
			internal_DrawTextureArguments.color = color;
			internal_DrawTextureArguments.leftBorderColor = Color.black;
			internal_DrawTextureArguments.topBorderColor = Color.black;
			internal_DrawTextureArguments.rightBorderColor = Color.black;
			internal_DrawTextureArguments.bottomBorderColor = Color.black;
			internal_DrawTextureArguments.pass = pass;
			internal_DrawTextureArguments.texture = texture;
			internal_DrawTextureArguments.smoothCorners = true;
			internal_DrawTextureArguments.mat = mat;
			Graphics.Internal_DrawTexture(ref internal_DrawTextureArguments);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Color32 c = new Color32(128, 128, 128, 128);
			Graphics.DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, c, mat, pass);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000D234 File Offset: 0x0000B434
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0000D26D File Offset: 0x0000B46D
		public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.DrawTexture(screenRect, texture, 0, 0, 0, 0, mat, pass);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0000D280 File Offset: 0x0000B480
		public unsafe static void RenderMesh(in RenderParams rparams, Mesh mesh, int submeshIndex, Matrix4x4 objectToWorld, [DefaultValue("null")] Matrix4x4? prevObjectToWorld = null)
		{
			bool flag = prevObjectToWorld != null;
			if (flag)
			{
				Matrix4x4 value = prevObjectToWorld.Value;
				Graphics.Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, &value);
			}
			else
			{
				Graphics.Internal_RenderMesh(rparams, mesh, submeshIndex, objectToWorld, null);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		private static RenderInstancedDataLayout GetCachedRenderInstancedDataLayout(Type type)
		{
			int hashCode = type.GetHashCode();
			RenderInstancedDataLayout renderInstancedDataLayout;
			bool flag = !Graphics.s_RenderInstancedDataLayouts.TryGetValue(hashCode, out renderInstancedDataLayout);
			if (flag)
			{
				renderInstancedDataLayout = new RenderInstancedDataLayout(type);
				Graphics.s_RenderInstancedDataLayouts.Add(hashCode, renderInstancedDataLayout);
			}
			return renderInstancedDataLayout;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000D310 File Offset: 0x0000B510
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(in RenderParams rparams, Mesh mesh, int submeshIndex, T[] instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			bool flag3 = instanceData == null;
			if (flag3)
			{
				throw new ArgumentNullException("instanceData");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint instanceCount2 = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
			fixed (T[] array = instanceData)
			{
				T* ptr;
				if (instanceData == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, instanceCount2);
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(in RenderParams rparams, Mesh mesh, int submeshIndex, List<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			bool flag3 = instanceData == null;
			if (flag3)
			{
				throw new ArgumentNullException("instanceData");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint instanceCount2 = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Count - startInstance));
			T[] array;
			T* ptr;
			if ((array = NoAllocHelpers.ExtractArrayFromListT<T>(instanceData)) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, instanceCount2);
			array = null;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000D498 File Offset: 0x0000B698
		public unsafe static void RenderMeshInstanced<[IsUnmanaged] T>(RenderParams rparams, Mesh mesh, int submeshIndex, NativeArray<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0) where T : struct, ValueType
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = !rparams.material.enableInstancing;
			if (flag2)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with RenderMeshInstanced.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			uint instanceCount2 = Math.Min((uint)instanceCount, (uint)Math.Max(0, instanceData.Length - startInstance));
			Graphics.Internal_RenderMeshInstanced(rparams, mesh, submeshIndex, (IntPtr)((void*)((byte*)instanceData.GetUnsafePtr<T>() + (IntPtr)startInstance * (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, instanceCount2);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000D52C File Offset: 0x0000B72C
		public static void RenderMeshIndirect(in RenderParams rparams, Mesh mesh, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderMeshIndirect(rparams, mesh, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000D564 File Offset: 0x0000B764
		public static void RenderMeshPrimitives(in RenderParams rparams, Mesh mesh, int submeshIndex, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderMeshPrimitives(rparams, mesh, submeshIndex, instanceCount);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000D598 File Offset: 0x0000B798
		public static void RenderPrimitives(in RenderParams rparams, MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitives(rparams, topology, vertexCount, instanceCount);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public static void RenderPrimitivesIndexed(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, [DefaultValue("0")] int startIndex = 0, [DefaultValue("1")] int instanceCount = 1)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndexed(rparams, topology, indexBuffer, indexCount, startIndex, instanceCount);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000D604 File Offset: 0x0000B804
		public static void RenderPrimitivesIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndirect(rparams, topology, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0000D63C File Offset: 0x0000B83C
		public static void RenderPrimitivesIndexedIndirect(in RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer commandBuffer, [DefaultValue("1")] int commandCount = 1, [DefaultValue("0")] int startCommand = 0)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			Graphics.Internal_RenderPrimitivesIndexedIndirect(rparams, topology, indexBuffer, commandBuffer, commandCount, startCommand);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000D674 File Offset: 0x0000B874
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow1(mesh, materialIndex, position, rotation);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh");
			}
			Graphics.Internal_DrawMeshNow2(mesh, materialIndex, matrix);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
		public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Graphics.DrawMeshNow(mesh, position, rotation, -1);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000D6DE File Offset: 0x0000B8DE
		public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
		{
			Graphics.DrawMeshNow(mesh, matrix, -1);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0000D72C File Offset: 0x0000B92C
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0000D768 File Offset: 0x0000B968
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0000D79C File Offset: 0x0000B99C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [DefaultValue("matrices.Length")] int count, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = !material.enableInstancing;
			if (flag5)
			{
				throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
			}
			bool flag6 = matrices == null;
			if (flag6)
			{
				throw new ArgumentNullException("matrices");
			}
			bool flag7 = count < 0 || count > Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length);
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("count", string.Format("Count must be in the range of 0 to {0}.", Mathf.Min(Graphics.kMaxDrawMeshInstanceCount, matrices.Length)));
			}
			bool flag8 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag8)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag9 = count > 0;
			if (flag9)
			{
				Graphics.Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0000D920 File Offset: 0x0000BB20
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = matrices == null;
			if (flag)
			{
				throw new ArgumentNullException("matrices");
			}
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(matrices), matrices.Count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0000D964 File Offset: 0x0000BB64
		public static void DrawMeshInstancedProcedural(Mesh mesh, int submeshIndex, Material material, Bounds bounds, int count, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes, LightProbeProxyVolume lightProbeProxyVolume = null)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = count <= 0;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			bool flag7 = count > 0;
			if (flag7)
			{
				Graphics.Internal_DrawMeshInstancedProcedural(mesh, submeshIndex, material, bounds, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0000DA40 File Offset: 0x0000BC40
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
		{
			bool flag = !SystemInfo.supportsInstancing;
			if (flag)
			{
				throw new InvalidOperationException("Instancing is not supported.");
			}
			bool flag2 = mesh == null;
			if (flag2)
			{
				throw new ArgumentNullException("mesh");
			}
			bool flag3 = submeshIndex < 0 || submeshIndex >= mesh.subMeshCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
			}
			bool flag4 = material == null;
			if (flag4)
			{
				throw new ArgumentNullException("material");
			}
			bool flag5 = bufferWithArgs == null;
			if (flag5)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			bool flag6 = lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.", "lightProbeProxyVolume");
			}
			Graphics.Internal_DrawMeshInstancedIndirectGraphicsBuffer(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public static void DrawProceduralNow(MeshTopology topology, int vertexCount, int instanceCount = 1)
		{
			Graphics.Internal_DrawProceduralNow(topology, vertexCount, instanceCount);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		public static void DrawProceduralNow(MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexedNow(topology, indexBuffer, indexCount, instanceCount);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0000DC18 File Offset: 0x0000BE18
		public static void DrawProceduralIndirectNow(MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNow(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNow(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000DC84 File Offset: 0x0000BE84
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectNowGraphicsBuffer(topology, bufferWithArgs, argsOffset);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public static void DrawProceduralIndirectNow(MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectNowGraphicsBuffer(topology, indexBuffer, bufferWithArgs, argsOffset);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			Graphics.Internal_DrawProcedural(material, bounds, topology, vertexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000DD14 File Offset: 0x0000BF14
		public static void DrawProcedural(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount = 1, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			Graphics.Internal_DrawProceduralIndexed(material, bounds, topology, indexBuffer, indexCount, instanceCount, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirect(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = bufferWithArgs == null;
			if (flag)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndirectGraphicsBuffer(material, bounds, topology, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirect(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000DE10 File Offset: 0x0000C010
		public static void DrawProceduralIndirect(Material material, Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset = 0, Camera camera = null, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0)
		{
			bool flag = indexBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("indexBuffer");
			}
			bool flag2 = bufferWithArgs == null;
			if (flag2)
			{
				throw new ArgumentNullException("bufferWithArgs");
			}
			Graphics.Internal_DrawProceduralIndexedIndirectGraphicsBuffer(material, bounds, topology, indexBuffer, bufferWithArgs, argsOffset, camera, properties, castShadows, receiveShadows, layer);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0000DE5D File Offset: 0x0000C05D
		public static void Blit(Texture source, RenderTexture dest)
		{
			Graphics.Blit2(source, dest);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000DE68 File Offset: 0x0000C068
		public static void Blit(Texture source, RenderTexture dest, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit3(source, dest, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000DE75 File Offset: 0x0000C075
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
		{
			Graphics.Blit4(source, dest, scale, offset);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000DE82 File Offset: 0x0000C082
		public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset, int sourceDepthSlice, int destDepthSlice)
		{
			Graphics.Blit5(source, dest, scale, offset, sourceDepthSlice, destDepthSlice);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000DE93 File Offset: 0x0000C093
		public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, dest, mat, pass, true);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000DEA1 File Offset: 0x0000C0A1
		public static void Blit(Texture source, RenderTexture dest, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, dest, mat, pass, true, destDepthSlice);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000DEB1 File Offset: 0x0000C0B1
		public static void Blit(Texture source, RenderTexture dest, Material mat)
		{
			Graphics.Blit(source, dest, mat, -1);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000DEBE File Offset: 0x0000C0BE
		public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
		{
			Graphics.Internal_BlitMaterial5(source, null, mat, pass, false);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public static void Blit(Texture source, Material mat, int pass, int destDepthSlice)
		{
			Graphics.Internal_BlitMaterial6(source, null, mat, pass, false, destDepthSlice);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0000DEDB File Offset: 0x0000C0DB
		public static void Blit(Texture source, Material mat)
		{
			Graphics.Blit(source, mat, -1);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap4(source, dest, mat, offsets);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, int destDepthSlice, params Vector2[] offsets)
		{
			bool flag = offsets.Length == 0;
			if (flag)
			{
				throw new ArgumentException("empty offsets list passed.", "offsets");
			}
			Graphics.Internal_BlitMultiTap5(source, dest, mat, offsets, destDepthSlice);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0000DF50 File Offset: 0x0000C150
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000DF7C File Offset: 0x0000C17C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0000E00C File Offset: 0x0000C20C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000E044 File Offset: 0x0000C244
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0000E07C File Offset: 0x0000C27C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000E114 File Offset: 0x0000C314
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, null, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000E134 File Offset: 0x0000C334
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, 0, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0000E158 File Offset: 0x0000C358
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, null, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000E17C File Offset: 0x0000C37C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, true, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000E21C File Offset: 0x0000C41C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000E244 File Offset: 0x0000C444
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000E26C File Offset: 0x0000C46C
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
		{
			Graphics.DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000E29C File Offset: 0x0000C49C
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage)
		{
			Graphics.Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, null);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, matrices.Length, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000E30C File Offset: 0x0000C50C
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000E330 File Offset: 0x0000C530
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000E354 File Offset: 0x0000C554
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0000E378 File Offset: 0x0000C578
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, null, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000E410 File Offset: 0x0000C610
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, ShadowCastingMode.On, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000E430 File Offset: 0x0000C630
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, true, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000E454 File Offset: 0x0000C654
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000E478 File Offset: 0x0000C678
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0000E49C File Offset: 0x0000C69C
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		[ExcludeFromDocs]
		public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
		{
			Graphics.DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0000E514 File Offset: 0x0000C714
		[ExcludeFromDocs]
		public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset = 0, MaterialPropertyBlock properties = null, ShadowCastingMode castShadows = ShadowCastingMode.On, bool receiveShadows = true, int layer = 0, Camera camera = null, LightProbeUsage lightProbeUsage = LightProbeUsage.BlendProbes)
		{
			Graphics.DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000E540 File Offset: 0x0000C740
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, -1);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0000E564 File Offset: 0x0000C764
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, null, -1);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0000E588 File Offset: 0x0000C788
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat, -1);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Graphics.DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, null, -1);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000E5F3 File Offset: 0x0000C7F3
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture, Material mat)
		{
			Graphics.DrawTexture(screenRect, texture, mat, -1);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0000E600 File Offset: 0x0000C800
		[ExcludeFromDocs]
		public static void DrawTexture(Rect screenRect, Texture texture)
		{
			Graphics.DrawTexture(screenRect, texture, null, -1);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0000E60D File Offset: 0x0000C80D
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt)
		{
			Graphics.SetRenderTarget(rt, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0000E61A File Offset: 0x0000C81A
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel)
		{
			Graphics.SetRenderTarget(rt, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000E627 File Offset: 0x0000C827
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(rt, mipLevel, face, 0);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000E634 File Offset: 0x0000C834
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000E642 File Offset: 0x0000C842
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, CubemapFace.Unknown, 0);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000E650 File Offset: 0x0000C850
		[ExcludeFromDocs]
		public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
		{
			Graphics.SetRenderTarget(colorBuffer, depthBuffer, mipLevel, face, 0);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000E65E File Offset: 0x0000C85E
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0000E66A File Offset: 0x0000C86A
		[ExcludeFromDocs]
		public static void SetRandomWriteTarget(int index, GraphicsBuffer uav)
		{
			Graphics.SetRandomWriteTarget(index, uav, false);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00002072 File Offset: 0x00000272
		public Graphics()
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0000E676 File Offset: 0x0000C876
		// Note: this type is marked as 'beforefieldinit'.
		static Graphics()
		{
		}

		// Token: 0x0600093C RID: 2364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x0600093D RID: 2365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x0600093E RID: 2366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRTSimple_Injected(ref RenderBuffer color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x0600093F RID: 2367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTSimple_Injected(RenderBuffer[] color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

		// Token: 0x06000940 RID: 2368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetMRTFullSetup_Injected(RenderBuffer[] color, ref RenderBuffer depth, int mip, CubemapFace face, int depthSlice, RenderBufferLoadAction[] colorLA, RenderBufferStoreAction[] colorSA, RenderBufferLoadAction depthLA, RenderBufferStoreAction depthSA);

		// Token: 0x06000941 RID: 2369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow1_Injected(Mesh mesh, int subsetIndex, ref Vector3 position, ref Quaternion rotation);

		// Token: 0x06000942 RID: 2370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshNow2_Injected(Mesh mesh, int subsetIndex, ref Matrix4x4 matrix);

		// Token: 0x06000943 RID: 2371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_RenderMesh_Injected(ref RenderParams rparams, Mesh mesh, int submeshIndex, ref Matrix4x4 objectToWorld, Matrix4x4* prevObjectToWorld);

		// Token: 0x06000944 RID: 2372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshInstanced_Injected(ref RenderParams rparams, Mesh mesh, int submeshIndex, IntPtr instanceData, ref RenderInstancedDataLayout layout, uint instanceCount);

		// Token: 0x06000945 RID: 2373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshIndirect_Injected(ref RenderParams rparams, Mesh mesh, GraphicsBuffer commandBuffer, int commandCount, int startCommand);

		// Token: 0x06000946 RID: 2374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderMeshPrimitives_Injected(ref RenderParams rparams, Mesh mesh, int submeshIndex, int instanceCount);

		// Token: 0x06000947 RID: 2375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitives_Injected(ref RenderParams rparams, MeshTopology topology, int vertexCount, int instanceCount);

		// Token: 0x06000948 RID: 2376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndexed_Injected(ref RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int startIndex, int instanceCount);

		// Token: 0x06000949 RID: 2377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndirect_Injected(ref RenderParams rparams, MeshTopology topology, GraphicsBuffer commandBuffer, int commandCount, int startCommand);

		// Token: 0x0600094A RID: 2378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RenderPrimitivesIndexedIndirect_Injected(ref RenderParams rparams, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer commandBuffer, int commandCount, int startCommand);

		// Token: 0x0600094B RID: 2379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMesh_Injected(Mesh mesh, int submeshIndex, ref Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x0600094C RID: 2380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedProcedural_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x0600094D RID: 2381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirect_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x0600094E RID: 2382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawMeshInstancedIndirectGraphicsBuffer_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, GraphicsBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

		// Token: 0x0600094F RID: 2383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProcedural_Injected(Material material, ref Bounds bounds, MeshTopology topology, int vertexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000950 RID: 2384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexed_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, int indexCount, int instanceCount, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000951 RID: 2385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirect_Injected(Material material, ref Bounds bounds, MeshTopology topology, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000952 RID: 2386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndirectGraphicsBuffer_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000953 RID: 2387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirect_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, ComputeBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000954 RID: 2388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawProceduralIndexedIndirectGraphicsBuffer_Injected(Material material, ref Bounds bounds, MeshTopology topology, GraphicsBuffer indexBuffer, GraphicsBuffer bufferWithArgs, int argsOffset, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer);

		// Token: 0x06000955 RID: 2389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit4_Injected(Texture source, RenderTexture dest, ref Vector2 scale, ref Vector2 offset);

		// Token: 0x06000956 RID: 2390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Blit5_Injected(Texture source, RenderTexture dest, ref Vector2 scale, ref Vector2 offset, int sourceDepthSlice, int destDepthSlice);

		// Token: 0x040003CB RID: 971
		internal static readonly int kMaxDrawMeshInstanceCount = Graphics.Internal_GetMaxDrawMeshInstanceCount();

		// Token: 0x040003CC RID: 972
		internal static Dictionary<int, RenderInstancedDataLayout> s_RenderInstancedDataLayouts = new Dictionary<int, RenderInstancedDataLayout>();
	}
}
