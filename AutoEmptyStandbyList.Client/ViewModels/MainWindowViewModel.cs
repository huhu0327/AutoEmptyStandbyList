using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AutoEmptyStandbyList.Shared.Models;
using AutoEmptyStandbyList.Shared.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace AutoEmptyStandbyList.Client.ViewModels;

partial class MainWindowViewModel : ViewModelBase
{
    private readonly ITaskService _taskService = default!;
    private readonly IPreferencesService _preferencesesService = default!;
    private Preferences? _preferences;

    [ObservableProperty] private int _timerMinute;

    public MainWindowViewModel()
    {
    }

    public MainWindowViewModel(ITaskService taskService, IPreferencesService preferencesesService)
    {
        _taskService = taskService;
        _preferencesesService = preferencesesService;

        ApplyPreferences(5);
    }

    [RelayCommand]
    private async Task Apply()
    {
        _preferences!.TimerMinute = TimerMinute;
        _preferencesesService.SavePreferences(_preferences);

        _taskService.SetTimer(_preferences!.TimerMinute);

        var box = MessageBoxManager
            .GetMessageBoxStandard("Apply", "적용 완료!", ButtonEnum.Ok, Icon.Success);

        await box.ShowAsync();
    }

    private void ApplyPreferences(int defaultTimerMinute)
    {
        _preferences = _preferencesesService.LoadPreferences();
        if (_preferences is null)
        {
            _preferences = new() { TimerMinute = defaultTimerMinute, TaskCreated = false };
            _preferencesesService.SavePreferences(_preferences);
        }

        TimerMinute = _preferences.TimerMinute;

        if (!_preferences.TaskCreated)
        {
            CreateTask();
        }
    }

    private void CreateTask()
    {
        var result = _taskService.CreateTask(_preferences!.TimerMinute);

        _preferences!.TaskCreated = result;
        _preferencesesService.SavePreferences(_preferences);
    }
}