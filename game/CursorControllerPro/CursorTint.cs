using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x02000007 RID: 7
	public class CursorTint : MonoBehaviour
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003188 File Offset: 0x00001388
		public void Start()
		{
			this.allChildren = base.GetComponentsInChildren<Transform>();
			this.cursorController = base.GetComponentInParent<CursorController>();
			foreach (Transform transform in this.allChildren)
			{
				if (transform.GetComponent<Image>() != null && transform.GetComponent<Image>().sprite != null && transform.GetComponent<TintBypass>() == null)
				{
					if (this.cursorController.overrideTint)
					{
						transform.GetComponent<Image>().color = this.cursorController.tint;
					}
					else
					{
						transform.GetComponent<Image>().color = this.tint;
					}
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000322C File Offset: 0x0000142C
		public void SetColor(Color tint)
		{
			this.allChildren = base.GetComponentsInChildren<Transform>();
			this.cursorController = base.GetComponentInParent<CursorController>();
			foreach (Transform transform in this.allChildren)
			{
				if (transform.GetComponent<Image>() != null && transform.GetComponent<Image>().sprite != null && transform.GetComponent<TintBypass>() == null)
				{
					if (this.cursorController.overrideTint)
					{
						transform.GetComponent<Image>().color = this.cursorController.tint;
					}
					else
					{
						transform.GetComponent<Image>().color = tint;
					}
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000032CA File Offset: 0x000014CA
		public CursorTint()
		{
		}

		// Token: 0x04000047 RID: 71
		[TextArea]
		public string note = "Place this script on the root of a cursor style and it will check all the child objects that need to be tinted. Remember to use the 'TineBypass' script to avoid having an image component colored!";

		// Token: 0x04000048 RID: 72
		private Transform[] allChildren;

		// Token: 0x04000049 RID: 73
		[Header("COLOR")]
		[Tooltip("The color of all child object elements in the cursor will use this tint. The 'alpha' value is overwritten, so setting that here won't affect the animation. NOTE: This tint is only used if the 'override tint' is 'unticked' on CursorControl!")]
		public Color tint;

		// Token: 0x0400004A RID: 74
		private CursorController cursorController;

		// Token: 0x0400004B RID: 75
		[Tooltip("If true, the CursorControl tint override won't affect this cursor and you can choose individual colors and customization.")]
		public bool localTintOverride;
	}
}
