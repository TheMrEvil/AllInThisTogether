using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000C1 RID: 193
	public class SolverManager : MonoBehaviour
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00039D1C File Offset: 0x00037F1C
		public void Disable()
		{
			Debug.Log("IK.Disable() is deprecated. Use enabled = false instead", base.transform);
			base.enabled = false;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00039D35 File Offset: 0x00037F35
		protected virtual void InitiateSolver()
		{
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00039D37 File Offset: 0x00037F37
		protected virtual void UpdateSolver()
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00039D39 File Offset: 0x00037F39
		protected virtual void FixTransforms()
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00039D3B File Offset: 0x00037F3B
		private void OnDisable()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.Initiate();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00039D4B File Offset: 0x00037F4B
		private void Start()
		{
			this.Initiate();
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00039D53 File Offset: 0x00037F53
		private bool animatePhysics
		{
			get
			{
				if (this.animator != null)
				{
					return this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics;
				}
				return this.legacy != null && this.legacy.animatePhysics;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00039D8D File Offset: 0x00037F8D
		private void Initiate()
		{
			if (this.componentInitiated)
			{
				return;
			}
			this.FindAnimatorRecursive(base.transform, true);
			this.InitiateSolver();
			this.componentInitiated = true;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00039DB2 File Offset: 0x00037FB2
		private void Update()
		{
			if (this.skipSolverUpdate)
			{
				return;
			}
			if (this.animatePhysics)
			{
				return;
			}
			if (this.fixTransforms)
			{
				this.FixTransforms();
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00039DD4 File Offset: 0x00037FD4
		private void FindAnimatorRecursive(Transform t, bool findInChildren)
		{
			if (this.isAnimated)
			{
				return;
			}
			this.animator = t.GetComponent<Animator>();
			this.legacy = t.GetComponent<Animation>();
			if (this.isAnimated)
			{
				return;
			}
			if (this.animator == null && findInChildren)
			{
				this.animator = t.GetComponentInChildren<Animator>();
			}
			if (this.legacy == null && findInChildren)
			{
				this.legacy = t.GetComponentInChildren<Animation>();
			}
			if (!this.isAnimated && t.parent != null)
			{
				this.FindAnimatorRecursive(t.parent, false);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00039E66 File Offset: 0x00038066
		private bool isAnimated
		{
			get
			{
				return this.animator != null || this.legacy != null;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00039E84 File Offset: 0x00038084
		private void FixedUpdate()
		{
			if (this.skipSolverUpdate)
			{
				this.skipSolverUpdate = false;
			}
			this.updateFrame = true;
			if (this.animatePhysics && this.fixTransforms)
			{
				this.FixTransforms();
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00039EB2 File Offset: 0x000380B2
		private void LateUpdate()
		{
			if (this.skipSolverUpdate)
			{
				return;
			}
			if (!this.animatePhysics)
			{
				this.updateFrame = true;
			}
			if (!this.updateFrame)
			{
				return;
			}
			this.updateFrame = false;
			this.UpdateSolver();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00039EE2 File Offset: 0x000380E2
		public void UpdateSolverExternal()
		{
			if (!base.enabled)
			{
				return;
			}
			this.skipSolverUpdate = true;
			this.UpdateSolver();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00039EFA File Offset: 0x000380FA
		public SolverManager()
		{
		}

		// Token: 0x040006C9 RID: 1737
		[Tooltip("If true, will fix all the Transforms used by the solver to their initial state in each Update. This prevents potential problems with unanimated bones and animator culling with a small cost of performance. Not recommended for CCD and FABRIK solvers.")]
		public bool fixTransforms = true;

		// Token: 0x040006CA RID: 1738
		private Animator animator;

		// Token: 0x040006CB RID: 1739
		private Animation legacy;

		// Token: 0x040006CC RID: 1740
		private bool updateFrame;

		// Token: 0x040006CD RID: 1741
		private bool componentInitiated;

		// Token: 0x040006CE RID: 1742
		private bool skipSolverUpdate;
	}
}
