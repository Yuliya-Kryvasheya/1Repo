

namespace WeeklyTask
{
    internal abstract class WeeklyTask
    {
        private readonly string _name;

        protected WeeklyTask(string name)
        {
            _name = name;
        }

        public virtual string ToString(int number)
        {
            return $"Task {number + 1}: {_name} ";
        }

        public abstract string GetAlarm();
    }
}
