using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating
{
	// Token: 0x02000060 RID: 96
	public class FGeneratorsGizmosDrawer : MonoBehaviour
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00018F90 File Offset: 0x00017190
		public static void CheckExistence()
		{
			if (FGeneratorsGizmosDrawer.Instance != null)
			{
				return;
			}
			FGeneratorsGizmosDrawer.Instance = UnityEngine.Object.FindObjectOfType<FGeneratorsGizmosDrawer>();
			if (FGeneratorsGizmosDrawer.Instance != null)
			{
				return;
			}
			FGeneratorsGizmosDrawer.Instance = new GameObject("FGenerators-GizmosDrawer").AddComponent<FGeneratorsGizmosDrawer>();
			FGeneratorsGizmosDrawer.Instance.Refresh();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00018FE1 File Offset: 0x000171E1
		public static void TemporaryRemove()
		{
			if (FGeneratorsGizmosDrawer.Instance == null)
			{
				return;
			}
			FGenerators.DestroyObject(FGeneratorsGizmosDrawer.Instance.gameObject);
			FGeneratorsGizmosDrawer.Instance = null;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00019008 File Offset: 0x00017208
		public static void AddEvent(Action ev)
		{
			FGeneratorsGizmosDrawer.CheckExistence();
			if (FGeneratorsGizmosDrawer.Instance.onGUI == null)
			{
				FGeneratorsGizmosDrawer.Instance.onGUI = new List<Action>();
			}
			if (!FGeneratorsGizmosDrawer.Instance.onGUI.Contains(ev))
			{
				FGeneratorsGizmosDrawer.Instance.onGUI.Add(ev);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00019057 File Offset: 0x00017257
		private void Refresh()
		{
			if (FGeneratorsGizmosDrawer.Instance && FGeneratorsGizmosDrawer.Instance != this)
			{
				FGenerators.DestroyObject(FGeneratorsGizmosDrawer.Instance.gameObject);
			}
			FGeneratorsGizmosDrawer.Instance = this;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00019087 File Offset: 0x00017287
		private void OnEnable()
		{
			this.Refresh();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001908F File Offset: 0x0001728F
		private void Awake()
		{
			this.Refresh();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00019098 File Offset: 0x00017298
		private void OnDrawGizmos()
		{
			this.Refresh();
			if (this.onGUI == null)
			{
				this.onGUI = new List<Action>();
			}
			for (int i = 0; i < this.onGUI.Count; i++)
			{
				if (this.onGUI[i] != null)
				{
					this.onGUI[i]();
				}
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000190F3 File Offset: 0x000172F3
		public FGeneratorsGizmosDrawer()
		{
		}

		// Token: 0x04000302 RID: 770
		public static FGeneratorsGizmosDrawer Instance;

		// Token: 0x04000303 RID: 771
		private List<Action> onGUI;
	}
}
