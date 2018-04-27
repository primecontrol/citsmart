using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CITSmart.PageObjects
{
    public class SmartDecisionsPage : TestBase
    {
        #region Elements

        public static By RequisicaoDeServicosEIncidentes(int timeoutSeconds = 10)
        {
            return By.XPath("//span[text()='Requisição de Serviços e Incidentes']");
        }

        public static By GerenciaDeRequisicaoEIncidente(int timeoutSeconds = 10)
        {
            return By.XPath("//span[text()='Gerência de Requisição e Incidente']");
        }

        public static By ProcessosItil(int timeoutSeconds = 10)
        {
            return By.XPath("//*[@id='nav']/li[1]/a/span");
        }

        #endregion

        #region Actions

        #region Clicks

        public static void ClickProcessosItil(int timeoutSeconds = 10)
        {
            Logger = "Click Processos ITIL";
            if (WaitElement(ProcessosItil(), timeoutSeconds))
            {
                GetElement(ProcessosItil(), timeoutSeconds).Click();
            }
        }

        public static void ClickRequisicaoDeServicosEIncidentes(int timeoutSeconds = 10)
        {
            Logger = "Click Requisição De Serviços E Incidentes";
            if (WaitElement(RequisicaoDeServicosEIncidentes(), timeoutSeconds))
            {
                GetElement(RequisicaoDeServicosEIncidentes(), timeoutSeconds).Click();
            }
        }

        public static void ClickGerenciaDeRequisicaoEIncidentes(int timeoutSeconds = 10)
        {
            Logger = "Click Gerência de Requisição e Incidente";
            if (WaitElement(GerenciaDeRequisicaoEIncidente(), timeoutSeconds))
            {
                GetElement(GerenciaDeRequisicaoEIncidente(), timeoutSeconds).Click();
            }
        }

        public static void ClickAcessoAoSistema(int timeoutSeconds = 10)
        {
            Logger = "Click Acesso Ao Sistema";
            if (WaitElement(ProcessosItil(), timeoutSeconds))
            {
                GetElement(ProcessosItil(), timeoutSeconds).Click();
            }
        }

        #endregion

        #endregion
    }
}