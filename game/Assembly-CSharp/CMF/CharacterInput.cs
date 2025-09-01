using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B3 RID: 947
	public abstract class CharacterInput : MonoBehaviour
	{
		// Token: 0x06001F4F RID: 8015
		public abstract float GetHorizontalMovementInput();

		// Token: 0x06001F50 RID: 8016
		public abstract float GetVerticalMovementInput();

		// Token: 0x06001F51 RID: 8017
		public abstract bool IsJumpKeyPressed();

		// Token: 0x06001F52 RID: 8018 RVA: 0x000BB499 File Offset: 0x000B9699
		protected CharacterInput()
		{
		}
	}
}
