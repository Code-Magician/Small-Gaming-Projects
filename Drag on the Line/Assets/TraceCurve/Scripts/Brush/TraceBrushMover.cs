using UnityEngine;

namespace TraceCurve
{
    public class TraceBrushMover : TraceBrushMoverBase
    {
        public float Speed = 1f;

        private Vector3 direction;
        private float totalDistance;

        public override void StartMove(GeometryData geometryFrom, Vector3 positionFrom, GeometryData geometryTo, Vector3 positionTo)
        {
            base.StartMove(geometryFrom, positionFrom, geometryTo, positionTo);
            BrushTransform.position = From;
            direction = From - To;
            totalDistance = Vector3.Distance(From, To);
        }

        public override void Move()
        {
            if (!MoveStarted)
            {
                MoveStarted = true;
                if (OnMoveStarted != null)
                {
                    OnMoveStarted();
                }
            }

            BrushTransform.position -= direction * Time.deltaTime * Speed;
            var distance = Vector3.Distance(From, BrushTransform.position);
            if (distance >= totalDistance)
            {
                BrushTransform.position = To;
                FindObjectOfType<TracePainter>().CanDraw = true;
                MoveStarted = false;
                IsBusy = false;
                if (OnMoveFinished != null)
                {
                    OnMoveFinished();
                }
            }
        }
    }
}