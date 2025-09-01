using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[Obsolete("Procedural Virtual Texturing is experimental, not ready for production use and Unity does not currently support it. The feature might be changed or removed in the future.", false)]
	[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
	public static class Procedural
	{
		// Token: 0x06000028 RID: 40
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCPUCacheSize(int sizeInMegabytes);

		// Token: 0x06000029 RID: 41
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCPUCacheSize();

		// Token: 0x0600002A RID: 42
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGPUCacheSettings(GPUCacheSetting[] cacheSettings);

		// Token: 0x0600002B RID: 43
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GPUCacheSetting[] GetGPUCacheSettings();

		// Token: 0x0200000C RID: 12
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
		internal static class Binding
		{
			// Token: 0x0600002C RID: 44 RVA: 0x00002251 File Offset: 0x00000451
			internal static ulong Create(Procedural.CreationParameters p)
			{
				return Procedural.Binding.Create_Injected(ref p);
			}

			// Token: 0x0600002D RID: 45
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void Destroy(ulong handle);

			// Token: 0x0600002E RID: 46
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern int PopRequests(ulong handle, IntPtr requestHandles, int length);

			// Token: 0x0600002F RID: 47
			[ThreadSafe]
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void GetRequestParameters(IntPtr requestHandles, IntPtr requestParameters, int length);

			// Token: 0x06000030 RID: 48
			[ThreadSafe]
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void UpdateRequestState(IntPtr requestHandles, IntPtr requestUpdates, int length);

			// Token: 0x06000031 RID: 49
			[ThreadSafe]
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void UpdateRequestStateWithCommandBuffer(IntPtr requestHandles, IntPtr requestUpdates, int length, CommandBuffer fenceBuffer);

			// Token: 0x06000032 RID: 50
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void BindToMaterialPropertyBlock(ulong handle, [NotNull("ArgumentNullException")] MaterialPropertyBlock material, string name);

			// Token: 0x06000033 RID: 51
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void BindToMaterial(ulong handle, [NotNull("ArgumentNullException")] Material material, string name);

			// Token: 0x06000034 RID: 52
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void BindGlobally(ulong handle, string name);

			// Token: 0x06000035 RID: 53 RVA: 0x0000225A File Offset: 0x0000045A
			[NativeThrows]
			internal static void RequestRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.RequestRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002266 File Offset: 0x00000466
			[NativeThrows]
			internal static void InvalidateRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.InvalidateRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002272 File Offset: 0x00000472
			[NativeThrows]
			public static void EvictRegion(ulong handle, Rect r, int mipMap, int numMips)
			{
				Procedural.Binding.EvictRegion_Injected(handle, ref r, mipMap, numMips);
			}

			// Token: 0x06000038 RID: 56
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong Create_Injected(ref Procedural.CreationParameters p);

			// Token: 0x06000039 RID: 57
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RequestRegion_Injected(ulong handle, ref Rect r, int mipMap, int numMips);

			// Token: 0x0600003A RID: 58
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void InvalidateRegion_Injected(ulong handle, ref Rect r, int mipMap, int numMips);

			// Token: 0x0600003B RID: 59
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void EvictRegion_Injected(ulong handle, ref Rect r, int mipMap, int numMips);
		}

		// Token: 0x0200000D RID: 13
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		public struct CreationParameters
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00002280 File Offset: 0x00000480
			internal void Validate()
			{
				bool flag = this.width <= 0 || this.height <= 0 || this.tilesize <= 0;
				if (flag)
				{
					throw new ArgumentException(string.Format("Zero sized dimensions are invalid (width: {0}, height: {1}, tilesize {2}", this.width, this.height, this.tilesize));
				}
				bool flag2 = this.layers == null || this.layers.Length > 4;
				if (flag2)
				{
					throw new ArgumentException(string.Format("layers is either invalid or has too many layers (maxNumLayers: {0})", 4));
				}
				bool flag3 = this.gpuGeneration == 1 && this.filterMode != FilterMode.Bilinear;
				if (flag3)
				{
					throw new ArgumentException("Filter mode invalid for GPU PVT; only FilterMode.Bilinear is currently supported");
				}
				bool flag4 = this.gpuGeneration == 0 && this.filterMode != FilterMode.Bilinear && this.filterMode != FilterMode.Trilinear;
				if (flag4)
				{
					throw new ArgumentException("Filter mode invalid for CPU PVT; only FilterMode.Bilinear and FilterMode.Trilinear are currently supported");
				}
				GraphicsFormat[] array = new GraphicsFormat[]
				{
					GraphicsFormat.R8G8B8A8_SRGB,
					GraphicsFormat.R8G8B8A8_UNorm,
					GraphicsFormat.R32G32B32A32_SFloat,
					GraphicsFormat.R8G8_SRGB,
					GraphicsFormat.R8G8_UNorm,
					GraphicsFormat.R32_SFloat,
					GraphicsFormat.RGB_DXT1_SRGB,
					GraphicsFormat.RGB_DXT1_UNorm,
					GraphicsFormat.RGBA_DXT5_SRGB,
					GraphicsFormat.RGBA_DXT5_UNorm,
					GraphicsFormat.RGBA_BC7_SRGB,
					GraphicsFormat.RGBA_BC7_UNorm,
					GraphicsFormat.RG_BC5_SNorm,
					GraphicsFormat.RG_BC5_UNorm,
					GraphicsFormat.RGB_BC6H_SFloat,
					GraphicsFormat.RGB_BC6H_UFloat,
					GraphicsFormat.R16_SFloat,
					GraphicsFormat.R16_UNorm,
					GraphicsFormat.R16G16_SFloat,
					GraphicsFormat.R16G16_UNorm,
					GraphicsFormat.R16G16B16A16_SFloat,
					GraphicsFormat.R16G16B16A16_UNorm
				};
				GraphicsFormat[] array2 = new GraphicsFormat[]
				{
					GraphicsFormat.R8G8B8A8_SRGB,
					GraphicsFormat.R8G8B8A8_UNorm,
					GraphicsFormat.R32G32B32A32_SFloat,
					GraphicsFormat.R8G8_SRGB,
					GraphicsFormat.R8G8_UNorm,
					GraphicsFormat.R32_SFloat,
					GraphicsFormat.A2B10G10R10_UNormPack32,
					GraphicsFormat.R16_UNorm
				};
				FormatUsage usage = (this.gpuGeneration == 1) ? FormatUsage.Render : FormatUsage.Sample;
				for (int i = 0; i < this.layers.Length; i++)
				{
					bool flag5 = SystemInfo.GetCompatibleFormat(this.layers[i], usage) != this.layers[i];
					if (flag5)
					{
						throw new ArgumentException(string.Format("Requested format {0} on layer {1} is not supported on this platform", this.layers[i], i));
					}
					bool flag6 = false;
					GraphicsFormat[] array3 = (this.gpuGeneration == 1) ? array2 : array;
					for (int j = 0; j < array3.Length; j++)
					{
						bool flag7 = this.layers[i] == array3[j];
						if (flag7)
						{
							flag6 = true;
							break;
						}
					}
					bool flag8 = !flag6;
					if (flag8)
					{
						string arg = (this.gpuGeneration == 1) ? "GPU" : "CPU";
						throw new ArgumentException(string.Format("{0} Procedural Virtual Texturing doesn't support GraphicsFormat {1} for stack layer {2}", arg, this.layers[i], i));
					}
				}
				bool flag9 = this.maxActiveRequests > 4095 || this.maxActiveRequests <= 0;
				if (flag9)
				{
					throw new ArgumentException(string.Format("Invalid requests per frame (maxActiveRequests: ]0, {0}])", this.maxActiveRequests));
				}
			}

			// Token: 0x04000011 RID: 17
			public const int MaxNumLayers = 4;

			// Token: 0x04000012 RID: 18
			public const int MaxRequestsPerFrameSupported = 4095;

			// Token: 0x04000013 RID: 19
			public int width;

			// Token: 0x04000014 RID: 20
			public int height;

			// Token: 0x04000015 RID: 21
			public int maxActiveRequests;

			// Token: 0x04000016 RID: 22
			public int tilesize;

			// Token: 0x04000017 RID: 23
			public GraphicsFormat[] layers;

			// Token: 0x04000018 RID: 24
			public FilterMode filterMode;

			// Token: 0x04000019 RID: 25
			internal int borderSize;

			// Token: 0x0400001A RID: 26
			internal int gpuGeneration;

			// Token: 0x0400001B RID: 27
			internal int flags;
		}

		// Token: 0x0200000E RID: 14
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		internal struct RequestHandlePayload : IEquatable<Procedural.RequestHandlePayload>
		{
			// Token: 0x0600003D RID: 61 RVA: 0x0000256C File Offset: 0x0000076C
			public static bool operator !=(Procedural.RequestHandlePayload lhs, Procedural.RequestHandlePayload rhs)
			{
				return !(lhs == rhs);
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00002588 File Offset: 0x00000788
			public override bool Equals(object obj)
			{
				return obj is Procedural.RequestHandlePayload && this == (Procedural.RequestHandlePayload)obj;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x000025B8 File Offset: 0x000007B8
			public bool Equals(Procedural.RequestHandlePayload other)
			{
				return this == other;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x000025D8 File Offset: 0x000007D8
			public override int GetHashCode()
			{
				int num = -2128608763;
				num = num * -1521134295 + this.id.GetHashCode();
				num = num * -1521134295 + this.lifetime.GetHashCode();
				return num * -1521134295 + this.callback.GetHashCode();
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002630 File Offset: 0x00000830
			public static bool operator ==(Procedural.RequestHandlePayload lhs, Procedural.RequestHandlePayload rhs)
			{
				return lhs.id == rhs.id && lhs.lifetime == rhs.lifetime && lhs.callback == rhs.callback;
			}

			// Token: 0x0400001C RID: 28
			internal int id;

			// Token: 0x0400001D RID: 29
			internal int lifetime;

			// Token: 0x0400001E RID: 30
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr callback;
		}

		// Token: 0x0200000F RID: 15
		public struct TextureStackRequestHandle<T> : IEquatable<Procedural.TextureStackRequestHandle<T>> where T : struct
		{
			// Token: 0x06000042 RID: 66 RVA: 0x00002674 File Offset: 0x00000874
			public static bool operator !=(Procedural.TextureStackRequestHandle<T> h1, Procedural.TextureStackRequestHandle<T> h2)
			{
				return !(h1 == h2);
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002690 File Offset: 0x00000890
			public override bool Equals(object obj)
			{
				return obj is Procedural.TextureStackRequestHandle<T> && this == (Procedural.TextureStackRequestHandle<T>)obj;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000026C0 File Offset: 0x000008C0
			public bool Equals(Procedural.TextureStackRequestHandle<T> other)
			{
				return this == other;
			}

			// Token: 0x06000045 RID: 69 RVA: 0x000026E0 File Offset: 0x000008E0
			public override int GetHashCode()
			{
				return this.payload.GetHashCode();
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002704 File Offset: 0x00000904
			public static bool operator ==(Procedural.TextureStackRequestHandle<T> h1, Procedural.TextureStackRequestHandle<T> h2)
			{
				return h1.payload == h2.payload;
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00002727 File Offset: 0x00000927
			public void CompleteRequest(Procedural.RequestStatus status)
			{
				Procedural.Binding.UpdateRequestState((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<Procedural.RequestStatus>(ref status), 1);
			}

			// Token: 0x06000048 RID: 72 RVA: 0x0000274A File Offset: 0x0000094A
			public void CompleteRequest(Procedural.RequestStatus status, CommandBuffer fenceBuffer)
			{
				Procedural.Binding.UpdateRequestStateWithCommandBuffer((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<Procedural.RequestStatus>(ref status), 1, fenceBuffer);
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002770 File Offset: 0x00000970
			public static void CompleteRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles, NativeSlice<Procedural.RequestStatus> status)
			{
				bool flag = !System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = requestHandles.Length != status.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", requestHandles.Length, status.Length));
				}
				Procedural.Binding.UpdateRequestState((IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)status.GetUnsafePtr<Procedural.RequestStatus>(), requestHandles.Length);
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002800 File Offset: 0x00000A00
			public static void CompleteRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles, NativeSlice<Procedural.RequestStatus> status, CommandBuffer fenceBuffer)
			{
				bool flag = !System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = requestHandles.Length != status.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", requestHandles.Length, status.Length));
				}
				Procedural.Binding.UpdateRequestStateWithCommandBuffer((IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)status.GetUnsafePtr<Procedural.RequestStatus>(), requestHandles.Length, fenceBuffer);
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002890 File Offset: 0x00000A90
			public T GetRequestParameters()
			{
				T result = Activator.CreateInstance<T>();
				Procedural.Binding.GetRequestParameters((IntPtr)UnsafeUtility.AddressOf<Procedural.TextureStackRequestHandle<T>>(ref this), (IntPtr)UnsafeUtility.AddressOf<T>(ref result), 1);
				return result;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x000028CC File Offset: 0x00000ACC
			public static void GetRequestParameters(NativeSlice<Procedural.TextureStackRequestHandle<T>> handles, NativeSlice<T> requests)
			{
				bool flag = !System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				bool flag2 = handles.Length != requests.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Array sizes do not match ({0} handles, {1} requests)", handles.Length, requests.Length));
				}
				Procedural.Binding.GetRequestParameters((IntPtr)handles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), (IntPtr)requests.GetUnsafePtr<T>(), handles.Length);
			}

			// Token: 0x0400001F RID: 31
			internal Procedural.RequestHandlePayload payload;
		}

		// Token: 0x02000010 RID: 16
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		public struct GPUTextureStackRequestLayerParameters
		{
			// Token: 0x0600004D RID: 77 RVA: 0x0000295B File Offset: 0x00000B5B
			public int GetWidth()
			{
				return Procedural.GPUTextureStackRequestLayerParameters.GetWidth_Injected(ref this);
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00002963 File Offset: 0x00000B63
			public int GetHeight()
			{
				return Procedural.GPUTextureStackRequestLayerParameters.GetHeight_Injected(ref this);
			}

			// Token: 0x0600004F RID: 79
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetWidth_Injected(ref Procedural.GPUTextureStackRequestLayerParameters _unity_self);

			// Token: 0x06000050 RID: 80
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetHeight_Injected(ref Procedural.GPUTextureStackRequestLayerParameters _unity_self);

			// Token: 0x04000020 RID: 32
			public int destX;

			// Token: 0x04000021 RID: 33
			public int destY;

			// Token: 0x04000022 RID: 34
			public RenderTargetIdentifier dest;
		}

		// Token: 0x02000011 RID: 17
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[UsedByNativeCode]
		public struct CPUTextureStackRequestLayerParameters
		{
			// Token: 0x06000051 RID: 81 RVA: 0x0000296C File Offset: 0x00000B6C
			public NativeArray<T> GetData<T>() where T : struct
			{
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(this.data, this.dataSize, Allocator.None);
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00002994 File Offset: 0x00000B94
			public NativeArray<T> GetMipData<T>() where T : struct
			{
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(this.mipData, this.mipDataSize, Allocator.None);
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000053 RID: 83 RVA: 0x000029B9 File Offset: 0x00000BB9
			public int scanlineSize
			{
				get
				{
					return this._scanlineSize;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000054 RID: 84 RVA: 0x000029C1 File Offset: 0x00000BC1
			public int mipScanlineSize
			{
				get
				{
					return this._mipScanlineSize;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000055 RID: 85 RVA: 0x000029C9 File Offset: 0x00000BC9
			public bool requiresCachedMip
			{
				get
				{
					return this.mipDataSize != 0;
				}
			}

			// Token: 0x04000023 RID: 35
			internal int _scanlineSize;

			// Token: 0x04000024 RID: 36
			internal int dataSize;

			// Token: 0x04000025 RID: 37
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* data;

			// Token: 0x04000026 RID: 38
			internal int _mipScanlineSize;

			// Token: 0x04000027 RID: 39
			internal int mipDataSize;

			// Token: 0x04000028 RID: 40
			[NativeDisableUnsafePtrRestriction]
			internal unsafe void* mipData;
		}

		// Token: 0x02000012 RID: 18
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		[UsedByNativeCode]
		public struct GPUTextureStackRequestParameters
		{
			// Token: 0x06000056 RID: 86 RVA: 0x000029D4 File Offset: 0x00000BD4
			public Procedural.GPUTextureStackRequestLayerParameters GetLayer(int index)
			{
				Procedural.GPUTextureStackRequestLayerParameters result;
				switch (index)
				{
				case 0:
					result = this.layer0;
					break;
				case 1:
					result = this.layer1;
					break;
				case 2:
					result = this.layer2;
					break;
				case 3:
					result = this.layer3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}

			// Token: 0x04000029 RID: 41
			public int level;

			// Token: 0x0400002A RID: 42
			public int x;

			// Token: 0x0400002B RID: 43
			public int y;

			// Token: 0x0400002C RID: 44
			public int width;

			// Token: 0x0400002D RID: 45
			public int height;

			// Token: 0x0400002E RID: 46
			public int numLayers;

			// Token: 0x0400002F RID: 47
			private Procedural.GPUTextureStackRequestLayerParameters layer0;

			// Token: 0x04000030 RID: 48
			private Procedural.GPUTextureStackRequestLayerParameters layer1;

			// Token: 0x04000031 RID: 49
			private Procedural.GPUTextureStackRequestLayerParameters layer2;

			// Token: 0x04000032 RID: 50
			private Procedural.GPUTextureStackRequestLayerParameters layer3;
		}

		// Token: 0x02000013 RID: 19
		[UsedByNativeCode]
		[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
		public struct CPUTextureStackRequestParameters
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002A2C File Offset: 0x00000C2C
			public Procedural.CPUTextureStackRequestLayerParameters GetLayer(int index)
			{
				Procedural.CPUTextureStackRequestLayerParameters result;
				switch (index)
				{
				case 0:
					result = this.layer0;
					break;
				case 1:
					result = this.layer1;
					break;
				case 2:
					result = this.layer2;
					break;
				case 3:
					result = this.layer3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}

			// Token: 0x04000033 RID: 51
			public int level;

			// Token: 0x04000034 RID: 52
			public int x;

			// Token: 0x04000035 RID: 53
			public int y;

			// Token: 0x04000036 RID: 54
			public int width;

			// Token: 0x04000037 RID: 55
			public int height;

			// Token: 0x04000038 RID: 56
			public int numLayers;

			// Token: 0x04000039 RID: 57
			private Procedural.CPUTextureStackRequestLayerParameters layer0;

			// Token: 0x0400003A RID: 58
			private Procedural.CPUTextureStackRequestLayerParameters layer1;

			// Token: 0x0400003B RID: 59
			private Procedural.CPUTextureStackRequestLayerParameters layer2;

			// Token: 0x0400003C RID: 60
			private Procedural.CPUTextureStackRequestLayerParameters layer3;
		}

		// Token: 0x02000014 RID: 20
		[UsedByNativeCode]
		internal enum ProceduralTextureStackRequestStatus
		{
			// Token: 0x0400003E RID: 62
			StatusFree = 65535,
			// Token: 0x0400003F RID: 63
			StatusRequested,
			// Token: 0x04000040 RID: 64
			StatusProcessing,
			// Token: 0x04000041 RID: 65
			StatusComplete,
			// Token: 0x04000042 RID: 66
			StatusDropped
		}

		// Token: 0x02000015 RID: 21
		public enum RequestStatus
		{
			// Token: 0x04000044 RID: 68
			Dropped = 65539,
			// Token: 0x04000045 RID: 69
			Generated = 65538
		}

		// Token: 0x02000016 RID: 22
		public class TextureStackBase<T> : IDisposable where T : struct
		{
			// Token: 0x06000058 RID: 88 RVA: 0x00002A84 File Offset: 0x00000C84
			public int PopRequests(NativeSlice<Procedural.TextureStackRequestHandle<T>> requestHandles)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				return Procedural.Binding.PopRequests(this.handle, (IntPtr)requestHandles.GetUnsafePtr<Procedural.TextureStackRequestHandle<T>>(), requestHandles.Length);
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00002ADC File Offset: 0x00000CDC
			public bool IsValid()
			{
				return this.handle > 0UL;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002AF8 File Offset: 0x00000CF8
			public TextureStackBase(string _name, Procedural.CreationParameters _creationParams, bool gpuGeneration)
			{
				bool flag = !System.enabled;
				if (flag)
				{
					throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
				}
				this.name = _name;
				this.creationParams = _creationParams;
				this.creationParams.borderSize = Procedural.TextureStackBase<T>.borderSize;
				this.creationParams.gpuGeneration = (gpuGeneration ? 1 : 0);
				this.creationParams.flags = 0;
				this.creationParams.Validate();
				this.handle = Procedural.Binding.Create(this.creationParams);
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002B80 File Offset: 0x00000D80
			public void Dispose()
			{
				bool flag = this.IsValid();
				if (flag)
				{
					Procedural.Binding.Destroy(this.handle);
					this.handle = 0UL;
				}
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002BB0 File Offset: 0x00000DB0
			public void BindToMaterialPropertyBlock(MaterialPropertyBlock mpb)
			{
				bool flag = mpb == null;
				if (flag)
				{
					throw new ArgumentNullException("mbp");
				}
				bool flag2 = !this.IsValid();
				if (flag2)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindToMaterialPropertyBlock(this.handle, mpb, this.name);
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002C0C File Offset: 0x00000E0C
			public void BindToMaterial(Material mat)
			{
				bool flag = mat == null;
				if (flag)
				{
					throw new ArgumentNullException("mat");
				}
				bool flag2 = !this.IsValid();
				if (flag2)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindToMaterial(this.handle, mat, this.name);
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002C68 File Offset: 0x00000E68
			public void BindGlobally()
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.BindGlobally(this.handle, this.name);
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002CAC File Offset: 0x00000EAC
			public void RequestRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.RequestRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002CF0 File Offset: 0x00000EF0
			public void InvalidateRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.InvalidateRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002D34 File Offset: 0x00000F34
			public void EvictRegion(Rect r, int mipMap, int numMips)
			{
				bool flag = !this.IsValid();
				if (flag)
				{
					throw new InvalidOperationException("Invalid ProceduralTextureStack " + this.name);
				}
				Procedural.Binding.EvictRegion(this.handle, r, mipMap, numMips);
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00002D75 File Offset: 0x00000F75
			// Note: this type is marked as 'beforefieldinit'.
			static TextureStackBase()
			{
			}

			// Token: 0x04000046 RID: 70
			internal ulong handle;

			// Token: 0x04000047 RID: 71
			public static readonly int borderSize = 8;

			// Token: 0x04000048 RID: 72
			private string name;

			// Token: 0x04000049 RID: 73
			private Procedural.CreationParameters creationParams;

			// Token: 0x0400004A RID: 74
			public const int AllMips = 2147483647;
		}

		// Token: 0x02000017 RID: 23
		public sealed class GPUTextureStack : Procedural.TextureStackBase<Procedural.GPUTextureStackRequestParameters>
		{
			// Token: 0x06000063 RID: 99 RVA: 0x00002D7D File Offset: 0x00000F7D
			public GPUTextureStack(string _name, Procedural.CreationParameters creationParams) : base(_name, creationParams, true)
			{
			}
		}

		// Token: 0x02000018 RID: 24
		public sealed class CPUTextureStack : Procedural.TextureStackBase<Procedural.CPUTextureStackRequestParameters>
		{
			// Token: 0x06000064 RID: 100 RVA: 0x00002D8A File Offset: 0x00000F8A
			public CPUTextureStack(string _name, Procedural.CreationParameters creationParams) : base(_name, creationParams, false)
			{
			}
		}
	}
}
