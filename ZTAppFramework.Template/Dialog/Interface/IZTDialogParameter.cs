namespace ZTAppFramework.Template.Dialog
{
    public interface IZTDialogParameter
    {
        void Add(string key, object value);
        T GetValue<T>(string key);
    }
}