﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);

        bool VerificarSeEmailJaExiste(string email);
    }
}
