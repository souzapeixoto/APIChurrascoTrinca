using AutoMapper;
using Domain.DTO;
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
        private readonly IRepositoryChurrasco _repositoryChurrasco;
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        private readonly ILogger _logger;


        public ChurrascoController(
        IUnitOfWork unitOfWork,
        IRepositoryUsuario repositoryUsuario,
        ILogger logger,
        IRepositoryChurrasco repositoryChurrasco,
        IMapper map)
        {
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
                var churrascos =  _repositoryChurrasco.GetAll(x => x.Usuario.Id == usuario.Id);

                ICollection<DTOChurrasco> response =
                    _map.Map<List<DTOChurrasco>>(churrascos);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
           
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(DTOChurrasco model)
        {
            var usuario = _repositoryUsuario.UsuarioLogado();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
            }
            try
            {
                var churrasco = _map.Map<Churrasco>(model);
                churrasco.Usuario = usuario;
                _repositoryChurrasco.Insert(churrasco);
                _unitOfWork.Commit();
                return Ok(new { message = "Churrasco criado com sucesso!"});

            }
            catch (Exception e)
            {
                return BadRequest(new {message = "Não foi possível criar o churrasco :(" });

            }
        }
    }
}
