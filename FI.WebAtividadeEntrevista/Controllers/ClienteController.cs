using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        { 
            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;

                    return Json(string.Join(Environment.NewLine, erros));
                }

                BoCliente boCliente = new BoCliente();

                if (boCliente.VerificarExistencia(model.CPF))
                {
                    Response.StatusCode = 400;

                    return Json(new { Result = "ERROR", Message = string.Format("Já existe cliente cadastrado de CPF: {0}", model.CPF) });
                }

                List<string> cpfsRepetidos = model.Beneficiarios.GroupBy(x => x.CPF).Where(y => y.Count() > 1).Select(z => z.Key).ToList();

                if (cpfsRepetidos.Any())
                {
                    Response.StatusCode = 400;

                    return Json(new { Result = "ERROR", Message = $"Existem beneficiários com o mesmo CPF: {string.Join(", ", cpfsRepetidos)}" });
                }

                Cliente cliente = new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                };

                model.Id = boCliente.Incluir(cliente);

                if (model.Beneficiarios.Count > 0)
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();

                    foreach (BeneficiarioModel beneficiarioModel in model.Beneficiarios)
                    {
                        if (beneficiarioModel == null)
                            continue;

                        Beneficiario beneficiario = new Beneficiario()
                        {
                            Nome = beneficiarioModel.Nome,
                            CPF = beneficiarioModel.CPF
                        };

                        beneficiario.Id = boBeneficiario.Incluir(beneficiario);
                    }
                }

                return Json("Cadastro efetuado com sucesso!");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;

                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente boCliente = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;

                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                boCliente.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });

                return Json("Cadastro alterado com sucesso!");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}