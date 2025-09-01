using System;

namespace MK.Toon
{
	// Token: 0x0200003C RID: 60
	public static class Uniforms
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003B94 File Offset: 0x00001D94
		// Note: this type is marked as 'beforefieldinit'.
		static Uniforms()
		{
		}

		// Token: 0x0400018F RID: 399
		public static readonly Uniform workflow = new Uniform("_Workflow");

		// Token: 0x04000190 RID: 400
		public static readonly Uniform renderFace = new Uniform("_RenderFace");

		// Token: 0x04000191 RID: 401
		public static readonly Uniform surface = new Uniform("_Surface");

		// Token: 0x04000192 RID: 402
		public static readonly Uniform zWrite = new Uniform("_ZWrite");

		// Token: 0x04000193 RID: 403
		public static readonly Uniform zTest = new Uniform("_ZTest");

		// Token: 0x04000194 RID: 404
		public static readonly Uniform blendSrc = new Uniform("_BlendSrc");

		// Token: 0x04000195 RID: 405
		public static readonly Uniform blendDst = new Uniform("_BlendDst");

		// Token: 0x04000196 RID: 406
		public static readonly Uniform blendSrcAlpha = new Uniform("_BlendSrcAlpha");

		// Token: 0x04000197 RID: 407
		public static readonly Uniform blendDstAlpha = new Uniform("_BlendDstAlpha");

		// Token: 0x04000198 RID: 408
		public static readonly Uniform blend = new Uniform("_Blend");

		// Token: 0x04000199 RID: 409
		public static readonly Uniform alphaClipping = new Uniform("_AlphaClipping");

		// Token: 0x0400019A RID: 410
		public static readonly Uniform albedoColor = new Uniform("_AlbedoColor");

		// Token: 0x0400019B RID: 411
		public static readonly Uniform alphaCutoff = new Uniform("_AlphaCutoff");

		// Token: 0x0400019C RID: 412
		public static readonly Uniform albedoMap = new Uniform("_AlbedoMap");

		// Token: 0x0400019D RID: 413
		public static readonly Uniform specularColor = new Uniform("_SpecularColor");

		// Token: 0x0400019E RID: 414
		public static readonly Uniform metallic = new Uniform("_Metallic");

		// Token: 0x0400019F RID: 415
		public static readonly Uniform smoothness = new Uniform("_Smoothness");

		// Token: 0x040001A0 RID: 416
		public static readonly Uniform roughness = new Uniform("_Roughness");

		// Token: 0x040001A1 RID: 417
		public static readonly Uniform specularMap = new Uniform("_SpecularMap");

		// Token: 0x040001A2 RID: 418
		public static readonly Uniform roughnessMap = new Uniform("_RoughnessMap");

		// Token: 0x040001A3 RID: 419
		public static readonly Uniform metallicMap = new Uniform("_MetallicMap");

		// Token: 0x040001A4 RID: 420
		public static readonly Uniform normalMapIntensity = new Uniform("_NormalMapIntensity");

		// Token: 0x040001A5 RID: 421
		public static readonly Uniform normalMap = new Uniform("_NormalMap");

		// Token: 0x040001A6 RID: 422
		public static readonly Uniform parallax = new Uniform("_Parallax");

		// Token: 0x040001A7 RID: 423
		public static readonly Uniform heightMap = new Uniform("_HeightMap");

		// Token: 0x040001A8 RID: 424
		public static readonly Uniform lightTransmission = new Uniform("_LightTransmission");

		// Token: 0x040001A9 RID: 425
		public static readonly Uniform lightTransmissionDistortion = new Uniform("_LightTransmissionDistortion");

		// Token: 0x040001AA RID: 426
		public static readonly Uniform lightTransmissionColor = new Uniform("_LightTransmissionColor");

		// Token: 0x040001AB RID: 427
		public static readonly Uniform thicknessMap = new Uniform("_ThicknessMap");

		// Token: 0x040001AC RID: 428
		public static readonly Uniform occlusionMapIntensity = new Uniform("_OcclusionMapIntensity");

