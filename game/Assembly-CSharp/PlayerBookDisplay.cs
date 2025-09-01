using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class PlayerBookDisplay : MonoBehaviour
{
	// Token: 0x06000776 RID: 1910 RVA: 0x00035C0C File Offset: 0x00033E0C
	private void Initialize()
	{
		Transform child = this.FrontEmblemHolder.GetChild(0);
		this.frontEmblem = ((child != null) ? child.gameObject : null);
		Transform child2 = this.FrontProtectorHolder.GetChild(0);
		this.frontProtector = ((child2 != null) ? child2.gameObject : null);
		Transform child3 = this.BackProtectorHolder.GetChild(0);
		this.backProtector = ((child3 != null) ? child3.gameObject : null);
		this._pageBlock = new MaterialPropertyBlock();
		this.Pages.SetPropertyBlock(this._pageBlock);
		this.followRef = base.GetComponentInParent<BookFollow>();
		this.isInitialized = true;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00035CA4 File Offset: 0x00033EA4
	public void ChangeCosmetic(Cosmetic c, Color glowColor)
	{
		Cosmetic_Book cosmetic_Book = c as Cosmetic_Book;
		if (cosmetic_Book == null)
		{
			return;
		}
		if (!this.isInitialized)
		{
			this.Initialize();
		}
		this.Cover.material = cosmetic_Book.BookCover;
		this.Pages.SetPropertyBlock(this._pageBlock);
		this.CurShape = cosmetic_Book.MarkShape;
		PlayerBookDisplay.Bookmark bookmark = null;
		foreach (PlayerBookDisplay.Bookmark bookmark2 in this.Bookmarks)
		{
			bookmark2.Obj.SetActive(bookmark2.Shape == this.CurShape);
			if (bookmark2.Shape == this.CurShape)
			{
				bookmark = bookmark2;
			}
		}
		if (bookmark != null)
		{
			bookmark.Render.material = cosmetic_Book.MarkMaterail;
		}
		this.Binding.sharedMesh = cosmetic_Book.BindingMesh;
		if (cosmetic_Book.BindingMesh != null)
		{
			this.Binding.material = cosmetic_Book.BindingMat;
		}
		if (this.frontEmblem != null)
		{
			UnityEngine.Object.Destroy(this.frontEmblem);
		}
		if (cosmetic_Book.Emblem != null)
		{
			this.frontEmblem = UnityEngine.Object.Instantiate<GameObject>(cosmetic_Book.Emblem, this.FrontEmblemHolder);
			this.frontEmblem.transform.ResetLocalTransform();
		}
		if (this.frontProtector != null)
		{
			UnityEngine.Object.Destroy(this.frontProtector);
		}
		if (this.backProtector != null)
		{
			UnityEngine.Object.Destroy(this.backProtector);
		}
		if (cosmetic_Book.Protector != null)
		{
			this.frontProtector = UnityEngine.Object.Instantiate<GameObject>(cosmetic_Book.Protector, this.FrontProtectorHolder);
			this.frontProtector.transform.ResetLocalTransform();
			this.backProtector = UnityEngine.Object.Instantiate<GameObject>(cosmetic_Book.Protector, this.BackProtectorHolder);
			this.backProtector.transform.ResetLocalTransform();
		}
		this.UpdateColor(glowColor);
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00035E8C File Offset: 0x0003408C
	public void UpdateColor(Color c)
	{
		this.Cover.material.SetColor(PlayerDisplay.BodyEmissiveTint, c);
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x00035EA4 File Offset: 0x000340A4
	public void ToggleVisibility(bool isVisible)
	{
		if (this.frontEmblem != null)
		{
			this.frontEmblem.SetActive(isVisible);
		}
		if (this.backProtector != null)
		{
			this.backProtector.SetActive(isVisible);
		}
		if (this.frontProtector != null)
		{
			this.frontProtector.SetActive(isVisible);
		}
		this.Binding.enabled = isVisible;
		foreach (PlayerBookDisplay.Bookmark bookmark in this.Bookmarks)
		{
			bookmark.Obj.SetActive(bookmark.Shape == this.CurShape && isVisible);
		}
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00035F68 File Offset: 0x00034168
	private void Update()
	{
		bool flag = false;
		if (this.pageLighting > 0f && (this.followRef == null || this.followRef.FollowCamera))
		{
			this.pageLighting -= Time.deltaTime * 6f;
			this.pageLighting = Mathf.Clamp(this.pageLighting, 0f, 1f);
			flag = true;
		}
		else if (this.pageLighting < 1f && this.followRef != null && !this.followRef.FollowCamera)
		{
			this.pageLighting += Time.deltaTime * 6f;
			this.pageLighting = Mathf.Clamp(this.pageLighting, 0f, 1f);
			flag = true;
		}
		if (!flag || this._pageBlock == null)
		{
			return;
		}
		this._pageBlock.SetFloat("_Light", this.pageLighting);
		this.Pages.SetPropertyBlock(this._pageBlock);
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x00036066 File Offset: 0x00034266
	public PlayerBookDisplay()
	{
	}

	// Token: 0x0400061B RID: 1563
	public SkinnedMeshRenderer Cover;

	// Token: 0x0400061C RID: 1564
	public SkinnedMeshRenderer Pages;

	// Token: 0x0400061D RID: 1565
	public SkinnedMeshRenderer Binding;

	// Token: 0x0400061E RID: 1566
	public Transform FrontEmblemHolder;

	// Token: 0x0400061F RID: 1567
	private GameObject frontEmblem;

	// Token: 0x04000620 RID: 1568
	public Transform FrontProtectorHolder;

	// Token: 0x04000621 RID: 1569
	public Transform BackProtectorHolder;

	// Token: 0x04000622 RID: 1570
	private GameObject frontProtector;

	// Token: 0x04000623 RID: 1571
	private GameObject backProtector;

	// Token: 0x04000624 RID: 1572
	public List<PlayerBookDisplay.Bookmark> Bookmarks;

	// Token: 0x04000625 RID: 1573
	public PlayerBookDisplay.MarkShape CurShape;

	// Token: 0x04000626 RID: 1574
	private MaterialPropertyBlock _pageBlock;

	// Token: 0x04000627 RID: 1575
	private BookFollow followRef;

	// Token: 0x04000628 RID: 1576
	private bool isInitialized;

	// Token: 0x04000629 RID: 1577
	private float pageLighting = 1f;

	// Token: 0x020004AF RID: 1199
	[Serializable]
	public class Bookmark
	{
		// Token: 0x06002260 RID: 8800 RVA: 0x000C6A9F File Offset: 0x000C4C9F
		public Bookmark()
		{
		}

		// Token: 0x04002404 RID: 9220
		public PlayerBookDisplay.MarkShape Shape;

		// Token: 0x04002405 RID: 9221
		public GameObject Obj;

		// Token: 0x04002406 RID: 9222
		public MeshRenderer Render;
	}

	// Token: 0x020004B0 RID: 1200
	public enum MarkShape
	{
		// Token: 0x04002408 RID: 9224
		Simple,
		// Token: 0x04002409 RID: 9225
		Forked,
		// Token: 0x0400240A RID: 9226
		Flat,
		// Token: 0x0400240B RID: 9227
		Tipped
	}
}
