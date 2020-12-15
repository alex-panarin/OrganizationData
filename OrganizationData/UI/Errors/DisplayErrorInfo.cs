using System.Windows.Forms;

namespace OrganizationData.UI.Errors
{
    internal class DisplayErrorInfo : IDisplayErrorInfo
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK);
        }

        public void ShowInfo(string info)
        {
            MessageBox.Show(info, "Сообщение", MessageBoxButtons.OK);
        }
    }
}
