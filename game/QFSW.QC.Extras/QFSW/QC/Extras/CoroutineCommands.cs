using System;
using System.Collections;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x02000009 RID: 9
	public class CoroutineCommands : MonoBehaviour
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002E28 File Offset: 0x00001028
		private void StartCoroutineCommand(string coroutineCommand)
		{
			object obj = QuantumConsoleProcessor.InvokeCommand(coroutineCommand);
			if (obj is IEnumerator)
			{
				base.StartCoroutine(obj as IEnumerator);
				return;
			}
			throw new ArgumentException(coroutineCommand + " is not a coroutine");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E62 File Offset: 0x00001062
		public CoroutineCommands()
		{
		}
	}
}
