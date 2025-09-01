using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using FIMSpace.FTools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FIMSpace.FTail
{
	// Token: 0x02000064 RID: 100
	[AddComponentMenu("FImpossible Creations/Tail Animator 2")]
	[DefaultExecutionOrder(-4)]
	public class TailAnimator2 : MonoBehaviour, IDropHandler, IEventSystemHandler, IFHierarchyIcon
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00019270 File Offset: 0x00017470
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00019278 File Offset: 0x00017478
		public List<Component> DynamicAlwaysInclude
		{
			[CompilerGenerated]
			get
			{
				return this.<DynamicAlwaysInclude>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DynamicAlwaysInclude>k__BackingField = value;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00019284 File Offset: 0x00017484
		private void RefreshSegmentsColliders()
		{
			if (this.CollisionSpace == TailAnimator2.ECollisionSpace.Selective_Fast && this.TailSegments != null && this.TailSegments.Count > 1)
			{
				for (int i = 0; i < this.TailSegments.Count; i++)
				{
					this.TailSegments[i].ColliderRadius = this.GetColliderSphereRadiusFor(i);
				}
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000192E0 File Offset: 0x000174E0
		private void BeginCollisionsUpdate()
		{
			if (this.CollisionSpace == TailAnimator2.ECollisionSpace.Selective_Fast)
			{
				this.RefreshIncludedCollidersDataList();
				this.CollidersDataToCheck.Clear();
				for (int i = 0; i < this.IncludedCollidersData.Count; i++)
				{
					if (this.IncludedCollidersData[i].Transform == null)
					{
						this.forceRefreshCollidersData = true;
						return;
					}
					if (this.IncludedCollidersData[i].Transform.gameObject.activeInHierarchy)
					{
						if (this.CollideWithDisabledColliders)
						{
							this.IncludedCollidersData[i].RefreshColliderData();
							this.CollidersDataToCheck.Add(this.IncludedCollidersData[i]);
						}
						else if (this.CollisionMode == TailAnimator2.ECollisionMode.m_3DCollision)
						{
							if (this.IncludedCollidersData[i].Collider == null)
							{
								this.forceRefreshCollidersData = true;
								return;
							}
							if (this.IncludedCollidersData[i].Collider.enabled)
							{
								this.IncludedCollidersData[i].RefreshColliderData();
								this.CollidersDataToCheck.Add(this.IncludedCollidersData[i]);
							}
						}
						else
						{
							if (this.IncludedCollidersData[i].Collider2D == null)
							{
								this.forceRefreshCollidersData = true;
								return;
							}
							if (this.IncludedCollidersData[i].Collider2D.enabled)
							{
								this.IncludedCollidersData[i].RefreshColliderData();
								this.CollidersDataToCheck.Add(this.IncludedCollidersData[i]);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001946C File Offset: 0x0001766C
		private void SetupSphereColliders()
		{
			if (this.CollisionSpace == TailAnimator2.ECollisionSpace.World_Slow)
			{
				for (int i = 1; i < this._TransformsGhostChain.Count; i++)
				{
					if (this.CollidersSameLayer)
					{
						this._TransformsGhostChain[i].gameObject.layer = base.gameObject.layer;
					}
					else
					{
						this._TransformsGhostChain[i].gameObject.layer = this.CollidersLayer;
					}
				}
				if (this.CollidersType != 0)
				{
					for (int j = 1; j < this._TransformsGhostChain.Count - 1; j++)
					{
						CapsuleCollider capsuleCollider = this._TransformsGhostChain[j].gameObject.AddComponent<CapsuleCollider>();
						TailCollisionHelper tailCollisionHelper = this._TransformsGhostChain[j].gameObject.AddComponent<TailCollisionHelper>().Init(this.CollidersAddRigidbody, this.RigidbodyMass, false);
						tailCollisionHelper.TailCollider = capsuleCollider;
						tailCollisionHelper.Index = j;
						tailCollisionHelper.ParentTail = this;
						capsuleCollider.radius = this.GetColliderSphereRadiusFor(this._TransformsGhostChain, j);
						capsuleCollider.direction = 2;
						capsuleCollider.height = (this._TransformsGhostChain[j].position - this._TransformsGhostChain[j + 1].position).magnitude * 2f - capsuleCollider.radius;
						capsuleCollider.center = this._TransformsGhostChain[j].InverseTransformPoint(Vector3.Lerp(this._TransformsGhostChain[j].position, this._TransformsGhostChain[j + 1].position, 0.5f));
						this.TailSegments[j].ColliderRadius = capsuleCollider.radius;
						this.TailSegments[j].CollisionHelper = tailCollisionHelper;
					}
				}
				else
				{
					for (int k = 1; k < this._TransformsGhostChain.Count; k++)
					{
						SphereCollider sphereCollider = this._TransformsGhostChain[k].gameObject.AddComponent<SphereCollider>();
						TailCollisionHelper tailCollisionHelper2 = this._TransformsGhostChain[k].gameObject.AddComponent<TailCollisionHelper>().Init(this.CollidersAddRigidbody, this.RigidbodyMass, false);
						tailCollisionHelper2.TailCollider = sphereCollider;
						tailCollisionHelper2.Index = k;
						tailCollisionHelper2.ParentTail = this;
						sphereCollider.radius = this.GetColliderSphereRadiusFor(this._TransformsGhostChain, k);
						this.TailSegments[k].ColliderRadius = sphereCollider.radius;
						this.TailSegments[k].CollisionHelper = tailCollisionHelper2;
					}
				}
			}
			else
			{
				for (int l = 0; l < this._TransformsGhostChain.Count; l++)
				{
					this.TailSegments[l].ColliderRadius = this.GetColliderSphereRadiusFor(l);
				}
				this.IncludedCollidersData = new List<FImp_ColliderData_Base>();
				this.CollidersDataToCheck = new List<FImp_ColliderData_Base>();
				if (this.DynamicWorldCollidersInclusion)
				{
					if (this.CollisionMode == TailAnimator2.ECollisionMode.m_3DCollision)
					{
						for (int m = 0; m < this.IncludedColliders.Count; m++)
						{
							this.DynamicAlwaysInclude.Add(this.IncludedColliders[m]);
						}
					}
					else
					{
						for (int n = 0; n < this.IncludedColliders2D.Count; n++)
						{
							this.DynamicAlwaysInclude.Add(this.IncludedColliders2D[n]);
						}
					}
					Transform transform = this.TailSegments[this.TailSegments.Count / 2].transform;
					float num = Vector3.Distance(this._TransformsGhostChain[0].position, this._TransformsGhostChain[this._TransformsGhostChain.Count - 1].position);
					TailCollisionHelper tailCollisionHelper3 = transform.gameObject.AddComponent<TailCollisionHelper>();
					tailCollisionHelper3.ParentTail = this;
					SphereCollider sphereCollider2 = null;
					CircleCollider2D circleCollider2D = null;
					if (this.CollisionMode == TailAnimator2.ECollisionMode.m_3DCollision)
					{
						sphereCollider2 = transform.gameObject.AddComponent<SphereCollider>();
						sphereCollider2.isTrigger = true;
						tailCollisionHelper3.TailCollider = sphereCollider2;
					}
					else
					{
						circleCollider2D = transform.gameObject.AddComponent<CircleCollider2D>();
						circleCollider2D.isTrigger = true;
						tailCollisionHelper3.TailCollider2D = circleCollider2D;
					}
					tailCollisionHelper3.Init(true, 1f, true);
					float num2 = Mathf.Abs(transform.transform.lossyScale.z);
					if (num2 == 0f)
					{
						num2 = 1f;
					}
					if (sphereCollider2 != null)
					{
						sphereCollider2.radius = num / num2;
					}
					else
					{
						circleCollider2D.radius = num / num2;
					}
					if (this.CollidersSameLayer)
					{
						transform.gameObject.layer = base.gameObject.layer;
					}
					else
					{
						transform.gameObject.layer = this.CollidersLayer;
					}
				}
				this.RefreshIncludedCollidersDataList();
			}
			this.collisionInitialized = true;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001990C File Offset: 0x00017B0C
		internal void CollisionDetection(int index, Collision collision)
		{
			this.TailSegments[index].collisionContacts = collision;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00019920 File Offset: 0x00017B20
		internal void ExitCollision(int index)
		{
			this.TailSegments[index].collisionContacts = null;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00019934 File Offset: 0x00017B34
		protected bool UseCollisionContact(int index, ref Vector3 pos)
		{
			if (this.TailSegments[index].collisionContacts == null)
			{
				return false;
			}
			if (this.TailSegments[index].collisionContacts.contacts.Length == 0)
			{
				return false;
			}
			Collision collisionContacts = this.TailSegments[index].collisionContacts;
			float num = FImp_ColliderData_Sphere.CalculateTrueRadiusOfSphereCollider(this.TailSegments[index].transform, this.TailSegments[index].ColliderRadius) * 0.95f;
			if (collisionContacts.collider)
			{
				SphereCollider sphereCollider = collisionContacts.collider as SphereCollider;
				if (sphereCollider)
				{
					FImp_ColliderData_Sphere.PushOutFromSphereCollider(sphereCollider, num, ref pos, Vector3.zero);
				}
				else
				{
					CapsuleCollider capsuleCollider = collisionContacts.collider as CapsuleCollider;
					if (capsuleCollider)
					{
						FImp_ColliderData_Capsule.PushOutFromCapsuleCollider(capsuleCollider, num, ref pos, Vector3.zero);
					}
					else
					{
						BoxCollider boxCollider = collisionContacts.collider as BoxCollider;
						if (boxCollider)
						{
							if (this.TailSegments[index].CollisionHelper.RigBody)
							{
								if (boxCollider.attachedRigidbody)
								{
									if (this.TailSegments[index].CollisionHelper.RigBody.mass > 1f)
									{
										FImp_ColliderData_Box.PushOutFromBoxCollider(boxCollider, collisionContacts, num, ref pos, false);
										Vector3 b = pos;
										FImp_ColliderData_Box.PushOutFromBoxCollider(boxCollider, num, ref pos, false);
										pos = Vector3.Lerp(pos, b, this.TailSegments[index].CollisionHelper.RigBody.mass / 5f);
									}
									else
									{
										FImp_ColliderData_Box.PushOutFromBoxCollider(boxCollider, num, ref pos, false);
									}
								}
								else
								{
									FImp_ColliderData_Box.PushOutFromBoxCollider(boxCollider, num, ref pos, false);
								}
							}
							else
							{
								FImp_ColliderData_Box.PushOutFromBoxCollider(boxCollider, num, ref pos, false);
							}
						}
						else
						{
							MeshCollider meshCollider = collisionContacts.collider as MeshCollider;
							if (meshCollider)
							{
								FImp_ColliderData_Mesh.PushOutFromMeshCollider(meshCollider, collisionContacts, num, ref pos);
							}
							else
							{
								FImp_ColliderData_Terrain.PushOutFromTerrain(collisionContacts.collider as TerrainCollider, num, ref pos);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00019B28 File Offset: 0x00017D28
		public void RefreshIncludedCollidersDataList()
		{
			bool flag = false;
			if (this.CollisionMode == TailAnimator2.ECollisionMode.m_3DCollision)
			{
				if (this.IncludedColliders.Count != this.IncludedCollidersData.Count || this.forceRefreshCollidersData)
				{
					this.IncludedCollidersData.Clear();
					for (int i = this.IncludedColliders.Count - 1; i >= 0; i--)
					{
						if (this.IncludedColliders[i] == null)
						{
							this.IncludedColliders.RemoveAt(i);
						}
						else
						{
							FImp_ColliderData_Base colliderDataFor = FImp_ColliderData_Base.GetColliderDataFor(this.IncludedColliders[i]);
							this.IncludedCollidersData.Add(colliderDataFor);
						}
					}
					flag = true;
				}
			}
			else if (this.IncludedColliders2D.Count != this.IncludedCollidersData.Count || this.forceRefreshCollidersData)
			{
				this.IncludedCollidersData.Clear();
				for (int j = this.IncludedColliders2D.Count - 1; j >= 0; j--)
				{
					if (this.IncludedColliders2D[j] == null)
					{
						this.IncludedColliders2D.RemoveAt(j);
					}
					else
					{
						FImp_ColliderData_Base colliderDataFor2 = FImp_ColliderData_Base.GetColliderDataFor(this.IncludedColliders2D[j]);
						this.IncludedCollidersData.Add(colliderDataFor2);
					}
				}
				flag = true;
			}
			if (flag)
			{
				this.forceRefreshCollidersData = false;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00019C60 File Offset: 0x00017E60
		public bool PushIfSegmentInsideCollider(TailAnimator2.TailSegment bone, ref Vector3 targetPoint)
		{
			bool flag = false;
			if (!this.CheapCollision)
			{
				for (int i = 0; i < this.CollidersDataToCheck.Count; i++)
				{
					bool flag2 = this.CollidersDataToCheck[i].PushIfInside(ref targetPoint, bone.GetRadiusScaled(), Vector3.zero);
					if (!flag && flag2)
					{
						flag = true;
						bone.LatestSelectiveCollision = this.CollidersDataToCheck[i];
					}
				}
			}
			else
			{
				for (int j = 0; j < this.CollidersDataToCheck.Count; j++)
				{
					if (this.CollidersDataToCheck[j].PushIfInside(ref targetPoint, bone.GetRadiusScaled(), Vector3.zero))
					{
						bone.LatestSelectiveCollision = this.CollidersDataToCheck[j];
						return true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00019D14 File Offset: 0x00017F14
		protected float GetColliderSphereRadiusFor(int i)
		{
			TailAnimator2.TailSegment tailSegment = this.TailSegments[i];
			float num = 1f;
			if (i >= this._TransformsGhostChain.Count)
			{
				return num;
			}
			if (this._TransformsGhostChain.Count > 1)
			{
				num = Vector3.Distance(this._TransformsGhostChain[1].position, this._TransformsGhostChain[0].position);
			}
			float num2 = num;
			if (i != 0)
			{
				num2 = Mathf.Lerp(num, Vector3.Distance(this._TransformsGhostChain[i - 1].position, this._TransformsGhostChain[i].position) * 0.5f, this.CollisionsAutoCurve);
			}
			float num3 = (float)(this._TransformsGhostChain.Count - 1);
			if (num3 <= 0f)
			{
				num3 = 1f;
			}
			float num4 = 1f / num3;
			return 0.5f * num2 * this.CollidersScaleMul * this.CollidersScaleCurve.Evaluate(num4 * (float)i);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00019E00 File Offset: 0x00018000
		protected float GetColliderSphereRadiusFor(List<Transform> transforms, int i)
		{
			float num = 1f;
			if (transforms.Count > 1)
			{
				num = Vector3.Distance(this._TransformsGhostChain[1].position, this._TransformsGhostChain[0].position);
			}
			float num2 = num;
			if (i != 0)
			{
				num2 = Vector3.Distance(this._TransformsGhostChain[i - 1].position, this._TransformsGhostChain[i].position);
			}
			float num3 = Mathf.Lerp(num, num2 * 0.5f, this.CollisionsAutoCurve);
			float num4 = 1f / (float)(transforms.Count - 1);
			return 0.5f * num3 * this.CollidersScaleMul * this.CollidersScaleCurve.Evaluate(num4 * (float)i);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00019EB5 File Offset: 0x000180B5
		public void AddCollider(Collider collider)
		{
			if (this.IncludedColliders.Contains(collider))
			{
				return;
			}
			this.IncludedColliders.Add(collider);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00019ED2 File Offset: 0x000180D2
		public void AddCollider(Collider2D collider)
		{
			if (this.IncludedColliders2D.Contains(collider))
			{
				return;
			}
			this.IncludedColliders2D.Add(collider);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00019EF0 File Offset: 0x000180F0
		public void CheckForColliderDuplicatesAndNulls()
		{
			for (int i = 0; i < this.IncludedColliders.Count; i++)
			{
				Collider col = this.IncludedColliders[i];
				if (this.IncludedColliders.Count((Collider o) => o == col) > 1)
				{
					this.IncludedColliders.RemoveAll((Collider o) => o == col);
					this.IncludedColliders.Add(col);
				}
			}
			for (int j = this.IncludedColliders.Count - 1; j >= 0; j--)
			{
				if (this.IncludedColliders[j] == null)
				{
					this.IncludedColliders.RemoveAt(j);
				}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00019FA8 File Offset: 0x000181A8
		public void CheckForColliderDuplicatesAndNulls2D()
		{
			for (int i = 0; i < this.IncludedColliders2D.Count; i++)
			{
				Collider2D col = this.IncludedColliders2D[i];
				if (this.IncludedColliders2D.Count((Collider2D o) => o == col) > 1)
				{
					this.IncludedColliders2D.RemoveAll((Collider2D o) => o == col);
					this.IncludedColliders2D.Add(col);
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001A028 File Offset: 0x00018228
		private void TailCalculations_ComputeSegmentCollisions(TailAnimator2.TailSegment bone, ref Vector3 position)
		{
			if (bone.CollisionContactFlag)
			{
				bone.CollisionContactFlag = false;
			}
			else if (bone.CollisionContactRelevancy > 0f)
			{
				bone.CollisionContactRelevancy -= this.justDelta;
			}
			if (this.CollisionSpace == TailAnimator2.ECollisionSpace.Selective_Fast)
			{
				if (this.PushIfSegmentInsideCollider(bone, ref position))
				{
					bone.CollisionContactFlag = true;
					bone.CollisionContactRelevancy = this.justDelta * 7f;
					bone.ChildBone.CollisionContactRelevancy = Mathf.Max(bone.ChildBone.CollisionContactRelevancy, this.justDelta * 3.5f);
					if (bone.ChildBone.ChildBone != null)
					{
						bone.ChildBone.ChildBone.CollisionContactRelevancy = Mathf.Max(bone.ChildBone.CollisionContactRelevancy, this.justDelta * 3f);
						return;
					}
				}
			}
			else if (this.UseCollisionContact(bone.Index, ref position))
			{
				bone.CollisionContactFlag = true;
				bone.CollisionContactRelevancy = this.justDelta * 7f;
				bone.ChildBone.CollisionContactRelevancy = Mathf.Max(bone.ChildBone.CollisionContactRelevancy, this.justDelta * 3.5f);
				if (bone.ChildBone.ChildBone != null)
				{
					bone.ChildBone.ChildBone.CollisionContactRelevancy = Mathf.Max(bone.ChildBone.CollisionContactRelevancy, this.justDelta * 3f);
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001A184 File Offset: 0x00018384
		private void ExpertParamsUpdate()
		{
			this.Expert_UpdatePosSpeed();
			this.Expert_UpdateRotSpeed();
			this.Expert_UpdateSpringiness();
			this.Expert_UpdateSlithery();
			this.Expert_UpdateCurling();
			this.Expert_UpdateSlippery();
			this.Expert_UpdateBlending();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001A1B0 File Offset: 0x000183B0
		private void ExpertCurvesEndUpdate()
		{
			this.lastPosSpeeds = this.ReactionSpeed;
			if (!this.UsePosSpeedCurve && this.lastPosCurvKeys != null)
			{
				this.lastPosCurvKeys = null;
				this.lastPosSpeeds += 0.001f;
			}
			this.lastRotSpeeds = this.RotationRelevancy;
			if (!this.UseRotSpeedCurve && this.lastRotCurvKeys != null)
			{
				this.lastRotCurvKeys = null;
				this.lastRotSpeeds += 0.001f;
			}
			this.lastSpringiness = this.Springiness;
			if (!this.UseSpringCurve && this.lastSpringCurvKeys != null)
			{
				this.lastSpringCurvKeys = null;
				this.lastSpringiness += 0.001f;
			}
			this.lastSlithery = this.Slithery;
			if (!this.UseSlitheryCurve && this.lastSlitheryCurvKeys != null)
			{
				this.lastSlitheryCurvKeys = null;
				this.lastSlithery += 0.001f;
			}
			this.lastCurling = this.Curling;
			if (!this.UseCurlingCurve && this.lastCurlingCurvKeys != null)
			{
				this.lastCurlingCurvKeys = null;
				this.lastCurling += 0.001f;
			}
			this.lastSlippery = this.CollisionSlippery;
			if (!this.UseSlipperyCurve && this.lastSlipperyCurvKeys != null)
			{
				this.lastSlipperyCurvKeys = null;
				this.lastSlippery += 0.001f;
			}
			this.lastTailAnimatorAmount = this.TailAnimatorAmount;
			if (!this.UsePartialBlend && this.lastBlendCurvKeys != null)
			{
				this.lastBlendCurvKeys = null;
				this.lastTailAnimatorAmount += 0.001f;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001A330 File Offset: 0x00018530
		private void Expert_UpdatePosSpeed()
		{
			if (this.UsePosSpeedCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.PositionSpeed = this.PosCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastPosSpeeds != this.ReactionSpeed)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.PositionSpeed = this.ReactionSpeed;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001A3E0 File Offset: 0x000185E0
		private void Expert_UpdateRotSpeed()
		{
			if (this.UseRotSpeedCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.RotationSpeed = this.RotCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastRotSpeeds != this.RotationRelevancy)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.RotationSpeed = this.RotationRelevancy;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001A490 File Offset: 0x00018690
		private void Expert_UpdateSpringiness()
		{
			if (this.UseSpringCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Springiness = this.SpringCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastSpringiness != this.Springiness)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Springiness = this.Springiness;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001A540 File Offset: 0x00018740
		private void Expert_UpdateSlithery()
		{
			if (this.UseSlitheryCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Slithery = this.SlitheryCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastSlithery != this.Slithery)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Slithery = this.Slithery;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001A5F0 File Offset: 0x000187F0
		private void Expert_UpdateCurling()
		{
			if (this.UseCurlingCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Curling = this.CurlingCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastCurling != this.Curling)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Curling = this.Curling;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001A6A0 File Offset: 0x000188A0
		private void Expert_UpdateSlippery()
		{
			if (this.UseSlipperyCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Slippery = this.SlipperyCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastSlippery != this.CollisionSlippery)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.Slippery = this.CollisionSlippery;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001A750 File Offset: 0x00018950
		private void Expert_UpdateBlending()
		{
			if (this.UsePartialBlend)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.BlendValue = this.BlendCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastTailAnimatorAmount != this.TailAnimatorAmount)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.BlendValue = this.TailAnimatorAmount;
					this._ex_bone = this._ex_bone.ChildBone;
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001A800 File Offset: 0x00018A00
		private void InitIK()
		{
			if (!this.IKSelectiveChain)
			{
				this.IK = new FIK_CCDProcessor(this._TransformsGhostChain.ToArray());
			}
			else
			{
				List<Transform> list = new List<Transform>();
				if (this.IKLimitSettings.Count != this._TransformsGhostChain.Count)
				{
					list = this._TransformsGhostChain;
				}
				else
				{
					for (int i = 0; i < this._TransformsGhostChain.Count; i++)
					{
						if (this.IKLimitSettings[i].UseInChain)
						{
							list.Add(this._TransformsGhostChain[i]);
						}
					}
				}
				this.IK = new FIK_CCDProcessor(list.ToArray());
			}
			if (this.IKAutoWeights)
			{
				this.IK.AutoWeightBones(this.IKBaseReactionWeight);
			}
			else
			{
				this.IK.AutoWeightBones(this.IKReactionWeightCurve);
			}
			if (this.IKAutoAngleLimits)
			{
				this.IK.AutoLimitAngle(this.IKAutoAngleLimit, 4f + this.IKAutoAngleLimit / 15f);
			}
			if (!this.IKSelectiveChain)
			{
				this.IK.Init(this._TransformsGhostChain[0]);
			}
			else
			{
				this.IK.Init(this.IK.Bones[0].transform);
			}
			this.ikInitialized = true;
			this.IK_ApplyLimitBoneSettings();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001A942 File Offset: 0x00018B42
		public void IKSetCustomPosition(Vector3? tgt)
		{
			this._IKCustomPos = tgt;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001A94C File Offset: 0x00018B4C
		private void UpdateIK()
		{
			if (!this.ikInitialized)
			{
				this.InitIK();
			}
			if (this.IKBlend <= Mathf.Epsilon)
			{
				return;
			}
			if (this._IKCustomPos != null)
			{
				this.IK.IKTargetPosition = this._IKCustomPos.Value;
			}
			else if (this.IKTarget == null)
			{
				this.IK.IKTargetPosition = this.TailSegments[this.TailSegments.Count - 1].ProceduralPosition;
			}
			else
			{
				this.IK.IKTargetPosition = this.IKTarget.position;
			}
			this.IK.IKWeight = this.IKBlend;
			this.IK.SyncWithAnimator = this.IKAnimatorBlend;
			this.IK.ReactionQuality = this.IKReactionQuality;
			this.IK.Smoothing = this.IKSmoothing;
			this.IK.MaxStretching = this.IKStretchToTarget;
			this.IK.StretchCurve = this.IKStretchCurve;
			this.IK.ContinousSolving = this.IKContinousSolve;
			if (this.IK.MaxStretching > 0f)
			{
				this.IK.ContinousSolving = false;
			}
			if (this.Axis2D == 3)
			{
				this.IK.Use2D = true;
			}
			else
			{
				this.IK.Use2D = false;
			}
			this.IK.Update();
			if (this.DetachChildren)
			{
				TailAnimator2.TailSegment tailSegment = this.TailSegments[0];
				tailSegment = this.TailSegments[1];
				if (!this.IncludeParent)
				{
					tailSegment.RefreshKeyLocalPositionAndRotation(tailSegment.InitialLocalPosition, tailSegment.InitialLocalRotation);
					tailSegment = this.TailSegments[2];
				}
				while (tailSegment != this.GhostChild)
				{
					tailSegment.RefreshKeyLocalPositionAndRotation(tailSegment.InitialLocalPosition, tailSegment.InitialLocalRotation);
					tailSegment = tailSegment.ChildBone;
				}
				return;
			}
			for (TailAnimator2.TailSegment tailSegment2 = this.TailSegments[0]; tailSegment2 != this.GhostChild; tailSegment2 = tailSegment2.ChildBone)
			{
				tailSegment2.RefreshKeyLocalPositionAndRotation();
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001AB40 File Offset: 0x00018D40
		public void IK_ApplyLimitBoneSettings()
		{
			if (!this.IKAutoAngleLimits)
			{
				if (this.IKLimitSettings.Count != this._TransformsGhostChain.Count)
				{
					this.IK_RefreshLimitSettingsContainer();
				}
				if (this.IK.IKBones.Length != this.IKLimitSettings.Count)
				{
					UnityEngine.Debug.Log("[TAIL ANIMATOR IK] Wrong IK bone count!");
					return;
				}
				if (!this.IKAutoAngleLimits)
				{
					for (int i = 0; i < this.IKLimitSettings.Count; i++)
					{
						this.IK.IKBones[i].AngleLimit = this.IKLimitSettings[i].AngleLimit;
						this.IK.IKBones[i].TwistAngleLimit = this.IKLimitSettings[i].TwistAngleLimit;
					}
				}
			}
			if (this.ikInitialized)
			{
				if (this.IKAutoWeights)
				{
					this.IK.AutoWeightBones(this.IKBaseReactionWeight);
				}
				else
				{
					this.IK.AutoWeightBones(this.IKReactionWeightCurve);
				}
			}
			if (this.IKAutoAngleLimits)
			{
				this.IK.AutoLimitAngle(this.IKAutoAngleLimit, 10f + this.IKAutoAngleLimit / 10f);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001AC60 File Offset: 0x00018E60
		public void IK_RefreshLimitSettingsContainer()
		{
			this.IKLimitSettings = new List<TailAnimator2.IKBoneSettings>();
			for (int i = 0; i < this._TransformsGhostChain.Count; i++)
			{
				this.IKLimitSettings.Add(new TailAnimator2.IKBoneSettings());
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001AC9E File Offset: 0x00018E9E
		private bool PostProcessingNeeded()
		{
			return this.Deflection > Mathf.Epsilon;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		private void PostProcessing_Begin()
		{
			this.TailSegments_UpdateCoordsForRootBone(this._pp_reference[this._tc_startI]);
			if (this.Deflection > Mathf.Epsilon)
			{
				this.Deflection_BeginUpdate();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001ACDC File Offset: 0x00018EDC
		private void PostProcessing_ReferenceUpdate()
		{
			TailAnimator2.TailSegment tailSegment;
			for (tailSegment = this._pp_reference[this._tc_startI]; tailSegment != this._pp_ref_lastChild; tailSegment = tailSegment.ChildBone)
			{
				tailSegment.ParamsFrom(this.TailSegments[tailSegment.Index]);
				this.TailSegment_PrepareVelocity(tailSegment);
			}
			this.TailSegment_PrepareMotionParameters(this._pp_ref_lastChild);
			this.TailSegment_PrepareVelocity(this._pp_ref_lastChild);
			tailSegment = this._pp_reference[this._tc_startII];
			if (!this.DetachChildren)
			{
				while (tailSegment != this._pp_ref_lastChild)
				{
					this.TailSegment_PrepareRotation(tailSegment);
					this.TailSegment_BaseSwingProcessing(tailSegment);
					this.TailCalculations_SegmentPreProcessingStack(tailSegment);
					this.TailSegment_PreRotationPositionBlend(tailSegment);
					tailSegment = tailSegment.ChildBone;
				}
			}
			else
			{
				while (tailSegment != this._pp_ref_lastChild)
				{
					this.TailSegment_PrepareRotationDetached(tailSegment);
					this.TailSegment_BaseSwingProcessing(tailSegment);
					this.TailCalculations_SegmentPreProcessingStack(tailSegment);
					this.TailSegment_PreRotationPositionBlend(tailSegment);
					tailSegment = tailSegment.ChildBone;
				}
			}
			this.TailCalculations_UpdateArtificialChildBone(this._pp_ref_lastChild);
			for (tailSegment = this._pp_reference[this._tc_startII]; tailSegment != this._pp_ref_lastChild; tailSegment = tailSegment.ChildBone)
			{
				this.TailCalculations_SegmentRotation(tailSegment, tailSegment.LastKeyframeLocalPosition);
			}
			this.TailCalculations_SegmentRotation(tailSegment, tailSegment.LastKeyframeLocalPosition);
			tailSegment.ParentBone.RefreshFinalRot(tailSegment.ParentBone.TrueTargetRotation);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001AE19 File Offset: 0x00019019
		private void ShapingParamsUpdate()
		{
			this.Shaping_UpdateCurving();
			this.Shaping_UpdateGravity();
			this.Shaping_UpdateLengthMultiplier();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001AE30 File Offset: 0x00019030
		private void Shaping_UpdateCurving()
		{
			if (!this.Curving.QIsZero())
			{
				if (this.UseCurvingCurve)
				{
					this._ex_bone = this.TailSegments[0];
					while (this._ex_bone != null)
					{
						this._ex_bone.Curving = Quaternion.LerpUnclamped(Quaternion.identity, this.Curving, this.CurvCurve.Evaluate(this._ex_bone.IndexOverlLength));
						this._ex_bone = this._ex_bone.ChildBone;
					}
					return;
				}
				if (!this.Curving.QIsSame(this.lastCurving))
				{
					for (int i = 0; i < this.TailSegments.Count; i++)
					{
						this.TailSegments[i].Curving = this.Curving;
					}
					return;
				}
			}
			else if (!this.Curving.QIsSame(this.lastCurving))
			{
				for (int j = 0; j < this.TailSegments.Count; j++)
				{
					this.TailSegments[j].Curving = Quaternion.identity;
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001AF34 File Offset: 0x00019134
		private void Shaping_UpdateGravity()
		{
			if (!this.Gravity.VIsZero())
			{
				if (this.UseGravityCurve)
				{
					this._ex_bone = this.TailSegments[0];
					while (this._ex_bone != null)
					{
						this._ex_bone.Gravity = this.Gravity * 40f * this.GravityCurve.Evaluate(this._ex_bone.IndexOverlLength);
						this._ex_bone = this._ex_bone.ChildBone;
					}
					return;
				}
				if (!this.Gravity.VIsSame(this.lastGravity))
				{
					for (int i = 0; i < this.TailSegments.Count; i++)
					{
						this.TailSegments[i].Gravity = this.Gravity / 40f;
						this.TailSegments[i].Gravity *= 1f + (float)this.TailSegments[i].Index / 2f * (1f - this.TailSegments[i].Slithery);
					}
					return;
				}
			}
			else if (!this.Gravity.VIsSame(this.lastGravity))
			{
				for (int j = 0; j < this.TailSegments.Count; j++)
				{
					this.TailSegments[j].Gravity = Vector3.zero;
					this.TailSegments[j].GravityLookOffset = Vector3.zero;
				}
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B0B8 File Offset: 0x000192B8
		private void Shaping_UpdateLengthMultiplier()
		{
			if (this.UseLengthMulCurve)
			{
				this._ex_bone = this.TailSegments[0];
				while (this._ex_bone != null)
				{
					this._ex_bone.LengthMultiplier = this.LengthMulCurve.Evaluate(this._ex_bone.IndexOverlLength);
					this._ex_bone = this._ex_bone.ChildBone;
				}
				return;
			}
			if (this.lastLengthMul != this.LengthMultiplier)
			{
				for (int i = 0; i < this.TailSegments.Count; i++)
				{
					this.TailSegments[i].LengthMultiplier = this.LengthMultiplier;
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001B158 File Offset: 0x00019358
		private void ShapingEndUpdate()
		{
			this.lastCurving = this.Curving;
			if (!this.UseCurvingCurve && this.lastCurvingKeys != null)
			{
				this.lastCurvingKeys = null;
				this.lastCurving.x = this.lastCurving.x + 0.001f;
			}
			this.lastGravity = this.Gravity;
			if (!this.UseGravityCurve && this.lastGravityKeys != null)
			{
				this.lastGravityKeys = null;
				this.lastGravity.x = this.lastGravity.x + 0.001f;
			}
			this.lastLengthMul = this.LengthMultiplier;
			if (!this.UseLengthMulCurve && this.lastLengthKeys != null)
			{
				this.lastLengthKeys = null;
				this.lastLengthMul += 0.0001f;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001B208 File Offset: 0x00019408
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001B210 File Offset: 0x00019410
		public Quaternion WavingRotationOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<WavingRotationOffset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WavingRotationOffset>k__BackingField = value;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001B21C File Offset: 0x0001941C
		private void Waving_Initialize()
		{
			if (this.FixedCycle == 0f)
			{
				this._waving_waveTime = UnityEngine.Random.Range(-3.1415927f, 3.1415927f) * 100f;
				this._waving_cosTime = UnityEngine.Random.Range(-3.1415927f, 3.1415927f) * 50f;
			}
			else
			{
				this._waving_waveTime = this.FixedCycle;
				this._waving_cosTime = this.FixedCycle;
			}
			this._waving_sustain = Vector3.zero;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001B294 File Offset: 0x00019494
		private void Waving_AutoSwingUpdate()
		{
			this._waving_waveTime += this.justDelta * (2f * this.WavingSpeed);
			if (this.WavingType == TailAnimator2.FEWavingType.Simple)
			{
				float num = Mathf.Sin(this._waving_waveTime) * (30f * this.WavingRange);
				if (this.CosinusAdd)
				{
					this._waving_cosTime += this.justDelta * (2.535f * this.WavingSpeed);
					num += Mathf.Cos(this._waving_cosTime) * (27f * this.WavingRange);
				}
				this.WavingRotationOffset = Quaternion.Euler(num * this.WavingAxis * this.TailSegments[0].BlendValue);
				return;
			}
			float num2 = this._waving_waveTime * 0.23f;
			float y = this.AlternateWave * -5f;
			float num3 = this.AlternateWave * 100f;
			float x = this.AlternateWave * 20f;
			float x2 = Mathf.PerlinNoise(num2, y) * 2f - 1f;
			float y2 = Mathf.PerlinNoise(num3 + num2, num2 + num3) * 2f - 1f;
			float z = Mathf.PerlinNoise(x, num2) * 2f - 1f;
			this.WavingRotationOffset = Quaternion.Euler(Vector3.Scale(this.WavingAxis * this.WavingRange * 35f * this.TailSegments[0].BlendValue, new Vector3(x2, y2, z)));
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001B418 File Offset: 0x00019618
		private void Waving_SustainUpdate()
		{
			TailAnimator2.TailSegment tailSegment = this.TailSegments[0];
			float num = this._TC_TailLength / (float)this.TailSegments.Count;
			num = Mathf.Pow(num, 1.65f);
			num = this._sg_curly / num / 6f;
			if (num < 0.1f)
			{
				num = 0.1f;
			}
			else if (num > 1f)
			{
				num = 1f;
			}
			int num2 = (int)Mathf.LerpUnclamped((float)this.TailSegments.Count * 0.4f, (float)this.TailSegments.Count * 0.6f, this.Sustain);
			float end = FEasing.EaseOutExpo(1f, 0.09f, this.Sustain, 1f);
			float num3 = 1.5f;
			num3 *= 1f - this.TailSegments[0].Curling / 8f;
			num3 *= 1.5f - num / 1.65f;
			num3 *= Mathf.Lerp(0.7f, 1.2f, tailSegment.Slithery);
			num3 *= FEasing.EaseOutExpo(1f, end, tailSegment.Springiness, 1f);
			Vector3 a = this.TailSegments[num2].PreviousPush;
			if (num2 + 1 < this.TailSegments.Count)
			{
				a += this.TailSegments[num2 + 1].PreviousPush;
			}
			if (num2 - 1 > this.TailSegments.Count)
			{
				a += this.TailSegments[num2 - 1].PreviousPush;
			}
			this._waving_sustain = a * this.Sustain * num3 * 2f;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B5CA File Offset: 0x000197CA
		private void WindEffectUpdate()
		{
			if (TailAnimatorWind.Instance)
			{
				TailAnimatorWind.Instance.AffectTailWithWind(this);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B5E4 File Offset: 0x000197E4
		protected virtual void Init()
		{
			if (this.initialized)
			{
				return;
			}
			if (this._TransformsGhostChain == null || this._TransformsGhostChain.Count == 0)
			{
				this._TransformsGhostChain = new List<Transform>();
				this.GetGhostChain();
			}
			this.TailSegments = new List<TailAnimator2.TailSegment>();
			for (int i = 0; i < this._TransformsGhostChain.Count; i++)
			{
				if (this._TransformsGhostChain[i] == null)
				{
					UnityEngine.Debug.Log("[Tail Animator] Null bones in " + base.name + " !");
				}
				else
				{
					TailAnimator2.TailSegment tailSegment = new TailAnimator2.TailSegment(this._TransformsGhostChain[i]);
					tailSegment.SetIndex(i, this._TransformsGhostChain.Count);
					this.TailSegments.Add(tailSegment);
				}
			}
			if (this.TailSegments.Count == 0)
			{
				UnityEngine.Debug.Log("[Tail Animator] Could not create tail bones chain in " + base.name + " !");
				return;
			}
			this._TC_TailLength = 0f;
			this._baseTransform = this._TransformsGhostChain[0];
			for (int j = 0; j < this.TailSegments.Count; j++)
			{
				TailAnimator2.TailSegment tailSegment2 = this.TailSegments[j];
				TailAnimator2.TailSegment tailSegment3;
				if (j == 0)
				{
					if (tailSegment2.transform.parent)
					{
						tailSegment3 = new TailAnimator2.TailSegment(tailSegment2.transform.parent);
						tailSegment3.SetParentRef(new TailAnimator2.TailSegment(tailSegment3.transform.parent));
					}
					else
					{
						tailSegment3 = new TailAnimator2.TailSegment(tailSegment2.transform);
						Vector3 b;
						if (this._TransformsGhostChain.Count > 1)
						{
							b = this._TransformsGhostChain[0].position - this._TransformsGhostChain[1].position;
							if (b.magnitude == 0f)
							{
								b = base.transform.position - this._TransformsGhostChain[1].position;
							}
						}
						else
						{
							b = tailSegment2.transform.position - this._TransformsGhostChain[0].position;
						}
						if (b.magnitude == 0f)
						{
							b = base.transform.position - this._TransformsGhostChain[0].position;
						}
						if (b.magnitude == 0f)
						{
							b = base.transform.forward;
						}
						tailSegment3.LocalOffset = tailSegment3.transform.InverseTransformPoint(tailSegment3.transform.position + b);
						tailSegment3.SetParentRef(new TailAnimator2.TailSegment(tailSegment2.transform));
					}
					this.GhostParent = tailSegment3;
					this.GhostParent.Validate();
					tailSegment2.SetParentRef(this.GhostParent);
				}
				else
				{
					tailSegment3 = this.TailSegments[j - 1];
					tailSegment2.ReInitializeLocalPosRot(tailSegment3.transform.InverseTransformPoint(tailSegment2.transform.position), tailSegment2.transform.localRotation);
				}
				if (j == this.TailSegments.Count - 1)
				{
					Transform transform = null;
					if (tailSegment2.transform.childCount > 0)
					{
						transform = tailSegment2.transform.GetChild(0);
					}
					this.GhostChild = new TailAnimator2.TailSegment(transform);
					Vector3 b2;
					if (this.EndBoneJointOffset.VIsZero())
					{
						if (tailSegment2.transform.parent)
						{
							b2 = tailSegment2.transform.position - tailSegment2.transform.parent.position;
						}
						else if (tailSegment2.transform.childCount > 0)
						{
							b2 = tailSegment2.transform.GetChild(0).position - tailSegment2.transform.position;
						}
						else
						{
							b2 = tailSegment2.transform.TransformDirection(Vector3.forward) * 0.05f;
						}
					}
					else
					{
						b2 = tailSegment2.transform.TransformVector(this.EndBoneJointOffset);
					}
					this.GhostChild.ProceduralPosition = tailSegment2.transform.position + b2;
					this.GhostChild.ProceduralPositionWeightBlended = this.GhostChild.ProceduralPosition;
					this.GhostChild.PreviousPosition = this.GhostChild.ProceduralPosition;
					this.GhostChild.PosRefRotation = Quaternion.identity;
					this.GhostChild.PreviousPosReferenceRotation = Quaternion.identity;
					this.GhostChild.ReInitializeLocalPosRot(tailSegment2.transform.InverseTransformPoint(this.GhostChild.ProceduralPosition), Quaternion.identity);
					this.GhostChild.RefreshFinalPos(this.GhostChild.ProceduralPosition);
					this.GhostChild.RefreshFinalRot(this.GhostChild.PosRefRotation);
					this.GhostChild.TrueTargetRotation = this.GhostChild.PosRefRotation;
					tailSegment2.TrueTargetRotation = tailSegment2.transform.rotation;
					tailSegment2.SetChildRef(this.GhostChild);
					this.GhostChild.SetParentRef(tailSegment2);
				}
				else
				{
					tailSegment2.SetChildRef(this.TailSegments[j + 1]);
				}
				tailSegment2.SetParentRef(tailSegment3);
				this._TC_TailLength += Vector3.Distance(tailSegment2.ProceduralPosition, tailSegment3.ProceduralPosition);
				if (tailSegment2.transform != this._baseTransform)
				{
					tailSegment2.AssignDetachedRootCoords(this.BaseTransform);
				}
			}
			this.GhostParent.SetIndex(-1, this.TailSegments.Count);
			this.GhostChild.SetIndex(this.TailSegments.Count, this.TailSegments.Count);
			this.GhostParent.SetChildRef(this.TailSegments[0]);
			this.previousWorldPosition = this.BaseTransform.position;
			this.WavingRotationOffset = Quaternion.identity;
			if (this.CollidersDataToCheck == null)
			{
				this.CollidersDataToCheck = new List<FImp_ColliderData_Base>();
			}
			this.DynamicAlwaysInclude = new List<Component>();
			if (this.UseCollision)
			{
				this.SetupSphereColliders();
			}
			if (this._defl_source == null)
			{
				this._defl_source = new List<TailAnimator2.TailSegment>();
			}
			this.Waving_Initialize();
			if (this.DetachChildren)
			{
				this.DetachChildrenTransforms();
			}
			this.initialized = true;
			if (this.TailSegments.Count == 1 && this.TailSegments[0].transform.parent == null)
			{
				UnityEngine.Debug.Log("[Tail Animator] Can't initialize one-bone length chain on bone which don't have any parent!");
				UnityEngine.Debug.LogError("[Tail Animator] Can't initialize one-bone length chain on bone which don't have any parent!");
				this.TailAnimatorAmount = 0f;
				this.initialized = false;
				return;
			}
			if (this.UseWind)
			{
				TailAnimatorWind.Refresh();
			}
			if (this.PostProcessingNeeded() && !this._pp_initialized)
			{
				this.InitializePostProcessing();
			}
			if (this.Prewarm)
			{
				this.ShapingParamsUpdate();
				this.ExpertParamsUpdate();
				this.Update();
				this.LateUpdate();
				this.justDelta = this.rateDelta;
				this.secPeriodDelta = 1f;
				this.deltaForLerps = this.secPeriodDelta;
				this.rateDelta = 0.016666668f;
				this.CheckIfTailAnimatorShouldBeUpdated();
				if (this.updateTailAnimator)
				{
					int num = 60 + this.TailSegments.Count / 2;
					for (int k = 0; k < num; k++)
					{
						this.PreCalibrateBones();
						this.LateUpdate();
					}
				}
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		public void DetachChildrenTransforms()
		{
			int num = this.IncludeParent ? 0 : 1;
			for (int i = this.TailSegments.Count - 1; i >= num; i--)
			{
				if (this.TailSegments[i].transform)
				{
					this.TailSegments[i].transform.DetachChildren();
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001BD38 File Offset: 0x00019F38
		private void InitializePostProcessing()
		{
			this._pp_reference = new List<TailAnimator2.TailSegment>();
			this._pp_ref_rootParent = new TailAnimator2.TailSegment(this.GhostParent);
			for (int i = 0; i < this.TailSegments.Count; i++)
			{
				TailAnimator2.TailSegment item = new TailAnimator2.TailSegment(this.TailSegments[i]);
				this._pp_reference.Add(item);
			}
			this._pp_ref_lastChild = new TailAnimator2.TailSegment(this.GhostChild);
			this._pp_ref_rootParent.SetChildRef(this._pp_reference[0]);
			this._pp_ref_rootParent.SetParentRef(new TailAnimator2.TailSegment(this.GhostParent.ParentBone.transform));
			for (int j = 0; j < this._pp_reference.Count; j++)
			{
				TailAnimator2.TailSegment tailSegment = this._pp_reference[j];
				tailSegment.SetIndex(j, this.TailSegments.Count);
				if (j == 0)
				{
					tailSegment.SetParentRef(this._pp_ref_rootParent);
					tailSegment.SetChildRef(this._pp_reference[j + 1]);
				}
				else if (j == this._pp_reference.Count - 1)
				{
					tailSegment.SetParentRef(this._pp_reference[j - 1]);
					tailSegment.SetChildRef(this._pp_ref_lastChild);
				}
				else
				{
					tailSegment.SetParentRef(this._pp_reference[j - 1]);
					tailSegment.SetChildRef(this._pp_reference[j + 1]);
				}
			}
			this._pp_ref_lastChild.SetParentRef(this._pp_reference[this._pp_reference.Count - 1]);
			this._pp_initialized = true;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001BEC4 File Offset: 0x0001A0C4
		protected void StretchingLimiting(TailAnimator2.TailSegment bone)
		{
			Vector3 a = bone.ParentBone.ProceduralPosition - bone.ProceduralPosition;
			float magnitude = a.magnitude;
			if (magnitude > 0f)
			{
				float num = bone.BoneLengthScaled + bone.BoneLengthScaled * 2.5f * this.MaxStretching;
				if (magnitude > num)
				{
					if (this.MaxStretching == 0f)
					{
						this._limiting_limitPosition = bone.ProceduralPosition + a * ((magnitude - bone.BoneLengthScaled) / magnitude);
						bone.ProceduralPosition = this._limiting_limitPosition;
						return;
					}
					this._limiting_limitPosition = bone.ParentBone.ProceduralPosition - a.normalized * num;
					float num2 = Mathf.InverseLerp(magnitude, 0f, num) + this._limiting_stretchingHelperTooLong;
					if (num2 > 0.999f)
					{
						num2 = 0.99f;
					}
					if (this.ReactionSpeed < 0.5f)
					{
						num2 *= this.deltaForLerps * (10f + this.ReactionSpeed * 30f);
					}
					bone.ProceduralPosition = Vector3.Lerp(bone.ProceduralPosition, this._limiting_limitPosition, num2);
					return;
				}
				else
				{
					num = bone.BoneLengthScaled + bone.BoneLengthScaled * 1.1f * this.MaxStretching;
					if (magnitude < num)
					{
						this._limiting_limitPosition = bone.ProceduralPosition + a * ((magnitude - bone.BoneLengthScaled) / magnitude);
						if (this.MaxStretching == 0f)
						{
							bone.ProceduralPosition = this._limiting_limitPosition;
							return;
						}
						bone.ProceduralPosition = Vector3.LerpUnclamped(bone.ProceduralPosition, this._limiting_limitPosition, Mathf.InverseLerp(magnitude, 0f, num) + this._limiting_stretchingHelperTooShort);
					}
				}
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001C064 File Offset: 0x0001A264
		protected Vector3 AngleLimiting(TailAnimator2.TailSegment child, Vector3 targetPos)
		{
			float num = 0f;
			this._limiting_limitPosition = targetPos;
			this._limiting_angle_ToTargetRot = Quaternion.FromToRotation(child.ParentBone.transform.TransformDirection(child.LastKeyframeLocalPosition), targetPos - child.ParentBone.ProceduralPosition) * child.ParentBone.transform.rotation;
			this._limiting_angle_targetInLocal = child.ParentBone.transform.rotation.QToLocal(this._limiting_angle_ToTargetRot);
			float num2;
			if (this.AngleLimitAxis.sqrMagnitude == 0f)
			{
				num2 = Quaternion.Angle(this._limiting_angle_targetInLocal, child.LastKeyframeLocalRotation);
			}
			else
			{
				this.AngleLimitAxis.Normalize();
				if (this.LimitAxisRange.x == this.LimitAxisRange.y)
				{
					num2 = Mathf.DeltaAngle(Vector3.Scale(child.InitialLocalRotation.eulerAngles, this.AngleLimitAxis).magnitude, Vector3.Scale(this._limiting_angle_targetInLocal.eulerAngles, this.AngleLimitAxis).magnitude);
					if (num2 < 0f)
					{
						num2 = -num2;
					}
				}
				else
				{
					num2 = Mathf.DeltaAngle(Vector3.Scale(child.InitialLocalRotation.eulerAngles, this.AngleLimitAxis).magnitude, Vector3.Scale(this._limiting_angle_targetInLocal.eulerAngles, this.AngleLimitAxis).magnitude);
					if (num2 > this.LimitAxisRange.x && num2 < this.LimitAxisRange.y)
					{
						num2 = 0f;
					}
					if (num2 < 0f)
					{
						num2 = -num2;
					}
				}
			}
			if (num2 > this.AngleLimit)
			{
				float value = Mathf.Abs(Mathf.DeltaAngle(num2, this.AngleLimit));
				num = Mathf.InverseLerp(0f, this.AngleLimit, value);
				if (this.LimitSmoothing > Mathf.Epsilon)
				{
					float num3 = Mathf.Lerp(55f, 15f, this.LimitSmoothing);
					this._limiting_angle_newLocal = Quaternion.SlerpUnclamped(this._limiting_angle_targetInLocal, child.LastKeyframeLocalRotation, this.deltaForLerps * num3 * num);
				}
				else
				{
					this._limiting_angle_newLocal = Quaternion.SlerpUnclamped(this._limiting_angle_targetInLocal, child.LastKeyframeLocalRotation, num);
				}
				this._limiting_angle_ToTargetRot = child.ParentBone.transform.rotation.QToWorld(this._limiting_angle_newLocal);
				this._limiting_limitPosition = child.ParentBone.ProceduralPosition + this._limiting_angle_ToTargetRot * Vector3.Scale(child.transform.lossyScale, child.LastKeyframeLocalPosition);
			}
			if (num > Mathf.Epsilon)
			{
				return this._limiting_limitPosition;
			}
			return targetPos;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		private void MotionInfluenceLimiting()
		{
			if (this.MotionInfluence != 1f)
			{
				this._limiting_influenceOffset = (this.BaseTransform.position - this.previousWorldPosition) * (1f - this.MotionInfluence);
				if (this.MotionInfluenceInY < 1f)
				{
					this._limiting_influenceOffset.y = (this.BaseTransform.position.y - this.previousWorldPosition.y) * (1f - this.MotionInfluenceInY);
				}
				for (int i = 0; i < this.TailSegments.Count; i++)
				{
					this.TailSegments[i].ProceduralPosition += this._limiting_influenceOffset;
					this.TailSegments[i].PreviousPosition += this._limiting_influenceOffset;
				}
				this.GhostChild.ProceduralPosition += this._limiting_influenceOffset;
				this.GhostChild.PreviousPosition += this._limiting_influenceOffset;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001C410 File Offset: 0x0001A610
		private void CalculateGravityPositionOffsetForSegment(TailAnimator2.TailSegment bone)
		{
			this._tc_segmentGravityOffset = (bone.Gravity + this.WindEffect) * bone.BoneLengthScaled;
			this._tc_segmentGravityToParentDir = bone.ProceduralPosition - bone.ParentBone.ProceduralPosition;
			this._tc_preGravOff = (this._tc_segmentGravityToParentDir + this._tc_segmentGravityOffset).normalized * this._tc_segmentGravityToParentDir.magnitude;
			bone.ProceduralPosition = bone.ParentBone.ProceduralPosition + this._tc_preGravOff;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001C4A6 File Offset: 0x0001A6A6
		private void Axis2DLimit(TailAnimator2.TailSegment child)
		{
			child.ProceduralPosition -= child.ParentBone.transform.VAxis2DLimit(child.ParentBone.ProceduralPosition, child.ProceduralPosition, this.Axis2D);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		public float GetDistanceMeasure(Vector3 targetPosition)
		{
			if (this.DistanceWithoutY)
			{
				Vector3 vector = this.BaseTransform.position + this.BaseTransform.TransformVector(this.DistanceMeasurePoint);
				return Vector2.Distance(new Vector2(vector.x, vector.z), new Vector2(targetPosition.x, targetPosition.z));
			}
			return Vector3.Distance(this.BaseTransform.position + this.BaseTransform.TransformVector(this.DistanceMeasurePoint), targetPosition);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001C568 File Offset: 0x0001A768
		private void MaxDistanceCalculations()
		{
			if (this.DistanceFrom != null)
			{
				this.finalDistanceFrom = this.DistanceFrom;
			}
			else if (this.finalDistanceFrom == null)
			{
				if (this._distanceFrom_Auto == null)
				{
					Camera camera = Camera.main;
					if (camera)
					{
						this._distanceFrom_Auto = camera.transform;
					}
					else if (!this.wasCameraSearch)
					{
						camera = UnityEngine.Object.FindObjectOfType<Camera>();
						if (camera)
						{
							this._distanceFrom_Auto = camera.transform;
						}
						this.wasCameraSearch = true;
					}
				}
				this.finalDistanceFrom = this._distanceFrom_Auto;
			}
			if (this.MaximumDistance > 0f && this.finalDistanceFrom != null)
			{
				if (!this.maxDistanceExceed)
				{
					if (this.GetDistanceMeasure(this.finalDistanceFrom.position) > this.MaximumDistance + this.MaximumDistance * this.MaxOutDistanceFactor)
					{
						this.maxDistanceExceed = true;
					}
					this.distanceWeight += Time.unscaledDeltaTime * (1f / this.FadeDuration);
					if (this.distanceWeight > 1f)
					{
						this.distanceWeight = 1f;
						return;
					}
				}
				else
				{
					if (this.GetDistanceMeasure(this.finalDistanceFrom.position) <= this.MaximumDistance)
					{
						this.maxDistanceExceed = false;
					}
					this.distanceWeight -= Time.unscaledDeltaTime * (1f / this.FadeDuration);
					if (this.distanceWeight < 0f)
					{
						this.distanceWeight = 0f;
						return;
					}
				}
			}
			else
			{
				this.maxDistanceExceed = false;
				this.distanceWeight = 1f;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		private Vector3 TailCalculations_SmoothPosition(Vector3 from, Vector3 to, TailAnimator2.TailSegment bone)
		{
			if (this.SmoothingStyle == TailAnimator2.EAnimationStyle.Accelerating)
			{
				return this.TailCalculations_SmoothPositionSmoothDamp(from, to, ref bone.VelocityHelper, bone.PositionSpeed);
			}
			if (this.SmoothingStyle == TailAnimator2.EAnimationStyle.Quick)
			{
				return this.TailCalculations_SmoothPositionLerp(from, to, bone.PositionSpeed);
			}
			return this.TailCalculations_SmoothPositionLinear(from, to, bone.PositionSpeed);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001C748 File Offset: 0x0001A948
		private Vector3 TailCalculations_SmoothPositionLerp(Vector3 from, Vector3 to, float speed)
		{
			return Vector3.Lerp(from, to, this.secPeriodDelta * speed);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001C759 File Offset: 0x0001A959
		private Vector3 TailCalculations_SmoothPositionSmoothDamp(Vector3 from, Vector3 to, ref Vector3 velo, float speed)
		{
			return Vector3.SmoothDamp(from, to, ref velo, Mathf.LerpUnclamped(0.08f, 0.0001f, Mathf.Sqrt(Mathf.Sqrt(speed))), float.PositiveInfinity, this.rateDelta);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001C789 File Offset: 0x0001A989
		private Vector3 TailCalculations_SmoothPositionLinear(Vector3 from, Vector3 to, float speed)
		{
			return Vector3.MoveTowards(from, to, this.deltaForLerps * speed * 45f);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
		private Quaternion TailCalculations_SmoothRotation(Quaternion from, Quaternion to, TailAnimator2.TailSegment bone)
		{
			if (this.SmoothingStyle == TailAnimator2.EAnimationStyle.Accelerating)
			{
				return this.TailCalculations_SmoothRotationSmoothDamp(from, to, ref bone.QVelocityHelper, bone.RotationSpeed);
			}
			if (this.SmoothingStyle == TailAnimator2.EAnimationStyle.Quick)
			{
				return this.TailCalculations_SmoothRotationLerp(from, to, bone.RotationSpeed);
			}
			return this.TailCalculations_SmoothRotationLinear(from, to, bone.RotationSpeed);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		private Quaternion TailCalculations_SmoothRotationLerp(Quaternion from, Quaternion to, float speed)
		{
			return Quaternion.Lerp(from, to, this.secPeriodDelta * speed);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001C801 File Offset: 0x0001AA01
		private Quaternion TailCalculations_SmoothRotationSmoothDamp(Quaternion from, Quaternion to, ref Quaternion velo, float speed)
		{
			return from.SmoothDampRotation(to, ref velo, Mathf.LerpUnclamped(0.25f, 0.0001f, Mathf.Sqrt(Mathf.Sqrt(speed))), this.rateDelta);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001C82C File Offset: 0x0001AA2C
		private Quaternion TailCalculations_SmoothRotationLinear(Quaternion from, Quaternion to, float speed)
		{
			return Quaternion.RotateTowards(from, to, speed * this.deltaForLerps * 1600f);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0001C843 File Offset: 0x0001AA43
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0001C84B File Offset: 0x0001AA4B
		public float _TC_TailLength
		{
			[CompilerGenerated]
			get
			{
				return this.<_TC_TailLength>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<_TC_TailLength>k__BackingField = value;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001C854 File Offset: 0x0001AA54
		private void TailCalculations_Begin()
		{
			if (this.IncludeParent)
			{
				this._tc_startI = 0;
				this._tc_rootBone = this.TailSegments[0];
			}
			else
			{
				this._tc_startI = 1;
				if (this.TailSegments.Count > 1)
				{
					this._tc_rootBone = this.TailSegments[1];
				}
				else
				{
					this._tc_rootBone = this.TailSegments[0];
					this._tc_startI = -1;
				}
			}
			this._tc_startII = this._tc_startI + 1;
			if (this._tc_startII > this.TailSegments.Count - 1)
			{
				this._tc_startII = -1;
			}
			if (this.Deflection > Mathf.Epsilon && !this._pp_initialized)
			{
				this.InitializePostProcessing();
			}
			if (this.Tangle < 0f)
			{
				this._tc_tangle = Mathf.LerpUnclamped(1f, 1.5f, this.Tangle + 1f);
				return;
			}
			this._tc_tangle = Mathf.LerpUnclamped(1f, -4f, this.Tangle);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001C958 File Offset: 0x0001AB58
		private void TailSegments_UpdateRootFeatures()
		{
			if (this.UseWaving)
			{
				this.Waving_AutoSwingUpdate();
				this._tc_startBoneRotOffset = this.WavingRotationOffset * this.RotationOffset;
			}
			else
			{
				this._tc_startBoneRotOffset = this.RotationOffset;
			}
			if (this.Sustain > Mathf.Epsilon)
			{
				this.Waving_SustainUpdate();
			}
			if (this.PostProcessingNeeded())
			{
				this.PostProcessing_Begin();
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		private void TailCalculations_SegmentPreProcessingStack(TailAnimator2.TailSegment child)
		{
			if (!this.UseCollision)
			{
				if (this.AngleLimit < 181f)
				{
					child.ProceduralPosition = this.AngleLimiting(child, child.ProceduralPosition);
				}
				if (child.PositionSpeed < 1f)
				{
					child.ProceduralPosition = this.TailCalculations_SmoothPosition(child.PreviousPosition, child.ProceduralPosition, child);
				}
			}
			else
			{
				if (child.PositionSpeed < 1f)
				{
					child.ProceduralPosition = this.TailCalculations_SmoothPosition(child.PreviousPosition, child.ProceduralPosition, child);
				}
				this.TailCalculations_ComputeSegmentCollisions(child, ref child.ProceduralPosition);
				if (this.AngleLimit < 181f)
				{
					child.ProceduralPosition = this.AngleLimiting(child, child.ProceduralPosition);
				}
			}
			if (this.MaxStretching < 1f)
			{
				this.StretchingLimiting(child);
			}
			if (!child.Gravity.VIsZero() || this.UseWind)
			{
				this.CalculateGravityPositionOffsetForSegment(child);
			}
			if (this.Axis2D > 0)
			{
				this.Axis2DLimit(child);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		private void TailCalculations_SegmentPostProcessing(TailAnimator2.TailSegment bone)
		{
			if (this.Deflection > Mathf.Epsilon)
			{
				this.Deflection_SegmentOffsetSimple(bone, ref bone.ProceduralPosition);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		private void TailCalculations_SegmentRotation(TailAnimator2.TailSegment child, Vector3 localOffset)
		{
			this._tc_lookRot = Quaternion.FromToRotation(child.ParentBone.transform.TransformDirection(localOffset), child.ProceduralPositionWeightBlended - child.ParentBone.ProceduralPositionWeightBlended);
			this._tc_targetParentRot = this._tc_lookRot * child.ParentBone.transform.rotation;
			if (this.AnimateRoll)
			{
				this._tc_targetParentRot = Quaternion.Lerp(child.ParentBone.TrueTargetRotation, this._tc_targetParentRot, this.deltaForLerps * Mathf.LerpUnclamped(10f, 60f, child.RotationSpeed));
			}
			child.ParentBone.TrueTargetRotation = this._tc_targetParentRot;
			child.ParentBone.PreviousPosReferenceRotation = child.ParentBone.PosRefRotation;
			if (!this.AnimateRoll && child.RotationSpeed < 1f)
			{
				this._tc_targetParentRot = this.TailCalculations_SmoothRotation(child.ParentBone.PosRefRotation, this._tc_targetParentRot, child);
			}
			child.ParentBone.PosRefRotation = this._tc_targetParentRot;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		private void TailCalculations_SegmentRotationDetached(TailAnimator2.TailSegment child, Vector3 localOffset)
		{
			this._tc_lookRot = Quaternion.FromToRotation(child.ParentBone.transform.TransformDirection(localOffset), child.ProceduralPositionWeightBlended - child.ParentBone.ProceduralPositionWeightBlended);
			this._tc_targetParentRot = this._tc_lookRot * child.transform.rotation;
			if (this.AnimateRoll)
			{
				this._tc_targetParentRot = Quaternion.Lerp(child.ParentBone.TrueTargetRotation, this._tc_targetParentRot, this.deltaForLerps * Mathf.LerpUnclamped(10f, 60f, child.RotationSpeed));
			}
			child.ParentBone.TrueTargetRotation = this._tc_targetParentRot;
			child.ParentBone.PreviousPosReferenceRotation = child.ParentBone.PosRefRotation;
			if (!this.AnimateRoll && child.RotationSpeed < 1f)
			{
				this._tc_targetParentRot = this.TailCalculations_SmoothRotation(child.ParentBone.PosRefRotation, this._tc_targetParentRot, child);
			}
			child.ParentBone.PosRefRotation = this._tc_targetParentRot;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		private void TailCalculations_ApplySegmentMotion(TailAnimator2.TailSegment child)
		{
			child.ParentBone.transform.rotation = child.ParentBone.TrueTargetRotation;
			child.transform.position = child.ProceduralPositionWeightBlended;
			child.RefreshFinalPos(child.ProceduralPositionWeightBlended);
			child.ParentBone.RefreshFinalRot(child.ParentBone.TrueTargetRotation);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001CD37 File Offset: 0x0001AF37
		private void TailSegment_PrepareBoneLength(TailAnimator2.TailSegment child)
		{
			child.BoneDimensionsScaled = Vector3.Scale(child.ParentBone.transform.lossyScale * child.LengthMultiplier, child.LastKeyframeLocalPosition);
			child.BoneLengthScaled = child.BoneDimensionsScaled.magnitude;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001CD78 File Offset: 0x0001AF78
		private void TailSegment_PrepareMotionParameters(TailAnimator2.TailSegment child)
		{
			this._sg_curly = Mathf.LerpUnclamped(0.5f, 0.125f, child.Curling);
			this._sg_springVelo = Mathf.LerpUnclamped(0.65f, 0.9f, child.Springiness);
			this._sg_curly = Mathf.Lerp(this._sg_curly, Mathf.LerpUnclamped(0.95f, 0.135f, child.Curling), child.Slithery);
			this._sg_springVelo = Mathf.Lerp(this._sg_springVelo, Mathf.LerpUnclamped(0.1f, 0.85f, child.Springiness), child.Slithery);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001CE14 File Offset: 0x0001B014
		private void TailSegment_PrepareVelocity(TailAnimator2.TailSegment child)
		{
			this._sg_push = child.ProceduralPosition - child.PreviousPosition;
			child.PreviousPosition = child.ProceduralPosition;
			float num = this._sg_springVelo;
			if (child.CollisionContactFlag)
			{
				num *= child.Slippery;
			}
			child.ProceduralPosition += this._sg_push * num;
			child.PreviousPush = this._sg_push;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001CE85 File Offset: 0x0001B085
		private void TailSegment_PrepareRotation(TailAnimator2.TailSegment child)
		{
			this._sg_targetChildWorldPosInParentFront = child.ParentBone.ProceduralPosition + this.TailSegment_GetSwingRotation(child, this._sg_slitFactor) * child.BoneDimensionsScaled;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001CEB5 File Offset: 0x0001B0B5
		private void TailSegment_PrepareRotationDetached(TailAnimator2.TailSegment child)
		{
			this._sg_targetChildWorldPosInParentFront = child.ParentBone.ProceduralPosition + this.TailSegment_GetSwingRotationDetached(child, this._sg_slitFactor) * child.BoneDimensionsScaled;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		private void TailSegment_BaseSwingProcessing(TailAnimator2.TailSegment child)
		{
			this._sg_slitFactor = child.Slithery;
			if (child.CollisionContactRelevancy > 0f)
			{
				this._sg_slitFactor = this.ReflectCollision;
			}
			this._sg_dirToTargetParentFront = this._sg_targetChildWorldPosInParentFront - child.ProceduralPosition;
			if (this.UnifyBendiness > 0f)
			{
				child.ProceduralPosition += this._sg_dirToTargetParentFront * this.secPeriodDelta * this._sg_curly * this.TailSegment_GetUnifiedBendinessMultiplier(child);
			}
			else
			{
				child.ProceduralPosition += this._sg_dirToTargetParentFront * this._sg_curly * this.secPeriodDelta;
			}
			if (this.Tangle != 0f && child.Slithery >= 1f)
			{
				child.ProceduralPosition = Vector3.LerpUnclamped(child.ProceduralPosition, this._sg_targetChildWorldPosInParentFront, this._tc_tangle);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		private void TailSegment_PreRotationPositionBlend(TailAnimator2.TailSegment child)
		{
			if (child.BlendValue * this.conditionalWeight < 1f)
			{
				child.ProceduralPositionWeightBlended = Vector3.LerpUnclamped(child.transform.position, child.ProceduralPosition, child.BlendValue * this.conditionalWeight);
				return;
			}
			child.ProceduralPositionWeightBlended = child.ProceduralPosition;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001D038 File Offset: 0x0001B238
		private Quaternion TailSegment_RotationSlithery(TailAnimator2.TailSegment child)
		{
			if (!child.Curving.QIsZero())
			{
				return this.GetSlitheryReferenceRotation(child) * child.Curving * child.ParentBone.LastKeyframeLocalRotation;
			}
			return this.GetSlitheryReferenceRotation(child) * child.ParentBone.LastKeyframeLocalRotation;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001D08C File Offset: 0x0001B28C
		private Quaternion TailSegment_RotationSlitheryDetached(TailAnimator2.TailSegment child)
		{
			if (!child.Curving.QIsZero())
			{
				return this.GetSlitheryReferenceRotation(child) * child.Curving * child.ParentBone.InitialLocalRotation;
			}
			return this.GetSlitheryReferenceRotation(child) * child.ParentBone.InitialLocalRotation;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		private Quaternion GetSlitheryReferenceRotation(TailAnimator2.TailSegment child)
		{
			if (child.Slithery <= 1f)
			{
				return child.ParentBone.ParentBone.PosRefRotation;
			}
			return Quaternion.LerpUnclamped(child.ParentBone.ParentBone.PosRefRotation, child.ParentBone.ParentBone.PreviousPosReferenceRotation, (child.Slithery - 1f) * 5f);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D144 File Offset: 0x0001B344
		private Quaternion TailSegment_RotationStiff(TailAnimator2.TailSegment child)
		{
			if (!child.Curving.QIsZero())
			{
				return child.ParentBone.transform.rotation * this.MultiplyQ(child.Curving, (float)child.Index * 2f);
			}
			return child.ParentBone.transform.rotation;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001D19D File Offset: 0x0001B39D
		private Quaternion TailSegment_GetSwingRotation(TailAnimator2.TailSegment child, float curlFactor)
		{
			if (curlFactor >= 1f)
			{
				return this.TailSegment_RotationSlithery(child);
			}
			if (curlFactor > Mathf.Epsilon)
			{
				return Quaternion.LerpUnclamped(this.TailSegment_RotationStiff(child), this.TailSegment_RotationSlithery(child), curlFactor);
			}
			return this.TailSegment_RotationStiff(child);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		private Quaternion TailSegment_GetSwingRotationDetached(TailAnimator2.TailSegment child, float curlFactor)
		{
			if (curlFactor >= 1f)
			{
				return this.TailSegment_RotationSlitheryDetached(child);
			}
			if (curlFactor > Mathf.Epsilon)
			{
				return Quaternion.LerpUnclamped(this.TailSegment_RotationStiff(child), this.TailSegment_RotationSlitheryDetached(child), curlFactor);
			}
			return this.TailSegment_RotationStiff(child);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001D20C File Offset: 0x0001B40C
		private float TailSegment_GetUnifiedBendinessMultiplier(TailAnimator2.TailSegment child)
		{
			float num = child.BoneLength / this._TC_TailLength;
			num = Mathf.Pow(num, 0.5f);
			if (num == 0f)
			{
				num = 1f;
			}
			float num2 = this._sg_curly / num / 2f;
			num2 = Mathf.LerpUnclamped(this._sg_curly, num2, this.UnifyBendiness);
			if (num2 < 0.15f)
			{
				num2 = 0.15f;
			}
			else if (num2 > 1.4f)
			{
				num2 = 1.4f;
			}
			return num2;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001D284 File Offset: 0x0001B484
		public void TailSegments_UpdateCoordsForRootBone(TailAnimator2.TailSegment parent)
		{
			TailAnimator2.TailSegment tailSegment = this.TailSegments[0];
			tailSegment.transform.localRotation = tailSegment.LastKeyframeLocalRotation * this._tc_startBoneRotOffset;
			parent.PreviousPosReferenceRotation = parent.PosRefRotation;
			parent.PosRefRotation = parent.transform.rotation;
			parent.PreviousPosition = parent.ProceduralPosition;
			parent.ProceduralPosition = parent.transform.position;
			if (this.DetachChildren)
			{
				tailSegment.TrueTargetRotation = tailSegment.transform.rotation;
			}
			parent.RefreshFinalPos(parent.transform.position);
			parent.ProceduralPositionWeightBlended = parent.ProceduralPosition;
			if (parent.ParentBone.transform != null)
			{
				parent.ParentBone.PreviousPosReferenceRotation = parent.ParentBone.PosRefRotation;
				parent.ParentBone.PreviousPosition = parent.ParentBone.ProceduralPosition;
				parent.ParentBone.ProceduralPosition = parent.ParentBone.transform.position;
				parent.ParentBone.PosRefRotation = parent.ParentBone.transform.rotation;
				parent.ParentBone.ProceduralPositionWeightBlended = parent.ParentBone.ProceduralPosition;
			}
			this.TailSegments[this._tc_startI].ChildBone.PreviousPosition += this._waving_sustain;
			tailSegment.RefreshKeyLocalPosition();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		public void TailCalculations_UpdateArtificialChildBone(TailAnimator2.TailSegment child)
		{
			if (this.DetachChildren)
			{
				this.TailSegment_PrepareRotationDetached(child);
			}
			else
			{
				this.TailSegment_PrepareRotation(child);
			}
			this.TailSegment_BaseSwingProcessing(child);
			if (child.PositionSpeed < 1f)
			{
				child.ProceduralPosition = this.TailCalculations_SmoothPosition(child.PreviousPosition, child.ProceduralPosition, child);
			}
			if (this.MaxStretching < 1f)
			{
				this.StretchingLimiting(child);
			}
			if (!child.Gravity.VIsZero() || this.UseWind)
			{
				this.CalculateGravityPositionOffsetForSegment(child);
			}
			if (this.Axis2D > 0)
			{
				this.Axis2DLimit(child);
			}
			child.CollisionContactRelevancy = -1f;
			if (child.BlendValue * this.conditionalWeight < 1f)
			{
				child.ProceduralPositionWeightBlended = Vector3.LerpUnclamped(child.ParentBone.transform.TransformPoint(child.LastKeyframeLocalPosition), child.ProceduralPosition, child.BlendValue * this.conditionalWeight);
				return;
			}
			child.ProceduralPositionWeightBlended = child.ProceduralPosition;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public void Editor_TailCalculations_RefreshArtificialParentBone()
		{
			this.GhostParent.ProceduralPosition = this.GhostParent.transform.position + this.GhostParent.transform.rotation.TransformVector(this.GhostParent.transform.lossyScale, this.GhostParent.LocalOffset);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001D53C File Offset: 0x0001B73C
		private void SimulateTailMotionFrame(bool pp)
		{
			this.TailSegments_UpdateRootFeatures();
			this.TailSegments_UpdateCoordsForRootBone(this._tc_rootBone);
			if (pp)
			{
				this.PostProcessing_ReferenceUpdate();
			}
			if (this._tc_startI > -1)
			{
				TailAnimator2.TailSegment tailSegment = this.TailSegments[this._tc_startI];
				if (!this.DetachChildren)
				{
					while (tailSegment != this.GhostChild)
					{
						tailSegment.BoneDimensionsScaled = Vector3.Scale(tailSegment.ParentBone.transform.lossyScale * tailSegment.LengthMultiplier, tailSegment.LastKeyframeLocalPosition);
						tailSegment.BoneLengthScaled = tailSegment.BoneDimensionsScaled.magnitude;
						this.TailSegment_PrepareBoneLength(tailSegment);
						this.TailSegment_PrepareMotionParameters(tailSegment);
						this.TailSegment_PrepareVelocity(tailSegment);
						tailSegment = tailSegment.ChildBone;
					}
				}
				else
				{
					while (tailSegment != this.GhostChild)
					{
						tailSegment.BoneDimensionsScaled = Vector3.Scale(tailSegment.ParentBone.transform.lossyScale * tailSegment.LengthMultiplier, tailSegment.InitialLocalPosition);
						tailSegment.BoneLengthScaled = tailSegment.BoneDimensionsScaled.magnitude;
						this.TailSegment_PrepareMotionParameters(tailSegment);
						this.TailSegment_PrepareVelocity(tailSegment);
						tailSegment = tailSegment.ChildBone;
					}
				}
			}
			this.TailSegment_PrepareBoneLength(this.GhostChild);
			this.TailSegment_PrepareMotionParameters(this.GhostChild);
			this.TailSegment_PrepareVelocity(this.GhostChild);
			if (this._tc_startII > -1)
			{
				TailAnimator2.TailSegment tailSegment2 = this.TailSegments[this._tc_startII];
				if (!this.DetachChildren)
				{
					while (tailSegment2 != this.GhostChild)
					{
						this.TailSegment_PrepareRotation(tailSegment2);
						this.TailSegment_BaseSwingProcessing(tailSegment2);
						this.TailCalculations_SegmentPreProcessingStack(tailSegment2);
						if (pp)
						{
							this.TailCalculations_SegmentPostProcessing(tailSegment2);
						}
						this.TailSegment_PreRotationPositionBlend(tailSegment2);
						tailSegment2 = tailSegment2.ChildBone;
					}
				}
				else
				{
					while (tailSegment2 != this.GhostChild)
					{
						this.TailSegment_PrepareRotationDetached(tailSegment2);
						this.TailSegment_BaseSwingProcessing(tailSegment2);
						this.TailCalculations_SegmentPreProcessingStack(tailSegment2);
						if (pp)
						{
							this.TailCalculations_SegmentPostProcessing(tailSegment2);
						}
						this.TailSegment_PreRotationPositionBlend(tailSegment2);
						tailSegment2 = tailSegment2.ChildBone;
					}
				}
			}
			this.TailCalculations_UpdateArtificialChildBone(this.GhostChild);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001D714 File Offset: 0x0001B914
		private void UpdateTailAlgorithm()
		{
			this.TailCalculations_Begin();
			if (this.framesToSimulate != 0)
			{
				if (this.UseCollision)
				{
					this.BeginCollisionsUpdate();
				}
				bool pp = this.PostProcessingNeeded();
				this.MotionInfluenceLimiting();
				for (int i = 0; i < this.framesToSimulate; i++)
				{
					this.SimulateTailMotionFrame(pp);
				}
				this.TailSegments[this._tc_startI].transform.position = this.TailSegments[this._tc_startI].ProceduralPositionWeightBlended;
				this.TailSegments[this._tc_startI].RefreshFinalPos(this.TailSegments[this._tc_startI].ProceduralPositionWeightBlended);
				if (!this.DetachChildren)
				{
					if (this._tc_startII > -1)
					{
						for (TailAnimator2.TailSegment tailSegment = this.TailSegments[this._tc_startII]; tailSegment != this.GhostChild; tailSegment = tailSegment.ChildBone)
						{
							this.TailCalculations_SegmentRotation(tailSegment, tailSegment.LastKeyframeLocalPosition);
							this.TailCalculations_ApplySegmentMotion(tailSegment);
						}
					}
				}
				else if (this._tc_startII > -1)
				{
					for (TailAnimator2.TailSegment tailSegment2 = this.TailSegments[this._tc_startII]; tailSegment2 != this.GhostChild; tailSegment2 = tailSegment2.ChildBone)
					{
						this.TailCalculations_SegmentRotation(tailSegment2, tailSegment2.InitialLocalPosition);
						this.TailCalculations_ApplySegmentMotion(tailSegment2);
					}
				}
				this.TailCalculations_SegmentRotation(this.GhostChild, this.GhostChild.LastKeyframeLocalPosition);
				this.GhostChild.ParentBone.transform.rotation = this.GhostChild.ParentBone.TrueTargetRotation;
				this.GhostChild.ParentBone.RefreshFinalRot(this.GhostChild.ParentBone.TrueTargetRotation);
				if (this.GhostChild.transform)
				{
					this.GhostChild.RefreshFinalPos(this.GhostChild.transform.position);
					this.GhostChild.RefreshFinalRot(this.GhostChild.transform.rotation);
					return;
				}
			}
			else
			{
				if (this.InterpolateRate)
				{
					this.secPeriodDelta = this.rateDelta / 24f;
					this.deltaForLerps = this.secPeriodDelta;
					this.SimulateTailMotionFrame(this.PostProcessingNeeded());
					if (this._tc_startII > -1)
					{
						for (TailAnimator2.TailSegment tailSegment3 = this.TailSegments[this._tc_startII]; tailSegment3 != this.GhostChild; tailSegment3 = tailSegment3.ChildBone)
						{
							this.TailCalculations_SegmentRotation(tailSegment3, tailSegment3.LastKeyframeLocalPosition);
							this.TailCalculations_ApplySegmentMotion(tailSegment3);
						}
					}
					this.TailCalculations_SegmentRotation(this.GhostChild, this.GhostChild.LastKeyframeLocalPosition);
					this.GhostChild.ParentBone.transform.rotation = this.GhostChild.ParentBone.TrueTargetRotation;
					this.GhostChild.ParentBone.RefreshFinalRot(this.GhostChild.ParentBone.TrueTargetRotation);
					return;
				}
				if (this._tc_startI > -1)
				{
					TailAnimator2.TailSegment tailSegment4 = this.TailSegments[this._tc_startI];
					while (tailSegment4 != null && tailSegment4.transform)
					{
						tailSegment4.transform.position = tailSegment4.LastFinalPosition;
						tailSegment4.transform.rotation = tailSegment4.LastFinalRotation;
						tailSegment4 = tailSegment4.ChildBone;
					}
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private void CheckIfTailAnimatorShouldBeUpdated()
		{
			if (!this.initialized)
			{
				if (this.StartAfterTPose)
				{
					this.startAfterTPoseCounter++;
					if (this.startAfterTPoseCounter > 6)
					{
						this.Init();
					}
				}
				this.updateTailAnimator = false;
				return;
			}
			if (this.UseMaxDistance)
			{
				this.MaxDistanceCalculations();
				this.conditionalWeight = this.OverrideWeight * this.distanceWeight;
			}
			else
			{
				this.conditionalWeight = this.OverrideWeight;
			}
			if (this._forceDisable)
			{
				if (this.FadeDuration > 0f)
				{
					this._forceDisableElapsed += Time.unscaledDeltaTime * (1f / this.FadeDuration);
					if (this._forceDisableElapsed > 1f)
					{
						this._forceDisableElapsed = 1f;
					}
				}
				else
				{
					this._forceDisableElapsed = 1f;
				}
				this.conditionalWeight *= 1f - this._forceDisableElapsed;
			}
			else if (this._forceDisableElapsed > 0f && this.FadeDuration > 0f)
			{
				this._forceDisableElapsed -= Time.unscaledDeltaTime * (1f / this.FadeDuration);
				if (this._forceDisableElapsed < 0f)
				{
					this._forceDisableElapsed = 0f;
				}
				this.conditionalWeight *= 1f - this._forceDisableElapsed;
			}
			if (this.DisabledByInvisibility())
			{
				return;
			}
			if (this.UseCollision && !this.collisionInitialized)
			{
				this.SetupSphereColliders();
			}
			if (this.TailSegments.Count == 0)
			{
				UnityEngine.Debug.LogError("[TAIL ANIMATOR] No tail bones defined in " + base.name + " !");
				this.initialized = false;
				this.updateTailAnimator = false;
				return;
			}
			if (this.TailAnimatorAmount * this.conditionalWeight <= Mathf.Epsilon)
			{
				this.wasDisabled = true;
				this.updateTailAnimator = false;
				return;
			}
			if (this.wasDisabled)
			{
				this.User_ReposeTail();
				this.previousWorldPosition = base.transform.position;
				this.wasDisabled = false;
			}
			if (this.IncludeParent && this.TailSegments.Count > 0 && !this.TailSegments[0].transform.parent)
			{
				this.IncludeParent = false;
			}
			if (this.TailSegments.Count < 1)
			{
				this.updateTailAnimator = false;
				return;
			}
			this.updateTailAnimator = true;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public bool DisabledByInvisibility()
		{
			if (this.OptimizeWithMesh != null)
			{
				bool flag = false;
				if (this.OptimizeWithMesh.isVisible)
				{
					flag = true;
				}
				else if (this.OptimizeWithMeshes != null && this.OptimizeWithMeshes.Length != 0)
				{
					for (int i = 0; i < this.OptimizeWithMeshes.Length; i++)
					{
						if (!(this.OptimizeWithMeshes[i] == null) && this.OptimizeWithMeshes[i].isVisible)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.updateTailAnimator = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001DCF8 File Offset: 0x0001BEF8
		private void DeltaTimeCalculations()
		{
			if (this.UpdateRate > 0)
			{
				switch (this.DeltaType)
				{
				case TailAnimator2.EFDeltaType.DeltaTime:
				case TailAnimator2.EFDeltaType.SafeDelta:
					this.justDelta = Time.deltaTime / Mathf.Clamp(Time.timeScale, 0.01f, 1f);
					break;
				case TailAnimator2.EFDeltaType.SmoothDeltaTime:
					this.justDelta = Time.smoothDeltaTime;
					break;
				case TailAnimator2.EFDeltaType.UnscaledDeltaTime:
					this.justDelta = Time.unscaledDeltaTime;
					break;
				case TailAnimator2.EFDeltaType.FixedDeltaTime:
					this.justDelta = Time.fixedDeltaTime;
					break;
				}
				this.justDelta *= this.TimeScale;
				this.secPeriodDelta = 1f;
				this.deltaForLerps = this.secPeriodDelta;
				this.rateDelta = 1f / (float)this.UpdateRate;
				this.StableUpdateRateCalculations();
				return;
			}
			switch (this.DeltaType)
			{
			case TailAnimator2.EFDeltaType.DeltaTime:
				this.justDelta = Time.deltaTime;
				break;
			case TailAnimator2.EFDeltaType.SmoothDeltaTime:
				this.justDelta = Time.smoothDeltaTime;
				break;
			case TailAnimator2.EFDeltaType.UnscaledDeltaTime:
				this.justDelta = Time.unscaledDeltaTime;
				break;
			case TailAnimator2.EFDeltaType.FixedDeltaTime:
				this.justDelta = Time.fixedDeltaTime;
				break;
			case TailAnimator2.EFDeltaType.SafeDelta:
				this.justDelta = Mathf.Lerp(this.justDelta, this.GetClampedSmoothDelta(), 0.075f);
				break;
			}
			this.rateDelta = this.justDelta;
			this.deltaForLerps = Mathf.Pow(this.secPeriodDelta, 0.1f) * 0.02f;
			this.justDelta *= this.TimeScale;
			this.secPeriodDelta = Mathf.Min(1f, this.justDelta * 60f);
			this.framesToSimulate = 1;
			this.previousframesToSimulate = 1;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001DE98 File Offset: 0x0001C098
		private void StableUpdateRateCalculations()
		{
			this.previousframesToSimulate = this.framesToSimulate;
			this.collectedDelta += this.justDelta;
			this.framesToSimulate = 0;
			while (this.collectedDelta >= this.rateDelta)
			{
				this.collectedDelta -= this.rateDelta;
				this.framesToSimulate++;
				if (this.framesToSimulate >= 3)
				{
					this.collectedDelta = 0f;
					return;
				}
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001DF14 File Offset: 0x0001C114
		private void PreCalibrateBones()
		{
			for (TailAnimator2.TailSegment tailSegment = this.TailSegments[0]; tailSegment != this.GhostChild; tailSegment = tailSegment.ChildBone)
			{
				tailSegment.PreCalibrate();
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001DF48 File Offset: 0x0001C148
		private void CalibrateBones()
		{
			if (this.UseIK && this.IKBlend > 0f)
			{
				this.UpdateIK();
			}
			this._limiting_stretchingHelperTooLong = Mathf.Lerp(0.4f, 0f, this.MaxStretching);
			this._limiting_stretchingHelperTooShort = this._limiting_stretchingHelperTooLong * 1.5f;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		public void CheckForNullsInGhostChain()
		{
			if (this._TransformsGhostChain == null)
			{
				this._TransformsGhostChain = new List<Transform>();
			}
			for (int i = this._TransformsGhostChain.Count - 1; i >= 0; i--)
			{
				if (this._TransformsGhostChain[i] == null)
				{
					this._TransformsGhostChain.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		private float GetClampedSmoothDelta()
		{
			return Mathf.Clamp(Time.smoothDeltaTime, 0f, 0.25f);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001E010 File Offset: 0x0001C210
		private Quaternion MultiplyQ(Quaternion rotation, float times)
		{
			return Quaternion.AngleAxis(rotation.x * 57.29578f * times, Vector3.right) * Quaternion.AngleAxis(rotation.z * 57.29578f * times, Vector3.forward) * Quaternion.AngleAxis(rotation.y * 57.29578f * times, Vector3.up);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001E06F File Offset: 0x0001C26F
		public float GetValueFromCurve(int i, AnimationCurve c)
		{
			if (!this.initialized)
			{
				return c.Evaluate((float)i / (float)this._TransformsGhostChain.Count);
			}
			return c.Evaluate(this.TailSegments[i].IndexOverlLength);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public AnimationCurve ClampCurve(AnimationCurve a, float timeStart, float timeEnd, float lowest, float highest)
		{
			Keyframe[] keys = a.keys;
			for (int i = 0; i < keys.Length; i++)
			{
				if (keys[i].time < timeStart)
				{
					keys[i].time = timeStart;
				}
				else if (keys[i].time > timeEnd)
				{
					keys[i].time = timeEnd;
				}
				if (keys[i].value < lowest)
				{
					keys[i].value = lowest;
				}
				else if (keys[i].value > highest)
				{
					keys[i].value = highest;
				}
			}
			a.keys = keys;
			return a;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001E150 File Offset: 0x0001C350
		public void RefreshTransformsList()
		{
			if (this._TransformsGhostChain == null)
			{
				this._TransformsGhostChain = new List<Transform>();
				return;
			}
			for (int i = this._TransformsGhostChain.Count - 1; i >= 0; i--)
			{
				if (this._TransformsGhostChain[0] == null)
				{
					this._TransformsGhostChain.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001E1A9 File Offset: 0x0001C3A9
		public TailAnimator2.TailSegment GetGhostChild()
		{
			return this.GhostChild;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001E1B1 File Offset: 0x0001C3B1
		private IEnumerator LateFixed()
		{
			WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
			this.lateFixedIsRunning = true;
			for (;;)
			{
				yield return fixedWait;
				this.PreCalibrateBones();
				this.fixedAllow = true;
			}
			yield break;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		private void Deflection_BeginUpdate()
		{
			this._defl_treshold = this.DeflectionStartAngle / 90f;
			float smoothTime = this.DeflectionSmooth / 9f;
			for (int i = this._tc_startII; i < this.TailSegments.Count; i++)
			{
				TailAnimator2.TailSegment tailSegment = this._pp_reference[i];
				if (!tailSegment.CheckDeflectionState(this._defl_treshold, smoothTime, this.rateDelta))
				{
					bool flag = true;
					if (this.DeflectOnlyCollisions && tailSegment.CollisionContactRelevancy <= 0f)
					{
						flag = false;
					}
					if (flag)
					{
						this.Deflection_AddDeflectionSource(tailSegment);
					}
					else
					{
						this.Deflection_RemoveDeflectionSource(tailSegment);
					}
				}
				else
				{
					this.Deflection_RemoveDeflectionSource(tailSegment);
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001E260 File Offset: 0x0001C460
		private void Deflection_RemoveDeflectionSource(TailAnimator2.TailSegment child)
		{
			if (child.DeflectionRestoreState() == null && this._defl_source.Contains(child))
			{
				this._defl_source.Remove(child);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001E298 File Offset: 0x0001C498
		private void Deflection_AddDeflectionSource(TailAnimator2.TailSegment child)
		{
			if (child.DeflectionRelevant() && !this._defl_source.Contains(child))
			{
				this._defl_source.Add(child);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		private void Deflection_SegmentOffsetSimple(TailAnimator2.TailSegment child, ref Vector3 position)
		{
			if (child.Index == this._tc_startI)
			{
				return;
			}
			float num = 0f;
			for (int i = 0; i < this._defl_source.Count; i++)
			{
				if (child.Index <= this._defl_source[i].Index && child.Index != this._defl_source[i].Index && this._defl_source[i].DeflectionFactor >= num)
				{
					num = this._defl_source[i].DeflectionFactor;
					float a = 0f;
					if (i > 0)
					{
						a = (float)this._defl_source[i].Index;
					}
					float time = Mathf.InverseLerp(a, (float)this._defl_source[i].Index, (float)child.Index);
					Vector3 vector = this._defl_source[i].DeflectionWorldPosition - child.ParentBone.ProceduralPosition;
					Vector3 vector2 = child.ParentBone.ProceduralPosition;
					vector2 += vector.normalized * child.BoneLengthScaled;
					child.ProceduralPosition = Vector3.LerpUnclamped(child.ProceduralPosition, vector2, this.Deflection * this.DeflectionFalloff.Evaluate(time) * this._defl_source[i].DeflectionSmooth);
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001E41B File Offset: 0x0001C61B
		public void User_SetTailTransforms(List<Transform> list)
		{
			this.StartBone = list[0];
			this.EndBone = list[list.Count - 1];
			this._TransformsGhostChain = list;
			this.StartAfterTPose = false;
			this.initialized = false;
			this.Init();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001E45C File Offset: 0x0001C65C
		public TailAnimator2.TailSegment User_AddTailTransform(Transform transform)
		{
			TailAnimator2.TailSegment tailSegment = new TailAnimator2.TailSegment(transform);
			TailAnimator2.TailSegment tailSegment2 = this.TailSegments[this.TailSegments.Count - 1];
			tailSegment.ParamsFromAll(tailSegment2);
			tailSegment.RefreshFinalPos(tailSegment.transform.position);
			tailSegment.RefreshFinalRot(tailSegment.transform.rotation);
			tailSegment.ProceduralPosition = tailSegment.transform.position;
			tailSegment.PosRefRotation = tailSegment.transform.rotation;
			this._TransformsGhostChain.Add(transform);
			this.TailSegments.Add(tailSegment);
			tailSegment2.SetChildRef(tailSegment);
			tailSegment.SetParentRef(tailSegment2);
			tailSegment.SetChildRef(this.GhostChild);
			this.GhostChild.SetParentRef(tailSegment);
			for (int i = 0; i < this.TailSegments.Count; i++)
			{
				this.TailSegments[i].SetIndex(i, this.TailSegments.Count);
			}
			return tailSegment;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001E548 File Offset: 0x0001C748
		public void User_CutEndSegmentsTo(int fromLastTo)
		{
			if (fromLastTo < this.TailSegments.Count)
			{
				this.GhostChild = this.TailSegments[fromLastTo];
				this.GhostChild.SetChildRef(null);
				for (int i = this.TailSegments.Count - 1; i >= fromLastTo; i--)
				{
					this.TailSegments.RemoveAt(i);
					this._TransformsGhostChain.RemoveAt(i);
				}
				return;
			}
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"[Tail Animator Cutting] Wrong index, you want cut from end to ",
				fromLastTo.ToString(),
				" segment but there are only ",
				this.TailSegments.Count.ToString(),
				" segments!"
			}));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		public void User_ReposeTail()
		{
			this.GhostParent.Reset();
			for (int i = 0; i < this.TailSegments.Count; i++)
			{
				this.TailSegments[i].Reset();
			}
			this.GhostChild.Reset();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001E646 File Offset: 0x0001C846
		public void User_ForceDisabled(bool disable)
		{
			this._forceDisable = disable;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001E64F File Offset: 0x0001C84F
		public string EditorIconPath
		{
			get
			{
				if (PlayerPrefs.GetInt("AnimsH", 1) == 0)
				{
					return "";
				}
				return "Tail Animator/Tail Animator Icon Small";
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001E669 File Offset: 0x0001C869
		public void OnDrop(PointerEventData data)
		{
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001E66B File Offset: 0x0001C86B
		public bool IsInitialized
		{
			get
			{
				return this.initialized;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001E674 File Offset: 0x0001C874
		private void OnValidate()
		{
			if (this.UpdateRate < 0)
			{
				this.UpdateRate = 0;
			}
			if (Application.isPlaying)
			{
				this.RefreshSegmentsColliders();
				if (this.UseIK)
				{
					this.IK_ApplyLimitBoneSettings();
				}
			}
			if (this.UsePartialBlend)
			{
				this.ClampCurve(this.BlendCurve, 0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		public void GetGhostChain()
		{
			if (this._TransformsGhostChain == null)
			{
				this._TransformsGhostChain = new List<Transform>();
			}
			if (this.EndBone == null)
			{
				this._TransformsGhostChain.Clear();
				Transform transform = this.StartBone;
				if (transform == null)
				{
					transform = base.transform;
				}
				this._TransformsGhostChain.Add(transform);
				while (transform.childCount > 0)
				{
					transform = transform.GetChild(0);
					if (!this._TransformsGhostChain.Contains(transform))
					{
						this._TransformsGhostChain.Add(transform);
					}
				}
				this._GhostChainInitCount = this._TransformsGhostChain.Count;
				return;
			}
			List<Transform> list = new List<Transform>();
			Transform transform2 = this.StartBone;
			if (transform2 == null)
			{
				transform2 = base.transform;
			}
			Transform transform3 = this.EndBone;
			list.Add(transform3);
			while (transform3 != null && transform3 != this.StartBone)
			{
				transform3 = transform3.parent;
				if (!list.Contains(transform3))
				{
					list.Add(transform3);
				}
			}
			if (transform3 == null)
			{
				UnityEngine.Debug.Log(string.Concat(new string[]
				{
					"[Tail Animator Editor] ",
					this.EndBone.name,
					" is not child of ",
					transform2.name,
					"!"
				}));
				UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"[Tail Animator Editor] ",
					this.EndBone.name,
					" is not child of ",
					transform2.name,
					"!"
				}));
				return;
			}
			if (!list.Contains(transform3))
			{
				list.Add(transform3);
			}
			this._TransformsGhostChain.Clear();
			this._TransformsGhostChain = list;
			this._TransformsGhostChain.Reverse();
			this._GhostChainInitCount = this._TransformsGhostChain.Count;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001E894 File Offset: 0x0001CA94
		public Transform BaseTransform
		{
			get
			{
				if (this._baseTransform)
				{
					return this._baseTransform;
				}
				if (this._TransformsGhostChain != null && this._TransformsGhostChain.Count > 0)
				{
					this._baseTransform = this._TransformsGhostChain[0];
				}
				if (this._baseTransform != null)
				{
					return this._baseTransform;
				}
				return base.transform;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		private void Start()
		{
			if (this.UpdateAsLast)
			{
				base.enabled = false;
				base.enabled = true;
			}
			if (this.StartAfterTPose)
			{
				this.startAfterTPoseCounter = 6;
				return;
			}
			this.Init();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001E928 File Offset: 0x0001CB28
		private void Reset()
		{
			Keyframe keyframe = new Keyframe(0f, 0f, 0.1f, 0.1f, 0f, 0.5f);
			Keyframe keyframe2 = new Keyframe(1f, 1f, 5f, 0f, 0.1f, 0f);
			this.DeflectionFalloff = new AnimationCurve(new Keyframe[]
			{
				keyframe,
				keyframe2
			});
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		private void Update()
		{
			this.CheckIfTailAnimatorShouldBeUpdated();
			this.DeltaTimeCalculations();
			if (this.UseWind)
			{
				this.WindEffectUpdate();
			}
			if (this.AnimatePhysics != TailAnimator2.EFixedMode.None)
			{
				return;
			}
			if (!this.updateTailAnimator)
			{
				return;
			}
			if (this.DetachChildren)
			{
				if (this._tc_rootBone != null && this._tc_rootBone.transform)
				{
					this._tc_rootBone.PreCalibrate();
				}
				return;
			}
			if (this.OverrideKeyframeAnimation < 1f)
			{
				this.PreCalibrateBones();
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		private void FixedUpdate()
		{
			if (this.AnimatePhysics != TailAnimator2.EFixedMode.Basic)
			{
				return;
			}
			if (!this.updateTailAnimator)
			{
				return;
			}
			if (this.DetachChildren)
			{
				if (this._tc_rootBone != null && this._tc_rootBone.transform)
				{
					this._tc_rootBone.PreCalibrate();
				}
				return;
			}
			this.fixedUpdated = true;
			this.PreCalibrateBones();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001EA78 File Offset: 0x0001CC78
		private void LateUpdate()
		{
			if (!this.updateTailAnimator)
			{
				return;
			}
			if (this.AnimatePhysics == TailAnimator2.EFixedMode.Late)
			{
				if (!this.lateFixedIsRunning)
				{
					base.StartCoroutine(this.LateFixed());
				}
				if (!this.fixedAllow)
				{
					return;
				}
				this.fixedAllow = false;
			}
			else
			{
				if (this.lateFixedIsRunning)
				{
					base.StopCoroutine(this.LateFixed());
					this.lateFixedIsRunning = false;
				}
				if (this.AnimatePhysics == TailAnimator2.EFixedMode.Basic)
				{
					if (!this.fixedUpdated)
					{
						return;
					}
					this.fixedUpdated = false;
				}
			}
			if (this.DetachChildren)
			{
				TailAnimator2.TailSegment tailSegment = this.TailSegments[0];
				this.TailSegments[0].RefreshKeyLocalPositionAndRotation(tailSegment.InitialLocalPosition, tailSegment.InitialLocalRotation);
				this.TailSegments[0].PreCalibrate();
				tailSegment = this.TailSegments[1];
				if (!this.IncludeParent)
				{
					tailSegment.RefreshKeyLocalPositionAndRotation(tailSegment.InitialLocalPosition, tailSegment.InitialLocalRotation);
					tailSegment.PreCalibrate();
					tailSegment = this.TailSegments[2];
				}
				while (tailSegment != this.GhostChild)
				{
					tailSegment.RefreshKeyLocalPositionAndRotation(tailSegment.InitialLocalPosition, tailSegment.InitialLocalRotation);
					tailSegment.transform.position = this._baseTransform.TransformPoint(tailSegment.InitialLocalPositionInRoot);
					tailSegment.transform.rotation = this._baseTransform.rotation.QToWorld(tailSegment.InitialLocalRotationInRoot);
					tailSegment = tailSegment.ChildBone;
				}
			}
			else if (this.OverrideKeyframeAnimation > 0f)
			{
				if (this.OverrideKeyframeAnimation >= 1f)
				{
					this.PreCalibrateBones();
					for (TailAnimator2.TailSegment tailSegment2 = this.TailSegments[0]; tailSegment2 != this.GhostChild; tailSegment2 = tailSegment2.ChildBone)
					{
						tailSegment2.RefreshKeyLocalPositionAndRotation();
					}
				}
				else
				{
					for (TailAnimator2.TailSegment tailSegment3 = this.TailSegments[0]; tailSegment3 != this.GhostChild; tailSegment3 = tailSegment3.ChildBone)
					{
						tailSegment3.transform.localPosition = Vector3.LerpUnclamped(tailSegment3.transform.localPosition, tailSegment3.InitialLocalPosition, this.OverrideKeyframeAnimation);
						tailSegment3.transform.localRotation = Quaternion.LerpUnclamped(tailSegment3.transform.localRotation, tailSegment3.InitialLocalRotation, this.OverrideKeyframeAnimation);
						tailSegment3.RefreshKeyLocalPositionAndRotation();
					}
				}
			}
			else
			{
				for (TailAnimator2.TailSegment tailSegment4 = this.TailSegments[0]; tailSegment4 != this.GhostChild; tailSegment4 = tailSegment4.ChildBone)
				{
					tailSegment4.RefreshKeyLocalPositionAndRotation();
				}
			}
			this.ExpertParamsUpdate();
			this.ShapingParamsUpdate();
			this.CalibrateBones();
			this.UpdateTailAlgorithm();
			this.EndUpdate();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001ECDD File Offset: 0x0001CEDD
		private void EndUpdate()
		{
			this.ShapingEndUpdate();
			this.ExpertCurvesEndUpdate();
			this.previousWorldPosition = this.BaseTransform.position;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		public TailAnimator2()
		{
		}

		// Token: 0x0400030B RID: 779
		[Tooltip("Using some simple calculations to make tail bend on colliders")]
		public bool UseCollision;

		// Token: 0x0400030C RID: 780
		[Tooltip("How collision should be detected, world gives you collision on all world colliders but with more use of cpu (using unity's rigidbodies), 'Selective' gives you possibility to detect collision on selected colliders without using Rigidbodies, it also gives smoother motion (deactivated colliders will still detect collision, unless its game object is disabled)")]
		public TailAnimator2.ECollisionSpace CollisionSpace = TailAnimator2.ECollisionSpace.Selective_Fast;

		// Token: 0x0400030D RID: 781
		public TailAnimator2.ECollisionMode CollisionMode;

		// Token: 0x0400030E RID: 782
		[Tooltip("If you want to stop checking collision if segment collides with one collider\n\nSegment collision with two or more colliders in the same time with this option enabled can result in stuttery motion")]
		public bool CheapCollision;

		// Token: 0x0400030F RID: 783
		[Tooltip("Using trigger collider to include encountered colliders into collide with list")]
		public bool DynamicWorldCollidersInclusion;

		// Token: 0x04000310 RID: 784
		[Tooltip("Radius of trigger collider for dynamic inclusion of colliders")]
		public float InclusionRadius = 1f;

		// Token: 0x04000311 RID: 785
		public bool IgnoreMeshColliders = true;

		// Token: 0x04000312 RID: 786
		public List<Collider> IncludedColliders;

		// Token: 0x04000313 RID: 787
		public List<Collider2D> IncludedColliders2D;

		// Token: 0x04000314 RID: 788
		[CompilerGenerated]
		private List<Component> <DynamicAlwaysInclude>k__BackingField;

		// Token: 0x04000315 RID: 789
		protected List<FImp_ColliderData_Base> IncludedCollidersData;

		// Token: 0x04000316 RID: 790
		protected List<FImp_ColliderData_Base> CollidersDataToCheck;

		// Token: 0x04000317 RID: 791
		[Tooltip("Capsules can give much more precise collision detection")]
		public int CollidersType;

		// Token: 0x04000318 RID: 792
		public bool CollideWithOtherTails;

		// Token: 0x04000319 RID: 793
		[Tooltip("Collision with colliders even if they're disabled (but game object must be enabled)\nHelpful to setup character limbs collisions without need to create new Layer")]
		public bool CollideWithDisabledColliders = true;

		// Token: 0x0400031A RID: 794
		[Range(0f, 1f)]
		public float CollisionSlippery = 1f;

		// Token: 0x0400031B RID: 795
		[Tooltip("If tail colliding objects should fit to colliders (0) or be reflect from them (Reflecting Only with 'Slithery' parameter greater than ~0.2)")]
		[Range(0f, 1f)]
		public float ReflectCollision;

		// Token: 0x0400031C RID: 796
		public AnimationCurve CollidersScaleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		// Token: 0x0400031D RID: 797
		public float CollidersScaleMul = 6.5f;

		// Token: 0x0400031E RID: 798
		[Range(0f, 1f)]
		public float CollisionsAutoCurve = 0.5f;

		// Token: 0x0400031F RID: 799
		public List<Collider> IgnoredColliders;

		// Token: 0x04000320 RID: 800
		public List<Collider2D> IgnoredColliders2D;

		// Token: 0x04000321 RID: 801
		public bool CollidersSameLayer = true;

		// Token: 0x04000322 RID: 802
		[Tooltip("If you add rigidbodies to each tail segment's collider, collision will work on everything but it will be less optimal, you don't have to add here rigidbodies but then you must have not kinematic rigidbodies on objects segments can collide")]
		public bool CollidersAddRigidbody = true;

		// Token: 0x04000323 RID: 803
		public float RigidbodyMass = 1f;

		// Token: 0x04000324 RID: 804
		[FPD_Layers]
		public int CollidersLayer;

		// Token: 0x04000325 RID: 805
		public bool UseSlitheryCurve;

		// Token: 0x04000326 RID: 806
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1.2f, 0.1f, 0.8f, 1f, 0.9f)]
		public AnimationCurve SlitheryCurve = AnimationCurve.EaseInOut(0f, 0.75f, 1f, 1f);

		// Token: 0x04000327 RID: 807
		private float lastSlithery = -1f;

		// Token: 0x04000328 RID: 808
		private Keyframe[] lastSlitheryCurvKeys;

		// Token: 0x04000329 RID: 809
		public bool UseCurlingCurve;

		// Token: 0x0400032A RID: 810
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.65f, 0.4f, 1f, 0.9f)]
		public AnimationCurve CurlingCurve = AnimationCurve.EaseInOut(0f, 0.7f, 1f, 0.3f);

		// Token: 0x0400032B RID: 811
		private float lastCurling = -1f;

		// Token: 0x0400032C RID: 812
		private Keyframe[] lastCurlingCurvKeys;

		// Token: 0x0400032D RID: 813
		public bool UseSpringCurve;

		// Token: 0x0400032E RID: 814
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.9f, 0.7f, 0.2f, 0.9f)]
		public AnimationCurve SpringCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f);

		// Token: 0x0400032F RID: 815
		private float lastSpringiness = -1f;

		// Token: 0x04000330 RID: 816
		private Keyframe[] lastSpringCurvKeys;

		// Token: 0x04000331 RID: 817
		public bool UseSlipperyCurve;

		// Token: 0x04000332 RID: 818
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.2f, 0.9f, 0.6f, 0.9f)]
		public AnimationCurve SlipperyCurve = AnimationCurve.EaseInOut(0f, 0.7f, 1f, 1f);

		// Token: 0x04000333 RID: 819
		private float lastSlippery = -1f;

		// Token: 0x04000334 RID: 820
		private Keyframe[] lastSlipperyCurvKeys;

		// Token: 0x04000335 RID: 821
		public bool UsePosSpeedCurve;

		// Token: 0x04000336 RID: 822
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.2f, 1f, 0.3f, 0.9f)]
		public AnimationCurve PosCurve = AnimationCurve.EaseInOut(0f, 0.7f, 1f, 1f);

		// Token: 0x04000337 RID: 823
		private float lastPosSpeeds = -1f;

		// Token: 0x04000338 RID: 824
		private Keyframe[] lastPosCurvKeys;

		// Token: 0x04000339 RID: 825
		public bool UseRotSpeedCurve;

		// Token: 0x0400033A RID: 826
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.7f, 0.7f, 0.7f, 0.9f)]
		public AnimationCurve RotCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0.9f);

		// Token: 0x0400033B RID: 827
		private float lastRotSpeeds = -1f;

		// Token: 0x0400033C RID: 828
		private Keyframe[] lastRotCurvKeys;

		// Token: 0x0400033D RID: 829
		[Tooltip("Spreading Tail Animator motion weight over bones")]
		public bool UsePartialBlend;

		// Token: 0x0400033E RID: 830
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.2f, 0.5f, 0.85f, 1f)]
		public AnimationCurve BlendCurve = AnimationCurve.EaseInOut(0f, 0.95f, 1f, 0.45f);

		// Token: 0x0400033F RID: 831
		private float lastTailAnimatorAmount = -1f;

		// Token: 0x04000340 RID: 832
		private Keyframe[] lastBlendCurvKeys;

		// Token: 0x04000341 RID: 833
		private TailAnimator2.TailSegment _ex_bone;

		// Token: 0x04000342 RID: 834
		public bool UseIK;

		// Token: 0x04000343 RID: 835
		private bool ikInitialized;

		// Token: 0x04000344 RID: 836
		[SerializeField]
		private FIK_CCDProcessor IK;

		// Token: 0x04000345 RID: 837
		[Tooltip("Target object to follow by IK")]
		public Transform IKTarget;

		// Token: 0x04000346 RID: 838
		public bool IKAutoWeights = true;

		// Token: 0x04000347 RID: 839
		[Range(0f, 1f)]
		public float IKBaseReactionWeight = 0.65f;

		// Token: 0x04000348 RID: 840
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.2f, 0.5f, 0.85f, 1f)]
		public AnimationCurve IKReactionWeightCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0.25f);

		// Token: 0x04000349 RID: 841
		public bool IKAutoAngleLimits = true;

		// Token: 0x0400034A RID: 842
		[FPD_Suffix(0f, 181f, FPD_SuffixAttribute.SuffixMode.FromMinToMaxRounded, "°", true, 0)]
		public float IKAutoAngleLimit = 40f;

		// Token: 0x0400034B RID: 843
		[Tooltip("If ik process should work referencing to previously computed CCDIK pose (can be more precise but need more adjusting in weights and angle limits)")]
		public bool IKContinousSolve;

		// Token: 0x0400034C RID: 844
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		[Tooltip("How much IK motion sohuld be used in tail animator motion -> 0: turned off")]
		public float IKBlend = 1f;

		// Token: 0x0400034D RID: 845
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		[Tooltip("If syncing with animator then applying motion of keyframe animation for IK")]
		public float IKAnimatorBlend = 0.5f;

		// Token: 0x0400034E RID: 846
		[Range(1f, 32f)]
		[Tooltip("How much iterations should do CCDIK algorithm in one frame")]
		public int IKReactionQuality = 2;

		// Token: 0x0400034F RID: 847
		[Range(0f, 1f)]
		[Tooltip("Smoothing reactions in CCD IK algorithm")]
		public float IKSmoothing;

		// Token: 0x04000350 RID: 848
		[Range(0f, 1.5f)]
		public float IKStretchToTarget;

		// Token: 0x04000351 RID: 849
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.9f, 0.4f, 0.5f, 1f)]
		public AnimationCurve IKStretchCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04000352 RID: 850
		public List<TailAnimator2.IKBoneSettings> IKLimitSettings;

		// Token: 0x04000353 RID: 851
		public bool IKSelectiveChain;

		// Token: 0x04000354 RID: 852
		private Vector3? _IKCustomPos;

		// Token: 0x04000355 RID: 853
		private List<TailAnimator2.TailSegment> _pp_reference;

		// Token: 0x04000356 RID: 854
		private TailAnimator2.TailSegment _pp_ref_rootParent;

		// Token: 0x04000357 RID: 855
		private TailAnimator2.TailSegment _pp_ref_lastChild;

		// Token: 0x04000358 RID: 856
		private bool _pp_initialized;

		// Token: 0x04000359 RID: 857
		[Tooltip("Rotation offset for tail (just first (root) bone is rotated)")]
		public Quaternion RotationOffset = Quaternion.identity;

		// Token: 0x0400035A RID: 858
		[Tooltip("Rotate each segment a bit to create curving effect")]
		public Quaternion Curving = Quaternion.identity;

		// Token: 0x0400035B RID: 859
		[Tooltip("Spread curving rotation offset weight over tail segments")]
		public bool UseCurvingCurve;

		// Token: 0x0400035C RID: 860
		[FPD_FixedCurveWindow(0f, -1f, 1f, 1f, 0.75f, 0.75f, 0.75f, 0.85f)]
		public AnimationCurve CurvCurve = AnimationCurve.EaseInOut(0f, 0.75f, 1f, 1f);

		// Token: 0x0400035D RID: 861
		private Quaternion lastCurving = Quaternion.identity;

		// Token: 0x0400035E RID: 862
		private Keyframe[] lastCurvingKeys;

		// Token: 0x0400035F RID: 863
		[Tooltip("Make tail longer or shorter")]
		public float LengthMultiplier = 1f;

		// Token: 0x04000360 RID: 864
		[Tooltip("Spread length multiplier weight over tail segments")]
		public bool UseLengthMulCurve;

		// Token: 0x04000361 RID: 865
		[FPD_FixedCurveWindow(0f, 0f, 1f, 3f, 0f, 1f, 1f, 1f)]
		public AnimationCurve LengthMulCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

		// Token: 0x04000362 RID: 866
		private float lastLengthMul = 1f;

		// Token: 0x04000363 RID: 867
		private Keyframe[] lastLengthKeys;

		// Token: 0x04000364 RID: 868
		[Tooltip("Spread gravity weight over tail segments")]
		public bool UseGravityCurve;

		// Token: 0x04000365 RID: 869
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.85f, 0.35f, 0.25f, 0.85f)]
		[Tooltip("Spread gravity weight over tail segments")]
		public AnimationCurve GravityCurve = AnimationCurve.EaseInOut(0f, 0.65f, 1f, 1f);

		// Token: 0x04000366 RID: 870
		[Tooltip("Simulate gravity weight for tail logics")]
		public Vector3 Gravity = Vector3.zero;

		// Token: 0x04000367 RID: 871
		private Vector3 lastGravity = Vector3.zero;

		// Token: 0x04000368 RID: 872
		private Keyframe[] lastGravityKeys;

		// Token: 0x04000369 RID: 873
		[Tooltip("Using auto waving option to give floating effect")]
		public bool UseWaving = true;

		// Token: 0x0400036A RID: 874
		[Tooltip("Adding some variation to waving animation")]
		public bool CosinusAdd;

		// Token: 0x0400036B RID: 875
		[Tooltip("If you want few tails to wave in the same way you can set this sinus period cycle value")]
		public float FixedCycle;

		// Token: 0x0400036C RID: 876
		[Tooltip("How frequent swings should be")]
		public float WavingSpeed = 3f;

		// Token: 0x0400036D RID: 877
		[Tooltip("How big swings should be")]
		public float WavingRange = 0.8f;

		// Token: 0x0400036E RID: 878
		[Tooltip("What rotation axis should be used in auto waving")]
		public Vector3 WavingAxis = new Vector3(1f, 1f, 1f);

		// Token: 0x0400036F RID: 879
		[CompilerGenerated]
		private Quaternion <WavingRotationOffset>k__BackingField;

		// Token: 0x04000370 RID: 880
		[Tooltip("Type of waving animation algorithm, it can be simple trigonometric wave or animation based on noises (advanced)")]
		public TailAnimator2.FEWavingType WavingType = TailAnimator2.FEWavingType.Advanced;

		// Token: 0x04000371 RID: 881
		[Tooltip("Offsetting perlin noise to generate different variation of tail rotations")]
		public float AlternateWave = 1f;

		// Token: 0x04000372 RID: 882
		private float _waving_waveTime;

		// Token: 0x04000373 RID: 883
		private float _waving_cosTime;

		// Token: 0x04000374 RID: 884
		private Vector3 _waving_sustain = Vector3.zero;

		// Token: 0x04000375 RID: 885
		public bool UseWind;

		// Token: 0x04000376 RID: 886
		[FPD_Suffix(0f, 2.5f, FPD_SuffixAttribute.SuffixMode.PercentageUnclamped, "%", true, 0)]
		public float WindEffectPower = 1f;

		// Token: 0x04000377 RID: 887
		[FPD_Suffix(0f, 2.5f, FPD_SuffixAttribute.SuffixMode.PercentageUnclamped, "%", true, 0)]
		public float WindTurbulencePower = 1f;

		// Token: 0x04000378 RID: 888
		[FPD_Suffix(0f, 1.5f, FPD_SuffixAttribute.SuffixMode.PercentageUnclamped, "%", true, 0)]
		public float WindWorldNoisePower = 0.5f;

		// Token: 0x04000379 RID: 889
		public Vector3 WindEffect = Vector3.zero;

		// Token: 0x0400037A RID: 890
		public List<TailAnimator2.TailSegment> TailSegments;

		// Token: 0x0400037B RID: 891
		[SerializeField]
		private TailAnimator2.TailSegment GhostParent;

		// Token: 0x0400037C RID: 892
		[SerializeField]
		private TailAnimator2.TailSegment GhostChild;

		// Token: 0x0400037D RID: 893
		private Vector3 _limiting_limitPosition = Vector3.zero;

		// Token: 0x0400037E RID: 894
		private Vector3 _limiting_influenceOffset = Vector3.zero;

		// Token: 0x0400037F RID: 895
		private float _limiting_stretchingHelperTooLong;

		// Token: 0x04000380 RID: 896
		private float _limiting_stretchingHelperTooShort;

		// Token: 0x04000381 RID: 897
		private Quaternion _limiting_angle_ToTargetRot;

		// Token: 0x04000382 RID: 898
		private Quaternion _limiting_angle_targetInLocal;

		// Token: 0x04000383 RID: 899
		private Quaternion _limiting_angle_newLocal;

		// Token: 0x04000384 RID: 900
		private Vector3 _tc_segmentGravityOffset = Vector3.zero;

		// Token: 0x04000385 RID: 901
		private Vector3 _tc_segmentGravityToParentDir = Vector3.zero;

		// Token: 0x04000386 RID: 902
		private Vector3 _tc_preGravOff = Vector3.zero;

		// Token: 0x04000387 RID: 903
		[Tooltip("If you want to use max distance fade option to smoothly disable tail animator when object is going far away from camera")]
		public bool UseMaxDistance;

		// Token: 0x04000388 RID: 904
		[Tooltip("(By default camera transform) Measuring distance from this object to define if object is too far and not need to update tail animator")]
		public Transform DistanceFrom;

		// Token: 0x04000389 RID: 905
		[HideInInspector]
		public Transform _distanceFrom_Auto;

		// Token: 0x0400038A RID: 906
		[Tooltip("Max distance to main camera / target object to smoothly turn off tail animator.")]
		public float MaximumDistance = 35f;

		// Token: 0x0400038B RID: 907
		[Tooltip("If object in range should be detected only when is nearer than 'MaxDistance' to avoid stuttery enabled - disable switching")]
		[Range(0f, 1f)]
		public float MaxOutDistanceFactor;

		// Token: 0x0400038C RID: 908
		[Tooltip("If distance should be measured not using Up (y) axis")]
		public bool DistanceWithoutY;

		// Token: 0x0400038D RID: 909
		[Tooltip("Offsetting point from which we want to measure distance to target")]
		public Vector3 DistanceMeasurePoint;

		// Token: 0x0400038E RID: 910
		[Tooltip("Disable fade duration in seconds")]
		[Range(0.25f, 2f)]
		public float FadeDuration = 0.75f;

		// Token: 0x0400038F RID: 911
		private bool maxDistanceExceed;

		// Token: 0x04000390 RID: 912
		private Transform finalDistanceFrom;

		// Token: 0x04000391 RID: 913
		private bool wasCameraSearch;

		// Token: 0x04000392 RID: 914
		private float distanceWeight = 1f;

		// Token: 0x04000393 RID: 915
		private int _tc_startI;

		// Token: 0x04000394 RID: 916
		private int _tc_startII = 1;

		// Token: 0x04000395 RID: 917
		[CompilerGenerated]
		private float <_TC_TailLength>k__BackingField;

		// Token: 0x04000396 RID: 918
		private TailAnimator2.TailSegment _tc_rootBone;

		// Token: 0x04000397 RID: 919
		private Quaternion _tc_lookRot = Quaternion.identity;

		// Token: 0x04000398 RID: 920
		private Quaternion _tc_targetParentRot = Quaternion.identity;

		// Token: 0x04000399 RID: 921
		private Quaternion _tc_startBoneRotOffset = Quaternion.identity;

		// Token: 0x0400039A RID: 922
		private float _tc_tangle = 1f;

		// Token: 0x0400039B RID: 923
		private float _sg_springVelo = 0.5f;

		// Token: 0x0400039C RID: 924
		private float _sg_curly = 0.5f;

		// Token: 0x0400039D RID: 925
		private Vector3 _sg_push;

		// Token: 0x0400039E RID: 926
		private Vector3 _sg_targetPos;

		// Token: 0x0400039F RID: 927
		private Vector3 _sg_targetChildWorldPosInParentFront;

		// Token: 0x040003A0 RID: 928
		private Vector3 _sg_dirToTargetParentFront;

		// Token: 0x040003A1 RID: 929
		private Quaternion _sg_orientation;

		// Token: 0x040003A2 RID: 930
		private float _sg_slitFactor = 0.5f;

		// Token: 0x040003A3 RID: 931
		private bool wasDisabled = true;

		// Token: 0x040003A4 RID: 932
		private float justDelta = 0.016f;

		// Token: 0x040003A5 RID: 933
		private float secPeriodDelta = 0.5f;

		// Token: 0x040003A6 RID: 934
		private float deltaForLerps = 0.016f;

		// Token: 0x040003A7 RID: 935
		private float rateDelta = 0.016f;

		// Token: 0x040003A8 RID: 936
		protected float collectedDelta;

		// Token: 0x040003A9 RID: 937
		protected int framesToSimulate = 1;

		// Token: 0x040003AA RID: 938
		protected int previousframesToSimulate = 1;

		// Token: 0x040003AB RID: 939
		private bool updateTailAnimator;

		// Token: 0x040003AC RID: 940
		private int startAfterTPoseCounter;

		// Token: 0x040003AD RID: 941
		private bool fixedUpdated;

		// Token: 0x040003AE RID: 942
		private bool lateFixedIsRunning;

		// Token: 0x040003AF RID: 943
		private bool fixedAllow = true;

		// Token: 0x040003B0 RID: 944
		[Tooltip("Making tail segment deflection influence back segments")]
		[Range(0f, 1f)]
		public float Deflection;

		// Token: 0x040003B1 RID: 945
		[FPD_Suffix(1f, 89f, FPD_SuffixAttribute.SuffixMode.FromMinToMaxRounded, "°", true, 0)]
		public float DeflectionStartAngle = 10f;

		// Token: 0x040003B2 RID: 946
		[Range(0f, 1f)]
		public float DeflectionSmooth;

		// Token: 0x040003B3 RID: 947
		[FPD_FixedCurveWindow(0f, 0f, 1f, 1f, 0.65f, 0.4f, 1f, 0.9f)]
		public AnimationCurve DeflectionFalloff = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040003B4 RID: 948
		[Tooltip("Deflection can be triggered every time tail is waving but you not always would want this feature be enabled (different behaviour of tail motion)")]
		public bool DeflectOnlyCollisions = true;

		// Token: 0x040003B5 RID: 949
		private List<TailAnimator2.TailSegment> _defl_source;

		// Token: 0x040003B6 RID: 950
		private float _defl_treshold = 0.01f;

		// Token: 0x040003B7 RID: 951
		private bool _forceDisable;

		// Token: 0x040003B8 RID: 952
		private float _forceDisableElapsed;

		// Token: 0x040003B9 RID: 953
		public TailAnimator2.ETailCategory _Editor_Category;

		// Token: 0x040003BA RID: 954
		public TailAnimator2.ETailFeaturesCategory _Editor_FeaturesCategory;

		// Token: 0x040003BB RID: 955
		public bool DrawGizmos = true;

		// Token: 0x040003BC RID: 956
		[Tooltip("First bone of tail motion chain")]
		public Transform StartBone;

		// Token: 0x040003BD RID: 957
		[Tooltip("Finish bone of tail motion chain")]
		public Transform EndBone;

		// Token: 0x040003BE RID: 958
		[Tooltip("Adjusting end point for end tail bone motion")]
		public Vector3 EndBoneJointOffset = Vector3.zero;

		// Token: 0x040003BF RID: 959
		public List<Transform> _TransformsGhostChain;

		// Token: 0x040003C0 RID: 960
		public int _GhostChainInitCount = -1;

		// Token: 0x040003C1 RID: 961
		protected bool initialized;

		// Token: 0x040003C2 RID: 962
		[Tooltip("Target FPS update rate for Tail Animator.\n\nIf you want Tail Animator to behave the same in low/high fps, set this value for example to 60.\nIt also can help optimizing if your game have more than 60 fps.")]
		public int UpdateRate;

		// Token: 0x040003C3 RID: 963
		[Tooltip("If your character Unity's Animator have update mode set to 'Animate Physics' you should enable it here too")]
		public TailAnimator2.EFixedMode AnimatePhysics;

		// Token: 0x040003C4 RID: 964
		[Tooltip("When using target fps rate you can interpolate coordinates for smoother effect when object with tail is moving a lot")]
		public bool InterpolateRate;

		// Token: 0x040003C5 RID: 965
		[Tooltip("Simulating tail motion at initiation to prevent jiggle start")]
		public bool Prewarm;

		// Token: 0x040003C6 RID: 966
		internal float OverrideWeight = 1f;

		// Token: 0x040003C7 RID: 967
		protected float conditionalWeight = 1f;

		// Token: 0x040003C8 RID: 968
		protected bool collisionInitialized;

		// Token: 0x040003C9 RID: 969
		protected bool forceRefreshCollidersData;

		// Token: 0x040003CA RID: 970
		private Vector3 previousWorldPosition;

		// Token: 0x040003CB RID: 971
		protected Transform rootTransform;

		// Token: 0x040003CC RID: 972
		protected bool preAutoCorrect;

		// Token: 0x040003CD RID: 973
		[Tooltip("Blending Slithery - smooth & soft tentacle like movement (value = 1)\nwith more stiff & springy motion (value = 0)\n\n0: Stiff somewhat like tree branch\n1: Soft like squid tentacle / Animal tail")]
		[Range(0f, 1.2f)]
		public float Slithery = 1f;

		// Token: 0x040003CE RID: 974
		[Tooltip("How curly motion should be applied to tail segments")]
		[Range(0f, 1f)]
		public float Curling = 0.5f;

		// Token: 0x040003CF RID: 975
		[Tooltip("Elastic spring effect making motion more 'meaty'")]
		[Range(0f, 1f)]
		public float Springiness;

		// Token: 0x040003D0 RID: 976
		[Tooltip("If you want to limit stretching/gumminess of position motion when object moves fast. Recommended adjust to go with it under 0.3 value.\nValue = 1: Unlimited stretching")]
		[Range(0f, 1f)]
		public float MaxStretching = 0.375f;

		// Token: 0x040003D1 RID: 977
		[Tooltip("Limiting max rotation angle for each tail segment")]
		[FPD_Suffix(1f, 181f, FPD_SuffixAttribute.SuffixMode.FromMinToMaxRounded, "°", true, 0)]
		public float AngleLimit = 181f;

		// Token: 0x040003D2 RID: 978
		[Tooltip("If you need specific axis to be limited.\nLeave unchanged to limit all axes.")]
		public Vector3 AngleLimitAxis = Vector3.zero;

		// Token: 0x040003D3 RID: 979
		[Tooltip("If you want limit axes symmetrically leave this parameter unchanged, if you want limit one direction of axis more than reversed, tweak this parameter")]
		public Vector2 LimitAxisRange = Vector2.zero;

		// Token: 0x040003D4 RID: 980
		[Tooltip("If limiting shouldn't be too rapidly performed")]
		[Range(0f, 1f)]
		public float LimitSmoothing = 0.5f;

		// Token: 0x040003D5 RID: 981
		[Tooltip("If your object moves very fast making tail influenced by speed too much then you can controll it with this parameter")]
		[FPD_Suffix(0f, 1.5f, FPD_SuffixAttribute.SuffixMode.PercentageUnclamped, "%", true, 0)]
		public float MotionInfluence = 1f;

		// Token: 0x040003D6 RID: 982
		[Tooltip("Additional Y influence controll useful when your character is jumping (works only when MotionInfluence value is other than 100%)")]
		[Range(0f, 1f)]
		public float MotionInfluenceInY = 1f;

		// Token: 0x040003D7 RID: 983
		[Tooltip("If first bone of chain should also be affected with whole chain")]
		public bool IncludeParent = true;

		// Token: 0x040003D8 RID: 984
		[Tooltip("By basic algorithm of Tail Animator different sized tails with different number of bones would animate with different bending thanks to this toggle every setup bends in very similar amount.\n\nShort tails will bend more and longer oner with bigger amount of bones less with this option enabled.")]
		[Range(0f, 1f)]
		public float UnifyBendiness;

		// Token: 0x040003D9 RID: 985
		[Tooltip("Reaction Speed is defining how fast tail segments will return to target position, it gives animation more underwater/floaty feeling if it's lower")]
		[Range(0f, 1f)]
		public float ReactionSpeed = 0.9f;

		// Token: 0x040003DA RID: 986
		[Tooltip("Sustain is similar to reaction speed in reverse, but providing sustain motion effect when increased")]
		[Range(0f, 1f)]
		public float Sustain;

		// Token: 0x040003DB RID: 987
		[Tooltip("Rotation speed is defining how fast tail segments will return to target rotation, it gives animation more lazy feeling if it's lower")]
		[Range(0f, 1f)]
		public float RotationRelevancy = 1f;

		// Token: 0x040003DC RID: 988
		[Tooltip("Smoothing motion values change over time style to be applied for 'Reaction Speed' and 'Rotation Relevancy' parameters")]
		public TailAnimator2.EAnimationStyle SmoothingStyle = TailAnimator2.EAnimationStyle.Accelerating;

		// Token: 0x040003DD RID: 989
		[Tooltip("Slowmo or speedup tail animation reaction")]
		public float TimeScale = 1f;

		// Token: 0x040003DE RID: 990
		[Tooltip("Delta time type to be used by algorithm")]
		public TailAnimator2.EFDeltaType DeltaType = TailAnimator2.EFDeltaType.SafeDelta;

		// Token: 0x040003DF RID: 991
		[Tooltip("Useful when you use other components to affect bones hierarchy and you want this component to follow other component's changes\n\nIt can be really useful when working with 'Spine Animator'")]
		public bool UpdateAsLast = true;

		// Token: 0x040003E0 RID: 992
		[Tooltip("Checking if keyframed animation has some empty keyframes which could cause unwanted twisting errors")]
		public bool DetectZeroKeyframes = true;

		// Token: 0x040003E1 RID: 993
		[Tooltip("Initializing Tail Animator after first frames of game to not initialize with model's T-Pose but after playing some other animation")]
		public bool StartAfterTPose = true;

		// Token: 0x040003E2 RID: 994
		[Tooltip("If you want Tail Animator to stop computing when choosed mesh is not visible in any camera view (editor's scene camera is detecting it too)")]
		public Renderer OptimizeWithMesh;

		// Token: 0x040003E3 RID: 995
		[Tooltip("If you want to check multiple meshes visibility on screen to define if you want to disable tail animator. (useful when using LOD for skinned mesh renderer)")]
		public Renderer[] OptimizeWithMeshes;

		// Token: 0x040003E4 RID: 996
		[Tooltip("Blend Source Animation (keyframed / unanimated) and Tail Animator")]
		[FPD_Suffix(0f, 1f, FPD_SuffixAttribute.SuffixMode.From0to100, "%", true, 0)]
		public float TailAnimatorAmount = 1f;

		// Token: 0x040003E5 RID: 997
		[Tooltip("Removing transforms hierachy structure to optimize Unity's calculations on Matrixes.\nIt can give very big boost in performance for long tails but it can't work with animated models!")]
		public bool DetachChildren;

		// Token: 0x040003E6 RID: 998
		[Tooltip("If tail movement should not move in depth you can use this parameter")]
		public int Axis2D;

		// Token: 0x040003E7 RID: 999
		[Tooltip("[Experimental: Works only with Slithery Blend set to >= 1] Making each segment go to target pose in front of parent segment creating new animation effect")]
		[Range(-1f, 1f)]
		public float Tangle;

		// Token: 0x040003E8 RID: 1000
		[Tooltip("Making tail animate also roll rotation like it was done in Tail Animator V1 ! Use Rotation Relevancy Parameter (set lower than 0.5) !")]
		public bool AnimateRoll;

		// Token: 0x040003E9 RID: 1001
		[Tooltip("Overriding keyframe animation with just Tail Animator option (keyframe animation treated as t-pose bones rotations)")]
		[Range(0f, 1f)]
		public float OverrideKeyframeAnimation;

		// Token: 0x040003EA RID: 1002
		private Transform _baseTransform;

		// Token: 0x020001B2 RID: 434
		[Serializable]
		public class TailSegment
		{
			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00062D1E File Offset: 0x00060F1E
			// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00062D26 File Offset: 0x00060F26
			public TailAnimator2.TailSegment ParentBone
			{
				[CompilerGenerated]
				get
				{
					return this.<ParentBone>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<ParentBone>k__BackingField = value;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00062D2F File Offset: 0x00060F2F
			// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00062D37 File Offset: 0x00060F37
			public TailAnimator2.TailSegment ChildBone
			{
				[CompilerGenerated]
				get
				{
					return this.<ChildBone>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<ChildBone>k__BackingField = value;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00062D40 File Offset: 0x00060F40
			// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00062D48 File Offset: 0x00060F48
			public Transform transform
			{
				[CompilerGenerated]
				get
				{
					return this.<transform>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<transform>k__BackingField = value;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00062D51 File Offset: 0x00060F51
			// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00062D59 File Offset: 0x00060F59
			public int Index
			{
				[CompilerGenerated]
				get
				{
					return this.<Index>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Index>k__BackingField = value;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00062D62 File Offset: 0x00060F62
			// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00062D6A File Offset: 0x00060F6A
			public float IndexOverlLength
			{
				[CompilerGenerated]
				get
				{
					return this.<IndexOverlLength>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IndexOverlLength>k__BackingField = value;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00062D73 File Offset: 0x00060F73
			// (set) Token: 0x06000F4C RID: 3916 RVA: 0x00062D7B File Offset: 0x00060F7B
			public float BoneLength
			{
				[CompilerGenerated]
				get
				{
					return this.<BoneLength>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<BoneLength>k__BackingField = value;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00062D84 File Offset: 0x00060F84
			// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00062D8C File Offset: 0x00060F8C
			public TailCollisionHelper CollisionHelper
			{
				[CompilerGenerated]
				get
				{
					return this.<CollisionHelper>k__BackingField;
				}
				[CompilerGenerated]
				internal set
				{
					this.<CollisionHelper>k__BackingField = value;
				}
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x00062D98 File Offset: 0x00060F98
			public TailSegment()
			{
				this.Index = -1;
				this.Curving = Quaternion.identity;
				this.Gravity = Vector3.zero;
				this.LengthMultiplier = 1f;
				this.Deflection = Vector3.zero;
				this.DeflectionFactor = 0f;
				this.DeflectionRelevancy = -1;
				this.deflectionSmoothVelo = 0f;
			}

			// Token: 0x06000F50 RID: 3920 RVA: 0x00062F10 File Offset: 0x00061110
			public TailSegment(Transform transform) : this()
			{
				if (transform == null)
				{
					return;
				}
				this.transform = transform;
				this.ProceduralPosition = transform.position;
				this.PreviousPosition = transform.position;
				this.PosRefRotation = transform.rotation;
				this.PreviousPosReferenceRotation = this.PosRefRotation;
				this.TrueTargetRotation = this.PosRefRotation;
				this.ReInitializeLocalPosRot(transform.localPosition, transform.localRotation);
				this.BoneLength = 0.1f;
			}

			// Token: 0x06000F51 RID: 3921 RVA: 0x00062F90 File Offset: 0x00061190
			public TailSegment(TailAnimator2.TailSegment copyFrom) : this(copyFrom.transform)
			{
				this.transform = copyFrom.transform;
				this.Index = copyFrom.Index;
				this.IndexOverlLength = copyFrom.IndexOverlLength;
				this.ProceduralPosition = copyFrom.ProceduralPosition;
				this.PreviousPosition = copyFrom.PreviousPosition;
				this.ProceduralPositionWeightBlended = copyFrom.ProceduralPosition;
				this.PosRefRotation = copyFrom.PosRefRotation;
				this.PreviousPosReferenceRotation = this.PosRefRotation;
				this.TrueTargetRotation = copyFrom.TrueTargetRotation;
				this.ReInitializeLocalPosRot(copyFrom.InitialLocalPosition, copyFrom.InitialLocalRotation);
			}

			// Token: 0x06000F52 RID: 3922 RVA: 0x00063027 File Offset: 0x00061227
			public void ReInitializeLocalPosRot(Vector3 initLocalPos, Quaternion initLocalRot)
			{
				this.InitialLocalPosition = initLocalPos;
				this.InitialLocalRotation = initLocalRot;
			}

			// Token: 0x06000F53 RID: 3923 RVA: 0x00063037 File Offset: 0x00061237
			public void SetIndex(int i, int tailSegments)
			{
				this.Index = i;
				if (i < 0)
				{
					this.IndexOverlLength = 0f;
					return;
				}
				this.IndexOverlLength = (float)i / (float)tailSegments;
			}

			// Token: 0x06000F54 RID: 3924 RVA: 0x0006305C File Offset: 0x0006125C
			public void SetParentRef(TailAnimator2.TailSegment parent)
			{
				this.ParentBone = parent;
				this.BoneLength = (this.ProceduralPosition - this.ParentBone.ProceduralPosition).magnitude;
			}

			// Token: 0x06000F55 RID: 3925 RVA: 0x00063094 File Offset: 0x00061294
			public void SetChildRef(TailAnimator2.TailSegment child)
			{
				this.ChildBone = child;
			}

			// Token: 0x06000F56 RID: 3926 RVA: 0x0006309D File Offset: 0x0006129D
			public float GetRadiusScaled()
			{
				return this.ColliderRadius * this.transform.lossyScale.x;
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000F57 RID: 3927 RVA: 0x000630B6 File Offset: 0x000612B6
			// (set) Token: 0x06000F58 RID: 3928 RVA: 0x000630BE File Offset: 0x000612BE
			public bool IsDetachable
			{
				[CompilerGenerated]
				get
				{
					return this.<IsDetachable>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<IsDetachable>k__BackingField = value;
				}
			}

			// Token: 0x06000F59 RID: 3929 RVA: 0x000630C7 File Offset: 0x000612C7
			public void AssignDetachedRootCoords(Transform root)
			{
				this.InitialLocalPositionInRoot = root.InverseTransformPoint(this.transform.position);
				this.InitialLocalRotationInRoot = root.rotation.QToLocal(this.transform.rotation);
				this.IsDetachable = true;
			}

			// Token: 0x06000F5A RID: 3930 RVA: 0x00063104 File Offset: 0x00061304
			internal Vector3 BlendMotionWeight(Vector3 newPosition)
			{
				return Vector3.LerpUnclamped(this.ParentBone.ProceduralPosition + this.ParentBone.LastKeyframeLocalRotation.TransformVector(this.ParentBone.transform.lossyScale, this.LastKeyframeLocalPosition), newPosition, this.BlendValue);
			}

			// Token: 0x06000F5B RID: 3931 RVA: 0x00063153 File Offset: 0x00061353
			internal void PreCalibrate()
			{
				this.transform.localPosition = this.InitialLocalPosition;
				this.transform.localRotation = this.InitialLocalRotation;
			}

			// Token: 0x06000F5C RID: 3932 RVA: 0x00063177 File Offset: 0x00061377
			internal void Validate()
			{
				if (this.BoneLength == 0f)
				{
					this.BoneLength = 0.001f;
				}
			}

			// Token: 0x06000F5D RID: 3933 RVA: 0x00063191 File Offset: 0x00061391
			public void RefreshKeyLocalPosition()
			{
				if (this.transform)
				{
					this.LastKeyframeLocalRotation = this.transform.localRotation;
					return;
				}
				this.LastKeyframeLocalRotation = this.InitialLocalRotation;
			}

			// Token: 0x06000F5E RID: 3934 RVA: 0x000631BE File Offset: 0x000613BE
			public void RefreshKeyLocalPositionAndRotation()
			{
				if (this.transform)
				{
					this.RefreshKeyLocalPositionAndRotation(this.transform.localPosition, this.transform.localRotation);
					return;
				}
				this.RefreshKeyLocalPositionAndRotation(this.InitialLocalPosition, this.InitialLocalRotation);
			}

			// Token: 0x06000F5F RID: 3935 RVA: 0x000631FC File Offset: 0x000613FC
			public void RefreshKeyLocalPositionAndRotation(Vector3 p, Quaternion r)
			{
				this.LastKeyframeLocalPosition = p;
				this.LastKeyframeLocalRotation = r;
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0006320C File Offset: 0x0006140C
			// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00063214 File Offset: 0x00061414
			public Vector3 LastFinalPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<LastFinalPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<LastFinalPosition>k__BackingField = value;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0006321D File Offset: 0x0006141D
			// (set) Token: 0x06000F63 RID: 3939 RVA: 0x00063225 File Offset: 0x00061425
			public Quaternion LastFinalRotation
			{
				[CompilerGenerated]
				get
				{
					return this.<LastFinalRotation>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<LastFinalRotation>k__BackingField = value;
				}
			}

			// Token: 0x06000F64 RID: 3940 RVA: 0x0006322E File Offset: 0x0006142E
			internal Vector3 ParentToFrontOffset()
			{
				return this.ParentBone.LastKeyframeLocalRotation.TransformVector(this.ParentBone.transform.lossyScale, this.LastKeyframeLocalPosition);
			}

			// Token: 0x06000F65 RID: 3941 RVA: 0x00063256 File Offset: 0x00061456
			public void RefreshFinalPos(Vector3 pos)
			{
				this.LastFinalPosition = pos;
			}

			// Token: 0x06000F66 RID: 3942 RVA: 0x0006325F File Offset: 0x0006145F
			public void RefreshFinalRot(Quaternion rot)
			{
				this.LastFinalRotation = rot;
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00063268 File Offset: 0x00061468
			// (set) Token: 0x06000F68 RID: 3944 RVA: 0x00063270 File Offset: 0x00061470
			public float DeflectionFactor
			{
				[CompilerGenerated]
				get
				{
					return this.<DeflectionFactor>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DeflectionFactor>k__BackingField = value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00063279 File Offset: 0x00061479
			// (set) Token: 0x06000F6A RID: 3946 RVA: 0x00063281 File Offset: 0x00061481
			public Vector3 Deflection
			{
				[CompilerGenerated]
				get
				{
					return this.<Deflection>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Deflection>k__BackingField = value;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0006328A File Offset: 0x0006148A
			// (set) Token: 0x06000F6C RID: 3948 RVA: 0x00063292 File Offset: 0x00061492
			public float DeflectionSmooth
			{
				[CompilerGenerated]
				get
				{
					return this.<DeflectionSmooth>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DeflectionSmooth>k__BackingField = value;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0006329B File Offset: 0x0006149B
			// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000632A3 File Offset: 0x000614A3
			public Vector3 DeflectionWorldPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<DeflectionWorldPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DeflectionWorldPosition>k__BackingField = value;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000F6F RID: 3951 RVA: 0x000632AC File Offset: 0x000614AC
			// (set) Token: 0x06000F70 RID: 3952 RVA: 0x000632B4 File Offset: 0x000614B4
			public int DeflectionRelevancy
			{
				[CompilerGenerated]
				get
				{
					return this.<DeflectionRelevancy>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DeflectionRelevancy>k__BackingField = value;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000632BD File Offset: 0x000614BD
			// (set) Token: 0x06000F72 RID: 3954 RVA: 0x000632C5 File Offset: 0x000614C5
			public FImp_ColliderData_Base LatestSelectiveCollision
			{
				[CompilerGenerated]
				get
				{
					return this.<LatestSelectiveCollision>k__BackingField;
				}
				[CompilerGenerated]
				internal set
				{
					this.<LatestSelectiveCollision>k__BackingField = value;
				}
			}

			// Token: 0x06000F73 RID: 3955 RVA: 0x000632D0 File Offset: 0x000614D0
			public bool CheckDeflectionState(float zeroWhenLower, float smoothTime, float delta)
			{
				Vector3 vector = this.LastKeyframeLocalPosition - this.ParentBone.transform.InverseTransformVector(this.ProceduralPosition - this.ParentBone.ProceduralPosition);
				this.DeflectionFactor = Vector3.Dot(this.LastKeyframeLocalPosition.normalized, vector.normalized);
				if (this.DeflectionFactor < zeroWhenLower)
				{
					if (smoothTime <= Mathf.Epsilon)
					{
						this.DeflectionSmooth = 0f;
					}
					else
					{
						this.DeflectionSmooth = Mathf.SmoothDamp(this.DeflectionSmooth, -Mathf.Epsilon, ref this.deflectionSmoothVelo, smoothTime / 1.5f, float.PositiveInfinity, delta);
					}
				}
				else if (smoothTime <= Mathf.Epsilon)
				{
					this.DeflectionSmooth = 1f;
				}
				else
				{
					this.DeflectionSmooth = Mathf.SmoothDamp(this.DeflectionSmooth, 1f, ref this.deflectionSmoothVelo, smoothTime, float.PositiveInfinity, delta);
				}
				if (this.DeflectionSmooth <= Mathf.Epsilon)
				{
					return true;
				}
				if (this.ChildBone.ChildBone != null)
				{
					this.DeflectionWorldPosition = this.ChildBone.ChildBone.ProceduralPosition;
				}
				else
				{
					this.DeflectionWorldPosition = this.ChildBone.ProceduralPosition;
				}
				return false;
			}

			// Token: 0x06000F74 RID: 3956 RVA: 0x000633F4 File Offset: 0x000615F4
			public bool DeflectionRelevant()
			{
				if (this.DeflectionRelevancy == -1)
				{
					this.DeflectionRelevancy = 3;
					return true;
				}
				this.DeflectionRelevancy = 3;
				return false;
			}

			// Token: 0x06000F75 RID: 3957 RVA: 0x00063410 File Offset: 0x00061610
			public bool? DeflectionRestoreState()
			{
				if (this.DeflectionRelevancy <= 0)
				{
					return new bool?(false);
				}
				int deflectionRelevancy = this.DeflectionRelevancy;
				this.DeflectionRelevancy = deflectionRelevancy - 1;
				if (this.DeflectionRelevancy == 0)
				{
					this.DeflectionRelevancy = -1;
					return null;
				}
				return new bool?(true);
			}

			// Token: 0x06000F76 RID: 3958 RVA: 0x0006345C File Offset: 0x0006165C
			internal void ParamsFrom(TailAnimator2.TailSegment other)
			{
				this.BlendValue = other.BlendValue;
				this.ColliderRadius = other.ColliderRadius;
				this.Gravity = other.Gravity;
				this.LengthMultiplier = other.LengthMultiplier;
				this.BoneLength = other.BoneLength;
				this.BoneLengthScaled = other.BoneLengthScaled;
				this.BoneDimensionsScaled = other.BoneDimensionsScaled;
				this.collisionContacts = other.collisionContacts;
				this.CollisionHelper = other.CollisionHelper;
				this.PositionSpeed = other.PositionSpeed;
				this.RotationSpeed = other.RotationSpeed;
				this.Springiness = other.Springiness;
				this.Slithery = other.Slithery;
				this.Curling = other.Curling;
				this.Slippery = other.Slippery;
			}

			// Token: 0x06000F77 RID: 3959 RVA: 0x00063520 File Offset: 0x00061720
			internal void ParamsFromAll(TailAnimator2.TailSegment other)
			{
				this.ParamsFrom(other);
				this.InitialLocalPosition = other.InitialLocalPosition;
				this.InitialLocalRotation = other.InitialLocalRotation;
				this.LastFinalPosition = other.LastFinalPosition;
				this.LastFinalRotation = other.LastFinalRotation;
				this.ProceduralPosition = other.ProceduralPosition;
				this.ProceduralPositionWeightBlended = other.ProceduralPositionWeightBlended;
				this.TrueTargetRotation = other.TrueTargetRotation;
				this.PosRefRotation = other.PosRefRotation;
				this.PreviousPosReferenceRotation = other.PreviousPosReferenceRotation;
				this.PreviousPosition = other.PreviousPosition;
				this.BoneLength = other.BoneLength;
				this.BoneDimensionsScaled = other.BoneDimensionsScaled;
				this.BoneLengthScaled = other.BoneLengthScaled;
				this.LocalOffset = other.LocalOffset;
				this.ColliderRadius = other.ColliderRadius;
				this.VelocityHelper = other.VelocityHelper;
				this.QVelocityHelper = other.QVelocityHelper;
				this.PreviousPush = other.PreviousPush;
			}

			// Token: 0x06000F78 RID: 3960 RVA: 0x0006360C File Offset: 0x0006180C
			internal void User_ReassignTransform(Transform t)
			{
				this.transform = t;
			}

			// Token: 0x06000F79 RID: 3961 RVA: 0x00063618 File Offset: 0x00061818
			public void Reset()
			{
				this.PreviousPush = Vector3.zero;
				this.VelocityHelper = Vector3.zero;
				this.QVelocityHelper = Quaternion.identity;
				if (this.transform)
				{
					this.ProceduralPosition = this.transform.position;
					this.PosRefRotation = this.transform.rotation;
					this.PreviousPosReferenceRotation = this.transform.rotation;
				}
				else if (this.ParentBone.transform)
				{
					this.ProceduralPosition = this.ParentBone.transform.position + this.ParentToFrontOffset();
				}
				this.PreviousPosition = this.ProceduralPosition;
				this.ProceduralPositionWeightBlended = this.ProceduralPosition;
			}

			// Token: 0x04000D65 RID: 3429
			[CompilerGenerated]
			private TailAnimator2.TailSegment <ParentBone>k__BackingField;

			// Token: 0x04000D66 RID: 3430
			[CompilerGenerated]
			private TailAnimator2.TailSegment <ChildBone>k__BackingField;

			// Token: 0x04000D67 RID: 3431
			[CompilerGenerated]
			private Transform <transform>k__BackingField;

			// Token: 0x04000D68 RID: 3432
			[CompilerGenerated]
			private int <Index>k__BackingField;

			// Token: 0x04000D69 RID: 3433
			[CompilerGenerated]
			private float <IndexOverlLength>k__BackingField;

			// Token: 0x04000D6A RID: 3434
			public Vector3 ProceduralPosition = Vector3.zero;

			// Token: 0x04000D6B RID: 3435
			public Vector3 ProceduralPositionWeightBlended = Vector3.zero;

			// Token: 0x04000D6C RID: 3436
			public Quaternion TrueTargetRotation = Quaternion.identity;

			// Token: 0x04000D6D RID: 3437
			public Quaternion PosRefRotation = Quaternion.identity;

			// Token: 0x04000D6E RID: 3438
			public Quaternion PreviousPosReferenceRotation = Quaternion.identity;

			// Token: 0x04000D6F RID: 3439
			public Vector3 PreviousPosition;

			// Token: 0x04000D70 RID: 3440
			public float BlendValue = 1f;

			// Token: 0x04000D71 RID: 3441
			[CompilerGenerated]
			private float <BoneLength>k__BackingField;

			// Token: 0x04000D72 RID: 3442
			public Vector3 BoneDimensionsScaled;

			// Token: 0x04000D73 RID: 3443
			public float BoneLengthScaled;

			// Token: 0x04000D74 RID: 3444
			public Vector3 InitialLocalPosition = Vector3.zero;

			// Token: 0x04000D75 RID: 3445
			public Vector3 InitialLocalPositionInRoot = Vector3.zero;

			// Token: 0x04000D76 RID: 3446
			public Quaternion InitialLocalRotationInRoot = Quaternion.identity;

			// Token: 0x04000D77 RID: 3447
			public Vector3 LocalOffset = Vector3.zero;

			// Token: 0x04000D78 RID: 3448
			public Quaternion InitialLocalRotation = Quaternion.identity;

			// Token: 0x04000D79 RID: 3449
			public float ColliderRadius = 1f;

			// Token: 0x04000D7A RID: 3450
			public bool CollisionContactFlag;

			// Token: 0x04000D7B RID: 3451
			public float CollisionContactRelevancy = -1f;

			// Token: 0x04000D7C RID: 3452
			public Collision collisionContacts;

			// Token: 0x04000D7D RID: 3453
			public Vector3 VelocityHelper = Vector3.zero;

			// Token: 0x04000D7E RID: 3454
			public Quaternion QVelocityHelper = Quaternion.identity;

			// Token: 0x04000D7F RID: 3455
			public Vector3 PreviousPush = Vector3.zero;

			// Token: 0x04000D80 RID: 3456
			public Quaternion Curving = Quaternion.identity;

			// Token: 0x04000D81 RID: 3457
			public Vector3 Gravity = Vector3.zero;

			// Token: 0x04000D82 RID: 3458
			public Vector3 GravityLookOffset = Vector3.zero;

			// Token: 0x04000D83 RID: 3459
			public float LengthMultiplier = 1f;

			// Token: 0x04000D84 RID: 3460
			public float PositionSpeed = 1f;

			// Token: 0x04000D85 RID: 3461
			public float RotationSpeed = 1f;

			// Token: 0x04000D86 RID: 3462
			public float Springiness;

			// Token: 0x04000D87 RID: 3463
			public float Slithery = 1f;

			// Token: 0x04000D88 RID: 3464
			public float Curling = 0.5f;

			// Token: 0x04000D89 RID: 3465
			public float Slippery = 1f;

			// Token: 0x04000D8A RID: 3466
			[CompilerGenerated]
			private TailCollisionHelper <CollisionHelper>k__BackingField;

			// Token: 0x04000D8B RID: 3467
			[CompilerGenerated]
			private bool <IsDetachable>k__BackingField;

			// Token: 0x04000D8C RID: 3468
			public Quaternion LastKeyframeLocalRotation;

			// Token: 0x04000D8D RID: 3469
			public Vector3 LastKeyframeLocalPosition;

			// Token: 0x04000D8E RID: 3470
			[CompilerGenerated]
			private Vector3 <LastFinalPosition>k__BackingField;

			// Token: 0x04000D8F RID: 3471
			[CompilerGenerated]
			private Quaternion <LastFinalRotation>k__BackingField;

			// Token: 0x04000D90 RID: 3472
			[CompilerGenerated]
			private float <DeflectionFactor>k__BackingField;

			// Token: 0x04000D91 RID: 3473
			[CompilerGenerated]
			private Vector3 <Deflection>k__BackingField;

			// Token: 0x04000D92 RID: 3474
			[CompilerGenerated]
			private float <DeflectionSmooth>k__BackingField;

			// Token: 0x04000D93 RID: 3475
			private float deflectionSmoothVelo;

			// Token: 0x04000D94 RID: 3476
			[CompilerGenerated]
			private Vector3 <DeflectionWorldPosition>k__BackingField;

			// Token: 0x04000D95 RID: 3477
			[CompilerGenerated]
			private int <DeflectionRelevancy>k__BackingField;

			// Token: 0x04000D96 RID: 3478
			[CompilerGenerated]
			private FImp_ColliderData_Base <LatestSelectiveCollision>k__BackingField;
		}

		// Token: 0x020001B3 RID: 435
		public enum ECollisionSpace
		{
			// Token: 0x04000D98 RID: 3480
			World_Slow,
			// Token: 0x04000D99 RID: 3481
			Selective_Fast
		}

		// Token: 0x020001B4 RID: 436
		public enum ECollisionMode
		{
			// Token: 0x04000D9B RID: 3483
			m_3DCollision,
			// Token: 0x04000D9C RID: 3484
			m_2DCollision
		}

		// Token: 0x020001B5 RID: 437
		[Serializable]
		public class IKBoneSettings
		{
			// Token: 0x06000F7A RID: 3962 RVA: 0x000636D3 File Offset: 0x000618D3
			public IKBoneSettings()
			{
			}

			// Token: 0x04000D9D RID: 3485
			[Range(0f, 181f)]
			public float AngleLimit = 45f;

			// Token: 0x04000D9E RID: 3486
			[Range(0f, 181f)]
			public float TwistAngleLimit = 5f;

			// Token: 0x04000D9F RID: 3487
			public bool UseInChain = true;
		}

		// Token: 0x020001B6 RID: 438
		public enum FEWavingType
		{
			// Token: 0x04000DA1 RID: 3489
			Simple,
			// Token: 0x04000DA2 RID: 3490
			Advanced
		}

		// Token: 0x020001B7 RID: 439
		public enum EFDeltaType
		{
			// Token: 0x04000DA4 RID: 3492
			DeltaTime,
			// Token: 0x04000DA5 RID: 3493
			SmoothDeltaTime,
			// Token: 0x04000DA6 RID: 3494
			UnscaledDeltaTime,
			// Token: 0x04000DA7 RID: 3495
			FixedDeltaTime,
			// Token: 0x04000DA8 RID: 3496
			SafeDelta
		}

		// Token: 0x020001B8 RID: 440
		public enum EAnimationStyle
		{
			// Token: 0x04000DAA RID: 3498
			Quick,
			// Token: 0x04000DAB RID: 3499
			Accelerating,
			// Token: 0x04000DAC RID: 3500
			Linear
		}

		// Token: 0x020001B9 RID: 441
		public enum ETailCategory
		{
			// Token: 0x04000DAE RID: 3502
			Setup,
			// Token: 0x04000DAF RID: 3503
			Tweak,
			// Token: 0x04000DB0 RID: 3504
			Features,
			// Token: 0x04000DB1 RID: 3505
			Shaping
		}

		// Token: 0x020001BA RID: 442
		public enum ETailFeaturesCategory
		{
			// Token: 0x04000DB3 RID: 3507
			Main,
			// Token: 0x04000DB4 RID: 3508
			Collisions,
			// Token: 0x04000DB5 RID: 3509
			IK,
			// Token: 0x04000DB6 RID: 3510
			Experimental
		}

		// Token: 0x020001BB RID: 443
		public enum EFixedMode
		{
			// Token: 0x04000DB8 RID: 3512
			None,
			// Token: 0x04000DB9 RID: 3513
			Basic,
			// Token: 0x04000DBA RID: 3514
			Late
		}

		// Token: 0x020001BC RID: 444
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0
		{
			// Token: 0x06000F7B RID: 3963 RVA: 0x000636F8 File Offset: 0x000618F8
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x06000F7C RID: 3964 RVA: 0x00063700 File Offset: 0x00061900
			internal bool <CheckForColliderDuplicatesAndNulls>b__0(Collider o)
			{
				return o == this.col;
			}

			// Token: 0x06000F7D RID: 3965 RVA: 0x0006370E File Offset: 0x0006190E
			internal bool <CheckForColliderDuplicatesAndNulls>b__1(Collider o)
			{
				return o == this.col;
			}

			// Token: 0x04000DBB RID: 3515
			public Collider col;
		}

		// Token: 0x020001BD RID: 445
		[CompilerGenerated]
		private sealed class <>c__DisplayClass45_0
		{
			// Token: 0x06000F7E RID: 3966 RVA: 0x0006371C File Offset: 0x0006191C
			public <>c__DisplayClass45_0()
			{
			}

			// Token: 0x06000F7F RID: 3967 RVA: 0x00063724 File Offset: 0x00061924
			internal bool <CheckForColliderDuplicatesAndNulls2D>b__0(Collider2D o)
			{
				return o == this.col;
			}

			// Token: 0x06000F80 RID: 3968 RVA: 0x00063732 File Offset: 0x00061932
			internal bool <CheckForColliderDuplicatesAndNulls2D>b__1(Collider2D o)
			{
				return o == this.col;
			}

			// Token: 0x04000DBC RID: 3516
			public Collider2D col;
		}

		// Token: 0x020001BE RID: 446
		[CompilerGenerated]
		private sealed class <LateFixed>d__277 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000F81 RID: 3969 RVA: 0x00063740 File Offset: 0x00061940
			[DebuggerHidden]
			public <LateFixed>d__277(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000F82 RID: 3970 RVA: 0x0006374F File Offset: 0x0006194F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x00063754 File Offset: 0x00061954
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TailAnimator2 tailAnimator = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					tailAnimator.PreCalibrateBones();
					tailAnimator.fixedAllow = true;
				}
				else
				{
					this.<>1__state = -1;
					fixedWait = new WaitForFixedUpdate();
					tailAnimator.lateFixedIsRunning = true;
				}
				this.<>2__current = fixedWait;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000637BB File Offset: 0x000619BB
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000F85 RID: 3973 RVA: 0x000637C3 File Offset: 0x000619C3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000637CA File Offset: 0x000619CA
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000DBD RID: 3517
			private int <>1__state;

			// Token: 0x04000DBE RID: 3518
			private object <>2__current;

			// Token: 0x04000DBF RID: 3519
			public TailAnimator2 <>4__this;

			// Token: 0x04000DC0 RID: 3520
			private WaitForFixedUpdate <fixedWait>5__2;
		}
	}
}
