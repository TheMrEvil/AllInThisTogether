using System;
using System.Collections.Generic;
using Fluxy;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class FluxyTrail : MonoBehaviour
{
	// Token: 0x060017DE RID: 6110 RVA: 0x000956DC File Offset: 0x000938DC
	private void Awake()
	{
		for (int i = 0; i < 15; i++)
		{
			FluxyTrail.TrailContainer trailContainer = new FluxyTrail.TrailContainer();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ContainerRef, base.transform);
			trailContainer.Container = gameObject.GetComponent<FluxyContainer>();
			this.Containers.Add(trailContainer);
		}
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x00095728 File Offset: 0x00093928
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.Target.transform.position = PlayerControl.myInstance.movement.GetPosition();
		ValueTuple<Vector3, Vector3> valueTuple = this.ProjectedPos(this.Target.transform.position);
		Vector3 item = valueTuple.Item1;
		Vector3 item2 = valueTuple.Item2;
		if (item != Vector3.zero && this.NearestContainerDistance(item) > this.ContainerWidth / 2f)
		{
			this.SetNextContainer(item, item2);
		}
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000957AF File Offset: 0x000939AF
	private void SetNextContainer(Vector3 pos, Vector3 up)
	{
		this.GetNextContainer(pos).SetPosition(pos, up);
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000957C0 File Offset: 0x000939C0
	private ValueTuple<Vector3, Vector3> ProjectedPos(Vector3 pos)
	{
		RaycastHit raycastHit;
		if (!Physics.Raycast(this.Target.transform.position + Vector3.up, Vector3.down, out raycastHit, 5f, this.Raymask))
		{
			return new ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.up);
		}
		return new ValueTuple<Vector3, Vector3>(raycastHit.point, raycastHit.normal);
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x00095828 File Offset: 0x00093A28
	private float NearestContainerDistance(Vector3 pos)
	{
		float num = this.ContainerWidth + 1f;
		foreach (FluxyTrail.TrailContainer trailContainer in this.Containers)
		{
			float num2 = Vector3.Distance(trailContainer.pos, pos);
			if (num2 < num)
			{
				num = num2;
			}
		}
		return num;
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x00095894 File Offset: 0x00093A94
	private FluxyTrail.TrailContainer GetNextContainer(Vector3 fromPos)
	{
		FluxyTrail.TrailContainer result = this.Containers[0];
		float num = 0f;
		foreach (FluxyTrail.TrailContainer trailContainer in this.Containers)
		{
			float num2 = Vector3.Distance(trailContainer.pos, fromPos);
			if (num2 > num)
			{
				num = num2;
				result = trailContainer;
			}
		}
		return result;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x0009590C File Offset: 0x00093B0C
	public FluxyTrail()
	{
	}

	// Token: 0x040017AD RID: 6061
	public GameObject ContainerRef;

	// Token: 0x040017AE RID: 6062
	public FluxyTarget Target;

	// Token: 0x040017AF RID: 6063
	public float ContainerWidth = 5f;

	// Token: 0x040017B0 RID: 6064
	public LayerMask Raymask;

	// Token: 0x040017B1 RID: 6065
	private List<FluxyTrail.TrailContainer> Containers = new List<FluxyTrail.TrailContainer>();

	// Token: 0x02000611 RID: 1553
	[Serializable]
	public class TrailContainer
	{
		// Token: 0x0600271D RID: 10013 RVA: 0x000D4F9C File Offset: 0x000D319C
		public void SetPosition(Vector3 p, Vector3 up)
		{
			this.pos = p;
			this.Container.transform.position = p + up * 0.1f;
			this.Container.transform.forward = up;
			this.Container.gameObject.SetActive(true);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000D4FF3 File Offset: 0x000D31F3
		public TrailContainer()
		{
		}

		// Token: 0x040029B0 RID: 10672
		public FluxyContainer Container;

		// Token: 0x040029B1 RID: 10673
		public Vector3 pos;

		// Token: 0x040029B2 RID: 10674
		public float lastTimeUsed;
	}
}