		// Token: 0x040001AD RID: 429
		public static readonly Uniform occlusionMap = new Uniform("_OcclusionMap");

		// Token: 0x040001AE RID: 430
		public static readonly Uniform emissionColor = new Uniform("_EmissionColor");

		// Token: 0x040001AF RID: 431
		public static readonly Uniform emissionMap = new Uniform("_EmissionMap");

		// Token: 0x040001B0 RID: 432
		public static readonly Uniform detailBlend = new Uniform("_DetailBlend");

		// Token: 0x040001B1 RID: 433
		public static readonly Uniform detailColor = new Uniform("_DetailColor");

		// Token: 0x040001B2 RID: 434
		public static readonly Uniform detailMix = new Uniform("_DetailMix");

		// Token: 0x040001B3 RID: 435
		public static readonly Uniform detailMap = new Uniform("_DetailMap");

		// Token: 0x040001B4 RID: 436
		public static readonly Uniform detailNormalMapIntensity = new Uniform("_DetailNormalMapIntensity");

		// Token: 0x040001B5 RID: 437
		public static readonly Uniform detailNormalMap = new Uniform("_DetailNormalMap");

		// Token: 0x040001B6 RID: 438
		public static readonly Uniform receiveShadows = new Uniform("_ReceiveShadows");

		// Token: 0x040001B7 RID: 439
		public static readonly Uniform wrappedLighting = new Uniform("_WrappedLighting");

		// Token: 0x040001B8 RID: 440
		public static readonly Uniform diffuseSmoothness = new Uniform("_DiffuseSmoothness");

		// Token: 0x040001B9 RID: 441
		public static readonly Uniform diffuseThresholdOffset = new Uniform("_DiffuseThresholdOffset");

		// Token: 0x040001BA RID: 442
		public static readonly Uniform specularSmoothness = new Uniform("_SpecularSmoothness");

		// Token: 0x040001BB RID: 443
		public static readonly Uniform specularThresholdOffset = new Uniform("_SpecularThresholdOffset");

		// Token: 0x040001BC RID: 444
		public static readonly Uniform rimSmoothness = new Uniform("_RimSmoothness");

		// Token: 0x040001BD RID: 445
		public static readonly Uniform rimThresholdOffset = new Uniform("_RimThresholdOffset");

		// Token: 0x040001BE RID: 446
		public static readonly Uniform lightTransmissionSmoothness = new Uniform("_LightTransmissionSmoothness");

		// Token: 0x040001BF RID: 447
		public static readonly Uniform lightTransmissionThresholdOffset = new Uniform("_LightTransmissionThresholdOffset");

		// Token: 0x040001C0 RID: 448
		public static readonly Uniform light = new Uniform("_Light");

		// Token: 0x040001C1 RID: 449
		public static readonly Uniform diffuseRamp = new Uniform("_DiffuseRamp");

		// Token: 0x040001C2 RID: 450
		public static readonly Uniform specularRamp = new Uniform("_SpecularRamp");

		// Token: 0x040001C3 RID: 451
		public static readonly Uniform rimRamp = new Uniform("_RimRamp");

		// Token: 0x040001C4 RID: 452
		public static readonly Uniform lightTransmissionRamp = new Uniform("_LightTransmissionRamp");

		// Token: 0x040001C5 RID: 453
		public static readonly Uniform lightBands = new Uniform("_LightBands");

		// Token: 0x040001C6 RID: 454
		public static readonly Uniform lightBandsScale = new Uniform("_LightBandsScale");

		// Token: 0x040001C7 RID: 455
		public static readonly Uniform lightThreshold = new Uniform("_LightThreshold");

		// Token: 0x040001C8 RID: 456
		public static readonly Uniform thresholdMap = new Uniform("_ThresholdMap");

		// Token: 0x040001C9 RID: 457
		public static readonly Uniform thresholdMapScale = new Uniform("_ThresholdMapScale");

