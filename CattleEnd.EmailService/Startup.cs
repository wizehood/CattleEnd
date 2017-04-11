using CattleEnd.DataAccessLayer.Repositories;
using System;
using System.Threading;

namespace CattleEnd.EmailService
{
    class Startup
    {
        private Thread workingThread;
        private System.Timers.Timer myTimer = new System.Timers.Timer();

        public void OnStart()
        {
            //Trigger DoWork method on start
            Start();
            workingThread = new System.Threading.Thread(PrepareTask);
            workingThread.Start();
        }

        private void PrepareTask()
        {
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimer_Elapsed);

            //Check time every 1 minute
            myTimer.Interval = 60000;
            myTimer.Start();
            Thread.Sleep(Timeout.Infinite);
        }

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Start();
        }

        public void OnStop()
        {
            Console.WriteLine("Service stoped.");
        }

        static void Start()
        {
            Console.WriteLine("Method triggered.");
            DoWork();
        }

        private static void DoWork()
        {
            try
            {
                var time = DateTime.Now.TimeOfDay;
                if (time.Hours == 15 && time.Minutes == 0)
                {
                    var warriorRepo = new WarriorRepository();
                    var scheduleRepo = new ScheduleRepository();

                    var warriorName = scheduleRepo.GetResponsibleWarriorName();
                    var mails = warriorRepo.GetEmails();
                    if (mails.Count > 0)
                    {
                        EmailService.SendMail(warriorName, mails);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred (ex:'{ex.Message}')");
                return;
            }
        }
    }
}
