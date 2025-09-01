using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000150 RID: 336
	public class GrounderDemo : MonoBehaviour
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x00059BF0 File Offset: 0x00057DF0
		private void OnGUI()
		{
			if (GUILayout.Button("Biped", Array.Empty<GUILayoutOption>()))
			{
				this.Activate(0);
			}
			if (GUILayout.Button("Quadruped", Array.Empty<GUILayoutOption>()))
			{
				this.Activate(1);
			}
			if (GUILayout.Button("Mech", Array.Empty<GUILayoutOption>()))
			{
				this.Activate(2);
			}
			if (GUILayout.Button("Bot", Array.Empty<GUILayoutOption>()))
			{
				this.Activate(3);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00059C60 File Offset: 0x00057E60
		public void Activate(int index)
		{
			for (int i = 0; i < this.characters.Length; i++)
			{
				this.characters[i].SetActive(i == index);
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00059C91 File Offset: 0x00057E91
		public GrounderDemo()
		{
		}

		// Token: 0x04000AEE RID: 2798
		public GameObject[] characters;
	}
}
