
using AutoEmptyStandbyList.Shared;
using AutoEmptyStandbyList.Shared.Models;
using AutoEmptyStandbyList.Shared.Services;

var task = new TaskService();
task.RemoveTask();

string currentPath = AppDomain.CurrentDomain.BaseDirectory;
var parserPath = Path.Combine(currentPath, Constants.PreferencesFileName);

File.Delete(parserPath);
