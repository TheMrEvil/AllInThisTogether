using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.AnimationTools
{
	// Token: 0x0200004C RID: 76
	public static class SkeletonRecognize
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00012044 File Offset: 0x00010244
		public static bool IsChildOf(Transform child, Transform parent)
		{
			Transform transform = child;
			while (transform != null)
			{
				if (transform == parent)
				{
					return true;
				}
				transform = transform.parent;
			}
			return false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00012074 File Offset: 0x00010274
		public static Transform GetBottomMostChildTransform(Transform parent)
		{
			Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>(true);
			int num = 0;
			Transform result = parent;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				int num2 = 0;
				while (transform.parent != parent && transform.parent != null)
				{
					num2++;
					transform = transform.parent;
				}
				if (num2 > num)
				{
					num = num2;
					result = componentsInChildren[i];
				}
			}
			return result;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000120E0 File Offset: 0x000102E0
		public static bool NameContains(string name, string[] names)
		{
			string text = name.ToLower();
			text = text.Replace("-", "");
			text = text.Replace(" ", "");
			text = text.Replace("_", "");
			text = text.Replace("|", "");
			text = text.Replace("@", "");
			for (int i = 0; i < names.Length; i++)
			{
				if (text.Contains(names[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00012168 File Offset: 0x00010368
		// Note: this type is marked as 'beforefieldinit'.
		static SkeletonRecognize()
		{
		}

		// Token: 0x0400022A RID: 554
		public static readonly string[] SpineNames = new string[]
		{
			"spine"
		};

		// Token: 0x0400022B RID: 555
		public static readonly string[] NeckNames = new string[]
		{
			"neck"
		};

		// Token: 0x0400022C RID: 556
		public static readonly string[] HeadNames = new string[]
		{
			"head"
		};

		// Token: 0x0400022D RID: 557
		public static readonly string[] RootNames = new string[]
		{
			"root",
			"origin",
			"skel"
		};

		// Token: 0x0400022E RID: 558
		public static readonly string[] PelvisNames = new string[]
		{
			"pelvis",
			"hips",
			"pelv"
		};

		// Token: 0x0400022F RID: 559
		public static readonly string[] ChestNames = new string[]
		{
			"chest",
			"upperspine"
		};

		// Token: 0x04000230 RID: 560
		public static readonly string[] ShouldersNames = new string[]
		{
			"shoulde",
			"collarbon",
			"clavicl"
		};

		// Token: 0x04000231 RID: 561
		public static readonly string[] UpperLegNames = new string[]
		{
			"upperleg",
			"thigh"
		};

		// Token: 0x04000232 RID: 562
		public static readonly string[] KneeNames = new string[]
		{
			"knee",
			"calf",
			"lowerleg"
		};

		// Token: 0x04000233 RID: 563
		public static readonly string[] ElbowNames = new string[]
		{
			"elbow",
			"lowerarm"
		};

		// Token: 0x020001A2 RID: 418
		public enum EWhatIsIt
		{
			// Token: 0x04000CDB RID: 3291
			Unknown,
			// Token: 0x04000CDC RID: 3292
			Humanoidal,
			// Token: 0x04000CDD RID: 3293
			Quadroped,
			// Token: 0x04000CDE RID: 3294
			Creature
		}

		// Token: 0x020001A3 RID: 419
		public class SkeletonInfo
		{
			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0006035D File Offset: 0x0005E55D
			public int SpineChainLength
			{
				get
				{
					return this.ProbablySpineChain.Count;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0006036A File Offset: 0x0005E56A
			public int LeftArms
			{
				get
				{
					return this.ProbablyLeftArms.Count;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00060377 File Offset: 0x0005E577
			public int LeftLegs
			{
				get
				{
					return this.ProbablyLeftLegs.Count;
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00060384 File Offset: 0x0005E584
			public int RightArms
			{
				get
				{
					return this.ProbablyRightArms.Count;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00060391 File Offset: 0x0005E591
			public int RightLegs
			{
				get
				{
					return this.ProbablyRightLegs.Count;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0006039E File Offset: 0x0005E59E
			public int Legs
			{
				get
				{
					return this.RightLegs + this.LeftLegs;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000603AD File Offset: 0x0005E5AD
			public int Arms
			{
				get
				{
					return this.LeftArms + this.RightArms;
				}
			}

			// Token: 0x06000EDB RID: 3803 RVA: 0x000603BC File Offset: 0x0005E5BC
			public SkeletonInfo(Transform t, List<Transform> checkOnly = null, Transform pelvisHelp = null)
			{
				this.AnimatorTransform = t;
				Transform[] array;
				if (checkOnly != null)
				{
					array = new Transform[checkOnly.Count];
					for (int i = 0; i < checkOnly.Count; i++)
					{
						array[i] = checkOnly[i];
					}
				}
				else
				{
					array = this.AnimatorTransform.GetComponentsInChildren<Transform>(true);
				}
				if (array.Length != 0)
				{
					Vector3 vector = this.AnimatorTransform.InverseTransformPoint(array[0].position);
					this.LocalSpaceHighest = vector;
					this.LocalSpaceMostRight = vector;
					this.LocalSpaceMostForward = vector;
					this.LocalSpaceMostBack = vector;
					this.LocalSpaceMostLeft = vector;
					this.LocalSpaceLowest = vector;
				}
				List<Transform> list = new List<Transform>();
				foreach (Transform transform in array)
				{
					if (!(transform.GetComponentInChildren<SkinnedMeshRenderer>() != null))
					{
						list.Add(transform);
					}
				}
				for (int k = 0; k < list.Count; k++)
				{
					Transform transform2 = list[k];
					if (!transform2.GetComponent<SkinnedMeshRenderer>())
					{
						Vector3 vector2 = this.AnimatorTransform.InverseTransformPoint(transform2.position);
						if (vector2.x > this.LocalSpaceMostRight.x)
						{
							this.LocalSpaceMostRight = vector2;
						}
						else if (vector2.x < this.LocalSpaceMostLeft.x)
						{
							this.LocalSpaceMostLeft = vector2;
						}
						if (vector2.z > this.LocalSpaceMostForward.z)
						{
							this.LocalSpaceMostForward = vector2;
						}
						else if (vector2.z < this.LocalSpaceMostBack.z)
						{
							this.LocalSpaceMostBack = vector2;
						}
						if (vector2.y > this.LocalSpaceHighest.y)
						{
							this.LocalSpaceHighest = vector2;
						}
						else if (vector2.y < this.LocalSpaceLowest.y)
						{
							this.LocalSpaceLowest = vector2;
						}
					}
				}
				this.LowestVsHighestLen = Mathf.Abs(this.LocalSpaceLowest.y - this.LocalSpaceHighest.y);
				this.MostLeftVsMostRightLen = Mathf.Abs(this.LocalSpaceMostLeft.x - this.LocalSpaceMostRight.x);
				this.MostForwVsMostBackLen = Mathf.Abs(this.LocalSpaceMostForward.z - this.LocalSpaceMostBack.z);
				this.AverageLen = (this.LowestVsHighestLen + this.MostLeftVsMostRightLen + this.MostForwVsMostBackLen) / 3f;
				float lowestVsHighestLen = this.LowestVsHighestLen;
				for (int l = 0; l < list.Count; l++)
				{
					Transform transform3 = list[l];
					if (SkeletonRecognize.NameContains(transform3.name, SkeletonRecognize.ShouldersNames))
					{
						Transform bottomMostChildTransform = SkeletonRecognize.GetBottomMostChildTransform(transform3);
						if (this.NotContainedYetByLimbs(bottomMostChildTransform))
						{
							this.TrReachingSides.Add(bottomMostChildTransform);
						}
					}
					else if (SkeletonRecognize.NameContains(transform3.name, SkeletonRecognize.ElbowNames))
					{
						Transform bottomMostChildTransform2 = SkeletonRecognize.GetBottomMostChildTransform(transform3);
						if (this.NotContainedYetByLimbs(bottomMostChildTransform2))
						{
							this.TrReachingSides.Add(bottomMostChildTransform2);
						}
					}
				}
				for (int m = 0; m < list.Count; m++)
				{
					Transform transform4 = list[m];
					if (SkeletonRecognize.NameContains(transform4.name, SkeletonRecognize.UpperLegNames))
					{
						Transform bottomMostChildTransform3 = SkeletonRecognize.GetBottomMostChildTransform(transform4);
						if (this.NotContainedYetByLimbs(bottomMostChildTransform3))
						{
							this.TrReachingGround.Add(bottomMostChildTransform3);
						}
					}
					else if (SkeletonRecognize.NameContains(transform4.name, SkeletonRecognize.KneeNames))
					{
						Transform bottomMostChildTransform4 = SkeletonRecognize.GetBottomMostChildTransform(transform4);
						if (this.NotContainedYetByLimbs(bottomMostChildTransform4))
						{
							this.TrReachingGround.Add(bottomMostChildTransform4);
						}
					}
				}
				bool flag = false;
				for (int n = 0; n < list.Count; n++)
				{
					Transform transform5 = list[n];
					if (SkeletonRecognize.NameContains(transform5.name, SkeletonRecognize.PelvisNames))
					{
						flag = true;
						this.ProbablyHips = transform5;
						break;
					}
				}
				bool flag2 = false;
				for (int num = 0; num < list.Count; num++)
				{
					Transform transform6 = list[num];
					if (SkeletonRecognize.NameContains(transform6.name, SkeletonRecognize.ChestNames))
					{
						flag2 = true;
						this.ProbablyChest = transform6;
						break;
					}
				}
				bool flag3 = false;
				for (int num2 = 0; num2 < list.Count; num2++)
				{
					Transform transform7 = list[num2];
					if (SkeletonRecognize.NameContains(transform7.name, SkeletonRecognize.HeadNames))
					{
						flag3 = true;
						this.ProbablyHead = transform7;
						break;
					}
				}
				if (this.ProbablyHead != null && this.ProbablyHips != null && !SkeletonRecognize.IsChildOf(this.ProbablyHead, this.ProbablyHips))
				{
					this.ProbablyHead = null;
				}
				for (int num3 = 0; num3 < list.Count; num3++)
				{
					Transform transform8 = list[num3];
					if (SkeletonRecognize.NameContains(transform8.name, SkeletonRecognize.RootNames))
					{
						this.ProbablyRootBone = transform8;
						break;
					}
				}
				if (list.Count > 2)
				{
					for (int num4 = 1; num4 < list.Count; num4++)
					{
						Transform transform9 = list[num4];
						if (transform9.childCount == 0)
						{
							this.TrEnds.Add(transform9);
							Vector3 vector3 = this.Loc(transform9);
							if (vector3.y < this.LocalSpaceLowest.y + this.LowestVsHighestLen * 0.1f)
							{
								if (this.NotContainedYetByLimbs(transform9))
								{
									this.TrReachingGround.Add(transform9);
								}
							}
							else if (vector3.y > this.LocalSpaceLowest.y + this.LowestVsHighestLen * 0.2f && (vector3.x < this.MostLeftVsMostRightLen * -0.1f || vector3.x > this.MostLeftVsMostRightLen * 0.1f) && this.NotContainedYetByLimbs(transform9))
							{
								this.TrReachingSides.Add(transform9);
							}
						}
					}
				}
				if (!flag2)
				{
					List<Transform> list2 = new List<Transform>();
					for (int num5 = 0; num5 < this.TrReachingSides.Count; num5++)
					{
						if (!list[num5].GetComponent<SkinnedMeshRenderer>())
						{
							Transform parent = this.TrReachingSides[num5].parent;
							while (parent != null)
							{
								if (parent.childCount > 2)
								{
									Vector3 vector4 = this.Loc(parent);
									if (vector4.x > -this.MostLeftVsMostRightLen * 0.03f && vector4.x < this.MostLeftVsMostRightLen * 0.03f)
									{
										list2.Add(parent);
										break;
									}
								}
								parent = parent.parent;
							}
						}
					}
					if (list2.Count == 1)
					{
						this.ProbablyChest = list2[0];
					}
					else if (list2.Count > 1 && list2[0] == list2[1])
					{
						this.ProbablyChest = list2[0];
					}
				}
				if (!flag)
				{
					List<Transform> list3 = new List<Transform>();
					for (int num6 = 0; num6 < this.TrReachingGround.Count; num6++)
					{
						Transform parent2 = this.TrReachingGround[num6].parent;
						while (parent2 != null)
						{
							if (parent2.childCount > 2)
							{
								Vector3 vector5 = this.Loc(parent2);
								if (vector5.y > this.LocalSpaceLowest.y + this.LowestVsHighestLen * 0.04f && vector5.x > -this.MostLeftVsMostRightLen * 0.02f && vector5.x < this.MostLeftVsMostRightLen * 0.02f)
								{
									list3.Add(parent2);
									break;
								}
							}
							parent2 = parent2.parent;
						}
					}
					if (list3.Count == 1)
					{
						this.ProbablyChest = list3[0];
					}
					else if (list3.Count > 1 && list3[0] == list3[1])
					{
						this.ProbablyHips = list3[0];
					}
				}
				if (this.ProbablyHips == null)
				{
					this.ProbablyHips = pelvisHelp;
				}
				if ((this.ProbablyChest == null || this.ProbablyChest == this.ProbablyHips || (this.ProbablyHips != null && !SkeletonRecognize.IsChildOf(this.ProbablyChest, this.ProbablyHips))) && this.ProbablyHips && this.ProbablyHead)
				{
					Transform parent3 = this.ProbablyHead.parent;
					bool flag4 = false;
					while (parent3.parent != null && parent3.parent != this.ProbablyHips)
					{
						if (parent3.childCount > 2)
						{
							for (int num7 = 0; num7 < this.TrReachingSides.Count; num7++)
							{
								if (SkeletonRecognize.IsChildOf(this.TrReachingSides[num7], parent3))
								{
									flag4 = true;
									break;
								}
							}
						}
						if (flag4)
						{
							break;
						}
						parent3 = parent3.parent;
					}
					if (flag4)
					{
						this.ProbablyChest = parent3;
					}
				}
				if (this.ProbablyHips == null)
				{
					this.ProbablyHips = pelvisHelp;
				}
				if (this.ProbablyChest && this.ProbablyHips)
				{
					if (this.MostForwVsMostBackLen > this.LowestVsHighestLen * 0.9f && this.Loc(this.ProbablyChest).z < this.Loc(this.ProbablyHips).z)
					{
						Transform probablyChest = this.ProbablyChest;
						this.ProbablyChest = this.ProbablyHips;
						this.ProbablyHips = probablyChest;
						Debug.Log("Hips - Chest - Reversed Detection Swap!");
					}
					if (!flag3)
					{
						Vector3 vector6 = Vector3.zero;
						for (int num8 = 0; num8 < this.ProbablyChest.childCount; num8++)
						{
							Transform child = this.ProbablyChest.GetChild(num8);
							Vector3 vector7;
							if (child.childCount > 0)
							{
								for (int num9 = 0; num9 < child.childCount; num9++)
								{
									Transform child2 = child.GetChild(num9);
									vector7 = this.Loc(child2);
									if (vector7.x > -this.MostLeftVsMostRightLen * 0.04f && vector7.x < this.MostLeftVsMostRightLen * 0.04f && this.Loc(child2).y > vector6.y)
									{
										vector6 = this.Loc(child2);
										this.ProbablyHead = child2;
									}
								}
							}
							vector7 = this.Loc(child);
							if (vector7.x > -this.MostLeftVsMostRightLen * 0.04f && vector7.x < this.MostLeftVsMostRightLen * 0.04f && vector7.y > vector6.y)
							{
								vector6 = this.Loc(child);
								this.ProbablyHead = child;
							}
						}
						if (this.ProbablyChest && this.ProbablyHead && this.ProbablyHips)
						{
							float num10 = Vector3.Distance(this.Loc(this.ProbablyChest), this.Loc(this.ProbablyHips));
							if ((this.ProbablyChest.childCount < 3 || num10 < this.AverageLen * 0.12f) && this.ProbablyHead.childCount > 1)
							{
								this.ProbablyChest = this.ProbablyHead;
								this.ProbablyHead = this.GetHighestChild(this.ProbablyHead, this.AnimatorTransform, this.MostLeftVsMostRightLen * 0.05f);
								if (this.ProbablyHead == this.ProbablyChest)
								{
									this.ProbablyHead = this.ProbablyChest.GetChild(0);
								}
							}
						}
					}
					if (this.ProbablyHead)
					{
						for (int num11 = this.TrReachingSides.Count - 1; num11 >= 0; num11--)
						{
							if (SkeletonRecognize.IsChildOf(this.TrReachingSides[num11], this.ProbablyHead))
							{
								this.TrReachingSides.RemoveAt(num11);
							}
						}
					}
					for (int num12 = this.TrReachingSides.Count - 1; num12 >= 0; num12--)
					{
						if (SkeletonRecognize.SkeletonInfo.GetDepth(this.TrReachingSides[num12], this.AnimatorTransform) < 5)
						{
							this.TrReachingSides.RemoveAt(num12);
						}
					}
					Transform transform10 = null;
					if (this.ProbablyHead)
					{
						this.ProbablySpineChain.Add(this.ProbablyHead);
						transform10 = this.ProbablyHead.parent;
					}
					while (transform10 != null && transform10 != this.ProbablyHips)
					{
						this.ProbablySpineChain.Add(transform10);
						transform10 = transform10.parent;
					}
					this.ProbablySpineChain.Reverse();
					for (int num13 = 0; num13 < Mathf.Min(4, this.ProbablySpineChain.Count); num13++)
					{
						this.ProbablySpineChainShort.Add(this.ProbablySpineChain[num13]);
					}
					List<Transform> list4 = new List<Transform>();
					for (int num14 = 0; num14 < this.TrReachingGround.Count; num14++)
					{
						Transform transform11 = this.TrReachingGround[num14];
						Vector3 vector8 = this.Loc(transform11);
						List<Transform> list5 = new List<Transform>();
						Transform transform12 = transform11;
						while (transform12 != null && transform12 != this.ProbablyHips && transform12 != this.ProbablyChest)
						{
							list5.Add(transform12);
							transform12 = transform12.parent;
						}
						if (list5.Count >= 3)
						{
							List<Transform> list6 = new List<Transform>();
							list6.Add(list5[list5.Count - 1]);
							list6.Add(list5[list5.Count - 2]);
							list6.Add(list5[list5.Count - 3]);
							list4.Add(transform11);
							if (vector8.x < this.MostLeftVsMostRightLen * 0.02f)
							{
								this.ProbablyLeftLegs.Add(list6);
								this.ProbablyLeftLegRoot.Add(transform12);
							}
							else
							{
								this.ProbablyRightLegs.Add(list6);
								this.ProbablyRightLegRoot.Add(transform12);
							}
						}
					}
					for (int num15 = 0; num15 < this.TrReachingSides.Count; num15++)
					{
						Transform transform13 = this.TrReachingSides[num15];
						Vector3 vector9 = this.Loc(transform13);
						List<Transform> list7 = new List<Transform>();
						Transform transform14 = transform13;
						while (transform14 != null && transform14 != this.ProbablyChest)
						{
							list7.Add(transform14);
							transform14 = transform14.parent;
						}
						if (list7.Count >= 4)
						{
							List<Transform> list8 = new List<Transform>();
							list8.Add(list7[list7.Count - 1]);
							list8.Add(list7[list7.Count - 2]);
							list8.Add(list7[list7.Count - 3]);
							list8.Add(list7[list7.Count - 4]);
							if (vector9.x < this.MostLeftVsMostRightLen * 0.02f)
							{
								this.ProbablyLeftArms.Add(list8);
							}
							else
							{
								this.ProbablyRightArms.Add(list8);
							}
						}
					}
					this.ClearDuplicates(this.ProbablyLeftArms, null);
					this.ClearDuplicates(this.ProbablyRightArms, null);
					this.ClearDuplicates(this.ProbablyLeftLegs, this.ProbablyLeftLegRoot);
					this.ClearDuplicates(this.ProbablyRightLegs, this.ProbablyRightLegRoot);
					if (this.Legs == 2 && this.Arms == 2)
					{
						this.WhatIsIt = SkeletonRecognize.EWhatIsIt.Humanoidal;
					}
					else if (this.Legs == 4 && this.Arms == 0)
					{
						this.WhatIsIt = SkeletonRecognize.EWhatIsIt.Quadroped;
					}
					else if (this.Legs > 0 || this.Arms > 0)
					{
						this.WhatIsIt = SkeletonRecognize.EWhatIsIt.Creature;
					}
					else
					{
						this.WhatIsIt = SkeletonRecognize.EWhatIsIt.Unknown;
					}
				}
				float y = Mathf.Lerp(this.LocalSpaceLowest.y, this.LocalSpaceHighest.y, 0.5f);
				Debug.DrawLine(t.TransformPoint(new Vector3(this.LocalSpaceMostLeft.x, this.LocalSpaceHighest.y, this.LocalSpaceMostForward.z)), t.TransformPoint(new Vector3(this.LocalSpaceMostLeft.x, this.LocalSpaceLowest.y, this.LocalSpaceMostForward.z)), Color.green, 12f);
				Debug.DrawLine(t.TransformPoint(new Vector3(this.LocalSpaceMostLeft.x, y, this.LocalSpaceMostForward.z)), t.TransformPoint(new Vector3(this.LocalSpaceMostRight.x, y, this.LocalSpaceMostForward.z)), Color.red, 12f);
				Debug.DrawLine(t.TransformPoint(new Vector3(this.LocalSpaceMostRight.x, y, this.LocalSpaceMostForward.z)), t.TransformPoint(new Vector3(this.LocalSpaceMostRight.x, y, this.LocalSpaceMostBack.z)), Color.blue, 12f);
			}

			// Token: 0x06000EDC RID: 3804 RVA: 0x000614E0 File Offset: 0x0005F6E0
			private bool NotContainedYetByAny(Transform t)
			{
				return !this.TrReachingSides.Contains(t) && !this.TrReachingGround.Contains(t) && !this.TrEnds.Contains(t) && t != this.ProbablyChest && t != this.ProbablyHips && t != this.ProbablyHead && t != this.ProbablyChest && t != this.ProbablyRootBone && t != this.AnimatorTransform;
			}

			// Token: 0x06000EDD RID: 3805 RVA: 0x0006156B File Offset: 0x0005F76B
			private bool NotContainedYetByLimbs(Transform t)
			{
				return !this.TrReachingSides.Contains(t) && !this.TrReachingGround.Contains(t);
			}

			// Token: 0x06000EDE RID: 3806 RVA: 0x0006158C File Offset: 0x0005F78C
			public Transform GetHighestChild(Transform t, Transform root, float inCenterRangeFactor)
			{
				if (t == null)
				{
					return null;
				}
				Transform result = t;
				Vector3 vector = root.InverseTransformPoint(t.position);
				foreach (Transform transform in t.GetComponentsInChildren<Transform>(true))
				{
					Vector3 vector2 = root.InverseTransformPoint(transform.position);
					if (vector2.x > -inCenterRangeFactor && vector2.x < inCenterRangeFactor && vector2.y > vector.y)
					{
						vector.y = vector2.y;
						result = transform;
					}
				}
				return result;
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x00061614 File Offset: 0x0005F814
			private void ClearDuplicates(List<List<Transform>> limbs, List<Transform> roots)
			{
				if (limbs.Count > 1)
				{
					for (int i = 0; i < limbs.Count; i++)
					{
						if (i >= limbs.Count)
						{
							return;
						}
						List<Transform> list = limbs[i];
						for (int j = limbs.Count - 1; j >= 0; j--)
						{
							if (j != i)
							{
								List<Transform> list2 = limbs[j];
								bool flag = false;
								for (int k = 0; k < list2.Count; k++)
								{
									if (list.Contains(list2[k]))
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									limbs.RemoveAt(j);
								}
							}
						}
					}
				}
			}

			// Token: 0x06000EE0 RID: 3808 RVA: 0x000616A4 File Offset: 0x0005F8A4
			private Vector3 Loc(Transform t)
			{
				return this.AnimatorTransform.InverseTransformPoint(t.position);
			}

			// Token: 0x06000EE1 RID: 3809 RVA: 0x000616B8 File Offset: 0x0005F8B8
			public string GetLog()
			{
				string text = "< " + this.AnimatorTransform.name + " >\n";
				text += "\nGenerate Guides:\n";
				string str = text;
				string str2 = "Highest: ";
				Vector3 vector = this.LocalSpaceHighest;
				text = str + str2 + vector.ToString() + "     ";
				string str3 = text;
				string str4 = "Lowest: ";
				vector = this.LocalSpaceLowest;
				text = str3 + str4 + vector.ToString() + "     ";
				string str5 = text;
				string str6 = "Left: ";
				vector = this.LocalSpaceMostLeft;
				text = str5 + str6 + vector.ToString() + "     ";
				string str7 = text;
				string str8 = "Right: ";
				vector = this.LocalSpaceMostRight;
				text = str7 + str8 + vector.ToString() + "     ";
				string str9 = text;
				string str10 = "Forward: ";
				vector = this.LocalSpaceMostForward;
				text = str9 + str10 + vector.ToString() + "     ";
				string str11 = text;
				string str12 = "Back: ";
				vector = this.LocalSpaceMostBack;
				text = str11 + str12 + vector.ToString() + "     ";
				text += "\n\nGenerated Helper Measurements: \n";
				text = text + "UpDown: " + this.LowestVsHighestLen.ToString() + "     ";
				text = text + "LeftRight: " + this.MostLeftVsMostRightLen.ToString() + "     ";
				text = text + "ForwBack: " + this.MostForwVsMostBackLen.ToString() + "     ";
				text = text + "Avr: " + this.AverageLen.ToString() + "     ";
				text += "\n\nDetected Propabilities: \n";
				string str13 = text;
				string str14 = "ProbablyHips: ";
				Transform probablyHips = this.ProbablyHips;
				text = str13 + str14 + ((probablyHips != null) ? probablyHips.ToString() : null) + "     ";
				string str15 = text;
				string str16 = "ProbablyChest: ";
				Transform probablyChest = this.ProbablyChest;
				text = str15 + str16 + ((probablyChest != null) ? probablyChest.ToString() : null) + "     ";
				string str17 = text;
				string str18 = "ProbablyHead: ";
				Transform probablyHead = this.ProbablyHead;
				text = str17 + str18 + ((probablyHead != null) ? probablyHead.ToString() : null) + "     ";
				text += "\n\nLimb End Detections: \n";
				text = text + "Reaching Ground: " + this.TrReachingGround.Count.ToString() + "     ";
				text = text + "Reaching Sides: " + this.TrReachingSides.Count.ToString() + "     ";
				text = string.Concat(new string[]
				{
					text,
					"Spine Chain Length: ",
					this.ProbablySpineChain.Count.ToString(),
					" (",
					this.ProbablySpineChainShort.Count.ToString(),
					")     "
				});
				text += "\n\nDetected Propabilities: \n";
				text = text + "Probably Left Arms: " + this.ProbablyLeftArms.Count.ToString() + "     ";
				text = text + "Probably Right Arms: " + this.ProbablyRightArms.Count.ToString() + "     ";
				text = text + "Probably Left Legs: " + this.ProbablyLeftLegs.Count.ToString() + "     ";
				text = text + "Probably Right Legs: " + this.ProbablyRightLegs.Count.ToString() + "     ";
				text += "\n\n\nTr Ends: \n";
				for (int i = 0; i < this.TrEnds.Count; i++)
				{
					if (!(this.TrEnds[i] == null))
					{
						text = text + this.TrEnds[i].name + "     ";
					}
				}
				text += "\n\nTr Reaching Ground: \n";
				for (int j = 0; j < this.TrReachingGround.Count; j++)
				{
					if (!(this.TrReachingGround[j] == null))
					{
						text = text + this.TrReachingGround[j].name + "     ";
					}
				}
				text += "\n\nTr Reaching Sides: \n";
				for (int k = 0; k < this.TrReachingSides.Count; k++)
				{
					if (!(this.TrReachingSides[k] == null))
					{
						text = text + this.TrReachingSides[k].name + "     ";
					}
				}
				if (this.ProbablyLeftArms.Count > 0)
				{
					text += "\n\nDebug Left Arms: \n";
					for (int l = 0; l < this.ProbablyLeftArms.Count; l++)
					{
						if (this.ProbablyLeftArms[l] != null)
						{
							text = text + "[" + l.ToString() + "] ";
							for (int m = 0; m < this.ProbablyLeftArms[l].Count; m++)
							{
								text = text + this.ProbablyLeftArms[l][m].name + "  ";
							}
							text += "\n";
						}
					}
				}
				if (this.ProbablySpineChainShort.Count > 0)
				{
					text += "\n\nDebug Spine Chain: \n";
					for (int n = 0; n < this.ProbablySpineChainShort.Count; n++)
					{
						if (!(this.ProbablySpineChainShort[n] == null))
						{
							text = text + this.ProbablySpineChainShort[n].name + "  ";
						}
					}
				}
				return text + "\n\n";
			}

			// Token: 0x06000EE2 RID: 3810 RVA: 0x00061C2C File Offset: 0x0005FE2C
			public static int GetDepth(Transform t, Transform skelRootBone)
			{
				int num = 0;
				if (t == skelRootBone)
				{
					return 0;
				}
				if (t == null)
				{
					return 0;
				}
				while (t != null && t.parent != skelRootBone)
				{
					t = t.parent;
					num++;
				}
				return num;
			}

			// Token: 0x04000CDF RID: 3295
			public Transform AnimatorTransform;

			// Token: 0x04000CE0 RID: 3296
			public float LowestVsHighestLen;

			// Token: 0x04000CE1 RID: 3297
			public float MostLeftVsMostRightLen;

			// Token: 0x04000CE2 RID: 3298
			public float MostForwVsMostBackLen;

			// Token: 0x04000CE3 RID: 3299
			public float AverageLen;

			// Token: 0x04000CE4 RID: 3300
			public Transform ProbablyRootBone;

			// Token: 0x04000CE5 RID: 3301
			public Transform ProbablyHips;

			// Token: 0x04000CE6 RID: 3302
			public Transform ProbablyChest;

			// Token: 0x04000CE7 RID: 3303
			public Transform ProbablyHead;

			// Token: 0x04000CE8 RID: 3304
			public List<Transform> TrReachingGround = new List<Transform>();

			// Token: 0x04000CE9 RID: 3305
			public List<Transform> TrReachingSides = new List<Transform>();

			// Token: 0x04000CEA RID: 3306
			public List<Transform> TrEnds = new List<Transform>();

			// Token: 0x04000CEB RID: 3307
			public List<Transform> ProbablySpineChain = new List<Transform>();

			// Token: 0x04000CEC RID: 3308
			public List<Transform> ProbablySpineChainShort = new List<Transform>();

			// Token: 0x04000CED RID: 3309
			public List<List<Transform>> ProbablyRightArms = new List<List<Transform>>();

			// Token: 0x04000CEE RID: 3310
			public List<List<Transform>> ProbablyLeftArms = new List<List<Transform>>();

			// Token: 0x04000CEF RID: 3311
			public List<List<Transform>> ProbablyLeftLegs = new List<List<Transform>>();

			// Token: 0x04000CF0 RID: 3312
			public List<Transform> ProbablyLeftLegRoot = new List<Transform>();

			// Token: 0x04000CF1 RID: 3313
			public List<List<Transform>> ProbablyRightLegs = new List<List<Transform>>();

			// Token: 0x04000CF2 RID: 3314
			public List<Transform> ProbablyRightLegRoot = new List<Transform>();

			// Token: 0x04000CF3 RID: 3315
			public Vector3 LocalSpaceHighest = Vector3.zero;

			// Token: 0x04000CF4 RID: 3316
			public Vector3 LocalSpaceMostRight = Vector3.zero;

			// Token: 0x04000CF5 RID: 3317
			public Vector3 LocalSpaceMostForward = Vector3.zero;

			// Token: 0x04000CF6 RID: 3318
			public Vector3 LocalSpaceMostBack = Vector3.zero;

			// Token: 0x04000CF7 RID: 3319
			public Vector3 LocalSpaceMostLeft = Vector3.zero;

			// Token: 0x04000CF8 RID: 3320
			public Vector3 LocalSpaceLowest = Vector3.zero;

			// Token: 0x04000CF9 RID: 3321
			public SkeletonRecognize.EWhatIsIt WhatIsIt;
		}
	}
}
