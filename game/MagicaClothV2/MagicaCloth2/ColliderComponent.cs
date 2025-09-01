using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x02000019 RID: 25
	public abstract class ColliderComponent : ClothBehaviour, IDataValidate
	{
		// Token: 0x06000061 RID: 97
		public abstract ColliderManager.ColliderType GetColliderType();

		// Token: 0x06000062 RID: 98
		public abstract void DataValidate();

		// Token: 0x06000063 RID: 99 RVA: 0x0000530A File Offset: 0x0000350A
		public virtual Vector3 GetSize()
		{
			return this.size;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005312 File Offset: 0x00003512
		public void SetSize(Vector3 size)
		{
			this.size = size;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000531B File Offset: 0x0000351B
		public float GetScale()
		{
			return base.transform.lossyScale.x;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000532D File Offset: 0x0000352D
		internal void Register(int teamId)
		{
			this.teamIdSet.Add(teamId);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000533C File Offset: 0x0000353C
		internal void Exit(int teamId)
		{
			this.teamIdSet.Remove(teamId);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000534C File Offset: 0x0000354C
		public void UpdateParameters()
		{
			this.DataValidate();
			foreach (int teamId in this.teamIdSet)
			{
				MagicaManager.Collider.UpdateParameters(this, teamId);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005305 File Offset: 0x00003505
		protected virtual void Start()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000053AC File Offset: 0x000035AC
		protected virtual void OnValidate()
		{
			this.UpdateParameters();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000053B4 File Offset: 0x000035B4
		protected virtual void OnEnable()
		{
			foreach (int teamId in this.teamIdSet)
			{
				MagicaManager.Collider.EnableCollider(this, teamId, true);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005410 File Offset: 0x00003610
		protected virtual void OnDisable()
		{
			foreach (int teamId in this.teamIdSet)
			{
				MagicaManager.Collider.EnableCollider(this, teamId, false);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000546C File Offset: 0x0000366C
		protected virtual void OnDestroy()
		{
			foreach (int teamId in this.teamIdSet)
			{
				MagicaManager.Collider.RemoveCollider(this, teamId);
			}
			this.teamIdSet.Clear();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000054D0 File Offset: 0x000036D0
		protected ColliderComponent()
		{
		}

		// Token: 0x040000A3 RID: 163
		public Vector3 center;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		protected Vector3 size;

		// Token: 0x040000A5 RID: 165
		private HashSet<int> teamIdSet = new HashSet<int>();
	}
}
