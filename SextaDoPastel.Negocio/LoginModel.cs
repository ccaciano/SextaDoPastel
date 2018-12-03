using SextaDoPastel.Dado;
using SextaDoPastel.Dominio;
using SextaDoPastel.Dominio.Excecoes;
using System;
using System.Collections.Generic;

namespace SextaDoPastel.Negocio
{
    public class LoginModel
    {
        private readonly LoginDados _loginDados;

        public LoginModel()
        {
            _loginDados = new LoginDados();
        }

        public IEnumerable<Login> Selecionar()
        {
            var lista = _loginDados.Selecionar();

            if (lista == null)
                throw new NaoEncontradoException();

            return lista;
        }

        public Login SelecionarPorId(int id)
        {
            var obj = _loginDados.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException();

            return obj;
        }

        public Login SelecionarPorEmail(string email)
        {
            var obj = _loginDados.SelecionarPorEmail(email);

            if (obj == null)
                throw new NaoEncontradoException();

            return obj;
        }

        public Login SelecionarPorNick(string nick)
        {
            var obj = _loginDados.SelecionarPorNick(nick);

            if (obj == null)
                throw new NaoEncontradoException();

            return obj;
        }

        // ALTERAR A FORMA DE LOGIN PARA EMAIL E OU NICKNAME
        public Login EfetuarLogin(string login, string senha)
        {
            var objEmail = _loginDados.EfetuarLoginEmail(login, senha);
            var objNick = _loginDados.EfetuarLoginNick(login, senha);

            if (objEmail != null)
            {
                return objEmail;
            }
            else if (objNick != null)
            {
                return objNick;
            }
            else
            {
                throw new NaoEncontradoException();
            }
        }

        public int Inserir(Login entity)
        {
            var emailExistente = _loginDados.SelecionarPorEmail(entity.EMAIL);
            var nickExistente = _loginDados.SelecionarPorNick(entity.NICK);

            if (emailExistente != null)
            {
                throw new ConflitoException($"Já existe cadastrado o EMAIL {emailExistente.EMAIL}, para outro Login!");
            }
            else if (nickExistente != null)
            {
                throw new ConflitoException($"Já existe cadastrado o NICK {emailExistente.NICK}, para outro Login!");
            }

            return _loginDados.Inserir(entity);
        }

        public Login AlterarSenha(int id, Login entity)
        {
            entity.ID_LOG = id;
            _loginDados.AlterarSenha(entity);

            return _loginDados.SelecionarPorId(id);
        }

        public Login AlterarEmailNick(int id, Login entity)
        {
            entity.ID_LOG = id;
            _loginDados.AlterarEmailNick(entity);

            return _loginDados.SelecionarPorId(id);
        }

        public Login AlterarStatus(int id, Login entity)
        {
            entity.ID_LOG = id;
            _loginDados.AlterarStatus(entity);

            return _loginDados.SelecionarPorId(id);
        }

        public Login AlterarPerfil(int id, Login entity)
        {
            entity.ID_LOG = id;
            _loginDados.AlterarPerfil(entity);

            return _loginDados.SelecionarPorId(id);
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		public void Deletar(int id)
        {
            var obj = _loginDados.SelecionarPorId(id);

            _loginDados.Deletar(obj.ID_LOG);
        }
    }
}
