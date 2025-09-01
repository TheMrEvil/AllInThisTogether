using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class AIFlyingMovement : AIMovement
{
	// Token: 0x0600035D RID: 861 RVA: 0x0001C5BC File Offset: 0x0001A7BC
	public override void Setup()
	{
		this.Navflight = new GameObject("_FlightNavRoot").transform;
		this.Navflight.position = base.transform.position;
		base.StartCoroutine("InitNav");
		System.Random random = new System.Random(this.controller.net.view.ViewID);
		this.movementOffset = new Vector3((float)random.Next(-100, 100) / 100f, (float)random.Next(-100, 100) / 100f, (float)random.Next(-100, 100) / 100f);
		base.Setup();
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001C65F File Offset: 0x0001A85F
	private IEnumerator InitNav()
	{
		while (!FlyingNavmesh.IsGenerated)
		{
			yield return true;
		}
		this.CurrentNode = new AIFlyingMovement.AgentPosition(new AIFlyingMovement.FlightPoint(FlyingNavmesh.instance.NearestNode(this.Navflight.position)));
		yield break;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0001C66E File Offset: 0x0001A86E
	public override Vector3 GetVelocity()
	{
		return this.velocity;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001C676 File Offset: 0x0001A876
	public override Vector3 GetPosition()
	{
		if (!(this.Navflight == null))
		{
			return this.Navflight.position;
		}
		return Vector3.one.INVALID();
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001C69C File Offset: 0x0001A89C
	public override bool SetTargetPoint(PotentialNavPoint pt)
	{
		if (pt.flynav != null)
		{
			return this.SetTargetPoint(pt.flynav);
		}
		return this.SetTargetPoint(pt.pos);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
	public override bool SetTargetPoint(Vector3 pt)
	{
		FlynavNode flynavNode = FlyingNavmesh.instance.NearestNode(pt);
		if (flynavNode == null)
		{
			return false;
		}
		if (!this.SetTargetPoint(flynavNode))
		{
			return false;
		}
		this.Path.Add(new AIFlyingMovement.FlightPoint(pt));
		return true;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001C6FC File Offset: 0x0001A8FC
	private bool SetTargetPoint(FlynavNode node)
	{
		AIFlyingMovement.AgentPosition currentNode = this.CurrentNode;
		List<AIFlyingMovement.FlightPoint> list = this.GeneratePath((currentNode != null) ? currentNode.LastNode : null, node);
		if (list.Count <= 0)
		{
			return false;
		}
		this.PathIndex = 0;
		this.Path = list;
		this.targetPoint = node.Position;
		return true;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001C74C File Offset: 0x0001A94C
	public void SetRandomTarget()
	{
		FlynavNode flynavNode = FlyingNavmesh.instance.Navmesh[UnityEngine.Random.Range(0, FlyingNavmesh.instance.Navmesh.Count)];
		this.SetTargetPoint(flynavNode.Position);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0001C78C File Offset: 0x0001A98C
	public override bool CanReachPosition(Vector3 point)
	{
		if (!FlyingNavmesh.IsGenerated)
		{
			return false;
		}
		FlynavNode flynavNode = FlyingNavmesh.instance.NearestNode(point);
		return flynavNode != null && this.GeneratePath(this.CurrentNode.LastNode, flynavNode).Count > 0;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
	public override List<PotentialNavPoint> GetAvailablePoints()
	{
		List<PotentialNavPoint> list = new List<PotentialNavPoint>();
		foreach (FlynavNode nav in FlyingNavmesh.instance.Navmesh)
		{
			list.Add(new PotentialNavPoint(nav));
		}
		return list;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0001C834 File Offset: 0x0001AA34
	public override bool IsMoving()
	{
		return this.Path.Count > 0 && this.CurrentNode != null && this.CurrentNode.lerpAmt < 1f;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0001C860 File Offset: 0x0001AA60
	public override Vector3 TransformPointToNavigable(Vector3 pt)
	{
		return FlyingNavmesh.instance.NearestNode(pt).Position;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0001C872 File Offset: 0x0001AA72
	public override void CancelMovement()
	{
		this.PathIndex = 0;
		this.Path.Clear();
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0001C888 File Offset: 0x0001AA88
	public override void Update()
	{
		base.Update();
		if (!FlyingNavmesh.IsGenerated || this.controller.IsDead)
		{
			return;
		}
		float currentSpeed = base.CurrentSpeed;
		if (!this.controller.IsMine)
		{
			this.Navflight.position = this.wantPosition;
			base.transform.position = Vector3.SmoothDamp(base.transform.position, this.GetPosition(), ref this.tVel, 0.5f, currentSpeed * 1.5f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.wantRot, Time.deltaTime * 9f);
			if (this.CurrentNode == null || Vector3.Distance(base.transform.position, this.CurrentNode.NodeA.Position) > 10f)
			{
				this.CurrentNode = new AIFlyingMovement.AgentPosition(new AIFlyingMovement.FlightPoint(FlyingNavmesh.instance.NearestNode(this.Navflight.position)));
			}
			return;
		}
		if (this.Path.Count > 0)
		{
			if (this.PathIndex < this.Path.Count)
			{
				if (this.CurrentNode.lerpAmt >= 1f)
				{
					this.CurrentNode.NextNode(this.Path[this.PathIndex]);
					this.PathIndex++;
				}
			}
			else
			{
				this.Path.Clear();
			}
		}
		this.lastPoint = this.GetPosition();
		if (this.CurrentNode != null)
		{
			this.CurrentNode.MoveForward(currentSpeed);
			this.Navflight.position = this.CurrentNode.CurrentPosition();
		}
		this.velocity = (this.GetPosition() - this.lastPoint) / Time.deltaTime;
		base.transform.position = Vector3.SmoothDamp(base.transform.position, this.GetPosition(), ref this.tVel, 0.5f, currentSpeed * 1.5f);
		if (this.controller.IsMine)
		{
			if (this.tVel.magnitude > 0f && this.IsMoving())
			{
				this.flyingForward = this.tVel.normalized;
			}
			else if (this.FaceTargetWhenIdle && base.Control.currentTarget != null && !this.IsMoving() && !base.Control.IsUsingActiveAbility())
			{
				this.flyingForward = (base.Control.currentTarget.movement.GetPosition() - this.GetPosition()).normalized;
			}
			if (this.flyingForward.magnitude > 0f && !this.controller.IsDead)
			{
				Vector3 newForward = Vector3.RotateTowards(this.GetForward(), this.flyingForward, this.RotationSpeed * 0.017453292f * Time.deltaTime, 1f);
				this.SetForward(newForward, false);
				return;
			}
		}
		else
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.wantRot, Time.deltaTime * 8f);
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0001CB98 File Offset: 0x0001AD98
	public List<AIFlyingMovement.FlightPoint> GeneratePath(FlynavNode start, FlynavNode goal)
	{
		if (this.Navflight == null || !FlyingNavmesh.IsGenerated)
		{
			return new List<AIFlyingMovement.FlightPoint>();
		}
		Dictionary<FlynavNode, FlynavNode> dictionary = new Dictionary<FlynavNode, FlynavNode>();
		Dictionary<FlynavNode, float> dictionary2 = new Dictionary<FlynavNode, float>();
		PriorityQueue<FlynavNode> priorityQueue = new PriorityQueue<FlynavNode>();
		if (start == null)
		{
			start = FlyingNavmesh.instance.NearestNode(this.Navflight.position);
		}
		priorityQueue.Enqueue(start, 0f);
		dictionary.Add(start, start);
		dictionary2.Add(start, 0f);
		FlynavNode flynavNode;
		while (priorityQueue.Count > 0)
		{
			flynavNode = priorityQueue.Dequeue();
			if (flynavNode == goal)
			{
				break;
			}
			foreach (FlynavNode.Edge edge in flynavNode.Edges)
			{
				FlynavNode node = edge.Node;
				float num = dictionary2[flynavNode] + edge.cost;
				if (!dictionary2.ContainsKey(node) || num < dictionary2[node])
				{
					if (dictionary2.ContainsKey(node))
					{
						dictionary2.Remove(node);
						dictionary.Remove(node);
					}
					dictionary2.Add(node, num);
					dictionary.Add(node, flynavNode);
					float priority = num + Vector3.Distance(node, goal);
					priorityQueue.Enqueue(node, priority);
				}
			}
		}
		List<AIFlyingMovement.FlightPoint> list = new List<AIFlyingMovement.FlightPoint>();
		flynavNode = goal;
		while (!flynavNode.Equals(start))
		{
			if (!dictionary.ContainsKey(flynavNode))
			{
				MonoBehaviour.print("No path found from " + start.Position.ToString() + " to " + goal.Position.ToString());
				return new List<AIFlyingMovement.FlightPoint>();
			}
			list.Add(new AIFlyingMovement.FlightPoint(flynavNode));
			flynavNode = dictionary[flynavNode];
		}
		list.Add(new AIFlyingMovement.FlightPoint(start));
		list.Reverse();
		return list;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0001CD80 File Offset: 0x0001AF80
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		base.transform.position = point;
		base.transform.forward = forward;
		this.Path.Clear();
		if (!FlyingNavmesh.IsGenerated)
		{
			return;
		}
		this.CurrentNode = new AIFlyingMovement.AgentPosition(new AIFlyingMovement.FlightPoint(FlyingNavmesh.instance.NearestNode(this.Navflight.position)));
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0001CDDD File Offset: 0x0001AFDD
	public override NavVisionPoint GetCurrentNavPoint()
	{
		AIFlyingMovement.AgentPosition currentNode = this.CurrentNode;
		if (currentNode == null)
		{
			return null;
		}
		return currentNode.LastNode.VisionPoint;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0001CDF5 File Offset: 0x0001AFF5
	public override Vector3 GetTargetPoint()
	{
		return this.targetPoint;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001CDFD File Offset: 0x0001AFFD
	public override Vector3 NearestLOSPoint(EntityControl targ, Vector2 distRange)
	{
		return FlyingNavmesh.instance.NearestNode(targ.movement.GetPosition());
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001CE19 File Offset: 0x0001B019
	public override void SetForward(Vector3 newForward, bool fromAbility)
	{
		base.transform.forward = newForward;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001CE27 File Offset: 0x0001B027
	private void OnDestroy()
	{
		if (this.Navflight != null)
		{
			UnityEngine.Object.DestroyImmediate(this.Navflight.gameObject);
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001CE48 File Offset: 0x0001B048
	private void OnDrawGizmos()
	{
		if (!this.DebugPath)
		{
			return;
		}
		if (this.Path.Count > 1)
		{
			Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
			for (int i = 1; i < this.Path.Count; i++)
			{
				Gizmos.DrawLine(this.Path[i].Position, this.Path[i - 1].Position);
			}
		}
		if (this.Navflight != null)
		{
			Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
			Gizmos.DrawWireSphere(this.Navflight.position, 0.75f);
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0001CF0A File Offset: 0x0001B10A
	public AIFlyingMovement()
	{
	}

	// Token: 0x04000349 RID: 841
	private Transform Navflight;

	// Token: 0x0400034A RID: 842
	private AIFlyingMovement.AgentPosition CurrentNode;

	// Token: 0x0400034B RID: 843
	[NonSerialized]
	private List<AIFlyingMovement.FlightPoint> Path = new List<AIFlyingMovement.FlightPoint>();

	// Token: 0x0400034C RID: 844
	[Tooltip("Degrees per Second")]
	public float RotationSpeed = 45f;

	// Token: 0x0400034D RID: 845
	public bool DebugPath;

	// Token: 0x0400034E RID: 846
	[NonSerialized]
	public Vector3 targetPoint;

	// Token: 0x0400034F RID: 847
	private Vector3 velocity;

	// Token: 0x04000350 RID: 848
	private int PathIndex;

	// Token: 0x04000351 RID: 849
	private Vector3 lastPoint;

	// Token: 0x04000352 RID: 850
	private Vector3 flyingForward;

	// Token: 0x04000353 RID: 851
	private Vector3 tVel;

	// Token: 0x02000483 RID: 1155
	public class FlightPoint
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000C380A File Offset: 0x000C1A0A
		public Vector3 Position
		{
			get
			{
				if (this.NavNode != null)
				{
					return this.NavNode.Position;
				}
				return this.PointOverride;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000C3826 File Offset: 0x000C1A26
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x000C382E File Offset: 0x000C1A2E
		public FlynavNode NavNode
		{
			[CompilerGenerated]
			get
			{
				return this.<NavNode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NavNode>k__BackingField = value;
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000C3837 File Offset: 0x000C1A37
		public FlightPoint(FlynavNode node)
		{
			this.NavNode = node;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000C3846 File Offset: 0x000C1A46
		public FlightPoint(Vector3 point)
		{
			this.PointOverride = point;
		}

		// Token: 0x040022EE RID: 8942
		[CompilerGenerated]
		private FlynavNode <NavNode>k__BackingField;

		// Token: 0x040022EF RID: 8943
		private Vector3 PointOverride;
	}

	// Token: 0x02000484 RID: 1156
	[Serializable]
	public class AgentPosition
	{
		// Token: 0x060021B9 RID: 8633 RVA: 0x000C3858 File Offset: 0x000C1A58
		public AgentPosition(AIFlyingMovement.FlightPoint Node)
		{
			this.LastNode = Node.NavNode;
			this.NodeA = Node;
			this.NodeB = Node;
			this.lerpAmt = 0f;
			this.debugA = this.NodeA.Position;
			this.debugB = this.NodeB.Position;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000C38B4 File Offset: 0x000C1AB4
		public void NextNode(AIFlyingMovement.FlightPoint Node)
		{
			this.LastNode = this.NodeA.NavNode;
			this.NodeA = this.NodeB;
			this.NodeB = Node;
			if (this.LastNode == null || Node.NavNode != null)
			{
				this.LastNode = Node.NavNode;
			}
			this.lerpAmt = 0f;
			this.debugA = this.NodeA.Position;
			this.debugB = this.NodeB.Position;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000C392E File Offset: 0x000C1B2E
		public Vector3 CurrentPosition()
		{
			return Vector3.Lerp(this.NodeA.Position, this.NodeB.Position, this.lerpAmt);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000C3954 File Offset: 0x000C1B54
		public void MoveForward(float speed)
		{
			if (this.NodeA == this.NodeB)
			{
				this.lerpAmt = 1f;
				return;
			}
			float num = Vector3.Distance(this.NodeA.Position, this.NodeB.Position);
			this.lerpAmt += speed / num * Time.deltaTime;
			this.lerpAmt = Mathf.Clamp01(this.lerpAmt);
		}

		// Token: 0x040022F0 RID: 8944
		public FlynavNode LastNode;

		// Token: 0x040022F1 RID: 8945
		public AIFlyingMovement.FlightPoint NodeA;

		// Token: 0x040022F2 RID: 8946
		public AIFlyingMovement.FlightPoint NodeB;

		// Token: 0x040022F3 RID: 8947
		public Vector3 debugA;

		// Token: 0x040022F4 RID: 8948
		public Vector3 debugB;

		// Token: 0x040022F5 RID: 8949
		public float lerpAmt;
	}

	// Token: 0x02000485 RID: 1157
	[CompilerGenerated]
	private sealed class <InitNav>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021BD RID: 8637 RVA: 0x000C39BE File Offset: 0x000C1BBE
		[DebuggerHidden]
		public <InitNav>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000C39CD File Offset: 0x000C1BCD
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000C39D0 File Offset: 0x000C1BD0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIFlyingMovement aiflyingMovement = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
			}
			if (FlyingNavmesh.IsGenerated)
			{
				aiflyingMovement.CurrentNode = new AIFlyingMovement.AgentPosition(new AIFlyingMovement.FlightPoint(FlyingNavmesh.instance.NearestNode(aiflyingMovement.Navflight.position)));
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000C3A46 File Offset: 0x000C1C46
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000C3A4E File Offset: 0x000C1C4E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x000C3A55 File Offset: 0x000C1C55
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022F6 RID: 8950
		private int <>1__state;

		// Token: 0x040022F7 RID: 8951
		private object <>2__current;

		// Token: 0x040022F8 RID: 8952
		public AIFlyingMovement <>4__this;
	}
}
