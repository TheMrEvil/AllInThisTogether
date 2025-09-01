using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200023B RID: 571
	public sealed class ShaderVariantCollection : Object
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600187F RID: 6271
		public extern int shaderCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001880 RID: 6272
		public extern int variantCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001881 RID: 6273
		public extern bool isWarmedUp { [NativeName("IsWarmedUp")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001882 RID: 6274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool AddVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords);

		// Token: 0x06001883 RID: 6275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RemoveVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords);

		// Token: 0x06001884 RID: 6276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ContainsVariant(Shader shader, PassType passType, [Unmarshalled] string[] keywords);

		// Token: 0x06001885 RID: 6277
		[NativeName("ClearVariants")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x06001886 RID: 6278
		[NativeName("WarmupShaders")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WarmUp();

		// Token: 0x06001887 RID: 6279
		[NativeName("CreateFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		// Token: 0x06001888 RID: 6280 RVA: 0x00027A57 File Offset: 0x00025C57
		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00027A68 File Offset: 0x00025C68
		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00027A94 File Offset: 0x00025C94
		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00027AC0 File Offset: 0x00025CC0
		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x0200023C RID: 572
		public struct ShaderVariant
		{
			// Token: 0x0600188C RID: 6284
			[NativeConditional("UNITY_EDITOR")]
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern string CheckShaderVariant(Shader shader, PassType passType, string[] keywords);

			// Token: 0x0600188D RID: 6285 RVA: 0x00027AEA File Offset: 0x00025CEA
			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
			}

			// Token: 0x0400083F RID: 2111
			public Shader shader;

			// Token: 0x04000840 RID: 2112
			public PassType passType;

			// Token: 0x04000841 RID: 2113
			public string[] keywords;
		}
	}
}
