using Avalonia;
using Avalonia.Controls;
using System.Collections.Generic;
using System.Linq;

namespace JsonDisplay;

public partial class CollapsibleViewer : Window
{
    public CollapsibleViewer(Dictionary<string, object> parsedJson)
    {
        FlowDirection = Avalonia.Media.FlowDirection.RightToLeft;
        InitializeComponent();
        Grid grid;
        var scroll = new ScrollViewer()
        {
            Content = grid = new Grid()
        };
        grid.Children.Add(CreateCollapsible(parsedJson));
        this.Content = scroll;
    }

    private StackPanel CreateKV(string k, string v)
    {
        var innerSp = new StackPanel()
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            Spacing = 5,
        };
        innerSp.Children.Add(new TextBlock() { VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Text = k });
        innerSp.Children.Add(new TextBox() { Text = v.ToString(), IsReadOnly = true });
        return innerSp;
    }

    private StackPanel CreateCollapsible(Dictionary<string, object> parsedJson)
    {
        var sp = new StackPanel();
        var innerSp = new StackPanel()
        {
            Orientation = Avalonia.Layout.Orientation.Horizontal,
            Margin = new Thickness(10),
            Spacing = 10
        };
        foreach (var (k, v) in parsedJson.Where((kv) => kv.Value is not Dictionary<string, object>))
        {
            innerSp.Children.Add(CreateKV(k, v?.ToString() ?? "null"));
        }
        if (innerSp.Children.Any())
        {
            sp.Children.Add(innerSp);
        }
        foreach (var (k, v) in parsedJson.Where((kv) => kv.Value is Dictionary<string, object>))
        {
            sp.Children.Add(new Expander()
            {
                IsExpanded = true,
                Header = k,
                Padding = new Avalonia.Thickness(20, 0, 0, 0),
                Content = CreateCollapsible((Dictionary<string, object>)v),
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
            });
        }
        return sp;
    }
}