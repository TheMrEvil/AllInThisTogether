using System;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.UtilityScripts
{
	// Token: 0x02000004 RID: 4
	public class PointedAtGameObjectInfo : MonoBehaviour
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002751 File Offset: 0x00000951
		private void Start()
		{
			if (PointedAtGameObjectInfo.Instance != null)
			{
				Debug.LogWarning("PointedAtGameObjectInfo is already featured in the scene, gameobject is destroyed");
				UnityEngine.Object.Destroy(base.gameObject);
			}
			PointedAtGameObjectInfo.Instance = this;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000277C File Offset: 0x0000097C
		public void SetFocus(PhotonView pv)
		{
			this.focus = ((pv != null) ? pv.transform : null);
			if (pv != null)
			{
				this.text.text = string.Format("id {0} own: {1} {2}{3}", new object[]
				{
					pv.ViewID,
					pv.OwnerActorNr,
					pv.IsRoomView ? "scn" : "",
					pv.IsMine ? " mine" : ""
				});
				return;
			}
			this.text.text = string.Empty;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002820 File Offset: 0x00000A20
		public void RemoveFocus(PhotonView pv)
		{
			if (pv == null)
			{
				this.text.text = string.Empty;
				return;
			}
			if (pv.transform == this.focus)
			{
				this.text.text = string.Empty;
				return;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002860 File Offset: 0x00000A60
		private void LateUpdate()
		{
			if (this.focus != null)
			{
				base.transform.position = Camera.main.WorldToScreenPoint(this.focus.position);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002890 File Offset: 0x00000A90
		public PointedAtGameObjectInfo()
		{
		}

		// Token: 0x0400000C RID: 12
		public static PointedAtGameObjectInfo Instance;

		// Token: 0x0400000D RID: 13
		public Text text;

		// Token: 0x0400000E RID: 14
		private Transform focus;
	}
}
