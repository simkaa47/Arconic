﻿using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using Timer = System.Timers.Timer;

namespace Arconic.Core.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly Timer _timer;

    public MainViewModel(PlcViewModel plcViewModel, 
        AccessViewModel accessViewModel, MainTrendsViewModel mainTrendsViewModel)
    {
        PlcViewModel = plcViewModel;
        AccessViewModel = accessViewModel;
        MainTrendsViewModel = mainTrendsViewModel;
        _timer = new Timer();
        _timer.Interval = 1000;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
        
    }

    private  void OnTimerElapsed(object? obj, ElapsedEventArgs args)
    {
        DateTime = DateTime.Now;
    }

    public PlcViewModel PlcViewModel { get; }
    public AccessViewModel AccessViewModel { get; }
    public MainTrendsViewModel MainTrendsViewModel { get; }

    [ObservableProperty]
    private DateTime _dateTime;
}