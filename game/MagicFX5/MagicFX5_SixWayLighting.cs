using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001D RID: 29
	public class MagicFX5_SixWayLighting : MonoBehaviour
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00005700 File Offset: 0x00003900
		private void Awake()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005702 File Offset: 0x00003902
		private void OnEnable()
		{
			MagicFX5_GlobalUpdate.CreateInstanceIfRequired();
			MagicFX5_GlobalUpdate.SixWayLightingInstances.Add(this);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005715 File Offset: 0x00003915
		private void OnDisable()
		{
			MagicFX5_GlobalUpdate.SixWayLightingInstances.Remove(this);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005723 File Offset: 0x00003923
		public MagicFX5_SixWayLighting()
		{
		}
	}
}