		// Token: 0x040001CA RID: 458
		public static readonly Uniform goochRampIntensity = new Uniform("_GoochRampIntensity");

		// Token: 0x040001CB RID: 459
		public static readonly Uniform goochRamp = new Uniform("_GoochRamp");

		// Token: 0x040001CC RID: 460
		public static readonly Uniform goochBrightColor = new Uniform("_GoochBrightColor");

		// Token: 0x040001CD RID: 461
		public static readonly Uniform goochBrightMap = new Uniform("_GoochBrightMap");

		// Token: 0x040001CE RID: 462
		public static readonly Uniform goochDarkColor = new Uniform("_GoochDarkColor");

		// Token: 0x040001CF RID: 463
		public static readonly Uniform goochDarkMap = new Uniform("_GoochDarkMap");

		// Token: 0x040001D0 RID: 464
		public static readonly Uniform colorGrading = new Uniform("_ColorGrading");

		// Token: 0x040001D1 RID: 465
		public static readonly Uniform contrast = new Uniform("_Contrast");

		// Token: 0x040001D2 RID: 466
		public static readonly Uniform saturation = new Uniform("_Saturation");

		// Token: 0x040001D3 RID: 467
		public static readonly Uniform brightness = new Uniform("_Brightness");

		// Token: 0x040001D4 RID: 468
		public static readonly Uniform iridescence = new Uniform("_Iridescence");

		// Token: 0x040001D5 RID: 469
		public static readonly Uniform iridescenceRamp = new Uniform("_IridescenceRamp");

		// Token: 0x040001D6 RID: 470
		public static readonly Uniform iridescenceSize = new Uniform("_IridescenceSize");

		// Token: 0x040001D7 RID: 471
		public static readonly Uniform iridescenceThresholdOffset = new Uniform("_IridescenceThresholdOffset");

		// Token: 0x040001D8 RID: 472
		public static readonly Uniform iridescenceSmoothness = new Uniform("_IridescenceSmoothness");

		// Token: 0x040001D9 RID: 473
		public static readonly Uniform iridescenceColor = new Uniform("_IridescenceColor");

		// Token: 0x040001DA RID: 474
		public static readonly Uniform rim = new Uniform("_Rim");

		// Token: 0x040001DB RID: 475
		public static readonly Uniform rimColor = new Uniform("_RimColor");

		// Token: 0x040001DC RID: 476
		public static readonly Uniform rimBrightColor = new Uniform("_RimBrightColor");

		// Token: 0x040001DD RID: 477
		public static readonly Uniform rimDarkColor = new Uniform("_RimDarkColor");

		// Token: 0x040001DE RID: 478
		public static readonly Uniform rimSize = new Uniform("_RimSize");

		// Token: 0x040001DF RID: 479
		public static readonly Uniform vertexAnimation = new Uniform("_VertexAnimation");

		// Token: 0x040001E0 RID: 480
		public static readonly Uniform vertexAnimationStutter = new Uniform("_VertexAnimationStutter");

		// Token: 0x040001E1 RID: 481
		public static readonly Uniform vertexAnimationMap = new Uniform("_VertexAnimationMap");

		// Token: 0x040001E2 RID: 482
		public static readonly Uniform vertexAnimationIntensity = new Uniform("_VertexAnimationIntensity");

		// Token: 0x040001E3 RID: 483
		public static readonly Uniform vertexAnimationFrequency = new Uniform("_VertexAnimationFrequency");

		// Token: 0x040001E4 RID: 484
		public static readonly Uniform dissolve = new Uniform("_Dissolve");

		// Token: 0x040001E5 RID: 485
		public static readonly Uniform dissolveMap = new Uniform("_DissolveMap");

		// Token: 0x040001E6 RID: 486
		public static readonly Uniform dissolveMapScale = new Uniform("_DissolveMapScale");

		// Token: 0x040001E7 RID: 487
		public static readonly Uniform dissolveAmount = new Uniform("_DissolveAmount");

