using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public struct InputControlSource
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005959 File Offset: 0x00003B59
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005961 File Offset: 0x00003B61
		public InputControlSourceType SourceType
		{
			get
			{
				return this.sourceType;
			}
			set
			{
				this.sourceType = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000596A File Offset: 0x00003B6A
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005972 File Offset: 0x00003B72
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000597B File Offset: 0x00003B7B
		public InputControlSource(InputControlSourceType sourceType, int index)
		{
			this.sourceType = sourceType;
			this.index = index;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000598B File Offset: 0x00003B8B
		public InputControlSource(KeyCode keyCode)
		{
			this = new InputControlSource(InputControlSourceType.KeyCode, (int)keyCode);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005998 File Offset: 0x00003B98
		public float GetValue(InputDevice inputDevice)
		{
			switch (this.SourceType)
			{
			case InputControlSourceType.None:
				return 0f;
			case InputControlSourceType.Button:
				if (!this.GetState(inputDevice))
				{
					return 0f;
				}
				return 1f;
			case InputControlSourceType.Analog:
				return inputDevice.ReadRawAnalogValue(this.Index);
			case InputControlSourceType.KeyCode:
				if (!this.GetState(inputDevice))
				{
					return 0f;
				}
				return 1f;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005A08 File Offset: 0x00003C08
		public bool GetState(InputDevice inputDevice)
		{
			switch (this.SourceType)
			{
			case InputControlSourceType.None:
				return false;
			case InputControlSourceType.Button:
				return inputDevice.ReadRawButtonState(this.Index);
			case InputControlSourceType.Analog:
				return Utility.IsNotZero(this.GetValue(inputDevice));
			case InputControlSourceType.KeyCode:
				return Input.GetKey((KeyCode)this.Index);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005A64 File Offset: 0x00003C64
		public string ToCode()
		{
			return string.Concat(new string[]
			{
				"new InputControlSource( InputControlSourceType.",
				this.SourceType.ToString(),
				", ",
				this.Index.ToString(),
				" )"
			});
		}

		// Token: 0x04000157 RID: 343
		[SerializeField]
		private InputControlSourceType sourceType;

		// Token: 0x04000158 RID: 344
		[SerializeField]
		private int index;
	}
}
