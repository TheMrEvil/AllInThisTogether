using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200013C RID: 316
	[StaticAccessor("GetQualitySettings()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	[NativeHeader("Runtime/Graphics/QualitySettings.h")]
	public sealed class QualitySettings : Object
	{
		// Token: 0x06000A12 RID: 2578 RVA: 0x0000F234 File Offset: 0x0000D434
		public static void IncreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() + 1, applyExpensiveChanges);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0000F245 File Offset: 0x0000D445
		public static void DecreaseLevel([DefaultValue("false")] bool applyExpensiveChanges)
		{
			QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel() - 1, applyExpensiveChanges);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0000F256 File Offset: 0x0000D456
		public static void SetQualityLevel(int index)
		{
			QualitySettings.SetQualityLevel(index, true);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0000F261 File Offset: 0x0000D461
		public static void IncreaseLevel()
		{
			QualitySettings.IncreaseLevel(false);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0000F26B File Offset: 0x0000D46B
		public static void DecreaseLevel()
		{
			QualitySettings.DecreaseLevel(false);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0000F278 File Offset: 0x0000D478
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0000F256 File Offset: 0x0000D456
		[Obsolete("Use GetQualityLevel and SetQualityLevel", false)]
		public static QualityLevel currentLevel
		{
			get
			{
				return (QualityLevel)QualitySettings.GetQualityLevel();
			}
			set
			{
				QualitySettings.SetQualityLevel((int)value, true);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000E886 File Offset: 0x0000CA86
		private QualitySettings()
		{
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A1A RID: 2586
		// (set) Token: 0x06000A1B RID: 2587
		public static extern int pixelLightCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A1C RID: 2588
		// (set) Token: 0x06000A1D RID: 2589
		[NativeProperty("ShadowQuality")]
		public static extern ShadowQuality shadows { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A1E RID: 2590
		// (set) Token: 0x06000A1F RID: 2591
		public static extern ShadowProjection shadowProjection { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A20 RID: 2592
		// (set) Token: 0x06000A21 RID: 2593
		public static extern int shadowCascades { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A22 RID: 2594
		// (set) Token: 0x06000A23 RID: 2595
		public static extern float shadowDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A24 RID: 2596
		// (set) Token: 0x06000A25 RID: 2597
		[NativeProperty("ShadowResolution")]
		public static extern ShadowResolution shadowResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A26 RID: 2598
		// (set) Token: 0x06000A27 RID: 2599
		[NativeProperty("ShadowmaskMode")]
		public static extern ShadowmaskMode shadowmaskMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A28 RID: 2600
		// (set) Token: 0x06000A29 RID: 2601
		public static extern float shadowNearPlaneOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A2A RID: 2602
		// (set) Token: 0x06000A2B RID: 2603
		public static extern float shadowCascade2Split { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0000F290 File Offset: 0x0000D490
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0000F2A5 File Offset: 0x0000D4A5
		public static Vector3 shadowCascade4Split
		{
			get
			{
				Vector3 result;
				QualitySettings.get_shadowCascade4Split_Injected(out result);
				return result;
			}
			set
			{
				QualitySettings.set_shadowCascade4Split_Injected(ref value);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A2E RID: 2606
		// (set) Token: 0x06000A2F RID: 2607
		[NativeProperty("LODBias")]
		public static extern float lodBias { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A30 RID: 2608
		// (set) Token: 0x06000A31 RID: 2609
		[NativeProperty("AnisotropicTextures")]
		public static extern AnisotropicFiltering anisotropicFiltering { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A32 RID: 2610
		// (set) Token: 0x06000A33 RID: 2611
		public static extern int masterTextureLimit { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A34 RID: 2612
		// (set) Token: 0x06000A35 RID: 2613
		public static extern int maximumLODLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A36 RID: 2614
		// (set) Token: 0x06000A37 RID: 2615
		public static extern int particleRaycastBudget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A38 RID: 2616
		// (set) Token: 0x06000A39 RID: 2617
		public static extern bool softParticles { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A3A RID: 2618
		// (set) Token: 0x06000A3B RID: 2619
		public static extern bool softVegetation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A3C RID: 2620
		// (set) Token: 0x06000A3D RID: 2621
		public static extern int vSyncCount { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A3E RID: 2622
		// (set) Token: 0x06000A3F RID: 2623
		public static extern int realtimeGICPUUsage { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A40 RID: 2624
		// (set) Token: 0x06000A41 RID: 2625
		public static extern int antiAliasing { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A42 RID: 2626
		// (set) Token: 0x06000A43 RID: 2627
		public static extern int asyncUploadTimeSlice { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A44 RID: 2628
		// (set) Token: 0x06000A45 RID: 2629
		public static extern int asyncUploadBufferSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A46 RID: 2630
		// (set) Token: 0x06000A47 RID: 2631
		public static extern bool asyncUploadPersistentBuffer { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A48 RID: 2632
		[NativeName("SetLODSettings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLODSettings(float lodBias, int maximumLODLevel, bool setDirty = true);

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A49 RID: 2633
		// (set) Token: 0x06000A4A RID: 2634
		public static extern bool realtimeReflectionProbes { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A4B RID: 2635
		// (set) Token: 0x06000A4C RID: 2636
		public static extern bool billboardsFaceCameraPosition { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A4D RID: 2637
		// (set) Token: 0x06000A4E RID: 2638
		public static extern float resolutionScalingFixedDPIFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A4F RID: 2639
		// (set) Token: 0x06000A50 RID: 2640
		[NativeName("RenderPipeline")]
		private static extern ScriptableObject INTERNAL_renderPipeline { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		public static RenderPipelineAsset renderPipeline
		{
			get
			{
				return QualitySettings.INTERNAL_renderPipeline as RenderPipelineAsset;
			}
			set
			{
				QualitySettings.INTERNAL_renderPipeline = value;
			}
		}

		// Token: 0x06000A53 RID: 2643
		[NativeName("GetRenderPipelineAssetAt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ScriptableObject InternalGetRenderPipelineAssetAt(int index);

		// Token: 0x06000A54 RID: 2644 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		public static RenderPipelineAsset GetRenderPipelineAssetAt(int index)
		{
			bool flag = index < 0 || index >= QualitySettings.names.Length;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("{0} is out of range [0..{1}[", "index", QualitySettings.names.Length));
			}
			return QualitySettings.InternalGetRenderPipelineAssetAt(index) as RenderPipelineAsset;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A55 RID: 2645
		// (set) Token: 0x06000A56 RID: 2646
		[Obsolete("blendWeights is obsolete. Use skinWeights instead (UnityUpgradable) -> skinWeights", true)]
		public static extern BlendWeights blendWeights { [NativeName("GetSkinWeights")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetSkinWeights")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000A57 RID: 2647
		// (set) Token: 0x06000A58 RID: 2648
		public static extern SkinWeights skinWeights { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000A59 RID: 2649
		// (set) Token: 0x06000A5A RID: 2650
		public static extern bool streamingMipmapsActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A5B RID: 2651
		// (set) Token: 0x06000A5C RID: 2652
		public static extern float streamingMipmapsMemoryBudget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A5D RID: 2653
		// (set) Token: 0x06000A5E RID: 2654
		public static extern int streamingMipmapsRenderersPerFrame { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A5F RID: 2655
		// (set) Token: 0x06000A60 RID: 2656
		public static extern int streamingMipmapsMaxLevelReduction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A61 RID: 2657
		// (set) Token: 0x06000A62 RID: 2658
		public static extern bool streamingMipmapsAddAllCameras { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A63 RID: 2659
		// (set) Token: 0x06000A64 RID: 2660
		public static extern int streamingMipmapsMaxFileIORequests { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A65 RID: 2661
		// (set) Token: 0x06000A66 RID: 2662
		[StaticAccessor("QualitySettingsScripting", StaticAccessorType.DoubleColon)]
		public static extern int maxQueuedFrames { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A67 RID: 2663
		[NativeName("GetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetQualityLevel();

		// Token: 0x06000A68 RID: 2664
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object GetQualitySettings();

		// Token: 0x06000A69 RID: 2665
		[NativeName("SetCurrentIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetQualityLevel(int index, [DefaultValue("true")] bool applyExpensiveChanges);

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A6A RID: 2666
		[NativeProperty("QualitySettingsNames")]
		public static extern string[] names { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A6B RID: 2667
		public static extern ColorSpace desiredColorSpace { [NativeName("GetColorSpace")] [StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A6C RID: 2668
		public static extern ColorSpace activeColorSpace { [NativeName("GetColorSpace")] [StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000A6D RID: 2669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_shadowCascade4Split_Injected(out Vector3 ret);

		// Token: 0x06000A6E RID: 2670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowCascade4Split_Injected(ref Vector3 value);
	}
}