		// Token: 0x040001E8 RID: 488
		public static readonly Uniform dissolveBorderSize = new Uniform("_DissolveBorderSize");

		// Token: 0x040001E9 RID: 489
		public static readonly Uniform dissolveBorderRamp = new Uniform("_DissolveBorderRamp");

		// Token: 0x040001EA RID: 490
		public static readonly Uniform dissolveBorderColor = new Uniform("_DissolveBorderColor");

		// Token: 0x040001EB RID: 491
		public static readonly Uniform artistic = new Uniform("_Artistic");

		// Token: 0x040001EC RID: 492
		public static readonly Uniform artisticProjection = new Uniform("_ArtisticProjection");

		// Token: 0x040001ED RID: 493
		public static readonly Uniform artisticFrequency = new Uniform("_ArtisticFrequency");

		// Token: 0x040001EE RID: 494
		public static readonly Uniform drawnMapScale = new Uniform("_DrawnMapScale");

		// Token: 0x040001EF RID: 495
		public static readonly Uniform drawnMap = new Uniform("_DrawnMap");

		// Token: 0x040001F0 RID: 496
		public static readonly Uniform hatchingMapScale = new Uniform("_HatchingMapScale");

		// Token: 0x040001F1 RID: 497
		public static readonly Uniform hatchingBrightMap = new Uniform("_HatchingBrightMap");

		// Token: 0x040001F2 RID: 498
		public static readonly Uniform hatchingDarkMap = new Uniform("_HatchingDarkMap");

		// Token: 0x040001F3 RID: 499
		public static readonly Uniform drawnClampMin = new Uniform("_DrawnClampMin");

		// Token: 0x040001F4 RID: 500
		public static readonly Uniform drawnClampMax = new Uniform("_DrawnClampMax");

		// Token: 0x040001F5 RID: 501
		public static readonly Uniform sketchMapScale = new Uniform("_SketchMapScale");

		// Token: 0x040001F6 RID: 502
		public static readonly Uniform sketchMap = new Uniform("_SketchMap");

		// Token: 0x040001F7 RID: 503
		public static readonly Uniform diffuse = new Uniform("_Diffuse");

		// Token: 0x040001F8 RID: 504
		public static readonly Uniform specular = new Uniform("_Specular");

		// Token: 0x040001F9 RID: 505
		public static readonly Uniform specularIntensity = new Uniform("_SpecularIntensity");

		// Token: 0x040001FA RID: 506
		public static readonly Uniform anisotropy = new Uniform("_Anisotropy");

		// Token: 0x040001FB RID: 507
		public static readonly Uniform lightTransmissionIntensity = new Uniform("_LightTransmissionIntensity");

		// Token: 0x040001FC RID: 508
		public static readonly Uniform environmentReflections = new Uniform("_EnvironmentReflections");

		// Token: 0x040001FD RID: 509
		public static readonly Uniform fresnelHighlights = new Uniform("_FresnelHighlights");

		// Token: 0x040001FE RID: 510
		public static readonly Uniform IndirectFade = new Uniform("_IndirectFade");

		// Token: 0x040001FF RID: 511
		public static readonly Uniform stencil = new Uniform("_Stencil");

		// Token: 0x04000200 RID: 512
		public static readonly Uniform renderPriority = new Uniform("_RenderPriority");

		// Token: 0x04000201 RID: 513
		public static readonly Uniform stencilRef = new Uniform("_StencilRef");

		// Token: 0x04000202 RID: 514
		public static readonly Uniform stencilReadMask = new Uniform("_StencilReadMask");

		// Token: 0x04000203 RID: 515
		public static readonly Uniform stencilWriteMask = new Uniform("_StencilWriteMask");

		// Token: 0x04000204 RID: 516
		public static readonly Uniform stencilComp = new Uniform("_StencilComp");

		// Token: 0x04000205 RID: 517
		public static readonly Uniform stencilPass = new Uniform("_StencilPass");

		// Token: 0x04000206 RID: 518
		public static readonly Uniform stencilFail = new Uniform("_StencilFail");

