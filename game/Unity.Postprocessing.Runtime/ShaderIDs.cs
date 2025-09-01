using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200006C RID: 108
	internal static class ShaderIDs
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00011D74 File Offset: 0x0000FF74
		// Note: this type is marked as 'beforefieldinit'.
		static ShaderIDs()
		{
		}

		// Token: 0x04000233 RID: 563
		internal static readonly int MainTex = Shader.PropertyToID("_MainTex");

		// Token: 0x04000234 RID: 564
		internal static readonly int Jitter = Shader.PropertyToID("_Jitter");

		// Token: 0x04000235 RID: 565
		internal static readonly int Sharpness = Shader.PropertyToID("_Sharpness");

		// Token: 0x04000236 RID: 566
		internal static readonly int FinalBlendParameters = Shader.PropertyToID("_FinalBlendParameters");

		// Token: 0x04000237 RID: 567
		internal static readonly int HistoryTex = Shader.PropertyToID("_HistoryTex");

		// Token: 0x04000238 RID: 568
		internal static readonly int SMAA_Flip = Shader.PropertyToID("_SMAA_Flip");

		// Token: 0x04000239 RID: 569
		internal static readonly int SMAA_Flop = Shader.PropertyToID("_SMAA_Flop");

		// Token: 0x0400023A RID: 570
		internal static readonly int AOParams = Shader.PropertyToID("_AOParams");

		// Token: 0x0400023B RID: 571
		internal static readonly int AOColor = Shader.PropertyToID("_AOColor");

		// Token: 0x0400023C RID: 572
		internal static readonly int OcclusionTexture1 = Shader.PropertyToID("_OcclusionTexture1");

		// Token: 0x0400023D RID: 573
		internal static readonly int OcclusionTexture2 = Shader.PropertyToID("_OcclusionTexture2");

		// Token: 0x0400023E RID: 574
		internal static readonly int SAOcclusionTexture = Shader.PropertyToID("_SAOcclusionTexture");

		// Token: 0x0400023F RID: 575
		internal static readonly int MSVOcclusionTexture = Shader.PropertyToID("_MSVOcclusionTexture");

		// Token: 0x04000240 RID: 576
		internal static readonly int DepthCopy = Shader.PropertyToID("DepthCopy");

		// Token: 0x04000241 RID: 577
		internal static readonly int LinearDepth = Shader.PropertyToID("LinearDepth");

		// Token: 0x04000242 RID: 578
		internal static readonly int LowDepth1 = Shader.PropertyToID("LowDepth1");

		// Token: 0x04000243 RID: 579
		internal static readonly int LowDepth2 = Shader.PropertyToID("LowDepth2");

		// Token: 0x04000244 RID: 580
		internal static readonly int LowDepth3 = Shader.PropertyToID("LowDepth3");

		// Token: 0x04000245 RID: 581
		internal static readonly int LowDepth4 = Shader.PropertyToID("LowDepth4");

		// Token: 0x04000246 RID: 582
		internal static readonly int TiledDepth1 = Shader.PropertyToID("TiledDepth1");

		// Token: 0x04000247 RID: 583
		internal static readonly int TiledDepth2 = Shader.PropertyToID("TiledDepth2");

		// Token: 0x04000248 RID: 584
		internal static readonly int TiledDepth3 = Shader.PropertyToID("TiledDepth3");

		// Token: 0x04000249 RID: 585
		internal static readonly int TiledDepth4 = Shader.PropertyToID("TiledDepth4");

		// Token: 0x0400024A RID: 586
		internal static readonly int Occlusion1 = Shader.PropertyToID("Occlusion1");

		// Token: 0x0400024B RID: 587
		internal static readonly int Occlusion2 = Shader.PropertyToID("Occlusion2");

		// Token: 0x0400024C RID: 588
		internal static readonly int Occlusion3 = Shader.PropertyToID("Occlusion3");

		// Token: 0x0400024D RID: 589
		internal static readonly int Occlusion4 = Shader.PropertyToID("Occlusion4");

		// Token: 0x0400024E RID: 590
		internal static readonly int Combined1 = Shader.PropertyToID("Combined1");

		// Token: 0x0400024F RID: 591
		internal static readonly int Combined2 = Shader.PropertyToID("Combined2");

		// Token: 0x04000250 RID: 592
		internal static readonly int Combined3 = Shader.PropertyToID("Combined3");

		// Token: 0x04000251 RID: 593
		internal static readonly int SSRResolveTemp = Shader.PropertyToID("_SSRResolveTemp");

		// Token: 0x04000252 RID: 594
		internal static readonly int Noise = Shader.PropertyToID("_Noise");

		// Token: 0x04000253 RID: 595
		internal static readonly int Test = Shader.PropertyToID("_Test");

		// Token: 0x04000254 RID: 596
		internal static readonly int Resolve = Shader.PropertyToID("_Resolve");

		// Token: 0x04000255 RID: 597
		internal static readonly int History = Shader.PropertyToID("_History");

		// Token: 0x04000256 RID: 598
		internal static readonly int ViewMatrix = Shader.PropertyToID("_ViewMatrix");

		// Token: 0x04000257 RID: 599
		internal static readonly int InverseViewMatrix = Shader.PropertyToID("_InverseViewMatrix");

		// Token: 0x04000258 RID: 600
		internal static readonly int ScreenSpaceProjectionMatrix = Shader.PropertyToID("_ScreenSpaceProjectionMatrix");

		// Token: 0x04000259 RID: 601
		internal static readonly int Params2 = Shader.PropertyToID("_Params2");

		// Token: 0x0400025A RID: 602
		internal static readonly int FogColor = Shader.PropertyToID("_FogColor");

		// Token: 0x0400025B RID: 603
		internal static readonly int FogParams = Shader.PropertyToID("_FogParams");

		// Token: 0x0400025C RID: 604
		internal static readonly int VelocityScale = Shader.PropertyToID("_VelocityScale");

		// Token: 0x0400025D RID: 605
		internal static readonly int MaxBlurRadius = Shader.PropertyToID("_MaxBlurRadius");

		// Token: 0x0400025E RID: 606
		internal static readonly int RcpMaxBlurRadius = Shader.PropertyToID("_RcpMaxBlurRadius");

		// Token: 0x0400025F RID: 607
		internal static readonly int VelocityTex = Shader.PropertyToID("_VelocityTex");

		// Token: 0x04000260 RID: 608
		internal static readonly int Tile2RT = Shader.PropertyToID("_Tile2RT");

		// Token: 0x04000261 RID: 609
		internal static readonly int Tile4RT = Shader.PropertyToID("_Tile4RT");

		// Token: 0x04000262 RID: 610
		internal static readonly int Tile8RT = Shader.PropertyToID("_Tile8RT");

		// Token: 0x04000263 RID: 611
		internal static readonly int TileMaxOffs = Shader.PropertyToID("_TileMaxOffs");

		// Token: 0x04000264 RID: 612
		internal static readonly int TileMaxLoop = Shader.PropertyToID("_TileMaxLoop");

		// Token: 0x04000265 RID: 613
		internal static readonly int TileVRT = Shader.PropertyToID("_TileVRT");

		// Token: 0x04000266 RID: 614
		internal static readonly int NeighborMaxTex = Shader.PropertyToID("_NeighborMaxTex");

		// Token: 0x04000267 RID: 615
		internal static readonly int LoopCount = Shader.PropertyToID("_LoopCount");

		// Token: 0x04000268 RID: 616
		internal static readonly int DepthOfFieldTemp = Shader.PropertyToID("_DepthOfFieldTemp");

		// Token: 0x04000269 RID: 617
		internal static readonly int DepthOfFieldTex = Shader.PropertyToID("_DepthOfFieldTex");

		// Token: 0x0400026A RID: 618
		internal static readonly int Distance = Shader.PropertyToID("_Distance");

		// Token: 0x0400026B RID: 619
		internal static readonly int LensCoeff = Shader.PropertyToID("_LensCoeff");

		// Token: 0x0400026C RID: 620
		internal static readonly int MaxCoC = Shader.PropertyToID("_MaxCoC");

		// Token: 0x0400026D RID: 621
		internal static readonly int RcpMaxCoC = Shader.PropertyToID("_RcpMaxCoC");

		// Token: 0x0400026E RID: 622
		internal static readonly int RcpAspect = Shader.PropertyToID("_RcpAspect");

		// Token: 0x0400026F RID: 623
		internal static readonly int CoCTex = Shader.PropertyToID("_CoCTex");

		// Token: 0x04000270 RID: 624
		internal static readonly int TaaParams = Shader.PropertyToID("_TaaParams");

		// Token: 0x04000271 RID: 625
		internal static readonly int AutoExposureTex = Shader.PropertyToID("_AutoExposureTex");

		// Token: 0x04000272 RID: 626
		internal static readonly int HistogramBuffer = Shader.PropertyToID("_HistogramBuffer");

		// Token: 0x04000273 RID: 627
		internal static readonly int Params = Shader.PropertyToID("_Params");

		// Token: 0x04000274 RID: 628
		internal static readonly int ScaleOffsetRes = Shader.PropertyToID("_ScaleOffsetRes");

		// Token: 0x04000275 RID: 629
		internal static readonly int BloomTex = Shader.PropertyToID("_BloomTex");

		// Token: 0x04000276 RID: 630
		internal static readonly int SampleScale = Shader.PropertyToID("_SampleScale");

		// Token: 0x04000277 RID: 631
		internal static readonly int Threshold = Shader.PropertyToID("_Threshold");

		// Token: 0x04000278 RID: 632
		internal static readonly int ColorIntensity = Shader.PropertyToID("_ColorIntensity");

		// Token: 0x04000279 RID: 633
		internal static readonly int Bloom_DirtTex = Shader.PropertyToID("_Bloom_DirtTex");

		// Token: 0x0400027A RID: 634
		internal static readonly int Bloom_Settings = Shader.PropertyToID("_Bloom_Settings");

		// Token: 0x0400027B RID: 635
		internal static readonly int Bloom_Color = Shader.PropertyToID("_Bloom_Color");

		// Token: 0x0400027C RID: 636
		internal static readonly int Bloom_DirtTileOffset = Shader.PropertyToID("_Bloom_DirtTileOffset");

		// Token: 0x0400027D RID: 637
		internal static readonly int ChromaticAberration_Amount = Shader.PropertyToID("_ChromaticAberration_Amount");

		// Token: 0x0400027E RID: 638
		internal static readonly int ChromaticAberration_SpectralLut = Shader.PropertyToID("_ChromaticAberration_SpectralLut");

		// Token: 0x0400027F RID: 639
		internal static readonly int Distortion_CenterScale = Shader.PropertyToID("_Distortion_CenterScale");

		// Token: 0x04000280 RID: 640
		internal static readonly int Distortion_Amount = Shader.PropertyToID("_Distortion_Amount");

		// Token: 0x04000281 RID: 641
		internal static readonly int Lut2D = Shader.PropertyToID("_Lut2D");

		// Token: 0x04000282 RID: 642
		internal static readonly int Lut3D = Shader.PropertyToID("_Lut3D");

		// Token: 0x04000283 RID: 643
		internal static readonly int Lut3D_Params = Shader.PropertyToID("_Lut3D_Params");

		// Token: 0x04000284 RID: 644
		internal static readonly int Lut2D_Params = Shader.PropertyToID("_Lut2D_Params");

		// Token: 0x04000285 RID: 645
		internal static readonly int UserLut2D_Params = Shader.PropertyToID("_UserLut2D_Params");

		// Token: 0x04000286 RID: 646
		internal static readonly int PostExposure = Shader.PropertyToID("_PostExposure");

		// Token: 0x04000287 RID: 647
		internal static readonly int ColorBalance = Shader.PropertyToID("_ColorBalance");

		// Token: 0x04000288 RID: 648
		internal static readonly int ColorFilter = Shader.PropertyToID("_ColorFilter");

		// Token: 0x04000289 RID: 649
		internal static readonly int HueSatCon = Shader.PropertyToID("_HueSatCon");

		// Token: 0x0400028A RID: 650
		internal static readonly int Brightness = Shader.PropertyToID("_Brightness");

		// Token: 0x0400028B RID: 651
		internal static readonly int ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

		// Token: 0x0400028C RID: 652
		internal static readonly int ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

		// Token: 0x0400028D RID: 653
		internal static readonly int ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

		// Token: 0x0400028E RID: 654
		internal static readonly int Lift = Shader.PropertyToID("_Lift");

		// Token: 0x0400028F RID: 655
		internal static readonly int InvGamma = Shader.PropertyToID("_InvGamma");

		// Token: 0x04000290 RID: 656
		internal static readonly int Gain = Shader.PropertyToID("_Gain");

		// Token: 0x04000291 RID: 657
		internal static readonly int Curves = Shader.PropertyToID("_Curves");

		// Token: 0x04000292 RID: 658
		internal static readonly int CustomToneCurve = Shader.PropertyToID("_CustomToneCurve");

		// Token: 0x04000293 RID: 659
		internal static readonly int ToeSegmentA = Shader.PropertyToID("_ToeSegmentA");

		// Token: 0x04000294 RID: 660
		internal static readonly int ToeSegmentB = Shader.PropertyToID("_ToeSegmentB");

		// Token: 0x04000295 RID: 661
		internal static readonly int MidSegmentA = Shader.PropertyToID("_MidSegmentA");

		// Token: 0x04000296 RID: 662
		internal static readonly int MidSegmentB = Shader.PropertyToID("_MidSegmentB");

		// Token: 0x04000297 RID: 663
		internal static readonly int ShoSegmentA = Shader.PropertyToID("_ShoSegmentA");

		// Token: 0x04000298 RID: 664
		internal static readonly int ShoSegmentB = Shader.PropertyToID("_ShoSegmentB");

		// Token: 0x04000299 RID: 665
		internal static readonly int Vignette_Color = Shader.PropertyToID("_Vignette_Color");

		// Token: 0x0400029A RID: 666
		internal static readonly int Vignette_Center = Shader.PropertyToID("_Vignette_Center");

		// Token: 0x0400029B RID: 667
		internal static readonly int Vignette_Settings = Shader.PropertyToID("_Vignette_Settings");

		// Token: 0x0400029C RID: 668
		internal static readonly int Vignette_Mask = Shader.PropertyToID("_Vignette_Mask");

		// Token: 0x0400029D RID: 669
		internal static readonly int Vignette_Opacity = Shader.PropertyToID("_Vignette_Opacity");

		// Token: 0x0400029E RID: 670
		internal static readonly int Vignette_Mode = Shader.PropertyToID("_Vignette_Mode");

		// Token: 0x0400029F RID: 671
		internal static readonly int Grain_Params1 = Shader.PropertyToID("_Grain_Params1");

		// Token: 0x040002A0 RID: 672
		internal static readonly int Grain_Params2 = Shader.PropertyToID("_Grain_Params2");

		// Token: 0x040002A1 RID: 673
		internal static readonly int GrainTex = Shader.PropertyToID("_GrainTex");

		// Token: 0x040002A2 RID: 674
		internal static readonly int Phase = Shader.PropertyToID("_Phase");

		// Token: 0x040002A3 RID: 675
		internal static readonly int GrainNoiseParameters = Shader.PropertyToID("_NoiseParameters");

		// Token: 0x040002A4 RID: 676
		internal static readonly int LumaInAlpha = Shader.PropertyToID("_LumaInAlpha");

		// Token: 0x040002A5 RID: 677
		internal static readonly int DitheringTex = Shader.PropertyToID("_DitheringTex");

		// Token: 0x040002A6 RID: 678
		internal static readonly int Dithering_Coords = Shader.PropertyToID("_Dithering_Coords");

		// Token: 0x040002A7 RID: 679
		internal static readonly int From = Shader.PropertyToID("_From");

		// Token: 0x040002A8 RID: 680
		internal static readonly int To = Shader.PropertyToID("_To");

		// Token: 0x040002A9 RID: 681
		internal static readonly int Interp = Shader.PropertyToID("_Interp");

		// Token: 0x040002AA RID: 682
		internal static readonly int TargetColor = Shader.PropertyToID("_TargetColor");

		// Token: 0x040002AB RID: 683
		internal static readonly int HalfResFinalCopy = Shader.PropertyToID("_HalfResFinalCopy");

		// Token: 0x040002AC RID: 684
		internal static readonly int WaveformSource = Shader.PropertyToID("_WaveformSource");

		// Token: 0x040002AD RID: 685
		internal static readonly int WaveformBuffer = Shader.PropertyToID("_WaveformBuffer");

		// Token: 0x040002AE RID: 686
		internal static readonly int VectorscopeBuffer = Shader.PropertyToID("_VectorscopeBuffer");

		// Token: 0x040002AF RID: 687
		internal static readonly int RenderViewportScaleFactor = Shader.PropertyToID("_RenderViewportScaleFactor");

		// Token: 0x040002B0 RID: 688
		internal static readonly int UVTransform = Shader.PropertyToID("_UVTransform");

		// Token: 0x040002B1 RID: 689
		internal static readonly int DepthSlice = Shader.PropertyToID("_DepthSlice");

		// Token: 0x040002B2 RID: 690
		internal static readonly int UVScaleOffset = Shader.PropertyToID("_UVScaleOffset");

		// Token: 0x040002B3 RID: 691
		internal static readonly int PosScaleOffset = Shader.PropertyToID("_PosScaleOffset");
	}
}
