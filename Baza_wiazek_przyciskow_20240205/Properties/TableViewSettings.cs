using System.Configuration;

namespace Baza_wiazek_przyciskow_20240205.Properties;

internal sealed partial class Settings
{
    [UserScopedSetting, DefaultSettingValue("False")]
    public bool ExcelTableView
    {
        get => (bool)this[nameof(ExcelTableView)];
        set => this[nameof(ExcelTableView)] = value;
    }
}
