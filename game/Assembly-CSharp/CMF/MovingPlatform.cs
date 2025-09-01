using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AD RID: 941
	public class MovingPlatform : MonoBehaviour
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x000BAF08 File Offset: 0x000B9108
		private void Start()
		{
			this.r = base.GetComponent<Rigidbody>();
			this.triggerArea = base.GetComponentInChildren<TriggerArea>();
			this.r.freezeRotation = true;
			this.r.useGravity = false;
			this.r.isKinematic = true;
			if (this.waypoints.Count <= 0)
			{
				UnityEngine.Debug.LogWarning("No waypoints have been assigned to 'MovingPlatform'!");
			}
			else
			{
				this.currentWaypoint = this.waypoints[this.currentWaypointIndex];
			}
			base.StartCoroutine(this.WaitRoutine());
			base.StartCoroutine(this.LateFixedUpdate());
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000BAF9C File Offset: 0x000B919C
		private IEnumerator LateFixedUpdate()
		{
			WaitForFixedUpdate _instruction = new WaitForFixedUpdate();
			for (;;)
			{
				yield return _instruction;
				this.MovePlatform();
			}
			yield break;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000BAFAC File Offset: 0x000B91AC
		private void MovePlatform()
		{
			if (this.waypoints.Count <= 0)
			{
				return;
			}
			if (this.isWaiting)
			{
				return;
			}
			Vector3 vector = this.currentWaypoint.position - base.transform.position;
			Vector3 vector2 = vector.normalized;
			vector2 *= this.movementSpeed * Time.deltaTime;
			if (vector2.magnitude >= vector.magnitude || vector2.magnitude == 0f)
			{
				this.r.transform.position = this.currentWaypoint.position;
				this.UpdateWaypoint();
			}
			else
			{
				this.r.transform.position += vector2;
			}
			if (this.triggerArea == null)
			{
				return;
			}
			for (int i = 0; i < this.triggerArea.rigidbodiesInTriggerArea.Count; i++)
			{
				this.triggerArea.rigidbodiesInTriggerArea[i].MovePosition(this.triggerArea.rigidbodiesInTriggerArea[i].position + vector2);
			}
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000BB0C4 File Offset: 0x000B92C4
		private void UpdateWaypoint()
		{
			if (this.reverseDirection)
			{
				this.currentWaypointIndex--;
			}
			else
			{
				this.currentWaypointIndex++;
			}
			if (this.currentWaypointIndex >= this.waypoints.Count)
			{
				this.currentWaypointIndex = 0;
			}
			if (this.currentWaypointIndex < 0)
			{
				this.currentWaypointIndex = this.waypoints.Count - 1;
			}
			this.currentWaypoint = this.waypoints[this.currentWaypointIndex];
			this.isWaiting = true;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000BB14B File Offset: 0x000B934B
		private IEnumerator WaitRoutine()
		{
			WaitForSeconds _waitInstruction = new WaitForSeconds(this.waitTime);
			for (;;)
			{
				if (this.isWaiting)
				{
					yield return _waitInstruction;
					this.isWaiting = false;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000BB15A File Offset: 0x000B935A
		public MovingPlatform()
		{
		}

		// Token: 0x04001F82 RID: 8066
		public float movementSpeed = 10f;

		// Token: 0x04001F83 RID: 8067
		public bool reverseDirection;

		// Token: 0x04001F84 RID: 8068
		public float waitTime = 1f;

		// Token: 0x04001F85 RID: 8069
		private bool isWaiting;

		// Token: 0x04001F86 RID: 8070
		private Rigidbody r;

		// Token: 0x04001F87 RID: 8071
		private TriggerArea triggerArea;

		// Token: 0x04001F88 RID: 8072
		public List<Transform> waypoints = new List<Transform>();

		// Token: 0x04001F89 RID: 8073
		private int currentWaypointIndex;

		// Token: 0x04001F8A RID: 8074
		private Transform currentWaypoint;

		// Token: 0x02000697 RID: 1687
		[CompilerGenerated]
		private sealed class <LateFixedUpdate>d__10 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002812 RID: 10258 RVA: 0x000D79D3 File Offset: 0x000D5BD3
			[DebuggerHidden]
			public <LateFixedUpdate>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002813 RID: 10259 RVA: 0x000D79E2 File Offset: 0x000D5BE2
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002814 RID: 10260 RVA: 0x000D79E4 File Offset: 0x000D5BE4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MovingPlatform movingPlatform = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					movingPlatform.MovePlatform();
				}
				else
				{
					this.<>1__state = -1;
					_instruction = new WaitForFixedUpdate();
				}
				this.<>2__current = _instruction;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06002815 RID: 10261 RVA: 0x000D7A3D File Offset: 0x000D5C3D
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002816 RID: 10262 RVA: 0x000D7A45 File Offset: 0x000D5C45
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x06002817 RID: 10263 RVA: 0x000D7A4C File Offset: 0x000D5C4C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C37 RID: 11319
			private int <>1__state;

			// Token: 0x04002C38 RID: 11320
			private object <>2__current;

			// Token: 0x04002C39 RID: 11321
			public MovingPlatform <>4__this;

			// Token: 0x04002C3A RID: 11322
			private WaitForFixedUpdate <_instruction>5__2;
		}

		// Token: 0x02000698 RID: 1688
		[CompilerGenerated]
		private sealed class <WaitRoutine>d__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002818 RID: 10264 RVA: 0x000D7A54 File Offset: 0x000D5C54
			[DebuggerHidden]
			public <WaitRoutine>d__13(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06002819 RID: 10265 RVA: 0x000D7A63 File Offset: 0x000D5C63
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600281A RID: 10266 RVA: 0x000D7A68 File Offset: 0x000D5C68
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MovingPlatform movingPlatform = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					_waitInstruction = new WaitForSeconds(movingPlatform.waitTime);
					break;
				case 1:
					this.<>1__state = -1;
					movingPlatform.isWaiting = false;
					goto IL_65;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (movingPlatform.isWaiting)
				{
					this.<>2__current = _waitInstruction;
					this.<>1__state = 1;
					return true;
				}
				IL_65:
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x0600281B RID: 10267 RVA: 0x000D7AF2 File Offset: 0x000D5CF2
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600281C RID: 10268 RVA: 0x000D7AFA File Offset: 0x000D5CFA
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003CA RID: 970
			// (get) Token: 0x0600281D RID: 10269 RVA: 0x000D7B01 File Offset: 0x000D5D01
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C3B RID: 11323
			private int <>1__state;

			// Token: 0x04002C3C RID: 11324
			private object <>2__current;

			// Token: 0x04002C3D RID: 11325
			public MovingPlatform <>4__this;

			// Token: 0x04002C3E RID: 11326
			private WaitForSeconds <_waitInstruction>5__2;
		}
	}
}
