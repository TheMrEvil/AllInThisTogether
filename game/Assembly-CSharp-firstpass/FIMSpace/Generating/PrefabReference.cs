using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.Generating
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class PrefabReference
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00018C46 File Offset: 0x00016E46
		public GameObject CoreGameObject
		{
			get
			{
				return this.Prefab;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00018C4E File Offset: 0x00016E4E
		public Collider CoreCollider
		{
			get
			{
				return this.MainCollider;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00018C56 File Offset: 0x00016E56
		public GameObject GameObject
		{
			get
			{
				if (this.tempReplacePrefab != null)
				{
					return this.tempReplacePrefab;
				}
				return this.Prefab;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00018C73 File Offset: 0x00016E73
		public Collider Collider
		{
			get
			{
				if (this.tempReplaceCollider != null)
				{
					return this.tempReplaceCollider;
				}
				return this.MainCollider;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00018C90 File Offset: 0x00016E90
		public Texture Preview
		{
			get
			{
				if (this.Prefab == null)
				{
					this.tex = null;
					return null;
				}
				if (this.tex == null || this.id != this.Prefab.GetInstanceID())
				{
					this.id = this.Prefab.GetInstanceID();
				}
				return this.tex;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00018CEC File Offset: 0x00016EEC
		protected virtual void DrawGUIWithPrefab(Color color, int previewSize = 72, string predicate = "", Action clickCallback = null, Action removeCallback = null, bool drawThumbnail = true, bool drawPrefabField = true)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00018CEE File Offset: 0x00016EEE
		protected virtual void DrawGUIWithoutPrefab(int previewSize = 72, string predicate = "", Action removeCallback = null, bool drawPrefabField = true)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public virtual void OnPrefabChanges()
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00018CF2 File Offset: 0x00016EF2
		public static void DrawPrefabField(PrefabReference prefabRef, Color defaultColor, string predicate = "", int previewSize = 72, Action clickCallback = null, Action removeCallback = null, bool drawThumbnail = true, UnityEngine.Object toDiry = null, bool drawPrefabField = true, bool drawAdditionalButtons = true)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00018CF4 File Offset: 0x00016EF4
		public static void DrawPrefabsList<T>(List<T> list, ref bool foldout, ref int selected, ref bool thumbnails, Color defaultC, Color selectedC, float viewWidth = 500f, int previewSize = 72, bool searchButtons = false, UnityEngine.Object toDirty = null, bool allowAdding = true) where T : PrefabReference, new()
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00018CF8 File Offset: 0x00016EF8
		public Mesh GetMesh(bool refresh = false)
		{
			if (this.Prefab == null)
			{
				return null;
			}
			if (refresh)
			{
				this._refMesh = null;
			}
			else if (this._refMesh)
			{
				if (this.MainCollider == null)
				{
					this.GetCollider();
				}
				return this._refMesh;
			}
			List<SkinnedMeshRenderer> list = FTransformMethods.FindComponentsInAllChildren<SkinnedMeshRenderer>(this.Prefab.transform, false, false);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] && list[i].sharedMesh)
				{
					this._refMesh = list[i].sharedMesh;
					if (this.MainCollider == null)
					{
						this.GetCollider();
					}
					return this._refMesh;
				}
			}
			List<MeshFilter> list2 = FTransformMethods.FindComponentsInAllChildren<MeshFilter>(this.Prefab.transform, false, false);
			for (int j = 0; j < list2.Count; j++)
			{
				if (list2[j] && list2[j].sharedMesh)
				{
					this._refMesh = list2[j].sharedMesh;
					if (this.MainCollider == null)
					{
						this.GetCollider();
					}
					return this._refMesh;
				}
			}
			if (this.MainCollider == null)
			{
				this.MainCollider = FTransformMethods.FindComponentInAllChildren<Collider>(this.Prefab.transform);
			}
			return this._refMesh;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00018E5C File Offset: 0x0001705C
		public Collider GetCollider()
		{
			if (this.Prefab == null)
			{
				return null;
			}
			if (this._refCol)
			{
				if (this.MainCollider == null)
				{
					this.MainCollider = this._refCol;
				}
				return this._refCol;
			}
			List<Collider> list = FTransformMethods.FindComponentsInAllChildren<Collider>(this.Prefab.transform, false, false);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i])
				{
					this._refCol = list[i];
					if (this.MainCollider == null)
					{
						this.MainCollider = this._refCol;
					}
					return this._refCol;
				}
			}
			if (this._refCol == null)
			{
				this._refCol = this.Prefab.GetComponent<Collider>();
			}
			if (this.MainCollider == null)
			{
				this.MainCollider = this._refCol;
			}
			return this._refCol;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00018F46 File Offset: 0x00017146
		public void SetPrefab(GameObject pf)
		{
			this.Prefab = pf;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00018F4F File Offset: 0x0001714F
		public void SetCollider(Collider pf)
		{
			this.MainCollider = pf;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00018F58 File Offset: 0x00017158
		public void TemporaryReplace(GameObject tempRepl)
		{
			if (tempRepl == null)
			{
				this.tempReplacePrefab = null;
				this.tempReplaceCollider = null;
				return;
			}
			this.tempReplacePrefab = tempRepl;
			this.tempReplaceCollider = tempRepl.GetComponentInChildren<Collider>();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00018F85 File Offset: 0x00017185
		public PrefabReference()
		{
		}

		// Token: 0x040002F5 RID: 757
		[SerializeField]
		private GameObject Prefab;

		// Token: 0x040002F6 RID: 758
		private GameObject tempReplacePrefab;

		// Token: 0x040002F7 RID: 759
		[SerializeField]
		private Collider MainCollider;

		// Token: 0x040002F8 RID: 760
		private Collider tempReplaceCollider;

		// Token: 0x040002F9 RID: 761
		private int id;

		// Token: 0x040002FA RID: 762
		public int subID;

		// Token: 0x040002FB RID: 763
		private Texture tex;

		// Token: 0x040002FC RID: 764
		public static GUILayoutOption[] opt;

		// Token: 0x040002FD RID: 765
		public static GUILayoutOption[] opt2;

		// Token: 0x040002FE RID: 766
		public static GUILayoutOption[] opt3;

		// Token: 0x040002FF RID: 767
		public static bool StopReloadLayoutOptions;

		// Token: 0x04000300 RID: 768
		[HideInInspector]
		[SerializeField]
		protected Mesh _refMesh;

		// Token: 0x04000301 RID: 769
		[HideInInspector]
		[SerializeField]
		protected Collider _refCol;
	}
}
