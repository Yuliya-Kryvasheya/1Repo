using System;


namespace WeeklyTask
{
    internal class RegularTask : WeeklyTask
    {
        public DateTime? date { get; set; }
        public TimeSpan? time { get; set; }

        private readonly string name;

        public RegularTask(string name) : base(name)
        {
            this.name = name;
        }

        public RegularTask(string name, DateTime date) : base(name)
        {
            this.name = name;
            this.date = date;
        }

        public RegularTask(string name, DateTime date, TimeSpan time) : base(name)
        {
            this.name = name;
            this.date = date;
            this.time = time;
        }

        public override string ToString(int index)
        {
            var output = base.ToString(index);

            if (date != default)
            {
                output += $"{date.Value.ToShortDateString()} ";
            }

            if (time != default)
            {
                output += $"{time.Value.Hours}:{time.Value.Minutes} ";
            }

            return output;
        }

        public DateTime GetDate()
        {
            return date.Value;
        }

        public override string GetAlarm() => $"{(date - DateTime.Today).Value.Days} days left";
    }
}
