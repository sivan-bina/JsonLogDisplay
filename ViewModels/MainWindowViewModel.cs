using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonDisplay.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        string _jsonInput = "";

        [ObservableProperty]
        Dictionary<string, object> _parsed = new();

        partial void OnParsedChanged(Dictionary<string, object> value)
        {
            var collapsible = new CollapsibleViewer(value);
            collapsible.Show();
        }

        [RelayCommand]
        void Deserialize()
        {
            var converter = new DictionaryConverter();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonInput, converter);
            if (dict is not null)
            {
                Parsed = dict;
            }
        }


    }
}
