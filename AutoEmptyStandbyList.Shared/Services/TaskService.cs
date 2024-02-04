namespace AutoEmptyStandbyList.Shared.Services;

using Scheduler = Microsoft.Win32.TaskScheduler;

public interface ITaskService
{
    bool CreateTask(int timerMinute);
    void RemoveTask();
    void SetTimer(int timeMinute);
}

public class TaskService : ITaskService
{
    public bool CreateTask(int timerMinute)
    {
        try
        {
            using Scheduler.TaskService service = new();

            using var task = service.NewTask();
            
            task.RegistrationInfo.Description = "AutoEmptyStandbyList Task";

            task.Principal.UserId = string.Concat(Environment.UserDomainName, @"\", Environment.UserName);
            task.Principal.LogonType = Scheduler.TaskLogonType.InteractiveToken;
            task.Principal.RunLevel = Scheduler.TaskRunLevel.Highest;

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var executePath = Path.Combine(currentPath, Constants.ExecuteProgramName);

            using var trigger = new Scheduler.TimeTrigger();
            trigger.Repetition.Interval = TimeSpan.FromMinutes(timerMinute);
            task.Triggers.Add(trigger);

            task.Actions.Add(new Scheduler.ExecAction(executePath));

            task.Settings.Compatibility = Scheduler.TaskCompatibility.V2_3;
            task.Settings.DisallowStartIfOnBatteries = false;
            task.Settings.Hidden = true;

            service.RootFolder.RegisterTaskDefinition(Constants.ProgramName, task);
        }
        catch
        {
            //execption
            return false;
        }

        return true;
    }

    public void RemoveTask()
    {
        using var service = new Scheduler.TaskService();
        service.RootFolder.DeleteTask(Constants.ProgramName);
    }

    public void SetTimer(int timeMinute)
    {
        UpdateTask(timeMinute);
    }

    private void UpdateTask(int timeMinute)
    {
        using var service = new Scheduler.TaskService();
        using var task = service.GetTask(Constants.ProgramName);

        if (task is null) return;

        task.Definition.Triggers[0].Repetition.Interval = TimeSpan.FromMinutes(timeMinute);
        task.RegisterChanges();
    }
}