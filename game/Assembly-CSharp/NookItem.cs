using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class NookItem : MonoBehaviour
{
	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00056E98 File Offset: 0x00055098
	public float Size
	{
		get
		{
			if (this.OverrideSize != 0f)
			{
				return this.OverrideSize;
			}
			if (this._size > 0f)
			{
				return this._size;
			}
			if (this.OverlapCollider != null)
			{
				this._size = Mathf.Max(new float[]
				{
					this.OverlapCollider.bounds.size.x,
					this.OverlapCollider.bounds.size.y,
					this.OverlapCollider.bounds.size.z
				});
			}
			else if (this.Display.Count > 0)
			{
				this._size = Mathf.Max(new float[]
				{
					this.Display[0].localBounds.size.x,
					this.Display[0].localBounds.size.y,
					this.Display[0].localBounds.size.z
				});
			}
			else
			{
				this._size = 1f;
			}
			return this._size;
		}
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00056FD8 File Offset: 0x000551D8
	private void Awake()
	{
		foreach (NookSurface nookSurface in this.Surfaces)
		{
			nookSurface.Parent = this;
		}
		this.Colliders = base.gameObject.GetAllComponents<Collider>();
		this.Colliders.Remove(this.OverlapCollider);
		if (this.OverlapCollider == null)
		{
			Debug.Log("No Overlap Collider for " + base.gameObject.name);
		}
		else
		{
			this.OverlapCollider.gameObject.layer = 14;
		}
		foreach (Collider collider in this.ExtraOverlaps)
		{
			collider.gameObject.layer = 14;
			this.Colliders.Remove(collider);
		}
		this.allOverlaps.Add(this.OverlapCollider);
		this.allOverlaps.AddRange(this.ExtraOverlaps);
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x00057100 File Offset: 0x00055300
	public void SetID(string id)
	{
		this.ID = id;
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00057109 File Offset: 0x00055309
	public void EnterPreviewMode()
	{
		this.ToggleColliders(false);
		this.SetRimMaterial(NookItem.VisibilityType.Hilight);
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x00057119 File Offset: 0x00055319
	public void ToggleHighlight(bool isHighlighted)
	{
		this.SetRimMaterial(isHighlighted ? NookItem.VisibilityType.Hilight : NookItem.VisibilityType.Default);
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00057128 File Offset: 0x00055328
	public void EnterPlacementMode(bool fromUI)
	{
		this.ToggleColliders(false);
		if (this.CurrentSurface != null)
		{
			this.ShouldReset = true;
			this.prevSurface = this.CurrentSurface;
			this.prevPos = base.transform.position;
			this.prevAngle = base.transform.localEulerAngles.y;
			this.CurrentSurface.Items.Remove(this);
			this.CurrentSurface = null;
		}
		this.Parent = null;
		if (!this.ShouldReset)
		{
			base.transform.position = PlayerNook.MyNook.ObjectSpawnPt.position;
		}
		PlayerNook.MyNook.StartPlacement(this, fromUI);
		this.SetRimMaterial(NookItem.VisibilityType.Placement);
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x000571DC File Offset: 0x000553DC
	public void PreviewPlacement(PlayerNook nook, NookSurface surface, Vector3 aimPoint)
	{
		if (surface != null)
		{
			if (PlayerNook.MyNook.SnapMode)
			{
				aimPoint = PlayerNook.MyNook.GetSnapPoint(aimPoint, surface.Surface);
			}
			aimPoint = surface.GetSurfacePoint(aimPoint);
			this.AlignOnSurface(surface);
			Transform pivotPoint = this.GetPivotPoint(surface.Surface);
			if (pivotPoint != base.transform)
			{
				Vector3 b = base.transform.position - pivotPoint.position;
				base.transform.position = aimPoint + b;
			}
			else
			{
				base.transform.position = aimPoint;
			}
			this.previewSurface = surface;
			bool flag = surface.IsInBounds(this);
			bool flag2 = surface.AnyIntersections(this);
			this.IsValid = (flag && !flag2);
			this.SetRimMaterial(this.IsValid ? NookItem.VisibilityType.Placement : NookItem.VisibilityType.Invalid);
		}
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x000572B0 File Offset: 0x000554B0
	public void AlignOnSurface(NookSurface surface)
	{
		Transform pivotPoint = this.GetPivotPoint(surface.Surface);
		if (surface.Surface == NookSurface.SurfaceType.Flat)
		{
			base.transform.localEulerAngles = new Vector3(0f, this.Angle, 0f);
			return;
		}
		Quaternion quaternion = Quaternion.LookRotation(surface.transform.forward, surface.transform.up);
		if (pivotPoint != base.transform)
		{
			quaternion *= Quaternion.Inverse(pivotPoint.localRotation);
		}
		base.transform.rotation = quaternion;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0005733C File Offset: 0x0005553C
	public void CancelPlacement(PlayerNook n)
	{
		if (this.ShouldReset && this.prevSurface != null)
		{
			base.transform.position = this.prevPos;
			this.Angle = this.prevAngle;
			if (this.prevSurface.Surface == NookSurface.SurfaceType.Flat)
			{
				base.transform.localEulerAngles = new Vector3(0f, this.Angle, 0f);
			}
			PlayerNook.MyNook.HeldItem = null;
			this.PlaceOnSurface(n, this.prevSurface, true);
		}
		else
		{
			this.RemoveSelf(false);
		}
		AudioManager.PlayInterfaceSFX(PlayerNook.MyNook.CancelPlacementSFX, 1f, 0f);
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x000573E8 File Offset: 0x000555E8
	public void PlaceOnSurface(PlayerNook n, NookSurface surface, bool withFX)
	{
		this.nook = n;
		this.CurrentSurface = surface;
		this.CurrentSurface.Items.Add(this);
		this.Parent = surface.Parent;
		if (this.Parent != null)
		{
			base.transform.SetParent(this.Parent.transform);
		}
		else
		{
			base.transform.SetParent(n.RootHolder);
		}
		this.Angle = base.transform.localEulerAngles.y;
		this.SetRimMaterial(NookItem.VisibilityType.Default);
		this.ToggleColliders(true);
		if (withFX)
		{
			PlayerNook.MyNook.PlayPlacementFX(this);
		}
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0005748C File Offset: 0x0005568C
	public void RemoveSelf(bool withFX)
	{
		if (this.CurrentSurface != null)
		{
			this.CurrentSurface.Items.Remove(this);
		}
		foreach (NookSurface nookSurface in this.Surfaces)
		{
			for (int i = nookSurface.Items.Count - 1; i >= 0; i--)
			{
				if (!(nookSurface.Items[i] == null))
				{
					nookSurface.Items[i].RemoveSelf(false);
				}
			}
		}
		if (withFX)
		{
			PlayerNook.MyNook.PlayRemovalFX(this);
		}
		if (this.nook != null)
		{
			this.nook.SpawnedItems.Remove(this);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x00057570 File Offset: 0x00055770
	public bool Overlaps(NookItem item)
	{
		if (item == this || item == null)
		{
			return false;
		}
		if (item.OverlapCollider == null || this.OverlapCollider == null || this.NoOverlap || item.NoOverlap)
		{
			return false;
		}
		foreach (Collider collider in this.allOverlaps)
		{
			foreach (Collider collider2 in item.allOverlaps)
			{
				Vector3 vector;
				float num;
				if (Physics.ComputePenetration(collider, collider.transform.position, collider.transform.rotation, collider2, collider2.transform.position, collider2.transform.rotation, out vector, out num))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0005767C File Offset: 0x0005587C
	private void ToggleColliders(bool active)
	{
		foreach (Collider collider in this.Colliders)
		{
			collider.enabled = active;
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000576D0 File Offset: 0x000558D0
	private Transform GetPivotPoint(NookSurface.SurfaceType pivot)
	{
		foreach (NookItem.Pivot pivot2 in this.Pivots)
		{
			if (pivot2.Type == pivot)
			{
				return pivot2.Point;
			}
		}
		return base.transform;
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00057738 File Offset: 0x00055938
	public void Calculate()
	{
		this.OverrideSize = this.Size;
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00057748 File Offset: 0x00055948
	public void AddValidSubsurfaces(NookItem item, ref List<NookSurface> validSurfaces)
	{
		foreach (NookSurface nookSurface in this.Surfaces)
		{
			if (nookSurface.CanHold(item))
			{
				validSurfaces.Add(nookSurface);
			}
			nookSurface.AddValidSubsurfaces(item, ref validSurfaces);
		}
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x000577B0 File Offset: 0x000559B0
	public void SetRimMaterial(NookItem.VisibilityType type)
	{
		foreach (MeshRenderer meshRenderer in this.Display)
		{
			List<Material> list = new List<Material>();
			meshRenderer.GetSharedMaterials(list);
			switch (type)
			{
			case NookItem.VisibilityType.Default:
				list.Remove(NookDB.DB.HilightMaterial);
				list.Remove(NookDB.DB.MovementMaterial);
				list.Remove(NookDB.DB.InvalidMaterial);
				break;
			case NookItem.VisibilityType.Hilight:
				list.Remove(NookDB.DB.MovementMaterial);
				list.Remove(NookDB.DB.InvalidMaterial);
				if (!list.Contains(NookDB.DB.HilightMaterial))
				{
					list.Add(NookDB.DB.HilightMaterial);
				}
				break;
			case NookItem.VisibilityType.Placement:
				list.Remove(NookDB.DB.HilightMaterial);
				list.Remove(NookDB.DB.InvalidMaterial);
				if (!list.Contains(NookDB.DB.MovementMaterial))
				{
					list.Add(NookDB.DB.MovementMaterial);
				}
				break;
			case NookItem.VisibilityType.Invalid:
				list.Remove(NookDB.DB.HilightMaterial);
				list.Remove(NookDB.DB.MovementMaterial);
				if (!list.Contains(NookDB.DB.InvalidMaterial))
				{
					list.Add(NookDB.DB.InvalidMaterial);
				}
				break;
			}
			meshRenderer.sharedMaterials = list.ToArray();
		}
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00057950 File Offset: 0x00055B50
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		foreach (Transform transform in this.BoundPoints)
		{
			if (transform != null)
			{
				Gizmos.DrawSphere(transform.position, 0.05f);
			}
		}
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x000579C0 File Offset: 0x00055BC0
	public static void CreateItem(JSONNode json, NookSurface surface, PlayerNook nook)
	{
		string text = json.GetValueOrDefault("id", "");
		float angle = 0f;
		if (json.HasKey("angle"))
		{
			angle = json.GetValueOrDefault("angle", 0);
		}
		Vector3 localPosition = json.GetValueOrDefault("pos", "").ToString().ToVector3();
		NookDB.NookObject item = NookDB.GetItem(text);
		if (item == null)
		{
			Debug.LogError("Invalid Item: " + text);
			return;
		}
		Transform parent = (surface.Parent == null) ? nook.RootHolder : surface.Parent.transform;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(item.Prefab, parent);
		gameObject.transform.localPosition = localPosition;
		NookItem component = gameObject.GetComponent<NookItem>();
		component.SetID(text);
		component.Angle = angle;
		component.AlignOnSurface(surface);
		component.PlaceOnSurface(nook, surface, false);
		nook.SpawnedItems.Add(component);
		if (json.HasKey("surfaces"))
		{
			JSONArray jsonarray = json["surfaces"] as JSONArray;
			for (int i = 0; i < jsonarray.Count; i++)
			{
				JSONArray items = jsonarray[i] as JSONArray;
				component.Surfaces[i].LoadJSONItems(items, nook);
			}
		}
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x00057B1C File Offset: 0x00055D1C
	public JSONObject GetJSON()
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.Add("id", this.ID);
		if (this.CurrentSurface != null && this.CurrentSurface.Surface == NookSurface.SurfaceType.Flat)
		{
			jsonobject.Add("angle", this.Angle);
		}
		jsonobject.Add("pos", base.transform.localPosition.ToDetailedString());
		if (this.Surfaces.Count > 0)
		{
			JSONArray jsonarray = new JSONArray();
			foreach (NookSurface nookSurface in this.Surfaces)
			{
				JSONArray jsonarray2 = new JSONArray();
				foreach (NookItem nookItem in nookSurface.Items)
				{
					JSONObject json = nookItem.GetJSON();
					jsonarray2.Add(json);
				}
				jsonarray.Add(jsonarray2);
			}
			jsonobject.Add("surfaces", jsonarray);
		}
		return jsonobject;
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00057C50 File Offset: 0x00055E50
	public NookItem()
	{
	}

	// Token: 0x04000B32 RID: 2866
	public NookSurface.SurfaceType AllowedSurfaces = NookSurface.SurfaceType.Flat;

	// Token: 0x04000B33 RID: 2867
	public NookItem.ItemMaterial Material;

	// Token: 0x04000B34 RID: 2868
	public List<MeshRenderer> Display;

	// Token: 0x04000B35 RID: 2869
	public List<Transform> BoundPoints = new List<Transform>();

	// Token: 0x04000B36 RID: 2870
	public Collider OverlapCollider;

	// Token: 0x04000B37 RID: 2871
	public List<Collider> ExtraOverlaps;

	// Token: 0x04000B38 RID: 2872
	public bool NoOverlap;

	// Token: 0x04000B39 RID: 2873
	public List<NookItem.Pivot> Pivots = new List<NookItem.Pivot>();

	// Token: 0x04000B3A RID: 2874
	public List<NookSurface> Surfaces = new List<NookSurface>();

	// Token: 0x04000B3B RID: 2875
	private PlayerNook nook;

	// Token: 0x04000B3C RID: 2876
	public float OverrideSize;

	// Token: 0x04000B3D RID: 2877
	private float _size;

	// Token: 0x04000B3E RID: 2878
	[NonSerialized]
	public List<Collider> allOverlaps = new List<Collider>();

	// Token: 0x04000B3F RID: 2879
	private List<Collider> Colliders = new List<Collider>();

	// Token: 0x04000B40 RID: 2880
	public NookSurface CurrentSurface;

	// Token: 0x04000B41 RID: 2881
	public NookItem Parent;

	// Token: 0x04000B42 RID: 2882
	[NonSerialized]
	public float Angle;

	// Token: 0x04000B43 RID: 2883
	[NonSerialized]
	public bool IsValid;

	// Token: 0x04000B44 RID: 2884
	[NonSerialized]
	public NookSurface previewSurface;

	// Token: 0x04000B45 RID: 2885
	private bool ShouldReset;

	// Token: 0x04000B46 RID: 2886
	private NookSurface prevSurface;

	// Token: 0x04000B47 RID: 2887
	private Vector3 prevPos;

	// Token: 0x04000B48 RID: 2888
	private float prevAngle;

	// Token: 0x04000B49 RID: 2889
	private string ID;

	// Token: 0x0200052E RID: 1326
	[Serializable]
	public class Pivot
	{
		// Token: 0x06002402 RID: 9218 RVA: 0x000CCA8C File Offset: 0x000CAC8C
		public Pivot()
		{
		}

		// Token: 0x04002635 RID: 9781
		public NookSurface.SurfaceType Type = NookSurface.SurfaceType.Flat;

		// Token: 0x04002636 RID: 9782
		public Transform Point;
	}

	// Token: 0x0200052F RID: 1327
	public enum VisibilityType
	{
		// Token: 0x04002638 RID: 9784
		Default,
		// Token: 0x04002639 RID: 9785
		Hilight,
		// Token: 0x0400263A RID: 9786
		Placement,
		// Token: 0x0400263B RID: 9787
		Invalid
	}

	// Token: 0x02000530 RID: 1328
	public enum ItemMaterial
	{
		// Token: 0x0400263D RID: 9789
		Wood,
		// Token: 0x0400263E RID: 9790
		Metal,
		// Token: 0x0400263F RID: 9791
		Glass,
		// Token: 0x04002640 RID: 9792
		Cloth
	}
}
