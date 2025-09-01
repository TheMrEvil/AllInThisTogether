using System;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000010 RID: 16
	public static class InputHelper
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002A84 File Offset: 0x00000C84
		private static bool IsKeySupported(KeyCode key)
		{
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A87 File Offset: 0x00000C87
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKey(key);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A8F File Offset: 0x00000C8F
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A97 File Offset: 0x00000C97
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A9F File Offset: 0x00000C9F
		public static Vector2 GetMousePosition()
		{
			return Input.mousePosition;
		}
	}
}
