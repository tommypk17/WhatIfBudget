namespace WhatIfBudget.App.Models;

public class AADInfo
{
    public string URL { get; set; }
    public string ClientId { get; set; }
    public string Authority { get; set; }
    public string RedirectUri { get; set; }
    public string KnownAuthorities { get; set; }
    public string DefaultScope { get; set; }
}