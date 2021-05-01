using System;


namespace WeeklyTask
{
    class Program
    {
        public static WeeklyTaskService service = new();

        static void Main(string[] args)
        {
            service.SetInputReader(ReadValues);
            service.SetOutputWriter(WriteTask);
            service.SetUpdateTaskNotifier(TaskNotifier);

            RunLoop();
            Console.ReadKey();
        }

        private static string ReadValues()
        {
            return Console.ReadLine();
        }

        private static void TaskNotifier(string message, int taskNumber)
        {
            Console.WriteLine($"Task #{taskNumber} {message}");
        }

        private static void WriteTask(string text)
        {
            Console.WriteLine(text);
        }

        private static void RunLoop()
        {
            string input = null;

            while (input != "exit")
            {
                Menu();

                input = Console.ReadLine();

                if (int.TryParse(input, out var parsedInput))
                {
                    ServiceCommands(parsedInput);
                }
                else
                {
                    Console.WriteLine("Invalid command, try again.");
                }
            }
        }

        public static void Menu()
        {
            Console.WriteLine(
                @"Choose a command: 1. Add new task
                  2. Print all tasks
                  3. Update task
                  4. Filter task by date
                  5. Filter task by priority");
        }

        public static void ServiceCommands(int choice)
        {
            switch(choice)
            {
                case 1:
                    service.AddTask();
                break;
                case 2:
                    service.PrintTasks();
                break;
                case 3:
                    service.UpdateTask();
                break;
                case 4:
                    service.FilterByDate();
                break;
                case 5:
                    service.FilterByPriority();
                break;
            }


        }
            

   

        






    }
}
