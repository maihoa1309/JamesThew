using Project3.Repository;
using Quartz;
using Quartz.Impl;

namespace Project3.Job
{
    // Tạo công việc của bạn
    public class SendMail : IJob
    {
        private readonly IUserRepository _userRepository;
        public SendMail(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _userRepository.CheckAndSendMails();
            Console.WriteLine("JOB RUNNING");
            return Task.CompletedTask;
        }
    }
}
