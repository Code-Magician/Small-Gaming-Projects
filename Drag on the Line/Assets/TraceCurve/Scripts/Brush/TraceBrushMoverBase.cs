using System;
using UnityEngine;

namespace TraceCurve
{
	public abstract class TraceBrushMoverBase : MonoBehaviour
	{
		public Action OnMoveStarted;
		public Action OnMoveFinished;
		
		private bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			protected set { isBusy = value; }
		}

		protected Transform BrushTransform;
		protected Vector3 From;
		protected Vector3 To;
		protected bool MoveStarted;
		
		public virtual void Init(Transform t)
		{
			BrushTransform = t;
		}

		public virtual void StartMove(GeometryData geometryFrom, Vector3 positionFrom, GeometryData geometryTo, Vector3 positionTo)
		{
			isBusy = true;
			From = positionFrom;
			To = positionTo;
		}

		public abstract void Move();
	}
}