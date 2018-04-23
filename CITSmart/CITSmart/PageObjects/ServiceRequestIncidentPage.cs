using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CITSmart.PageObjects
{
    public class ServiceRequestIncidentPage : TestBase
    {
        #region Elements

        public static By Fechada(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='tabelaSolicitacoes']/div/table/tbody/tr/td[12]");
        }

        public static By Pesquisar(int timeoutSeconds = 10)
        {
            return By.Name("btnPesquisar");
        }

        public static By Numero(int timeoutSeconds = 10)
        {
            return By.Id("idSolicitacaoServicoPesquisa");
        }

        public static By PesquisaAvancada(int timeoutSeconds = 10)
        {
            return By.Id("aba-pesquisa-avancada");
        }

        public static By MenuTicketsTarefas(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='service-request-incident-container']/div[1]/button");
        }

        public static By NenhumRegistroEncontrado(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='service-request-list-container']/div[1]/div/div[1]");
        }

        public static By GravarEAvancarFluxo(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='button-request-gravar-avancar']/button");
        }

        public static By Opcoes(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='button-request-opcoes']/div[2]/button");
        }

        public static By SolucaoResposta(int timeoutSeconds = 10)
        {
            return By.XPath("/html/body");
        }

        public static By DetalhamentoDaCausa(int timeoutSeconds = 10)
        {
            return By.XPath("/html/body");
        }

        public static By CategoriaDeSolucao(int timeoutSeconds = 10)
        {
            return By.Id("select-request-solucao");
        }

        public static By Causa(int timeoutSeconds = 10)
        {
            return By.Id("select-request-causa");
        }

        public static By Resolvida(int timeoutSeconds = 10)
        {
            return By.Id("radio-request-resolvida");
        }

        public static By ConfirmacaoSim(int timeoutSeconds = 10)
        {
            return By.XPath("/html/body/div[1]/div/div/div[3]/button[1]");
        }

        public static By Abrir(int timeoutSeconds = 10)
        {
            return By.Id("list-item-options-open");
        }

        public static By Ticket(int timeoutSeconds = 10)
        {
            return By.XPath("//div[@class='tableless-td numero']");
        }

        public static By PesquiseAqui(int timeoutSeconds = 10)
        {
            return By.Id("pesquisaSolicitacao");
        }

        #endregion

        #region Actions

        #region Clicks

        public static void ClickFechada(int timeoutSeconds = 10)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(By.Id("iframeModal")));

            Logger = "Click Fechada";
            if (WaitElement(Fechada(), timeoutSeconds))
            {
                GetElement(Fechada(), timeoutSeconds).Click();
            }

            Driver.SwitchTo().DefaultContent();
        }

        public static void ClickPesquisar(int timeoutSeconds = 10)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(By.Id("iframeModal")));

            Logger = "Click Pesquisar";
            if (WaitElement(Pesquisar(), timeoutSeconds))
            {
                GetElement(Pesquisar(), timeoutSeconds).Click();
            }

            Driver.SwitchTo().DefaultContent();
        }

        public static void ClickPesquisaAvancada(int timeoutSeconds = 10)
        {
            Logger = "Click Pesquisa Avançada";
            if (WaitElement(PesquisaAvancada(), timeoutSeconds))
            {
                GetElement(PesquisaAvancada(), timeoutSeconds).Click();
            }
        }

        public static void ClickMenuTicketsTarefas(int timeoutSeconds = 10)
        {
            Logger = "Click Menu Tickets/Tarefas";
            if (WaitElement(MenuTicketsTarefas(), timeoutSeconds))
            {
                GetElement(MenuTicketsTarefas(), timeoutSeconds).Click();
            }
        }

        public static void ClickNenhumRegistroEncontrado(int timeoutSeconds = 10)
        {
            Logger = "Click Nenhum Registro Encontrado";
            if (WaitElement(NenhumRegistroEncontrado(), timeoutSeconds))
            {
                GetElement(NenhumRegistroEncontrado(), timeoutSeconds).Click();
            }
        }

        public static void ClickGravarEAvancarFluxo(int timeoutSeconds = 10)
        {
            Logger = "Click Gravar e Avançar fluxo";
            if (WaitElement(GravarEAvancarFluxo(), timeoutSeconds))
            {
                GetElement(GravarEAvancarFluxo(), timeoutSeconds).Click();
            }
        }

        public static void ClickOpcoes(int timeoutSeconds = 10)
        {
            Logger = "Click Opções";
            if (WaitElement(Opcoes(), timeoutSeconds))
            {
                GetElement(Opcoes(), timeoutSeconds).Click();
            }
        }

        public static void ClickResolvida(int timeoutSeconds = 10)
        {
            Logger = "Click Resolvida";
            if (WaitElement(Resolvida(), timeoutSeconds))
            {
                GetElement(Resolvida(), timeoutSeconds).Click();
            }
        }

        public static void ClickConfirmacaoSim(int timeoutSeconds = 10)
        {
            Logger = "Click Sim em Comfirmação";
            if (WaitElement(ConfirmacaoSim(), timeoutSeconds))
            {
                GetElement(ConfirmacaoSim(), timeoutSeconds).Click();
            }
        }

        public static void ClickAbrir(int timeoutSeconds = 10)
        {
            Logger = "Click Abrir";
            if (WaitElement(Abrir(), timeoutSeconds))
            {
                GetElement(Abrir(), timeoutSeconds).Click();
            }
        }

        public static void ClickTicket(int timeoutSeconds = 10)
        {
            Logger = "Click Ticket";
            if (WaitElement(Ticket(), timeoutSeconds))
            {
                GetElement(Ticket(), timeoutSeconds).Click();
            }
        }

        #endregion

        #region Selects

        public static void SelectCategoriaDeSolucao(int timeoutSeconds = 10)
        {
            Logger = "Select Categoria De Solução";
            if (WaitElement(CategoriaDeSolucao(), timeoutSeconds))
            {
                new SelectElement(GetElement(CategoriaDeSolucao())).SelectByText("Backup e Restore");
            }
        }

        public static void SelectCausa(int timeoutSeconds = 10)
        {
            Logger = "Select Causa";
            if (WaitElement(Causa(), timeoutSeconds))
            {
                new SelectElement(GetElement(Causa())).SelectByText("Hardware");
            }
        }

        #endregion

        #region SendKeys

        public static void SetNumero(string text, int timeoutSeconds = 10)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(By.Id("iframeModal")));

            Logger = "Set Número: " + text;
            if (WaitElement(Numero(), timeoutSeconds))
            {
                GetElement(Numero(), timeoutSeconds).SendKeys(text);
            }

            Driver.SwitchTo().DefaultContent();
        }

        public static void SetSolucaoResposta(string text, int timeoutSeconds = 10)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(By.XPath("//*[@id='cke_3_contents']/iframe")));

            Logger = "Set Solução Resposta: " + text;
            if (WaitElement(SolucaoResposta(), timeoutSeconds))
            {
                GetElement(SolucaoResposta(), timeoutSeconds).SendKeys(text);
            }

            Driver.SwitchTo().DefaultContent();
        }

        public static void SetDetalhamentoDaCausa(string text, int timeoutSeconds = 10)
        {
            Driver.SwitchTo().Frame(Driver.FindElement(By.XPath("//*[@id='cke_2_contents']/iframe")));

            Logger = "Set Detalhamento Da Causa: " + text;
            if (WaitElement(DetalhamentoDaCausa(), timeoutSeconds))
            {
                GetElement(DetalhamentoDaCausa(), timeoutSeconds).SendKeys(text);
            }

            Driver.SwitchTo().DefaultContent();
        }

        public static void SetPesquiseAqui(string text, int timeoutSeconds = 10)
        {
            Logger = "Set Pesquise Aqui: " + text;
            if (WaitElement(PesquiseAqui(), timeoutSeconds))
            {
                GetElement(PesquiseAqui(), timeoutSeconds).SendKeys(text);
            }
        }

        #endregion

        #endregion
    }
}
