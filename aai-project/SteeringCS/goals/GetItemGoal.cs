using System;
using SteeringCS.entity;
using SteeringCS.graphs;

namespace SteeringCS.goals
{
    internal class GetItemGoal : CompositeGoal<Ant>
    {
        private ExtraInfo _itemType;
        private Ant _ant;

        public GetItemGoal(Ant entity, ExtraInfo type) : base(entity)
        {
            _ant = entity;
            _itemType = type;
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override GoalState Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}