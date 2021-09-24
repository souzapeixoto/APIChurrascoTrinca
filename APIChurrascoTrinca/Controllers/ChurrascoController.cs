using Application.DTO;
using Application.InputModel;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChurrascoTrinca.Controllers
{
    [Route("api/Churrascos")]
    public class ChurrascoController : Controller
    {
        private readonly IRepositoryOpcao _repositoryOpcao;
        private readonly IRepositoryConvidado _repositoryConvidado;
        private readonly IRepositoryChurrasco _repositoryChurrasco;
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly ILogger<ChurrascoController> _logger;


        public ChurrascoController(
        IUnitOfWork unitOfWork,
        IRepositoryOpcao repositoryOpcao,
        IRepositoryConvidado repositoryConvidado,
        IRepositoryUsuario repositoryUsuario,
        ILogger<ChurrascoController> logger,
        IRepositoryChurrasco repositoryChurrasco,
        IMapper map)
        {
            _repositoryConvidado = repositoryConvidado;
            _repositoryOpcao = repositoryOpcao;
            _unitOfWork = unitOfWork;
            _repositoryUsuario = repositoryUsuario;
            _logger = logger;
            _map = map;
            _repositoryChurrasco = repositoryChurrasco;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            try
            {
                var usuario = _repositoryUsuario.UsuarioLogado();
                var churrascos = _repositoryChurrasco.GetAll(x => x.Usuario.Id == usuario.Id);

                ICollection<DTOChurrasco> response =
                   _map.Map<List<DTOChurrasco>>(churrascos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            var churrascos = _repositoryChurrasco.GetAll(x => x.Usuario.Id == usuario.Id).Where(x => x.Id == id);
            if (churrascos.Count() <= 0)
                return NoContent();

            DTOChurrasco response = _map.Map<DTOChurrasco>(churrascos.First());
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(ChurrascoCreateInputModel model)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();

            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }
            try
            {
                var churrasco = _map.Map<Churrasco>(model);
                churrasco.Usuario = usuario;
                _repositoryChurrasco.Insert(churrasco);
                _unitOfWork.Commit();
                return Ok("Churrasco criado com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível criar o churrasco :(");
            }
        }

        [Authorize]
        [HttpPut("{idChurrasco}")]
        public async Task<ActionResult> Put(ChurrascoUpdateInputModel model, int idChurrasco)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }
            var churrasco = _repositoryChurrasco.GetById(idChurrasco);
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return NoContent();
            }

            try
            {
                churrasco = _map.Map(model, churrasco);
                _repositoryChurrasco.Update(churrasco);
                _unitOfWork.Commit();
                return Ok("Churrasco atualizado com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível atualizar o churrasco :(");
            }
        }

        [Authorize]
        [HttpDelete("{idChurrasco}")]
        public async Task<ActionResult> Delete(int idChurrasco)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var churrasco = _repositoryChurrasco.GetById(idChurrasco);
            //var churrasco = _repositoryChurrasco.GetAll(x => x.Id == opcao.Churrasco.Id).First();
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return NoContent();
            }
            try
            {
                _repositoryChurrasco.Delete(churrasco);
                _unitOfWork.Commit();
                return Ok("Churrasco excluído com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível excluir o Churrasco :(");
            }
        }

        [Authorize]
        [HttpPut("{idChurrasco}/Convidados/{idconvidado}")]
        public async Task<ActionResult> Put(ChurrascoUpdateConvidadoInputModel model, int idChurrasco, int idConvidado)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var convidado = _repositoryConvidado.GetById(idConvidado);
            var churrasco = _repositoryChurrasco.GetAll(x => x.Id == convidado.Churrasco.Id).First();
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                convidado = _map.Map(model, convidado);
                convidado.Opcao = _repositoryOpcao.GetById(model.IdOpcao);
                _repositoryConvidado.Update(convidado);
                _unitOfWork.Commit();
                return Ok("Convidado atualizado com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível atualizar o convidado :(");
            }
        }

        [Authorize]
        [HttpPost("{idChurrasco}/Convidados")]
        public async Task<ActionResult> Post(ChurrascoCreateConvidadoInputModel model, int idChurrasco)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var churrasco = _repositoryChurrasco.GetById(idChurrasco);
            if (churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                var convidado = _map.Map<Convidado>(model);
                convidado.Opcao = _repositoryOpcao.GetById(model.IdOpcao);
                convidado.Churrasco = churrasco;
                _repositoryConvidado.Insert(convidado);
                _unitOfWork.Commit();
                return Ok("Convidado adicionado com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível adicionar o convidado :(");
            }
        }

        [Authorize]
        [HttpDelete("{idChurrasco}/Convidados/{idconvidado}")]
        public async Task<ActionResult> Delete(int idChurrasco, int idConvidado)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var convidado = _repositoryConvidado.GetById(idConvidado);
            var churrasco = _repositoryChurrasco.GetAll(x => x.Id == convidado.Churrasco.Id).First();
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                _repositoryConvidado.Delete(convidado);
                _unitOfWork.Commit();
                return Ok("Convidado excluído com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível excluir o convidado :(");
            }
        }

        [Authorize]
        [HttpPost("{idChurrasco}/Opcoes")]
        public async Task<ActionResult> Post(ChurrascoCreateOpcaoInputModel model, int idChurrasco)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var churrasco = _repositoryChurrasco.GetById(idChurrasco);
            if (churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                var opcao = _map.Map<Opcao>(model);
                opcao.Churrasco = churrasco;
                _repositoryOpcao.Insert(opcao);
                _unitOfWork.Commit();
                return Ok("Opção adicionada com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível adicionar a Opção :(");
            }
        }

        [Authorize]
        [HttpPut("{idChurrasco}/Opcoes/{idOpcao}")]
        public async Task<ActionResult> Put(ChurrascoUpdateOpcaoInputModel model, int idChurrasco, int idOpcao)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var opcao = _repositoryOpcao.GetById(idOpcao);
            var churrasco = _repositoryChurrasco.GetAll(x => x.Id == opcao.Churrasco.Id).First();
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                opcao = _map.Map(model, opcao);
                _repositoryOpcao.Update(opcao);
                _unitOfWork.Commit();
                return Ok("Opção atualizada com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível atualizar a Opção :(");
            }
        }
        [Authorize]
        [HttpDelete("{idChurrasco}/Opcoes/{idOpcao}")]
        public async Task<ActionResult> DeleteOpcao(int idChurrasco, int idOpcao)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();
            if (!ModelState.IsValid)
            {
                _logger.LogError(ModelState.Values.SelectMany(x => x.Errors).ToString());
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }

            var opcao = _repositoryOpcao.GetById(idOpcao);
            var churrasco = _repositoryChurrasco.GetAll(x => x.Id == opcao.Churrasco.Id).First();
            if (churrasco == null || churrasco.Usuario.Id != usuario.Id)
            {
                return Unauthorized();
            }
            try
            {
                _repositoryOpcao.Delete(opcao);
                _unitOfWork.Commit();
                return Ok("Opção excluída com sucesso!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Não foi possível excluir a Opção :(");
            }
        }
    }
}
