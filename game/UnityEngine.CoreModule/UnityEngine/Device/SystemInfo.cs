using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine.Device
{
	// Token: 0x02000453 RID: 1107
	public static class SystemInfo
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x00041515 File Offset: 0x0003F715
		public static float batteryLevel
		{
			get
			{
				return SystemInfo.batteryLevel;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0004151C File Offset: 0x0003F71C
		public static BatteryStatus batteryStatus
		{
			get
			{
				return SystemInfo.batteryStatus;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x00041523 File Offset: 0x0003F723
		public static string operatingSystem
		{
			get
			{
				return SystemInfo.operatingSystem;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0004152A File Offset: 0x0003F72A
		public static OperatingSystemFamily operatingSystemFamily
		{
			get
			{
				return SystemInfo.operatingSystemFamily;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x00041531 File Offset: 0x0003F731
		public static string processorType
		{
			get
			{
				return SystemInfo.processorType;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x00041538 File Offset: 0x0003F738
		public static int processorFrequency
		{
			get
			{
				return SystemInfo.processorFrequency;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x0004153F File Offset: 0x0003F73F
		public static int processorCount
		{
			get
			{
				return SystemInfo.processorCount;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x00041546 File Offset: 0x0003F746
		public static int systemMemorySize
		{
			get
			{
				return SystemInfo.systemMemorySize;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x0004154D File Offset: 0x0003F74D
		public static string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00041554 File Offset: 0x0003F754
		public static string deviceName
		{
			get
			{
				return SystemInfo.deviceName;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x0004155B File Offset: 0x0003F75B
		public static string deviceModel
		{
			get
			{
				return SystemInfo.deviceModel;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x00041562 File Offset: 0x0003F762
		public static bool supportsAccelerometer
		{
			get
			{
				return SystemInfo.supportsAccelerometer;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x00041569 File Offset: 0x0003F769
		public static bool supportsGyroscope
		{
			get
			{
				return SystemInfo.supportsGyroscope;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00041570 File Offset: 0x0003F770
		public static bool supportsLocationService
		{
			get
			{
				return SystemInfo.supportsLocationService;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00041577 File Offset: 0x0003F777
		public static bool supportsVibration
		{
			get
			{
				return SystemInfo.supportsVibration;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0004157E File Offset: 0x0003F77E
		public static bool supportsAudio
		{
			get
			{
				return SystemInfo.supportsAudio;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00041585 File Offset: 0x0003F785
		public static DeviceType deviceType
		{
			get
			{
				return SystemInfo.deviceType;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0004158C File Offset: 0x0003F78C
		public static int graphicsMemorySize
		{
			get
			{
				return SystemInfo.graphicsMemorySize;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00041593 File Offset: 0x0003F793
		public static string graphicsDeviceName
		{
			get
			{
				return SystemInfo.graphicsDeviceName;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0004159A File Offset: 0x0003F79A
		public static string graphicsDeviceVendor
		{
			get
			{
				return SystemInfo.graphicsDeviceVendor;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000415A1 File Offset: 0x0003F7A1
		public static int graphicsDeviceID
		{
			get
			{
				return SystemInfo.graphicsDeviceID;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000415A8 File Offset: 0x0003F7A8
		public static int graphicsDeviceVendorID
		{
			get
			{
				return SystemInfo.graphicsDeviceVendorID;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000415AF File Offset: 0x0003F7AF
		public static GraphicsDeviceType graphicsDeviceType
		{
			get
			{
				return SystemInfo.graphicsDeviceType;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000415B6 File Offset: 0x0003F7B6
		public static bool graphicsUVStartsAtTop
		{
			get
			{
				return SystemInfo.graphicsUVStartsAtTop;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000415BD File Offset: 0x0003F7BD
		public static string graphicsDeviceVersion
		{
			get
			{
				return SystemInfo.graphicsDeviceVersion;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000415C4 File Offset: 0x0003F7C4
		public static int graphicsShaderLevel
		{
			get
			{
				return SystemInfo.graphicsShaderLevel;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000415CB File Offset: 0x0003F7CB
		public static bool graphicsMultiThreaded
		{
			get
			{
				return SystemInfo.graphicsMultiThreaded;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000415D2 File Offset: 0x0003F7D2
		public static RenderingThreadingMode renderingThreadingMode
		{
			get
			{
				return SystemInfo.renderingThreadingMode;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000415D9 File Offset: 0x0003F7D9
		public static bool hasHiddenSurfaceRemovalOnGPU
		{
			get
			{
				return SystemInfo.hasHiddenSurfaceRemovalOnGPU;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000415E0 File Offset: 0x0003F7E0
		public static bool hasDynamicUniformArrayIndexingInFragmentShaders
		{
			get
			{
				return SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x000415E7 File Offset: 0x0003F7E7
		public static bool supportsShadows
		{
			get
			{
				return SystemInfo.supportsShadows;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000415EE File Offset: 0x0003F7EE
		public static bool supportsRawShadowDepthSampling
		{
			get
			{
				return SystemInfo.supportsRawShadowDepthSampling;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x000415F5 File Offset: 0x0003F7F5
		public static bool supportsMotionVectors
		{
			get
			{
				return SystemInfo.supportsMotionVectors;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000415FC File Offset: 0x0003F7FC
		public static bool supports3DTextures
		{
			get
			{
				return SystemInfo.supports3DTextures;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00041603 File Offset: 0x0003F803
		public static bool supportsCompressed3DTextures
		{
			get
			{
				return SystemInfo.supportsCompressed3DTextures;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x0004160A File Offset: 0x0003F80A
		public static bool supports2DArrayTextures
		{
			get
			{
				return SystemInfo.supports2DArrayTextures;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00041611 File Offset: 0x0003F811
		public static bool supports3DRenderTextures
		{
			get
			{
				return SystemInfo.supports3DRenderTextures;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x00041618 File Offset: 0x0003F818
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return SystemInfo.supportsCubemapArrayTextures;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x0004161F File Offset: 0x0003F81F
		public static bool supportsAnisotropicFilter
		{
			get
			{
				return SystemInfo.supportsAnisotropicFilter;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x00041626 File Offset: 0x0003F826
		public static CopyTextureSupport copyTextureSupport
		{
			get
			{
				return SystemInfo.copyTextureSupport;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x0004162D File Offset: 0x0003F82D
		public static bool supportsComputeShaders
		{
			get
			{
				return SystemInfo.supportsComputeShaders;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x00041634 File Offset: 0x0003F834
		public static bool supportsGeometryShaders
		{
			get
			{
				return SystemInfo.supportsGeometryShaders;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x0004163B File Offset: 0x0003F83B
		public static bool supportsTessellationShaders
		{
			get
			{
				return SystemInfo.supportsTessellationShaders;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x00041642 File Offset: 0x0003F842
		public static bool supportsRenderTargetArrayIndexFromVertexShader
		{
			get
			{
				return SystemInfo.supportsRenderTargetArrayIndexFromVertexShader;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x00041649 File Offset: 0x0003F849
		public static bool supportsInstancing
		{
			get
			{
				return SystemInfo.supportsInstancing;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x00041650 File Offset: 0x0003F850
		public static bool supportsHardwareQuadTopology
		{
			get
			{
				return SystemInfo.supportsHardwareQuadTopology;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x00041657 File Offset: 0x0003F857
		public static bool supports32bitsIndexBuffer
		{
			get
			{
				return SystemInfo.supports32bitsIndexBuffer;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x0004165E File Offset: 0x0003F85E
		public static bool supportsSparseTextures
		{
			get
			{
				return SystemInfo.supportsSparseTextures;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x00041665 File Offset: 0x0003F865
		public static int supportedRenderTargetCount
		{
			get
			{
				return SystemInfo.supportedRenderTargetCount;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x0004166C File Offset: 0x0003F86C
		public static bool supportsSeparatedRenderTargetsBlend
		{
			get
			{
				return SystemInfo.supportsSeparatedRenderTargetsBlend;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x00041673 File Offset: 0x0003F873
		public static int supportedRandomWriteTargetCount
		{
			get
			{
				return SystemInfo.supportedRandomWriteTargetCount;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x0004167A File Offset: 0x0003F87A
		public static int supportsMultisampledTextures
		{
			get
			{
				return SystemInfo.supportsMultisampledTextures;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x00041681 File Offset: 0x0003F881
		public static bool supportsMultisampled2DArrayTextures
		{
			get
			{
				return SystemInfo.supportsMultisampled2DArrayTextures;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x00041688 File Offset: 0x0003F888
		public static bool supportsMultisampleAutoResolve
		{
			get
			{
				return SystemInfo.supportsMultisampleAutoResolve;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0004168F File Offset: 0x0003F88F
		public static int supportsTextureWrapMirrorOnce
		{
			get
			{
				return SystemInfo.supportsTextureWrapMirrorOnce;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x00041696 File Offset: 0x0003F896
		public static bool usesReversedZBuffer
		{
			get
			{
				return SystemInfo.usesReversedZBuffer;
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000416A0 File Offset: 0x0003F8A0
		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRenderTextureFormat(format);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000416B8 File Offset: 0x0003F8B8
		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsBlendingOnRenderTextureFormat(format);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000416D0 File Offset: 0x0003F8D0
		public static bool SupportsTextureFormat(TextureFormat format)
		{
			return SystemInfo.SupportsTextureFormat(format);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000416E8 File Offset: 0x0003F8E8
		public static bool SupportsVertexAttributeFormat(VertexAttributeFormat format, int dimension)
		{
			return SystemInfo.SupportsVertexAttributeFormat(format, dimension);
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00041701 File Offset: 0x0003F901
		public static NPOTSupport npotSupport
		{
			get
			{
				return SystemInfo.npotSupport;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00041708 File Offset: 0x0003F908
		public static int maxTextureSize
		{
			get
			{
				return SystemInfo.maxTextureSize;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0004170F File Offset: 0x0003F90F
		public static int maxTexture3DSize
		{
			get
			{
				return SystemInfo.maxTexture3DSize;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x00041716 File Offset: 0x0003F916
		public static int maxTextureArraySlices
		{
			get
			{
				return SystemInfo.maxTextureArraySlices;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x0004171D File Offset: 0x0003F91D
		public static int maxCubemapSize
		{
			get
			{
				return SystemInfo.maxCubemapSize;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x00041724 File Offset: 0x0003F924
		public static int maxAnisotropyLevel
		{
			get
			{
				return SystemInfo.maxAnisotropyLevel;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x0004172B File Offset: 0x0003F92B
		public static int maxComputeBufferInputsVertex
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsVertex;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x00041732 File Offset: 0x0003F932
		public static int maxComputeBufferInputsFragment
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsFragment;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00041739 File Offset: 0x0003F939
		public static int maxComputeBufferInputsGeometry
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsGeometry;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x00041740 File Offset: 0x0003F940
		public static int maxComputeBufferInputsDomain
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsDomain;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x00041747 File Offset: 0x0003F947
		public static int maxComputeBufferInputsHull
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsHull;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x0004174E File Offset: 0x0003F94E
		public static int maxComputeBufferInputsCompute
		{
			get
			{
				return SystemInfo.maxComputeBufferInputsCompute;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00041755 File Offset: 0x0003F955
		public static int maxComputeWorkGroupSize
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSize;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x0004175C File Offset: 0x0003F95C
		public static int maxComputeWorkGroupSizeX
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeX;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00041763 File Offset: 0x0003F963
		public static int maxComputeWorkGroupSizeY
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeY;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x0004176A File Offset: 0x0003F96A
		public static int maxComputeWorkGroupSizeZ
		{
			get
			{
				return SystemInfo.maxComputeWorkGroupSizeZ;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00041771 File Offset: 0x0003F971
		public static int computeSubGroupSize
		{
			get
			{
				return SystemInfo.computeSubGroupSize;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x00041778 File Offset: 0x0003F978
		public static bool supportsAsyncCompute
		{
			get
			{
				return SystemInfo.supportsAsyncCompute;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x0004177F File Offset: 0x0003F97F
		public static bool supportsGpuRecorder
		{
			get
			{
				return SystemInfo.supportsGpuRecorder;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00041786 File Offset: 0x0003F986
		public static bool supportsGraphicsFence
		{
			get
			{
				return SystemInfo.supportsGraphicsFence;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x0004178D File Offset: 0x0003F98D
		public static bool supportsAsyncGPUReadback
		{
			get
			{
				return SystemInfo.supportsAsyncGPUReadback;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x00041794 File Offset: 0x0003F994
		public static bool supportsRayTracing
		{
			get
			{
				return SystemInfo.supportsRayTracing;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x0004179B File Offset: 0x0003F99B
		public static bool supportsSetConstantBuffer
		{
			get
			{
				return SystemInfo.supportsSetConstantBuffer;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x000417A2 File Offset: 0x0003F9A2
		public static int constantBufferOffsetAlignment
		{
			get
			{
				return SystemInfo.constantBufferOffsetAlignment;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x000417A9 File Offset: 0x0003F9A9
		public static long maxGraphicsBufferSize
		{
			get
			{
				return SystemInfo.maxGraphicsBufferSize;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000417B0 File Offset: 0x0003F9B0
		public static bool hasMipMaxLevel
		{
			get
			{
				return SystemInfo.hasMipMaxLevel;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000417B7 File Offset: 0x0003F9B7
		public static bool supportsMipStreaming
		{
			get
			{
				return SystemInfo.supportsMipStreaming;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000417BE File Offset: 0x0003F9BE
		public static bool usesLoadStoreActions
		{
			get
			{
				return SystemInfo.usesLoadStoreActions;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000417C5 File Offset: 0x0003F9C5
		public static HDRDisplaySupportFlags hdrDisplaySupportFlags
		{
			get
			{
				return SystemInfo.hdrDisplaySupportFlags;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000417CC File Offset: 0x0003F9CC
		public static bool supportsConservativeRaster
		{
			get
			{
				return SystemInfo.supportsConservativeRaster;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000417D3 File Offset: 0x0003F9D3
		public static bool supportsMultiview
		{
			get
			{
				return SystemInfo.supportsMultiview;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000417DA File Offset: 0x0003F9DA
		public static bool supportsStoreAndResolveAction
		{
			get
			{
				return SystemInfo.supportsStoreAndResolveAction;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000417E1 File Offset: 0x0003F9E1
		public static bool supportsMultisampleResolveDepth
		{
			get
			{
				return SystemInfo.supportsMultisampleResolveDepth;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000417E8 File Offset: 0x0003F9E8
		public static bool IsFormatSupported(GraphicsFormat format, FormatUsage usage)
		{
			return SystemInfo.IsFormatSupported(format, usage);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00041804 File Offset: 0x0003FA04
		public static GraphicsFormat GetCompatibleFormat(GraphicsFormat format, FormatUsage usage)
		{
			return SystemInfo.GetCompatibleFormat(format, usage);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00041820 File Offset: 0x0003FA20
		public static GraphicsFormat GetGraphicsFormat(DefaultFormat format)
		{
			return SystemInfo.GetGraphicsFormat(format);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00041838 File Offset: 0x0003FA38
		public static int GetRenderTextureSupportedMSAASampleCount(RenderTextureDescriptor desc)
		{
			return SystemInfo.GetRenderTextureSupportedMSAASampleCount(desc);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00041850 File Offset: 0x0003FA50
		public static bool SupportsRandomWriteOnRenderTextureFormat(RenderTextureFormat format)
		{
			return SystemInfo.SupportsRandomWriteOnRenderTextureFormat(format);
		}

		// Token: 0x04000E3C RID: 3644
		public const string unsupportedIdentifier = "n/a";
	}
}
