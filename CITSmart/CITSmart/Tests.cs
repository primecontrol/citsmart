using System;
using System.IO;
using CITSmart.PageObjects;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CITSmart
{
    [CodedUITest]
    [DeploymentItem(@"CITSmart\Drivers")]
    [DeploymentItem(@"CITSmart\Data")]
    public class Tests : TestBase
    {
        [TestMethod]
        public void AtenderUmaSolicitacaoDeServico()
        {
            string ticket = "";

            using (StreamReader sr =
                new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ticket.txt")))
            {
                ticket = sr.ReadLine();
            }

            Test.Info("Scenario #2: Atender uma Solicitação de Serviço");
            EfetuarLogin("HELLEN", "1");
            SmartPortalPage.ClickAcessoAoSistema();
            SmartDecisionsPage.ClickProcessosItil();
            SmartDecisionsPage.ClickGerenciaDeRequisicaoEIncidentes();
            SmartDecisionsPage.ClickRequisicaoDeServicosEIncidentes();
            ServiceRequestIncidentPage.SetPesquiseAqui(ticket);
            Checkpoint(!WaitElement(ServiceRequestIncidentPage.NenhumRegistroEncontrado()), "Pesquisar: " + ticket);
            ServiceRequestIncidentPage.ClickTicket();
            ServiceRequestIncidentPage.ClickAbrir();
            ServiceRequestIncidentPage.ClickConfirmacaoSim();
            ServiceRequestIncidentPage.ClickResolvida();
            ServiceRequestIncidentPage.SelectCausa();
            ServiceRequestIncidentPage.SelectCategoriaDeSolucao();
            ServiceRequestIncidentPage.SetDetalhamentoDaCausa("Detalhamento da Causa " + DateTime.Now.ToString("dd-MM-yyyyThh:mm:ss"));
            ServiceRequestIncidentPage.SetSolucaoResposta("Solução Resposta " + DateTime.Now.ToString("dd-MM-yyyyThh:mm:ss"));
            ServiceRequestIncidentPage.ClickOpcoes();
            ServiceRequestIncidentPage.ClickGravarEAvancarFluxo();
            ServiceRequestIncidentPage.ClickNenhumRegistroEncontrado();
            ServiceRequestIncidentPage.ClickMenuTicketsTarefas();
            ServiceRequestIncidentPage.ClickPesquisaAvancada();
            ServiceRequestIncidentPage.SetNumero(ticket);
            ServiceRequestIncidentPage.ClickPesquisar();
            ServiceRequestIncidentPage.ClickFechada();
        }

        [TestMethod]
        public void CadastrarUmaSolicitacaoPeloPortal()
        {
            Test.Info("Scenario #1: Cadastrar uma solicitação pelo Portal");
            EfetuarLogin("LUCAS", "1");
            SmartPortalPage.ClickPortifolio();
            SmartPortalPage.ClickServicoDeNegocio();
            SmartPortalPage.ClickRequisicao();
            SmartPortalPage.SetDescricao("Descrição " + DateTime.Now.ToString("dd-MM-yyyyThh:mm:ss"));
            SmartPortalPage.ClickConcluir();
            Checkpoint(WaitElement(SmartPortalPage.SolicitacoesCriadas()), GetElement(SmartPortalPage.SolicitacoesCriadas()).Text);

            var ticket = GetElement(SmartPortalPage.SolicitacoesCriadas()).Text.Split('\n')[1].Split('\r')[0];
            var dataSource = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { "TestResults" }, StringSplitOptions.None)[0],
                @"CITSmart\Data\Ticket.txt");

            using (StreamWriter sw = new StreamWriter(dataSource, false))
            {
                sw.Write(ticket);
            }
        }

        public void EfetuarLogin(string usuario, string senha)
        {
            User = usuario;
            Password = senha;
            Login(LoginPage.Usuario(), LoginPage.Senha(), LoginPage.Entrar(), SmartPortalPage.PesquisaPortal());
        }
    }
}