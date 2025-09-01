using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B7 RID: 439
	[NativeHeader("Runtime/Export/Input/Cursor.bindings.h")]
	public class Cursor
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x0001AF9B File Offset: 0x0001919B
		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			Cursor.SetCursor(texture, Vector2.zero, cursorMode);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0001AFAB File Offset: 0x000191AB
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.SetCursor_Injected(texture, ref hotspot, cursorMode);
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001341 RID: 4929
		// (set) Token: 0x06001342 RID: 4930
		public static extern bool visible { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001343 RID: 4931
		// (set) Token: 0x06001344 RID: 4932
		public static extern CursorLockMode lockState { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001345 RID: 4933 RVA: 0x00002072 File Offset: 0x00000272
		public Cursor()
		{
		}

		// Token: 0x06001346 RID: 4934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCursor_Injected(Texture2D texture, ref Vector2 hotspot, CursorMode cursorMode);
	}
}
