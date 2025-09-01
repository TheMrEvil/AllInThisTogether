using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000018 RID: 24
	internal class CursorManager : ICursorManager
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004225 File Offset: 0x00002425
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000422D File Offset: 0x0000242D
		public bool isCursorOverriden
		{
			[CompilerGenerated]
			get
			{
				return this.<isCursorOverriden>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isCursorOverriden>k__BackingField = value;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004238 File Offset: 0x00002438
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
				this.isCursorOverriden = true;
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000429C File Offset: 0x0000249C
		public void ResetCursor()
		{
			bool isCursorOverriden = this.isCursorOverriden;
			if (isCursorOverriden)
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
			this.isCursorOverriden = false;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000020C2 File Offset: 0x000002C2
		public CursorManager()
		{
		}

		// Token: 0x0400003B RID: 59
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <isCursorOverriden>k__BackingField;
	}
}
