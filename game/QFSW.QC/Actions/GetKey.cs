using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC.Actions
{
	// Token: 0x02000073 RID: 115
	public class GetKey : ICommandAction
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A721 File Offset: 0x00008921
		public bool IsFinished
		{
			get
			{
				this._key = this.GetCurrentKeyDown();
				return this._key > KeyCode.None;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A738 File Offset: 0x00008938
		public bool StartsIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A73B File Offset: 0x0000893B
		public GetKey(Action<KeyCode> onKey)
		{
			this._onKey = onKey;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A74A File Offset: 0x0000894A
		private KeyCode GetCurrentKeyDown()
		{
			return GetKey.KeyCodes.FirstOrDefault(new Func<KeyCode, bool>(InputHelper.GetKeyDown));
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A762 File Offset: 0x00008962
		public void Start(ActionContext context)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A764 File Offset: 0x00008964
		public void Finalize(ActionContext context)
		{
			this._onKey(this._key);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A777 File Offset: 0x00008977
		// Note: this type is marked as 'beforefieldinit'.
		static GetKey()
		{
		}

		// Token: 0x04000156 RID: 342
		private KeyCode _key;

		// Token: 0x04000157 RID: 343
		private readonly Action<KeyCode> _onKey;

		// Token: 0x04000158 RID: 344
		private static readonly KeyCode[] KeyCodes = (from KeyCode k in Enum.GetValues(typeof(KeyCode))
		where k < KeyCode.Mouse0
		select k).ToArray<KeyCode>();

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000395 RID: 917 RVA: 0x0000CE9E File Offset: 0x0000B09E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000396 RID: 918 RVA: 0x0000CEAA File Offset: 0x0000B0AA
			public <>c()
			{
			}

			// Token: 0x06000397 RID: 919 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
			internal bool <.cctor>b__11_0(KeyCode k)
			{
				return k < KeyCode.Mouse0;
			}

			// Token: 0x0400025D RID: 605
			public static readonly GetKey.<>c <>9 = new GetKey.<>c();
		}
	}
}
