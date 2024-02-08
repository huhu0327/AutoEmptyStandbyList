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


            using var trigger = new Scheduler.TimeTrigger();
            trigger.Repetition.Interval = TimeSpan.FromMinutes(timerMinute);
            
            task.Triggers.Add(trigger);
            
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            var exePath = Path.Combine(currentPath, Constants.ExeProgramName);
            var scriptPath = Path.Combine(currentPath, Constants.ScriptProgramName);

            using var action = new Scheduler.ExecAction();
            action.Path = "wscript.exe";
            action.Arguments = $"\"{scriptPath}\" \"{exePath}\"";
            
            task.Actions.Add(action);

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

    private void UpdateTask(int timerMinute)
    {
        using var service = new Scheduler.TaskService();
        using var task = service.GetTask(Constants.ProgramName);

        if (task is null) return;

        task.Definition.Triggers[0].Repetition.Interval = TimeSpan.FromMinutes(timerMinute);
        task.RegisterChanges();
    }
}