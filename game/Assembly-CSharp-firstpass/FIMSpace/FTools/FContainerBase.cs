using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200004E RID: 78
	public abstract class FContainerBase : ScriptableObject
	{
		// Token: 0x06000227 RID: 551 RVA: 0x000122E8 File Offset: 0x000104E8
		public virtual bool Contains(UnityEngine.Object obj)
		{
			for (int i = this.ContainedAssets.Count - 1; i >= 0; i--)
			{
				if (this.ContainedAssets[i].Reference == null)
				{
					this.ContainedAssets.RemoveAt(i);
				}
				else if (this.ContainedAssets[i].Reference == obj)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00012350 File Offset: 0x00010550
		public virtual void Remove(UnityEngine.Object obj)
		{
			for (int i = this.ContainedAssets.Count - 1; i >= 0; i--)
			{
				if (this.ContainedAssets[i].Reference == null)
				{
					this.ContainedAssets.RemoveAt(i);
				}
				else if (this.ContainedAssets[i].Reference == obj)
				{
					this.ContainedAssets.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000123C2 File Offset: 0x000105C2
		public virtual void RemoveAndDestroy(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return;
			}
			this.Remove(obj);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000123D5 File Offset: 0x000105D5
		public virtual void CopyAsset(UnityEngine.Object obj, string extension = ".asset")
		{
			obj == null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000123E0 File Offset: 0x000105E0
		public virtual void Add(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return;
			}
			FContainerBase.AssetReference assetReference = new FContainerBase.AssetReference();
			assetReference.Reference = obj;
			this.ContainedAssets.Add(assetReference);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00012410 File Offset: 0x00010610
		public FContainerBase.AssetReference GetReferenceTo(UnityEngine.Object asset)
		{
			if (asset == null)
			{
				return null;
			}
			for (int i = this.ContainedAssets.Count - 1; i >= 0; i--)
			{
				if (this.ContainedAssets[i].Reference == null)
				{
					this.ContainedAssets.RemoveAt(i);
				}
				else if (this.ContainedAssets[i].Reference == asset)
				{
					return this.ContainedAssets[i];
				}
			}
			return null;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001248E File Offset: 0x0001068E
		public virtual void AddAsset(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return;
			}
			if (this.Contains(obj))
			{
				this.UnpackSingleAsset(obj);
				return;
			}
			if (!this.Contains(obj))
			{
				this.Add(obj);
			}
			FContainerBase.AddAssetTo(this, obj);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000124C2 File Offset: 0x000106C2
		public virtual void UnpackSingleAsset(UnityEngine.Object asset)
		{
			if (asset == null)
			{
				return;
			}
			FContainerBase.UnpackSingleAsset(this, asset);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000124D5 File Offset: 0x000106D5
		public virtual void UnpackAll()
		{
			FContainerBase.UnpackAll(this);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000124DD File Offset: 0x000106DD
		public void _SetDirty()
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000124DF File Offset: 0x000106DF
		public static void AddAssetTo(ScriptableObject container, UnityEngine.Object asset)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000124E1 File Offset: 0x000106E1
		public static void UnpackAll(FContainerBase container)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000124E3 File Offset: 0x000106E3
		public static void UnpackSingleAsset(FContainerBase container, UnityEngine.Object tgt)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000124E5 File Offset: 0x000106E5
		protected FContainerBase()
		{
		}

		// Token: 0x04000234 RID: 564
		public List<FContainerBase.AssetReference> ContainedAssets = new List<FContainerBase.AssetReference>();

		// Token: 0x020001A4 RID: 420
		[Serializable]
		public class AssetReference
		{
			// Token: 0x06000EE3 RID: 3811 RVA: 0x00061C75 File Offset: 0x0005FE75
			public AssetReference()
			{
			}

			// Token: 0x04000CFA RID: 3322
			public UnityEngine.Object Reference;

			// Token: 0x04000CFB RID: 3323
			public string OriginalExtension = "";
		}
	}
}
