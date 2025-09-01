using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000277 RID: 631
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	public class SpriteAtlas : Object
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B64 RID: 7012
		public extern bool isVariant { [NativeMethod("IsVariant")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B65 RID: 7013
		public extern string tag { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B66 RID: 7014
		public extern int spriteCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B67 RID: 7015
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanBindTo([NotNull("ArgumentNullException")] Sprite sprite);

		// Token: 0x06001B68 RID: 7016
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Sprite GetSprite(string name);

		// Token: 0x06001B69 RID: 7017 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		public int GetSprites(Sprite[] sprites)
		{
			return this.GetSpritesScripting(sprites);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
		public int GetSprites(Sprite[] sprites, string name)
		{
			return this.GetSpritesWithNameScripting(sprites, name);
		}

		// Token: 0x06001B6B RID: 7019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetSpritesScripting([Unmarshalled] Sprite[] sprites);

		// Token: 0x06001B6C RID: 7020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetSpritesWithNameScripting([Unmarshalled] Sprite[] sprites, string name);

		// Token: 0x06001B6D RID: 7021 RVA: 0x000243E7 File Offset: 0x000225E7
		public SpriteAtlas()
		{
		}
	}
}
