using System;
using System.Collections.Generic;


namespace WeeklyTask
{

    delegate void WriteTask(string text);
    delegate string ReadValues();
    public delegate void UpdateTaskNotifier(string message, int taskNumber);

    class WeeklyTaskService
    {
        private readonly List<WeeklyTask> tasks;
        private WriteTask writeTask;
        private ReadValues readValues;
        private UpdateTaskNotifier updateTaskNotifier;

        public WeeklyTaskService()
        {
            tasks = new();
        }

        public void SetOutputWriter(WriteTask outputWriter)
        {
            writeTask = outputWriter;
        }

        public void SetInputReader(ReadValues inputWriter)
        {
            readValues = inputWriter;
        }

        public void SetUpdateTaskNotifier(UpdateTaskNotifier taskNotifierUpdate)
        {
            updateTaskNotifier = taskNotifierUpdate;
        }

        public void AddTask()
        {
            writeTask("Add task in format: {},{},{},{}");
            var inputData = readValues();
            Add(inputData);
        }

        private void Add(string inputData)
        {
            if (!string.IsNullOrEmpty(inputData))
            {
                var task = ParseNewTask(inputData);
                AddTask((WeeklyTask)task);
            }
            else
            {
                writeTask("Invalid task format, try again.");
            }
        }

        private WeeklyTask ParseNewTask(string inputData)
        {
            var parts = inputData?.Split(',');

            if (parts == null || parts.Length < 1 || parts.Length > 4)
            {
                writeTask("Invalid task format, try again.");
                return null;
            }

            return CreateNewTask(parts);
        }

        private WeeklyTask CreateNewTask(string[] parts)
        {
            switch (parts.Length)
            {
                case 1:
                    return TaskWithName(parts);
                case 2:
                    return TaskWithDate(parts);
                case 3:
                    return TaskWithDateTime(parts);
                case 4:
                    return TaskWithDateTimeAndPriority(parts);
                default:
                    return null;
            }
        }
        private static WeeklyTask TaskWithName(string[] parts)
        {
            return new RegularTask(parts[0]);
        }

        private WeeklyTask TaskWithDate(string[] parts)
        {
            var date = DateTime.Parse(parts[1]);
            return new RegularTask(parts[0], date);
        }

        private WeeklyTask TaskWithDateTime(string[] parts)
        {
            var (date, time) = ParseDateTime(parts);
            return new RegularTask(parts[0], date, time);
        }

        private (DateTime date, TimeSpan time) ParseDateTime(string[] parts)
        {
            var date = DateTime.Parse(parts[1]);
            var time = TimeSpan.Parse(parts[2]);
            return (date, time);
        }

        private WeeklyTask TaskWithDateTimeAndPriority(string[] parts)
        {
            var (date, time) = ParseDateTime(parts);

            var priority = Enum.Parse<Priority>(parts[3]);
            return new PriorityTask(parts[0], date, time, priority);
        }

        public void AddTask(WeeklyTask task)
        {
            tasks.Add(task);
        }

        public void PrintTasks()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                PrintTask(i);
                writeTask(tasks[i].GetAlarm());
            }
        }

        private void PrintTask(int i)
        {
            writeTask(tasks[i].ToString(i));
        }

        public void UpdateTask()
        {
            writeTask("Input number to update:");
            var inputNumber = readValues();
            var taskNumber = int.Parse(inputNumber);

            writeTask("Input new task data:");
            var inputTaskData = readValues().Trim();
            Update(taskNumber, inputTaskData);
        }

        private void Update(int taskNumber, string inputTaskData)
        {
            if (!string.IsNullOrEmpty(inputTaskData))
            {
                var task = ParseNewTask(inputTaskData);
                UpdateTask(taskNumber, task);
            }
            else
            {
                writeTask($"Task #{taskNumber} hasn't been updated");
            }
        }

        private void UpdateTask(int taskNumber, WeeklyTask task)
        {
            tasks[taskNumber - 1] = task;
            updateTaskNotifier(" has been updated", taskNumber);
        }

        public void FilterByDate()
        {
            writeTask("Input data:");
            var inputDate = readValues();
            var date = DateTime.Parse(inputDate);

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i] is RegularTask regularTask && regularTask.GetDate() >= date)
                {
                    PrintTask(i);
                }
            }
        }

        public void FilterByPriority()
        {
            writeTask("Input priority:");
            var parsedPriority = Enum.Parse<Priority>(readValues());

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i] is PriorityTask priorityTask && priorityTask.GetPriority() == parsedPriority)
                {
                    PrintTask(i);
                }
            }
        }
    }
}
