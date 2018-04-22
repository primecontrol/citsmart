using OpenQA.Selenium;

namespace CITSmart.PageObjects
{
    public class SmartPortalPage : TestBase
    {
        #region Elements

        public static By SolicitacoesCriadas(int timeoutSeconds = 10)
        {
            return By.Id("solicitacoes-criadas-content");
        }

        public static By Concluir(int timeoutSeconds = 10)
        {
            return By.Id("btn-add-servico-and-finish");
        }

        public static By Descricao(int timeoutSeconds = 10)
        {
            return By.Id("solicitacaoObservacao");
        }

        public static By Requisicao(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='atividades-content']/div");
        }

        public static By ServicoDeNegocio(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='servicos-content']/div");
        }

        public static By PesquisaPortal(int timeoutSeconds = 10)
        {
            return By.Id("pesquisaPortal");
        }

        public static By Portifolio(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='gerenciamento-servicos-content']/div/div");
        }

        #endregion

        #region Actions

        #region Clicks

        public static void ClickConcluir(int timeoutSeconds = 10)
        {
            Logger = "Click Concluir";
            if (WaitElement(Concluir(), timeoutSeconds))
            {
                GetElement(Concluir(), timeoutSeconds).Click();
            }
        }

        public static void ClickRequisicao(int timeoutSeconds = 10)
        {
            Logger = "Click Requisicao";
            if (WaitElement(Requisicao(), timeoutSeconds))
            {
                GetElement(Requisicao(), timeoutSeconds).Click();
            }
        }

        public static void ClickServicoDeNegocio(int timeoutSeconds = 10)
        {
            Logger = "Click Serviço De Negócio";
            if (WaitElement(ServicoDeNegocio(), timeoutSeconds))
            {
                GetElement(ServicoDeNegocio(), timeoutSeconds).Click();
            }
        }

        public static void ClickPortifolio(int timeoutSeconds = 10)
        {
            Logger = "Click Entrar";
            if (WaitElement(Portifolio(), timeoutSeconds))
            {
                GetElement(Portifolio(), timeoutSeconds).Click();
            }
        }

        #endregion

        #region SendKeys

        public static void SetDescricao(string text,int timeoutSeconds = 10)
        {
            Logger = "Set Descrição: " + text;
            if (WaitElement(Descricao(), timeoutSeconds))
            {
                GetElement(Descricao(), timeoutSeconds).SendKeys(text);
            }
        }

        #endregion

        #endregion
    }
}