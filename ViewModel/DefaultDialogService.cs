using Sanator.Interfaces;

namespace Sanator.ViewModel
{
    internal class DefaultDialogService : IDialogService
    {
        public string FilePath { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public bool OpenFileDialog()
        {
            throw new System.NotImplementedException();
        }

        public bool SaveFileDialog()
        {
            throw new System.NotImplementedException();
        }

        public void ShowMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public bool ShowMessageOKCancel(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}