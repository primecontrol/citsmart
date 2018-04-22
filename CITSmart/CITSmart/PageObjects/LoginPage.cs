using OpenQA.Selenium;

namespace CITSmart.PageObjects
{
    public class LoginPage : TestBase
    {
        #region Elements

        public static By Usuario(int timeoutSeconds = 10)
        {
            return By.Id("user");
        }

        public static By Senha(int timeoutSeconds = 10)
        {
            return By.Id("senha");
        }

        public static By Entrar(int timeoutSeconds = 10)
        {
            return By.Id("btnEntrar");
        }

        #endregion

        #region Actions

        #region Clicks

        public static void ClickEntrar(int timeoutSeconds = 10)
        {
            Logger = "Click Entrar";
            if (WaitElement(Entrar(), timeoutSeconds))
            {
                GetElement(Entrar(), timeoutSeconds).Click();
            }
        }

        #endregion

        #region SendKeys

        public static void SetSenha(string text, int timeoutSeconds = 10)
        {
            Logger = "Set Senha: " + text;
            if (WaitElement(Senha(), timeoutSeconds))
            {
                GetElement(Senha(), timeoutSeconds).SendKeys(text);
            }
        }

        public static void SetUsuario(string text, int timeoutSeconds = 10)
        {
            Logger = "Set Usuário: " + text;
            if (WaitElement(Usuario(), timeoutSeconds))
            {
                GetElement(Usuario(), timeoutSeconds).SendKeys(text);
            }
        }

        #endregion

        #endregion
    }
}