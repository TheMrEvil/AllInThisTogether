using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class PlayerNook : MonoBehaviour
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00058ADF File Offset: 0x00056CDF
	public int ItemCount
	{
		get
		{
			return this.SpawnedItems.Count;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00058AEC File Offset: 0x00056CEC
	public static int MyItemCount
	{
		get
		{
			PlayerNook myNook = PlayerNook.MyNook;
			if (myNook == null)
			{
				return 0;
			}
			return myNook.ItemCount;
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00058AFE File Offset: 0x00056CFE
	private void Start()
	{
		this.SetOpenState(false);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00058B07 File Offset: 0x00056D07
	private void Update()
	{
		if (this != PlayerNook.MyNook)
		{
			return;
		}
		if (PlayerNook.IsInEditMode)
		{
			this.UpdateEditing();
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00058B24 File Offset: 0x00056D24
	public void SetOwner(PlayerControl owner)
	{
		if (this.Owner == PlayerControl.myInstance && owner == null)
		{
			PlayerNook.IsPlayerInside = false;
			this.ExitEditMode();
			PlayerNook.MyNook = null;
		}
		this.Owner = owner;
		if (this.Owner == PlayerControl.myInstance)
		{
			PlayerNook.MyNook = this;
			this.LoadLocalLayout();
			if (Library_LorePages.ShouldShowNookTutorial(this.NookTutorialLorePage.UID))
			{
				this.NookTutorialLorePage.gameObject.SetActive(true);
			}
		}
		this.SetOpenState(owner != null);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00058BB3 File Offset: 0x00056DB3
	private void SetOpenState(bool isOpen)
	{
		if (!isOpen)
		{
			this.NookTutorialLorePage.gameObject.SetActive(false);
			this.ClearItems();
		}
		this.ClosedGroup.SetActive(!isOpen);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00058BE0 File Offset: 0x00056DE0
	public void ClearItems()
	{
		while (this.SpawnedItems.Count > 0)
		{
			if (this.SpawnedItems[0] != null)
			{
				this.SpawnedItems[0].RemoveSelf(false);
			}
			if (this.SpawnedItems.Count > 0 && this.SpawnedItems[0] == null)
			{
				this.SpawnedItems.RemoveAt(0);
			}
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00058C54 File Offset: 0x00056E54
	private void OnTriggerEnter(Collider col)
	{
		if (this.Owner != PlayerControl.myInstance || PlayerNook.IsPlayerInside)
		{
			return;
		}
		PlayerControl componentInParent = col.GetComponentInParent<PlayerControl>();
		if (componentInParent == null || componentInParent != PlayerControl.myInstance)
		{
			return;
		}
		PlayerNook.IsPlayerInside = true;
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00058CA0 File Offset: 0x00056EA0
	private void OnTriggerExit(Collider col)
	{
		if (this.Owner != PlayerControl.myInstance || !PlayerNook.IsPlayerInside)
		{
			return;
		}
		PlayerControl componentInParent = col.GetComponentInParent<PlayerControl>();
		if (componentInParent == null || componentInParent != PlayerControl.myInstance)
		{
			return;
		}
		PlayerNook.IsPlayerInside = false;
		if (PlayerNook.IsInEditMode)
		{
			this.ExitEditMode();
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x00058CF8 File Offset: 0x00056EF8
	private void OnDestroy()
	{
		PlayerNook.IsPlayerInside = false;
		PlayerNook.IsInEditMode = false;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00058D06 File Offset: 0x00056F06
	public static void ToggleEditMode()
	{
		if (PlayerNook.MyNook == null || !PlayerNook.IsPlayerInside)
		{
			return;
		}
		if (PlayerNook.IsInEditMode)
		{
			PlayerNook.MyNook.ExitEditMode();
			return;
		}
		PlayerNook.MyNook.EnterEditMode();
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00058D39 File Offset: 0x00056F39
	private void EnterEditMode()
	{
		if (!PlayerNook.IsPlayerInside || PlayerNook.IsInEditMode)
		{
			return;
		}
		PlayerNook.IsInEditMode = true;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00058D50 File Offset: 0x00056F50
	public void PrimaryActionTaken()
	{
		if (this.HeldItem != null)
		{
			if (this.HeldItem.IsValid && this.HeldItem.previewSurface != null)
			{
				this.HeldItem.PlaceOnSurface(this, this.HeldItem.previewSurface, true);
				if (!this.SpawnedItems.Contains(this.HeldItem))
				{
					this.SpawnedItems.Add(this.HeldItem);
				}
				this.HeldItem = null;
				this.SaveLayout();
			}
			return;
		}
		if (this.HilightedItem != null)
		{
			this.HilightedItem.EnterPlacementMode(false);
			this.CameFromUI = false;
		}
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x00058DF8 File Offset: 0x00056FF8
	public void SecondaryActionTaken()
	{
		if (this.HeldItem != null)
		{
			this.HeldItem.CancelPlacement(this);
			if (this.CameFromUI)
			{
				NookPanel.Toggle();
				this.CameFromUI = false;
				return;
			}
		}
		else if (this.HilightedItem != null)
		{
			this.HilightedItem.RemoveSelf(true);
			this.SaveLayout();
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00058E54 File Offset: 0x00057054
	private void UpdateEditing()
	{
		if (InputManager.Actions.Pause.WasPressed && PanelManager.CurPanel == PanelType.GameInvisible)
		{
			if (this.HeldItem != null)
			{
				this.HeldItem.CancelPlacement(this);
			}
			else
			{
				this.ExitEditMode();
			}
		}
		if (!(this.HeldItem != null))
		{
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				if (this.HilightedItem != null)
				{
					this.HilightedItem.ToggleHighlight(false);
					this.HilightedItem = null;
					return;
				}
			}
			else
			{
				Transform transform = PlayerControl.MyCamera.transform;
				RaycastHit raycastHit;
				if (Physics.Raycast(transform.position, transform.forward, out raycastHit, 100f, this.HighlightMask))
				{
					NookItem componentInParent = raycastHit.collider.GetComponentInParent<NookItem>();
					if (componentInParent != null && componentInParent != this.HilightedItem)
					{
						if (this.HilightedItem != null)
						{
							this.HilightedItem.ToggleHighlight(false);
						}
						this.HilightedItem = componentInParent;
						this.HilightedItem.ToggleHighlight(true);
						return;
					}
				}
				else if (this.HilightedItem != null)
				{
					this.HilightedItem.ToggleHighlight(false);
					this.HilightedItem = null;
				}
			}
			return;
		}
		if (InputManager.IsUsingController)
		{
			float num = 0f;
			if (this.SnapMode)
			{
				if (InputManager.Actions.DPLeft.WasPressed || InputManager.Actions.DPLeft.WasRepeated)
				{
					num = 1f;
				}
				else if (InputManager.Actions.DPRight.WasPressed || InputManager.Actions.DPRight.WasRepeated)
				{
					num = -1f;
				}
				this.HeldItem.Angle = this.HeldItem.Angle - this.HeldItem.Angle % 15f + num * 15f;
			}
			else
			{
				this.HeldItem.Angle += -InputManager.Actions.DPad.X * 64f * Time.deltaTime;
			}
		}
		else if (this.SnapMode && Input.mouseScrollDelta.y != 0f)
		{
			this.HeldItem.Angle = this.HeldItem.Angle - this.HeldItem.Angle % 15f + Mathf.Sign(Input.mouseScrollDelta.y) * 15f;
		}
		else
		{
			this.HeldItem.Angle += Input.mouseScrollDelta.y * 10f;
		}
		if (this.HeldValidSurfaces.Count == 0)
		{
			this.HeldItem.CancelPlacement(this);
			return;
		}
		ValueTuple<NookSurface, Vector3> aimPoint = NookSurface.GetAimPoint(this.HeldValidSurfaces);
		NookSurface item = aimPoint.Item1;
		Vector3 vector = aimPoint.Item2;
		if (item != null)
		{
			this.lastSurface = item;
		}
		if (item == null && this.lastSurface != null)
		{
			item = this.lastSurface;
			vector = this.lastSurface.GetInboundSurfacePoint(vector);
		}
		this.HeldItem.PreviewPlacement(this, item, vector);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0005914C File Offset: 0x0005734C
	private void ExitEditMode()
	{
		if (!PlayerNook.IsInEditMode)
		{
			return;
		}
		PlayerNook.IsInEditMode = false;
		NookPanel.Toggle();
		if (this.HeldItem != null)
		{
			this.HeldItem.CancelPlacement(this);
		}
		if (this.HilightedItem != null)
		{
			this.HilightedItem.ToggleHighlight(false);
			this.HilightedItem = null;
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x000591A8 File Offset: 0x000573A8
	public void StartPlacement(NookItem item, bool fromUI)
	{
		this.HeldItem = item;
		this.HilightedItem = null;
		this.GetValidSurfaces(item);
		if (fromUI)
		{
			AudioManager.PlayInterfaceSFX(this.StartPlacementSFX, 1f, 0f);
			return;
		}
		AudioClip placementSound = NookDB.GetPlacementSound(item);
		if (placementSound != null)
		{
			AudioManager.PlayClipAtPoint(placementSound, item.transform.position, 1f, 1f, 1f, 10f, 250f);
		}
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00059220 File Offset: 0x00057420
	private void GetValidSurfaces(NookItem item)
	{
		List<NookSurface> list = new List<NookSurface>();
		foreach (NookSurface nookSurface in this.Surfaces)
		{
			if (nookSurface.CanHold(item))
			{
				list.Add(nookSurface);
			}
			nookSurface.AddValidSubsurfaces(item, ref list);
		}
		this.lastSurface = ((list.Count > 0) ? list[0] : null);
		this.HeldValidSurfaces = list;
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x000592AC File Offset: 0x000574AC
	public Vector3 GetSnapPoint(Vector3 point, NookSurface.SurfaceType surface)
	{
		float num = 0.5f;
		Vector3 vector = this.RootHolder.InverseTransformPoint(point);
		vector.x = Mathf.Round(vector.x / num) * num;
		if (surface != NookSurface.SurfaceType.Flat)
		{
			vector.y = Mathf.Round(vector.y / num) * num;
		}
		vector.z = Mathf.Round(vector.z / num) * num;
		return this.RootHolder.TransformPoint(vector);
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00059320 File Offset: 0x00057520
	public void PlayPlacementFX(NookItem item)
	{
		float value = item.Size / 5f;
		this.PlacementFX.transform.localScale = Vector3.one * Mathf.Clamp(value, 0.2f, 1f);
		this.PlacementFX.transform.position = item.transform.position;
		this.PlacementFX.transform.up = item.CurrentSurface.transform.forward;
		this.PlacementFX.Play();
		AudioClip placementSound = NookDB.GetPlacementSound(item);
		if (placementSound != null)
		{
			AudioManager.PlayClipAtPoint(placementSound, item.transform.position, 1f, 1f, 1f, 10f, 250f);
		}
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x000593E8 File Offset: 0x000575E8
	public void PlayRemovalFX(NookItem item)
	{
		float value = item.Size / 4.5f;
		this.RemovalFX.transform.localScale = Vector3.one * Mathf.Clamp(value, 0.25f, 1f);
		this.RemovalFX.transform.position = item.transform.position;
		this.RemovalFX.transform.up = item.CurrentSurface.transform.forward;
		this.RemovalFX.Play();
		AudioManager.PlayClipAtPoint(NookDB.DB.RemoveSound, item.transform.position, 1f, 1f, 1f, 10f, 250f);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x000594A8 File Offset: 0x000576A8
	private void SaveLayout()
	{
		string layoutJSON = this.GetLayoutJSON();
		Settings.SaveNookLayout(layoutJSON);
		NookManager.instance.SendNookLayout(this, layoutJSON);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000594D0 File Offset: 0x000576D0
	private void LoadLocalLayout()
	{
		string nookLayout = Settings.GetNookLayout();
		this.LoadLayout(nookLayout);
		NookManager.instance.SendNookLayout(this, nookLayout);
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000594F8 File Offset: 0x000576F8
	public void LoadLayout(string layout)
	{
		this.ClearItems();
		JSONNode jsonnode = JSON.Parse(layout);
		if (jsonnode.HasKey("Root"))
		{
			JSONArray jsonarray = jsonnode["Root"] as JSONArray;
			for (int i = 0; i < jsonarray.Count; i++)
			{
				JSONArray items = jsonarray[i] as JSONArray;
				this.Surfaces[i].LoadJSONItems(items, this);
			}
		}
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00059564 File Offset: 0x00057764
	public string GetLayoutJSON()
	{
		JSONObject jsonobject = new JSONObject();
		JSONArray jsonarray = new JSONArray();
		foreach (NookSurface nookSurface in this.Surfaces)
		{
			JSONArray jsonarray2 = new JSONArray();
			foreach (NookItem nookItem in nookSurface.Items)
			{
				jsonarray2.Add(nookItem.GetJSON());
			}
			jsonarray.Add(jsonarray2);
		}
		jsonobject.Add("Root", jsonarray);
		return jsonobject.ToString();
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x00059624 File Offset: 0x00057824
	public PlayerNook()
	{
	}

	// Token: 0x04000B56 RID: 2902
	public GameObject ClosedGroup;

	// Token: 0x04000B57 RID: 2903
	public Transform ItemHolder;

	// Token: 0x04000B58 RID: 2904
	public List<NookSurface> Surfaces = new List<NookSurface>();

	// Token: 0x04000B59 RID: 2905
	public AudioClip StartPlacementSFX;

	// Token: 0x04000B5A RID: 2906
	public AudioClip CancelPlacementSFX;

	// Token: 0x04000B5B RID: 2907
	public ParticleSystem PlacementFX;

	// Token: 0x04000B5C RID: 2908
	public ParticleSystem RemovalFX;

	// Token: 0x04000B5D RID: 2909
	public LayerMask SurfaceMask;

	// Token: 0x04000B5E RID: 2910
	public LayerMask HighlightMask;

	// Token: 0x04000B5F RID: 2911
	public Transform RootHolder;

	// Token: 0x04000B60 RID: 2912
	public Transform ObjectSpawnPt;

	// Token: 0x04000B61 RID: 2913
	public LorePage NookTutorialLorePage;

	// Token: 0x04000B62 RID: 2914
	public PlayerControl Owner;

	// Token: 0x04000B63 RID: 2915
	public List<NookItem> SpawnedItems = new List<NookItem>();

	// Token: 0x04000B64 RID: 2916
	public static PlayerNook MyNook;

	// Token: 0x04000B65 RID: 2917
	public static bool IsPlayerInside;

	// Token: 0x04000B66 RID: 2918
	public static bool IsInEditMode;

	// Token: 0x04000B67 RID: 2919
	[NonSerialized]
	public bool SnapMode;

	// Token: 0x04000B68 RID: 2920
	[NonSerialized]
	public bool CameFromUI;

	// Token: 0x04000B69 RID: 2921
	[NonSerialized]
	public NookItem HilightedItem;

	// Token: 0x04000B6A RID: 2922
	[NonSerialized]
	public NookItem HeldItem;

	// Token: 0x04000B6B RID: 2923
	private List<NookSurface> HeldValidSurfaces = new List<NookSurface>();

	// Token: 0x04000B6C RID: 2924
	private NookSurface lastSurface;
}
