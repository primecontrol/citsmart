using System;
using CITSmart.PageObjects;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace CITSmart
{
    [CodedUITest]
    [DeploymentItem(@"CITSmart\Drivers")]
    [DeploymentItem(@"CITSmart\Data")]
    public class Tests : TestBase
    {
        [TestMethod]
        public void CadastrarUmaSolicitacaoPeloPortal()
        {
            EfetuarLogin("LUCAS", "1");
            SmartPortalPage.ClickPortifolio();
            SmartPortalPage.ClickServicoDeNegocio();
            SmartPortalPage.ClickRequisicao();
            SmartPortalPage.SetDescricao("Descrição " + DateTime.Now.ToString("dd-MM-yyyyThh:mm:ss"));
            SmartPortalPage.ClickConcluir();
            Checkpoint(WaitElement(SmartPortalPage.SolicitacoesCriadas()), GetElement(SmartPortalPage.SolicitacoesCriadas()).Text);
        }

        public void EfetuarLogin(string usuario, string senha)
        {
            User = usuario;
            Password = senha;
            Login(LoginPage.Usuario(), LoginPage.Senha(), LoginPage.Entrar(), SmartPortalPage.PesquisaPortal());
        }
    }
}