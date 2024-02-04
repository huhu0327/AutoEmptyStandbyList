using AutoEmptyStandbyList.Shared.Models;
using Salaros.Configuration;

namespace AutoEmptyStandbyList.Shared.Services;

public interface IPreferencesService
{
    void SavePreferences(Preferences preferences);
    Preferences? LoadPreferences();
}

public class PreferencesesService : IPreferencesService
{
    private string PreferencesSection => Constants.PreferencesSection;
    private string PreferencesTimerKey => Constants.PreferencesTimerKey;
    private string PreferencesCreatedKey => Constants.PreferencesCreatedKey;
    private readonly ConfigParser _parser;

    public PreferencesesService()
    {
        string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        var parserPath = Path.Combine(currentPath, Constants.PreferencesFileName);
        _parser = new(parserPath);
    }

    public void SavePreferences(Preferences preferences)
    {
        _parser.SetValue(PreferencesSection, PreferencesTimerKey, preferences.TimerMinute);
        _parser.SetValue(PreferencesSection, PreferencesCreatedKey, preferences.TaskCreated);
        _parser.Save();
    }

    public Preferences? LoadPreferences()
    {
        if (_parser.Sections.Count == 0) return null;

        var value = _parser.GetValue(PreferencesSection, PreferencesTimerKey);
        var timerMinute = Convert.ToInt32(value);

        value = _parser.GetValue(PreferencesSection, PreferencesCreatedKey);
        var createdTask = Convert.ToBoolean(value);

        return new() { TimerMinute = timerMinute, TaskCreated = createdTask};
    }
}