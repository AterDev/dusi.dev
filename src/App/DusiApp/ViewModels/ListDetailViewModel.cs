﻿using Share.Models;
using System.Net.Http.Json;
using Share.Models.ThirdNewsDtos;

namespace DusiApp.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<ThirdNewsItemDto> items;

    public ListDetailViewModel()
    {

    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;

        try
        {
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task LoadMore()
    {

    }

    public async Task LoadDataAsync()
    {

        var data = await GetNewsAsync();
        Items = new ObservableCollection<ThirdNewsItemDto>(data);
    }

    /// <summary>
    /// 获取资讯内容
    /// </summary>
    /// <returns></returns>
    public async Task<List<ThirdNewsItemDto>> GetNewsAsync()
    {
        var news = ApiService.AdminClient.ThirdNews;

        var data = new ThirdNewsFilterDto()
        {
            PageIndex = 1,
            PageSize = 100,
        };

        var res = await news.FilterAsync(data);
        if (res != null)
        {
            return res.Data;
        }
        return new List<ThirdNewsItemDto>();
    }

    [RelayCommand]
    private async void GoToDetails(ThirdNewsItemDto item)
    {
        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            { "News", item }
        });

    }
}
