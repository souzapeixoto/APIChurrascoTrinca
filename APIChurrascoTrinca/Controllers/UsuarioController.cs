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
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IMapper _map;
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(
        IRepositoryUsuario repositoryUsuario,
        ILogger<UsuarioController> logger,
        IMapper map)
        {
            _repositoryUsuario = repositoryUsuario;
            _logger = logger;
            _map = map;
        }
        [Authorize]
        [HttpGet]
        public DTOUsuario Get()
        {
            var user = _repositoryUsuario.UsuarioLogado();
            var uservm = _map.Map<DTOUsuario>(user);
            return uservm;
        }

        
    }
}
