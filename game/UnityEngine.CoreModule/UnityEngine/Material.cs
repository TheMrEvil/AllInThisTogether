using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200014A RID: 330
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Material.h")]
	public class Material : Object
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x00011470 File Offset: 0x0000F670
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x06000CFF RID: 3327
		[FreeFunction("MaterialScripting::CreateWithShader")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithShader([Writable] Material self, [NotNull("ArgumentNullException")] Shader shader);

		// Token: 0x06000D00 RID: 3328
		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithMaterial([Writable] Material self, [NotNull("ArgumentNullException")] Material source);

		// Token: 0x06000D01 RID: 3329
		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithString([Writable] Material self);

		// Token: 0x06000D02 RID: 3330 RVA: 0x00011488 File Offset: 0x0000F688
		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0001149A File Offset: 0x0000F69A
		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000114AC File Offset: 0x0000F6AC
		[Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		// Token: 0x06000D05 RID: 3333
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultMaterial();

		// Token: 0x06000D06 RID: 3334
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultParticleMaterial();

		// Token: 0x06000D07 RID: 3335
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Material GetDefaultLineMaterial();

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D08 RID: 3336
		// (set) Token: 0x06000D09 RID: 3337
		public extern Shader shader { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x000114C0 File Offset: 0x0000F6C0
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00011500 File Offset: 0x0000F700
		public Color color
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(firstPropertyNameIdByAttribute);
				}
				else
				{
					color = this.GetColor("_Color");
				}
				return color;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetColor(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetColor("_Color", value);
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00011540 File Offset: 0x0000F740
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00011580 File Offset: 0x0000F780
		public Texture mainTexture
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(firstPropertyNameIdByAttribute);
				}
				else
				{
					texture = this.GetTexture("_MainTex");
				}
				return texture;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTexture(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTexture("_MainTex", value);
				}
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x000115C0 File Offset: 0x0000F7C0
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00011600 File Offset: 0x0000F800
		public Vector2 mainTextureOffset
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 textureOffset;
				if (flag)
				{
					textureOffset = this.GetTextureOffset(firstPropertyNameIdByAttribute);
				}
				else
				{
					textureOffset = this.GetTextureOffset("_MainTex");
				}
				return textureOffset;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureOffset(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureOffset("_MainTex", value);
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00011640 File Offset: 0x0000F840
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00011680 File Offset: 0x0000F880
		public Vector2 mainTextureScale
		{
			get
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				Vector2 textureScale;
				if (flag)
				{
					textureScale = this.GetTextureScale(firstPropertyNameIdByAttribute);
				}
				else
				{
					textureScale = this.GetTextureScale("_MainTex");
				}
				return textureScale;
			}
			set
			{
				int firstPropertyNameIdByAttribute = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = firstPropertyNameIdByAttribute >= 0;
				if (flag)
				{
					this.SetTextureScale(firstPropertyNameIdByAttribute, value);
				}
				else
				{
					this.SetTextureScale("_MainTex", value);
				}
			}
		}

		// Token: 0x06000D12 RID: 3346
		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag);

		// Token: 0x06000D13 RID: 3347
		[NativeName("HasPropertyFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProperty(int nameID);

		// Token: 0x06000D14 RID: 3348 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		// Token: 0x06000D15 RID: 3349
		[NativeName("HasFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasFloatImpl(int name);

		// Token: 0x06000D16 RID: 3350 RVA: 0x000116E0 File Offset: 0x0000F8E0
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00011700 File Offset: 0x0000F900
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0001171C File Offset: 0x0000F91C
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0001173C File Offset: 0x0000F93C
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000D1A RID: 3354
		[NativeName("HasIntegerFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasIntImpl(int name);

		// Token: 0x06000D1B RID: 3355 RVA: 0x00011758 File Offset: 0x0000F958
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00011778 File Offset: 0x0000F978
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x06000D1D RID: 3357
		[NativeName("HasTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasTextureImpl(int name);

		// Token: 0x06000D1E RID: 3358 RVA: 0x00011794 File Offset: 0x0000F994
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x06000D20 RID: 3360
		[NativeName("HasMatrixFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasMatrixImpl(int name);

		// Token: 0x06000D21 RID: 3361 RVA: 0x000117D0 File Offset: 0x0000F9D0
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06000D23 RID: 3363
		[NativeName("HasVectorFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasVectorImpl(int name);

		// Token: 0x06000D24 RID: 3364 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001182C File Offset: 0x0000FA2C
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00011848 File Offset: 0x0000FA48
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00011868 File Offset: 0x0000FA68
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000D28 RID: 3368
		[NativeName("HasBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasBufferImpl(int name);

		// Token: 0x06000D29 RID: 3369 RVA: 0x00011884 File Offset: 0x0000FA84
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06000D2B RID: 3371
		[NativeName("HasConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasConstantBufferImpl(int name);

		// Token: 0x06000D2C RID: 3372 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D2E RID: 3374
		// (set) Token: 0x06000D2F RID: 3375
		public extern int renderQueue { [NativeName("GetActualRenderQueue")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetCustomRenderQueue")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D30 RID: 3376
		internal extern int rawRenderQueue { [NativeName("GetCustomRenderQueue")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000D31 RID: 3377
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06000D32 RID: 3378
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06000D33 RID: 3379
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x06000D34 RID: 3380 RVA: 0x000118F9 File Offset: 0x0000FAF9
		[FreeFunction("MaterialScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			this.EnableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00011903 File Offset: 0x0000FB03
		[FreeFunction("MaterialScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			this.DisableLocalKeyword_Injected(ref keyword);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001190D File Offset: 0x0000FB0D
		[FreeFunction("MaterialScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword_Injected(ref keyword, value);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00011918 File Offset: 0x0000FB18
		[FreeFunction("MaterialScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled_Injected(ref keyword);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00011922 File Offset: 0x0000FB22
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00011932 File Offset: 0x0000FB32
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00011942 File Offset: 0x0000FB42
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00011954 File Offset: 0x0000FB54
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x06000D3C RID: 3388
		[FreeFunction("MaterialScripting::GetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern LocalKeyword[] GetEnabledKeywords();

		// Token: 0x06000D3D RID: 3389
		[FreeFunction("MaterialScripting::SetEnabledKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnabledKeywords(LocalKeyword[] keywords);

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00011974 File Offset: 0x0000FB74
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x0001198C File Offset: 0x0000FB8C
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D40 RID: 3392
		// (set) Token: 0x06000D41 RID: 3393
		public extern MaterialGlobalIlluminationFlags globalIlluminationFlags { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D42 RID: 3394
		// (set) Token: 0x06000D43 RID: 3395
		public extern bool doubleSidedGI { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D44 RID: 3396
		// (set) Token: 0x06000D45 RID: 3397
		[NativeProperty("EnableInstancingVariants")]
		public extern bool enableInstancing { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D46 RID: 3398
		public extern int passCount { [NativeName("GetShader()->GetPassCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000D47 RID: 3399
		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetShaderPassEnabled(string passName, bool enabled);

		// Token: 0x06000D48 RID: 3400
		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetShaderPassEnabled(string passName);

		// Token: 0x06000D49 RID: 3401
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetPassName(int pass);

		// Token: 0x06000D4A RID: 3402
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindPass(string passName);

		// Token: 0x06000D4B RID: 3403
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetOverrideTag(string tag, string val);

		// Token: 0x06000D4C RID: 3404
		[NativeName("GetTag")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue);

		// Token: 0x06000D4D RID: 3405 RVA: 0x00011998 File Offset: 0x0000FB98
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000119B8 File Offset: 0x0000FBB8
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		// Token: 0x06000D4F RID: 3407
		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true)]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Lerp(Material start, Material end, float t);

		// Token: 0x06000D50 RID: 3408
		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPass(int pass);

		// Token: 0x06000D51 RID: 3409
		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000D52 RID: 3410
		[FreeFunction("MaterialScripting::CopyMatchingPropertiesFrom", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyMatchingPropertiesFromMaterial(Material mat);

		// Token: 0x06000D53 RID: 3411
		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] GetShaderKeywords();

		// Token: 0x06000D54 RID: 3412
		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetShaderKeywords(string[] names);

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000119DC File Offset: 0x0000FBDC
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x000119F4 File Offset: 0x0000FBF4
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

		// Token: 0x06000D57 RID: 3415
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int ComputeCRC();

		// Token: 0x06000D58 RID: 3416
		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetTexturePropertyNames();

		// Token: 0x06000D59 RID: 3417
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int[] GetTexturePropertyNameIDs();

		// Token: 0x06000D5A RID: 3418
		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTexturePropertyNamesInternal(object outNames);

		// Token: 0x06000D5B RID: 3419
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTexturePropertyNameIDsInternal(object outNames);

		// Token: 0x06000D5C RID: 3420 RVA: 0x00011A00 File Offset: 0x0000FC00
		public void GetTexturePropertyNames(List<string> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNamesInternal(outNames);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00011A2C File Offset: 0x0000FC2C
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		// Token: 0x06000D5E RID: 3422
		[NativeName("SetIntFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntImpl(int name, int value);

		// Token: 0x06000D5F RID: 3423
		[NativeName("SetFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatImpl(int name, float value);

		// Token: 0x06000D60 RID: 3424 RVA: 0x00011A56 File Offset: 0x0000FC56
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			this.SetColorImpl_Injected(name, ref value);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00011A61 File Offset: 0x0000FC61
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			this.SetMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000D62 RID: 3426
		[NativeName("SetTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureImpl(int name, Texture value);

		// Token: 0x06000D63 RID: 3427
		[NativeName("SetRenderTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element);

		// Token: 0x06000D64 RID: 3428
		[NativeName("SetBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBufferImpl(int name, ComputeBuffer value);

		// Token: 0x06000D65 RID: 3429
		[NativeName("SetBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGraphicsBufferImpl(int name, GraphicsBuffer value);

		// Token: 0x06000D66 RID: 3430
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size);

		// Token: 0x06000D67 RID: 3431
		[NativeName("SetConstantBufferFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size);

		// Token: 0x06000D68 RID: 3432
		[NativeName("GetIntFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntImpl(int name);

		// Token: 0x06000D69 RID: 3433
		[NativeName("GetFloatFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatImpl(int name);

		// Token: 0x06000D6A RID: 3434 RVA: 0x00011A6C File Offset: 0x0000FC6C
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			Color result;
			this.GetColorImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00011A84 File Offset: 0x0000FC84
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			Matrix4x4 result;
			this.GetMatrixImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000D6C RID: 3436
		[NativeName("GetTextureFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture GetTextureImpl(int name);

		// Token: 0x06000D6D RID: 3437
		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatArrayImpl(int name, float[] values, int count);

		// Token: 0x06000D6E RID: 3438
		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVectorArrayImpl(int name, Vector4[] values, int count);

		// Token: 0x06000D6F RID: 3439
		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorArrayImpl(int name, Color[] values, int count);

		// Token: 0x06000D70 RID: 3440
		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count);

		// Token: 0x06000D71 RID: 3441
		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float[] GetFloatArrayImpl(int name);

		// Token: 0x06000D72 RID: 3442
		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector4[] GetVectorArrayImpl(int name);

		// Token: 0x06000D73 RID: 3443
		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Color[] GetColorArrayImpl(int name);

		// Token: 0x06000D74 RID: 3444
		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Matrix4x4[] GetMatrixArrayImpl(int name);

		// Token: 0x06000D75 RID: 3445
		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetFloatArrayCountImpl(int name);

		// Token: 0x06000D76 RID: 3446
		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetVectorArrayCountImpl(int name);

		// Token: 0x06000D77 RID: 3447
		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetColorArrayCountImpl(int name);

		// Token: 0x06000D78 RID: 3448
		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMatrixArrayCountImpl(int name);

		// Token: 0x06000D79 RID: 3449
		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractFloatArrayImpl(int name, [Out] float[] val);

		// Token: 0x06000D7A RID: 3450
		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractVectorArrayImpl(int name, [Out] Vector4[] val);

		// Token: 0x06000D7B RID: 3451
		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractColorArrayImpl(int name, [Out] Color[] val);

		// Token: 0x06000D7C RID: 3452
		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val);

		// Token: 0x06000D7D RID: 3453 RVA: 0x00011A9C File Offset: 0x0000FC9C
		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			Vector4 result;
			this.GetTextureScaleAndOffsetImpl_Injected(name, out result);
			return result;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			this.SetTextureOffsetImpl_Injected(name, ref offset);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00011ABE File Offset: 0x0000FCBE
		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			this.SetTextureScaleImpl_Injected(name, ref scale);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00011ACC File Offset: 0x0000FCCC
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

		// Token: 0x06000D81 RID: 3457 RVA: 0x00011B20 File Offset: 0x0000FD20
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

		// Token: 0x06000D82 RID: 3458 RVA: 0x00011B74 File Offset: 0x0000FD74
		private void SetColorArray(int name, Color[] values, int count)
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
			this.SetColorArrayImpl(name, values, count);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00011BC8 File Offset: 0x0000FDC8
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

		// Token: 0x06000D84 RID: 3460 RVA: 0x00011C1C File Offset: 0x0000FE1C
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

		// Token: 0x06000D85 RID: 3461 RVA: 0x00011C74 File Offset: 0x0000FE74
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

		// Token: 0x06000D86 RID: 3462 RVA: 0x00011CCC File Offset: 0x0000FECC
		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int colorArrayCountImpl = this.GetColorArrayCountImpl(name);
			bool flag2 = colorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, colorArrayCountImpl);
				this.ExtractColorArrayImpl(name, (Color[])NoAllocHelpers.ExtractArrayFromList(values));
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00011D24 File Offset: 0x0000FF24
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

		// Token: 0x06000D88 RID: 3464 RVA: 0x00011D79 File Offset: 0x0000FF79
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00011D8B File Offset: 0x0000FF8B
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00011D98 File Offset: 0x0000FF98
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00011DA9 File Offset: 0x0000FFA9
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00011DB5 File Offset: 0x0000FFB5
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00011DC6 File Offset: 0x0000FFC6
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00011DD2 File Offset: 0x0000FFD2
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00011DE3 File Offset: 0x0000FFE3
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00011DEF File Offset: 0x0000FFEF
		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00011E05 File Offset: 0x00010005
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00011E16 File Offset: 0x00010016
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00011E27 File Offset: 0x00010027
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00011E33 File Offset: 0x00010033
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00011E44 File Offset: 0x00010044
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00011E50 File Offset: 0x00010050
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00011E62 File Offset: 0x00010062
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00011E6F File Offset: 0x0001006F
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00011E80 File Offset: 0x00010080
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00011E8C File Offset: 0x0001008C
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00011E9D File Offset: 0x0001009D
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00011EA9 File Offset: 0x000100A9
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00011EBD File Offset: 0x000100BD
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00011ECC File Offset: 0x000100CC
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00011EE0 File Offset: 0x000100E0
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00011EEF File Offset: 0x000100EF
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00011F0B File Offset: 0x0001010B
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromListT<float>(values), values.Count);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00011F22 File Offset: 0x00010122
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00011F36 File Offset: 0x00010136
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00011F45 File Offset: 0x00010145
		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00011F61 File Offset: 0x00010161
		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Color>(values), values.Count);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00011F78 File Offset: 0x00010178
		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00011F8C File Offset: 0x0001018C
		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00011F9B File Offset: 0x0001019B
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00011FB7 File Offset: 0x000101B7
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Vector4>(values), values.Count);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00011FCE File Offset: 0x000101CE
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00011FE2 File Offset: 0x000101E2
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00011FF1 File Offset: 0x000101F1
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001200D File Offset: 0x0001020D
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromListT<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00012024 File Offset: 0x00010224
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00012038 File Offset: 0x00010238
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00012048 File Offset: 0x00010248
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00012068 File Offset: 0x00010268
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00012084 File Offset: 0x00010284
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000120A4 File Offset: 0x000102A4
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000120C0 File Offset: 0x000102C0
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000120E0 File Offset: 0x000102E0
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000120FC File Offset: 0x000102FC
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001211C File Offset: 0x0001031C
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00012138 File Offset: 0x00010338
		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001215C File Offset: 0x0001035C
		public Vector4 GetVector(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001217C File Offset: 0x0001037C
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001219C File Offset: 0x0001039C
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000121B8 File Offset: 0x000103B8
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000121D8 File Offset: 0x000103D8
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000121F4 File Offset: 0x000103F4
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00012214 File Offset: 0x00010414
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001223C File Offset: 0x0001043C
		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001225C File Offset: 0x0001045C
		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00012284 File Offset: 0x00010484
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000122A4 File Offset: 0x000104A4
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000122CC File Offset: 0x000104CC
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000122EC File Offset: 0x000104EC
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00012311 File Offset: 0x00010511
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00012322 File Offset: 0x00010522
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001232E File Offset: 0x0001052E
		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001233F File Offset: 0x0001053F
		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0001234B File Offset: 0x0001054B
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0001235C File Offset: 0x0001055C
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00012368 File Offset: 0x00010568
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00012379 File Offset: 0x00010579
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00012385 File Offset: 0x00010585
		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00012396 File Offset: 0x00010596
		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000123A2 File Offset: 0x000105A2
		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000123B3 File Offset: 0x000105B3
		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000123C0 File Offset: 0x000105C0
		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000123E0 File Offset: 0x000105E0
		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.z, textureScaleAndOffsetImpl.w);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0001240C File Offset: 0x0001060C
		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0001242C File Offset: 0x0001062C
		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 textureScaleAndOffsetImpl = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(textureScaleAndOffsetImpl.x, textureScaleAndOffsetImpl.y);
		}

		// Token: 0x06000DD6 RID: 3542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EnableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DD7 RID: 3543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DisableLocalKeyword_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DD8 RID: 3544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLocalKeyword_Injected(ref LocalKeyword keyword, bool value);

		// Token: 0x06000DD9 RID: 3545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsLocalKeywordEnabled_Injected(ref LocalKeyword keyword);

		// Token: 0x06000DDA RID: 3546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorImpl_Injected(int name, ref Color value);

		// Token: 0x06000DDB RID: 3547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrixImpl_Injected(int name, ref Matrix4x4 value);

		// Token: 0x06000DDC RID: 3548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorImpl_Injected(int name, out Color ret);

		// Token: 0x06000DDD RID: 3549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x06000DDE RID: 3550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTextureScaleAndOffsetImpl_Injected(int name, out Vector4 ret);

		// Token: 0x06000DDF RID: 3551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureOffsetImpl_Injected(int name, ref Vector2 offset);

		// Token: 0x06000DE0 RID: 3552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTextureScaleImpl_Injected(int name, ref Vector2 scale);
	}
}
