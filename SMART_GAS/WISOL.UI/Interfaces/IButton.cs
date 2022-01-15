namespace Wisol.MES.Interfaces
{
    interface IButton
    {
        void InitializePage();
        void SearchPage();
        void LoadCodes();
        void SaveCodes();
        void LoadLayout();
        void SaveLayout();
        void OpenChart();
        void ClosePage();
        void CloseAll();
        void OpenManual();
    }
}
