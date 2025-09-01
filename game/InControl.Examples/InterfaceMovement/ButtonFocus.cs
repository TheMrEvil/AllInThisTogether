using System;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x02000006 RID: 6
	public class ButtonFocus : MonoBehaviour
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000274C File Offset: 0x0000094C
		private void Update()
		{
			Button focusedButton = base.transform.parent.GetComponent<ButtonManager>().focusedButton;
			base.transform.position = Vector3.MoveTowards(base.transform.position, focusedButton.transform.position, Time.deltaTime * 10f);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000027A0 File Offset: 0x000009A0
		public ButtonFocus()
		{
		}
	}
}
