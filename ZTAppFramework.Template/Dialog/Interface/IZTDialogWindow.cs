namespace ZTAppFramework.Template.Dialog
{
    internal interface IZTDialogWindow
    {
        /// <summary>
        /// 内容
        /// </summary>
        object Content { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        IZTDialogResult Result { get; set; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        object DataContext { get; set; }
    }
}