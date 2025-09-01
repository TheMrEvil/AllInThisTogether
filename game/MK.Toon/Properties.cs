using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000024 RID: 36
	public static class Properties
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002434 File Offset: 0x00000634
		public static void UpdateSystemProperties(Material material)
		{
			material.SetTexture(Uniforms.mainTex.id, Properties.albedoMap.GetValue(material));
			material.SetFloat(Uniforms.cutoff.id, Properties.alphaCutoff.GetValue(material));
			material.SetColor(Uniforms.color.id, Properties.albedoColor.GetValue(material));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002494 File Offset: 0x00000694
		// Note: this type is marked as 'beforefieldinit'.
		static Properties()
		{
		}

		// Token: 0x040000EE RID: 238
		internal static readonly string shaderComponentOutlineName = "Outline";

		// Token: 0x040000EF RID: 239
		internal static readonly string shaderComponentRefractionName = "Refraction";

		// Token: 0x040000F0 RID: 240
		internal static readonly string shaderVariantPBSName = "Physically Based";

		// Token: 0x040000F1 RID: 241
		internal static readonly string shaderVariantSimpleName = "Simple";

		// Token: 0x040000F2 RID: 242
		internal static readonly string shaderVariantUnlitName = "Unlit";

		// Token: 0x040000F3 RID: 243
		public static readonly EnumProperty<Workflow> workflow = new EnumProperty<Workflow>(Uniforms.workflow, Keywords.workflow);

		// Token: 0x040000F4 RID: 244
		public static readonly EnumProperty<RenderFace> renderFace = new EnumProperty<RenderFace>(Uniforms.renderFace, Array.Empty<string>());

		// Token: 0x040000F5 RID: 245
		public static readonly SurfaceProperty surface = new SurfaceProperty(Uniforms.surface, Keywords.surface);

		// Token: 0x040000F6 RID: 246
		public static readonly EnumProperty<ZWrite> zWrite = new EnumProperty<ZWrite>(Uniforms.zWrite, Array.Empty<string>());

		// Token: 0x040000F7 RID: 247
		public static readonly EnumProperty<ZTest> zTest = new EnumProperty<ZTest>(Uniforms.zTest, Array.Empty<string>());

		// Token: 0x040000F8 RID: 248
		public static readonly EnumProperty<BlendFactor> blendSrc = new EnumProperty<BlendFactor>(Uniforms.blendSrc, Array.Empty<string>());

		// Token: 0x040000F9 RID: 249
		public static readonly EnumProperty<BlendFactor> blendDst = new EnumProperty<BlendFactor>(Uniforms.blendDst, Array.Empty<string>());

		// Token: 0x040000FA RID: 250
		public static readonly EnumProperty<BlendFactor> blendSrcAlpha = new EnumProperty<BlendFactor>(Uniforms.blendSrcAlpha, Array.Empty<string>());

		// Token: 0x040000FB RID: 251
		public static readonly EnumProperty<BlendFactor> blendDstAlpha = new EnumProperty<BlendFactor>(Uniforms.blendDstAlpha, Array.Empty<string>());

		// Token: 0x040000FC RID: 252
		public static readonly BlendProperty blend = new BlendProperty(Uniforms.blend, Keywords.blend);

		// Token: 0x040000FD RID: 253
		public static readonly AlphaClippingProperty alphaClipping = new AlphaClippingProperty(Uniforms.alphaClipping, Keywords.alphaClipping);

		// Token: 0x040000FE RID: 254
		public static readonly ColorProperty albedoColor = new ColorProperty(Uniforms.albedoColor);

		// Token: 0x040000FF RID: 255
		public static readonly RangeProperty alphaCutoff = new RangeProperty(Uniforms.alphaCutoff, 0f, 1f);

		// Token: 0x04000100 RID: 256
		public static readonly TextureProperty albedoMap = new TextureProperty(Uniforms.albedoMap, Keywords.albedoMap);

		// Token: 0x04000101 RID: 257
		public static readonly TilingProperty mainTiling = new TilingProperty(Uniforms.albedoMap);

		// Token: 0x04000102 RID: 258
		public static readonly OffsetProperty mainOffset = new OffsetProperty(Uniforms.albedoMap);

		// Token: 0x04000103 RID: 259
		public static readonly ColorProperty specularColor = new ColorProperty(Uniforms.specularColor);

		// Token: 0x04000104 RID: 260
		public static readonly RangeProperty metallic = new RangeProperty(Uniforms.metallic, 0f, 1f);

		// Token: 0x04000105 RID: 261
		public static readonly RangeProperty smoothness = new RangeProperty(Uniforms.smoothness, 0f, 1f);

		// Token: 0x04000106 RID: 262
		public static readonly RangeProperty roughness = new RangeProperty(Uniforms.roughness, 0f, 1f);

		// Token: 0x04000107 RID: 263
		public static readonly TextureProperty specularMap = new TextureProperty(Uniforms.specularMap, Keywords.pbsMap0);

		// Token: 0x04000108 RID: 264
		public static readonly TextureProperty roughnessMap = new TextureProperty(Uniforms.roughnessMap, Keywords.pbsMap1);

		// Token: 0x04000109 RID: 265
		public static readonly TextureProperty metallicMap = new TextureProperty(Uniforms.metallicMap, Keywords.pbsMap0);

		// Token: 0x0400010A RID: 266
		public static readonly FloatProperty normalMapIntensity = new FloatProperty(Uniforms.normalMapIntensity);

		// Token: 0x0400010B RID: 267
		public static readonly TextureProperty normalMap = new TextureProperty(Uniforms.normalMap, Keywords.normalMap);

		// Token: 0x0400010C RID: 268
		public static readonly RangeProperty parallax = new RangeProperty(Uniforms.parallax, Keywords.parallax, 0f, 0.1f);

		// Token: 0x0400010D RID: 269
		public static readonly TextureProperty heightMap = new TextureProperty(Uniforms.heightMap, Keywords.heightMap);

		// Token: 0x0400010E RID: 270
		public static readonly EnumProperty<LightTransmission> lightTransmission = new EnumProperty<LightTransmission>(Uniforms.lightTransmission, Array.Empty<string>());

		// Token: 0x0400010F RID: 271
		public static readonly RangeProperty lightTransmissionDistortion = new RangeProperty(Uniforms.lightTransmissionDistortion, 0f, 1f);

		// Token: 0x04000110 RID: 272
		public static readonly ColorProperty lightTransmissionColor = new ColorProperty(Uniforms.lightTransmissionColor);

		// Token: 0x04000111 RID: 273
		public static readonly TextureProperty thicknessMap = new TextureProperty(Uniforms.thicknessMap, Keywords.thicknessMap);

		// Token: 0x04000112 RID: 274
		public static readonly RangeProperty occlusionMapIntensity = new RangeProperty(Uniforms.occlusionMapIntensity, 0f, 1f);

		// Token: 0x04000113 RID: 275
		public static readonly TextureProperty occlusionMap = new TextureProperty(Uniforms.occlusionMap, Keywords.occlusionMap);

		// Token: 0x04000114 RID: 276
		public static readonly ColorProperty emissionColor = new ColorProperty(Uniforms.emissionColor, Keywords.emission);

		// Token: 0x04000115 RID: 277
		public static readonly TextureProperty emissionMap = new TextureProperty(Uniforms.emissionMap, Keywords.emissionMap);

		// Token: 0x04000116 RID: 278
		public static readonly EnumProperty<DetailBlend> detailBlend = new EnumProperty<DetailBlend>(Uniforms.detailBlend, Keywords.detailBlend);

		// Token: 0x04000117 RID: 279
		public static readonly ColorProperty detailColor = new ColorProperty(Uniforms.detailColor);

		// Token: 0x04000118 RID: 280
		public static readonly RangeProperty detailMix = new RangeProperty(Uniforms.detailMix, 0f, 1f);

		// Token: 0x04000119 RID: 281
		public static readonly TextureProperty detailMap = new TextureProperty(Uniforms.detailMap);

		// Token: 0x0400011A RID: 282
		public static readonly TilingProperty detailTiling = new TilingProperty(Uniforms.detailMap);

		// Token: 0x0400011B RID: 283
		public static readonly OffsetProperty detailOffset = new OffsetProperty(Uniforms.detailMap);

		// Token: 0x0400011C RID: 284
		public static readonly FloatProperty detailNormalMapIntensity = new FloatProperty(Uniforms.detailNormalMapIntensity);

		// Token: 0x0400011D RID: 285
		public static readonly TextureProperty detailNormalMap = new TextureProperty(Uniforms.detailNormalMap);

		// Token: 0x0400011E RID: 286
		public static readonly BoolProperty receiveShadows = new BoolProperty(Uniforms.receiveShadows, Keywords.receiveShadows);

		// Token: 0x0400011F RID: 287
		public static readonly BoolProperty wrappedLighting = new BoolProperty(Uniforms.wrappedLighting, Keywords.wrappedLighting);

		// Token: 0x04000120 RID: 288
		public static readonly RangeProperty diffuseSmoothness = new RangeProperty(Uniforms.diffuseSmoothness, 0f, 1f);

		// Token: 0x04000121 RID: 289
		public static readonly RangeProperty diffuseThresholdOffset = new RangeProperty(Uniforms.diffuseThresholdOffset, 0f, 1f);

		// Token: 0x04000122 RID: 290
		public static readonly RangeProperty specularSmoothness = new RangeProperty(Uniforms.specularSmoothness, 0f, 1f);

		// Token: 0x04000123 RID: 291
		public static readonly RangeProperty specularThresholdOffset = new RangeProperty(Uniforms.specularThresholdOffset, 0f, 1f);

		// Token: 0x04000124 RID: 292
		public static readonly RangeProperty rimSmoothness = new RangeProperty(Uniforms.rimSmoothness, 0f, 1f);

		// Token: 0x04000125 RID: 293
		public static readonly RangeProperty rimThresholdOffset = new RangeProperty(Uniforms.rimThresholdOffset, 0f, 1f);

		// Token: 0x04000126 RID: 294
		public static readonly RangeProperty lightTransmissionSmoothness = new RangeProperty(Uniforms.lightTransmissionSmoothness, 0f, 1f);

		// Token: 0x04000127 RID: 295
		public static readonly RangeProperty lightTransmissionThresholdOffset = new RangeProperty(Uniforms.lightTransmissionThresholdOffset, 0f, 1f);

		// Token: 0x04000128 RID: 296
		public static readonly EnumProperty<Light> light = new EnumProperty<Light>(Uniforms.light, Keywords.light);

		// Token: 0x04000129 RID: 297
		public static readonly TextureProperty diffuseRamp = new TextureProperty(Uniforms.diffuseRamp);

		// Token: 0x0400012A RID: 298
		public static readonly TextureProperty specularRamp = new TextureProperty(Uniforms.specularRamp);

		// Token: 0x0400012B RID: 299
		public static readonly TextureProperty rimRamp = new TextureProperty(Uniforms.rimRamp);

		// Token: 0x0400012C RID: 300
		public static readonly TextureProperty lightTransmissionRamp = new TextureProperty(Uniforms.lightTransmissionRamp);

		// Token: 0x0400012D RID: 301
		public static readonly StepProperty lightBands = new StepProperty(Uniforms.lightBands, 2, 12);

		// Token: 0x0400012E RID: 302
		public static readonly RangeProperty lightBandsScale = new RangeProperty(Uniforms.lightBandsScale, 0f, 1f);

		// Token: 0x0400012F RID: 303
		public static readonly RangeProperty lightThreshold = new RangeProperty(Uniforms.lightThreshold, 0f, 1f);

		// Token: 0x04000130 RID: 304
		public static readonly TextureProperty thresholdMap = new TextureProperty(Uniforms.thresholdMap, Keywords.thresholdMap);

		// Token: 0x04000131 RID: 305
		public static readonly FloatProperty thresholdMapScale = new FloatProperty(Uniforms.thresholdMapScale);

		// Token: 0x04000132 RID: 306
		public static readonly RangeProperty goochRampIntensity = new RangeProperty(Uniforms.goochRampIntensity, 0f, 1f);

		// Token: 0x04000133 RID: 307
		public static readonly TextureProperty goochRamp = new TextureProperty(Uniforms.goochRamp, Keywords.goochRamp);

		// Token: 0x04000134 RID: 308
		public static readonly ColorProperty goochBrightColor = new ColorProperty(Uniforms.goochBrightColor);

		// Token: 0x04000135 RID: 309
		public static readonly TextureProperty goochBrightMap = new TextureProperty(Uniforms.goochBrightMap, Keywords.goochBrightMap);

		// Token: 0x04000136 RID: 310
		public static readonly ColorProperty goochDarkColor = new ColorProperty(Uniforms.goochDarkColor);

		// Token: 0x04000137 RID: 311
		public static readonly TextureProperty goochDarkMap = new TextureProperty(Uniforms.goochDarkMap, Keywords.goochDarkMap);

		// Token: 0x04000138 RID: 312
		public static readonly EnumProperty<ColorGrading> colorGrading = new EnumProperty<ColorGrading>(Uniforms.colorGrading, Keywords.colorGrading);

		// Token: 0x04000139 RID: 313
		public static readonly FloatProperty contrast = new FloatProperty(Uniforms.contrast);

		// Token: 0x0400013A RID: 314
		public static readonly RangeProperty saturation = new RangeProperty(Uniforms.saturation, 0f);

		// Token: 0x0400013B RID: 315
		public static readonly RangeProperty brightness = new RangeProperty(Uniforms.brightness, 0f);

		// Token: 0x0400013C RID: 316
		public static readonly EnumProperty<Iridescence> iridescence = new EnumProperty<Iridescence>(Uniforms.iridescence, Keywords.iridescence);

		// Token: 0x0400013D RID: 317
		public static readonly TextureProperty iridescenceRamp = new TextureProperty(Uniforms.iridescenceRamp);

		// Token: 0x0400013E RID: 318
		public static readonly RangeProperty iridescenceSize = new RangeProperty(Uniforms.iridescenceSize, 0f, 5f);

		// Token: 0x0400013F RID: 319
		public static readonly RangeProperty iridescenceThresholdOffset = new RangeProperty(Uniforms.iridescenceThresholdOffset, 0f, 1f);

		// Token: 0x04000140 RID: 320
		public static readonly RangeProperty iridescenceSmoothness = new RangeProperty(Uniforms.iridescenceSmoothness, 0f, 1f);

		// Token: 0x04000141 RID: 321
		public static readonly ColorProperty iridescenceColor = new ColorProperty(Uniforms.iridescenceColor);

		// Token: 0x04000142 RID: 322
		public static readonly EnumProperty<Rim> rim = new EnumProperty<Rim>(Uniforms.rim, Keywords.rim);

		// Token: 0x04000143 RID: 323
		public static readonly ColorProperty rimColor = new ColorProperty(Uniforms.rimColor);

		// Token: 0x04000144 RID: 324
		public static readonly ColorProperty rimBrightColor = new ColorProperty(Uniforms.rimBrightColor);

		// Token: 0x04000145 RID: 325
		public static readonly ColorProperty rimDarkColor = new ColorProperty(Uniforms.rimDarkColor);

		// Token: 0x04000146 RID: 326
		public static readonly RangeProperty rimSize = new RangeProperty(Uniforms.rimSize, 0f, 1f);

		// Token: 0x04000147 RID: 327
		public static readonly EnumProperty<VertexAnimation> vertexAnimation = new EnumProperty<VertexAnimation>(Uniforms.vertexAnimation, Keywords.vertexAnimation);

		// Token: 0x04000148 RID: 328
		public static readonly BoolProperty vertexAnimationStutter = new BoolProperty(Uniforms.vertexAnimationStutter, Keywords.vertexAnimationStutter);

		// Token: 0x04000149 RID: 329
		public static readonly TextureProperty vertexAnimationMap = new TextureProperty(Uniforms.vertexAnimationMap, Keywords.vertexAnimationMap);

		// Token: 0x0400014A RID: 330
		public static readonly RangeProperty vertexAnimationIntensity = new RangeProperty(Uniforms.vertexAnimationIntensity, 0f, 1f);

		// Token: 0x0400014B RID: 331
		public static readonly Vector3Property vertexAnimationFrequency = new Vector3Property(Uniforms.vertexAnimationFrequency);

		// Token: 0x0400014C RID: 332
		public static readonly EnumProperty<Dissolve> dissolve = new EnumProperty<Dissolve>(Uniforms.dissolve, Keywords.dissolve);

		// Token: 0x0400014D RID: 333
		public static readonly TextureProperty dissolveMap = new TextureProperty(Uniforms.dissolveMap);

		// Token: 0x0400014E RID: 334
		public static readonly FloatProperty dissolveMapScale = new FloatProperty(Uniforms.dissolveMapScale);

		// Token: 0x0400014F RID: 335
		public static readonly RangeProperty dissolveAmount = new RangeProperty(Uniforms.dissolveAmount, 0f, 1f);

		// Token: 0x04000150 RID: 336
		public static readonly RangeProperty dissolveBorderSize = new RangeProperty(Uniforms.dissolveBorderSize, 0f, 1f);

		// Token: 0x04000151 RID: 337
		public static readonly TextureProperty dissolveBorderRamp = new TextureProperty(Uniforms.dissolveBorderRamp);

		// Token: 0x04000152 RID: 338
		public static readonly ColorProperty dissolveBorderColor = new ColorProperty(Uniforms.dissolveBorderColor);

		// Token: 0x04000153 RID: 339
		public static readonly EnumProperty<Artistic> artistic = new EnumProperty<Artistic>(Uniforms.artistic, Keywords.artistic);

		// Token: 0x04000154 RID: 340
		public static readonly EnumProperty<ArtisticProjection> artisticProjection = new EnumProperty<ArtisticProjection>(Uniforms.artisticProjection, Keywords.artisticProjection);

		// Token: 0x04000155 RID: 341
		public static readonly RangeProperty artisticFrequency = new RangeProperty(Uniforms.artisticFrequency, Keywords.artisticAnimation, 1f, 1f, 10f);

		// Token: 0x04000156 RID: 342
		public static readonly FloatProperty drawnMapScale = new FloatProperty(Uniforms.drawnMapScale);

		// Token: 0x04000157 RID: 343
		public static readonly TextureProperty drawnMap = new TextureProperty(Uniforms.drawnMap);

		// Token: 0x04000158 RID: 344
		public static readonly FloatProperty hatchingMapScale = new FloatProperty(Uniforms.hatchingMapScale);

		// Token: 0x04000159 RID: 345
		public static readonly TextureProperty hatchingBrightMap = new TextureProperty(Uniforms.hatchingBrightMap);

		// Token: 0x0400015A RID: 346
		public static readonly TextureProperty hatchingDarkMap = new TextureProperty(Uniforms.hatchingDarkMap);

		// Token: 0x0400015B RID: 347
		public static readonly RangeProperty drawnClampMin = new RangeProperty(Uniforms.drawnClampMin, 0f, 1f);

		// Token: 0x0400015C RID: 348
		public static readonly RangeProperty drawnClampMax = new RangeProperty(Uniforms.drawnClampMax, 0f, 1f);

		// Token: 0x0400015D RID: 349
		public static readonly FloatProperty sketchMapScale = new FloatProperty(Uniforms.sketchMapScale);

		// Token: 0x0400015E RID: 350
		public static readonly TextureProperty sketchMap = new TextureProperty(Uniforms.sketchMap);

		// Token: 0x0400015F RID: 351
		public static readonly EnumProperty<Diffuse> diffuse = new EnumProperty<Diffuse>(Uniforms.diffuse, Keywords.diffuse);

		// Token: 0x04000160 RID: 352
		public static readonly SpecularProperty specular = new SpecularProperty(Uniforms.specular, Keywords.specular);

		// Token: 0x04000161 RID: 353
		public static readonly RangeProperty specularIntensity = new RangeProperty(Uniforms.specularIntensity, 0f);

		// Token: 0x04000162 RID: 354
		public static readonly RangeProperty anisotropy = new RangeProperty(Uniforms.anisotropy, -1f, 1f);

		// Token: 0x04000163 RID: 355
		public static readonly RangeProperty lightTransmissionIntensity = new RangeProperty(Uniforms.lightTransmissionIntensity, 0f);

		// Token: 0x04000164 RID: 356
		public static readonly EnvironmentReflectionProperty environmentReflections = new EnvironmentReflectionProperty(Uniforms.environmentReflections, Keywords.environmentReflections);

		// Token: 0x04000165 RID: 357
		public static readonly BoolProperty fresnelHighlights = new BoolProperty(Uniforms.fresnelHighlights, Keywords.fresnelHighlights);

		// Token: 0x04000166 RID: 358
		public static readonly BoolProperty indirectFade = new BoolProperty(Uniforms.IndirectFade);

		// Token: 0x04000167 RID: 359
		public static readonly RenderPriorityProperty renderPriority = new RenderPriorityProperty(Uniforms.renderPriority);

		// Token: 0x04000168 RID: 360
		public static readonly StencilModeProperty stencil = new StencilModeProperty(Uniforms.stencil);

		// Token: 0x04000169 RID: 361
		public static readonly StepProperty stencilRef = new StepProperty(Uniforms.stencilRef, 0, 255);

		// Token: 0x0400016A RID: 362
		public static readonly StepProperty stencilReadMask = new StepProperty(Uniforms.stencilReadMask, 0, 255);

		// Token: 0x0400016B RID: 363
		public static readonly StepProperty stencilWriteMask = new StepProperty(Uniforms.stencilWriteMask, 0, 255);

		// Token: 0x0400016C RID: 364
		public static readonly EnumProperty<StencilComparison> stencilComp = new EnumProperty<StencilComparison>(Uniforms.stencilComp, Array.Empty<string>());

		// Token: 0x0400016D RID: 365
		public static readonly EnumProperty<StencilOperation> stencilPass = new EnumProperty<StencilOperation>(Uniforms.stencilPass, Array.Empty<string>());

		// Token: 0x0400016E RID: 366
		public static readonly EnumProperty<StencilOperation> stencilFail = new EnumProperty<StencilOperation>(Uniforms.stencilFail, Array.Empty<string>());

		// Token: 0x0400016F RID: 367
		public static readonly EnumProperty<StencilOperation> stencilZFail = new EnumProperty<StencilOperation>(Uniforms.stencilZFail, Array.Empty<string>());

		// Token: 0x04000170 RID: 368
		public static readonly EnumProperty<Outline> outline = new EnumProperty<Outline>(Uniforms.outline, Keywords.outline);

		// Token: 0x04000171 RID: 369
		public static readonly EnumProperty<OutlineData> outlineData = new EnumProperty<OutlineData>(Uniforms.outlineData, new string[]
		{
			Keywords.outlineData
		});

		// Token: 0x04000172 RID: 370
		public static readonly TextureProperty outlineMap = new TextureProperty(Uniforms.outlineMap, Keywords.outlineMap);

		// Token: 0x04000173 RID: 371
		public static readonly RangeProperty outlineSize = new RangeProperty(Uniforms.outlineSize, 0f);

		// Token: 0x04000174 RID: 372
		public static readonly ColorProperty outlineColor = new ColorProperty(Uniforms.outlineColor);

		// Token: 0x04000175 RID: 373
		public static readonly RangeProperty outlineNoise = new RangeProperty(Uniforms.outlineNoise, Keywords.outlineNoise, -1f, 1f);

		// Token: 0x04000176 RID: 374
		public static readonly FloatProperty refractionDistortionMapScale = new FloatProperty(Uniforms.refractionDistortionMapScale);

		// Token: 0x04000177 RID: 375
		public static readonly TextureProperty refractionDistortionMap = new TextureProperty(Uniforms.refractionDistortionMap, Keywords.refractionDistortionMap);

		// Token: 0x04000178 RID: 376
		public static readonly FloatProperty refractionDistortion = new FloatProperty(Uniforms.refractionDistortion);

		// Token: 0x04000179 RID: 377
		public static readonly RangeProperty refractionDistortionFade = new RangeProperty(Uniforms.refractionDistortionFade, 0f, 1f);

		// Token: 0x0400017A RID: 378
		public static readonly RangeProperty indexOfRefraction = new RangeProperty(Uniforms.indexOfRefraction, Keywords.indexOfRefraction, 0f, 0.5f);

		// Token: 0x0400017B RID: 379
		public static readonly BoolProperty flipbook = new BoolProperty(Uniforms.flipbook, Keywords.flipbook);

		// Token: 0x0400017C RID: 380
		public static readonly BoolProperty softFade = new BoolProperty(Uniforms.softFade, Keywords.softFade);

		// Token: 0x0400017D RID: 381
		public static readonly FloatProperty softFadeNearDistance = new FloatProperty(Uniforms.softFadeNearDistance);

		// Token: 0x0400017E RID: 382
		public static readonly FloatProperty softFadeFarDistance = new FloatProperty(Uniforms.softFadeFarDistance);

		// Token: 0x0400017F RID: 383
		public static readonly BoolProperty cameraFade = new BoolProperty(Uniforms.cameraFade, Keywords.cameraFade);

		// Token: 0x04000180 RID: 384
		public static readonly FloatProperty cameraFadeNearDistance = new FloatProperty(Uniforms.cameraFadeNearDistance);

		// Token: 0x04000181 RID: 385
		public static readonly FloatProperty cameraFadeFarDistance = new FloatProperty(Uniforms.cameraFadeFarDistance);

		// Token: 0x04000182 RID: 386
		public static readonly EnumProperty<ColorBlend> colorBlend = new EnumProperty<ColorBlend>(Uniforms.colorBlend, Keywords.colorBlend);
	}
}
