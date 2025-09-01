using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000106 RID: 262
	public class GenericPoser : Poser
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0004F708 File Offset: 0x0004D908
		[ContextMenu("Auto-Mapping")]
		public override void AutoMapping()
		{
			if (this.poseRoot == null)
			{
				this.maps = new GenericPoser.Map[0];
				return;
			}
			this.maps = new GenericPoser.Map[0];
			Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
			Transform[] componentsInChildren2 = this.poseRoot.GetComponentsInChildren<Transform>();
			for (int i = 1; i < componentsInChildren.Length; i++)
			{
				Transform targetNamed = this.GetTargetNamed(componentsInChildren[i].name, componentsInChildren2);
				if (targetNamed != null)
				{
					Array.Resize<GenericPoser.Map>(ref this.maps, this.maps.Length + 1);
					this.maps[this.maps.Length - 1] = new GenericPoser.Map(componentsInChildren[i], targetNamed);
				}
			}
			this.StoreDefaultState();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0004F7B0 File Offset: 0x0004D9B0
		protected override void InitiatePoser()
		{
			this.StoreDefaultState();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004F7B8 File Offset: 0x0004D9B8
		protected override void UpdatePoser()
		{
			if (this.weight <= 0f)
			{
				return;
			}
			if (this.localPositionWeight <= 0f && this.localRotationWeight <= 0f)
			{
				return;
			}
			if (this.poseRoot == null)
			{
				return;
			}
			float localRotationWeight = this.localRotationWeight * this.weight;
			float localPositionWeight = this.localPositionWeight * this.weight;
			for (int i = 0; i < this.maps.Length; i++)
			{
				this.maps[i].Update(localRotationWeight, localPositionWeight);
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004F83C File Offset: 0x0004DA3C
		protected override void FixPoserTransforms()
		{
			for (int i = 0; i < this.maps.Length; i++)
			{
				this.maps[i].FixTransform();
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0004F86C File Offset: 0x0004DA6C
		private void StoreDefaultState()
		{
			for (int i = 0; i < this.maps.Length; i++)
			{
				this.maps[i].StoreDefaultState();
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0004F89C File Offset: 0x0004DA9C
		private Transform GetTargetNamed(string tName, Transform[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].name == tName)
				{
					return array[i];
				}
			}
			return null;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0004F8CC File Offset: 0x0004DACC
		public GenericPoser()
		{
		}

		// Token: 0x0400092B RID: 2347
		public GenericPoser.Map[] maps;

		// Token: 0x02000212 RID: 530
		[Serializable]
		public class Map
		{
			// Token: 0x0600112B RID: 4395 RVA: 0x0006BAE8 File Offset: 0x00069CE8
			public Map(Transform bone, Transform target)
			{
				this.bone = bone;
				this.target = target;
				this.StoreDefaultState();
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x0006BB04 File Offset: 0x00069D04
			public void StoreDefaultState()
			{
				this.defaultLocalPosition = this.bone.localPosition;
				this.defaultLocalRotation = this.bone.localRotation;
			}

			// Token: 0x0600112D RID: 4397 RVA: 0x0006BB28 File Offset: 0x00069D28
			public void FixTransform()
			{
				this.bone.localPosition = this.defaultLocalPosition;
				this.bone.localRotation = this.defaultLocalRotation;
			}

			// Token: 0x0600112E RID: 4398 RVA: 0x0006BB4C File Offset: 0x00069D4C
			public void Update(float localRotationWeight, float localPositionWeight)
			{
				this.bone.localRotation = Quaternion.Lerp(this.bone.localRotation, this.target.localRotation, localRotationWeight);
				this.bone.localPosition = Vector3.Lerp(this.bone.localPosition, this.target.localPosition, localPositionWeight);
			}

			// Token: 0x04000FD6 RID: 4054
			public Transform bone;

			// Token: 0x04000FD7 RID: 4055
			public Transform target;

			// Token: 0x04000FD8 RID: 4056
			private Vector3 defaultLocalPosition;

			// Token: 0x04000FD9 RID: 4057
			private Quaternion defaultLocalRotation;
		}
	}
}
