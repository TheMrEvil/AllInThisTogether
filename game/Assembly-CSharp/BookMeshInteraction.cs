using System;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class BookMeshInteraction : DiageticOption
{
	// Token: 0x06000019 RID: 25 RVA: 0x00004128 File Offset: 0x00002328
	protected override void Awake()
	{
		base.Awake();
		this.bookMesh = base.GetComponentInParent<BookMesh>();
		GameObject gameObject = new GameObject("UIRoot")
		{
			transform = 
			{
				parent = base.transform,
				localPosition = Vector3.zero,
				localRotation = Quaternion.identity
			}
		};
		if (this.bookMesh != null)
		{
			gameObject.transform.localPosition += Vector3.up * (float)this.InteractionShelf * this.bookMesh.ShelfHeight;
		}
		this.UIRoot = gameObject.transform;
		this.InteractDistance = ((this.bookMesh == null) ? (this.Width / 2f) : (this.bookMesh.ShelfWidth / 2f + 0.33f));
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000420D File Offset: 0x0000240D
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00004215 File Offset: 0x00002415
	public override void Select()
	{
		LorePanel.instance.Load(this);
		LorePanel.instance.Eyeball();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000422C File Offset: 0x0000242C
	private bool ValidateBookMesh()
	{
		if (this.bookMesh == null)
		{
			this.bookMesh = base.GetComponentInParent<BookMesh>();
		}
		return this.bookMesh == null;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00004254 File Offset: 0x00002454
	private void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.3f, 0.5f, 0.5f, 0.1f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00004285 File Offset: 0x00002485
	public BookMeshInteraction()
	{
	}

	// Token: 0x0400000F RID: 15
	private BookMesh bookMesh;

	// Token: 0x04000010 RID: 16
	public float Width = 4f;

	// Token: 0x04000011 RID: 17
	public int InteractionShelf;

	// Token: 0x04000012 RID: 18
	[TextArea(6, 10)]
	public string Info;
}
