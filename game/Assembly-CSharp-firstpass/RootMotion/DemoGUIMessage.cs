using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B8 RID: 184
	public class DemoGUIMessage : MonoBehaviour
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0003893A File Offset: 0x00036B3A
		private void OnGUI()
		{
			GUI.color = this.color;
			GUILayout.Label(this.text, Array.Empty<GUILayoutOption>());
			GUI.color = Color.white;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00038961 File Offset: 0x00036B61
		public DemoGUIMessage()
		{
		}

		// Token: 0x040006A6 RID: 1702
		public string text;

		// Token: 0x040006A7 RID: 1703
		public Color color = Color.white;
	}
}
