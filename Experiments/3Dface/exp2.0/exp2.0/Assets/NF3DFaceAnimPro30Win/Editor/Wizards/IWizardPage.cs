namespace Assets.Scripts.NFEditor
{
    public interface IWizardPage
    {
        void OnNext();

        void OnBack();

        void OnReset();
    }
}