		// Token: 0x04000207 RID: 519
		public static readonly Uniform stencilZFail = new Uniform("_StencilZFail");

		// Token: 0x04000208 RID: 520
		public static readonly Uniform outline = new Uniform("_Outline");

		// Token: 0x04000209 RID: 521
		public static readonly Uniform outlineData = new Uniform("_OutlineData");

		// Token: 0x0400020A RID: 522
		public static readonly Uniform outlineMap = new Uniform("_OutlineMap");

		// Token: 0x0400020B RID: 523
		public static readonly Uniform outlineSize = new Uniform("_OutlineSize");

		// Token: 0x0400020C RID: 524
		public static readonly Uniform outlineColor = new Uniform("_OutlineColor");

		// Token: 0x0400020D RID: 525
		public static readonly Uniform outlineNoise = new Uniform("_OutlineNoise");

		// Token: 0x0400020E RID: 526
		public static readonly Uniform refractionDistortionMapScale = new Uniform("_RefractionDistortionMapScale");

		// Token: 0x0400020F RID: 527
		public static readonly Uniform refractionDistortionMap = new Uniform("_RefractionDistortionMap");

		// Token: 0x04000210 RID: 528
		public static readonly Uniform refractionDistortion = new Uniform("_RefractionDistortion");

		// Token: 0x04000211 RID: 529
		public static readonly Uniform indexOfRefraction = new Uniform("_IndexOfRefraction");

		// Token: 0x04000212 RID: 530
		public static readonly Uniform refractionDistortionFade = new Uniform("_RefractionDistortionFade");

		// Token: 0x04000213 RID: 531
		public static readonly Uniform flipbook = new Uniform("_Flipbook");

		// Token: 0x04000214 RID: 532
		public static readonly Uniform softFade = new Uniform("_SoftFade");

		// Token: 0x04000215 RID: 533
		public static readonly Uniform softFadeNearDistance = new Uniform("_SoftFadeNearDistance");

		// Token: 0x04000216 RID: 534
		public static readonly Uniform softFadeFarDistance = new Uniform("_SoftFadeFarDistance");

		// Token: 0x04000217 RID: 535
		public static readonly Uniform cameraFade = new Uniform("_CameraFade");

		// Token: 0x04000218 RID: 536
		public static readonly Uniform cameraFadeNearDistance = new Uniform("_CameraFadeNearDistance");

		// Token: 0x04000219 RID: 537
		public static readonly Uniform cameraFadeFarDistance = new Uniform("_CameraFadeFarDistance");

		// Token: 0x0400021A RID: 538
		public static readonly Uniform colorBlend = new Uniform("_ColorBlend");

		// Token: 0x0400021B RID: 539
		public static readonly Uniform initialized = new Uniform("_Initialized");

		// Token: 0x0400021C RID: 540
		public static readonly Uniform optionsTab = new Uniform("_OptionsTab");

		// Token: 0x0400021D RID: 541
		public static readonly Uniform inputTab = new Uniform("_InputTab");

		// Token: 0x0400021E RID: 542
		public static readonly Uniform stylizeTab = new Uniform("_StylizeTab");

		// Token: 0x0400021F RID: 543
		public static readonly Uniform advancedTab = new Uniform("_AdvancedTab");

		// Token: 0x04000220 RID: 544
		public static readonly Uniform particlesTab = new Uniform("_ParticlesTab");

		// Token: 0x04000221 RID: 545
		public static readonly Uniform outlineTab = new Uniform("_OutlineTab");

		// Token: 0x04000222 RID: 546
		public static readonly Uniform refractionTab = new Uniform("_RefractionTab");

		// Token: 0x04000223 RID: 547
		public static readonly Uniform mainTex = new Uniform("_MainTex");

		// Token: 0x04000224 RID: 548
		public static readonly Uniform cutoff = new Uniform("_Cutoff");

		// Token: 0x04000225 RID: 549
		public static readonly Uniform color = new Uniform("_Color");
	}
}
