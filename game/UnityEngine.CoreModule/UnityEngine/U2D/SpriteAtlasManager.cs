using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.U2D
{
	// Token: 0x02000276 RID: 630
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[StaticAccessor("GetSpriteAtlasManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlasManager.h")]
	public class SpriteAtlasManager
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001B5C RID: 7004 RVA: 0x0002BEA4 File Offset: 0x0002A0A4
		// (remove) Token: 0x06001B5D RID: 7005 RVA: 0x0002BED8 File Offset: 0x0002A0D8
		public static event Action<string, Action<SpriteAtlas>> atlasRequested
		{
			[CompilerGenerated]
			add
			{
				Action<string, Action<SpriteAtlas>> action = SpriteAtlasManager.atlasRequested;
				Action<string, Action<SpriteAtlas>> action2;
				do
				{
					action2 = action;
					Action<string, Action<SpriteAtlas>> value2 = (Action<string, Action<SpriteAtlas>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string, Action<SpriteAtlas>>>(ref SpriteAtlasManager.atlasRequested, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string, Action<SpriteAtlas>> action = SpriteAtlasManager.atlasRequested;
				Action<string, Action<SpriteAtlas>> action2;
				do
				{
					action2 = action;
					Action<string, Action<SpriteAtlas>> value2 = (Action<string, Action<SpriteAtlas>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string, Action<SpriteAtlas>>>(ref SpriteAtlasManager.atlasRequested, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0002BF0C File Offset: 0x0002A10C
		[RequiredByNativeCode]
		private static bool RequestAtlas(string tag)
		{
			bool flag = SpriteAtlasManager.atlasRequested != null;
			bool result;
			if (flag)
			{
				SpriteAtlasManager.atlasRequested(tag, new Action<SpriteAtlas>(SpriteAtlasManager.Register));
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001B5F RID: 7007 RVA: 0x0002BF48 File Offset: 0x0002A148
		// (remove) Token: 0x06001B60 RID: 7008 RVA: 0x0002BF7C File Offset: 0x0002A17C
		public static event Action<SpriteAtlas> atlasRegistered
		{
			[CompilerGenerated]
			add
			{
				Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
				Action<SpriteAtlas> action2;
				do
				{
					action2 = action;
					Action<SpriteAtlas> value2 = (Action<SpriteAtlas>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SpriteAtlas>>(ref SpriteAtlasManager.atlasRegistered, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
				Action<SpriteAtlas> action2;
				do
				{
					action2 = action;
					Action<SpriteAtlas> value2 = (Action<SpriteAtlas>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SpriteAtlas>>(ref SpriteAtlasManager.atlasRegistered, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0002BFAF File Offset: 0x0002A1AF
		[RequiredByNativeCode]
		private static void PostRegisteredAtlas(SpriteAtlas spriteAtlas)
		{
			Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
			if (action != null)
			{
				action(spriteAtlas);
			}
		}

		// Token: 0x06001B62 RID: 7010
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Register(SpriteAtlas spriteAtlas);

		// Token: 0x06001B63 RID: 7011 RVA: 0x00002072 File Offset: 0x00000272
		public SpriteAtlasManager()
		{
		}

		// Token: 0x040008FB RID: 2299
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, Action<SpriteAtlas>> atlasRequested;

		// Token: 0x040008FC RID: 2300
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<SpriteAtlas> atlasRegistered;
	}
}
