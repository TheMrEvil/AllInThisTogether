using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000122 RID: 290
	[NativeHeader("Runtime/Graphics/CustomRenderTextureManager.h")]
	public static class CustomRenderTextureManager
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060007FB RID: 2043 RVA: 0x0000BF34 File Offset: 0x0000A134
		// (remove) Token: 0x060007FC RID: 2044 RVA: 0x0000BF68 File Offset: 0x0000A168
		public static event Action<CustomRenderTexture> textureLoaded
		{
			[CompilerGenerated]
			add
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.textureLoaded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.textureLoaded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0000BF9B File Offset: 0x0000A19B
		[RequiredByNativeCode]
		private static void InvokeOnTextureLoaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060007FE RID: 2046 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		// (remove) Token: 0x060007FF RID: 2047 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public static event Action<CustomRenderTexture> textureUnloaded
		{
			[CompilerGenerated]
			add
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.textureUnloaded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.textureUnloaded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000C017 File Offset: 0x0000A217
		[RequiredByNativeCode]
		private static void InvokeOnTextureUnloaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x06000801 RID: 2049
		[FreeFunction(Name = "CustomRenderTextureManagerScripting::GetAllCustomRenderTextures", HasExplicitThis = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAllCustomRenderTextures(List<CustomRenderTexture> currentCustomRenderTextures);

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000802 RID: 2050 RVA: 0x0000C02C File Offset: 0x0000A22C
		// (remove) Token: 0x06000803 RID: 2051 RVA: 0x0000C060 File Offset: 0x0000A260
		public static event Action<CustomRenderTexture, int> updateTriggered
		{
			[CompilerGenerated]
			add
			{
				Action<CustomRenderTexture, int> action = CustomRenderTextureManager.updateTriggered;
				Action<CustomRenderTexture, int> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture, int> value2 = (Action<CustomRenderTexture, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture, int>>(ref CustomRenderTextureManager.updateTriggered, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CustomRenderTexture, int> action = CustomRenderTextureManager.updateTriggered;
				Action<CustomRenderTexture, int> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture, int> value2 = (Action<CustomRenderTexture, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture, int>>(ref CustomRenderTextureManager.updateTriggered, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0000C093 File Offset: 0x0000A293
		internal static void InvokeTriggerUpdate(CustomRenderTexture crt, int updateCount)
		{
			Action<CustomRenderTexture, int> action = CustomRenderTextureManager.updateTriggered;
			if (action != null)
			{
				action(crt, updateCount);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000805 RID: 2053 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		// (remove) Token: 0x06000806 RID: 2054 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		public static event Action<CustomRenderTexture> initializeTriggered
		{
			[CompilerGenerated]
			add
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.initializeTriggered;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.initializeTriggered, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<CustomRenderTexture> action = CustomRenderTextureManager.initializeTriggered;
				Action<CustomRenderTexture> action2;
				do
				{
					action2 = action;
					Action<CustomRenderTexture> value2 = (Action<CustomRenderTexture>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<CustomRenderTexture>>(ref CustomRenderTextureManager.initializeTriggered, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000C10F File Offset: 0x0000A30F
		internal static void InvokeTriggerInitialize(CustomRenderTexture crt)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.initializeTriggered;
			if (action != null)
			{
				action(crt);
			}
		}

		// Token: 0x040003A6 RID: 934
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<CustomRenderTexture> textureLoaded;

		// Token: 0x040003A7 RID: 935
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<CustomRenderTexture> textureUnloaded;

		// Token: 0x040003A8 RID: 936
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<CustomRenderTexture, int> updateTriggered;

		// Token: 0x040003A9 RID: 937
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<CustomRenderTexture> initializeTriggered;
	}
}
