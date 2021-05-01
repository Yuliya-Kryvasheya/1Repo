using System;


namespace WeeklyTask
{
    class PriorityTask : RegularTask

    {
        private readonly Priority _priority;

        public PriorityTask(string name, DateTime date, TimeSpan time, Priority priority) : base(name, date, time)
        {
            _priority = priority;
        }

        public Priority GetPriority()
        {
            return _priority;
        }

        public override string ToString(int index)
        {
            var output = base.ToString(index);

            if (_priority != Priority.Empty)
            {
                output += $"{_priority}";
            }

            return output;
        }

        public override string GetAlarm()
        {
            return base.GetAlarm() + $" Priority: {_priority}";
        }
    }
}
