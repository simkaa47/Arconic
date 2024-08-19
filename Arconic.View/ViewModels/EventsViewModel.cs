using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Arconic.Core.Abstractions.DataAccess;
using Arconic.Core.Infrastructure.DataContext.Data;
using Arconic.Core.Models.AccessControl;
using Arconic.Core.Models.Event;
using Arconic.Core.Services.Events;
using Arconic.Core.Services.Plc;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Arconic.View.ViewModels;

public partial class EventsViewModel:ObservableObject
{
    private readonly EventMainService _eventMainService;
    private readonly MainPlcService _plcService;
    private readonly DbContext _dbContext;

    private readonly ObservableCollection<EventHistoryItem> _events 
        = new ObservableCollection<EventHistoryItem>();

    public FlatTreeDataGridSource<EventHistoryItem> EventsSource { get; }
    

    public EventsViewModel(EventMainService eventMainService, 
        MainPlcService plcService, ArconicDbContext dbContext)
    {
        _eventMainService = eventMainService;
        _plcService = plcService;
        _dbContext = dbContext;
        EventsSource = new FlatTreeDataGridSource<EventHistoryItem>(_events)
        {
            Columns =
            {
                new TextColumn<EventHistoryItem, DateTime>
                    ("Дата", x => x.Date),
                new TextColumn<EventHistoryItem, string>
                    ("Код события", x => x.EventCode),
                new TextColumn<EventHistoryItem, string>
                    ("Сообщение", x => x.Message),
                new TextColumn<EventHistoryItem, string>
                    ("Пользователь", x => (x.User == null 
                        ? "Пользователь не авторизован" : x.User.FullName))
                    
            },
        };
        
        InitAsync();
    }
    
    [ObservableProperty]
    private DateTime _startHistoryPoint = DateTime.Today.AddDays(-2);
    
    [ObservableProperty]
    private DateTime _endHistoryPoint = DateTime.Now;
    
    [ObservableProperty]
    private List<PlcError>? _allErrors;
    [ObservableProperty]
    private List<ErrorGroup>? _groupErrors;
    

    private async void InitAsync()
    {
        _plcService.PlcScanCompleted += OnFirstPlcScan;
        _eventMainService.ErrorOccuredEvent += OnErrorOccure;
        _eventMainService.EventOccuredEvent += AddEventHistoryItem;
        await GetEventsAsync();
    }

    private async Task GetEventsAsync()
    {
        try
        {
            var events = await _dbContext.Set<EventHistoryItem>().Where(i => i.Date >= StartHistoryPoint
                                                                         && i.Date <= EndHistoryPoint)
                .Include(e=>e.User)
                .ToListAsync();
            _events.Clear();
            foreach (var e in events)
            {
                _events.Add(e);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
    [RelayCommand]
    private async Task DateSelectedAsync()
    {
        await GetEventsAsync();
    }

    private void GetErrors()
    {
        AllErrors = PlcError.Errors.Where(e => e.IsActive).ToList();
        if(GroupErrors is null)
        {
            GroupErrors = PlcError.Errors.GroupBy(e => e.Tag)
                .Select(g => new ErrorGroup(g.Key))
                .Append(new ErrorGroup("ВСЕ ОШИБКИ"))
                .ToList();
        }

        foreach (var group in GroupErrors)
        {
            group.Errors = AllErrors.Where(e => e.Tag == group.GroupName 
                                                || group.GroupName == "ВСЕ ОШИБКИ")
                .OrderByDescending(e=>e.LastExecTime)
                .ToList();
        }
        
        
    }

    private void OnFirstPlcScan()
    {
        GetErrors();
        _plcService.PlcScanCompleted -= OnFirstPlcScan;
    }

    private async void AddEventHistoryItem(EventHistoryItem item)
    {
        try
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                _events.Add(item);
            });
        }
        catch (Exception e)
        {
            //Ignore
        }
    }

    private void OnErrorOccure()
    {
        if (_plcService.FirstScanCompleted)
        {
            GetErrors();
        }
    }
}