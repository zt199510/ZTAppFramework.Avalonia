namespace ZTAppFramework.Template.Dialog
{
    public interface ILayDialogParameter
    {
        void Add(string key, object value);
        T GetValue<T>(string key);
    }
}